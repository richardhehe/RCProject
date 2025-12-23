using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCProject.PLC
{
    class PlcModbusAddr
    {
        //plc读取index信号

        public const ushort StartCoilAddr = 1000;//读线圈起始地址


        public const ushort StartCoilAddr1 = 0X09C4;


        public const ushort StartNinePintAddr = 0X0000;//九点起始地址
    }
}
