namespace ShopChatPlus.View
{
    partial class ShopTreeView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShopTreeView));
            this.treeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyShopIDTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.delteGroupDTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.treeImageList = new System.Windows.Forms.ImageList(this.components);
            this.chatFormNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.imageListTabIcon = new System.Windows.Forms.ImageList(this.components);
            this.storeTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.添加店铺ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除店铺ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.添加店群ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除店群ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.生成模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeContextMenuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeContextMenuStrip
            // 
            this.treeContextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.treeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyShopIDTSM,
            this.toolStripSeparator1,
            this.delteGroupDTSM});
            this.treeContextMenuStrip.Name = "treeContextMenuStrip";
            this.treeContextMenuStrip.Size = new System.Drawing.Size(144, 54);
            // 
            // copyShopIDTSM
            // 
            this.copyShopIDTSM.Name = "copyShopIDTSM";
            this.copyShopIDTSM.Size = new System.Drawing.Size(143, 22);
            this.copyShopIDTSM.Text = "复制ShopID";
            this.copyShopIDTSM.Click += new System.EventHandler(this.copyShopIDTSM_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // delteGroupDTSM
            // 
            this.delteGroupDTSM.Name = "delteGroupDTSM";
            this.delteGroupDTSM.Size = new System.Drawing.Size(143, 22);
            this.delteGroupDTSM.Text = "删除店群";
            this.delteGroupDTSM.Click += new System.EventHandler(this.delteGroupTSM_Click);
            // 
            // treeImageList
            // 
            this.treeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImageList.ImageStream")));
            this.treeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeImageList.Images.SetKeyName(0, "group0.png");
            this.treeImageList.Images.SetKeyName(1, "group1.png");
            this.treeImageList.Images.SetKeyName(2, "region0.png");
            this.treeImageList.Images.SetKeyName(3, "region1.png");
            this.treeImageList.Images.SetKeyName(4, "store0.png");
            this.treeImageList.Images.SetKeyName(5, "store1.png");
            this.treeImageList.Images.SetKeyName(6, "user0.png");
            this.treeImageList.Images.SetKeyName(7, "user1.png");
            this.treeImageList.Images.SetKeyName(8, "list0.png");
            this.treeImageList.Images.SetKeyName(9, "list1.png");
            this.treeImageList.Images.SetKeyName(10, "choose0.png");
            this.treeImageList.Images.SetKeyName(11, "choose1.png");
            // 
            // chatFormNotifyIcon
            // 
            this.chatFormNotifyIcon.BalloonTipText = "店聊通";
            this.chatFormNotifyIcon.BalloonTipTitle = "店聊通";
            this.chatFormNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("chatFormNotifyIcon.Icon")));
            this.chatFormNotifyIcon.Text = "店聊通";
            this.chatFormNotifyIcon.Visible = true;
            // 
            // imageListTabIcon
            // 
            this.imageListTabIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTabIcon.ImageStream")));
            this.imageListTabIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTabIcon.Images.SetKeyName(0, "聊聊整合.png");
            this.imageListTabIcon.Images.SetKeyName(1, "采集上货.png");
            this.imageListTabIcon.Images.SetKeyName(2, "流量统计.png");
            this.imageListTabIcon.Images.SetKeyName(3, "违规统计.png");
            this.imageListTabIcon.Images.SetKeyName(4, "营销管理.png");
            this.imageListTabIcon.Images.SetKeyName(5, "订单状态.png");
            this.imageListTabIcon.Images.SetKeyName(6, "对账辅助.png");
            this.imageListTabIcon.Images.SetKeyName(7, "代发贴单.png");
            this.imageListTabIcon.Images.SetKeyName(8, "产品管理.png");
            this.imageListTabIcon.Images.SetKeyName(9, "订单处理.png");
            this.imageListTabIcon.Images.SetKeyName(10, "粉丝关注.png");
            this.imageListTabIcon.Images.SetKeyName(11, "消息群发.png");
            this.imageListTabIcon.Images.SetKeyName(12, "计划任务.png");
            // 
            // storeTreeView
            // 
            this.storeTreeView.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.storeTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.storeTreeView.ContextMenuStrip = this.treeContextMenuStrip;
            this.storeTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storeTreeView.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.storeTreeView.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.storeTreeView.ImageIndex = 0;
            this.storeTreeView.ImageList = this.treeImageList;
            this.storeTreeView.Indent = 25;
            this.storeTreeView.ItemHeight = 25;
            this.storeTreeView.Location = new System.Drawing.Point(0, 0);
            this.storeTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.storeTreeView.Name = "storeTreeView";
            this.storeTreeView.SelectedImageIndex = 11;
            this.storeTreeView.ShowNodeToolTips = true;
            this.storeTreeView.Size = new System.Drawing.Size(162, 224);
            this.storeTreeView.TabIndex = 1;
            this.storeTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.storeTreeView_AfterSelect);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 224);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(162, 55);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加店铺ToolStripMenuItem,
            this.删除店铺ToolStripMenuItem});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.toolStripButton1.Size = new System.Drawing.Size(65, 35);
            this.toolStripButton1.Text = "添加店铺";
            this.toolStripButton1.ToolTipText = "店铺设置";
            // 
            // 添加店铺ToolStripMenuItem
            // 
            this.添加店铺ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("添加店铺ToolStripMenuItem.Image")));
            this.添加店铺ToolStripMenuItem.Name = "添加店铺ToolStripMenuItem";
            this.添加店铺ToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.添加店铺ToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.添加店铺ToolStripMenuItem.Text = "添加店铺";
            // 
            // 删除店铺ToolStripMenuItem
            // 
            this.删除店铺ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("删除店铺ToolStripMenuItem.Image")));
            this.删除店铺ToolStripMenuItem.Name = "删除店铺ToolStripMenuItem";
            this.删除店铺ToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.删除店铺ToolStripMenuItem.Text = "删除店铺";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加店群ToolStripMenuItem,
            this.删除店群ToolStripMenuItem});
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.toolStripButton2.Size = new System.Drawing.Size(65, 35);
            this.toolStripButton2.Text = "店群管理";
            // 
            // 添加店群ToolStripMenuItem
            // 
            this.添加店群ToolStripMenuItem.Name = "添加店群ToolStripMenuItem";
            this.添加店群ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.添加店群ToolStripMenuItem.Text = "添加店群";
            // 
            // 删除店群ToolStripMenuItem
            // 
            this.删除店群ToolStripMenuItem.Name = "删除店群ToolStripMenuItem";
            this.删除店群ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.删除店群ToolStripMenuItem.Text = "删除店群";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.生成模板ToolStripMenuItem,
            this.导入模板ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Padding = new System.Windows.Forms.Padding(0, 0, 40, 0);
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(85, 36);
            this.toolStripDropDownButton1.Text = "店铺模板";
            // 
            // 生成模板ToolStripMenuItem
            // 
            this.生成模板ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("生成模板ToolStripMenuItem.Image")));
            this.生成模板ToolStripMenuItem.Name = "生成模板ToolStripMenuItem";
            this.生成模板ToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.生成模板ToolStripMenuItem.Text = "生成模板";
            // 
            // 导入模板ToolStripMenuItem
            // 
            this.导入模板ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("导入模板ToolStripMenuItem.Image")));
            this.导入模板ToolStripMenuItem.Name = "导入模板ToolStripMenuItem";
            this.导入模板ToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.导入模板ToolStripMenuItem.Text = "导入模板";
            // 
            // ShopTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.storeTreeView);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ShopTreeView";
            this.Size = new System.Drawing.Size(162, 279);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.treeContextMenuStrip.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList treeImageList;
        private System.Windows.Forms.ContextMenuStrip treeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyShopIDTSM;
        private System.Windows.Forms.NotifyIcon chatFormNotifyIcon;
        private System.Windows.Forms.ImageList imageListTabIcon;
        private System.Windows.Forms.TreeView storeTreeView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem delteGroupDTSM;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem 添加店铺ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除店铺ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem 添加店群ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除店群ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 生成模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入模板ToolStripMenuItem;
    }
}