namespace AccelerateBall
{
    partial class BigForm
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
            if(disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            this.title = new System.Windows.Forms.Label();
            this.DetailsPanel = new System.Windows.Forms.Panel();
            this.progressPanel = new System.Windows.Forms.Panel();
            this.processListBox = new System.Windows.Forms.CheckedListBox();
            this.systemResources = new System.Windows.Forms.Label();
            this.ballControl = new AccelerateBall.BallControl();
            this.info = new System.Windows.Forms.Label();
            this.networkSpeedPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.greenLabel = new System.Windows.Forms.Label();
            this.AcceleratePanel = new System.Windows.Forms.Panel();
            this.computer = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.hideDetailsFormTimer = new System.Windows.Forms.Timer(this.components);
            this.DetailsPanel.SuspendLayout();
            this.progressPanel.SuspendLayout();
            this.networkSpeedPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Location = new System.Drawing.Point(14, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(65, 12);
            this.title.TabIndex = 0;
            this.title.Text = "电脑加速球";
            this.title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.title.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // DetailsPanel
            // 
            this.DetailsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.DetailsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.DetailsPanel.Controls.Add(this.progressPanel);
            this.DetailsPanel.Controls.Add(this.ballControl);
            this.DetailsPanel.Controls.Add(this.info);
            this.DetailsPanel.Controls.Add(this.networkSpeedPanel);
            this.DetailsPanel.Controls.Add(this.greenLabel);
            this.DetailsPanel.Controls.Add(this.AcceleratePanel);
            this.DetailsPanel.Controls.Add(this.computer);
            this.DetailsPanel.Controls.Add(this.closeBtn);
            this.DetailsPanel.Controls.Add(this.clearBtn);
            this.DetailsPanel.Controls.Add(this.title);
            this.DetailsPanel.Location = new System.Drawing.Point(0, 0);
            this.DetailsPanel.Name = "DetailsPanel";
            this.DetailsPanel.Size = new System.Drawing.Size(285, 419);
            this.DetailsPanel.TabIndex = 1;
            this.DetailsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DetailsPanel_Paint);
            this.DetailsPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.DetailsPanel.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.DetailsPanel.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            this.DetailsPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.DetailsPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // progressPanel
            // 
            this.progressPanel.BackColor = System.Drawing.Color.White;
            this.progressPanel.Controls.Add(this.processListBox);
            this.progressPanel.Controls.Add(this.systemResources);
            this.progressPanel.Location = new System.Drawing.Point(1, 147);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(283, 238);
            this.progressPanel.TabIndex = 7;
            this.progressPanel.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.progressPanel.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            // 
            // processListBox
            // 
            this.processListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.processListBox.CheckOnClick = true;
            this.processListBox.FormattingEnabled = true;
            this.processListBox.Location = new System.Drawing.Point(19, 31);
            this.processListBox.Name = "processListBox";
            this.processListBox.Size = new System.Drawing.Size(264, 208);
            this.processListBox.TabIndex = 1;
            this.processListBox.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.processListBox.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            // 
            // systemResources
            // 
            this.systemResources.AutoSize = true;
            this.systemResources.BackColor = System.Drawing.Color.White;
            this.systemResources.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.systemResources.ForeColor = System.Drawing.Color.Blue;
            this.systemResources.Location = new System.Drawing.Point(17, 9);
            this.systemResources.Name = "systemResources";
            this.systemResources.Size = new System.Drawing.Size(83, 12);
            this.systemResources.TabIndex = 0;
            this.systemResources.Text = "优化系统资源";
            // 
            // ballControl
            // 
            this.ballControl.Location = new System.Drawing.Point(9, 36);
            this.ballControl.Name = "ballControl";
            this.ballControl.Size = new System.Drawing.Size(105, 105);
            this.ballControl.TabIndex = 6;
            this.ballControl.Text = "ballControl";
            this.ballControl.Click += new System.EventHandler(this.ballControl_Click);
            this.ballControl.MouseEnter += new System.EventHandler(this.ballControl_MouseEnter);
            this.ballControl.MouseLeave += new System.EventHandler(this.ballControl_MouseLeave);
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.info.Location = new System.Drawing.Point(117, 71);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(137, 12);
            this.info.TabIndex = 5;
            this.info.Text = "可用内存充足，继续保持";
            this.info.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.info.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.info.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.info.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // networkSpeedPanel
            // 
            this.networkSpeedPanel.BackgroundImage = global::AccelerateBall.Properties.Resources.network_pressed;
            this.networkSpeedPanel.Controls.Add(this.panel2);
            this.networkSpeedPanel.Location = new System.Drawing.Point(145, 385);
            this.networkSpeedPanel.Name = "networkSpeedPanel";
            this.networkSpeedPanel.Size = new System.Drawing.Size(140, 35);
            this.networkSpeedPanel.TabIndex = 3;
            this.networkSpeedPanel.Click += new System.EventHandler(this.networkSpeedPanel_Click);
            this.networkSpeedPanel.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.networkSpeedPanel.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(141, 387);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(153, 44);
            this.panel2.TabIndex = 6;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // greenLabel
            // 
            this.greenLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.greenLabel.ForeColor = System.Drawing.Color.LimeGreen;
            this.greenLabel.Location = new System.Drawing.Point(149, 28);
            this.greenLabel.Name = "greenLabel";
            this.greenLabel.Size = new System.Drawing.Size(123, 32);
            this.greenLabel.TabIndex = 4;
            this.greenLabel.Text = "充满活力";
            this.greenLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.greenLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.greenLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // AcceleratePanel
            // 
            this.AcceleratePanel.BackgroundImage = global::AccelerateBall.Properties.Resources.ball_normal;
            this.AcceleratePanel.Location = new System.Drawing.Point(0, 385);
            this.AcceleratePanel.Name = "AcceleratePanel";
            this.AcceleratePanel.Size = new System.Drawing.Size(145, 35);
            this.AcceleratePanel.TabIndex = 2;
            this.AcceleratePanel.Click += new System.EventHandler(this.AcceleratePanel_Click);
            this.AcceleratePanel.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.AcceleratePanel.MouseLeave += new System.EventHandler(this.DetailsPanel_MouseLeave);
            // 
            // computer
            // 
            this.computer.AutoSize = true;
            this.computer.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.computer.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.computer.Location = new System.Drawing.Point(114, 41);
            this.computer.Name = "computer";
            this.computer.Size = new System.Drawing.Size(42, 16);
            this.computer.TabIndex = 3;
            this.computer.Text = "电脑";
            this.computer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseDown);
            this.computer.MouseEnter += new System.EventHandler(this.DetailsPanel_MouseEnter);
            this.computer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseMove);
            this.computer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DetailsPanel_MouseUp);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.ForeColor = System.Drawing.Color.Transparent;
            this.closeBtn.Image = global::AccelerateBall.Properties.Resources.close_normal;
            this.closeBtn.Location = new System.Drawing.Point(259, 6);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(18, 18);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            this.closeBtn.MouseEnter += new System.EventHandler(this.closeBtn_MouseEnter);
            this.closeBtn.MouseLeave += new System.EventHandler(this.closeBtn_MouseLeave);
            // 
            // clearBtn
            // 
            this.clearBtn.BackColor = System.Drawing.Color.Transparent;
            this.clearBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.clearBtn.FlatAppearance.BorderSize = 0;
            this.clearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearBtn.ForeColor = System.Drawing.Color.Transparent;
            this.clearBtn.Image = global::AccelerateBall.Properties.Resources.normal_clear_btn;
            this.clearBtn.Location = new System.Drawing.Point(117, 100);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(135, 35);
            this.clearBtn.TabIndex = 1;
            this.clearBtn.UseVisualStyleBackColor = false;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            this.clearBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clearBtn_MouseDown);
            this.clearBtn.MouseEnter += new System.EventHandler(this.clearBtn_MouseEnter);
            this.clearBtn.MouseLeave += new System.EventHandler(this.clearBtn_MouseLeave);
            // 
            // hideDetailsFormTimer
            // 
            this.hideDetailsFormTimer.Interval = 700;
            this.hideDetailsFormTimer.Tick += new System.EventHandler(this.hideDetailsFormTimer_Tick);
            // 
            // BigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 420);
            this.Controls.Add(this.DetailsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "DetailsForm";
            this.TopMost = true;
            this.DetailsPanel.ResumeLayout(false);
            this.DetailsPanel.PerformLayout();
            this.progressPanel.ResumeLayout(false);
            this.progressPanel.PerformLayout();
            this.networkSpeedPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Panel DetailsPanel;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Label computer;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.Label greenLabel;
        private BallControl ballControl;
        private System.Windows.Forms.Panel networkSpeedPanel;
        private System.Windows.Forms.Panel AcceleratePanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer hideDetailsFormTimer;
        private System.Windows.Forms.Panel progressPanel;
        private System.Windows.Forms.Label systemResources;
        private System.Windows.Forms.CheckedListBox processListBox;
    }
}