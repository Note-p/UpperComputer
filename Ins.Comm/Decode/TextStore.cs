// * *************************************************** *
// * 功能说明: 文本存储模块 + 接口                       *
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
using System.Globalization;



namespace Ins.Comm
{
    class TextStore
    {        
        /* =========================== 内部变量 =========================== */


        private FileStream _fs = null;         // Txt流存储（多机）
        private StreamWriter _sw = null;
        private string _path = null;

        /* =========================== 开放调用 =========================== */

        public void Load(string path, string name)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _path = path + name;   // 存储文件名

            _fs = new FileStream(_path, FileMode.Create, FileAccess.Write);
            _sw = new StreamWriter(_fs, Encoding.Default);

            switch (CultureInfo.CurrentUICulture.Name)      // 加入帧头
                {
                    case "en-US":       // 英语
                        {
                        _sw.Write("_SystemTime");
                        _sw.Write("  " + "_TotalSeconds");
                        _sw.Write("  " + "Datalink1Freq");
                        _sw.Write("  " + "Datalink2Freq");


                        }
                        break;

                    default:            // 简中(默认)
                        {
                        _sw.Write("_系统时间");
                        _sw.Write("  " + "_本地总秒数");
                        _sw.Write("  " + "遥测频率");
                        _sw.Write("  " + "数传2遥测频率");
                        _sw.Write("  " + "俯仰角");
                        _sw.Write("  " + "滚转角");
                        _sw.Write("  " + "偏航角");
                        _sw.Write("  " + "经度");
                        _sw.Write("  " + "纬度");
                        _sw.Write("  " + "高度");
                        _sw.Write("  " + "东速");
                        _sw.Write("  " + "北速");
                        _sw.Write("  " + "天速");
                        _sw.Write("  " + "GPS经度");
                        _sw.Write("  " + "GPS纬度");
                        _sw.Write("  " + "GPS高度");
                        _sw.Write("  " + "GPS东速");
                        _sw.Write("  " + "GPS北速");
                        _sw.Write("  " + "GPS天速");
                        _sw.Write("  " + "航迹角");
                        _sw.Write("  " + "双天线航向");
                        _sw.Write("  " + "双天线航向精度");
                        _sw.Write("  " + "地面速度");
                        _sw.Write("  " + "星数");
                        _sw.Write("  " + "水平精度");
                        _sw.Write("  " + "iTow");
                        _sw.Write("  " + "x向角速度");
                        _sw.Write("  " + "y向角速度");
                        _sw.Write("  " + "z向角速度");
                        _sw.Write("  " + "x向加速度");
                        _sw.Write("  " + "y向加速度");
                        _sw.Write("  " + "z向加速度");
                        _sw.Write("  " + "x向磁强计");
                        _sw.Write("  " + "y向磁强计");
                        _sw.Write("  " + "z向磁强计");
                        _sw.Write("  " + "温度");
                        _sw.Write("  " + "俯仰精度");
                        _sw.Write("  " + "滚转精度");
                        _sw.Write("  " + "偏航精度");
                        _sw.Write("  " + "东速精度");
                        _sw.Write("  " + "北速精度");
                        _sw.Write("  " + "天速精度");
                        _sw.Write("  " + "经度状态精度");
                        _sw.Write("  " + "纬度状态精度");
                        _sw.Write("  " + "高度精度");
                        _sw.Write("  " + "ebx");
                        _sw.Write("  " + "eby");
                        _sw.Write("  " + "ebz");
                        _sw.Write("  " + "dbx");
                        _sw.Write("  " + "dby");
                        _sw.Write("  " + "dbz");
                        _sw.Write("  " + "x向杆臂误差经度");
                        _sw.Write("  " + "y向杆臂误差经度");
                        _sw.Write("  " + "z向杆臂误差经度");
                        _sw.Write("  " + "延时误差精度");
                        _sw.Write("  " + "气压高度");
                        _sw.Write("  " + "导航模式");



                        }
                        break;
            }

                _sw.Write("\r\n");
                _sw.Flush();
            
        }
        public void Store(ushort revfreq, FrameDecode frameDecode)
        {
            try
            {

                _sw.Write("{0,-15}", DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0') + DateTime.Now.Millisecond.ToString().PadLeft(3, '0'));
                _sw.Write(" ");
                _sw.Write((DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second + (float)DateTime.Now.Millisecond / 1000).ToString("F3"));
                _sw.Write(" ");
                // 数传频率
                _sw.Write(revfreq.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.navi_Pitch.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.navi_Roll.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.navi_Yaw.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.navi_Lng.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.navi_Lat.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.navi_Alt.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.navi_Espeed.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.navi_Nspeed.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.navi_Aspeed.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Lng.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Lat.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Alt.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Espeed.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Nspeed.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Aspeed.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_TrackAngle.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Heading.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Headingprec.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Dspeed.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Count.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_Horiacc.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.gps_iTow.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sensor_Xangv.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sensor_Yangv.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sensor_Zangv.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sensor_Xacc.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sensor_Yacc.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sensor_Zacc.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sensor_Xmag.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sensor_Ymag.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sensor_Zmag.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Pitch.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Roll.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Yaw.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_E.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_N.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_A.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Lng.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Lat.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Alt.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Ebx.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Eby.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Ebz.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Dbx.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Dby.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Dbz.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_X.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Y.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Z.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.prec_Time.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sup_Alt.ToString());
                _sw.Write(" ");
                _sw.Write(frameDecode.sup_Mode.ToString());
                _sw.Write(" ");
                _sw.Write("\r\n");
                _sw.Flush();


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Close()
        {
            if (_sw != null)
            {
                try
                {
                    _sw.Close();
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
