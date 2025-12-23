using CjBase;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modbus
{
    public abstract class ModbusMaster : IDisposable
    {
        public int TimeOut { get; set; } = 150;
        public static ModbusMaster CreateTCP(ServerInfo info)
        {
            return new ModbusTcpMaster(info);
        }

        public string GetstartingAddress(string str)
        {
            try
            {
                string[] addr = null;

                if (str.Contains("M"))
                {
                    addr = str.Split('M');
                    return addr[1];
                }
                else if (str.Contains("D"))
                {
                    addr = str.Split('D');
                    return addr[1];

                }
                return addr[1];
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        public bool[] ReadCoils(byte slaveAddress, ushort startingAddress, ushort registerNumber)
        {

            bool[] result = new bool[registerNumber];

            byte[] buffer = Read(slaveAddress, (byte)FunctionCode.ReadCoils, startingAddress, registerNumber);
            if (buffer.Length > 0)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    int index1 = i / 8;
                    int index2 = i % 8;
                    result[i] = (buffer[index1] & (byte)(0x01 << index2)) > 0 ? true : false;
                }
            }
            return result;
        }

        public void WriteSingleCoil(byte slaveAddress, ushort startingAddress, bool data)
        {
            byte[] buffer = new byte[2];
            if (data == true)
            {
                buffer[0] = 0xFF;
                buffer[1] = 0x00;
            }
            Write(slaveAddress, (byte)FunctionCode.WriteSingleCoil, startingAddress, 0, buffer);
        }
        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startingAddress, ushort registerNumber)
        {
            ushort[] result = new ushort[registerNumber];
            byte[] buffer = Read(slaveAddress, (byte)FunctionCode.ReadHoldingRegisters, startingAddress, registerNumber);
            if (buffer.Length > 0)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = ValueHelper.Instance.ToUInt16(buffer, 2 * i);
                }
            }
            return result;
        }
        public float[] ReadHoldingRegistersFloat(byte slaveAddress, ushort startingAddress, ushort floatNumber)
        {
            float[] result = new float[floatNumber];
            byte[] buffer = Read(slaveAddress, (byte)FunctionCode.ReadHoldingRegisters, startingAddress, (ushort)(floatNumber * 2));
            if (buffer.Length > 0)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = ValueHelper.Instance.ToSingle(buffer, 4 * i);
                }
            }
            return result;
        }
      

        public string ReadHoldingRegistersStr(byte slaveAddress, ushort startingAddress, ushort floatNumber)
        {
            List<byte> bytes = new List<byte>();
            byte[] buffer1 = Read(slaveAddress, (byte)FunctionCode.ReadHoldingRegisters, startingAddress, floatNumber);
            for (int i = 0; i < buffer1.Length / 2; i++)
            {
                bytes.Add(buffer1[2 * i + 1]);
                bytes.Add(buffer1[2 * i]);
            }
            return Encoding.ASCII.GetString(bytes.ToArray());
        }
        public void WriteMultipleRegisters(byte slaveAddress, ushort startingAddress, ushort[] data)
        {
            byte[] buffer = new byte[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
            {
                byte[] temp = ValueHelper.Instance.GetBytes(data[i]);
                buffer[2 * i] = temp[0];
                buffer[2 * i + 1] = temp[1];
            }
            Write(slaveAddress, (byte)FunctionCode.WriteMultipleRegisters, startingAddress, (ushort)data.Length, buffer);
        }
        public void WriteSingleRegisterFloat(byte slaveAddress, ushort startingAddress, float data)
        {
            byte[] buffer = ValueHelper.Instance.GetBytes(data);
           // var buffer= BitConverter.GetBytes(data);
            byte[] buffer1 = new byte[4];
            buffer1[0] = buffer[2];
            buffer1[1] = buffer[3];
            buffer1[2] = buffer[0];
            buffer1[3] = buffer[1];



            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Write(slaveAddress, (byte)FunctionCode.WriteMultipleRegisters, startingAddress, (ushort)(buffer1.Length / 2), buffer1);
                    // logManger.WriteInfo($"写PMAC成功 startingAddress:{startingAddress }  data{data}");
                    LogManager.WriteLog($"写PMAC成功 startingAddress:{startingAddress }  data{data}", LogMode.TestMsg2);

                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(30);

                    LogManager.WriteLog($"写PLC失败 startingAddress:{startingAddress }  data{data}  次数{i + 1}" + ex.Message,LogMode.TestMsg2);
                   
                    continue;
                }
            }
        }

        public void WriteSingleRegisterFloat2(byte slaveAddress, ushort startingAddress, float data)
        {
            byte[] buffer = ValueHelper.Instance.GetBytes(data);
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Write(slaveAddress, (byte)FunctionCode.WriteMultipleRegisters, startingAddress, (ushort)(buffer.Length), buffer);
                    // logManger.WriteInfo($"写PMAC成功 startingAddress:{startingAddress }  data{data}");
                    LogManager.WriteLog($"写PMAC成功 startingAddress:{startingAddress }  data{data}", LogMode.TestMsg2);

                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(30);

                    LogManager.WriteLog($"写PLC失败 startingAddress:{startingAddress }  data{data}  次数{i + 1}" + ex.Message, LogMode.TestMsg2);

                    continue;
                }
            }
        }

        public void WriteSingleRegisterDouble(byte slaveAddress, ushort startingAddress, double data)
        {
            byte[] buffer = ValueHelper.Instance.GetBytes(data);
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Write(slaveAddress, (byte)FunctionCode.WriteMultipleRegisters, startingAddress, (ushort)(buffer.Length / 2), buffer);
                    // logManger.WriteInfo($"写PMAC成功 startingAddress:{startingAddress }  data{data}");
                    LogManager.WriteLog($"写PMAC成功 startingAddress:{startingAddress }  data{data}", LogMode.TestMsg2);

                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(30);

                    LogManager.WriteLog($"写PLC失败 startingAddress:{startingAddress }  data{data}  次数{i + 1}" + ex.Message, LogMode.TestMsg2);

                    continue;
                }
            }
        }


        public void WriteSingleRegister(byte slaveAddress, ushort startingAddress, ushort data)
        {
            byte[] buffer = ValueHelper.Instance.GetBytes(data);
            Write(slaveAddress, (byte)FunctionCode.WriteSingleRegister, startingAddress, 0, buffer);
        }
        public bool[] ReadInputs(byte slaveAddress, ushort startingAddress, ushort registerNumber)
        {
            bool[] result = new bool[registerNumber];
            byte[] buffer = Read(slaveAddress, (byte)FunctionCode.ReadInputs, startingAddress, registerNumber);
            if (buffer.Length > 0)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    int index1 = i / 8;
                    int index2 = i % 8;
                    result[i] = (buffer[index1] & (byte)(0x01 << index2)) > 0 ? true : false;
                }
            }
            return result;
        }
        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startingAddress, ushort registerNumber)
        {
            ushort[] result = new ushort[registerNumber];
            byte[] buffer = Read(slaveAddress, (byte)FunctionCode.ReadInputRegisters, startingAddress, registerNumber);
            if (buffer.Length > 0)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = ValueHelper.Instance.ToUInt16(buffer, 2 * i);
                }
            }
            return result;
        }
        protected abstract void Connect();

        public abstract bool ConnectPlc();

        public abstract bool DisConnectPlc();

        protected abstract byte[] Read(byte slaveAddress, byte functionCode, ushort startingAddress, ushort registerNumber);

        protected abstract void Write(byte slaveAddress, byte functionCode, ushort startingAddress, ushort registerNumber, byte[] data);



        public virtual void Dispose()
        {
        }
    }
}

