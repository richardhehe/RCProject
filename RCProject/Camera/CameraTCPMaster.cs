using CjBase;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RCProject.Camera
{
    public  class CameraTCPMaster
    {
        private ServerInfo serverInfo;
        public int TimeOut { get; set; } = 10000;

        private ManualResetEvent mreEvent = new ManualResetEvent(false);
        private HTCOMMSocketClient client;
        private byte[] recvBuffer = null;
        private object lockObj = new object();

        public CameraTCPMaster(ServerInfo info)
        {
            serverInfo = info;

            client = new HTCOMMSocketClient();
            client.ReceiveData += socketClicent_DataReceived;
            Connect();
        }


        protected  void Connect()
        {
            if (client.Connected == false)
            {
                client.Connect(serverInfo.IP, serverInfo.Port);
            }
        }

        public  bool ConnectCamera()
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
        public void DisConnected()
        {
            client.Disconnect();
            client.Dispose();

        }
        private void socketClicent_DataReceived(object sender, SocketEventArgs e)
        {
            recvBuffer = null;
            recvBuffer = e.Datas;
            if (recvBuffer != null)
            {
                mreEvent.Set();

                LogManager.WriteLog("相机 Received: " + string.Join(" ", recvBuffer.ToArray()), LogMode.Camera);      
            }
        }

        public bool send(string str )
        {

            mreEvent.Reset();

            this.client.Send(Encoding.UTF8.GetBytes(str)); //发送测试请求

            if (mreEvent.WaitOne(TimeOut) == false)
            {
                LogManager.WriteLog("相机 Send: " + string.Join(" ", Encoding.UTF8.GetBytes(str).ToArray() + "，失败"), LogMode.Camera);

                return false;
            }
            LogManager.WriteLog("相机 Send: " + string.Join(" ", Encoding.UTF8.GetBytes(str).ToArray()+"，成功"), LogMode.Camera);

            return true;
        }


        public string Recieve()
        {
           return   Encoding.UTF8.GetString(recvBuffer);
        }
    }
}
