
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
    public partial class KeywordFollowForm : Form
    {
        StoreRegion region;
        Store store;
        StoreWebBrowser webBrower;
        bool quitThread = false;


        public KeywordFollowForm(StoreRegion region, Store store)
        {
            this.region = region;
            this.store = store;
            InitializeComponent();
            Shopname.Text = store.DisplayName;
            TextLogHelper.Instance.SetConsolOut(logTxtRTBox);
        }

        private void followBtn_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            this.Enabled = false;
            ThreadPool.QueueUserWorkItem(new WaitCallback(searchBuyer));
        }
        delegate void addNewRowDelte( BuyerBriefInfo buyerInfo, string userid, string time, string icoUR);
        void addNewRow(BuyerBriefInfo buyerInfo,string userid, string time,string icoURL)
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke(new addNewRowDelte(addNewRow), buyerInfo, userid, time, icoURL);
            }
            else
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = Tool.GetUTCDateTime(buyerInfo.last_active_time).ToString();
                this.dataGridView1.Rows[index].Cells[0].Tag = buyerInfo;
                this.dataGridView1.Rows[index].Cells[1].Value = userid.Substring(0,2) +"****";
                this.dataGridView1.Rows[index].Cells[2].Value = time;
                //this.dataGridView1.Rows[index].Cells[3].Value = buyerInfo.chat_disabled?"不想说话":"打声招呼";
                //this.dataGridView1.Rows[index].Cells[3].Tag = buyerInfo;
                string labelstr = buyerInfo.account.is_seller ? (buyerInfo.buyer_rating == null ?"内地卖家":("本地卖家，买家评分：" + buyerInfo.buyer_rating.rating_star.ToString() + ";共下单：" + buyerInfo.buyer_rating.rating_count[5] + "次"))
                                                              : (buyerInfo.buyer_rating == null ? "新买家,没有购买记录" : ("买家评分：" + buyerInfo.buyer_rating.rating_star.ToString() + ";共下单：" + buyerInfo.buyer_rating.rating_count[5] + "次"));
                this.dataGridView1.Rows[index].Cells[3].Value = labelstr;
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
                if ((int)messageNumNUD.Value > 0)
                {
                    progressBar1.Value = Math.Min(((int)(success + failure) * 100) / (int)messageNumNUD.Value + 1, 100);
                }
                
            }
        }
        private bool followValidUser(long shopid)
        {
            bool ret = false;
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
                addNewRow(buyerIinfo, buyerIinfo.account.username, dt.ToString(), "https://cf.shopee.tw/file/" + buyerIinfo.account);
                ret = api.FollowUser(store, buyerIinfo.userid.ToString(), buyerIinfo.shopid.ToString());
                //if(ret)
                //{
                //    sucess = 1;
                //    failure = 0;
                //}
                //else
                //{
                //    sucess = 0;
                //    failure = 1;
                //}

            }
            //else
            //{
            //    //Thread.Sleep(1500);
            //    if (null != buyerIinfo)
            //    {
            //        Console.WriteLine(buyerIinfo.account.username + "不符合筛选条件，不放入列表中.");
            //    }
            //    sucess = 0;
            //    failure = 0;
            //}
            return ret;
        }
        delegate void threadCompleted(object state);
        private List<UserDetailInfo> getFollowingList(long shopid, List<UserDetailInfo> hisList = null)
        {
            ShopeeAPI api = new ShopeeAPI();
            List<UserDetailInfo> followingUsers = new List<UserDetailInfo>();
            BuyerBriefInfo buyerIinfo = api.GetBuyerBriefInfo(store, shopid);
            if(null != buyerIinfo)
            {
                int remainNum = buyerIinfo.account.following_count;
                int loop = remainNum / 100 + 1;
                int offset = 0;
                while (remainNum > 0 && loop >= 0)
                {
                    try
                    {
                        List<UserDetailInfo> users = api.GetFollowingList(store, shopid, 100, offset);
                        followingUsers.AddRange(users);

                        remainNum -= users.Count;
                        loop--;
                        offset += users.Count;

                        if (null != hisList)
                        {
                            int sameNum = 0;
                            foreach (UserDetailInfo ui in users)
                            {
                                if (hisList.Contains(ui))
                                {
                                    sameNum++;
                                    if (sameNum > 5)
                                    {
                                        loop = -1;
                                        Console.WriteLine("已经到达历史列表顶端，不在重新获取");
                                        break;
                                    }
                                }
                                else
                                {
                                    sameNum = 0;
                                }
                            }

                        }
                    }
                    catch (Exception xe)
                    {
                        Console.WriteLine("获取粉丝列表错误：" + xe.Message);
                    }
                }
                Console.WriteLine("获取粉丝列表：" + followingUsers.Count + "/应:" + buyerIinfo.follower_count);
            }
            
            
            return followingUsers;
        }
        
        private void searchBuyer(object state)
        {
            //保存的历史列表
            //List<UserDetailInfo> followingUsers = followingUsers = getStoreFollowingHistoryList();
            //Console.WriteLine("关注开始：" + followingUsers.Count);
            //取消关注
            //unfollowUser(followingUsers, (int)messageNumNUD.Value);

            int success = 0;
            int failure = 0;
            int continuedFailure = 0;
            quitThread = false;
            ShopeeAPI api = new ShopeeAPI();

            List<long> shopids = new List<long>();
            SearchedProductInfo info = api.GetProductInfoByKeyword(region, store, keyWordTBox.Text);
            //resultRTBox.Text = info.items.Count().ToString() + Environment.NewLine;
            int i = 0;
            //待关注的用户信息
            List<UserDetailInfo> userinfos = new List<UserDetailInfo>();

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
                    if (!shopids.Contains(rating.author_shopid))// && null == followingUsers.FirstOrDefault(p => p.shopid == rating.author_shopid)
                    {
                        shopids.Add(rating.author_shopid);
                        bool ret = followValidUser(rating.author_shopid);
                        success += ret ? 1 : 0;
                        failure += ret ? 0 : 1;
                        Console.WriteLine("关注用户：" + rating.author_username + (ret ? "成功" : "失败"));
                        setShow(success, failure);
                        if (ret)
                        {
                            continuedFailure = 0;
                        }
                        else
                        {
                            continuedFailure++;
                        }
                        if (success + failure >= messageNumNUD.Value || continuedFailure > 10)
                        {
                            quitThread = true;
                        }
                        Thread.Sleep((int)sendSpanNUD.Value * 1000);
                        if (success + failure > 20 && !AccessControl.Instance.IsLevelRight(UserLevel.VIPUser))
                        {
                            Console.WriteLine("每次只能关粉20个，非会员关粉结束！会员每次关粉可最大1000");
                            quitThread = true;
                            //return;
                        }
                    }
                }
                //关注这个产品对应商店的粉丝
                //BuyerBriefInfo buyerIinfo = api.GetBuyerBriefInfo(store, item.shopid);
                //if (null != buyerIinfo)
                //{
                //    int remainNum = buyerIinfo.follower_count;
                //    int loop = remainNum / 100 + 1;
                //    int offset = 0;
                //    while (remainNum > 0 && loop >= 0)
                //    {
                //        //获得前100个粉丝
                //        List<UserDetailInfo> users = api.GetFollowerList(store, item.shopid, 100, offset);
                //        foreach (UserDetailInfo user in users)
                //        {
                //            if (!shopids.Contains(user.shopid) && null == followingUsers.FirstOrDefault(p => p.shopid == user.shopid))
                //            {
                //                shopids.Add(user.shopid);
                //                bool ret = followValidUser(user.shopid);
                //                Console.WriteLine("关注用户：" + user.username + (ret ? "成功" : "失败"));
                //                success += ret ? 1 : 0;
                //                failure += ret ? 0 : 1;
                //                setShow(success, failure);

                //                if (i >= messageNumNUD.Value || failure > 10)
                //                {
                //                    quitThread = true;
                //                }
                //                Thread.Sleep((int)sendSpanNUD.Value * 1000);
                //                //if (success + failure > 20 && !AccessControl.Instance.IsLevelRight(UserLevel.VIPUser))
                //                //{
                //                //    Console.WriteLine("非会员关粉结束！");
                //                //    return;
                //                //}
                //            }
                //            if (success + failure > messageNumNUD.Value)
                //            {
                //                break;
                //            }
                //        }
                //        remainNum -= users.Count;
                //        loop--;
                //        offset += users.Count;
                //        if (success + failure > messageNumNUD.Value)
                //        {
                //            break;
                //        }
                //    }
                //    if (success + failure > messageNumNUD.Value)
                //    {
                //        break;
                //    }
                //}

                Thread.Sleep(1500);
            }

            //关键词店铺
            //if ((success + failure) < messageNumNUD.Value && AccessControl.Instance.IsLevelRight(UserLevel.VIPUser))
            //{
            //    SearcheShopInfos shopinfos = api.GetKeyWordShopInfo(region, store, keyWordTBox.Text);
            //    if(shopinfos.users != null)
            //    {
            //        foreach(SearcheShopInfo shopinfo in shopinfos.users)
            //        {
            //                 int remainNum = shopinfo.follower_count;
            //                int loop = remainNum / 100 + 1;
            //                int offset = 0;
            //                while (remainNum > 0 && loop >= 0)
            //                {
            //                    List<UserDetailInfo> users = api.GetFollowerList(store, shopinfo.shopid, 100, offset);
            //                    foreach (UserDetailInfo user in users)
            //                    {
            //                        if (!shopids.Contains(user.shopid) && null == followingUsers.FirstOrDefault(p => p.shopid == user.shopid))
            //                        {
            //                            shopids.Add(user.shopid);
            //                            bool ret = followValidUser(user.shopid);
            //                            Console.WriteLine("关注用户：" + user.username + (ret ? "成功" : "失败"));
            //                            success += ret ? 1 : 0;
            //                            failure += ret ? 0 : 1;
            //                            setShow(success, failure);
            //                            if (success >= messageNumNUD.Value || failure > 10)
            //                            {
            //                                quitThread = true;
            //                            }
            //                            Thread.Sleep((int)sendSpanNUD.Value * 1000);
            //                            if (success + failure > 20 && !AccessControl.Instance.IsLevelRight(UserLevel.VIPUser))
            //                            {
            //                                Console.WriteLine("非会员关粉结束！");
            //                                return;
            //                            }
            //                        }
            //                        if (success + failure > messageNumNUD.Value)
            //                        {
            //                            break;
            //                        }
            //                    }
            //                    remainNum -= users.Count;
            //                    loop--;
            //                    offset += users.Count;
            //                    if (success + failure > messageNumNUD.Value)
            //                    {
            //                        break;
            //                    }
            //                }
            //                if (success + failure > messageNumNUD.Value)
            //                {
            //                    break;
            //                }
            //        }
            //    }
            //}
            
            try
            {
                this.Invoke(new threadCompleted(followCompleted), state);
            }
            catch (Exception ex)
            {
                Console.WriteLine("搜索用户:" + ex.Message);
            }
            //saveStoreFollowingHistoryList(followingUsers);
        }
       
        //保留此段代码，前端点赞可用
        //private void followUser(object state)
        //{
        //    int success = 0;
        //    int failure = 0;
        //    interAction.InitHandler((result) =>
        //    {
        //        //Dictionary<string, string> ret = (Dictionary<string, string>)result;
        //        //if (result is Dictionary<string,string>)
        //        {
        //            int j = (result.ToString().ToLower().Contains("success") && result.ToString().ToLower().Contains("true")) ? success++ : failure++;
        //            setShow(success, failure);
        //        }
        //    });
        //    String js = Resources.followjs;

        //    ShopeeAPI api = new ShopeeAPI();
        //    SearchedProductInfo info = api.GetProductInfoByKeyword(region, store, keyWordTBox.Text);
        //    //resultRTBox.Text = info.items.Count().ToString() + Environment.NewLine;
        //    int i = 0;
        //    foreach (ProductItem item in info.items)
        //    {
        //        if (quitThread)
        //        {
        //            break;
        //        }
        //        RatingCustomInfos ratings = api.GetRatingCustoms(region, store, item.shopid, item.itemid, item.item_rating.rating_count[0]);
        //        foreach (RatingItem rating in ratings.data.ratings)
        //        {
        //            if (quitThread)
        //            {
        //                break;
        //            }
        //            string url = string.Format(commondPartern, rating.author_shopid);
        //            string fjs = js.Replace("%{url}%", url).Replace("%{csrf}%", data);
        //            webBrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync(fjs);
        //            long jsTimeStamp = rating.mtime;
        //            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
        //            DateTime dt = startTime.AddSeconds(jsTimeStamp);

        //            addNewRow(rating,rating.author_username, dt.ToString(), "https://cf.shopee.tw/file/"+rating.author_portrait);
        //            Thread.Sleep(1000*(new Random()).Next(2, 5));
        //            if ((success + failure) % 5 == 0 && (success + failure) != 0)
        //            {
        //                webBrower.Load(webBrower.Address);
        //                Thread.Sleep(5000);
        //            }
        //            if (++i >= followNumNUD.Value)
        //            {
        //                quitThread = true;
        //            }
        //        }
        //    }
        //    try
        //    {
        //        this.Invoke(new threadCompleted(followCompleted), state);
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine("添加关注:" + ex.Message);
        //    }
        //}
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                if(e.RowIndex > 5)
                {
                    return;
                }
                RatingItem i = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as RatingItem;
                ShopeeAPI api = new ShopeeAPI();
                api.SendMessageToUserId(store,i.userid,"Hi,How are you!");
            }
            
        }

        private void unfollowBtn_Click(object sender, EventArgs e)
        {
            BackgroundWorker unFollowBgWork = new BackgroundWorker();
            unFollowBgWork.DoWork += unFollowBgWork_DoWork;
            unFollowBgWork.RunWorkerCompleted += unFollowBgWork_RunWorkerCompleted;
            unFollowBgWork.RunWorkerAsync();
            this.Enabled = false;
        }

        private void unFollowBgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
        }
        
        private void unFollowBgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            //保存的历史列表
            List<UserDetailInfo> userinfos = new List<UserDetailInfo>();
            if (e.Argument != null &&  e.Argument is List<UserDetailInfo>)
            {
                userinfos = e.Argument as List<UserDetailInfo>;
            }
            else
            {
                userinfos = getStoreFollowingHistoryList();
            }
            Console.WriteLine("取消关注开始：" + userinfos.Count);
            int unFollowNum = unfollowUser(userinfos);
            Console.WriteLine("取消关注成功：" + unFollowNum);
            saveStoreFollowingHistoryList(userinfos);
        }
        private int  unfollowUser(List<UserDetailInfo> userinfos,int count = 300)
        {
            int unFollowNum = 0;
            foreach (UserDetailInfo user in userinfos)
            {
                if (user.IsFollowed && upfollowInvalidUser(store, user.shopid))
                {
                    unFollowNum++;
                    user.IsFollowed = false;
                }
            }

            ShopeeAPI api = new ShopeeAPI();
            for (int i = userinfos.Count - 1; i >= 0 && unFollowNum < count && userinfos.Count > 2000; i--)
            {
                if (userinfos[i].IsFollowed)
                {
                    bool rt = api.UnFollowUser(store, userinfos[i].id.ToString(), userinfos[i].shopid.ToString());
                    if (rt)
                    {
                        userinfos[i].IsFollowed = false;
                        unFollowNum++;
                    }
                }

            }

            return unFollowNum;
        }
        private List<UserDetailInfo> getStoreFollowingHistoryList()
        {
            string file = BrowerHelper.Instatce.GetCacheDir(store) + "\\" + store.ShopInfo.user.shop_id + ".fli";

            long shopid = store.ShopInfo.user.shop_id;
            //保存的历史列表
            List<UserDetailInfo> userinfos = Tool.LoadSerializationFromFile<List<UserDetailInfo>>(file);
            if(userinfos == null)
            {
                userinfos = new List<UserDetailInfo>();
            }

            foreach (UserDetailInfo user in getFollowingList(shopid, userinfos))
            {
                if (null == userinfos.FirstOrDefault(p => p.shopid == user.shopid))
                {
                    userinfos.Add(user);
                }
            }
            //Tool.SaveSerializationToFile<List<UserDetailInfo>>(file, userinfos);
            return userinfos;
        }
        private void  saveStoreFollowingHistoryList(List<UserDetailInfo> userinfos )
        {
            string file = BrowerHelper.Instatce.GetCacheDir(store) + "\\" + store.ShopInfo.user.shop_id + ".fli";
            Tool.SaveSerializationToFile<List<UserDetailInfo>>(file, userinfos);
            //return userinfos;
        }
        private bool upfollowInvalidUser(Store store, long shopid)
        {
            bool ret = false;
            ShopeeAPI api = new ShopeeAPI();
            BuyerBriefInfo buyerIinfo = api.GetBuyerBriefInfo(store, shopid);
            if (buyerIinfo != null
                            //|| buyerIinfo.chat_disabled 
                            &&
                            (
                              (buyerIinfo.account.is_seller && buyerIinfo.buyer_rating == null)
                              ||
                              ((DateTime.Now - Tool.GetUTCDateTime(buyerIinfo.last_active_time)).TotalDays > 60)
                            )
                  )
            {
                ret = api.UnFollowUser(store, buyerIinfo.userid.ToString(), buyerIinfo.shopid.ToString());
            }
            return ret;
        }
    }
}
