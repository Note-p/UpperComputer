using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;
using Gcs.Base;

namespace Ins.Comm
{
    
     public class FrameEncode 
     {
        private InsComm _ic = null;
        public FrameEncode(InsComm ic)
        {
            _ic = ic;
        }
        
        public void Cmd_Connect()
        {
            
            byte_conversion_array getInteger = new byte_conversion_array();
            byte[] buffer = new byte[16];
            buffer[0] = 0xAA;
            buffer[1] = 0x55;
            buffer[2] = 0x0C;
            buffer[3] = 0x01;
            buffer[4] = 0;
            buffer[5] = 0;
            buffer[6] = 0;
            buffer[7] = 0;
            buffer[8] = 0;
            buffer[9] = 0;
            buffer[10] = 0;
            buffer[11] = 0;
            buffer[12] = 0;
            buffer[13] = 0;
            getInteger.uint16_3 = CheckSum.CalCRC16(buffer, buffer.Length - 2);
            buffer[14] = getInteger.byte1;
            buffer[15] = getInteger.byte0;
            _ic.Write(buffer);
        }
        public void Cmd_IMU(float x, float y, float z)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            byte[] buffer = new byte[16];
            buffer[0] = 0xAA;
            buffer[1] = 0x55;
            buffer[2] = 0x0C;
            buffer[3] = 0x02;
            getInteger.int16_3 = (short)(x * 100);
            buffer[4] = getInteger.byte1;
            buffer[5] = getInteger.byte0;
            getInteger.int16_3 = (short)(y * 100);
            buffer[6] = getInteger.byte1;
            buffer[7] = getInteger.byte0;
            getInteger.int16_3 = (short)(z * 100);
            buffer[8] = getInteger.byte1;
            buffer[9] = getInteger.byte0;
            buffer[10] = 0;
            buffer[11] = 0;
            buffer[12] = 0;
            buffer[13] = 0;
            getInteger.uint16_3 = CheckSum.CalCRC16(buffer, buffer.Length - 2);
            buffer[14] = getInteger.byte1;
            buffer[15] = getInteger.byte0;
            _ic.Write(buffer);
        }
        public void Cmd_Sensor(byte type)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            byte[] buffer = new byte[16];
            buffer[0] = 0xAA;
            buffer[1] = 0x55;
            buffer[2] = 0x0C;
            buffer[3] = 0x03;
            buffer[4] = type;
            buffer[5] = 0;
            buffer[6] = 0;
            buffer[7] = 0;
            buffer[8] = 0;
            buffer[9] = 0;
            buffer[10] = 0;
            buffer[11] = 0;
            buffer[12] = 0;
            buffer[13] = 0;
            getInteger.uint16_3 = CheckSum.CalCRC16(buffer, buffer.Length - 2);
            buffer[14] = getInteger.byte1;
            buffer[15] = getInteger.byte0;
            _ic.Write(buffer);
        }
        public void Cmd_InitCoor(float lng, float lat, float alt)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            byte[] buffer = new byte[32];
            buffer[0] = 0xAA;
            buffer[1] = 0x55;
            buffer[2] = 0x1C;
            buffer[3] = 0x04;
            getInteger.int32_1 = (int)(lng * 10000000);
            buffer[4] = getInteger.byte3;
            buffer[5] = getInteger.byte2;
            buffer[6] = getInteger.byte1;
            buffer[7] = getInteger.byte0;
            getInteger.int32_1 = (int)(lat * 10000000);
            buffer[8] = getInteger.byte3;
            buffer[9] = getInteger.byte2;
            buffer[10] = getInteger.byte1;
            buffer[11] = getInteger.byte0;
            getInteger.int32_1 = (int)(alt * 10000000);
            buffer[12] = getInteger.byte3;
            buffer[13] = getInteger.byte2;
            buffer[14] = getInteger.byte1;
            buffer[15] = getInteger.byte0;
            buffer[16] = 0;
            buffer[17] = 0;
            buffer[18] = 0;
            buffer[19] = 0;
            buffer[20] = 0;
            buffer[21] = 0;
            buffer[22] = 0;
            buffer[23] = 0;
            buffer[24] = 0;
            buffer[25] = 0;
            buffer[26] = 0;
            buffer[27] = 0;
            buffer[28] = 0;
            buffer[29] = 0;
            getInteger.uint16_3 = CheckSum.CalCRC16(buffer, buffer.Length - 2);
            buffer[30] = getInteger.byte1;
            buffer[31] = getInteger.byte0;
            _ic.Write(buffer);
        }
        public void Cmd_Acc(float f1, float f2, float f3, float f4, float f5, float f6, float f7, float f8, float f9, float bias_x, float bias_y, float bias_z)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            byte[] buffer = new byte[64];
            buffer[0] = 0xAA;
            buffer[1] = 0x55;
            buffer[2] = 0x3C;
            buffer[3] = 0x05;
            getInteger.float_1 = f1;
            buffer[4] = getInteger.byte3;
            buffer[5] = getInteger.byte2;
            buffer[6] = getInteger.byte1;
            buffer[7] = getInteger.byte0;
            getInteger.float_1 = f2;
            buffer[8] = getInteger.byte3;
            buffer[9] = getInteger.byte2;
            buffer[10] = getInteger.byte1;
            buffer[11] = getInteger.byte0;
            getInteger.float_1 = f3;
            buffer[12] = getInteger.byte3;
            buffer[13] = getInteger.byte2;
            buffer[14] = getInteger.byte1;
            buffer[15] = getInteger.byte0;
            getInteger.float_1 = f4;
            buffer[16] = getInteger.byte3;
            buffer[17] = getInteger.byte2;
            buffer[18] = getInteger.byte1;
            buffer[19] = getInteger.byte0;
            getInteger.float_1 = f5;
            buffer[20] = getInteger.byte3;
            buffer[21] = getInteger.byte2;
            buffer[22] = getInteger.byte1;
            buffer[23] = getInteger.byte0;
            getInteger.float_1 = f6;
            buffer[24] = getInteger.byte3;
            buffer[25] = getInteger.byte2;
            buffer[26] = getInteger.byte1;
            buffer[27] = getInteger.byte0;
            getInteger.float_1 = f7;
            buffer[28] = getInteger.byte3;
            buffer[29] = getInteger.byte2;
            buffer[30] = getInteger.byte1;
            buffer[31] = getInteger.byte0;
            getInteger.float_1 = f8;
            buffer[32] = getInteger.byte3;
            buffer[33] = getInteger.byte2;
            buffer[34] = getInteger.byte1;
            buffer[35] = getInteger.byte0;
            getInteger.float_1 = f9;
            buffer[36] = getInteger.byte3;
            buffer[37] = getInteger.byte2;
            buffer[38] = getInteger.byte1;
            buffer[39] = getInteger.byte0;
            getInteger.float_1 = bias_x;
            buffer[40] = getInteger.byte3;
            buffer[41] = getInteger.byte2;
            buffer[42] = getInteger.byte1;
            buffer[43] = getInteger.byte0;
            getInteger.float_1 = bias_y;
            buffer[44] = getInteger.byte3;
            buffer[45] = getInteger.byte2;
            buffer[46] = getInteger.byte1;
            buffer[47] = getInteger.byte0;
            getInteger.float_1 = bias_z;
            buffer[48] = getInteger.byte3;
            buffer[49] = getInteger.byte2;
            buffer[50] = getInteger.byte1;
            buffer[51] = getInteger.byte0;
            buffer[52] = 0;
            buffer[53] = 0;
            buffer[54] = 0;
            buffer[55] = 0;
            buffer[56] = 0;
            buffer[57] = 0;
            buffer[58] = 0;
            buffer[59] = 0;
            buffer[60] = 0;
            buffer[61] = 0;
            getInteger.uint16_3 = CheckSum.CalCRC16(buffer, buffer.Length - 2);
            buffer[62] = getInteger.byte1;
            buffer[63] = getInteger.byte0;
            _ic.Write(buffer);
        }
        public void Cmd_Mag(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9, float bias_x, float bias_y, float bias_z)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            byte[] buffer = new byte[64];
            buffer[0] = 0xAA;
            buffer[1] = 0x55;
            buffer[2] = 0x3C;
            buffer[3] = 0x06;
            getInteger.float_1 = m1;
            buffer[4] = getInteger.byte3;
            buffer[5] = getInteger.byte2;
            buffer[6] = getInteger.byte1;
            buffer[7] = getInteger.byte0;
            getInteger.float_1 = m2;
            buffer[8] = getInteger.byte3;
            buffer[9] = getInteger.byte2;
            buffer[10] = getInteger.byte1;
            buffer[11] = getInteger.byte0;
            getInteger.float_1 = m3;
            buffer[12] = getInteger.byte3;
            buffer[13] = getInteger.byte2;
            buffer[14] = getInteger.byte1;
            buffer[15] = getInteger.byte0;
            getInteger.float_1 = m4;
            buffer[16] = getInteger.byte3;
            buffer[17] = getInteger.byte2;
            buffer[18] = getInteger.byte1;
            buffer[19] = getInteger.byte0;
            getInteger.float_1 = m5;
            buffer[20] = getInteger.byte3;
            buffer[21] = getInteger.byte2;
            buffer[22] = getInteger.byte1;
            buffer[23] = getInteger.byte0;
            getInteger.float_1 = m6;
            buffer[24] = getInteger.byte3;
            buffer[25] = getInteger.byte2;
            buffer[26] = getInteger.byte1;
            buffer[27] = getInteger.byte0;
            getInteger.float_1 = m7;
            buffer[28] = getInteger.byte3;
            buffer[29] = getInteger.byte2;
            buffer[30] = getInteger.byte1;
            buffer[31] = getInteger.byte0;
            getInteger.float_1 = m8;
            buffer[32] = getInteger.byte3;
            buffer[33] = getInteger.byte2;
            buffer[34] = getInteger.byte1;
            buffer[35] = getInteger.byte0;
            getInteger.float_1 = m9;
            buffer[36] = getInteger.byte3;
            buffer[37] = getInteger.byte2;
            buffer[38] = getInteger.byte1;
            buffer[39] = getInteger.byte0;
            getInteger.float_1 = bias_x;
            buffer[40] = getInteger.byte3;
            buffer[41] = getInteger.byte2;
            buffer[42] = getInteger.byte1;
            buffer[43] = getInteger.byte0;
            getInteger.float_1 = bias_y;
            buffer[44] = getInteger.byte3;
            buffer[45] = getInteger.byte2;
            buffer[46] = getInteger.byte1;
            buffer[47] = getInteger.byte0;
            getInteger.float_1 = bias_z;
            buffer[48] = getInteger.byte3;
            buffer[49] = getInteger.byte2;
            buffer[50] = getInteger.byte1;
            buffer[51] = getInteger.byte0;
            buffer[52] = 0;
            buffer[53] = 0;
            buffer[54] = 0;
            buffer[55] = 0;
            buffer[56] = 0;
            buffer[57] = 0;
            buffer[58] = 0;
            buffer[59] = 0;
            buffer[60] = 0;
            buffer[61] = 0;
            getInteger.uint16_3 = CheckSum.CalCRC16(buffer, buffer.Length - 2);
            buffer[62] = getInteger.byte1;
            buffer[63] = getInteger.byte0;
            _ic.Write(buffer);
        }
        public void Cmd_IMU1(float x, float y, float z)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            byte[] buffer = new byte[16];
            buffer[0] = 0xAA;
            buffer[1] = 0x55;
            buffer[2] = 0x0C;
            buffer[3] = 0x07;
            getInteger.int16_3 = (short)(x * 100);
            buffer[4] = getInteger.byte1;
            buffer[5] = getInteger.byte0;
            getInteger.int16_3 = (short)(y * 100);
            buffer[6] = getInteger.byte1;
            buffer[7] = getInteger.byte0;
            getInteger.int16_3 = (short)(z * 100);
            buffer[8] = getInteger.byte1;
            buffer[9] = getInteger.byte0;
            buffer[10] = 0;
            buffer[11] = 0;
            buffer[12] = 0;
            buffer[13] = 0;
            getInteger.uint16_3 = CheckSum.CalCRC16(buffer, buffer.Length - 2);
            buffer[14] = getInteger.byte1;
            buffer[15] = getInteger.byte0;
            _ic.Write(buffer);
        }

      

        public void Cmd_Gyro(float xA, float xB, float xC, float xD, float yA, float yB, float yC, float yD, float zA, float zB, float zC, float zD)
        {
            byte_conversion_array getInteger = new byte_conversion_array();
            byte[] buffer = new byte[64];
            buffer[0] = 0xAA;
            buffer[1] = 0x55;
            buffer[2] = 0x3C;
            buffer[3] = 0x08;
            getInteger.float_1 = xA;
            buffer[4] = getInteger.byte3;
            buffer[5] = getInteger.byte2;
            buffer[6] = getInteger.byte1;
            buffer[7] = getInteger.byte0;
            getInteger.float_1 = xB;
            buffer[8] = getInteger.byte3;
            buffer[9] = getInteger.byte2;
            buffer[10] = getInteger.byte1;
            buffer[11] = getInteger.byte0;
            getInteger.float_1 = xC;
            buffer[12] = getInteger.byte3;
            buffer[13] = getInteger.byte2;
            buffer[14] = getInteger.byte1;
            buffer[15] = getInteger.byte0;
            getInteger.float_1 = xD;
            buffer[16] = getInteger.byte3;
            buffer[17] = getInteger.byte2;
            buffer[18] = getInteger.byte1;
            buffer[19] = getInteger.byte0;
            getInteger.float_1 = yA;
            buffer[20] = getInteger.byte3;
            buffer[21] = getInteger.byte2;
            buffer[22] = getInteger.byte1;
            buffer[23] = getInteger.byte0;
            getInteger.float_1 = yB;
            buffer[24] = getInteger.byte3;
            buffer[25] = getInteger.byte2;
            buffer[26] = getInteger.byte1;
            buffer[27] = getInteger.byte0;
            getInteger.float_1 = yC;
            buffer[28] = getInteger.byte3;
            buffer[29] = getInteger.byte2;
            buffer[30] = getInteger.byte1;
            buffer[31] = getInteger.byte0;
            getInteger.float_1 = yD;
            buffer[32] = getInteger.byte3;
            buffer[33] = getInteger.byte2;
            buffer[34] = getInteger.byte1;
            buffer[35] = getInteger.byte0;
            getInteger.float_1 = zA;
            buffer[36] = getInteger.byte3;
            buffer[37] = getInteger.byte2;
            buffer[38] = getInteger.byte1;
            buffer[39] = getInteger.byte0;

            getInteger.float_1 = zB;
            buffer[40] = getInteger.byte3;
            buffer[41] = getInteger.byte2;
            buffer[42] = getInteger.byte1;
            buffer[43] = getInteger.byte0;

            getInteger.float_1 = zC;
            buffer[44] = getInteger.byte3;
            buffer[45] = getInteger.byte2;
            buffer[46] = getInteger.byte1;
            buffer[47] = getInteger.byte0;

            getInteger.float_1 = zD;
            buffer[48] = getInteger.byte3;
            buffer[49] = getInteger.byte2;
            buffer[50] = getInteger.byte1;
            buffer[51] = getInteger.byte0;

            buffer[52] = 0;
            buffer[53] = 0;
            buffer[54] = 0;
            buffer[55] = 0;

            buffer[56] = 0;
            buffer[57] = 0;
            buffer[58] = 0;
            buffer[59] = 0;

            buffer[60] = 0;
            buffer[61] = 0;
            getInteger.uint16_3 = CheckSum.CalCRC16(buffer, buffer.Length - 2);
            buffer[62] = getInteger.byte1;
            buffer[63] = getInteger.byte0;
            _ic.Write(buffer);
        }
    }
}
