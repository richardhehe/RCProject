using CjBase;
using Communication;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RCProject.Laser
{
    public class LaserManager
    {
        private ServerInfo info;
        public LaserTCPMaster master;
        public void Init()
        {
            try
            {
                XDocument xDoc = XDocument.Load(@"Config\Laser.xml");
                XElement element = xDoc.Root.Element("Connections");
                string Index = element.Element("Index")?.Value;
                string port = element.Element("Port")?.Value;
                string ip = element.Element("IP")?.Value;

                if (string.IsNullOrEmpty(ip) == false && string.IsNullOrEmpty(Index) == false && string.IsNullOrEmpty(port) == false)
                {
                    info = (new ServerInfo
                    {
                        IP = ip,
                        Port = Convert.ToInt32(port),
                        index = Convert.ToInt32(Index)
                    });
                }
                else
                {
                    Frm_Main.Instance.SetMsg("Laser接口参数值异常！", Color.Red);

                    Frm_Main.Instance.SetLabel_HadeWareSatus(HadewareStatus.Laser_Null);

                    return;
                }

                master = new LaserTCPMaster(info);

                if (GetConnected() == false)
                {
                    Frm_Main.Instance.SetLabel_HadeWareSatus(HadewareStatus.Laser_Null);
                    Frm_Main.Instance.SetMsg("Laser连接失败！", Color.Red);
                    return;
                }
                Frm_Main.Instance.SetLabel_HadeWareSatus(HadewareStatus.Laser);
                Frm_Main.Instance.SetMsg("Laser连接成功！", Color.Blue);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("读取Laser XML文件失败————" + ex.Message + "/r/n" + ex.StackTrace, LogMode.Sys);
                Frm_Main.Instance.SetLabel_HadeWareSatus(HadewareStatus.Laser_Null);
                Frm_Main.Instance.SetMsg("Laser连接失败！", Color.Red);
            }

        }

        private bool GetConnected()
        {

            if (master.ConnectCamera() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string Recieve()
        {
            return master.Recieve();
        }

        public void SetTimeOut(int time)
        {
            master.TimeOut = time;
        }
    }
}
