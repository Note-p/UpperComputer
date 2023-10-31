using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ins.Forms
{
    public partial class Plots : Form
    {

        Ins.Comm.InsComm ic = null;

        public Plots(Ins.Comm.InsComm _ic)
        {
            InitializeComponent();
            ic = _ic;
        }

        Acc _acc = null;
        public void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                this.treeView1.SelectedNode = null;
                if (e.Node.Nodes.Count == 0)//说明当前选中节点没有子节点

                    if (e.Node.Text == "三轴加速度计")
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
                    else if (e.Node.Text == "三轴陀螺仪")
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
                    else if (e.Node.Text == "三轴磁数据")
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
                    else if (e.Node.Text == "温度显示")
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
                    else if (e.Node.Text == "姿态显示")
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
                    else if (e.Node.Text == "三角度误差精度")
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
                    else if (e.Node.Text == "三速度误差精度")
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
                    else if (e.Node.Text == "三位置误差精度")
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
                    else if (e.Node.Text == "三轴eb精度")
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
                    else if (e.Node.Text == "三轴db精度")
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
                    else if (e.Node.Text == "三轴杆臂误差精度")
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
                    else if (e.Node.Text == "延时误差精度")
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
                    else if (e.Node.Text == "东北天速度")
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
                    else if (e.Node.Text == "高度")
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
                    else if (e.Node.Text == "气压高度")
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

        private void Plots_Load(object sender, EventArgs e)
        {
            //ic = new Comm.InsComm();
        }
    }
}
