using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ins.Comm
{
    
    public class InsComm : Gcs.Base.Collections
    {
        public InsComm()
        {
            string dataPath = Directory.GetCurrentDirectory() + "\\data\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + "\\";
            string dataName = "Ins" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');

            _bin.Load(dataPath, dataName + ".hex");

            _binTime.Load(dataPath, dataName + ".thex");

            _txt.Load(dataPath, dataName + ".txt");


            _enc = new FrameEncode(this);
        }

        internal BinaryStore _bin = new BinaryStore();
        internal FrameCut _cut = new FrameCut();
        internal BinaryStoreTime _binTime = new BinaryStoreTime();
        internal FrequencyCount _freq = new FrequencyCount();
        internal FrameDecode _dec = new FrameDecode();
        internal TextStore _txt = new TextStore();
        internal FrameEncode _enc = null;
        
        protected override void Read(byte[] buf1, byte[] buf2, ref int head, ref int tail)
        {
            _bin.Store(buf1);
            byte[][] data = _cut.Cut(buf2, ref head, ref tail);
            for (int i = 0; i < data.Length; i++)
            {
                _binTime.Store(data[i]);

                 _freq.UpdateRev(data[i]);

                _dec.DecodeOneFrame(data[i]);

                if (data[i][3] == 0x05)
                {
                    _txt.Store(_freq.Frequency, _dec);
                }

            }
        }


        public void FreqCalculate()
        {
            _freq.FreqCalculate();

        }
        public ushort DownFreqs
        {
            get { return _freq.Frequency; }
        }
        
        public FrameDecode Dec
        {
            get { return _dec; }
        }


        public FrameEncode Enc
        {
            get { return _enc; }
        }





    



       
       

       
        ~InsComm()
        {
            _bin.Close();

            _binTime.Close();

            _txt.Close();
        }
    }
}
