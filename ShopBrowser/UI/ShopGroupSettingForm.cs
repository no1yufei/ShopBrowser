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
    public partial class ShopGroupSettingForm : Form
    {
        public ShopGroupSettingForm(List<StoreGroup> groups,StoreGroup group = null)
        {
            InitializeComponent();
            initGroupsCombox(groups, group);
        }
        void initGroupsCombox(List<StoreGroup> groups, StoreGroup group)
        {
            groupCBox.DataSource = groups;
            if(null != group)
            {
                groupCBox.SelectedIndex = groups.IndexOf(group);
                groupCBox.Enabled = false;
            }
        }
        private void sureBtn_Click(object sender, EventArgs e)
        {
            StoreGroup group = (StoreGroup)groupCBox.SelectedValue;
            group.ID = Guid.NewGuid();
            //group.GroupName = groupNameTBox.Text;
            group.ProxyIP = serverTBox.Text;
            group.Port = (long)portNumDU.Value;
            group.ProxyUserName = userNameTBox.Text;
            group.Password = passwordTBox.Text;
            group.IsProxy = isProxyCkBox.Checked;
            MessageBox.Show("修改成功");
            //Close();
        }

        private void groupCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(groupCBox.SelectedValue != null)
            {
                StoreGroup group = (StoreGroup)groupCBox.SelectedValue;
                serverTBox.Text = group.ProxyIP;
                portNumDU.Value = group.Port;
                userNameTBox.Text = group.ProxyUserName;
                passwordTBox.Text = group.Password;
                isProxyCkBox.Checked = group.IsProxy;
            }
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

        private void cancelBtn_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
