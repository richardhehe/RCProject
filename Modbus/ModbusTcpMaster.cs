using Communication;
using Modbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



//ModbusTcpMaster = 通过 TCP Socket 实现 Modbus 协议的主站

//    它做了 5 件核心事情：
//1.建立 TCP 连接
//2.组装 Modbus TCP 报文（MBAP + PDU）
//3.发送请求
//4.等待 PLC 响应
//5.校验并解析数据
namespace Modbus
{
    internal class ModbusTcpMaster : ModbusMaster
    {

        // PLC 的 IP、端口信息
        private ServerInfo serverInfo;

        //线程同步器:初始为 false. 用来：“发送后阻塞 → 收到数据后唤醒”
        private ManualResetEvent mreEvent = new ManualResetEvent(false);

        //TCP Socket 客户端（第三方 / 封装类）
        private HTCOMMSocketClient client;

        //保存 PLC 返回的数据
        private byte[] recvBuffer = null;

        //  线程锁,保证 Read / Write 不能同时执行,防止响应错乱
        private object lockObj = new object();

        public ModbusTcpMaster(ServerInfo info)
        {
            //保存 PLC 地址信息
            serverInfo = info;
            //创建 Socket 客户端
            client = new HTCOMMSocketClient();
            //注册 接收数据事件
            client.ReceiveData += socketClicent_DataReceived;
            //立即连接 PLC
            Connect();
        }

        //保证每次 Read / Write 前都是已连接状态
        protected override void Connect()
        {
            if (client.Connected == false)
            {
                client.Connect(serverInfo.IP, serverInfo.Port);
            }
        }


        //返回当前连接状态（给上位机 UI 用）
        public override bool ConnectPlc()
        {

            if (client.Connected == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //主动断开 TCP
        public override  bool DisConnectPlc()
        {

            client.Disconnect();
            return true;
        }


        //接收数据事件
        private void socketClicent_DataReceived(object sender, SocketEventArgs e)
        {
            recvBuffer = null;
            //接收数据事件
            recvBuffer = e.Datas;
            if (recvBuffer != null)
            {
                //通知 Read / Write：数据已到达
                mreEvent.Set();
                //logManger.WriteInfo("Received:" + string.Join(" ", recvBuffer.ToArray()));
            }
        }


        //Modbus TCP 的 Transaction Identifier
        //每次请求递增, 用来区分：本次响应是不是“我刚才发的请求”
        protected byte DataIndex { get; set; }


        protected override byte[] Read(byte slaveAddress, byte functionCode, ushort startingAddress, ushort registerNumber)
        {
            try
            {
                //防止：一个线程在 Read, 另一个线程在 Write, 响应包对不上
                //Read与Write不能同时执行
                Monitor.Enter(lockObj); 
                this.Connect();


                //创建数据包并发送,  Modbus TCP 报文
                List<byte> sendData = new List<byte>();
                sendData.AddRange(ValueHelper.Instance.GetBytes((ushort)(++DataIndex)));//1~2.(Transaction Identifier)事务标识符
                sendData.AddRange(new byte[] { 0, 0 });//3~4:Protocol Identifier,0 = MODBUS protocol
                sendData.AddRange(ValueHelper.Instance.GetBytes((ushort)6));//5~6:后续的Byte数量（针对读请求，后续为6个byte）
                sendData.Add(slaveAddress);//7:Unit Identifier:This field is used for intra-system routing purpose.
                sendData.Add(functionCode);//8.Function Code
                sendData.AddRange(ValueHelper.Instance.GetBytes(startingAddress));//9~10.起始地址
                sendData.AddRange(ValueHelper.Instance.GetBytes(registerNumber));//11~12.需要读取的寄存器数量


                mreEvent.Reset();
                this.client.Send(sendData.ToArray()); //发送读请求
                //logManger.WriteInfo("Read:" + string.Join(" ", sendData.ToArray()));



                //等待服务器返回数据
                if (mreEvent.WaitOne(TimeOut) == false)
                {
                    string message = string.Format("Modbus read error,no responce! Fun:{0}, Addr:{1}, Num:{2}",
                            functionCode, startingAddress, registerNumber);
                    //logManger.WriteInfo(message);
                    throw new Exception(message);
                }
                //完后会返回：MBAP报文头（7个字节）+ 功能码（1个字节）+ 字节数（1个字节）+ 数据（n个字节）
                ushort identifier = ValueHelper.Instance.ToUInt16(recvBuffer, 0);
                if (identifier != DataIndex) //请求的数据标识与返回的标识不一致，则丢掉数据包
                {
                    string message = string.Format("Modbus read error,id error! Fun:{0}, Addr:{1}, Num:{2}",
                            functionCode, startingAddress, registerNumber);
                    //logManger.WriteInfo(message);
                    throw new Exception(message);
                }
                //获取响应帧中的功能码，正确的响应帧中的功能码应一致
                byte recvCode = recvBuffer[7];
                if (recvCode == functionCode + 0x80) //返回的为错误帧
                {
                    string message = string.Format("Modbus read error,frame error! Fun:{0}, Addr:{1}, Num:{2}",
                        functionCode, startingAddress, registerNumber);
                    //logManger.WriteInfo(message);
                    throw new Exception(message);
                }
                //获取读取的数据
                byte length = recvBuffer[8];
                byte[] result = new byte[length];
                Array.Copy(recvBuffer, 9, result, 0, length);
                return result;
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }

        protected override void Write(byte slaveAddress, byte functionCode, ushort startingAddress, ushort registerNumber, byte[] data)
        {
            try
            {
                Monitor.Enter(lockObj);
                this.Connect();
                //创建数据包并发送,写命令发送后，会返回12个字节的响应帧
                List<byte> sendData = new List<byte>();
                if (registerNumber > 0) //写多个寄存器
                {
                    sendData.AddRange(ValueHelper.Instance.GetBytes((ushort)(++DataIndex)));//1~2.(Transaction Identifier)
                    sendData.AddRange(new byte[] { 0, 0 });//3~4:Protocol Identifier,0 = MODBUS protocol
                    sendData.AddRange(ValueHelper.Instance.GetBytes((ushort)(data.Length + 7)));//5~6:后续的Byte数量
                    sendData.Add(slaveAddress);//7:Unit Identifier:This field is used for intra-system routing purpose.
                    sendData.Add(functionCode);//8.Function Code
                    sendData.AddRange(ValueHelper.Instance.GetBytes(startingAddress));//9~10.起始地址
                    sendData.AddRange(ValueHelper.Instance.GetBytes(registerNumber));//11~12.寄存器数量
                    sendData.Add((byte)data.Length); //13.数据的Byte数量
                    sendData.AddRange(data);//14~End:需要发送的数据
                }
                else //写单个寄存器
                {
                    sendData.AddRange(ValueHelper.Instance.GetBytes((ushort)(++DataIndex)));//1~2.(Transaction Identifier)
                    sendData.AddRange(new byte[] { 0, 0 });//3~4:Protocol Identifier,0 = MODBUS protocol
                    sendData.AddRange(ValueHelper.Instance.GetBytes((ushort)6)); //5~6:后续的Byte数量
                    sendData.Add(slaveAddress);//7:Unit Identifier:This field is used for intra-system routing purpose.
                    sendData.Add(functionCode);//8.Function Code
                    sendData.AddRange(ValueHelper.Instance.GetBytes(startingAddress));//9~10.起始地址
                    sendData.AddRange(data);//11~12:需要发送的数据
                }
                mreEvent.Reset();
                this.client.Send(sendData.ToArray());
                //logManger.WriteInfo("Write:"+string.Join(" ",sendData.ToArray()));
                //等待服务器返回数据
                if (mreEvent.WaitOne(TimeOut) == false)
                {
                    string message = string.Format("Modbus write error,no responce! Fun:{0}, Addr:{1}, Num:{2}",
                            functionCode, startingAddress, registerNumber);
                    //logManger.WriteInfo(message);
                    throw new Exception(message);
                }
                //完后会返回：MBAP报文头（7个字节）+ 功能码（1个字节）+ 字节数（1个字节）+ 数据（n个字节）
                ushort identifier = ValueHelper.Instance.ToUInt16(recvBuffer, 0);
                if (identifier != DataIndex) //请求的数据标识与返回的标识不一致，则丢掉数据包
                {
                    string message = string.Format("Modbus write error,id error! Fun:{0}, Addr:{1}, Num:{2}",
                            functionCode, startingAddress, registerNumber);
                    //logManger.WriteInfo(message);
                    throw new Exception(message);
                }
                //获取响应帧中的功能码，正确的响应帧中的功能码应一致
                byte recvCode = recvBuffer[7];
                if (recvCode == functionCode + 0x80) //返回的为错误帧
                {
                    string message = string.Format("Modbus write error,frame error! Fun:{0}, Addr:{1}, Num:{2}",
                        functionCode, startingAddress, registerNumber);

                    //logManger.WriteInfo(message);
                    throw new Exception(message);
                }
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }


        public override void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
                client = null;
            }
        }
    }
}

