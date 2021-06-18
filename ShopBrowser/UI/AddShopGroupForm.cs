using ShopeeChat.Shopee.API;
using ShopeeChat.SysData;
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
    public partial class AddShopGroupForm : Form
    {
        public StoreGroup ShopGroup;
        public AddShopGroupForm()
        {
            InitializeComponent();
        }

        private void sureBtn_Click(object sender, EventArgs e)
        {
            ShopGroup = new StoreGroup();
            ShopGroup.ID = Guid.NewGuid();
            ShopGroup.GroupName = groupNameTBox.Text;
            ShopGroup.ProxyIP = serverTBox.Text;
            ShopGroup.Port = (long)portNumDU.Value;
            ShopGroup.ProxyUserName = userNameTBox.Text;
            ShopGroup.Password = passwordTBox.Text;
            ShopGroup.IsProxy = isProxyCkBox.Checked;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void isProxyCkBox_CheckedChanged(object sender, EventArgs e)
        {
            serverTBox.Enabled = isProxyCkBox.Checked;
            portNumDU.Enabled = isProxyCkBox.Checked;
            userNameTBox.Enabled = isProxyCkBox.Checked;
            passwordTBox.Enabled = isProxyCkBox.Checked;
        }
    }
}
