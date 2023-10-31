using Ins.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer;

namespace Ins.Forms
{
    public partial class Datasend : UserControl
    {
        private Config _config = new Config(Directory.GetCurrentDirectory() + "\\config\\", "set.ini");
        internal InsComm _ic;
        public byte[] buf;   //一帧数据
        public byte[] bytes;  //读取所有数据
        List<byte> send_bytes = new List<byte>();   //有效数据
        FileStream fileStream;
        BinaryReader binaryReader;
        int num = 0;       
        int count = 0;              //发送计数
        int frameLength = 0;
        byte check;
        byte checknum;
        static MilliTimer _timer = new MilliTimer();
        public Datasend(InsComm ic)
        {
            InitializeComponent();
            _ic = ic;
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            bytes = new byte[0];
            timer1.Enabled = true;
            num = 0;
            count = 0;
            lb_read.Text = "读取中";
            frameLength = Convert.ToInt32(tb_length.Text);
            buf = new byte[Convert.ToInt32(tb_length.Text)];  //确定帧长度
            send_bytes.Clear();                               //清空有效数据数组
            Array.Clear(buf, 0, buf.Length);       
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files(*.txt) | *.txt";
             if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
             {             
                fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(fileStream);
                long length = fileStream.Length;              
                bytes = new byte[length];
                //读取文件中的内容并保存到字节数组中
                binaryReader.Read(bytes, 0, bytes.Length);         //写入所有数据
                
                lb_read.Text = "读取完成";
                lb_total.Text = length.ToString();

                
            }
            if(cb_check.SelectedIndex==0)
            {
                checksum();
            }
            else if(cb_check.SelectedIndex==1)
            {
                checkCRC();
            }

        }
        public void checksum()
        {
            while (num < bytes.Length)
            {
                Array.Clear(buf, 0, buf.Length);              //先清空一帧长度的数组
                if (bytes[num] == 0xAA && bytes[num + 1] == 0x55)
                {
                    for (int i = 0; i < buf.Length; i++)
                    {
                        if (num + i < bytes.Length)
                        {
                            buf[i] = bytes[num + i];           //传入一帧数据
                        }
                    }
                  
                    //binaryReader.Read(buf, num, buf.Length);
                    check = 0;
                    for (int idx = 0; idx < buf.Length; idx++)
                    {
                        check += buf[idx];
                    }
                    if (check == 0)
                    {
                        num += buf.Length;                    //计数加上帧长度
                        for (int i = 0; i < buf.Length; i++)
                        {
                            send_bytes.Add(buf[i]);           //通过校验放入有效数组
                        }
                    }
                    else
                    {
                        num++;
                    }

                }
                else
                {
                    num++;
                }
                
            }
            lb_count.Text = send_bytes.Count().ToString();
        }
        public void checkCRC()
        {
            BinaryReader binaryReader = new BinaryReader(fileStream);
            while (num < bytes.Length)
            {
                Array.Clear(buf, 0, buf.Length);
                if (bytes[num] == 0xAA && bytes[num + 1] == 0x55)
                {
                    binaryReader.Read(buf, num, buf.Length);
                    byte_conversion_array getInteger = new byte_conversion_array();
                    getInteger.byte1 = buf[buf.Length - 2]; getInteger.byte0 = buf[buf.Length - 1];
                    uint b = getInteger.uint16_3;
                 
                    if (b== CheckSum.CalCRC16(buf, buf.Length - 2))
                    {
                        num += buf.Length;
                        for (int i = 0; i < buf.Length; i++)
                        {
                            send_bytes.Add(buf[i]);
                        }
                    }
                    else
                    {
                        num++;
                    }

                }
                else
                {
                    num++;
                }
            }
            lb_count.Text = send_bytes.Count().ToString();
        }
        private void btn_send_Click(object sender, EventArgs e)
        {           
            _timer.OpenTimer(_timer, 5, timer_tick);

        }
        private void timer_tick(object sender, EventArgs e)
        {
            if (count < send_bytes.Count)
            {
                Array.Clear(buf, 0, buf.Length);
                for (int i = 0; i < buf.Length; i++)
                {
                    buf[i] = send_bytes[count + i];
                }
                _ic.Write(buf);
                count += buf.Length;
                
            }
            else
            {
                _timer.StopTimer(_timer);
            }
        }
        private void Datasend_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (int.TryParse(_config.ConfigRd("Data", "length"), out frameLength)) tb_length.Text = frameLength.ToString();
            else tb_length.Text = "0";
            if (Byte.TryParse(_config.ConfigRd("Data", "check"), out checknum)) cb_check.SelectedIndex = checknum;
            else cb_check.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lb_send.Text = (((float)((float) (100*count)/ (float)send_bytes.Count()))).ToString("F4")+"%";
            lb_num.Text = count.ToString();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            send_bytes.Clear();                              
            Array.Clear(buf, 0, buf.Length);
            Array.Clear(bytes, 0, bytes.Length);
            count = 0;
            num = 0;
            _timer.StopTimer(_timer);
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            _timer.StopTimer(_timer);
        }

        private void tb_length_TextChanged(object sender, EventArgs e)
        {
            _config.ConfigWr("Data", "length", tb_length.Text);
        }

        private void cb_check_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.ConfigWr("Data", "check", cb_check.SelectedIndex.ToString());
        }
    }
}
