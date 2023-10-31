using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Timer;
using WindowsFormsApp;
using Ins.Comm;

namespace Ins.Forms
{
    public partial class Form1 : Form
    {
       
        Ins.Comm.InsComm ic;
        Ins.Comm.FrameDecode fd;
        Panel_set pset = null;
        Datasend dsend = null;
        private Config _config = new Config(Directory.GetCurrentDirectory() + "\\config\\", "set.ini");
        public Form1()
        {
            InitializeComponent();
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ic.ConnStatus == false)
                {
                    ic.Connect(460800, cb_port.Text);
                    ic.Enc.Cmd_Connect();
                    timer.Enabled = true;
                    _config.ConfigWr("Serial", "Port", cb_port.Text);
                }
                else
                {
                    ic.Close();
                    timer.Enabled = false;

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            if (ic.ConnStatus==true)
            {
                btn_connect.Text = "断开连接";
                lb_connect.Text = "已连接";
                lb_connect.ForeColor = Color.Green;
            }
            else
            {
                btn_connect.Text = "点击连接";
                lb_connect.Text = "未连接";
                lb_connect.ForeColor = Color.Red;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ic = new Comm.InsComm();
            //  fd = new Comm.FrameDecode();
            string[] ports = SerialPort.GetPortNames();             // 预读所有串口
            cb_port.Items.Clear();               // 串口预初始化
            if (ports.Length > 0)
            {
                foreach (string port in ports)
                {
                    cb_port.Items.Add(port);
                }

                cb_port.SelectedItem = _config.ConfigRd("Serial","Port");
            }
            btn_connect.Text = "点击连接";
            lb_connect.Text = "未连接";
            lb_connect.ForeColor = Color.Red;
        }

        private void CommMonitor()
        {
            if (ic.ErrorClose == true && ic.ConnStatus == false)
            {
                ic.ErrorReset();
                btn_link_Click(new object(), new EventArgs());
                CommError();
            }
        }
        public void CommError()
        {
            Form1_Load(new object(), new EventArgs());
            MessageBox.Show(this, "连接异常断开","错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        Link _link = null;
        private void link_Click(object sender, EventArgs e)
        {
            if(_link==null||_link.IsDisposed==true)
            {
                _link = new Link();
                _link.Show(this);
            }
            else if(_link.Visible==false)
            {
                _link.Show(this);
                _link.BringToFront();
            }
            else
            {
                _link.BringToFront();
                _link.Location = new Point(_link.Owner.Location.X + 10, _link.Owner.Location.Y + 10);
                _link.WindowState = FormWindowState.Normal;
            }
        }

        Plots _plots = null;
        private void charts_Click(object sender, EventArgs e)
        {
            if (_plots == null || _plots.IsDisposed == true)
            {
                _plots = new Plots(ic);
                _plots.Show(this);
            }
            else if (_plots.Visible == false)
            {
                _plots.Show(this);
                _plots.BringToFront();
            }
            else
            {
                _plots.BringToFront();
                _plots.Location = new Point(_plots.Owner.Location.X + 10, _plots.Owner.Location.Y + 10);
                _plots.WindowState = FormWindowState.Normal;
            }
        }

        //Setting _setting = null;
        //private void settings_Click(object sender, EventArgs e)
        //{
        //    if (_setting == null || _setting.IsDisposed == true)
        //    {
        //        _setting = new Setting(ic);
        //        _setting.Show(this);
        //    }
        //    else if (_setting.Visible == false)
        //    {
        //        _setting.Show(this);
        //        _setting.BringToFront();
        //    }
        //    else
        //    {
        //        _setting.BringToFront();
        //        _setting.Location = new Point(_setting.Owner.Location.X + 10, _setting.Owner.Location.Y + 10);
        //        _setting.WindowState = FormWindowState.Normal;
        //    }
        //}

        private void timer_Tick(object sender, EventArgs e)
        {
            CommMonitor();
            lb_frame.Text = ic.DownFreqs.ToString();
            ic.FreqCalculate();

            tb_navi_pitch.Text = ic.Dec.navi_Pitch.ToString("F2");
            tb_navi_roll.Text = ic.Dec.navi_Roll.ToString("F2");
            tb_navi_yaw.Text = ic.Dec.navi_Yaw.ToString("F2");
            tb_espeed.Text = ic.Dec.navi_Espeed.ToString("F2");
            tb_nspeed.Text = ic.Dec.navi_Nspeed.ToString("F2");
            tb_navi_aspeed.Text = ic.Dec.navi_Aspeed.ToString("F2");
            tb_navi_lng.Text = ic.Dec.navi_Lng.ToString("F2");
            tb_navi_lat.Text = ic.Dec.navi_Lat.ToString("F2");
            tb_navi_alt.Text = ic.Dec.navi_Alt.ToString("F2");

            tb_gps_lng.Text = ic.Dec.gps_Lng.ToString("F2");
            tb_gps_lat.Text = ic.Dec.gps_Lat.ToString("F2");
            tb_gps_alt.Text = ic.Dec.gps_Alt.ToString("F2");
            tb_gps_espeed.Text = ic.Dec.gps_Espeed.ToString("F2");
            tb_gps_nspeed.Text = ic.Dec.gps_Nspeed.ToString("F2");
            tb_gps_aspeed.Text = ic.Dec.gps_Aspeed.ToString("F2");
            tb_gps_trackangle.Text = ic.Dec.gps_TrackAngle.ToString("F2");
            tb_gps_heading.Text = ic.Dec.gps_Heading.ToString("F2");
            tb_gps_headingprec.Text = ic.Dec.gps_Headingprec.ToString("F2");
            tb_gps_dspeed.Text = ic.Dec.gps_Dspeed.ToString("F2");
            tb_gps_count.Text = ic.Dec.gps_Count.ToString("F2");
            tb_gps_horiacc.Text = ic.Dec.gps_Count.ToString("F2");

            tb_prec_time.Text = ic.Dec.prec_Time.ToString("F2");
            tb_prec_pitch.Text = ic.Dec.prec_Pitch.ToString("F2");
            tb_prec_roll.Text = ic.Dec.prec_Roll.ToString("F2");
            tb_prec_yaw.Text = ic.Dec.prec_Yaw.ToString("F2");
            tb_prec_espeed.Text = ic.Dec.prec_E.ToString("F2");
            tb_prec_nspeed.Text = ic.Dec.prec_N.ToString("F2");
            tb_prec_aspeed.Text = ic.Dec.prec_A.ToString("F2");
            tb_prec_lng.Text = ic.Dec.prec_Lng.ToString("F2");
            tb_prec_lat.Text = ic.Dec.prec_Lat.ToString("F2");
            tb_prec_alt.Text = ic.Dec.prec_Alt.ToString("F2");
            tb_prec_ebx.Text = ic.Dec.prec_Ebx.ToString("F2");
            tb_prec_eby.Text = ic.Dec.prec_Eby.ToString("F2");
            tb_prec_ebz.Text = ic.Dec.prec_Ebz.ToString("F2");
            tb_prec_dbx.Text = ic.Dec.prec_Dbx.ToString("F2");
            tb_prec_dby.Text = ic.Dec.prec_Dby.ToString("F2");
            tb_prec_dbz.Text = ic.Dec.prec_Dbz.ToString("F2");
            tb_prec_x.Text = ic.Dec.prec_X.ToString("F2");
            tb_prec_y.Text = ic.Dec.prec_Y.ToString("F2");
            tb_prec_z.Text = ic.Dec.prec_Z.ToString("F2");

            tb_sensor_temp.Text = ic.Dec.sensor_Temp.ToString("F2");
            tb_sensor_xang.Text = ic.Dec.sensor_Xangv.ToString("F2");
            tb_sensor_yang.Text = ic.Dec.sensor_Yangv.ToString("F2");
            tb_sensor_zang.Text = ic.Dec.sensor_Zangv.ToString("F2");
            tb_sensor_xacc.Text = ic.Dec.sensor_Xacc.ToString("F2");
            tb_sensor_yacc.Text = ic.Dec.sensor_Yacc.ToString("F2");
            tb_sensor_zacc.Text = ic.Dec.sensor_Zacc.ToString("F2");
            tb_sensor_xmag.Text = ic.Dec.sensor_Xmag.ToString("F2");
            tb_sensor_ymag.Text = ic.Dec.sensor_Ymag.ToString("F2");
            tb_sensor_zmag.Text = ic.Dec.sensor_Zmag.ToString("F2");

            tb_sup_alt.Text = ic.Dec.sup_Alt.ToString("F2");
            tb_sup_mode.Text = ic.Dec.sup_Mode.ToString("F2");

            
        }

        private void btn_link_Click(object sender, EventArgs e)
        {
              panel_link.Visible = true;
            panel_set.Visible = true;
            panel_data.Visible = true;
            //panel_info.Visible = false;
            // panel_set.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel_link.Visible = false;
           
            panel_info.Visible = true;
            panel_set.Visible = true;
            panel_data.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pset = new Panel_set(ic);
            panel_set.Controls.Add(pset);
            pset.Dock = DockStyle.Fill;
            panel_link.Visible = false;
            panel_set.Visible = true;
            panel_info.Visible = false;
            panel_data.Visible = false;
        }
        private void btn_data_Click(object sender, EventArgs e)
        {
            dsend = new Datasend(ic);
            panel_data.Controls.Add(dsend);
            dsend.Dock = DockStyle.Fill;
            panel_link.Visible = false;
            panel_set.Visible = false;
            panel_info.Visible = false;
            panel_data.Visible = true;
        }
        Acc _acc = null;
      
        private void btn_navi_pry_Click(object sender, EventArgs e)
        {
            _acc = new Acc("姿态显示", "pitch", "roll", "yaw", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_navi_speed_Click(object sender, EventArgs e)
        {
            _acc = new Acc("东北天速度", "North", "East", "Down", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_navi_loc_Click(object sender, EventArgs e)
        {
            _acc = new Acc("高度", "Alt", "", "", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_prec_time_Click(object sender, EventArgs e)
        {
            _acc = new Acc("延时误差精度", "delay", "", "", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_prec_pry_Click(object sender, EventArgs e)
        {
            _acc = new Acc("三角度误差精度", "pitch", "roll", "yaw", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_prec_speed_Click(object sender, EventArgs e)
        {
            _acc = new Acc("三速度误差精度", "North", "East", "Down", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_prec_loc_Click(object sender, EventArgs e)
        {
            _acc = new Acc("三位置误差精度", "Lng", "Lat", "Alt", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_prec_eb_Click(object sender, EventArgs e)
        {
            _acc = new Acc("三轴eb精度", "ebx", "eby", "ebz", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_prec_db_Click(object sender, EventArgs e)
        {

            _acc = new Acc("三轴db精度", "dbx", "dby", "dbz", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_prec_xyz_Click(object sender, EventArgs e)
        {
            _acc = new Acc("三轴杆臂误差精度", "x", "y", "z", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            _acc = new Acc("温度显示", "temp", "", "", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_sensor_acc_Click(object sender, EventArgs e)
        {
            _acc = new Acc("三轴加速度计", "ax", "ay", "az", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_sensor_ang_Click(object sender, EventArgs e)
        {
            _acc = new Acc("三轴陀螺仪", "gx", "gy", "gz", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_sensor_mag_Click(object sender, EventArgs e)
        {
            _acc = new Acc("三轴磁数据", "mx", "my", "mz", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_height_Click(object sender, EventArgs e)
        {
            _acc = new Acc("气压高度", "Alt", "", "", ic.Dec);
            _acc.Show(this);
            if (_acc.Visible == false)
            {
                _acc.Show(this);
                _acc.BringToFront();
            }
            else
            {
                _acc.BringToFront();
                _acc.Location = new Point(_acc.Owner.Location.X + 10, _acc.Owner.Location.Y + 10);
                _acc.WindowState = FormWindowState.Normal;
            }
        }


    }
}