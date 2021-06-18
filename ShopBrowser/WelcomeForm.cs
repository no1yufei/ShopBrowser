using CefSharp.WinForms;
using Common.Browser;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopeeChat
{
    public partial class WelcomeForm : Form
    {
        //ChromiumWebBrowser cwb;
        StoreWebBrowser swb;
        public WelcomeForm(string url)
        {
            InitializeComponent();
            //webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            //webBrowser1.Navigate(url);
            //"http://www.dianliaotong.com/news/news.html"
            swb = new StoreWebBrowser(url, BrowerHelper.Instatce.GetCacheDir("\\cap\\cap"));
            this.panel1.Controls.Add(swb.ChromiumWebBrowser);
            
        }

        private void WelcomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            swb.Dispose();
            swb = null;
           // webBrowser1.Dispose();
        }
        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)

        {

            WebBrowser webBrowser = (WebBrowser)sender;

            if (webBrowser.ReadyState == WebBrowserReadyState.Complete)

            {

                //获取文档编码

                Encoding encoding = Encoding.GetEncoding(webBrowser.Document.Encoding);

                StreamReader stream = new StreamReader(webBrowser.DocumentStream, encoding);

                string htmlMessage = stream.ReadToEnd();

            }

        }
    }
}
