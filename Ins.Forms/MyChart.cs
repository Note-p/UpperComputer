using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp
{
    public class MyChart
    {
        public void ChartInit(Chart chart, string TabName, List<ChartData> value)
        {
            #region 定义图表区域、设置图表显示样式
            chart.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea("C1");

            // 允许X轴放大
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.Interval = 0;
            chartArea.CursorX.IntervalOffset = 0;
            chartArea.CursorX.IntervalType = DateTimeIntervalType.Minutes;

            // 允许X轴放大
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.AutoScroll = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.Interval = 1D;
            chartArea.CursorX.IntervalOffset = 0;
            chartArea.CursorX.IntervalOffsetType = DateTimeIntervalType.Seconds;
            chartArea.CursorX.IntervalType = DateTimeIntervalType.Seconds;
            chartArea.CursorX.LineColor = Color.Blue;

            // 允许Y轴放大
            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.AutoScroll = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.CursorY.Interval = 1D;
            chartArea.CursorY.IntervalOffset = 0;
            chartArea.CursorY.IntervalOffsetType = DateTimeIntervalType.Seconds;
            chartArea.CursorY.IntervalType = DateTimeIntervalType.Seconds;
            chartArea.CursorY.LineColor = Color.Blue;

            // chartArea.AxisX.Interval = 1; // 设置轴的间隔(这个不能开)
            //chartArea.AxisX.IsInterlaced = true; // 交错网格
            chartArea.AxisX.IsStartedFromZero = false;
            chartArea.AxisX.MajorGrid.LineColor = Color.Silver;
            chartArea.AxisX.MajorGrid.Enabled = true;
            chartArea.AxisX.ScrollBar.Enabled = true;
            chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chartArea.AxisX.IntervalType = DateTimeIntervalType.NotSet;
            chartArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//启用X轴滚动条按钮
            chartArea.AxisX.ScaleView.Scroll(ScrollType.Last);
            chartArea.AxisX.LabelStyle.Format = "HH:mm:ss"; // 设置时间作为X轴
            chartArea.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Seconds;
            chartArea.AxisX.LabelStyle.Interval = 1;                //坐标值间隔1S
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = false;   //防止X轴坐标跳跃
            chartArea.AxisX.LabelStyle.Angle = -90;
            chartArea.AxisX.LabelStyle.Font = new Font("微软雅黑", 10f);
            chartArea.AxisX.MajorGrid.IntervalType = DateTimeIntervalType.Seconds;
            chartArea.AxisX.MajorGrid.Interval = 1;                 //网格间隔
            chartArea.AxisX.Minimum = DateTime.Now.ToOADate();      //当前时间
            chartArea.AxisX.Maximum = DateTime.Now.ToOADate();
            chartArea.AxisX.ScaleView.Zoomable = true; // 允许X轴放大
            chartArea.AxisX.ScrollBar.IsPositionedInside = false;

            //设置图表显示样式
            //chartArea.AxisY.Minimum = DateTime.Now.ToOADate(); // Y轴最小值
            //chartArea.AxisY.Maximum = DateTime.Now.ToOADate(); // Y轴最大值
            //chartArea.AxisY.IsStartedFromZero = false;
            //chartArea.AxisY.MajorGrid.Enabled = true;

            // 背景样式
            chartArea.BackColor = Color.White;                       //背景色
            chartArea.BackSecondaryColor = Color.White;              //渐变背景色
            chartArea.BackGradientStyle = GradientStyle.TopBottom;   //渐变方式
            chartArea.BackHatchStyle = ChartHatchStyle.None;         //背景阴影
            chartArea.BorderDashStyle = ChartDashStyle.NotSet;       //边框线样式
            chartArea.BorderWidth = 1;                               //边框宽度
            chartArea.BorderColor = Color.Black;

            chart.ChartAreas.Add(chartArea);
            #endregion

            #region Series 数据初始化
            chart.Series.Clear();
            foreach (ChartData c in value)
            {
                Series series = new Series(c.Name);
                series.ChartArea = "C1";
                series.Color = c.color;
                series.Points.Clear();
                series.XValueType = ChartValueType.String;
                series.YValueType = ChartValueType.Double;
                series.BorderWidth = 1;
                series.MarkerColor = Color.Green;
                series.MarkerSize = 7;
                series.MarkerStyle = MarkerStyle.None;  // MarkerStyle.Circle
                series.ChartType = SeriesChartType.Spline; // Line折线图 Spline 曲线图
                series.IsValueShownAsLabel = false; // 是否在标签上显示数值
                series.ToolTip = "时间：#VALX\n当前值：#VALY\n最大值：#MAX\n最小值：#MIN\n平均值：#AVG";
                chart.Series.Add(series);
            }
            #endregion

            #region 设置标题等样式
            chart.Titles.Clear();
            chart.Titles.Add("n1");
            chart.Titles[0].Text = TabName;
            chart.Titles[0].ForeColor = Color.RoyalBlue;
            chart.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);

            // 设置边框
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BorderlineWidth = 1;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.None;
            #endregion
        }
    }

    public class ChartData
    {
        /// <summary>
        /// 曲线名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 曲线颜色
        /// </summary>
        public Color color { get; set; }
    }
}
