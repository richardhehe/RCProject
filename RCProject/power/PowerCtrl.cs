using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCProject.power
{
    public class PowerCtrl
    {
        public int PowerIndex { get; set; }
        public string comPort { get; set; }
        public int baudRate { get; set; }
        public int dataBits { get; set; }
        public int stopBits { get; set; }
        public int parity { get; set; }
        public string SerialDataGet { get; set; }
        public virtual bool InitSerialport()
        {
            return true;
        }
        public virtual bool WriteSerialport(int chNum, int lightValue)
        {
            return true;
        }
    }
}
