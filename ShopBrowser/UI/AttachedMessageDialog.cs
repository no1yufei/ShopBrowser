
using CefSharp;
using CefSharp.WinForms;
using Common.Brower;
using ShopBrowser.Properties;
using ShopeeChat.Shopee;
using ShopeeChat.Shopee.API;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopeeChat.UI
{
    public partial class AttachedMessageDialog : UserControl
    {
        Store store;
        ChromiumWebBrowser targetWebbrower;
        GoogleTrans gTrans = new GoogleTrans();
        Timer timer = new Timer();

        public AttachedMessageDialog(Store store, ChromiumWebBrowser targetwebbrower)
        {
            this.store = store;
            this.targetWebbrower = targetwebbrower;
            InitializeComponent();
            initLangSelection();
     
            initJQuery();
            timer.Interval = 2000;
            timer.Tick += timer_Tick;
        }

        void initLangSelection()
        {
            string defaultLang = langCodeNameMap.First(p => p[2] == GroupConfigHelper.Instatce.GetStoreRegion(store).RegionID)[0];
            bindLang(langTsCBox, defaultLang);
        }

        public void SetTargerWebBrower(Store store,ChromiumWebBrowser targetwebbrower)
        {
            this.store = store;
            this.targetWebbrower = targetwebbrower;
            initJQuery();
            initLangSelection();
        }
       
        String[][] langCodeNameMap = new String[][] {
                                                      //new string[] { "auto", "自动检测" },
                                                      new string[] { "en", "英语","sg" },
                                                      new string[] { "th", "泰语" ,"th"},
                                                      new string[] { "vi", "越南语" ,"vn"},
                                                      new string[] { "id", "印尼语" ,"id"},
                                                      new string[] { "ms", "马来语","my" },
                                                      new string[] { "tl", "菲律宾语","ph" },
                                                      new string[] { "jw", "印尼爪哇语","id" },
                                                      new string[] { "su", "印尼巽他语", "id" },
                                                      new string[] { "pt", "葡萄牙语", "br" },
                                                      new string[] { "es", "西班牙语", "br" },
                                                      new string[] { "zh-TW", "繁体中文","tw" }
        };
        private void bindLang(ToolStripComboBox cBox, string defultValue = "auto")
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("langcode", typeof(string));
            dt.Columns.Add("langname", typeof(string));
            dt.Columns.Add("regionid", typeof(string));
            foreach (String[] langcodeName in langCodeNameMap)
            {
                DataRow dr = dt.NewRow();
                dr["langcode"] = langcodeName[0];
                dr["langname"] = langcodeName[1];
                dr["regionid"] = langcodeName[2];
                dt.Rows.Add(dr);
            }
            cBox.ComboBox.DisplayMember = "langname";
            cBox.ComboBox.ValueMember = "langcode";
            cBox.ComboBox.DataSource = dt;
            cBox.ComboBox.SelectedValue = defultValue;
        }

        ChromiumWebBrowser webBrower;
        Interaction interaction;
      
               
        private void setSourceAutoResultText(String str)
        {
            try
            {
                string jquery = Resources.jquery;
                string fixedJs = @"$('textarea').click();";
                fixedJs += @"$('textarea').val( '" + str + "');";
                targetWebbrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(jquery);
                targetWebbrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(fixedJs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("写入浏览器消息：" + ex.Message);
            }
        }
       
       
        delegate void textChangedDele(object sender, EventArgs e);
        private void msgRTBox_TextChanged(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Start();

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            descMsgRTBox.Text = gTrans.Translate(orgmsgRTBox.Text,(string)langTsCBox.ComboBox.SelectedValue); ;
            setSourceAutoResultText(descMsgRTBox.Text);
        }

        delegate void operateBrowerJSDelt(string js);
        void opertaeBrowerJs(string js)
        {
            if(targetWebbrower.InvokeRequired)
            {
                targetWebbrower.Invoke(new operateBrowerJSDelt(opertaeBrowerJs),js);
            }
            else
            {
                targetWebbrower.ExecuteScriptAsync(js);
            }
        }
        void initJQuery()
        {
            if(targetWebbrower.IsBrowserInitialized)
            {
                targetWebbrower.ExecuteScriptAsync(Resources.jquery);
            }
            else
            {
                targetWebbrower.IsBrowserInitializedChanged += (sender,args) => {
                    if(targetWebbrower.CanExecuteJavascriptInMainFrame)
                    {
                        targetWebbrower.ExecuteScriptAsync(Resources.jquery);
                    }
                };
            }
        }



        private void TranslateBtn_Click(object sender, EventArgs e)
        {
            timer_Tick(null, null);
        }

        private void sendMsgBtn_Click(object sender, EventArgs e)
        {
            string jquery = Resources.jquery;
            string fixedJs = @"$('button').click();";

            targetWebbrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(jquery);
            targetWebbrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(fixedJs);
        }
    }
}
