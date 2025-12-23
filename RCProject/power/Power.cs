using CjBase;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCProject.power
{
  public class Power
    {
        private static int PowerCount = 1;         
        private static PowerManager PowerManager = new PowerManager();
        public static void Init()
        {
            IniUtilityManager.FilePath = Application.StartupPath + @"\" + "Config.cfg";
            PowerCount = Convert.ToInt32(IniUtilityManager.GetIniKeyValue("SerialPortCount", "PowerCount","1"));
            List<PowerInfo> lstInfos = new List<PowerInfo>();
            for (int i = 0; i < PowerCount; i++)
            {
                PowerInfo info = new PowerInfo();
                info.comPort = Convert.ToInt32(IniUtilityManager.GetIniKeyValue("SerialPortParameter", "ComPort_"+i.ToString(), "0"));
                info.baudRate = Convert.ToInt32(IniUtilityManager.GetIniKeyValue("SerialPortParameter", "baudRate_" + i.ToString(), "19200"));
                info.dataBits = Convert.ToInt32(IniUtilityManager.GetIniKeyValue("SerialPortParameter", "dataBits_" + i.ToString(), "8"));
                info.stopBits = Convert.ToInt32(IniUtilityManager.GetIniKeyValue("SerialPortParameter", "stopBits_" + i.ToString(), "1"));
                info.parity = Convert.ToInt32(IniUtilityManager.GetIniKeyValue("SerialPortParameter", "parity_" + i.ToString(), "0"));              
                lstInfos.Add(info);
            }
            PowerManager.Init(lstInfos);
        }
        /// <summary>
        /// 打开、关闭光源
        /// </summary>
        /// <param name="chNum">通道</param>
        /// <param name="lightValue">光源亮度值</param>
        /// <param name="index">光源索引</param>
        public static void WriteSerialportInit( int chNum, int lightValue, int index= 0)
        {
            PowerManager.GetPower(index).WriteSerialport(chNum, lightValue);
        }
    }

   
}
