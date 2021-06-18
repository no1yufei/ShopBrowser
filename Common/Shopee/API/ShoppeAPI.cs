using CsharpHttpHelper;
using ShopeeChat.Shopee;
using ShopeeChat.Shopee.API;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat
{
    public partial class ShopeeAPI
    {
        public int GetUnReadMessageCount(StoreInfo storeInfo)
        {
            HtmlHttpHelper hhh = storeInfo.Hhh;
            string unreadCountQuestStr =storeInfo.ServerURL+"/webchat/api/v1/messages/unread-count";
            hhh.Authorization = "Bearer " + storeInfo.Token;
            HttpResult result = hhh.Get(unreadCountQuestStr, "UTF-8");
            string countResult = result.Html;
             int count = -1;
            try
            {
                count = int.Parse(countResult.Replace("{", "").Replace("\"", "").Replace("}", "").Split(':')[1]); //{"total_unread_count": 0}
            
            }
            catch(Exception ex)
            {
                Console.WriteLine("countResult:"+ countResult+ex.Message);
            }
            if(hhh.bError)
            {
                storeInfo.TotalErrorTime++;
            }
            return count;
        }
        public bool Logind(ShopGroup group,StoreInfo storeInfo,LogInDataWithCaptcha data = null)
        {
            bool success = false;
            //{"status": "verified", "p_token": "zZJsp01atE7y9cUm+oCOTqVnV4W17AW2teBtDiXyqwc=", "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImJlc3RicmFuZC5pZCIsImNyZWF0ZV90aW1lIjoxNTU3OTA0OTY3LCJpZCI6IjNkODU3NTg4NzZlMjExZTk5ZWUzYjQ5NjkxNDRkZjNlIiwiZGV2aWNlX2lkIjoiNDExMjBkZGItZjIxZS00MDdmLWJkOWQtOWRiZmQ5MDc1MjNkIn0.pG55WRhnep4G7RqAmGP1-lT2FFYewYmKp9wrVgHlR2c", "user": {"username": "bestbrand.id", "rating": 0, "uid": "0-53754837", "city": null, "locale": "zh-Hans", "gender": "unknown", "created_at": "2018-01-24T14:39:30+07:00", "distribution_status": null, "updated_at": "2019-04-30T14:08:41+07:00", "logined_at": "2019-04-30T08:50:45+07:00", "age": 49, "shop_id": 53753447, "status": "normal", "avatar": "https://cf.shopee.co.id/file/5d6e2ecff84f9cc3f53ca9fb0038a17c", "country": "ID", "is_blocked": false, "type": "seller", "id": 53754837}}
            HtmlHttpHelper hhh = storeInfo.Hhh;
            hhh.sCookies = "";
            hhh.Authorization = "";
            storeInfo.TotalErrorTime = 0;
            if(group.IsProxy)
            {
                hhh.bProxy = true;
                hhh.sProxyIP = group.ProxyIP;
                hhh.sProxyPort = group.Port.ToString();
                hhh.sProxyUserName = group.ProxyUserName;
                hhh.sProxyPassWord = group.Password;
            }
            string sessionQuestStr = storeInfo.ServerURL + "/webchat/api/v1/sessions";
            string postData = null;
            if(null == data)
            {
                postData = "{\"username\": \"%{username}%\", \"password\": \"%{password}%\", \"device_id\": \"%{deviceid}%\"}";
                postData = postData.Replace("%{username}%", storeInfo.UserName).Replace("%{password}%", storeInfo.Password).Replace("%{deviceid}%", Guid.NewGuid().ToString());
            }
            else
            {
                postData = data.ToString();
            }
            //
            //hhh.Referer = storeInfo.ServerURL + "/ webchat/login";

            HttpResult result = hhh.Post(sessionQuestStr, postData, "UTF-8");
            if (null != result.Header)
            {
                //{"status": "verified", "p_token": "PeG2WlPc9nKsu7/NojiQNNirEQcjBKcwY9bxUWUNCwQ=", "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFueWdvZ28wMSIsImNyZWF0ZV90aW1lIjoxNTU3NjQ4MzYyLCJpZCI6ImM5MWIzYzk4NzQ4YzExZTk5M2NhY2NiYmZlZjc2MzVkIiwiZGV2aWNlX2lkIjoiOTg4NDk4MjEtNzE4Ni00MTEwLWE1ZDItNzAwZDJiYTMyNTlmIn0.L_03-sNhVMR_tOxERG9nJiJHG-1Znkc595BCo_gi9To", "user": {"username": "anygogo01", "rating": 0, "uid": "0-61937185", "city": null, "locale": "zh-Hans", "gender": "unknown", "created_at": "2018-03-23T15:39:23+08:00", "distribution_status": null, "updated_at": "2019-05-12T15:42:18+08:00", "logined_at": "2019-05-12T12:19:11+08:00", "age": 49, "shop_id": 61935741, "status": "normal", "avatar": "https://cfshopeetw-a.akamaihd.net/file/9510e04ba0c437081f6c2748a58fefde", "country": "TW", "is_blocked": false, "type": "seller", "id": 61937185}}
                foreach (String nameValue in result.Html.Replace("{", "").Replace("}", "").Replace("\"", "").Split(','))
                {
                    if (nameValue.Split(':')[0].Trim().StartsWith("token"))
                    {
                        storeInfo.Token = nameValue.Split(':')[1].Trim();
                        hhh.Authorization = "Bearer " + storeInfo.Token;
                        success = true;
                        storeInfo.bRequestCaptcha = false;
                        storeInfo.requestCaptcha = 0;
                        storeInfo.bInvalidPassword = false;
                        storeInfo.loginInvalidPassword = 0;
                        //https://seller.xiapi.shopee.cn/api/v2/login/?SPC_CDS=b00ab7cc-f4e1-4a00-a582-5187e23370fb&SPC_CDS_VER=2

                        break;
                    }
                }
                storeInfo.ShopInfo = ShopInfo.FromJson(result.Html);

                LoginSPCCDS(storeInfo);
            }
            if(result.Html.ToLower().Contains("Captcha can't be blank".ToLower()))
            {
                storeInfo.requestCaptcha++;
                if(storeInfo.requestCaptcha > 1)
                {
                    storeInfo.bRequestCaptcha = true;
                }
               
            }
            if(result.Html.ToLower().Contains("password"))
            {
                storeInfo.loginInvalidPassword++;
                if(storeInfo.loginInvalidPassword > 1)
                {
                    storeInfo.bInvalidPassword = true;
                }
            }
            storeInfo.Cookies = hhh.sCookies;
            storeInfo.Hhh = hhh;
            if (!success)
            {
                Console.WriteLine(storeInfo.UserName + "-消息检索登录失败:" + result.Html);
            }
            return success;
        }
        public bool LoginSPCCDS(StoreInfo store)
        {
            if(store.Token != null && store.Token != string.Empty)
            {
                //https://seller.xiapi.shopee.cn/api/v2/login/?SPC_CDS=b00ab7cc-f4e1-4a00-a582-5187e23370fb&SPC_CDS_VER=2
                //https://seller.xiapi.shopee.cn/api/v2/shops/34796202/?SPC_CDS=88478753-eae9-4f97-a42d-659ef3087060&SPC_CDS_VER=2
                Guid SPC_CDS = Guid.NewGuid();
                string querURL = store.ServerURL + "/api/v2/login/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains(store.UserName))
                {
                    store.SPC_CDS = SPC_CDS;
                    Console.WriteLine("SPC登录成功!");
                }
               // querURL = store.ServerURL + "/api/v2/shops/"+ store.ShopInfo.user.id + "/?SPC_CDS="+ SPC_CDS.ToString() + "&SPC_CDS_VER=2";
               //spcresult = store.Hhh.Get(querURL);
               // if (spcresult.Html != null && spcresult.Html.Contains(store.UserName))
               // {
               //     Console.WriteLine("获取SPCToken成功!");
               // }
                return true;
            }
            return false;
        }
        public bool Logout(StoreInfo storeInfo)
        {
            bool success = false;
            //{"status": "verified", "p_token": "zZJsp01atE7y9cUm+oCOTqVnV4W17AW2teBtDiXyqwc=", "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImJlc3RicmFuZC5pZCIsImNyZWF0ZV90aW1lIjoxNTU3OTA0OTY3LCJpZCI6IjNkODU3NTg4NzZlMjExZTk5ZWUzYjQ5NjkxNDRkZjNlIiwiZGV2aWNlX2lkIjoiNDExMjBkZGItZjIxZS00MDdmLWJkOWQtOWRiZmQ5MDc1MjNkIn0.pG55WRhnep4G7RqAmGP1-lT2FFYewYmKp9wrVgHlR2c", "user": {"username": "bestbrand.id", "rating": 0, "uid": "0-53754837", "city": null, "locale": "zh-Hans", "gender": "unknown", "created_at": "2018-01-24T14:39:30+07:00", "distribution_status": null, "updated_at": "2019-04-30T14:08:41+07:00", "logined_at": "2019-04-30T08:50:45+07:00", "age": 49, "shop_id": 53753447, "status": "normal", "avatar": "https://cf.shopee.co.id/file/5d6e2ecff84f9cc3f53ca9fb0038a17c", "country": "ID", "is_blocked": false, "type": "seller", "id": 53754837}}
            HtmlHttpHelper hhh = storeInfo.Hhh;
            string sessionQuestStr = storeInfo.ServerURL + "webchat/api/v1/session?_uid=0-" + storeInfo.ShopInfo.user.shop_id;
            HttpResult result = hhh.Get(sessionQuestStr);
            if (null != result.Header)
            {
                //{"status": "verified", "p_token": "PeG2WlPc9nKsu7/NojiQNNirEQcjBKcwY9bxUWUNCwQ=", "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFueWdvZ28wMSIsImNyZWF0ZV90aW1lIjoxNTU3NjQ4MzYyLCJpZCI6ImM5MWIzYzk4NzQ4YzExZTk5M2NhY2NiYmZlZjc2MzVkIiwiZGV2aWNlX2lkIjoiOTg4NDk4MjEtNzE4Ni00MTEwLWE1ZDItNzAwZDJiYTMyNTlmIn0.L_03-sNhVMR_tOxERG9nJiJHG-1Znkc595BCo_gi9To", "user": {"username": "anygogo01", "rating": 0, "uid": "0-61937185", "city": null, "locale": "zh-Hans", "gender": "unknown", "created_at": "2018-03-23T15:39:23+08:00", "distribution_status": null, "updated_at": "2019-05-12T15:42:18+08:00", "logined_at": "2019-05-12T12:19:11+08:00", "age": 49, "shop_id": 61935741, "status": "normal", "avatar": "https://cfshopeetw-a.akamaihd.net/file/9510e04ba0c437081f6c2748a58fefde", "country": "TW", "is_blocked": false, "type": "seller", "id": 61937185}}
                foreach (String nameValue in result.Html.Replace("{", "").Replace("}", "").Replace("\"", "").Split(','))
                {
                    if (nameValue.Split(':')[0].Trim().StartsWith("token"))
                    {
                        storeInfo.Token = nameValue.Split(':')[1].Trim();
                        hhh.Authorization = "Bearer " + storeInfo.Token;
                        success = true;
                        break;
                    }
                }
                storeInfo.ShopInfo = ShopInfo.FromJson(result.Html);
            }
            storeInfo.Cookies = hhh.sCookies;
            storeInfo.Hhh = hhh;
            
            return success;
        }
        //logout: https://seller.xiapi.shopee.cn/webchat/api/v1/session?_uid=0-12545457
        public void  GetCustomerList(StoreInfo storeInfo)
        {
            //https://seller.shopee.co.id/webchat/api/v1/conversations?next_timestamp=-1&direction=older
            HtmlHttpHelper hhh = storeInfo.Hhh;
            //https://seller.shopee.co.th/webchat/api/v1/conversations?next_timestamp=-1&direction=older
            string customerQuestStr = storeInfo.ServerURL + "/webchat/api/v1/conversations?next_timestamp=-1&direction=older";

            HttpResult result = hhh.Get(customerQuestStr, "UTF-8");
            if (result.Header != null)
            {
                storeInfo.CustomerInfos = CustomerInfo.ListFromJson(result.Html);
            }
        }

        public void SendMessage(StoreInfo store, CustomerInfo customer, String text)
        {
            //https://seller.shopee.co.id/webchat/api/v1/messages
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = store.ServerURL + "/webchat/api/v1/messages";
            TextMessageContent content = new TextMessageContent(text);
            Message msg = new Message(store,customer, content);
            HttpResult result = hhh.Post(msgQuestStr, msg.ToJson());
            if (result.Header != null)
            {
                Console.WriteLine(result.Html);
            }
            Console.WriteLine(result.Html);
        }
        public void SendMessageToUserId(StoreInfo store,long userID, String text)
        {
            //https://seller.shopee.co.id/webchat/api/v1/messages
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = store.ServerURL + "/webchat/api/v1/messages";
            TextMessageContent content = new TextMessageContent(text);
            Message msg = new Message(store, userID, content);
            HttpResult result = hhh.Post(msgQuestStr, msg.ToJson());
            if (result.Header != null)
            {
                Console.WriteLine(result.Html);
            }
            Console.WriteLine(result.Html);
        }
        public RatingCustomInfos GetRatingCustoms(Region region,StoreInfo store,long itemShopId,long itemID,int count)
        {
            RatingCustomInfos customInfo = new RatingCustomInfos();
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = region.BuyerUrl + String.Format("/api/v2/item/get_ratings?filter=0&flag=1&itemid={0}&limit={1}&offset=0&shopid={2}&type=0",itemID,count,itemShopId);
           
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
        public SearchedProductInfo GetProductInfoByKeyword(Region region, StoreInfo store, String keyword)
        {
            SearchedProductInfo customInfo = new SearchedProductInfo();
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = region.BuyerUrl + String.Format("/api/v2/search_items/?by=relevancy&keyword={0}&limit=50&newest=0&order=desc&page_type=search", keyword.Trim().Replace(" ","%20"));

            HttpResult result = hhh.Get(msgQuestStr);
            if (result.Header != null)
            {
                Console.WriteLine(result.Html);
                customInfo = SearchedProductInfo.DataJson(result.Html);
            }
            if(customInfo.items.Count() <= 0)
            {
                Console.WriteLine(result.Html);
            }
           
            return customInfo;
        }
        public bool GetSummaryData(StoreInfo store)
        {
            if (store.Token != null && store.Token != string.Empty && store.SPC_CDS != null)
            {
                if (store.LastUpdateSummaryTime != null && (DateTime.UtcNow - store.LastUpdateSummaryTime).TotalMinutes < 60)
                {
                    return true;
                }
                long endtime = Tool.GetTimeStampSeconds() / 3600 * 3600;
                long starttime = (endtime / (3600 * 24)) * 3600 * 24;

                string querURL = store.ServerURL + "/api/mydata/v1/metrics/shop/summary/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&start_time="
                    + starttime + "&end_time=" + endtime + "&period=real_time";
                HttpResult spcresult = store.Hhh.Get(querURL);

                if (spcresult.Html != null && spcresult.Html.Contains("value"))
                {
                    SummaryInfo sum = SummaryInfo.DataJson(spcresult.Html.Replace("\n",""));
                    if (null != sum)
                    {
                        store.SummaryInfo = sum;
                        store.LastUpdateSummaryTime = Tool.GetUTCDateTime(endtime);
                        //store.SPC_CDS = SPC_CDS;
                        Console.WriteLine(store.UserName + ":统计数据成功！");
                        return true;
                    }
                }
            }
            return false;
        }
        public bool FollowUser(StoreInfo store,string UserId,String ShopId)
        {
            if (store.Token != null && store.Token != string.Empty)
            {
                //https://seller.xiapi.shopee.cn/api/v2/users/44012663/?SPC_CDS=3d007cc9-8878-4bef-98fe-3dfa2f5d743f&SPC_CDS_VER=2
                Guid SPC_CDS = store.SPC_CDS;
                String data = "{\"user\":{\"followed\":1,\"shopid\":" + ShopId + ",\"user_command\":\"follow\"} }";
                string querURL = store.ServerURL + "/api/v2/users/"+ UserId + "/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if(!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies +=  "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Put(querURL, data);
                //Console.WriteLine("关注：" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("{}"))
                {
                    Console.WriteLine("SPC 关注成功!");
                    return true;
                }
                Console.WriteLine("关注返回:" + spcresult.Html);
            }
            return false;
        }
        public bool UnFollowUser(StoreInfo store, string UserId, String ShopId)
        {
            if (store.Token != null && store.Token != string.Empty)
            {
                //https://seller.xiapi.shopee.cn/api/v2/users/44012663/?SPC_CDS=3d007cc9-8878-4bef-98fe-3dfa2f5d743f&SPC_CDS_VER=2
                Guid SPC_CDS = store.SPC_CDS;
                String data = "{\"user\":{\"followed\":0,\"shopid\":" + ShopId + ",\"user_command\":\"follow\"} }";
                string querURL = store.ServerURL + "/api/v2/users/" + UserId + "/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Put(querURL, data);
                //Console.WriteLine("关注：" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("{}"))
                {
                    Console.WriteLine("SPC 取消关注成功!");
                    return true;
                }
                Console.WriteLine("关注返回:" + spcresult.Html);
            }
            return false;
        }

        public bool UpdateSummaryOrderInfo(StoreInfo store)
        {
            if (store.Token != null && store.Token != string.Empty)
            {
                //https://seller.xiapi.shopee.cn/api/v2/orders/meta/?SPC_CDS=1a53c935-eb11-4196-9ad3-58be21b10889&SPC_CDS_VER=2
                //https://seller.xiapi.shopee.cn/api/v2/users/44012663/?SPC_CDS=3d007cc9-8878-4bef-98fe-3dfa2f5d743f&SPC_CDS_VER=2
                Guid SPC_CDS = store.SPC_CDS;                string querURL = store.ServerURL + "/api/v2/orders/meta/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (store.Hhh.bError)
                {
                    store.TotalErrorTime++;
                }
                //Console.WriteLine("更新订单汇总" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("toship"))
                {
                    store.OrderSummaryInfo = OrderSummaryInfo.FromJson(spcresult.Html);
                    Console.WriteLine("SPC 订单汇总成功!");
                    return true;
                }
                Console.WriteLine("订单汇总:" + spcresult.Html);
            }
            return false;
        }
        public bool UpdateOrderInfo(StoreInfo store)
        {
            if (store.Token != null && store.Token != string.Empty && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            {
            //https://seller.xiapi.shopee.cn/api/v2/orders/?is_massship=false&limit=40&offset=0&type=toship&SPC_CDS=1a53c935-eb11-4196-9ad3-58be21b10889&SPC_CDS_VER=2
                
                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v2/orders/?is_massship=false&limit=40&offset=0&type=toship&SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                //Console.WriteLine("更新订单信息" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("toship"))
                {
                    store.OrderInfos = OrderInfos.FromJson(spcresult.Html);
                    Console.WriteLine("更新订单信息!");
                    return true;
                }
                Console.WriteLine("更新订单信息:" + spcresult.Html);
            }
            return false;
        }
        public OrderInfo GetOrderInfo(StoreInfo store,String orderid)
        {
            OrderInfo orderInfo = null;
            if (store.Token != null && store.Token != string.Empty && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            {
                //https://seller.xiapi.shopee.cn/api/v2/orders/1411621647/?SPC_CDS=051171ba-0fab-4b81-a225-c682f268b763&SPC_CDS_VER=2

                Guid SPC_CDS = store.SPC_CDS; string querURL = store.ServerURL + "/api/v2/orders/"+ orderid + "?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                //Console.WriteLine("更新订单信息" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("toship"))
                {
                    orderInfo = OrderInfo.FromJson(spcresult.Html);
                    Console.WriteLine("更新订单信息!");
                    //return true;
                }
                Console.WriteLine("更新订单信息:" + spcresult.Html);
            }
            return orderInfo;
        }
        
        public bool GenerateTraceNo(StoreInfo store,String orderid)
        {
            if (store.Token != null && store.Token != string.Empty && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            {

                //https://seller.xiapi.shopee.cn/api/v2/orders/dropoffs/1411621647/?SPC_CDS=58b7eefa-fa8b-443d-8fcf-80a922ee51a2&SPC_CDS_VER=2
                String data = "{\"orderLogistic\":{\"userid\":0,\"orderid\":null,\"type\":0,\"status\":0,\"channelid\":0,\"channel_status\":\"\",\"consignment_no\":\"\",\"booking_no\":\"\",\"pickup_time\":0,\"actual_pickup_time\":0,\"deliver_time\":0,\"actual_deliver_time\":0,\"ctime\":0,\"mtime\":0,\"seller_realname\":\"\",\"branchid\":0,\"slug\":\"\",\"shipping_carrier\":\"\",\"logistic_command\":\"generate_tracking_no\",\"extra_data\":\"{ }\"}}";
                Guid SPC_CDS = store.SPC_CDS; string querURL = store.ServerURL + "/api/v2/orders/dropoffs/"+ orderid + "/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Put(querURL,data);
                //Console.WriteLine("申请跟踪号：" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("{}"))
                {
                    //store.OrderInfos = OrderInfos.FromJson(spcresult.Html);
                    Console.WriteLine("申请跟踪号成功!");
                    return true;
                }
                Console.WriteLine("申请跟踪号:" + spcresult.Html);
            }
            return false;
        }
        
        public List<String> DownloadPostLable(StoreInfo store, List<String> orderids)
        {
            List<String> oderPdfList = new List<string>();
            if (store.Token != null && store.Token != string.Empty && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            {
                Guid id = Guid.NewGuid();
                string tempDic = id.ToString("N")+"\\";
                string basePath = Environment.CurrentDirectory+"\\Lable\\";
                if(!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                if (!Directory.Exists(basePath+tempDic))
                {
                    Directory.CreateDirectory(basePath + tempDic);
                }
                
              
                foreach (String orderid in orderids)
                {
                    //https://seller.xiapi.shopee.cn/api/v2/orders/waybill/?orderids=[1411621647]&language=tw&api_from=waybill
                    Guid SPC_CDS = store.SPC_CDS;
                    string querURL = store.ServerURL + "/api/v2/orders/waybill/?orderids=[" + orderid + "]&language=tw&api_from=waybill";
                    if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                    {
                        store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                    }
                    string labeFile =  basePath +tempDic + orderid + ".pdf";
                    HttpResult spcresult = store.Hhh.DownLoad(querURL,labeFile);
                    Console.WriteLine("下载面单：" + querURL);
                    oderPdfList.Add(labeFile);
                    if (spcresult.Html != null && spcresult.Html.Contains("{}"))
                    {
                        //store.OrderInfos = OrderInfos.FromJson(spcresult.Html);
                        Console.WriteLine("下载面单成功!");
                        //return true;
                    }
                    Console.WriteLine("下载面单:" + spcresult.Html);
                }
            }
            return oderPdfList;
        }
        public FollowInfo GetFollowInfo(Region region,StoreInfo store)
        {
            FollowInfo finfo = new FollowInfo();
            if (store.Token != null && store.Token != string.Empty && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            {
                //https://seller.xiapi.shopee.cn/api/v2/shops/34796202/?SPC_CDS=6e0cb4ed-7ac0-4f49-8f6d-f0ad30361d17&SPC_CDS_VER=2
                String data = "{\"shop_ids\":["+store.ShopInfo.user.shop_id+"]}";
              
                string querURL = region.BuyerUrl + "/api/v1/shops/";
                HtmlHttpHelper hhh = new HtmlHttpHelper();
                HttpResult spcresult = hhh.Post(querURL, data);
                if (spcresult.Html != null && spcresult.Html.Contains("username"))
                {
                    finfo = FollowInfo.FromJson(spcresult.Html);
                    Console.WriteLine("更新关粉信息成功!");
                }
                Console.WriteLine("更新关粉信息:" + spcresult.Html);
            }
            return finfo;
        }
    }
}
