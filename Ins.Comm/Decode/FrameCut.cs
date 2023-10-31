// * *************************************************** *
// * 功能说明: 分帧裁剪模块 - 类型1                      *
// * 创建时间: 2021.01.10                                *
// * 作    者: 李先江                                    *
// * 修改时间:                                           *
// * 修 改 人:                                           *
// * 备    注:                                           *
// * *************************************************** *

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Ins.Comm
{
    class FrameCut
    {
        /* =========================== 开放调用 =========================== */
       
        public byte[][] Cut(byte[] data, ref int head, ref int tail)   /* 有效帧提取 动态长度 */
        {
            Queue<byte[]> que = new Queue<byte[]>();
            byte[][] _frame = new byte[0][];

            int _len = (data.Length - tail + head) % data.Length;

            while (_len >= 20)  // 最短20字节判断
            {
                if ((data[tail] == 0xAA) && (data[(tail + 1) % data.Length] == 0x55))
                {
                    if (data[(tail + 2) % data.Length] + 4 > _len)  // 增加判断长度逻辑，预期帧长大于当前剩余总长度，则break
                    {
                        break;
                    }

                    byte[] _validData = new byte[data[(tail + 2) % data.Length] + 4]; // 动态长度

                    ushort _check;
                    _check = CheckSum.CalCRC16(_validData, _validData.Length - 2);
                    byte_conversion_array getInteger = new byte_conversion_array();
                    getInteger.byte1 = _validData[_validData.Length-2]; getInteger.byte0 = _validData[_validData.Length - 1];
                    uint b = getInteger.uint16_3;
                    if (_check == b)
                    {
                        for (int i = 0; i < _validData.Length; i++)
                        {
                            _validData[i] = data[tail++];
                            tail = tail % data.Length;
                        }

                        que.Enqueue(_validData);
                    }
                    else
                    {
                        tail = (++tail % data.Length);
                    }
                }
                else
                {
                    tail = (++tail % data.Length);
                }

                _len = (data.Length - tail + head) % data.Length;
            }

            return _frame = que.ToArray();  // 交错数组返回
        }
    }
}
