using Common.Shopee.API.Data;
using ShopeeChat.Shopee.API;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using ShopeeChat.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ShopeeChat.SysData.GroupConfigHelper;

namespace ShopeeChat
{
    public partial class CaptchaLoginForm : Form
    {
        ShopLogDataWithCaptchaInfo loginData;
        Store store;
        StoreGroup group;
        
        string capchaURL = "/api/selleraccount/get_captcha_info/?captcha_key=";
        public bool IsLoginSuccess = false;

        public bool IsUpdateInfo = false;
        notifyStoreInfoUpdaeDele updateInfoHandler = null;
        public CaptchaLoginForm(StoreGroup group, Store store, notifyStoreInfoUpdaeDele handler = null)
        {
            InitializeComponent();
            this.store = store;
            this.group = group;
            userTBox.Text = store.UserName;
            passwordTBox.Text = store.Password;
            updateInfoHandler = handler;

            capchaURL = store.ServerURL + capchaURL;
            ShopLogDataWithCaptchaInfo data = new ShopLogDataWithCaptchaInfo(store);
            loginData = data;
            userTBox.Text = loginData.username;
            passwordTBox.Text = loginData.password;
            updateDisplay();
        }
        void updateDisplay()
        {
            passwordTBox.Enabled =  store.LogStatus == LoginStatus.PasswordErr;
            userTBox.Enabled = store.LogStatus == LoginStatus.UserNameErr || store.LogStatus == LoginStatus.PasswordErr;
            statusTBox.Text = store.LogMessage[store.LogStatus];
            captchTBox.Text  = "";
            captchTBox.Enabled = (store.LogStatus == LoginStatus.Captcha_Req || store.LogStatus == LoginStatus.OTP_Req);
            clearCaptchaPicture();
            if(store.LogStatus == LoginStatus.Captcha_Req )
            {
                loadCaptchaPicture();
            }
        }
        private void loginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string captcha = null;
                string captcha_key = null;
                string vcode = null;
                if (store.LogStatus == LoginStatus.PasswordErr || store.LogStatus == LoginStatus.UserNameErr)
                {
                    store.Password = passwordTBox.Text;
                    store.UserName = userTBox.Text;
                    IsUpdateInfo = true;
                }
                else if (store.LogStatus == LoginStatus.Captcha_Req)
                {
                    captcha = captchTBox.Text;
                    captcha_key = loginData.captcha.GetGuid().ToString("N");
                }
                else if (store.LogStatus == LoginStatus.OTP_Req)
                {
                    vcode = captchTBox.Text;
                }

                ShopeeAPI shoppeAPI = new ShopeeAPI();
                store.Cookies = "";
                store.Hhh.sCookies = "";
                shoppeAPI.Login(group, store, captcha, captcha_key, vcode);
                if (!shoppeAPI.IsLogin(store))
                {
                    MessageBox.Show("登录失败："+store.LogMessage[store.LogStatus]);
                    updateDisplay();
                    if (null != updateInfoHandler)
                    {
                        updateInfoHandler(group);
                    }
                }
                else
                {
                    IsLoginSuccess = true;
                    MessageBox.Show("登录成功！");
                    if(store.IsLocalAccount)
                    {
                        GroupConfigHelper.Instatce.Save();
                    }
                    Close();
                }
            }
            catch(Exception xe)
            {
                Console.WriteLine("登录出现错误：" + xe.Message);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void captchaPBox_Click(object sender, EventArgs e)
        {
            loadCaptchaPicture();
        }
        Timer timer = new Timer();
        private void clearCaptchaPicture()
        {
            timer.Stop();
            captchTBox.Text = "";
            captchaPBox.Visible = false;
        }
            private void loadCaptchaPicture()
        {
            loginData.captcha = new Captcha();
            string captchaURL = capchaURL + loginData.captcha.GetGuid().ToString("N");
            string filePath = Environment.CurrentDirectory + "/" + loginData.captcha.GetGuid().ToString("N") + ".png";
            store.Hhh.DownLoad(captchaURL, filePath);
            captchaPBox.Load(filePath);
            captchaPBox.Visible = true;
            return;
            //timer.Stop();
            //captchTBox.Text = "验证码加载中...";
            //captchaPBox.LoadCompleted -= captchaPBox_LoadCompleted;
            //captchaPBox.CancelAsync();
            //loginData.captcha = new Captcha();
            //string captchaURL = capchaURL + loginData.captcha.GetGuid().ToString("N");
            //captchaPBox.Load(captchaURL);
            //captchaPBox.LoadCompleted += captchaPBox_LoadCompleted;
            //this.loginBtn.Enabled = false;
            //this.captchTBox.Enabled = false;
            //timer.Interval = 30000;
            //timer.Tick += timer_Tick;
            //timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            captchaPBox.CancelAsync();
            captchaPBox.Visible = true;
            captchTBox.Text = "加载失败，点击下方图标重新加载...";
        }

        private void captchaPBox_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            timer.Stop();
            captchaPBox.Visible = true;
            this.loginBtn.Enabled = true;
            this.captchTBox.Enabled = true;
            captchTBox.Text = "";
        }
    }
}
