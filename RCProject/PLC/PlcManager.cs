using CjBase;
using Communication;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using RCProject.NinePointDeal;
using Modbus;

namespace RCProject.PLC
{
    public class PlcManager  // PLC管理类
    {

        // 核心成员变量
        public ModbusMaster master; // Modbus主站对象
        private ServerInfo info; // PLC服务器信息
        public static PlcReadCoils ReadCoils = new PlcReadCoils(); // 当前读取的线圈状态
        public static PlcReadCoils LastReadCoils = new PlcReadCoils(); // 上一次读取的线圈状态

        public void Init()
        {
            try
            {
                // 1. 加载XML配置文件
                XDocument xDoc = XDocument.Load(@"Config\PLC.xml");


                // XML文件结构示例：
                /*
                <PLC>
                  <Connections>
                    <Index>1</Index>      <!-- 设备索引 -->
                    <Port>502</Port>      <!-- Modbus端口（标准502） -->
                    <IP>192.168.1.100</IP><!-- PLC的IP地址 -->
                  </Connections>
                </PLC>
                */

                // 2. 解析配置参数
                XElement element = xDoc.Root.Element("Connections");
                string Index = element.Element("Index")?.Value;
                string port = element.Element("Port")?.Value;
                string ip = element.Element("IP")?.Value;



                // 3. 验证配置参数
                if (string.IsNullOrEmpty(ip) == false && string.IsNullOrEmpty(Index) == false && string.IsNullOrEmpty(port) == false)
                {


                    // 创建服务器信息对象
                    info = (new ServerInfo
                    {
                        IP = ip,
                        Port = Convert.ToInt32(port),
                        index = Convert.ToInt32(Index)
                    });
                }
                else
                {

                    // 参数异常，更新UI状态
                    Frm_Main.Instance.SetLabel_HadeWareSatus(HadewareStatus.PLC_Null);
                    Frm_Main.Instance.SetMsg("PLC接口参数值异常！", Color.Red);
                    return;
                }


                // 4. 创建Modbus主站   作用：建立TCP连接，准备与PLC通信
                master = ModbusMaster.CreateTCP(info);


                // 5. 测试连接
                if (GetConnected() == false)                    
                {                  
                    Frm_Main.Instance.SetLabel_HadeWareSatus(HadewareStatus.PLC_Null);
                    Frm_Main.Instance.SetMsg("PLC连接失败！", Color.Red);
                    return;
                }

                // 6. 连接成功，更新UI
                Frm_Main.Instance.SetLabel_HadeWareSatus(HadewareStatus.PLC);
                Frm_Main.Instance.SetMsg("PLC连接成功！", Color.Blue);


                // 7. 启动后台读取线程
                Thread thRead = new Thread(PlcRead);
                thRead.IsBackground = true; // 设置为后台线程，主程序退出时自动终止
                thRead.Start();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("读取PLC Config文件失败————" + ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);
                Frm_Main.Instance.SetLabel_HadeWareSatus(HadewareStatus.PLC_Null);
                Frm_Main.Instance.SetMsg("PLC连接失败！", Color.Red);
            }

        }
        public bool GetConnected()
        {

            if (master.ConnectPlc() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DisConnected()
        {
            master.DisConnectPlc();
            master.Dispose();

        }
        private void PlcRead()
        {

            while (true)
            {
                try
                {
                    if (GetConnected() == false)
                    {
                        Thread.Sleep(500);

                        continue;
                    }

                  var DataReadCoils = master.ReadHoldingRegisters(1, PlcModbusAddr.StartCoilAddr, 100);
             
                   // ReadCoils.Updata(DataReadCoils);

                   //PlcSingalsDeal.ProcessSignals();

                   // LastReadCoils = Copy.DeepCopyWithBinarySerializer(ReadCoils);

                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message +"\r\n"+ ex.StackTrace, LogMode.Sys);
                }
            }
        }
    }
}
