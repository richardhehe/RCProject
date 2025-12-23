using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus
{
    public enum FunctionCode
    {
        /// <summary>
        /// 读线圈
        /// </summary>
        ReadCoils = 0x01,
        /// <summary>
        /// 读离散输入
        /// </summary>
        ReadInputs = 0x02,
        /// <summary>
        /// 读保持寄存器
        /// </summary>
        ReadHoldingRegisters = 0x03,
        /// <summary>
        /// 读输入寄存器
        /// </summary>
        ReadInputRegisters = 0x04,

        /// <summary>
        /// 写单个线圈
        /// </summary>
        WriteSingleCoil = 0x05,
        /// <summary>
        /// 写单个保持寄存器
        /// </summary>
        WriteSingleRegister = 0x06,
        /// <summary>
        /// 写多个线圈
        /// </summary>
        WriteMultipleCoils = 0x0F,
        /// <summary>
        /// 写多个保持寄存器
        /// </summary>
        WriteMultipleRegisters = 0x10
    }
}
