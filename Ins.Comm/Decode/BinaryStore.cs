// * *************************************************** *
// * 功能说明: 二进制数据存储模块                        *
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
using System.IO;

namespace Ins.Comm
{
    class BinaryStore
    {
        /* =========================== 内部变量 =========================== */

        // 流存储
        private FileStream _fs = null;      
        private BinaryWriter _bw = null;
        private string _path = null;        // 存储路径

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
            if (_bw != null)
            {
                _bw.Write(buf, 0, buf.Length);
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
