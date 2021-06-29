using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using Common.Brower;
using Common.Browser;
using ShopeeChat.Shopee;
using ShopeeChat.SysData;
using ShopeeChat.Tools;

namespace ShopeeChat
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
//#if x86
            AutoUpdater.Start("http://www.bjepower.com/ShopeeChatPlus/update.xml");
            AutoUpdater.Mandatory = true;
            AutoUpdater.UpdateMode = Mode.ForcedDownload;

            //#endif
            ////#if x64
            ////            AutoUpdater.Start("http://www.bjepower.com/ShopeeChatPlus/update64.xml");
            //#endif

#if TEST
            if(true)
#else
            if (false)
#endif
            {
                //Application.Run(new WebsockForm());
                //Application.Run(new ChatForm("yf", "123456"));
                MachineInfoHelper.Initilize();
                AccessControl.Instance.Initialize();
                AccessControl.Instance.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                Application.Run(new APITestForm());
            }
            else
            {
                MachineInfoHelper.Initilize();
                AccessControl.Instance.Initialize();
                AccessControl.Instance.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                LogInForm logForm = new LogInForm("登录");
                DialogResult result;

                bool normalQuite = false;
                while(!normalQuite)
                {
                    try
                    {
                        result = logForm.ShowDialog();
                        if (logForm.IsLogSuccess)
                        {
                            if (logForm.IsClearCache)
                            {
                                BrowerHelper.ClearCache();
                            }
                            GroupConfigHelper.Instatce.Initialize(logForm.UserName);
                            BrowerHelper.Instatce.Initialize(logForm.UserName);
                            GroupConfigHelper.Instatce.StartStoreInfoTask();
                            Application.Run(new MainForm(logForm.UserName));
                        }
                        normalQuite = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    GroupConfigHelper.Instatce.StopStoreInfoTask();
                }
            }
        }
    }
}
