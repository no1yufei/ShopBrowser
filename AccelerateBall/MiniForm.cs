using System;
using System.Drawing;
using System.Windows.Forms;
using NetWorkSpeedMonitor;
using System.Threading;
using System.Drawing.Drawing2D;
using AccelerateBall.Util;

namespace AccelerateBall {
    public partial class MiniForm : Form {
        
        private BigForm bigForm = null;
        private NetworkAdapter[] adapters;
        private Thread monitorMemoryThread = null;
        private Thread monitorNetworkThread = null;
        private AppConfig config = new AppConfig();
        private MemoryInfo memoryInfo = new MemoryInfo();
        private Point mouseOffset;
        private ToolStripMenuItem currentOpacityItem = null;
        public MiniFormLocation miniFormLocation;

        private bool isMouseDown = false;
        public bool isMouseEnter = false;
        public int miniBigFormSpace = 5; 
        public int miniFormWidth = 96;
        public int miniFormHeight = 40;


        /*移动时小球出现在bigForm窗体的位置方向枚举*/
        public enum MiniFormLocation{
            topLeft,
            topRigh,
            bottomLeft,
            bottomRight
        }
        
        public MiniForm() {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            StartupSetting.autoRun("加速球.EXE", Application.ExecutablePath);
            initParameter();
            StartMonitorNetwork(); //初始化网络流量监控         
        }

        public void initParameter() {
            config.loadConfigFile(); //加载配置文件
            currentOpacityItem = getCurrentOpacityItem(config.getOpacity());
            setOpacity(currentOpacityItem, config.getOpacity()); //设置透明度
            Location = config.getMiniBallInitLocation(); //设置小球的坐标
            TopMost = config.getTopMost();
            if(TopMost){
                showStyle2.Image = new Bitmap(Properties.Resources.dot);
                showStyle1.Image = null;
            }
            else {
                showStyle1.Image = new Bitmap(Properties.Resources.dot);
                showStyle2.Image = null;
            }
        }

        #region 内存使用率监控和网速监控
        /*开始监控内存*/
        private void StartMonitorMemory()
        {
            monitorMemoryThread = new Thread(MenoryPercentage);
            monitorMemoryThread.Start();
        }

        /*开始监控网络*/
        private void StartMonitorNetwork()
        {
            NetworkMonitor monitor = new NetworkMonitor();
            this.adapters = monitor.Adapters;
            if(this.adapters != null && adapters.Length > 0)
            {
                foreach(NetworkAdapter adapter in adapters)
                {
                    monitor.StartMonitoring(adapter);
                }
                monitorNetworkThread = new Thread(NetworkMonitor);
                monitorNetworkThread.IsBackground = true;
                monitorNetworkThread.Start();
            }
            else
            {
                MessageBox.Show("无网卡");
            }
            StartMonitorMemory();  //初始化内存使用率     
        }

        /*网络监控*/
        private void NetworkMonitor()
        {
            while(this.adapters != null && adapters.Length > 0)
            {
                Thread.Sleep(500);
                double downloadSpeedKbps = 0;
                double uploadSpeedKbps = 0;

                foreach(NetworkAdapter adapter in adapters)
                {
                    downloadSpeedKbps += adapter.DownloadSpeedKbps;
                    uploadSpeedKbps += adapter.UploadSpeedKbps;
                }
                this.uploadRate.Text = getFormatNetworkSpeed(uploadSpeedKbps);
                this.downloadRate.Text = getFormatNetworkSpeed(downloadSpeedKbps);
            }
        }

        /*获取格式化的网速*/
        private string getFormatNetworkSpeed(double speedKbps)
        {
            string speed = "";
            if(speedKbps >= 100) {
                speed = String.Format("{0:N0}", speedKbps);
            }
            else if(speedKbps >= 10) {
                speed = String.Format("{0:N1}", speedKbps);
            }
            else {
                if(speedKbps == 0.0 || speedKbps == 0.00)  {
                    speed = String.Format("{0:N0}", speedKbps);
                }
                else {
                    speed = String.Format("{0:N2}", speedKbps);
                }
            }
            return speed;
        }

        /*内存监控*/
        private void MenoryPercentage() {
            string oldRate = "";
            while(true) {
                Thread.Sleep(500);
                string usedMemoryRate = memoryInfo.getUsedMemoryRate();
                if(!usedMemoryRate.Equals(oldRate)) { 
                    paintMiniBallControl(usedMemoryRate + "%");
                    if(bigForm != null && bigForm.Visible) {
                        bigForm.paintBigBallControl(usedMemoryRate);
                    }
                }
                oldRate = usedMemoryRate;
            }
        }

        /*刷新内存使用率*/
        private void paintMiniBallControl(string usedMemoryRate)
        {
            if(miniBallControl != null) {
                Graphics g = miniBallControl.CreateGraphics();
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Brush brush = new SolidBrush(Color.GreenYellow);
                g.FillRectangle(brush, 10, 10, 20, 17);

                brush = new SolidBrush(Color.Black);//填充的颜色
                g.DrawString(usedMemoryRate, new Font("宋体", 8), brush, new PointF(9, 14));
                g.Dispose();
            }
        }
        #endregion

        #region 小球的右键菜单单击事件
        /*退出程序*/
        private void quit_Click(object sender, EventArgs e){
            if(monitorMemoryThread != null)
            {
                monitorMemoryThread.Abort();
                monitorMemoryThread.Join();
            }
            if(monitorNetworkThread != null)
            {
                monitorNetworkThread.Abort();
                monitorNetworkThread.Join();
            }
            config.saveInfos(this.Location.X, this.Location.Y, (int)(this.Opacity*100), this.TopMost);
            notifyIcon.Dispose();
            Application.Exit();
        }

        /*显示或隐藏所有窗口*/
        private void showhide_Click(object sender, EventArgs e)
        {
            if(showhide.Text == "隐藏")
            {
                this.Hide();
                if(bigForm != null && bigForm.Visible)
                    bigForm.Hide();
                showhide.Text = "显示";
            }
            else
            {
                this.Show();
                showhide.Text = "隐藏";
            }
        }

        private void showStyle1_Click(object sender, EventArgs e)
        {
            showStyle1.Image = new Bitmap(Properties.Resources.dot);
            showStyle2.Image = null;
            this.TopMost = false;
            if(bigForm != null)
            {
                bigForm.TopMost = false;
            }
        }

        private void showStyle2_Click(object sender, EventArgs e)
        {
            showStyle2.Image = new Bitmap(Properties.Resources.dot);
            showStyle1.Image = null;
            this.TopMost = true;
            if(bigForm != null)
            {
                bigForm.TopMost = true;
            }
        }

        private void opacity100_Click(object sender, EventArgs e)
        {
            setOpacity(opacity100, 100);
        }

        private void opacity95_Click(object sender, EventArgs e)
        {
            setOpacity(opacity95, 95);
        }

        private void opacity85_Click(object sender, EventArgs e)
        {
            setOpacity(opacity85, 85);
        }

        private void opacity80_Click(object sender, EventArgs e)
        {
            setOpacity(opacity80, 80);
        }

        private void opacity75_Click(object sender, EventArgs e)
        {
            setOpacity(opacity75, 75);
        }

        private void opacity50_Click(object sender, EventArgs e)
        {
            setOpacity(opacity50, 50);
        }

        private void opacity25_Click(object sender, EventArgs e)
        {
            setOpacity(opacity25, 25);
        }

        /*设置窗体的透明度*/
        private void setOpacity(ToolStripMenuItem opacityItem, int opacity)
        {
            currentOpacityItem.Image = null;
            opacityItem.Image = new Bitmap(Properties.Resources.dot);
            this.Opacity = opacity * 0.01;
            if(bigForm != null)
            {
                bigForm.Opacity = opacity * 0.01;
            }
            currentOpacityItem = opacityItem;
        }

        private ToolStripMenuItem getCurrentOpacityItem(int opacity)
        {
            switch(opacity)
            {
                case 100: return opacity100;
                case 95: return opacity95;
                case 85: return opacity85;
                case 80: return opacity80;
                case 75: return opacity75;
                case 50: return opacity50;
                case 25: return opacity25;
                default: return opacity100;
            }
        }
        #endregion

        #region 小球的鼠标事件
        private void miniBigFormSpace_MouseEnter(object sender, EventArgs e)
        {
            isMouseEnter = true;
            if(bigForm == null || !bigForm.Visible)
                showDetailFormTimer.Enabled = true;
        }

        private void miniBigFormSpace_MouseLeave(object sender, EventArgs e)
        {
            Point p = MousePosition;         
            if(p.X - 10 <= this.Left || p.X + 10 >= this.Left + miniFormWidth || p.Y - 10 <= this.Top || p.Y + 10 >= this.Bottom){            
                isMouseEnter = false;
                hideDetailFormTimer.Enabled = true;
            }
        }

        private void miniBigFormSpace_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                mouseOffset = new Point(MousePosition.X - this.Location.X, MousePosition.Y - this.Location.Y);
                this.Cursor = Cursors.SizeAll;
            }

        }

        private void miniBigFormSpace_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            this.Cursor = Cursors.Default;
        }

        private void miniBigFormSpace_MouseMove(object sender, MouseEventArgs e)
        {
            if(isMouseDown == true)
            {
                Point old = this.Location;
                this.Location = getMiniBallMoveLocation();
                if(old.X != this.Location.X || old.Y != this.Location.Y)
                {
                    if(bigForm != null && bigForm.Visible)
                        hideDetailsForm();
                }
                else
                {
                    if(bigForm != null && !bigForm.Visible)
                    {
                        isMouseEnter = true;
                        showDetailFormTimer.Enabled = true;
                    }
                }
            }
        }
        #endregion

        #region 小球和bigForm的位置方法
        /*小球出现的位置*/
        private Point getMiniBallMoveLocation()
        {
            int x = MousePosition.X - mouseOffset.X;
            int y = MousePosition.Y - mouseOffset.Y;
            if(x < 0)
            {
                x = 0;
            }
            if(y < 0)
            {
                y = 0;
            }
            if(Screen.PrimaryScreen.WorkingArea.Width - x < miniFormWidth)
            {
                x = Screen.PrimaryScreen.WorkingArea.Width - miniFormWidth;
            }
            if(Screen.PrimaryScreen.WorkingArea.Height - y < miniFormHeight)
            {
                y = Screen.PrimaryScreen.WorkingArea.Height - miniFormHeight;
            }
            return new Point(x, y);
        }

        /*获取bigForm出现的位置*/
        private Point getDetailsFormLocation()
        {
            int x = 0, y = 0;
            Point miniBallLocation = this.Location;
            if(this.Location.Y >= bigForm.Height) //minBall在bigBall下面
            {
                if(Screen.PrimaryScreen.WorkingArea.Width - this.Location.X <= bigForm.Width)
                {
                    x = this.Location.X + miniFormWidth - bigForm.Width;
                    miniFormLocation = MiniFormLocation.bottomRight;
                }
                else
                {
                    x = this.Location.X;
                    miniFormLocation = MiniFormLocation.bottomLeft;
                }
                y = this.Location.Y - bigForm.Height - miniBigFormSpace;
            }
            else if(this.Location.Y < bigForm.Height) //minBall在bigBall上面
            {
                if(Screen.PrimaryScreen.WorkingArea.Width - this.Location.X > bigForm.Width)
                {
                    x = this.Location.X;
                    miniFormLocation = MiniFormLocation.topLeft;
                }
                else
                {
                    x = this.Location.X + miniFormWidth - bigForm.Width;
                    miniFormLocation = MiniFormLocation.topRigh;
                }
                y = this.Location.Y + miniFormHeight + miniBigFormSpace;
            }
            return new Point(x, y);
        }
        #endregion

        #region 显示和隐藏detailForm的方法和定时器
        /*隐藏bigForm*/
        private void hideDetailsForm()
        {
            if(bigForm != null && bigForm.Visible)
            {
                bigForm.Hide();              
            }
        }

        /*显示bigForm*/
        private void showDetailsForm() {
            if(bigForm == null) {
                bigForm = new BigForm(this);
                bigForm.Show();
                bigForm.Opacity = this.Opacity;
                bigForm.Location = getDetailsFormLocation();
            } else if(!bigForm.Visible){
                bigForm.Location = getDetailsFormLocation();
                bigForm.Show();
            }
            bigForm.refreshThread();            
        }

        /*显示bigForm的定时器*/
        private void showDetailFormTimer_Tick(object sender, EventArgs e) {
            if(isMouseEnter && !mainContextMenu.Visible) {
                showDetailsForm();
            }
            showDetailFormTimer.Enabled = false;
        }

        /*隐藏bigForm的定时器*/
        private void hideDetailFormTimer_Tick(object sender, EventArgs e) {
            hideDetailFormTimer.Enabled = false;
            if(bigForm != null && bigForm.Visible && !bigForm.isMouseEnter && !isMouseEnter) {
                hideDetailsForm();
            }
            if(bigForm != null && bigForm.isMouseEnter)
                isMouseEnter = false;
        }
        #endregion
    }
}
