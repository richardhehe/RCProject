
using CjBase;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCProject.power
{
  public  class PowerCtrlRq: PowerCtrl
    {
        private SerialPort sp = new SerialPort();     
        public override bool InitSerialport()
        {
            try
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                }
                sp.PortName = comPort;            //串口名         
                sp.BaudRate = baudRate;       //波特率
                sp.DataBits = dataBits;        //数据位
                sp.StopBits = (StopBits)stopBits;        //停止位          
                if (parity == 1)
                {
                    sp.Parity = Parity.Even;
                }
                else
                {
                    sp.Parity = Parity.None;
                }
                sp.Open();
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString() + ex.StackTrace, LogMode.Power);
                return false;
            }
        }
        public override bool WriteSerialport(int chNum,int lightValue)
        {
            try
            {
                string str = Fn_GenerateControlCommand(chNum, 3, lightValue);
                if (sp.IsOpen)
                {
                    SerialDataGet = "";
                    sp.Write(str);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message + ex.StackTrace, LogMode.Power);
                return false;
            }
        }

        private string Fn_GenerateControlCommand(int channel, int cmdType, int level)
        {
            //特征字
            string cmdStr = "$";
            //指令字 ＝ 1，2，3，4，分别定义为：
            //1:      打开对应通道电源()
            //2:      关闭对应通道电源()
            //3:      设置对应通道电源参数()
            //4:      读出对应通道电源参数()
            cmdStr = cmdStr + cmdType.ToString();
            cmdStr = cmdStr + channel.ToString();

            if (level < 16)
            {
                cmdStr = cmdStr + "00" + Convert.ToString(level, 16).ToUpper();
            }
            else
            {
                cmdStr = cmdStr + "0" + Convert.ToString(level, 16).ToUpper();
            }
            Fn_CalculateXOR(ref cmdStr);
            return cmdStr;
        }
        private void Fn_CalculateXOR(ref string str)
        {
            int length = str.Length;
            int i = 0;
            byte[] byteArray = new byte[length + 1];
            byte result = 0;

            for (i = 1; i <= length; i++)
            {
                // byteArray[i] = System.Text.Encoding.GetEncoding("unicode").GetBytes(str)[i-1];
                byteArray[i]= Encoding.UTF8.GetBytes(str)[i-1];

                result = Convert.ToByte(result ^ byteArray[i]);
            }
            str = str + Convert.ToString(result, 16).ToUpper();
        }
    }
}
