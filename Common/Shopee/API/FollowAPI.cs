using Common.Shopee.API.Data;
using CsharpHttpHelper;
using HtmlAgilityPack;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
        public RatingCustomInfos GetRatingCustoms(StoreRegion region, Store store, long itemShopId, long itemID, int count)
        {
            RatingCustomInfos customInfo = new RatingCustomInfos();
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = region.GetBuyerUrl() + String.Format("/api/v2/item/get_ratings?filter=0&flag=1&itemid={0}&limit={1}&offset=0&shopid={2}&type=0", 
                itemID,Math.Min(count,50), itemShopId);

            HttpResult result = hhh.Get(msgQuestStr);
            if (result.Header != null)
            {
                Console.Write(result.Html);
                customInfo = RatingCustomInfos.DataJson(result.Html);
            }
            if (customInfo.data.ratings.Length <= 0)
            {
                Console.WriteLine(result.Html);
            }

            return customInfo;
        }
        public SearcheShopInfos GetKeyWordShopInfo(StoreRegion region, Store store, String keyword)
        {
            SearcheShopInfos shopInfos = new SearcheShopInfos();
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = region.GetBuyerUrl() + String.Format("/api/v2/search_users/?keyword={0}&limit=50&offset=0&page=search_user&with_search_cover=true", keyword.Trim().Replace(" ", "%20"));

            HttpResult result = hhh.Get(msgQuestStr);
            if (result.Header != null)
            {
                VersionMessage<SearcheShopInfos> msgData = VersionMessage<SearcheShopInfos>.FromJson(result.Html);
                if(null != msgData && 0==msgData.error && null != msgData.data)
                {
                    shopInfos = msgData.data;
                }
                
            }
            if (shopInfos.users == null || shopInfos.users.Length <= 0)
            {
                Console.WriteLine("关键词店铺搜索失败:"+ result.Html);
            }

            return shopInfos;
        }
        public SearchedProductInfo GetProductInfoByKeyword(StoreRegion region, Store store, String keyword)
        {
            SearchedProductInfo customInfo = new SearchedProductInfo();
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = region.GetBuyerUrl() + String.Format("/api/v2/search_items/?by=relevancy&keyword={0}&limit=50&newest=0&order=desc&page_type=search", keyword.Trim().Replace(" ", "%20"));

            HttpResult result = hhh.Get(msgQuestStr);
            if (result.Header != null)
            {
                Console.WriteLine(result.Html);
                customInfo = SearchedProductInfo.DataJson(result.Html);
            }
            if (customInfo.items.Count() <= 0)
            {
                Console.WriteLine(result.Html);
            }

            return customInfo;
        }

        public bool FollowUser(Store store, string UserId, String ShopId)
        {
            if (store.Token != null && store.Token != string.Empty)
            {
                //https://seller.xiapi.shopee.cn/api/v2/users/44012663/?SPC_CDS=3d007cc9-8878-4bef-98fe-3dfa2f5d743f&SPC_CDS_VER=2
                //https://seller.shopee.sg/api/v3/settings/follow_shop/?SPC_CDS=3cf76b56-b1dc-4f6c-be07-099c6570551f&SPC_CDS_VER=2

                Guid SPC_CDS = store.SPC_CDS;
                //String data = "{\"user\":{\"followed\":1,\"shopid\":" + ShopId + ",\"user_command\":\"follow\"} }";
                //string querURL = store.ServerURL + "/api/v2/users/" + UserId + "/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                string querURL = store.ServerURL + "/api/v3/settings/follow_shop/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                string data = "{\"is_follow\": true, \"target_shop_id\":" + ShopId + "}";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Post(querURL, data);
                //Console.WriteLine("关注：" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    Console.WriteLine("关注成功!");
                    return true;
                }
                if (spcresult.Html.Contains("ps_basicservice_error_10006"))
                {
                    Console.WriteLine("关注人数已经达到上限，请取消关注后再试。");
                }
                Console.WriteLine("错误信息：" + spcresult.Html);
            }
            return false;
        }
        public bool UnFollowUser(Store store, string UserId, String ShopId)
        {
            if (store.Token != null && store.Token != string.Empty)
            {
                //https://seller.xiapi.shopee.cn/api/v2/users/44012663/?SPC_CDS=3d007cc9-8878-4bef-98fe-3dfa2f5d743f&SPC_CDS_VER=2
                Guid SPC_CDS = store.SPC_CDS;
                //String data = "{\"user\":{\"followed\":0,\"shopid\":" + ShopId + ",\"user_command\":\"follow\"} }";
                //string querURL = store.ServerURL + "/api/v2/users/" + UserId + "/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";

                string querURL = store.ServerURL + "/api/v3/settings/follow_shop/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                string data = "{\"is_follow\": false, \"target_shop_id\":" + ShopId + "}";

                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Post(querURL, data);
                //Console.WriteLine("关注：" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    Console.WriteLine("SPC 取消关注成功!");
                    return true;
                }
                Console.WriteLine("关注返回:" + spcresult.Html);
            }
            return false;
        }
        public ShopFollowerInfo GetFollowInfo(StoreRegion region, Store store)
        {
            ShopFollowerInfo finfo = new ShopFollowerInfo();
            if (store.Token != null && store.Token != string.Empty && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            {
                //https://seller.xiapi.shopee.cn/api/v2/shops/34796202/?SPC_CDS=6e0cb4ed-7ac0-4f49-8f6d-f0ad30361d17&SPC_CDS_VER=2
                String data = "{\"shop_ids\":[" + store.ShopInfo.user.shop_id + "]}";

                string querURL = region.GetBuyerUrl() + "/api/v1/shops/";
                HtmlHttpHelper hhh = new HtmlHttpHelper();
                HttpResult spcresult = hhh.Post(querURL, data);
                if (spcresult.Html != null && spcresult.Html.Contains("username"))
                {
                    finfo = ShopFollowerInfo.FromJson(spcresult.Html);
                    Console.WriteLine("关粉信息成功!");
                }
                Console.WriteLine("更新关粉信息:" + spcresult.Html);
            }
            return finfo;
        }

        public List<UserDetailInfo> GetFollowerList(Store store, long shopid, int count, int offset = 0)
        {

            String url = StoreRegionMap.GetBuyerURL(store.RegionID); ;

            int limit = count;
            List<UserDetailInfo> userinfos = new List<UserDetailInfo>();
            string url_follower_list_page = url + "/shop/" + shopid + "/followers/?offset=1&limit=" + limit + "&offset_of_offset=" + offset;
           
            {
                string spcresult_Html = "<html><body>" + DoGetRequestSendData(url_follower_list_page) + "</body></html>";
                //string spcresult_Html =  result.Html ;
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(spcresult_Html);
                if (null != document)
                {
                    int list_index = 1;
                    while (document.DocumentNode.SelectSingleNode("//html/body/li[" + list_index + "]") != null)
                    {
                        try
                        {
                            UserDetailInfo userinfo = new UserDetailInfo();
                            HtmlNode nodeLi = document.DocumentNode.SelectSingleNode("//html/body/li[" + list_index + "]");
                            HtmlNode nodeA = document.DocumentNode.SelectSingleNode("//html/body/li[" + list_index + "]/div[1]/div[1]/a");
                            userinfo.shopid = long.Parse(nodeLi.Attributes["data-follower-shop-id"].Value.ToString().Trim());
                            userinfo.id = long.Parse(nodeA.Attributes["userid"].Value.ToString().Trim());
                            userinfo.username = nodeA.Attributes["username"].Value.ToString().Trim();
                            userinfos.Add(userinfo);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("获取粉丝异常:" + e.Message);
                        }
                        list_index++;
                    }
                    Console.WriteLine("获取粉丝共:" + userinfos.Count);
                }
            }
            return userinfos;
        }
        public List<UserDetailInfo> GetFollowingList(Store store, long shopid, int count, int offset = 0)
        {
            List<UserDetailInfo> users = new List<UserDetailInfo>();
            try
            {
                ShopeeAPI api = new ShopeeAPI();

              
                String url = StoreRegionMap.GetBuyerURL(store.RegionID);
                int limit = count;//获得店铺粉丝页 粉丝个数
                               //https://shopee.tw/shop/12402310/following/?offset=1&limit=20&offset_of_offset=0&__classic__=1
                               //string url_follower_list_page = url + "/shop/" + shopid + "/followers/?offset=1&limit=" + limit + "&offset_of_offset=0";
                string url_follower_list_page = url + "/shop/" + shopid + "/following/?offset=1&limit=" + limit + "&offset_of_offset="+offset+"&__classic__=1";
                string spcresult_Html = "<html><body>" + DoGetRequestSendData(url_follower_list_page) + "</body></html>";
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(spcresult_Html);
                if (null != document)
                {
                    int list_index = 1;
                    while (document.DocumentNode.SelectSingleNode("//html/body/li[" + list_index + "]") != null)
                    {
                        try
                        {
                            UserDetailInfo user = new UserDetailInfo();
                            HtmlNode nodeLi = document.DocumentNode.SelectSingleNode("//html/body/li[" + list_index + "]");
                            HtmlNode nodeA = document.DocumentNode.SelectSingleNode("//html/body/li[" + list_index + "]/div[1]/div[1]/a");
                            user.shopid = long.Parse(nodeLi.Attributes["data-follower-shop-id"].Value);
                            user.id = long.Parse(nodeA.Attributes["userid"].Value);
                            user.username = nodeA.Attributes["username"].Value;
                            users.Add(user);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("获取关注用户失败："+e.Message);
                            //showLog( "获取粉丝异常");
                        }
                        list_index++;
                    }
                }
                else
                {
                    //取消完成
                    Console.WriteLine("获取关注用户失败：未能读取关注列表" );
                }

            }
            catch (Exception xe)
            {
                Console.WriteLine("获取关注用户失败"+xe.Message);
            }
            Console.WriteLine("获取关注用户数：计划"+count+"/实际" + users.Count);
            return users;
        }
        public string DoGetRequestSendData(string url)
        {
            HttpWebRequest hwRequest = null;
            HttpWebResponse hwResponse = null;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)WebRequest.Create(url);
                //hwRequest.Timeout = 30000;
                hwRequest.Method = "GET";
                //hwRequest.ContentType = "text/html; charset=utf-8";
                hwRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            }
            catch (System.Exception err)
            {

            }
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
            }
            return strResult;
        }
    }
}
