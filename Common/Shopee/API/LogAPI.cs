using Common.Shopee.API.Data;
using CsharpHttpHelper;
using ShopeeChat.Security;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
        string reg = @"[0-9a-fA-F]{8}(-[0-9a-fA-F]{4}){3}-[0-9a-fA-F]{12}";
        private string replaceSPC(string cookie)
        {
            return Regex.Replace(cookie, @"SPC_CDS=" + reg + ";", "");
        }
        public bool Login(StoreGroup group, Store store, string captcha = null, string captcha_key = null, string vcode = null)
        {
            HtmlHttpHelper hhh = store.Hhh;
            if (null == hhh)
            {
                hhh = new HtmlHttpHelper();
                store.Hhh = hhh;
            }
            hhh.bProxy = group.IsProxy;
            if (hhh.bProxy)
            {
                hhh.sProxyIP = group.ProxyIP;
                hhh.sProxyPort = group.Port.ToString();
                hhh.sProxyUserName = group.ProxyUserName;
                hhh.sProxyPassWord = group.Password;
            }
            if (store.IsLocalAccount && store.Cookies != "")
            {
                store.Hhh.sCookies = store.Cookies;
            }
            
            if (loginSPC(store))
            {
                return LoginWebChat(store);
            }
            else if (!store.OnlyCookie)
            {
                if (LoginSellerCenter(store, captcha, captcha_key, vcode))
                {
                    if (LoginWebChat(store))
                    {
                        store.Cookies = store.Hhh.sCookies;
                        return true;

                    }
                }
            }
            return false;
        }
        public bool LoginWebChat(Store storeInfo)
        {
            bool success = false;
            //{"status": "verified", "p_token": "zZJsp01atE7y9cUm+oCOTqVnV4W17AW2teBtDiXyqwc=", "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImJlc3RicmFuZC5pZCIsImNyZWF0ZV90aW1lIjoxNTU3OTA0OTY3LCJpZCI6IjNkODU3NTg4NzZlMjExZTk5ZWUzYjQ5NjkxNDRkZjNlIiwiZGV2aWNlX2lkIjoiNDExMjBkZGItZjIxZS00MDdmLWJkOWQtOWRiZmQ5MDc1MjNkIn0.pG55WRhnep4G7RqAmGP1-lT2FFYewYmKp9wrVgHlR2c", "user": {"username": "bestbrand.id", "rating": 0, "uid": "0-53754837", "city": null, "locale": "zh-Hans", "gender": "unknown", "created_at": "2018-01-24T14:39:30+07:00", "distribution_status": null, "updated_at": "2019-04-30T14:08:41+07:00", "logined_at": "2019-04-30T08:50:45+07:00", "age": 49, "shop_id": 53753447, "status": "normal", "avatar": "https://cf.shopee.co.id/file/5d6e2ecff84f9cc3f53ca9fb0038a17c", "country": "ID", "is_blocked": false, "type": "seller", "id": 53754837}}
            HtmlHttpHelper hhh = storeInfo.Hhh;
            if (null == hhh)
            {
                Console.WriteLine(storeInfo.UserName + "-登录时，未能争取初始化链接！");
                return false;
            }
            hhh.Authorization = "";
            storeInfo.Token = null;
            //https://seller.xiapi.shopee.cn/webchat/api/v1.1/login?_v=3.9.0

            //hhh.sCookies = storeInfo.Cookies;//恢复cookie
            string sessionQuestStr = storeInfo.ServerURL + "/webchat/api/v1.1/login?_v=3.9.0";
            HttpResult result = hhh.Post(sessionQuestStr, "");
            if (result.StatusCode == System.Net.HttpStatusCode.OK && result.Html != null)
            {
                storeInfo.ShopInfo = ShopInfo.FromJson(result.Html);
                if (null != storeInfo.ShopInfo)
                {
                    storeInfo.LogStatus = LoginStatus.Log_Succuss;
                    storeInfo.Token = storeInfo.ShopInfo.token;
                    hhh.Authorization = "Bearer " + storeInfo.Token;

                    storeInfo.ShopID = storeInfo.ShopInfo.user.shop_id;
                    storeInfo.UserID = storeInfo.ShopInfo.user.id;
                    storeInfo.TotalErrorTime = 0;
                    success = true;
                    Console.WriteLine(storeInfo.UserName + "-消息检索登录成功！");
                }
            }
            if (!success)
            {
                Console.WriteLine(storeInfo.UserName + "-消息检索登录失败:" + result.Html);
            }
            return success;
        }
       
        public bool LoginSellerCenter(Store store, string captcha = null, string captcha_key = null, string vcode = null)
        {
            HtmlHttpHelper hhh = store.Hhh;
            if (null == hhh)
            {
                Console.WriteLine(store.UserName + "-登录用户中心时，未能争取初始化链接！");
                return false;
            }
            //https://seller.xiapi.shopee.cn/api/v2/login/?SPC_CDS=b00ab7cc-f4e1-4a00-a582-5187e23370fb&SPC_CDS_VER=2
            //https://seller.xiapi.shopee.cn/api/v2/shops/34796202/?SPC_CDS=88478753-eae9-4f97-a42d-659ef3087060&SPC_CDS_VER=2
            if (null == store.SPC_CDS || store.SPC_CDS == Guid.Empty)
            {
                store.SPC_CDS = Guid.NewGuid();
            }
            Guid SPC_CDS = store.SPC_CDS;
            string querURL = store.ServerURL + "/api/v2/login/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
            if (captcha_key == null || captcha == null)
            {
                captcha_key = Guid.NewGuid().ToString("N");
                captcha = "";
            }
            String dataPattern = "captcha=" + captcha
                               + "&captcha_key=" + captcha_key
                               + "&remember=false"
                               + "&password_hash=" + HashHelper.GetSHA256PassWord(store.Password)
                               + "&username=" + store.UserName.Trim();
            if (null != vcode)
            {
                dataPattern += "&vcode=" + vcode;
            }

            store.Hhh.sContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            HttpResult spcresult = store.Hhh.Post(querURL, dataPattern);
            store.Hhh.sContentType = "";

            if (spcresult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("用户中心登录成功!");

                store.LogStatus = LoginStatus.Log_Succuss;
                store.Cookies = store.Hhh.sCookies;
                return true;

            }
            else if (spcresult.Html.Length > 0)
            {
                ErrorMessage errmsg = ErrorMessage.FromJson(spcresult.Html);
                if (null != errmsg)
                {
                    store.LogStatus = errmsg.ErrType;
                }
                Console.WriteLine("用户中心登录:!" + spcresult.Html);
            }

            Console.WriteLine("用户中心登录失败：" + store.LogMessage[store.LogStatus]);
            return false;
        }
        private bool loginSPC(Store store)
        {
            HtmlHttpHelper hhh = store.Hhh;
            if (null == hhh)
            {
                Console.WriteLine(store.UserName + "-登录用户中心时，未能争取初始化链接！");
                return false;
            }
            if (store.SPC_CDS == Guid.Empty)
            {
                store.SPC_CDS = Guid.NewGuid();
            }
            var SPC_CDS = store.SPC_CDS;
            //https://seller.xiapi.shopee.cn/api/v2/login/?SPC_CDS=b00ab7cc-f4e1-4a00-a582-5187e23370fb&SPC_CDS_VER=2
            //https://seller.xiapi.shopee.cn/api/v2/shops/34796202/?SPC_CDS=88478753-eae9-4f97-a42d-659ef3087060&SPC_CDS_VER=2

            string querURL = store.ServerURL + "/api/v2/login/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";

            if (!store.Hhh.sCookies.Contains("SPC_CDS"))
            {
                store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
            }
            store.Hhh.Referer = store.ServerURL;
            HttpResult spcresult = store.Hhh.Get(querURL);
            store.Hhh.Referer = "";
            if (spcresult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                store.LogStatus = LoginStatus.Log_Succuss;
                Console.WriteLine(store.DisplayName + "SPC_CDS登录成功!");
                return true;
            }
            Console.WriteLine(store.DisplayName + "SPC_CDS登录失败!" + spcresult.Html);

            return false;
        }
        public bool Logout(Store storeInfo)
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

        /// <summary>
        /// 判断当前店铺是否已经登录
        /// </summary>
        /// <param name="store">店铺实例</param>
        /// <returns>True-已经登录，False-未登录</returns>
        public bool IsLogin(Store store)
        {
            if (store.Token != null && store.Token != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool LoginWebChatUP(Store storeInfo)
        {
            bool success = false;
            //{"status": "verified", "p_token": "zZJsp01atE7y9cUm+oCOTqVnV4W17AW2teBtDiXyqwc=", "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImJlc3RicmFuZC5pZCIsImNyZWF0ZV90aW1lIjoxNTU3OTA0OTY3LCJpZCI6IjNkODU3NTg4NzZlMjExZTk5ZWUzYjQ5NjkxNDRkZjNlIiwiZGV2aWNlX2lkIjoiNDExMjBkZGItZjIxZS00MDdmLWJkOWQtOWRiZmQ5MDc1MjNkIn0.pG55WRhnep4G7RqAmGP1-lT2FFYewYmKp9wrVgHlR2c", "user": {"username": "bestbrand.id", "rating": 0, "uid": "0-53754837", "city": null, "locale": "zh-Hans", "gender": "unknown", "created_at": "2018-01-24T14:39:30+07:00", "distribution_status": null, "updated_at": "2019-04-30T14:08:41+07:00", "logined_at": "2019-04-30T08:50:45+07:00", "age": 49, "shop_id": 53753447, "status": "normal", "avatar": "https://cf.shopee.co.id/file/5d6e2ecff84f9cc3f53ca9fb0038a17c", "country": "ID", "is_blocked": false, "type": "seller", "id": 53754837}}
            HtmlHttpHelper hhh = storeInfo.Hhh;
            if (null == hhh)
            {
                Console.WriteLine(storeInfo.UserName + "-登录时，未能争取初始化链接！");
                return false;
            }
            hhh.Authorization = "";
            storeInfo.Token = null;

            string sessionQuestStr = storeInfo.ServerURL + "/webchat/api/v1/sessions";
            string postData = null;

            postData = "{\"username\": \"%{username}%\", \"password\": \"%{password}%\", \"device_id\": \"%{deviceid}%\"}";
            postData = postData.Replace("%{username}%", storeInfo.UserName).Replace("%{password}%", storeInfo.Password).Replace("%{deviceid}%", Guid.NewGuid().ToString());

            HttpResult result = hhh.Post(sessionQuestStr, postData, "UTF-8");
            if (result.StatusCode == System.Net.HttpStatusCode.OK && result.Html != null)
            {
                storeInfo.ShopInfo = ShopInfo.FromJson(result.Html);
                if (null != storeInfo.ShopInfo)
                {
                    storeInfo.Token = storeInfo.ShopInfo.token;
                    hhh.Authorization = "Bearer " + storeInfo.Token;
                    success = true;

                    storeInfo.ShopID = storeInfo.ShopInfo.user.shop_id;
                    storeInfo.UserID = storeInfo.ShopInfo.user.id;
                    storeInfo.Hhh = hhh;
                    storeInfo.TotalErrorTime = 0;
                    Console.WriteLine(storeInfo.UserName + "-消息检索登录成功！");
                }
            }
            if (!success)
            {
                Console.WriteLine(storeInfo.UserName + "-消息检索登录失败:" + result.Html);
            }
            return success;
        }
    }
}
