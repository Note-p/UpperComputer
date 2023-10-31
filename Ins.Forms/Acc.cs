using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp;
using Ins.Comm;

namespace Ins.Forms
{
    public partial class Acc : Form
    {
        
        Form1 _fm1 = null;
        string _title;
        string _a;
        string _b;
        string _c;
        float _x;
        float _y;
        float _z;
        FrameDecode _dec;
        /// <summary>
        ///  横坐标最初值
        /// </summary>
        private DateTime X_minValue;
        public Acc(string title,string a,string b,string c, FrameDecode dec)
        {
            InitializeComponent();

            

            _title = title;
            _a = a;
            _b = b;
            _c = c;

            _dec = dec;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // 添加数据
                switch (_title)
                {
                    case "三轴加速度计":
                        {

                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.sensor_Xacc);
                           
                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.sensor_Yacc);
                            
                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.sensor_Zacc);

                            this.chart1.ChartAreas[0].AxisY.Maximum = 40;
                            chart1.ChartAreas[0].AxisY.Minimum = -40;
                        }
                        break;
                
                    case "三轴陀螺仪":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.sensor_Xangv);

                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.sensor_Yangv);

                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.sensor_Zangv);

                            this.chart1.ChartAreas[0].AxisY.Maximum = 400;
                            chart1.ChartAreas[0].AxisY.Minimum = -400;
                        }
                        break;

                    case "三轴磁数据":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.sensor_Xmag);

                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.sensor_Ymag);

                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.sensor_Zmag);

                            this.chart1.ChartAreas[0].AxisY.Maximum = 2;
                            chart1.ChartAreas[0].AxisY.Minimum = -2;
                        }
                        break;
                    case "温度显示":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.sensor_Temp);

                            this.chart1.ChartAreas[0].AxisY.Maximum = 80;
                            chart1.ChartAreas[0].AxisY.Minimum = -40;
                        }
                        break;
                    case "姿态显示":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.navi_Pitch);

                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.navi_Roll);

                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.navi_Yaw);

                            this.chart1.ChartAreas[0].AxisY.Maximum = 360;
                            chart1.ChartAreas[0].AxisY.Minimum = -180;
                        }
                        break;
                    case "三角度误差精度":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Pitch);

                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Roll);

                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Yaw);
                        }
                        break;
                    case "三速度误差精度":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_E);

                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_N);

                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_A);
                        }
                        break;
                    case "三位置误差精度":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Lng);

                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Lat);

                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Alt);
                        }
                        break;
                    case "三轴eb精度":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Ebx);

                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Eby);

                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Ebz);
                        }
                        break;
                    case "三轴db精度":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Dbx);

                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Dby);

                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Dbz);
                        }
                        break;
                    case "三轴杆臂误差精度":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_X);

                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Y);

                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Z);
                        }
                        break;
                    case "延时误差精度":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.prec_Time);
                        }
                        break;
                    case "东北天速度":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.navi_Espeed);

                            this.chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), _dec.navi_Nspeed);

                            this.chart1.Series[2].Points.AddXY(DateTime.Now.ToOADate(), _dec.navi_Aspeed);

                            this.chart1.ChartAreas[0].AxisY.Maximum = 50;
                            chart1.ChartAreas[0].AxisY.Minimum = -50;
                        }
                        break;
                    case "高度":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.navi_Alt);
                        }
                        break;
                    case "气压高度":
                        {
                            this.chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), _dec.sup_Alt);
                        }
                        break;


                }
                if (this.chart1.Series[0].Points.Count > 200)
                {
                    this.chart1.Series[0].Points.RemoveAt(0);
                }
                if (_b != "")
                {
                    if (this.chart1.Series[1].Points.Count > 200)
                    {
                        this.chart1.Series[1].Points.RemoveAt(0);
                    }
                }
                if (_c != "")
                {
                    if (this.chart1.Series[2].Points.Count > 200)
                    {
                        this.chart1.Series[2].Points.RemoveAt(0);
                    }
                }
                // X坐标后移1秒
                this.chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddSeconds(1).ToOADate();
               

                
                chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.AddSeconds(-3).ToOADate();
             
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void Acc_Load(object sender, EventArgs e)
        {
            // 初始化模式数据的设置
            List<Color> c = new List<Color>() { Color.Red, Color.Blue, Color.Lime, Color.Green };
            List<ChartData> data = new List<ChartData>();

            data.Add(new ChartData() { Name = _a, color = c[0] });
            if (_b != "")
            {
                data.Add(new ChartData() { Name = _b, color = c[1] });
            }
            if (_c != "")
            {
                data.Add(new ChartData() { Name = _c, color = c[2] });
            }

            // x轴最小刻度 
            X_minValue = DateTime.Now;
            // 初始化chart1
            new MyChart().ChartInit(chart1,_title, data);

            timer1.Enabled = true;
        }

       
    }
}
