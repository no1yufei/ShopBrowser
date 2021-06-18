
using Common.Browser;
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

namespace Common.Brower
{
    public partial class PopBrowerForm : Form
    {
        public PopBrowerForm(StoreGroup group,StoreRegion region,String url)
        {
            InitializeComponent();
            StoreWebBrowser brower = new StoreWebBrowser(group,url,false);
            brower.AddToContainner(this,DockStyle.Fill);
        }
        public PopBrowerForm(StoreGroup group, String url,RequestHandler.RequestDataHandler callback)
        {
            InitializeComponent();
            StoreWebBrowser brower = new StoreWebBrowser(group, url, true,callback);
            brower.AddToContainner(this, DockStyle.Fill);
        }
        public PopBrowerForm(StoreGroup group, String url)
        {
            InitializeComponent();
            StoreWebBrowser brower = new StoreWebBrowser(group, url, true);
            brower.AddToContainner(this, DockStyle.Fill);
        }
        public PopBrowerForm(String url)
        {
            InitializeComponent();
            StoreWebBrowser brower = new StoreWebBrowser(url);
            brower.AddToContainner(this, DockStyle.Fill);
        }
        public PopBrowerForm(StoreGroup group, Store store, String url)
        {
            InitializeComponent();
            String cacheDir = BrowerHelper.Instatce.GetCacheDir(group, store);
            StoreWebBrowser webBrower = new StoreWebBrowser(group, store, cacheDir, url);
            webBrower.AddToContainner(this, DockStyle.Fill);
        }
        public PopBrowerForm(String url,String jsCode)
        {
            InitializeComponent();
            StoreWebBrowser brower = new StoreWebBrowser(url);
            brower.LoadUrl(url, jsCode);
            brower.AddToContainner(this, DockStyle.Fill);
        }
    }
}
