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
        public StoreGroup shopGroup;
        public StoreGroup ShopGroup {
            set {
                pfCBox.SelectedIndex = value.Plateform;
                shopGroup = value;
            }
            get {
                return shopGroup;
            }
        }

        public AddShopGroupForm()
        {
            InitializeComponent();
        }

        private void sureBtn_Click(object sender, EventArgs e)
        {
            shopGroup = new StoreGroup();
            shopGroup.Plateform = pfCBox.SelectedIndex;
            shopGroup.ID = Guid.NewGuid();
            shopGroup.GroupName = groupNameTBox.Text;
            shopGroup.ProxyIP = serverTBox.Text;
            shopGroup.Port = (long)portNumDU.Value;
            shopGroup.ProxyUserName = userNameTBox.Text;
            shopGroup.Password = passwordTBox.Text;
            shopGroup.IsProxy = isProxyCkBox.Checked;
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
