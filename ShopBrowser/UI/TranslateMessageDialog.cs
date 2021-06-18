
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

namespace ShopeeChat
{
    public partial class TranslateMessageDialog : UserControl
    {
        Store store;
        ChromiumWebBrowser targetWebbrower;
        byte[] space = new byte[] { 0xc2, 0xa0 };
        string UTFSpace;
        Timer timer = new Timer();

        public TranslateMessageDialog(Store store, ChromiumWebBrowser targetwebbrower)
        {
            this.store = store;
            this.targetWebbrower = targetwebbrower;
            InitializeComponent();
            inlitBrower();
            initLangSelection();
            UTFSpace = Encoding.GetEncoding("UTF-8").GetString(space);
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
      

        void TranslaterMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            webBrower.Dispose();
        }
     
        public void inlitBrower()
        {
            webBrower = new ChromiumWebBrowser(string.Empty);
            interaction = new Interaction(new Interaction.ResultHandler(getResultText));
           // webBrower.RegisterJsObject("interaction", interaction);
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            CefSharpSettings.WcfEnabled = true;
            webBrower.JavascriptObjectRepository.Register("interaction", interaction, isAsync: false, options: BindingOptions.DefaultBinder);
            webBrower.Load("https://translate.google.cn/#auto/en");
            webBrower.FrameLoadEnd += brower_FrameLoadEnd;
      
            splitContainer2.Panel2.Controls.Add(webBrower);
            webBrower.Dock = DockStyle.Fill;
            this.InitDevTools(webBrower);

            this.Load += MainForm_Load;
        }

        void brower_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            msgRTBox_TextChanged(null, null);
            //registerOuputResultHandler(webBrower);
            //
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2Collapsed = true;//是否隐藏浏览器区域
        }

        /// <summary>
        /// 初始化开发者工具
        /// </summary>
        [Conditional("DEBUG")]
        private void InitDevTools(ChromiumWebBrowser brower)
        {
            const int f12KeyCode = 123;
            var keyboardHandler = new CefKeyboardHandler();
            keyboardHandler.KeyArrived += (b, t, c, m, i) =>
            {
                if ((t == KeyType.RawKeyDown) && (c == f12KeyCode))
                {
                    brower.ShowDevTools();
                }
            };
            brower.KeyboardHandler = keyboardHandler;
        }

        /// <summary>
        /// 进行翻译
        /// </summary>
        /// <param name="sentence">待翻译句子</param>
        private void doTranslate(string sentence, ChromiumWebBrowser brower)
        {
            try
            {
                registerOuputResultHandler(brower);
                brower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(string.Format("source.value='{0}'", sentence.Replace("\n", "\\n").Replace("'", "\\'").Replace("\"", "\\\"")));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void getTransResult()
        {
            string fixedJs = @"

             function getSpanByClass(sClass){
                 var aEle = document.getElementsByTagName('span');
                      var aResult = [];
                 for(var i = 0; i < aEle.length; i++){
                     if(aEle[i].className.indexOf(sClass) > -1){
                         aResult.push(aEle[i]);
                     }
                 }
                 return aResult;
                 }
        var spanobj = getSpanByClass('tlid-translation translation')[0];
        		                         interaction.setResult(spanobj.innerText);
        		                         console.log(spanobj.innerText);
                                  ";
            webBrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(fixedJs);

        }
        private void registerOuputResultHandler(ChromiumWebBrowser brower)
        {
            string fixedJs = @"
                          function getDivByClass(sClass){
         var aEle = document.getElementsByTagName('div');
              var aResult = [];
         for(var i = 0; i < aEle.length; i++){
             if(aEle[i].className.indexOf(sClass) > -1){
                 aResult.push(aEle[i]);
             }
         }
         return aResult;
     }
     function getSpanByClass(sClass){
         var aEle = document.getElementsByTagName('span');
              var aResult = [];
         for(var i = 0; i < aEle.length; i++){
             if(aEle[i].className.indexOf(sClass) > -1){
                 aResult.push(aEle[i]);
             }
         }
         return aResult;
     }
     var result_box = getDivByClass('result tlid-copy-target')[0];
     if(outputResult==undefined){
	                           function outputResult(target){
 var spanobj = getSpanByClass('tlid-translation translation')[0];
		                         interaction.setResult(spanobj.innerText);
		                         console.log(spanobj.innerText);
                               }
    }
    result_box.removeEventListener('DOMNodeInserted', outputResult);
	                        result_box.addEventListener('DOMNodeInserted', outputResult);
                          ";
            brower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(fixedJs);

        }
        string transResult = "";
        delegate void setTBoxDele(String str);
        private void getResultText(String str)
        {
            transResult = str;
            setSourceAutoResultText(str);
            Console.WriteLine("及时翻译："+str);
        }
        private void setSourceAutoResultText(String str)
        {
            //if (this.msgRTBox.InvokeRequired)
            //{
            //    this.Invoke(new setTBoxDele(setSourceAutoResultText), new Object[] { str });
            //}
            //else
            {
                string jquery = Resources.jquery;
                string fixedJs = @"$('textarea').val( '" + str.Replace(UTFSpace,"").Replace("\n","  ").Replace("'","\\'") +"');";
                targetWebbrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(jquery);
                //getTransResult();
                targetWebbrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(fixedJs);
                //targetWebbrower.ExecuteScriptAsync(fixedJs);
            }
        }
       
        /// <summary>
        /// 和浏览器交互
        /// </summary>
        internal class Interaction
        {
            public delegate void ResultHandler(String resultStr);
           
            private ResultHandler resultHandler;
            private ResultHandler userNameHandler;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="vm">主窗口的模型视图</param>
            internal Interaction(ResultHandler resultHandler)
            {
                this.resultHandler = resultHandler;
            }

            /// <summary>
            /// 设置翻译结果
            /// </summary>
            /// <param name="result">翻译结果</param>
            public void SetResult(string result)
            {
                //result = result.Replace("\n", "").Replace("\r", "");
                if (null != resultHandler)
                {
                    resultHandler(result);
                }
            }
            public void SetUserName(string result)
            {
                //result = result.Replace("\n", "").Replace("\r", "");
                if (null != resultHandler)
                {
                    userNameHandler(result);
                }
            }
        }
       
        delegate void textChangedDele(object sender, EventArgs e);
        private void msgRTBox_TextChanged(object sender, EventArgs e)
        {
            if (this.msgRTBox.InvokeRequired)
            {
                this.Invoke(new textChangedDele(msgRTBox_TextChanged), new Object[] { sender, e });
            }
            else
            //if((DateTime.Now - lastSourceInputTime).TotalSeconds > 4)
            {
                doTranslate(msgRTBox.Text, webBrower);
                timer.Stop();
                timer.Start();
                
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            getTransResult();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string jquery = Resources.jquery;
            string fixedJs = @"$('button').click();";
            
            targetWebbrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(jquery);
            targetWebbrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(fixedJs);
        }

        private void transBtn_Click(object sender, EventArgs e)
        {
            setSourceAutoResultText(transResult);
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
                    targetWebbrower.ExecuteScriptAsync(Resources.jquery);
                };
            }
        }

        private void langTsCBox_Click(object sender, EventArgs e)
        {
           
        }

        private void langTsCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            webBrower.Load("https://translate.google.cn/#auto/" + langTsCBox.ComboBox.SelectedValue);
            //doTranslate(msgRTBox.Text, webBrower);
        }

       
    }
}
