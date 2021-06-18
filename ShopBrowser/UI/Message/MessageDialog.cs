using Common.Tools;
using CommonData.SysData.Enum;
using ShopeeChat.Shopee;
using ShopeeChat.Shopee.API;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopeeChat
{
    public partial class MessageDialog : Form
    {
        GoogleTrans trans = new GoogleTrans();
        Store curStore;
        ShopCustomerInfo myCurCustomer;
        List<ShopCustomerInfo> cstomers = new List<ShopCustomerInfo>();
        public MessageDialog(Store store,ShopCustomerInfo customer )
        {
            this.curStore = store;
            this.myCurCustomer = customer;
            customerListCBox.Text = customer.to_name;
            this.Text = store.DisplayName + " 发送消息给：" + customer.to_name;
            InitializeComponent();
            initLangSelection();
            TextLogHelper.Instance.SetConsolOut(logRText);
        }
        public MessageDialog(Store store)
        {
            this.curStore = store;
            InitializeComponent();
            initLangSelection();
            TextLogHelper.Instance.SetConsolOut(logRText);
            loadChatUserList();
        }
        public void SetCustomer(Store store, ShopCustomerInfo customer)
        {
            this.curStore = store;
            this.myCurCustomer = customer;
            this.Text =store.DisplayName + " 发送消息给：" + customer.to_name;
        }
        private void transSendBtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("您将使用消息群发功能，开始发送后，将不可撤销，请您确保您的行为符合平台规则，点是，表示您已经悉知平台规则，并自行承担由此带来的后果！点否，退出消息发送！", "用户协议", MessageBoxButtons.YesNo))
            {
                sendMsg(msgRTBox.Text);
            }
        }
        private void sendMsg(string text)
        {
            BackgroundWorker sendBgWorker = new BackgroundWorker();
            sendBgWorker.DoWork += sendBgWorker_DoWork;
            sendBgWorker.RunWorkerCompleted += sendBgWorker_RunWorkerCompleted;
            this.Enabled = false;
            sendBgWorker.RunWorkerAsync(text);

        }
        private void sendBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("消息发送完成!", "信息提示");
            this.Enabled = true;
        }

        private void sendBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string orgText = e.Argument as string;
            ShopCustomerInfo[] custmers = new ShopCustomerInfo[0];
            
            if(mutilChatCkBox.Checked)
            {
                custmers = new ShopCustomerInfo[Math.Min(this.cstomers.Count, (int)msgCountNUD.Value)] ;
                Array.Copy(this.cstomers.ToArray(), custmers, custmers.Length);
            }
            else
            {
                custmers = new ShopCustomerInfo[] { myCurCustomer};
            }
            foreach(ShopCustomerInfo cus in custmers)
            {
                ShopeeAPI api = new ShopeeAPI();
                List<MessageContent> messages = new List<MessageContent>();
                foreach (string tex in orgText.Split('|'))
                {
                    MessageContent msg;
                    string msgTex = tex.Trim();
                    if (msgTex == null || msgTex == "")
                    {
                        continue;
                    }
                    if (msgTex.Trim().StartsWith("[图片]"))
                    {
                        string url = msgTex.Replace("[图片]", "").Trim();
                        if (url.Trim().StartsWith("http"))
                        {
                            msg = new ImageMessageContent(url);
                        }
                        else
                        {
                            Console.WriteLine("错误的图片格式");
                            continue;
                        }
                    }
                    else if (msgTex.Trim().StartsWith("[产品]"))
                    {
                        string productid = msgTex.Replace("[产品]", "").Trim();
                        try
                        {
                            msg = new ProductMessageContent(long.Parse(productid), curStore.ShopInfo.user.shop_id);
                        }
                        catch (Exception xe)
                        {
                            Console.WriteLine("错误的产品ID", xe.Message);
                            continue;
                        }

                    }
                    else
                    {
                        string txt = msgTex.Replace("[username]", cus.to_name);
                        msg = new TextMessageContent(txt);
                    }
                    messages.Add(msg);
                }
                MessageContent lastMsg = messages[messages.Count -1];
                if ((lastMsg is TextMessageContent) && null != cus.latest_message_content && cus.latest_message_content.text.Trim().Contains((lastMsg as TextMessageContent).text.Trim()))
                {
                    Console.WriteLine("当前用户已经发送了相同内容的消息，不再发送");
                    continue;
                }
                foreach(MessageContent msg in messages)
                {
                    Console.WriteLine(curStore.DisplayName + "发送给" + cus.to_name + "消息：" + msg);

                    api.SendMessage(curStore, cus.to_id, msg);
                    if (!AccessControl.Instance.IsLevelRight(UserLevel.VIPUser) && custmers.Length > 1)
                    {
                        Console.WriteLine("******非VIP用户一次只能发送一条消息！");
                        return;
                    }

                }
                Thread.Sleep((int)msgSpanNUD.Value * 1000);
            }
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            if(DialogResult.OK == MessageBox.Show("您将使用消息群发功能，开始发送后，将不可撤销，请您确保您的行为符合平台规则，点是，表示您已经悉知平台规则，并自行承担由此带来的后果！点否，退出消息发送！","用户协议",MessageBoxButtons.YesNo))
            {
                sendMsg(msgRTBox.Text);
            }
        }
        private void transBtn_Click(object sender, EventArgs e)
        {
            if (!msgRTBox.Text.Contains("[username]"))
            {
                msgRTBox.Text = trans.Translate(msgRTBox.Text, ((string)langTsCBox.ComboBox.SelectedValue));
            }
            else
            {
                MessageBox.Show("消息中包含[username],请在翻译完成的结果中再插入[username]!");
            }
           
        }
        void initLangSelection()
        {
            string defaultLang = langCodeNameMap.First(p => p[2] == GroupConfigHelper.Instatce.GetStoreRegion(curStore).RegionID)[0];
            bindLang(langTsCBox, defaultLang);
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

        private void customerListCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            myCurCustomer = customerListCBox.SelectedItem as ShopCustomerInfo;
        }

        private void mutilChatCkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(AccessControl.Instance.IsLevelRight(UserLevel.VIPUser))
            {
                customerListCBox.Enabled = !mutilChatCkBox.Checked;

            }
            else
            {
                MessageBox.Show("当前功能是VIP功能，您暂时无法使用。如需使用，请升级到VIP用户！当前用户仅可以同时给一个买家发送消息","权限不足");
            }
        }
        private void loadChatUserList()
        {
            BackgroundWorker loadOrderBgWorker = new BackgroundWorker();
            loadOrderBgWorker.DoWork += loadOrderBgWorker_DoWork;
            loadOrderBgWorker.RunWorkerCompleted += loadOrderBgWorker_RunWorkerCompleted;
            this.Enabled = false;
            loadOrderBgWorker.RunWorkerAsync();
        }

        private void loadOrderBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            customerListCBox.DataSource = cstomers;
            msgCountNUD.Maximum = cstomers.Count;
        }

        private void loadOrderBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ShopeeAPI api = new ShopeeAPI();
            cstomers.Clear();
            cstomers = api.GetCustomerList(curStore, -1);
        }

        private void msgSpanNUD_ValueChanged(object sender, EventArgs e)
        {
            if(msgSpanNUD.Value < 15)
            {
                MessageBox.Show("当前间隔过小，请小心核对参数！","系统提示");
            }
        }

        private void picTSMI_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();
            if(of.FileName != null && of.FileName != "")
            {
                ShopeeAPI aPI = new ShopeeAPI();
                string url = aPI.UploadMessageImage(curStore, of.FileName);
                if(url != "")
                {
                    string picTxt = " |[图片]" + url + "| ";
                    msgRTBox.Text = msgRTBox.Text.Insert(msgRTBox.SelectionStart, picTxt);
                    msgRTBox.SelectionStart = msgRTBox.SelectionStart + picTxt.Length;
                }

            }
            //msgRTBox.SelectionStart;
        }

        private void itemTSMI_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请在当前位置输入你的产品ID,用产品ID替换XXXXX");
            string picTxt = " |[产品]XXXXXX| ";
            msgRTBox.Text = msgRTBox.Text.Insert(msgRTBox.SelectionStart, picTxt);
            msgRTBox.SelectionStart = msgRTBox.SelectionStart + picTxt.Length-1;
        }
    }
}
