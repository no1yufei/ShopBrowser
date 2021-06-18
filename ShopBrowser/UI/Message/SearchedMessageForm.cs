using CefSharp.WinForms;
using Common.Brower;
using Common.Browser;
using Common.Shopee.API.Data;
using Common.Tools;
using CommonData.SysData.Enum;
using ShopBrowser.Properties;
using ShopeeChat.Shopee;
using ShopeeChat.Shopee.API;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopeeChat
{
    public partial class SearchedMessageForm : Form
    {
        StoreRegion region;
        Store store;
        StoreWebBrowser webBrower;
        bool quitThread = false;
        public SearchedMessageForm(StoreRegion region, Store store)
        {
            this.region = region;
            this.store = store;
            InitializeComponent();
            Shopname.Text = store.UserName;
            TextLogHelper.Instance.SetConsolOut(logTxtRTBox);
        }

        private void followBtn_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            this.Enabled = false;
            ThreadPool.QueueUserWorkItem(new WaitCallback(searchBuyer));
        }
        delegate void addNewRowDelte( BuyerBriefInfo buyerInfo, RatingItem rating, string userid, string time, string icoUR);
        void addNewRow(BuyerBriefInfo buyerInfo,RatingItem rating, string userid, string time,string icoURL)
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke(new addNewRowDelte(addNewRow), buyerInfo, rating,userid, time, icoURL);
            }
            else
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Tag = buyerInfo;
                this.dataGridView1.Rows[index].Cells[0].Value = Tool.GetUTCDateTime(buyerInfo.last_active_time).ToString();
                this.dataGridView1.Rows[index].Cells[0].Tag = buyerInfo;
                this.dataGridView1.Rows[index].Cells[1].Value = userid.Substring(0,2) +"****";
                this.dataGridView1.Rows[index].Cells[2].Value = time;
                this.dataGridView1.Rows[index].Cells[3].Value = buyerInfo.chat_disabled?"不想说话":"打声招呼";
                this.dataGridView1.Rows[index].Cells[3].Tag = rating;
                string labelstr = buyerInfo.account.is_seller ? (buyerInfo.buyer_rating == null ?"内地卖家":("本地卖家，买家评分：" + buyerInfo.buyer_rating.rating_star.ToString() + "共下单：" + buyerInfo.buyer_rating.rating_count[5] + "次"))
                                                              : (buyerInfo.buyer_rating == null ? "新买家,没有购买记录" : ("买家评分：" + buyerInfo.buyer_rating.rating_star.ToString() + "共下单：" + buyerInfo.buyer_rating.rating_count[5] + "次"));
                this.dataGridView1.Rows[index].Cells[4].Value = labelstr;
            }

        }
        delegate void setShowDelete(int success, int failure);
        private void setShow(int success, int failure)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new setShowDelete(setShow), success, failure);
            }
            else
            {
                progressBar1.Value = ((int)(success + failure) * 100) / (int)messageNumNUD.Value + 1;
            }
        }
        private bool addValidUser(long shopid,RatingItem rating)
        {
            ShopeeAPI api = new ShopeeAPI();
            BuyerBriefInfo buyerIinfo = api.GetBuyerBriefInfo(store, shopid);
            if (buyerIinfo != null
                //|| buyerIinfo.chat_disabled 
                && 
                ((buyerCkBox.Checked && !buyerIinfo.account.is_seller)
                || (localSellerCkBox.Checked && buyerIinfo.account.is_seller && buyerIinfo.buyer_rating != null)
                || (chineseCkBox.Checked && buyerIinfo.account.is_seller && buyerIinfo.buyer_rating == null)
                )
                && (DateTime.Now - Tool.GetUTCDateTime(buyerIinfo.last_active_time)).TotalDays < 30)
            {
                long jsTimeStamp = buyerIinfo.mtime;
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
                DateTime dt = startTime.AddSeconds(jsTimeStamp);
                addNewRow(buyerIinfo, rating, buyerIinfo.account.username, dt.ToString(), "https://cf.shopee.tw/file/" + buyerIinfo.account);

                //if (failure > 10)
                //{
                //    bool ret = api.FollowUser(store, buyerIinfo.userid.ToString(), buyerIinfo.shopid.ToString());
                //    int count = ret ? success++ : failure++;
                //    setShow(success, failure);
                //}
                return true;
            }
            else
            {
                //Thread.Sleep(1500);
                if (null != buyerIinfo)
                {
                    Console.WriteLine(buyerIinfo.account.username + "不符合筛选条件，不放入列表中.");
                }
            }
            return false;
        }
        delegate void threadCompleted(object state);
        private void searchBuyer(object state)
        {
            int success = 0;
            int failure = 0;
            quitThread = false;
                       ShopeeAPI api = new ShopeeAPI();
            SearchedProductInfo info = api.GetProductInfoByKeyword(region, store, keyWordTBox.Text);
            //resultRTBox.Text = info.items.Count().ToString() + Environment.NewLine;
            int i = 0;
            //待关注的用户信息
            List<UserDetailInfo> userinfos = new List<UserDetailInfo>();
            List<long> shopids = new List<long>();
            //留评买家
            foreach (ProductItem item in info.items)
            {
                if (quitThread)
                {
                    break;
                }
                RatingCustomInfos ratings = api.GetRatingCustoms(region, store, item.shopid, item.itemid, item.item_rating.rating_count[0]);
                foreach (RatingItem rating in ratings.data.ratings)
                {
                    if (quitThread)
                    {
                        break;
                    }
                    if(!shopids.Contains(rating.author_shopid))
                    {
                        shopids.Add(rating.author_shopid);
                        i= addValidUser(rating.author_shopid,rating)?i+1:i;
                        if (i >= messageNumNUD.Value)
                        {
                            quitThread = true;
                        }
                        Thread.Sleep(1500);
                    }
                }

                BuyerBriefInfo buyerIinfo = api.GetBuyerBriefInfo(store, item.shopid);
                if(null != buyerIinfo)
                {
                    int remainNum = buyerIinfo.follower_count;
                    int loop = remainNum / 100 + 1;
                    int offset = 0;
                    while (remainNum > 0 && loop >= 0)
                    {
                        List<UserDetailInfo> users = api.GetFollowerList(store, item.shopid, 100, offset);
                        foreach (UserDetailInfo user in users)
                        {
                            if (!shopids.Contains(user.shopid))
                            {
                                shopids.Add(user.shopid);
                                i = addValidUser(user.shopid, null) ? i + 1 : i;
                                if (shopids.Count >= messageNumNUD.Value)
                                {
                                    quitThread = true;
                                    break;
                                }
                                Thread.Sleep(1500);
                            }
                        }
                        if (shopids.Count >= messageNumNUD.Value)
                        {
                            quitThread = true;
                            break;
                        }
                        remainNum -= users.Count;
                        loop--;
                        offset += users.Count;
                    }
                    if (shopids.Count > messageNumNUD.Value)
                    {
                        quitThread = true;
                        break;
                    }
                }
                
                Thread.Sleep(1500);
            }
            
        
            try
            {
                this.Invoke(new threadCompleted(followCompleted), state);
            }
            catch (Exception ex)
            {
                Console.WriteLine("搜索用户:" + ex.Message);
            }
        }
     
        private void followCompleted(object sate)
        {
            this.Enabled = true;
            progressBar1.Value = 100;
            messageNumNUD.Maximum = dataGridView1.Rows.Count;
            messageNumNUD.Value = dataGridView1.Rows.Count;
        }
       

        private void FollowForm_Load(object sender, EventArgs e)
        {
            ShopeeAPI api = new ShopeeAPI();
            BuyerBriefInfo info = api.GetBuyerBriefInfo(store, store.UserName);
            if(null != info)
            {
                followingLabel.Text = info.account.following_count.ToString();
                followerLabel.Text = info.follower_count.ToString();
            }
        }

        private void FollowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            quitThread = true;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (dataGridView1.Columns[e.ColumnIndex].Name.ToLower().Equals("imagecol"))
            {
                string path = e.Value.ToString();
               // e.Value = downloadImage(path);
            }
        }
        
        System.Drawing.Image downloadImage(String url)
        {
            System.Drawing.Image result = null;
            try
            {

                using (Stream imgStream = System.Net.WebRequest.Create(url).GetResponse().GetResponseStream())
                {
                    result = System.Drawing.Image.FromStream(imgStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }


        private void sendMsgBtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("您将使用消息群发功能，开始发送后，将不可撤销，请您确保您的行为符合平台规则，点是，表示您已经悉知平台规则，并自行承担由此带来的后果！点否，退出消息发送！", "用户协议", MessageBoxButtons.YesNo))
            {
                if(dataGridView1.Rows.Count > 0)
                {
                    sendMsg(msgRTBox.Text);
                }
                else
                {
                    MessageBox.Show("请先通过关键词搜索出希望发送消息的买家！");
                }
            }
        }
        private void sendMsg(string text)
        {
            BackgroundWorker sendBgWorker = new BackgroundWorker();
            sendBgWorker.DoWork += sendBgWorker_DoWork;
            sendBgWorker.RunWorkerCompleted += sendBgWorker_RunWorkerCompleted;
            sendBgWorker.ProgressChanged += sendBgWorker_ProgressChanged;
            sendBgWorker.WorkerReportsProgress = true;
            this.Enabled = false;
            sendBgWorker.RunWorkerAsync(text);

        }

        private void sendBgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void sendBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("消息发送完成!", "信息提示");
            this.Enabled = true;
            progressBar1.Value = 100;
        }

        private void sendBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string msg = e.Argument as string;
            BackgroundWorker bgwork = sender as BackgroundWorker;
            int num = 0;
            foreach (DataGridViewRow  row in dataGridView1.Rows)
            {
                //if(row.Cells[3].Value !="打声招呼")
                //{
                //    continue;
                //}
                num++;
                BuyerBriefInfo buyerInfo = row.Cells[0].Tag as BuyerBriefInfo;
                sendMsgToUser(store, buyerInfo, msg);
                Console.WriteLine(store.DisplayName + "发送给" + buyerInfo.account.username + "消息：" + msg);
                bgwork.ReportProgress((int)(num / (dataGridView1.Rows.Count * 1.0) * 100));
                Thread.Sleep((int)sendSpanNUD.Value * 1000);
                if (!AccessControl.Instance.IsLevelRight(UserLevel.VIPUser) && dataGridView1.Rows.Count > 1)
                {
                    Console.WriteLine("******非VIP用户一次只能发送一条消息！");
                    return;
                }
            }
        }
        private void sendMsgToUser(Store store, BuyerBriefInfo buyerInfo, string msgTxt)
        {
            ShopeeAPI api = new ShopeeAPI();
            List<MessageContent> messages = new List<MessageContent>();
            foreach (string tex in msgTxt.Split('|'))
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
                        msg = new ProductMessageContent(long.Parse(productid), store.ShopInfo.user.shop_id);
                    }
                    catch (Exception xe)
                    {
                        Console.WriteLine("错误的产品ID", xe.Message);
                        continue;
                    }
                }
                else
                {
                    string txt = msgTex.Replace("[username]", buyerInfo.account.username);
                    msg = new TextMessageContent(txt);
                }
                messages.Add(msg);
            }
            
            foreach (MessageContent msg in messages)
            {
                Console.WriteLine(store.DisplayName + "发送给" + buyerInfo.account.username + "消息：" + msg);
                api.SendMessage(store, buyerInfo.userid, msg);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if(null != dataGridView1.Rows[e.RowIndex].Tag)
                {
                    BuyerBriefInfo buyerInfo = dataGridView1.Rows[e.RowIndex].Tag as BuyerBriefInfo;
                    sendMsgToUser(store, buyerInfo, msgRTBox.Text);
                    Console.WriteLine(store.DisplayName + "发送给" + buyerInfo.account.username + "消息：" + msgRTBox.Text);
                }
                else
                {
                    Console.WriteLine("当前用户关闭陌人生聊天功能，不能进行对话！");
                }
            }

        }

        private void picTSMI_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();
            if (of.FileName != null && of.FileName != "")
            {
                ShopeeAPI aPI = new ShopeeAPI();
                string url = aPI.UploadMessageImage(store, of.FileName);
                if (url != "")
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
            msgRTBox.SelectionStart = msgRTBox.SelectionStart + picTxt.Length - 1;
        }
    }
}
