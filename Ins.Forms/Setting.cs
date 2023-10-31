﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ins.Comm;
using Timer;
using static Ins.Forms.martix;

namespace Ins.Forms
{
    public partial class Setting : Form
    {
        private Config _config = new Config(Directory.GetCurrentDirectory() + "\\config\\", "set.ini");
        internal InsComm _ic;
        Calibration1 _cali = new Calibration1();
        public float _gps_x, _gps_y, _gps_z, _imu_x, _imu_y, _imu_z, _lng, _lat, _alt,
                     _acc1, _acc2, _acc3, _acc4, _acc5, _acc6, _acc7, _acc8, _acc9, _accx, _accy, _accz,
                     _mag1, _mag2, _mag3, _mag4, _mag5, _mag6, _mag7, _mag8, _mag9, _magx, _magy, _magz,
                     _ax, _ay, _az, _bx, _by, _bz, _cx, _cy, _cz;

        static MilliTimer _timer = new MilliTimer();
        static MilliTimer _timer_mag = new MilliTimer();
        List<double> acc_x = new List<double>();
        List<double> acc_y = new List<double>();
        List<double> acc_z = new List<double>();
        List<double> mag_x = new List<double>();
        List<double> mag_y = new List<double>();
        List<double> mag_z = new List<double>();
        //List<double> acc_center = new List<double>();
        double[] acc_center = new double[3];
        Matrix acc_matrix = new Matrix();
        double[] mag_center = new double[3];
        Matrix mag_matrix = new Matrix();
        public static int acc_num = 0, mag_num = 0;

        private void timer_acc_Tick(object sender, EventArgs e)
        {
            num_acc.Text = acc_x.Count().ToString();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            acc_x.Add(_ic.Dec.sensor_Xacc);
            acc_y.Add(_ic.Dec.sensor_Yacc);
            acc_z.Add(_ic.Dec.sensor_Zacc);
            _cali.cursor = (uint)acc_x.Count();
            
        }

        private void btn_collect_Click(object sender, EventArgs e)
        {
            timer_acc.Enabled = true;
            _timer.OpenTimer(_timer, 5, timer_Tick);
            acc_status.Text = ("开始收集");
            btn_collect.Enabled = false;
            btn_stop.Enabled = true;
            btn_reset.Enabled = true;
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            _timer.StopTimer(_timer);
            acc_status.Text = ("停止收集");
            btn_collect.Enabled = true;
            btn_stop.Enabled = false;
            btn_reset.Enabled = true;
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            _timer.StopTimer(_timer);
            acc_x.Clear();
            acc_y.Clear();
            acc_z.Clear();
            num_acc.Text = acc_x.Count().ToString();
            acc_status.Text = ("复位");
            btn_stop.Enabled = false;
            btn_reset.Enabled = false;
            btn_collect.Enabled = true;
        }
        private void btn_calc_Click(object sender, EventArgs e)
        {
            _cali.Calibration(acc_x, acc_y, acc_z, acc_center, acc_matrix);
            tb_f1.Text = acc_matrix.data[0].ToString();
            tb_f2.Text = acc_matrix.data[1].ToString();
            tb_f3.Text = acc_matrix.data[2].ToString();
            tb_f4.Text = acc_matrix.data[3].ToString();
            tb_f5.Text = acc_matrix.data[4].ToString();
            tb_f6.Text = acc_matrix.data[5].ToString();
            tb_f7.Text = acc_matrix.data[6].ToString();
            tb_f8.Text = acc_matrix.data[7].ToString();
            tb_f9.Text = acc_matrix.data[8].ToString();

            tb_x.Text = acc_center[0].ToString();
            tb_y.Text = acc_center[1].ToString();
            tb_z.Text = acc_center[2].ToString();
        }

        private void timer_mag_Tick(object sender, EventArgs e)
        {
            num_mag.Text = mag_x.Count().ToString();
        }
        private void _timer_mag_Tick(object sender, EventArgs e)
        {
            mag_x.Add(_ic.Dec.sensor_Xmag);
            mag_y.Add(_ic.Dec.sensor_Ymag);
            mag_z.Add(_ic.Dec.sensor_Zmag);
            _cali.cursor = (uint)mag_x.Count();

        }
        private void btn_collect1_Click(object sender, EventArgs e)
        {
            timer_mag.Enabled = true;
            _timer_mag.OpenTimer(_timer_mag, 5, _timer_mag_Tick);
            mag_status.Text = ("开始收集");
            btn_collect1.Enabled = false;
            btn_stop1.Enabled = true;
            btn_reset1.Enabled = true;
        }
        private void btn_stop1_Click(object sender, EventArgs e)
        {
            _timer_mag.StopTimer(_timer_mag);
            mag_status.Text = ("停止收集");
            btn_collect1.Enabled = true;
            btn_stop1.Enabled = false;
            btn_reset1.Enabled = true;
        }
        private void btn_reset1_Click(object sender, EventArgs e)
        {
            _timer_mag.StopTimer(_timer_mag);
            mag_x.Clear();
            mag_y.Clear();
            mag_z.Clear();
            num_mag.Text = mag_x.Count().ToString();
            mag_status.Text = ("复位");
            btn_stop1.Enabled = false;
            btn_reset1.Enabled = false;
            btn_collect1.Enabled = true;
        }
        private void btn_calc1_Click(object sender, EventArgs e)
        {
            _cali.Calibration(mag_x, mag_y, mag_z, mag_center, mag_matrix);
            tb_m1.Text = mag_matrix.data[0].ToString();
            tb_m2.Text = mag_matrix.data[1].ToString();
            tb_m3.Text = mag_matrix.data[2].ToString();
            tb_m4.Text = mag_matrix.data[3].ToString();
            tb_m5.Text = mag_matrix.data[4].ToString();
            tb_m6.Text = mag_matrix.data[5].ToString();
            tb_m7.Text = mag_matrix.data[6].ToString();
            tb_m8.Text = mag_matrix.data[7].ToString();
            tb_m9.Text = mag_matrix.data[8].ToString();

            tb_mx.Text = mag_center[0].ToString();
            tb_my.Text = mag_center[1].ToString();
            tb_mz.Text = mag_center[2].ToString();
        }
        public byte _sensor;
        public Setting(InsComm ic)
        {

            InitializeComponent();
            _ic = ic;
        }
        private void Setting_Load(object sender, EventArgs e)
        {
            if (float.TryParse(_config.ConfigRd("Arm", "armx"), out _gps_x)) gps_x.Value = (decimal)_gps_x;
            else gps_x.Value = 0;
            if (float.TryParse(_config.ConfigRd("Arm", "army"), out _gps_y)) gps_y.Value = (decimal)_gps_y;
            else gps_y.Value = 0;
            if (float.TryParse(_config.ConfigRd("Arm", "armz"), out _gps_z)) gps_z.Value = (decimal)_gps_z;
            else gps_z.Value = 0;
            if (float.TryParse(_config.ConfigRd("Arm1", "arm1x"), out _imu_x)) IMU_x.Value = (decimal)_imu_x;
            else IMU_x.Value = 0;
            if (float.TryParse(_config.ConfigRd("Arm1", "arm1y"), out _imu_y)) IMU_y.Value = (decimal)_imu_y;
            else IMU_y.Value = 0;
            if (float.TryParse(_config.ConfigRd("Arm1", "arm1z"), out _imu_z)) IMU_z.Value = (decimal)_imu_z;
            else IMU_z.Value = 0;
            if (Byte.TryParse(_config.ConfigRd("Sensor", "select"), out _sensor)) cb_sensor.SelectedIndex = _sensor;
            else cb_sensor.SelectedIndex = 0;
            if (float.TryParse(_config.ConfigRd("Initcoor", "Lng"), out _lng)) tb_lng.Text = _lng.ToString();
            else tb_lng.Text = "0";
            if (float.TryParse(_config.ConfigRd("Initcoor", "Lat"), out _lat)) tb_lat.Text = _lat.ToString();
            else tb_lat.Text = "0";
            if (float.TryParse(_config.ConfigRd("Initcoor", "Alt"), out _alt)) tb_alt.Text = _alt.ToString();
            else tb_alt.Text  = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Acc1"), out _acc1)) tb_f1.Text = _acc1.ToString();
            else tb_f1.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Acc2"), out _acc2)) tb_f2.Text = _acc2.ToString();
            else tb_f2.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Acc3"), out _acc3)) tb_f3.Text = _acc3.ToString();
            else tb_f3.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Acc4"), out _acc4)) tb_f4.Text = _acc4.ToString();
            else tb_f4.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Acc5"), out _acc5)) tb_f5.Text = _acc5.ToString();
            else tb_f5.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Acc6"), out _acc6)) tb_f6.Text = _acc6.ToString();
            else tb_f6.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Acc7"), out _acc7)) tb_f7.Text = _acc7.ToString();
            else tb_f7.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Acc8"), out _acc8)) tb_f8.Text = _acc8.ToString();
            else tb_f8.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Acc9"), out _acc9)) tb_f9.Text = _acc9.ToString();
            else tb_f9.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Accx"), out _accx)) tb_x.Text = _accx.ToString();
            else tb_x.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Accy"), out _accy)) tb_y.Text = _accy.ToString();
            else tb_y.Text = "0";
            if (float.TryParse(_config.ConfigRd("Acc", "Accz"), out _accz)) tb_z.Text = _accz.ToString();
            else tb_z.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Mag1"), out _mag1)) tb_m1.Text = _mag1.ToString();
            else tb_m1.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Mag2"), out _mag2)) tb_m2.Text = _mag2.ToString();
            else tb_m2.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Mag3"), out _mag3)) tb_m3.Text = _mag3.ToString();
            else tb_m3.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Mag4"), out _mag4)) tb_m4.Text = _mag4.ToString();
            else tb_m4.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Mag5"), out _mag5)) tb_m5.Text = _mag5.ToString();
            else tb_m5.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Mag6"), out _mag6)) tb_m6.Text = _mag6.ToString();
            else tb_m6.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Mag7"), out _mag7)) tb_m7.Text = _mag7.ToString();
            else tb_m7.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Mag8"), out _mag8)) tb_m8.Text = _mag8.ToString();
            else tb_m8.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Mag9"), out _mag9)) tb_m9.Text = _mag9.ToString();
            else tb_m9.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Magx"), out _magx)) tb_mx.Text = _magx.ToString();
            else tb_mx.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Magy"), out _magy)) tb_my.Text = _magy.ToString();
            else tb_my.Text = "0";
            if (float.TryParse(_config.ConfigRd("Mag", "Magz"), out _magz)) tb_mz.Text = _magz.ToString();
            else tb_mz.Text = "0";
            if (float.TryParse(_config.ConfigRd("Temp", "ax"), out _ax)) tb_ax.Text = _ax.ToString();
            else tb_ax.Text = "0";
            if (float.TryParse(_config.ConfigRd("Temp", "ay"), out _ay)) tb_ay.Text = _ay.ToString();
            else tb_ay.Text = "0";
            if (float.TryParse(_config.ConfigRd("Temp", "az"), out _az)) tb_az.Text = _az.ToString();
            else tb_az.Text = "0";
            if (float.TryParse(_config.ConfigRd("Temp", "bx"), out _bx)) tb_bx.Text = _bx.ToString();
            else tb_bx.Text = "0";
            if (float.TryParse(_config.ConfigRd("Temp", "by"), out _by)) tb_by.Text = _by.ToString();
            else tb_by.Text = "0";
            if (float.TryParse(_config.ConfigRd("Temp", "bz"), out _bz)) tb_bz.Text = _bz.ToString();
            else tb_bz.Text = "0";
            if (float.TryParse(_config.ConfigRd("Temp", "cx"), out _cx)) tb_cx.Text = _cx.ToString();
            else tb_cx.Text = "0";
            if (float.TryParse(_config.ConfigRd("Temp", "cy"), out _cy)) tb_cy.Text = _cy.ToString();
            else tb_cy.Text = "0";
            if (float.TryParse(_config.ConfigRd("Temp", "cz"), out _cz)) tb_cz.Text = _cz.ToString();
            else tb_cz.Text = "0";
        }
        private void btn_arm_Click(object sender, EventArgs e)
        {
            _ic.Enc.Cmd_IMU((float)gps_x.Value, (float)gps_y.Value, (float)gps_z.Value);
            _config.ConfigWr("Arm", "armx", gps_x.Value.ToString());
            _config.ConfigWr("Arm", "army", gps_y.Value.ToString());
            _config.ConfigWr("Arm", "armz", gps_z.Value.ToString());
        }

        private void btn_arm1_Click(object sender, EventArgs e)
        {
            _ic.Enc.Cmd_IMU1((float)IMU_x.Value, (float)IMU_y.Value, (float)IMU_z.Value);
            _config.ConfigWr("Arm1", "arm1x", IMU_x.Value.ToString());
            _config.ConfigWr("Arm1", "arm1y", IMU_y.Value.ToString());
            _config.ConfigWr("Arm1", "arm1z", IMU_z.Value.ToString());
        }

        private void cb_sensor_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ic.Enc.Cmd_Sensor((byte)cb_sensor.SelectedIndex);
            _config.ConfigWr("Sensor", "select", cb_sensor.SelectedIndex.ToString());
        }

        private void btn_sensor_Click(object sender, EventArgs e)
        {
            _ic.Enc.Cmd_InitCoor(float.Parse(tb_lng.Text), float.Parse(tb_lat.Text), float.Parse(tb_alt.Text));
            _config.ConfigWr("Initcoor", "Lng", tb_lng.Text);
            _config.ConfigWr("Initcoor", "Lat", tb_lat.Text);
            _config.ConfigWr("Initcoor", "Alt", tb_alt.Text);
        }

        private void btn_acc_Click(object sender, EventArgs e)
        {
            _ic.Enc.Cmd_Acc(float.Parse(tb_f1.Text), float.Parse(tb_f2.Text), float.Parse(tb_f3.Text), float.Parse(tb_f4.Text), float.Parse(tb_f5.Text), float.Parse(tb_f6.Text), float.Parse(tb_f7.Text), float.Parse(tb_f8.Text), float.Parse(tb_f9.Text), float.Parse(tb_x.Text), float.Parse(tb_y.Text), float.Parse(tb_z.Text));
            _config.ConfigWr("Acc", "Acc1", tb_f1.Text);
            _config.ConfigWr("Acc", "Acc2", tb_f2.Text);
            _config.ConfigWr("Acc", "Acc3", tb_f3.Text);
            _config.ConfigWr("Acc", "Acc4", tb_f4.Text);
            _config.ConfigWr("Acc", "Acc5", tb_f5.Text);
            _config.ConfigWr("Acc", "Acc6", tb_f6.Text);
            _config.ConfigWr("Acc", "Acc7", tb_f7.Text);
            _config.ConfigWr("Acc", "Acc8", tb_f8.Text);
            _config.ConfigWr("Acc", "Acc9", tb_f9.Text);
            _config.ConfigWr("Acc", "Accx", tb_x.Text);
            _config.ConfigWr("Acc", "Accy", tb_y.Text);
            _config.ConfigWr("Acc", "Accz", tb_z.Text);
        }

       

        private void btn_mag_Click(object sender, EventArgs e)
        {
            _ic.Enc.Cmd_Mag(float.Parse(tb_m1.Text), float.Parse(tb_m2.Text), float.Parse(tb_m3.Text), float.Parse(tb_m4.Text), float.Parse(tb_m5.Text), float.Parse(tb_m6.Text), float.Parse(tb_m7.Text), float.Parse(tb_m8.Text), float.Parse(tb_m9.Text), float.Parse(tb_mx.Text), float.Parse(tb_my.Text), float.Parse(tb_mz.Text));
            _config.ConfigWr("Mag", "Mag1", tb_m1.Text);
            _config.ConfigWr("Mag", "Mag2", tb_m2.Text);
            _config.ConfigWr("Mag", "Mag3", tb_m3.Text);
            _config.ConfigWr("Mag", "Mag4", tb_m4.Text);
            _config.ConfigWr("Mag", "Mag5", tb_m5.Text);
            _config.ConfigWr("Mag", "Mag6", tb_m6.Text);
            _config.ConfigWr("Mag", "Mag7", tb_m7.Text);
            _config.ConfigWr("Mag", "Mag8", tb_m8.Text);
            _config.ConfigWr("Mag", "Mag9", tb_m9.Text);
            _config.ConfigWr("Mag", "Magx", tb_mx.Text);
            _config.ConfigWr("Mag", "Magy", tb_my.Text);
            _config.ConfigWr("Mag", "Magz", tb_mz.Text);
        }

        private void btn_temp_Click(object sender, EventArgs e)
        {
           // _ic.Enc.Cmd_Gyro(float.Parse(tb_ax.Text), float.Parse(tb_ay.Text), float.Parse(tb_az.Text), float.Parse(tb_bx.Text), float.Parse(tb_by.Text), float.Parse(tb_bz.Text), float.Parse(tb_cx.Text), float.Parse(tb_cy.Text), float.Parse(tb_cz.Text));
            _config.ConfigWr("Temp", "ax", tb_ax.Text);
            _config.ConfigWr("Temp", "ay", tb_ay.Text);
            _config.ConfigWr("Temp", "az", tb_az.Text);
            _config.ConfigWr("Temp", "bx", tb_bx.Text);
            _config.ConfigWr("Temp", "by", tb_by.Text);
            _config.ConfigWr("Temp", "bz", tb_bz.Text);
            _config.ConfigWr("Temp", "cx", tb_cx.Text);
            _config.ConfigWr("Temp", "cy", tb_cy.Text);
            _config.ConfigWr("Temp", "cz", tb_cz.Text);
        }

       
    }
}
