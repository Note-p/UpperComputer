
namespace Ins.Forms
{
    partial class Plots
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("三轴加速度计");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("三轴陀螺仪");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("三轴磁数据");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("温度显示");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("传感器", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("姿态显示");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("姿态", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("三角度误差精度");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("三速度误差精度");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("三位置误差精度");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("三轴eb精度");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("三轴db精度");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("三轴杆臂误差精度");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("延时误差精度");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("精度", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("东北天速度");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("高度");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("导航信息", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("气压高度");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("辅助信息", new System.Windows.Forms.TreeNode[] {
            treeNode19});
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("绘制", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode7,
            treeNode15,
            treeNode18,
            treeNode20});
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(345, 611);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据曲线";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.Location = new System.Drawing.Point(5, 6);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点7";
            treeNode1.Text = "三轴加速度计";
            treeNode2.Name = "节点8";
            treeNode2.Text = "三轴陀螺仪";
            treeNode3.Name = "节点10";
            treeNode3.Text = "三轴磁数据";
            treeNode4.Name = "节点11";
            treeNode4.Text = "温度显示";
            treeNode5.Name = "节点1";
            treeNode5.Text = "传感器";
            treeNode6.Name = "节点12";
            treeNode6.Text = "姿态显示";
            treeNode7.Name = "节点2";
            treeNode7.Text = "姿态";
            treeNode8.Name = "节点4";
            treeNode8.Text = "三角度误差精度";
            treeNode9.Name = "节点13";
            treeNode9.Text = "三速度误差精度";
            treeNode10.Name = "节点14";
            treeNode10.Text = "三位置误差精度";
            treeNode11.Name = "节点15";
            treeNode11.Text = "三轴eb精度";
            treeNode12.Name = "节点16";
            treeNode12.Text = "三轴db精度";
            treeNode13.Name = "节点17";
            treeNode13.Text = "三轴杆臂误差精度";
            treeNode14.Name = "节点18";
            treeNode14.Text = "延时误差精度";
            treeNode15.Name = "节点3";
            treeNode15.Text = "精度";
            treeNode16.Name = "节点19";
            treeNode16.Text = "东北天速度";
            treeNode17.Name = "节点20";
            treeNode17.Text = "高度";
            treeNode18.Name = "节点5";
            treeNode18.Text = "导航信息";
            treeNode19.Name = "节点21";
            treeNode19.Text = "气压高度";
            treeNode20.Name = "节点6";
            treeNode20.Text = "辅助信息";
            treeNode21.Name = "节点0";
            treeNode21.Text = "绘制";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode21});
            this.treeView1.Size = new System.Drawing.Size(333, 586);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(353, 646);
            this.tabControl1.TabIndex = 0;
            // 
            // Plots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(374, 670);
            this.Controls.Add(this.tabControl1);
            this.Name = "Plots";
            this.Text = "数据曲线";
            this.Load += new System.EventHandler(this.Plots_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TreeView treeView1;
    }
}