// * *************************************************** *
// * 功能说明: ini配置文件类                             *
// * 创建时间: 2020.07.13                                *
// * 作    者: 李先江                                    *
// * 修改时间: -                                         *
// * 修 改 人: -                                         *
// * 备    注: 存档本地配置文件                          *
// * *************************************************** *

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace Ins.Forms
{
    public class Config
    {
        /* =========================== 内部变量 =========================== */

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        
        // 存储路径
        public string _path;
        // 存储名称
        public string _name;

        /* =========================== 构造函数 =========================== */

        public Config(string path, string name)
        {
            _path = path;
            _name = name;

            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
        }

        /* =========================== 开放调用 =========================== */

        public void ConfigWr(string Section, string Key, string Value)
        {
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);

            WritePrivateProfileString(Section, Key, Value, _path + _name);
        }
        public string ConfigRd(string Section, string Key)
        {
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);

            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, _path + _name);
            return temp.ToString();
        }
    }
}
