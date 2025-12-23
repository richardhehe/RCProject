using CjBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCProject.power
{
    public class PowerManager
    {
        private PowerInfo[] infos;
        private PowerCtrl[] Power;
        internal void Init(List<PowerInfo> lstInfos)
        {
            infos = lstInfos.ToArray();
            Power = new PowerCtrl[lstInfos.Count];
            for (int i = 0; i < lstInfos.Count; i++)
            {
                Power[i] = new PowerCtrlRq();
                Power[i].comPort = "COM" + lstInfos[i].comPort;
                Power[i].baudRate = lstInfos[i].baudRate;
                Power[i].dataBits = lstInfos[i].dataBits;
                Power[i].stopBits = lstInfos[i].stopBits;
                Power[i].parity = lstInfos[i].parity;
                Power[i].PowerIndex = i;
                //程序打开的时候先释放线程                
                if (Power[i].InitSerialport())
                {
                    Frm_Main.Instance.SetLabel_HadeWareSatus(HadewareStatus.Power);
                    Frm_Main.Instance.SetMsg("光源:" + i + "连接成功", Color.Green);
                    LogManager.WriteLog("光源:" + i + "连接成功", LogMode.Power);
                }
                else
                {   
                    Frm_Main.Instance.SetLabel_HadeWareSatus(HadewareStatus.Power_Null);
                    Frm_Main.Instance.SetMsg("光源:" + i + "连接失败", Color.Red);
                }
            }
        }

        public PowerCtrl GetPower(int index)
        {
            return Power[index];
        }
    }

}
