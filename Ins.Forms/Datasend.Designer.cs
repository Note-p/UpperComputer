
namespace Ins.Forms
{
    partial class Datasend
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_length = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_check = new System.Windows.Forms.ComboBox();
            this.btn_check = new System.Windows.Forms.Button();
            this.btn_send = new System.Windows.Forms.Button();
            this.label241 = new System.Windows.Forms.Label();
            this.lb_send = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.lb_read = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_total = new System.Windows.Forms.Label();
            this.lb_count = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lb_num = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_pause = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "帧大小";
            // 
            // tb_length
            // 
            this.tb_length.Location = new System.Drawing.Point(140, 78);
            this.tb_length.Name = "tb_length";
            this.tb_length.Size = new System.Drawing.Size(100, 31);
            this.tb_length.TabIndex = 1;
            this.tb_length.TextChanged += new System.EventHandler(this.tb_length_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "校验方法";
            // 
            // cb_check
            // 
            this.cb_check.FormattingEnabled = true;
            this.cb_check.Items.AddRange(new object[] {
            "校验和",
            "16位CRC"});
            this.cb_check.Location = new System.Drawing.Point(474, 81);
            this.cb_check.Name = "cb_check";
            this.cb_check.Size = new System.Drawing.Size(121, 29);
            this.cb_check.TabIndex = 3;
            this.cb_check.SelectedIndexChanged += new System.EventHandler(this.cb_check_SelectedIndexChanged);
            // 
            // btn_check
            // 
            this.btn_check.Location = new System.Drawing.Point(49, 182);
            this.btn_check.Name = "btn_check";
            this.btn_check.Size = new System.Drawing.Size(120, 41);
            this.btn_check.TabIndex = 4;
            this.btn_check.Text = "文件选择";
            this.btn_check.UseVisualStyleBackColor = true;
            this.btn_check.Click += new System.EventHandler(this.btn_check_Click);
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(327, 182);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(120, 41);
            this.btn_send.TabIndex = 5;
            this.btn_send.Text = "数据发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // label241
            // 
            this.label241.AutoSize = true;
            this.label241.Location = new System.Drawing.Point(45, 355);
            this.label241.Name = "label241";
            this.label241.Size = new System.Drawing.Size(94, 21);
            this.label241.TabIndex = 6;
            this.label241.Text = "发送进度";
            // 
            // lb_send
            // 
            this.lb_send.AutoSize = true;
            this.lb_send.Location = new System.Drawing.Point(153, 355);
            this.lb_send.Name = "lb_send";
            this.lb_send.Size = new System.Drawing.Size(32, 21);
            this.lb_send.TabIndex = 7;
            this.lb_send.Text = "0%";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 21);
            this.label3.TabIndex = 10;
            this.label3.Text = "读取状态:";
            // 
            // lb_read
            // 
            this.lb_read.AutoSize = true;
            this.lb_read.Location = new System.Drawing.Point(158, 286);
            this.lb_read.Name = "lb_read";
            this.lb_read.Size = new System.Drawing.Size(0, 21);
            this.lb_read.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(323, 286);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "数据总量:";
            // 
            // lb_total
            // 
            this.lb_total.AutoSize = true;
            this.lb_total.Location = new System.Drawing.Point(436, 286);
            this.lb_total.Name = "lb_total";
            this.lb_total.Size = new System.Drawing.Size(21, 21);
            this.lb_total.TabIndex = 13;
            this.lb_total.Text = "0";
            // 
            // lb_count
            // 
            this.lb_count.AutoSize = true;
            this.lb_count.Location = new System.Drawing.Point(436, 355);
            this.lb_count.Name = "lb_count";
            this.lb_count.Size = new System.Drawing.Size(21, 21);
            this.lb_count.TabIndex = 15;
            this.lb_count.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(323, 355);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 21);
            this.label6.TabIndex = 14;
            this.label6.Text = "有效数据:";
            // 
            // lb_num
            // 
            this.lb_num.AutoSize = true;
            this.lb_num.Location = new System.Drawing.Point(436, 426);
            this.lb_num.Name = "lb_num";
            this.lb_num.Size = new System.Drawing.Size(21, 21);
            this.lb_num.TabIndex = 17;
            this.lb_num.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(344, 426);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 21);
            this.label7.TabIndex = 16;
            this.label7.Text = "已发送:";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(49, 416);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(120, 41);
            this.btn_cancel.TabIndex = 18;
            this.btn_cancel.Text = "取消发送";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_pause
            // 
            this.btn_pause.Location = new System.Drawing.Point(453, 182);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(120, 41);
            this.btn_pause.TabIndex = 19;
            this.btn_pause.Text = "暂停发送";
            this.btn_pause.UseVisualStyleBackColor = true;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // Datasend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_pause);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.lb_num);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lb_count);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lb_total);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb_read);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_send);
            this.Controls.Add(this.label241);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.btn_check);
            this.Controls.Add(this.cb_check);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_length);
            this.Controls.Add(this.label1);
            this.Name = "Datasend";
            this.Size = new System.Drawing.Size(800, 700);
            this.Load += new System.EventHandler(this.Datasend_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_length;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_check;
        private System.Windows.Forms.Button btn_check;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Label label241;
        private System.Windows.Forms.Label lb_send;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_read;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_total;
        private System.Windows.Forms.Label lb_count;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lb_num;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_pause;
    }
}
