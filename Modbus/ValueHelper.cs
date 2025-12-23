using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus
{
    /// <summary>
    /// 基本数据转换帮助类
    /// </summary>
    public class ValueHelper
    {
        #region Factory
        private static ValueHelper _Instance = null;
        /// <summary>
        /// 实例
        /// </summary>
        public static ValueHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = BitConverter.IsLittleEndian ? new LittleEndianValueHelper() : new ValueHelper();
                }
                return _Instance;
            }
        }
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        protected ValueHelper()
        {

        }
        /// <summary>
        /// 以字节数组的形式返回指定的 16 位有符号整数值。BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="value">要转换的数字</param>
        /// <returns>长度为 2 的字节数组。</returns>
        public virtual byte[] GetBytes(short value)
        {
            return BitConverter.GetBytes(value);
        }
        /// <summary>
        /// 以字节数组的形式返回指定的 16 位无符号整数值。BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="value">要转换的数字。</param>
        /// <returns>长度为 2 的字节数组。</returns>
        public virtual byte[] GetBytes(ushort value)
        {
            return BitConverter.GetBytes(value);
        }
        /// <summary>
        /// 以字节数组的形式返回指定的 32 位有符号整数值。BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="value">要转换的数字。</param>
        /// <returns>长度为 4 的字节数组。</returns>
        public virtual byte[] GetBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }
        /// <summary>
        /// 以字节数组的形式返回指定的 32 位无符号整数值。BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="value">要转换的数字。</param>
        /// <returns>长度为 4 的字节数组。</returns>
        public virtual byte[] GetBytes(uint value)
        {
            return BitConverter.GetBytes(value);
        }
        /// <summary>
        /// 以字节数组的形式返回指定的单精度浮点值。BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="value">要转换的数字。</param>
        /// <returns>长度为 4 的字节数组。</returns>
        public virtual byte[] GetBytes(float value)
        {
            return BitConverter.GetBytes(value);
        }
        /// <summary>
        /// 以字节数组的形式返回指定的双精度浮点值。BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="value">要转换的数字。</param>
        /// <returns>长度为 8 的字节数组。</returns>
        public virtual byte[] GetBytes(double value)
        {
            return BitConverter.GetBytes(value);
        }
        /// <summary>
        /// 返回由字节数组中指定位置的两个字节转换来的 16 位有符号整数。输入数组为BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="data">字节数组。</param>
        /// <param name="startIndex">data内的起始位置。</param>
        /// <returns>由两个字节构成、从 startIndex 开始的 16 位有符号整数。</returns>
        public virtual short ToInt16(byte[] data, int startIndex)
        {
            return BitConverter.ToInt16(data, startIndex);
        }
        /// <summary>
        /// 返回由字节数组中指定位置的两个字节转换来的 16 位无符号整数。输入数组为BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="data">字节数组。</param>
        /// <param name="startIndex">data内的起始位置。</param>
        /// <returns>由两个字节构成、从 startIndex 开始的 16 位无符号整数。</returns>
        public virtual ushort ToUInt16(byte[] data, int startIndex)
        {
            return BitConverter.ToUInt16(data, startIndex);
        }

        /// <summary>
        /// 返回由字节数组中指定位置的四个字节转换来的 32 位单精度浮点数。输入数组为BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public virtual float ToSingle(byte[] data, int startIndex)
        {
            return BitConverter.ToSingle(data, startIndex);
        }
        /// <summary>
        /// 返回由字节数组中指定位置的四个字节转换来的 32 位有符号整数。输入数组为BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="data">字节数组。</param>
        /// <param name="startIndex">data内的起始位置。</param>
        /// <returns>由四个字节构成、从 startIndex 开始的 32 位有符号整数。</returns>
        public virtual int ToInt32(byte[] data, int startIndex)
        {
            return BitConverter.ToInt32(data, startIndex);
        }
        /// <summary>
        /// 返回由字节数组中指定位置的四个字节转换来的 32 位无符号整数。输入数组为BigEndian，高位在前，低位在后。
        /// </summary>
        /// <param name="data">字节数组。</param>
        /// <param name="startIndex">data内的起始位置。</param>
        /// <returns>由四个字节构成、从 startIndex 开始的 32 位无符号整数。</returns>
        public virtual uint ToUInt32(byte[] data, int startIndex)
        {
            return BitConverter.ToUInt32(data, startIndex);
        }
    }

    internal class LittleEndianValueHelper : ValueHelper
    {
        public override byte[] GetBytes(short value)
        {
            return this.Reverse(BitConverter.GetBytes(value));
        }

        public override byte[] GetBytes(ushort value)
        {
            return this.Reverse(BitConverter.GetBytes(value));
        }

        public override byte[] GetBytes(int value)
        {
            return this.Reverse(BitConverter.GetBytes(value));
        }

        public override byte[] GetBytes(uint value)
        {
            return this.Reverse(BitConverter.GetBytes(value));
        }

        public override byte[] GetBytes(float value)
        {
            return this.Reverse(BitConverter.GetBytes(value));
        }

        public override byte[] GetBytes(double value)
        {
            return this.Reverse(BitConverter.GetBytes(value));
        }

        public override short ToInt16(byte[] data, int startIndex)
        {
            byte[] temp = new byte[2];
            Array.Copy(data, startIndex, temp, 0, 2);
            return BitConverter.ToInt16(this.Reverse(temp), 0);
        }

        public override ushort ToUInt16(byte[] data, int startIndex)
        {
            byte[] temp = new byte[2];
            Array.Copy(data, startIndex, temp, 0, 2);
            return BitConverter.ToUInt16(this.Reverse(temp), 0);
        }

        public override float ToSingle(byte[] data, int startIndex)
        {
            byte[] temp = new byte[4];
            Array.Copy(data, startIndex, temp, 0, 4);
            return BitConverter.ToSingle(this.Reverse(temp), 0);
        }

        public override int ToInt32(byte[] data, int startIndex)
        {
            byte[] temp = new byte[4];
            Array.Copy(data, startIndex, temp, 0, 4);
            return BitConverter.ToInt32(this.Reverse(temp), 0);
        }

        public override uint ToUInt32(byte[] data, int startIndex)
        {
            byte[] temp = new byte[4];
            Array.Copy(data, startIndex, temp, 0, 4);
            return BitConverter.ToUInt32(this.Reverse(temp), 0);
        }

        private byte[] Reverse(byte[] data)
        {
            Array.Reverse(data);
            return data;
        }
    }
}
