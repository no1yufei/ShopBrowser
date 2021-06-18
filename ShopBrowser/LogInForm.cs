
using Common.Brower;
using Common.Browser;
using CommonData.SysData;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopeeChat
{
    public partial class LogInForm : Form
    {
        public bool IsLogSuccess = false;
        public bool IsClearCache = false;
        public string UserName;
        public string Password;
        private string ramdomCode = "";
        bool noPasswordLogin = false;
        UserConfig user;
        public LogInForm(string text,bool onlyVerify = false)
        {
            InitializeComponent();
            user = AccessControl.Instance.LoadUserUserInfoFromLocal();
            this.Text += "-" + text+" 版本号:"+AccessControl.Instance.Version;
            if(onlyVerify)
            {
                userTBox.Text = AccessControl.Instance.UserName;
                userTBox.ReadOnly = true;
            }
            else if(null != user)
            {
                passwordTBox.Text = "88888888";
                userTBox.Text = user.UserName;
                noPasswordLogin = true;
            }
        }

        private void loginBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
             createRandomCode();
        }
        private void createRandomCode()
        {
           ramdomCode = RandomCode.CreateRandomCode(4);
           RandomCode.CreateValidateImage(ramdomCode,randomPBox);
            if(noPasswordLogin)
            {
                randomCodeTBox.Text = ramdomCode;
            }
            //randomPBox.Refresh();
        }
        private void sureBtn_Click(object sender, EventArgs e)
        {
            try
            {

                if (randomCodeTBox.Text.ToLower() == ramdomCode.ToLower())
                {
                    
                    if(noPasswordLogin)
                    {
                        IsLogSuccess = AccessControl.Instance.Login(user);
                    }
                    else
                    {
                        IsLogSuccess = AccessControl.Instance.Login(userTBox.Text, passwordTBox.Text);
                    }
                    
                    if (IsLogSuccess)
                    {
                        UserName = userTBox.Text;
                        IsClearCache = clearCacheCBox.Checked;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("登录失败！");
                    }
                    passwordTBox.Text = "";
                }
                else
                {
                    MessageBox.Show("验证码错误！");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("网络错误，请稍后再试或更换您的网络！"+ex.Message);
            }
            createRandomCode();
        }

        private void registerAccountLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BrowerHelper.Instatce.OpenURLViaSysBrower("http://www.dianliaotong.com/Account/UserRegister");
        }

        private void randomPBox_Click(object sender, EventArgs e)
        {
            createRandomCode();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            createRandomCode();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            IsLogSuccess = false;
        }

        private void addGroupPBox_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://shang.qq.com/wpa/qunwpa?idkey=76a26046a9bbd0818be61b7da679a169e2f5f3f12bf599540f5f9536920e6b9d");
        }

       

        private void userTBox_TextChanged(object sender, EventArgs e)
        {
            if(noPasswordLogin)
            {
                passwordTBox.Text = "";
                randomCodeTBox.Text = "";
            }
            noPasswordLogin = false;
            
        }
     

        private void passwordTBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (noPasswordLogin)
            {
                passwordTBox.Text = "";
                randomCodeTBox.Text = "";
            }
            noPasswordLogin = false;
        }

        private void LinkLabelPwdReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BrowerHelper.Instatce.OpenURLViaSysBrower("http://www.dianliaotong.com/Account/ResetPassword");
            //MessageBox.Show("请各位注册店聊通时使用真实的手机号。\r\n重置密码发送信息“重置店聊通密码”至136 8268 7817。\r\n否则无法重置接收到重置密码。");
        }
    }
}
