// * *************************************************** *
// * 功能说明: 二进制数据存储模块，带时间戳              *
// * 创建时间: 2022.12.01                                *
// * 作    者:                                           *
// * 修改时间:                                           *
// * 修 改 人:                                           *
// * 备    注:                                           *
// * *************************************************** *

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ins.Comm
{
    class BinaryStoreTime
    {
        /* =========================== 内部变量 =========================== */

        // 流存储
        private FileStream _fs = null;      
        private BinaryWriter _bw = null;
        private string _path = null;        // 存储路径

        //private DataConvert setByte = new DataConvert();

        /* =========================== 开放调用 =========================== */

        public void Load(string path, string name)      /* 初始化 */
        {
            if (!Directory.Exists(path))    // 路径
            {
                Directory.CreateDirectory(path);
            }

            _path = path + name;    // 存储名

            _fs = new FileStream(_path, FileMode.Create, FileAccess.Write);
            _bw = new BinaryWriter(_fs, Encoding.Default);
        }
        public void Store(byte[] buf)                   /* 流存储 */
        {
            if (buf.Length > 0)
            {
                byte[] timeByte = Encoding.ASCII.GetBytes(DateTime.Now.ToString("yyyyMMddHHmmss.fff")); // 时间编码
                byte[] storeBuf = new byte[4 + timeByte.Length + buf.Length + 1];       // 封装后数据长度：帧头长度4，时间码，原始数据，校验和1
                // 帧头
                storeBuf[0] = 0xEC;
                storeBuf[1] = 0x91;
                // 长度
                byte_conversion_array getInteger = new byte_conversion_array();
                getInteger.uint16_3 = (ushort)storeBuf.Length;
                storeBuf[2] = getInteger.byte1;
                storeBuf[3] = getInteger.byte0;
                // 时间码
                Buffer.BlockCopy(timeByte, 0, storeBuf, 4, timeByte.Length);    
                // 原始数据
                Buffer.BlockCopy(buf, 0, storeBuf, 4 + timeByte.Length, buf.Length);
                // 校验和
                CheckSum.SumZero(ref storeBuf, storeBuf.Length);

                if (_bw != null)
                {
                    _bw.Write(storeBuf, 0, storeBuf.Length);
                }
            }
        }
        public void Close()                             /* 关闭流 */
        {
            if (_bw != null)
            {
                try
                {
                    _bw.Close();
                }
                catch { }
            }
            if (_fs != null)
            {
                try
                {
                    _fs.Close();
                }
                catch { }
                File.SetAttributes(_path, FileAttributes.ReadOnly); // 只读
            }
        }
    }
}
