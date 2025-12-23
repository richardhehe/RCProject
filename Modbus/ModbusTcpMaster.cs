using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modbus
{
    internal class ModbusTcpMaster : ModbusMaster
    {
        private ServerInfo serverInfo;
        private ManualResetEvent mreEvent = new ManualResetEvent(false);
        private HTCOMMSocketClient client;

        private byte[] recvBuffer = null;
        private object lockObj = new object();
        public ModbusTcpMaster(ServerInfo info)
        {
            serverInfo = info;

            client = new HTCOMMSocketClient();
            client.ReceiveData += socketClicent_DataReceived;
            Connect();
        }
        protected override void Connect()
        {
            if (client.Connected == false)
            {
                client.Connect(serverInfo.IP, serverInfo.Port);
            }
        }

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



        public override  bool DisConnectPlc()
        {

            client.Disconnect();
            return true;
        }

        private void socketClicent_DataReceived(object sender, SocketEventArgs e)
        {
            recvBuffer = null;
            recvBuffer = e.Datas;
            if (recvBuffer != null)
            {
                mreEvent.Set();
                //logManger.WriteInfo("Received:" + string.Join(" ", recvBuffer.ToArray()));
            }
        }
        protected byte DataIndex { get; set; }

        protected override byte[] Read(byte slaveAddress, byte functionCode, ushort startingAddress, ushort registerNumber)
        {
            try
            {
                Monitor.Enter(lockObj); //Read与Write不能同时执行
                this.Connect();
                //创建数据包并发送
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

