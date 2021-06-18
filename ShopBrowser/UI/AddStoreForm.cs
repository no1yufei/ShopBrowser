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

namespace ShopeeChat.UI
{
    public partial class AddStoreForm : Form
    {
        private string importCookie = "";
        Guid spc_cds = Guid.NewGuid();
        public AddStoreForm(List<StoreGroup> groups, List<StoreRegion> regions = null,Store store = null)
        {
            InitializeComponent();
            initControl(groups,regions,store);
        }
        private void initControl(List<StoreGroup> groups, List<StoreRegion> regions, Store store)
        {
            groupCBox.DataSource = groups;
            groupCBox.DisplayMember = "GroupName";
            localCkBox.Checked = store == null? false: store.IsLocalAccount;
            onlyCookieCkBox.Checked = store == null ? false : store.OnlyCookie;

            regions = (regions == null) ? StoreRegionMap.GetRegionList() : regions;
            regionCBox.DataSource = regions;

            if(null != store)
            {
                accountTBox.Text = store.UserName;
                accountTBox.ReadOnly = true;
                this.Text = "修改店铺信息";
                addBtn.Text = "修改";
            }
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            if( accountTBox.Text.Length < 4)
            {
                MessageBox.Show("账号长度太短！");
                return;
            }
            if (passwordTBox.Text.Length < 6)
            {
                MessageBox.Show("密码长度太短！");
                return;
            }
            if (accountTBox.Text.Contains("@"))
            {
                MessageBox.Show("系统无法使用邮箱作为账号登录！请使用用户名作为虾皮登录账号！");
                return;
            }
            System.Text.RegularExpressions.Regex g = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
            if(g.IsMatch(accountTBox.Text))
            {
                MessageBox.Show("系统无法使用手机号作为账号登录！请使用用户名作为虾皮登录账号！");
                return;
            }
            try
            {
                ShopeeAPI shopeeAPI = new ShopeeAPI();
               
                StoreGroup group = groupCBox.SelectedItem as StoreGroup;
                StoreRegion region = regionCBox.SelectedItem as StoreRegion;

                Store store = new Store(region.RegionID, accountTBox.Text, passwordTBox.Text);
                store.IsLocalAccount = localCkBox.Checked;
                store.OnlyCookie = onlyCookieCkBox.Checked;
                if(store.IsLocalAccount && importCookie.Length  > 1)
                {
                    store.Cookies = importCookie;
                }
                store.SPC_CDS = spc_cds;
                shopeeAPI.Login(group,store);
                if(!shopeeAPI.IsLogin(store))
                {
                    CaptchaLoginForm loginForm = new CaptchaLoginForm(group, store);
                    while (loginForm.ShowDialog() == DialogResult.Yes)
                    {
                        if (loginForm.IsLoginSuccess)
                        {
                            break;
                        }
                    }
                    if (!shopeeAPI.IsLogin(store))
                    {
                        if(DialogResult.No == MessageBox.Show("添加用户验证失败:"+store.LogMessage[store.LogStatus]+",是否强制添加账号?","强制添加",MessageBoxButtons.YesNo))
                        {
                            return;
                        }
                    }
                }
                region = group.Regions.FirstOrDefault(p => p.RegionID == region.RegionID);
                if (null == region)
                {
                    region = new StoreRegion(region);
                    group.Regions.Add(region);
                }
                Store inStore = region.Stores.FirstOrDefault(p => p.UserName == store.UserName);
                if(null != inStore)
                { 
                        if (accountTBox.ReadOnly)
                        {
                            inStore.Password = passwordTBox.Text;
                            inStore.IsLocalAccount = localCkBox.Checked;
                        if (store.IsLocalAccount && importCookie.Length > 1)
                        {
                            store.Cookies = importCookie;
                        }
                        MessageBox.Show("修改店铺信息成功！");
                            cancelBtn_Click(null, null);
                        }
                        else
                        {
                            MessageBox.Show("已经包含相同名称的店铺账号！");
                        }
                        return;
                }
                region.Stores.Add(store);
                MessageBox.Show("添加店铺账号成功：" + accountTBox.Text);
                accountTBox.Text = "";
            }
            catch(Exception ex)
            {
                Console.WriteLine("添加账号出错:" + ex.Message);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.FormClosed += AddStoreForm_FormClosed;
            Close();
        }

        private void AddStoreForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GroupConfigHelper.Instatce.Save();
        }

        private void localCkBox_CheckedChanged(object sender, EventArgs e)
        {
            importCookieBtn.Visible = localCkBox.Checked;
            onlyCookieCkBox.Visible = localCkBox.Checked;
        }

        private void importCookieBtn_Click(object sender, EventArgs e)
        {
            CookieImportForm cForm = new CookieImportForm();
            cForm.ShowDialog();
            if(cForm.SCookie.Length  > 1)
            {
                importCookie = cForm.SCookie;
                spc_cds = cForm.SPC_CDS==Guid.Empty?spc_cds: cForm.SPC_CDS;
            }
        }
    }
}
