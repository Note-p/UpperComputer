// * *************************************************** *
// * 功能说明: 帧解码模块 + 接口                         *
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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;


namespace Ins.Comm
{
    public  class FrameDecode 
    {
        public float navi_Pitch;
        public float navi_Roll;
        public float navi_Espeed;
        public float navi_Nspeed;
        public float navi_Aspeed;
        public float navi_Yaw;
        public float navi_Lng;
        public float navi_Lat;
        public float navi_Alt;

        public float gps_Lng;
        public float gps_Lat;
        public float gps_Alt;
        public float gps_Espeed;
        public float gps_Nspeed;
        public float gps_Aspeed;
        public float gps_Dspeed;
        public float gps_TrackAngle;
        public float gps_Heading;
        public float gps_Headingprec;
        public float gps_Horiacc;
        public byte gps_Count;
        public ulong gps_iTow;

        public float sensor_Xangv;
        public float sensor_Yangv;
        public float sensor_Zangv;
        public float sensor_Xacc;
        public float sensor_Yacc;
        public float sensor_Zacc;
        public float sensor_Xmag;
        public float sensor_Ymag;
        public float sensor_Zmag;
        public byte sensor_Temp;

        public float prec_Pitch;
        public float prec_Roll;
        public float prec_Yaw;
        public float prec_E;
        public float prec_N;
        public float prec_A;
        public float prec_Lng;
        public float prec_Lat;
        public float prec_Alt;
        public float prec_Ebx;
        public float prec_Eby;
        public float prec_Ebz;
        public float prec_Dbx;
        public float prec_Dby;
        public float prec_Dbz;
        public float prec_X;
        public float prec_Y;
        public float prec_Z;
        public float prec_Time;

        public float sup_Alt;
        public byte sup_Mode;

      
    
        public FrameDecode()
        {
        }

        internal void DecodeOneFrame(byte[] Buf)                /* 一帧解码 */
        {
            try
            {
                if (Buf[2] == 0x3C && Buf[3] == 0x01 && Buf[4] == 0x01)
                {
                    DecNavi(Buf.Length, Buf);
                }
                else if(Buf[2] == 0x3C && Buf[3] == 0x02 && Buf[4] == 0x01)
                {
                    DecGps(Buf.Length, Buf);
                }
                else if (Buf[2] == 0x3C && Buf[3] == 0x03 && Buf[4] == 0x01)
                {
                    DecSensor(Buf.Length, Buf);
                }
                else if (Buf[2] == 0x7C && Buf[3] == 0x04 && Buf[4] == 0x01)
                {
                    DecPrec(Buf.Length, Buf);
                }
                else if (Buf[2] == 0x1C && Buf[3] == 0x05 && Buf[4] == 0x01)
                {
                    DecSup(Buf.Length, Buf);
                }
                else if (Buf[2] == 0x3C && Buf[3] == 0x0F)
                {
                    DecReturn(Buf.Length, Buf);
                }
            }
            catch
            {

            }
        }

        private void DecNavi(int length, byte[] tmp)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            getInteger.byte0 = tmp[10]; getInteger.byte1 = tmp[9];
            navi_Pitch = getInteger.int16_3 * 0.01f;
            getInteger.byte0 = tmp[12]; getInteger.byte1 = tmp[11];
            navi_Roll = getInteger.int16_3 * 0.01f;
            getInteger.byte0 = tmp[14]; getInteger.byte1 = tmp[13];
            navi_Yaw = getInteger.uint16_3 * 0.01f;
            getInteger.byte0 = tmp[18]; getInteger.byte1 = tmp[17];
            getInteger.byte2 = tmp[16]; getInteger.byte3 = tmp[15];
            navi_Lng = getInteger.int32_1 * 0.0000001f;
            getInteger.byte0 = tmp[22]; getInteger.byte1 = tmp[21];
            getInteger.byte2 = tmp[20]; getInteger.byte3 = tmp[19];
            navi_Lat = getInteger.int32_1 * 0.0000001f;
            getInteger.byte0 = tmp[26]; getInteger.byte1 = tmp[25];
            getInteger.byte2 = tmp[24]; getInteger.byte3 = tmp[23];
            navi_Alt = getInteger.int32_1 * 0.01f;
            getInteger.byte0 = tmp[28]; getInteger.byte1 = tmp[27];
            navi_Espeed = getInteger.int16_3 * 0.01f;
            getInteger.byte0 = tmp[30]; getInteger.byte1 = tmp[29];
            navi_Nspeed = getInteger.int16_3 * 0.01f;
            getInteger.byte0 = tmp[32]; getInteger.byte1 = tmp[31];
            navi_Aspeed = getInteger.int16_3 * 0.01f;
        }
        private void DecGps(int length, byte[] tmp)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            getInteger.byte0 = tmp[12]; getInteger.byte1 = tmp[11];
            getInteger.byte2 = tmp[10]; getInteger.byte3 = tmp[9];
            gps_Lng = getInteger.int32_1 * 0.0000001f;
            getInteger.byte0 = tmp[16]; getInteger.byte1 = tmp[15];
            getInteger.byte2 = tmp[14]; getInteger.byte3 = tmp[13];
            gps_Lng = getInteger.int32_1 * 0.0000001f;
            getInteger.byte0 = tmp[20]; getInteger.byte1 = tmp[19];
            getInteger.byte2 = tmp[18]; getInteger.byte3 = tmp[17];
            gps_Alt = getInteger.int32_1 * 0.01f;
            getInteger.byte0 = tmp[22]; getInteger.byte1 = tmp[21];
            gps_Espeed = getInteger.int16_3 * 0.01f;
            getInteger.byte0 = tmp[24]; getInteger.byte1 = tmp[23];
            gps_Nspeed = getInteger.int16_3 * 0.01f;
            getInteger.byte0 = tmp[26]; getInteger.byte1 = tmp[25];
            gps_Aspeed = getInteger.int16_3 * 0.01f;
            getInteger.byte0 = tmp[28]; getInteger.byte1 = tmp[27];
            gps_TrackAngle = getInteger.uint16_3 * 0.01f;
            getInteger.byte0 = tmp[30]; getInteger.byte1 = tmp[29];
            gps_Heading = getInteger.uint16_3 * 0.01f;
            getInteger.byte0 = tmp[34]; getInteger.byte1 = tmp[33];
            getInteger.byte2 = tmp[32]; getInteger.byte3 = tmp[31];
            gps_Headingprec = getInteger.float_1;
            getInteger.byte0 = tmp[36]; getInteger.byte1 = tmp[35];
            gps_Dspeed = getInteger.int16_3 * 0.01f;
            gps_Count = tmp[37];
            getInteger.byte0 = tmp[41]; getInteger.byte1 = tmp[40];
            getInteger.byte2 = tmp[39]; getInteger.byte3 = tmp[38];
            gps_Horiacc = getInteger.float_1;
            getInteger.byte0 = tmp[45]; getInteger.byte1 = tmp[44];
            getInteger.byte2 = tmp[43]; getInteger.byte3 = tmp[42];
            gps_iTow = (ulong)getInteger.int32_1;
        }
        private void DecSensor(int length, byte[] tmp)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            getInteger.byte0 = tmp[12]; getInteger.byte1 = tmp[11];
            getInteger.byte2 = tmp[10]; getInteger.byte3 = tmp[9];
            sensor_Xangv = getInteger.float_1;
            getInteger.byte0 = tmp[16]; getInteger.byte1 = tmp[15];
            getInteger.byte2 = tmp[14]; getInteger.byte3 = tmp[13];
            sensor_Yangv = getInteger.float_1;
            getInteger.byte0 = tmp[20]; getInteger.byte1 = tmp[19];
            getInteger.byte2 = tmp[18]; getInteger.byte3 = tmp[17];
            sensor_Zangv = getInteger.float_1;
            getInteger.byte0 = tmp[24]; getInteger.byte1 = tmp[23];
            getInteger.byte2 = tmp[22]; getInteger.byte3 = tmp[21];
            sensor_Xacc = getInteger.float_1;
            getInteger.byte0 = tmp[28]; getInteger.byte1 = tmp[27];
            getInteger.byte2 = tmp[26]; getInteger.byte3 = tmp[25];
            sensor_Yacc = getInteger.float_1;
            getInteger.byte0 = tmp[32]; getInteger.byte1 = tmp[31];
            getInteger.byte2 = tmp[30]; getInteger.byte3 = tmp[29];
            sensor_Zacc = getInteger.float_1;
            getInteger.byte0 = tmp[36]; getInteger.byte1 = tmp[35];
            getInteger.byte2 = tmp[34]; getInteger.byte3 = tmp[33];
            sensor_Xmag = getInteger.float_1;
            getInteger.byte0 = tmp[40]; getInteger.byte1 = tmp[39];
            getInteger.byte2 = tmp[38]; getInteger.byte3 = tmp[37];
            sensor_Ymag = getInteger.float_1;
            getInteger.byte0 = tmp[44]; getInteger.byte1 = tmp[43];
            getInteger.byte2 = tmp[42]; getInteger.byte3 = tmp[41];
            sensor_Zmag = getInteger.float_1;
            sensor_Temp = tmp[45];
        }
        private void DecPrec(int length, byte[] tmp)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            getInteger.byte0 = tmp[12]; getInteger.byte1 = tmp[11];
            getInteger.byte2 = tmp[10]; getInteger.byte3 = tmp[9];
            prec_Pitch = getInteger.float_1;
            getInteger.byte0 = tmp[16]; getInteger.byte1 = tmp[15];
            getInteger.byte2 = tmp[14]; getInteger.byte3 = tmp[13];
            prec_Roll = getInteger.float_1;
            getInteger.byte0 = tmp[20]; getInteger.byte1 = tmp[19];
            getInteger.byte2 = tmp[18]; getInteger.byte3 = tmp[17];
            prec_Yaw = getInteger.float_1;
            getInteger.byte0 = tmp[24]; getInteger.byte1 = tmp[23];
            getInteger.byte2 = tmp[22]; getInteger.byte3 = tmp[21];
            prec_E = getInteger.float_1;
            getInteger.byte0 = tmp[28]; getInteger.byte1 = tmp[27];
            getInteger.byte2 = tmp[26]; getInteger.byte3 = tmp[25];
            prec_N = getInteger.float_1;
            getInteger.byte0 = tmp[32]; getInteger.byte1 = tmp[31];
            getInteger.byte2 = tmp[30]; getInteger.byte3 = tmp[29];
            prec_A = getInteger.float_1;
            getInteger.byte0 = tmp[36]; getInteger.byte1 = tmp[35];
            getInteger.byte2 = tmp[34]; getInteger.byte3 = tmp[33];
            prec_Lng = getInteger.float_1;
            getInteger.byte0 = tmp[40]; getInteger.byte1 = tmp[39];
            getInteger.byte2 = tmp[38]; getInteger.byte3 = tmp[37];
            prec_Lat = getInteger.float_1;
            getInteger.byte0 = tmp[44]; getInteger.byte1 = tmp[43];
            getInteger.byte2 = tmp[42]; getInteger.byte3 = tmp[41];
            prec_Alt = getInteger.float_1;
            getInteger.byte0 = tmp[48]; getInteger.byte1 = tmp[47];
            getInteger.byte2 = tmp[46]; getInteger.byte3 = tmp[45];
            prec_Ebx = getInteger.float_1;
            getInteger.byte0 = tmp[52]; getInteger.byte1 = tmp[51];
            getInteger.byte2 = tmp[50]; getInteger.byte3 = tmp[49];
            prec_Eby = getInteger.float_1;
            getInteger.byte0 = tmp[56]; getInteger.byte1 = tmp[55];
            getInteger.byte2 = tmp[54]; getInteger.byte3 = tmp[53];
            prec_Ebz = getInteger.float_1;
            getInteger.byte0 = tmp[60]; getInteger.byte1 = tmp[59];
            getInteger.byte2 = tmp[58]; getInteger.byte3 = tmp[57];
            prec_Dbx = getInteger.float_1;
            getInteger.byte0 = tmp[64]; getInteger.byte1 = tmp[63];
            getInteger.byte2 = tmp[62]; getInteger.byte3 = tmp[61];
            prec_Dby = getInteger.float_1;
            getInteger.byte0 = tmp[68]; getInteger.byte1 = tmp[67];
            getInteger.byte2 = tmp[66]; getInteger.byte3 = tmp[65];
            prec_Dbz = getInteger.float_1;
            getInteger.byte0 = tmp[72]; getInteger.byte1 = tmp[71];
            getInteger.byte2 = tmp[70]; getInteger.byte3 = tmp[69];
            prec_X = getInteger.float_1;
            getInteger.byte0 = tmp[76]; getInteger.byte1 = tmp[75];
            getInteger.byte2 = tmp[74]; getInteger.byte3 = tmp[73];
            prec_Y = getInteger.float_1;
            getInteger.byte0 = tmp[80]; getInteger.byte1 = tmp[79];
            getInteger.byte2 = tmp[78]; getInteger.byte3 = tmp[77];
            prec_Z = getInteger.float_1;
            getInteger.byte0 = tmp[84]; getInteger.byte1 = tmp[83];
            getInteger.byte2 = tmp[82]; getInteger.byte3 = tmp[81];
            prec_Time = getInteger.float_1;
        }
        private void DecSup(int length, byte[] tmp)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            getInteger.byte0 = tmp[12]; getInteger.byte1 = tmp[11];
            getInteger.byte2 = tmp[10]; getInteger.byte3 = tmp[9];
            sup_Alt = getInteger.float_1;
            sup_Mode = tmp[13];
        }
        private void DecReturn(int length, byte[] tmp)
        {

        }
    }
}

