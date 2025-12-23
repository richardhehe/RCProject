using CjBase;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RCProject.Laser
{
    public class LaserTCPMaster
    {
        private ServerInfo serverInfo;
        public int TimeOut { get; set; } = 500;

        private ManualResetEvent mreEvent = new ManualResetEvent(false);
        private HTCOMMSocketClient client;
        private byte[] recvBuffer = null;
        private object lockObj = new object();

        public LaserTCPMaster(ServerInfo info)
        {
            serverInfo = info;

            client = new HTCOMMSocketClient();
            client.ReceiveData += socketClicent_DataReceived;
            Connect();
            
        }


        protected void Connect()
        {
            if (client.Connected == false)
            {
                client.Connect(serverInfo.IP, serverInfo.Port);
            }
        }

        public bool ConnectCamera()
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

        private void socketClicent_DataReceived(object sender, SocketEventArgs e)
        {
            recvBuffer = null;

            recvBuffer = e.Datas;

            if (recvBuffer != null)
            {
                mreEvent.Set();

                LogManager.WriteLog("镭射 Received: " + string.Join(" ", recvBuffer.ToArray()), LogMode.Camera);

                //logManger.WriteInfo("Received:" + string.Join(" ", recvBuffer.ToArray()));
            }
        }
        public string Recieve()
        {
            return Encoding.UTF8.GetString(recvBuffer);
        }
    }
}
