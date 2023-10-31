// * *************************************************** *
// * 功能说明: 接收有效频率计算模块                      *
// * 创建时间: 2021.01.10                                *
// * 作    者: 李先江                                    *
// * 修改时间:                                           *
// * 修 改 人:                                           *
// * 备    注:                                           *
// * *************************************************** *

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ins.Comm
{
    class FrequencyCount
    {

        /* =========================== 内部变量 =========================== */

        // 接收频率计算
        private uint _revFrame;       // 下行新帧数
        private uint _oldFrame;       // 下行旧帧数
        private ushort _freqRead;     // 读频率

        /* =========================== 开放调用 =========================== */

        public ushort Frequency                     /* 频率 */
        {
            get { return _freqRead; }
            // set;
        }
        public void UpdateRev(byte[] data)            /* 接收帧累计 方法 */
        {
            _revFrame += 1;
        }
        public void FreqCalculate()                   /* 频率计算 方法  放在1秒的地方 */
        {

            if (_revFrame >= _oldFrame)  // 在接收数有效刷新时
            {
                _freqRead = (UInt16)(_revFrame - _oldFrame);
                _oldFrame = _revFrame;
            }

        }
    }
}
