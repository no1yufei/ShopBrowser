using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Management;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;

namespace AccelerateBall {
    public partial class BigForm : Form {       
        
        public enum ShowCommands{
            HIDE = 1,
        }
        [DllImport("shell32.dll")]
        static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, 
            string lpParameters, string lpDirectory, ShowCommands nShowCmd);
        [DllImport("shell32.dll")]
        static extern int SHEmptyRecycleBin(IntPtr handle, string root, int falgs);

        private bool isMouseDown = false;
        public bool isMouseEnter = false;

        private Point mouseOffset;
        private MiniForm miniForm;

        public BigForm(MiniForm miniForm) {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.miniForm = miniForm;           
        }

        #region 按钮不同状态的图标
        private void clearBtn_MouseEnter(object sender, EventArgs e)
        {
            clearBtn.Image = new Bitmap(Properties.Resources.over_clear_btn);
        }

        private void clearBtn_MouseDown(object sender, MouseEventArgs e)
        {
            clearBtn.Image = new Bitmap(Properties.Resources.press_clear_btn);
        }

        private void clearBtn_MouseLeave(object sender, EventArgs e)
        {
            clearBtn.Image = new Bitmap(Properties.Resources.normal_clear_btn);
        }

        private void closeBtn_MouseEnter(object sender, EventArgs e)
        {
            closeBtn.Image = new Bitmap(Properties.Resources.close_press);
        }

        private void closeBtn_MouseLeave(object sender, EventArgs e)
        {
            closeBtn.Image = new Bitmap(Properties.Resources.close_normal);
        }
        #endregion

        #region DetailsPanel和ballControl的鼠标事件
        private void DetailsPanel_MouseLeave(object sender, EventArgs e)
        {
            Point p = MousePosition;

            if(p.X - 10 <= this.Left || p.X + 10 >= this.Right || p.Y - 10 <= this.Top || p.Y + 10 >= this.Top + this.Height)
            {
                isMouseEnter = false;
                hideDetailsFormTimer.Enabled = true;
            }
        }

        private void DetailsPanel_MouseEnter(object sender, EventArgs e)
        {
            isMouseEnter = true;
            miniForm.TopLevel = this.TopLevel;

        }

        private void DetailsPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                mouseOffset = new Point(MousePosition.X - this.Location.X, MousePosition.Y - this.Location.Y);
                this.Cursor = Cursors.SizeAll;
            }
        }

        private void DetailsPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            this.Cursor = Cursors.Default;
        }

        private void DetailsPanel_MouseMove(object sender, MouseEventArgs e)
        {
            miniForm.isMouseEnter = false;
            if(isMouseDown == true)
            {                
                this.Location = getDetailFormMoveLocation();
                miniForm.Location = getMiniBallMoveLocation();
            }
        }

        private void ballControl_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void ballControl_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        /*隐藏DetailsForm的定时器*/
        private void hideDetailsFormTimer_Tick(object sender, EventArgs e) {
            hideDetailsFormTimer.Enabled = false;
            if(!isMouseEnter && !miniForm.isMouseEnter)
            {
                this.Hide();
            }
        }

        /*获取小球出现的位置*/
        private Point getMiniBallMoveLocation() {
            int x = 0, y = 0;
            if(this.Location.Y <= miniForm.Location.Y) {
                if(miniForm.miniFormLocation == MiniForm.MiniFormLocation.bottomRight)
                    x = this.Location.X + this.Width - miniForm.miniFormWidth;
                else     
                    x = this.Location.X;
                y = this.Location.Y + this.Height + miniForm.miniBigFormSpace;         
            }
            else {
                if(miniForm.miniFormLocation == MiniForm.MiniFormLocation.topRigh)
                    x = this.Location.X + this.Width - miniForm.miniFormWidth;
                else 
                    x = this.Location.X;
                y = this.Location.Y - miniForm.miniFormHeight - miniForm.miniBigFormSpace;
            }
            return new Point(x, y);
        }

        /*获取DetailForm出现的位置*/
        private Point getDetailFormMoveLocation() {
            int x = MousePosition.X - mouseOffset.X;
            int y = MousePosition.Y - mouseOffset.Y;
            if(x < 0) {
                x = 0;
            }
            if(miniForm.Top > this.Top && y < 0) {
                y = 0;
            }
            if(miniForm.Top < this.Top && y < miniForm.miniFormHeight + miniForm.miniBigFormSpace){ //minBall在上面
                y = miniForm.miniFormHeight + miniForm.miniBigFormSpace;
            }

            if(Screen.PrimaryScreen.WorkingArea.Width - x < this.Width) {
                x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            }
            if(miniForm.Top < this.Top && Screen.PrimaryScreen.WorkingArea.Height - y < this.Height) { //minBall在上面               
                y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;                
            }
            if(miniForm.Top > this.Top && Screen.PrimaryScreen.WorkingArea.Height - y < (this.Height + miniForm.miniFormHeight + miniForm.miniBigFormSpace)) { //minBall在下面
                y = Screen.PrimaryScreen.WorkingArea.Height - this.Height - miniForm.miniFormHeight - miniForm.miniBigFormSpace;  
            }
            return new Point(x, y);
        }
        #endregion

        #region 单击事件
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AcceleratePanel_Click(object sender, EventArgs e)
        {
            AcceleratePanel.BackgroundImage = Properties.Resources.ball_normal;
            networkSpeedPanel.BackgroundImage = Properties.Resources.network_pressed;
        }
        
        private void networkSpeedPanel_Click(object sender, EventArgs e)
        {
            AcceleratePanel.BackgroundImage = Properties.Resources.ball_pressed;
            networkSpeedPanel.BackgroundImage = Properties.Resources.network_normal;
        }
        #endregion

        #region 刷新进程和服务列表
        public void refreshThread() {
            clearList();
            Thread refreshListThread = new Thread(refresh);
            refreshListThread.Start();
        }
        
        private void refresh() {
            refreshProcess();
            refreshService();
        }

        public void refreshProcess() {
            Process[] processs = Process.GetProcesses();            
            StringCollection collection = systemProcess();
            foreach(Process p in processs) {
                if(!collection.Contains(p.ProcessName.ToLower())) //不添加系统进程
                    processListBox.Items.Add(p.ProcessName.ToLower());
            }
        }

        public void refreshService() {
            ServiceController[] scs = ServiceController.GetServices();
            foreach(ServiceController sc in scs) {
                if(sc.Status.ToString().Equals("Running")) //只添加正在运行的服务
                    processListBox.Items.Add(sc.DisplayName + " "); //加一个空格是为了区分进程和服务的标志，结束进程和服务时用到
                sc.Close();
            }
        }

        public StringCollection systemProcess() {
            StringCollection collection = new StringCollection();
            String[] systemProcesss = { "svchost", "csrss", "rundll32", "snmp", "lserver", "wins", 
                                       "ismserv", "internat", "explorer", "dwm", "idle", "system",
                                       "smss", "winlogon", "services", "mstask", "winmgmt", "inetinfo", 
                                       "tlntsvr", "dns","dfssvc", "msdtc", "dmadmin", "msiexec", "rsvp",
                                       "dllhost", "wmiprvse", "dashost", "devenv", "msmpeng", "lsass",
                                       "spoolsv", "wininit", "taskhostex", "runtimebroker", "searchindexer",
                                       "taskmgr", "searchfilterhost", "searchprotocolhost", "加速球"};
            foreach(string process in systemProcesss) {
                collection.Add(process);
            }
            return collection;
        }

        public void clearList() {
            this.processListBox.Items.Clear();
        }
        #endregion

        #region 加速操作（内存、IE临时文件、回收站等加速）
        private void accelerate() {
            KillProcessServices(); 
            clearRecycleBin();
            clearIETemporaryFile();
        }

        public void KillProcessServices() {
            int count = processListBox.CheckedItems.Count;
            for(int i = count - 1; i > -1; i--) {
                string name = processListBox.CheckedItems[i].ToString();
                processListBox.Items.Remove(processListBox.CheckedItems[i]);
                if(name.EndsWith(" ")){ //区分进程和服务                   
                    killServices(name.TrimEnd());
                }
                else {                  
                    killProcess(name);
                }
            }         
        }

        private void killServices(string serviceName) {
            try {
                ServiceController[] scs = ServiceController.GetServices();
                foreach(ServiceController sc in scs) {
                    if(sc.DisplayName.Equals(serviceName)){                       
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped);                                                                              
                    }
                    sc.Refresh();
                    sc.Close();
                }                           
            }catch(Win32Exception e1) {                
            }catch(InvalidOperationException e2) {              
            }           
        }

        private void killProcess(string processName) {
            Process[] ps = Process.GetProcessesByName(processName);
            if(ps.Length > 0) {
                foreach(Process p in ps){
                    try {                       
                        p.Kill();
                    } catch(Win32Exception e1) {
                    } catch(NotSupportedException e2) {
                    } catch(InvalidOperationException e3) {
                    }                      
                }
            }
        }        
        
        private void clearRecycleBin() {
            SHEmptyRecycleBin(this.Handle, "", 0x000007);
        }

        private void clearIETemporaryFile() {
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 8", "", ShowCommands.HIDE);
        }
        #endregion

        #region 单击加速按钮事件
        private void clearBtn_Click(object sender, EventArgs e) {
            accelerate();
        }

        private void ballControl_Click(object sender, EventArgs e) {
            accelerate();
        }
        #endregion        
        
        /*画分割线*/
        private void DetailsPanel_Paint(object sender, PaintEventArgs e){
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.FromArgb(220, 220, 220));
            g.DrawLine(p, new Point(0, 145), new Point(285, 145));
            g.Dispose();
        }

        /*刷新大球里的内存使用率*/
        public void paintBigBallControl(string usedMemoryRate)
        {
            Graphics g = ballControl.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new SolidBrush(Color.FromArgb(212, 212, 212));
            g.FillEllipse(brush, 0, 0, 100, 100);
            brush = new SolidBrush(Color.FromArgb(169, 169, 169));
            g.FillEllipse(brush, 4, 4, 92, 92);
            brush = new SolidBrush(Color.GreenYellow);//填充的颜色
            g.FillEllipse(brush, 7, 7, 86, 86);

            brush = new SolidBrush(Color.FromArgb(75, 75, 75));//填充的颜色
            g.DrawString("%", new Font("微软雅黑", 8), brush, new PointF(53, 39));
            g.DrawString("已用", new Font("微软雅黑", 8), brush, new PointF(53, 51));
            g.DrawString(usedMemoryRate, new Font("微软雅黑", 20), brush, new PointF(18, 35));
            g.Dispose();
        }
    }
}
