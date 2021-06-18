namespace ShopeeChat
{
    partial class MessageDialog
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.msgCountNUD = new System.Windows.Forms.NumericUpDown();
            this.mutilChatCkBox = new System.Windows.Forms.CheckBox();
            this.customerListCBox = new System.Windows.Forms.ComboBox();
            this.msgSpanNUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.le1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.transBtn = new System.Windows.Forms.Button();
            this.sendBtn = new System.Windows.Forms.Button();
            this.msgRTBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stickerTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.picTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.itemTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.langTsCBox = new System.Windows.Forms.ToolStripComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logRText = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msgCountNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.msgSpanNUD)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.msgCountNUD);
            this.splitContainer1.Panel1.Controls.Add(this.mutilChatCkBox);
            this.splitContainer1.Panel1.Controls.Add(this.customerListCBox);
            this.splitContainer1.Panel1.Controls.Add(this.msgSpanNUD);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.le1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.msgRTBox);
            this.splitContainer1.Panel2.Controls.Add(this.menuStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(751, 371);
            this.splitContainer1.SplitterDistance = 52;
            this.splitContainer1.TabIndex = 0;
            // 
            // msgCountNUD
            // 
            this.msgCountNUD.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.msgCountNUD.Location = new System.Drawing.Point(474, 17);
            this.msgCountNUD.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.msgCountNUD.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.msgCountNUD.Name = "msgCountNUD";
            this.msgCountNUD.Size = new System.Drawing.Size(64, 25);
            this.msgCountNUD.TabIndex = 11;
            this.msgCountNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.msgCountNUD.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // mutilChatCkBox
            // 
            this.mutilChatCkBox.AutoSize = true;
            this.mutilChatCkBox.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mutilChatCkBox.Location = new System.Drawing.Point(307, 17);
            this.mutilChatCkBox.Name = "mutilChatCkBox";
            this.mutilChatCkBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mutilChatCkBox.Size = new System.Drawing.Size(171, 23);
            this.mutilChatCkBox.TabIndex = 9;
            this.mutilChatCkBox.Text = "聊聊列表群发，给列表前";
            this.mutilChatCkBox.UseVisualStyleBackColor = true;
            this.mutilChatCkBox.CheckedChanged += new System.EventHandler(this.mutilChatCkBox_CheckedChanged);
            // 
            // customerListCBox
            // 
            this.customerListCBox.FormattingEnabled = true;
            this.customerListCBox.Location = new System.Drawing.Point(146, 17);
            this.customerListCBox.Name = "customerListCBox";
            this.customerListCBox.Size = new System.Drawing.Size(117, 20);
            this.customerListCBox.TabIndex = 8;
            this.customerListCBox.SelectedIndexChanged += new System.EventHandler(this.customerListCBox_SelectedIndexChanged);
            // 
            // msgSpanNUD
            // 
            this.msgSpanNUD.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.msgSpanNUD.Location = new System.Drawing.Point(686, 18);
            this.msgSpanNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.msgSpanNUD.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.msgSpanNUD.Name = "msgSpanNUD";
            this.msgSpanNUD.Size = new System.Drawing.Size(52, 25);
            this.msgSpanNUD.TabIndex = 7;
            this.msgSpanNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.msgSpanNUD.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.msgSpanNUD.ValueChanged += new System.EventHandler(this.msgSpanNUD_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(544, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "个买家, 每个消息间隔";
            // 
            // le1
            // 
            this.le1.AutoSize = true;
            this.le1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.le1.Location = new System.Drawing.Point(3, 17);
            this.le1.Name = "le1";
            this.le1.Size = new System.Drawing.Size(138, 20);
            this.le1.TabIndex = 5;
            this.le1.Text = "发送给聊聊列表用户:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.transBtn);
            this.panel1.Controls.Add(this.sendBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 283);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(751, 32);
            this.panel1.TabIndex = 2;
            // 
            // transBtn
            // 
            this.transBtn.BackColor = System.Drawing.SystemColors.Control;
            this.transBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.transBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.transBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.transBtn.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.transBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.transBtn.Location = new System.Drawing.Point(0, 0);
            this.transBtn.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.transBtn.Name = "transBtn";
            this.transBtn.Size = new System.Drawing.Size(98, 32);
            this.transBtn.TabIndex = 2;
            this.transBtn.Text = "翻  译";
            this.transBtn.UseVisualStyleBackColor = false;
            this.transBtn.Click += new System.EventHandler(this.transBtn_Click);
            // 
            // sendBtn
            // 
            this.sendBtn.BackColor = System.Drawing.Color.Coral;
            this.sendBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendBtn.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.sendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendBtn.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.sendBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.sendBtn.Location = new System.Drawing.Point(0, 0);
            this.sendBtn.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(751, 32);
            this.sendBtn.TabIndex = 1;
            this.sendBtn.Text = "发 送";
            this.sendBtn.UseVisualStyleBackColor = false;
            this.sendBtn.Click += new System.EventHandler(this.transSendBtn_Click);
            // 
            // msgRTBox
            // 
            this.msgRTBox.Location = new System.Drawing.Point(0, 32);
            this.msgRTBox.Name = "msgRTBox";
            this.msgRTBox.Size = new System.Drawing.Size(743, 245);
            this.msgRTBox.TabIndex = 1;
            this.msgRTBox.Text = "[username]";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stickerTSMI,
            this.picTSMI,
            this.itemTSMI,
            this.toolStripTextBox1,
            this.langTsCBox});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(512, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // stickerTSMI
            // 
            this.stickerTSMI.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.stickerTSMI.Name = "stickerTSMI";
            this.stickerTSMI.Size = new System.Drawing.Size(53, 28);
            this.stickerTSMI.Text = "表 情";
            // 
            // picTSMI
            // 
            this.picTSMI.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.picTSMI.Name = "picTSMI";
            this.picTSMI.Size = new System.Drawing.Size(53, 28);
            this.picTSMI.Text = "图 片";
            this.picTSMI.Click += new System.EventHandler(this.picTSMI_Click);
            // 
            // itemTSMI
            // 
            this.itemTSMI.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.itemTSMI.Name = "itemTSMI";
            this.itemTSMI.Size = new System.Drawing.Size(53, 28);
            this.itemTSMI.Text = "商 品";
            this.itemTSMI.Click += new System.EventHandler(this.itemTSMI_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripTextBox1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 28);
            this.toolStripTextBox1.Tag = "";
            this.toolStripTextBox1.Text = "消息翻译为:";
            this.toolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // langTsCBox
            // 
            this.langTsCBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.langTsCBox.Items.AddRange(new object[] {
            "英语",
            "泰语",
            "马来语",
            "菲律宾语",
            "越南语"});
            this.langTsCBox.Name = "langTsCBox";
            this.langTsCBox.Size = new System.Drawing.Size(121, 28);
            this.langTsCBox.Text = "目标语言";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(751, 513);
            this.splitContainer2.SplitterDistance = 371;
            this.splitContainer2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logRText);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(751, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // logRText
            // 
            this.logRText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRText.Location = new System.Drawing.Point(3, 17);
            this.logRText.Name = "logRText";
            this.logRText.ReadOnly = true;
            this.logRText.Size = new System.Drawing.Size(745, 118);
            this.logRText.TabIndex = 0;
            this.logRText.Text = "***消息中可以使用[username],来指代用户名，群发时，将使用买家名称来取代[username],注意，是连同中括号的";
            // 
            // MessageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 513);
            this.Controls.Add(this.splitContainer2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MessageDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发送消息";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.msgCountNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.msgSpanNUD)).EndInit();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.NumericUpDown msgSpanNUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label le1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button transBtn;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.RichTextBox msgRTBox;
        private System.Windows.Forms.ToolStripMenuItem stickerTSMI;
        private System.Windows.Forms.ToolStripMenuItem picTSMI;
        private System.Windows.Forms.ToolStripMenuItem itemTSMI;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripComboBox langTsCBox;
        private System.Windows.Forms.ComboBox customerListCBox;
        private System.Windows.Forms.NumericUpDown msgCountNUD;
        private System.Windows.Forms.CheckBox mutilChatCkBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox logRText;
    }
}