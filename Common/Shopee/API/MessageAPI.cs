using CsharpHttpHelper;
using Newtonsoft.Json;
using ShopeeChat.SysData;
using System;
using System.IO;
using System.Linq;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
        public int GetUnReadMessageCountMini(Store storeInfo)
        {
            int count = -1;
            if (!IsLogin(storeInfo))
            {
                Console.WriteLine(storeInfo.DisplayName + "获取新消息数量失败:" + storeInfo.LogMessage[storeInfo.LogStatus]);
                return count;
            }
            //https://seller.xiapi.shopee.cn/webchat/api/v1.1/mini/conversation/unread-count?_uid=0-172333356&_v=1.8.0
            HtmlHttpHelper hhh = storeInfo.Hhh;
            Guid SPC_CDS = storeInfo.SPC_CDS;
            string unreadCountQuestStr = storeInfo.ServerURL
                + "/webchat/api/v1.1/mini/conversation/unread-count?_uid=0-" + storeInfo.ShopID + "&_v=1.8.0";

           // hhh.Authorization = "Bearer " + storeInfo.Token;
            HttpResult result = hhh.Get(unreadCountQuestStr);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                UnreadMessageCount countResult = UnreadMessageCount.FromJson(result.Html);
                if (null != countResult)
                {
                    return countResult.total_unread_count;
                }
            }
            Console.WriteLine(storeInfo.DisplayName + "获取新消息数量失败:" + result.Html);
            if (hhh.bError)
            {
                storeInfo.TotalErrorTime++;
            }
            return count;
        }
        public class UnreadMessageCount
        {
            public int total_unread_count;
            public long shop_id;
            static public UnreadMessageCount FromJson(String str)
            {
                UnreadMessageCount ret = null;
                try
                {
                    ret = JsonConvert.DeserializeObject<UnreadMessageCount>(str);
                }
                catch (Exception xe)
                {
                    Console.WriteLine(typeof(UnreadMessageCount) + "\r\n转换Json失败：" + xe.Message.ToString() + "\r\n" + str);
                }
                return ret;
            }
        }
        public int GetUnReadMessageCountToken1(Store storeInfo)
        {
            int count = -1;
            if(!IsLogin(storeInfo) || !storeInfo.Hhh.CookiesDic.Keys.Contains("CTOKEN"))
            {
                Console.WriteLine(storeInfo.DisplayName + "获取新消息数量失败:" + storeInfo.LogMessage[storeInfo.LogStatus]);
                return count;
            }
             //https://seller.xiapi.shopee.cn/webchat/api/v1.1/mini/conversation/unread-count?_uid=0-172333356&_v=1.8.0
            //https://seller.xiapi.shopee.cn/webchat/api/v1.1/mini/user/sync?_uid=0-270047724&_v=1.8.0&csrf_token=v7XLnsHLEeqSmPBj%2BXbyZw%3D%3D
            HtmlHttpHelper hhh = storeInfo.Hhh;
            Guid SPC_CDS = storeInfo.SPC_CDS;
            string unreadCountQuestStr = storeInfo.ServerURL 
                + "/webchat/api/v1.1/mini/user/sync?_uid=0-"+storeInfo.ShopID+"&_v=1.8.0&csrf_token="+storeInfo.Hhh.CookiesDic["CTOKEN"];

            hhh.Authorization = "Bearer " + storeInfo.Token;
            HttpResult result = hhh.Post(unreadCountQuestStr, "");
            if(result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                UnreadMessageCountByToken countResult = UnreadMessageCountByToken.FromJson(result.Html);
                if(null != countResult)
                {
                    return countResult.unread_count;
                }
            }
            Console.WriteLine(storeInfo.DisplayName +"获取新消息数量失败:"+ result.Html);
            if (hhh.bError)
            {
                storeInfo.TotalErrorTime++;
            }
            return count;
        }
        public class UnreadMessageCountByToken
        {
            public int unread_count;
            public bool have_new_msg;
            static public UnreadMessageCountByToken FromJson(String str)
            {
                UnreadMessageCountByToken ret = null;
                try
                {
                    ret = JsonConvert.DeserializeObject<UnreadMessageCountByToken>(str);
                }
                catch (Exception xe)
                {
                    Console.WriteLine(typeof(UnreadMessageCountByToken) + "\r\n转换Json失败：" + xe.Message.ToString() + "\r\n" + str);
                }
                return ret;
            }
        }

        public int GetUnReadMessageCount(Store storeInfo)
        {
            HtmlHttpHelper hhh = storeInfo.Hhh;
            string unreadCountQuestStr = storeInfo.ServerURL + "/webchat/api/v1/messages/unread-count";
            hhh.Authorization = "Bearer " + storeInfo.Token;
            HttpResult result = hhh.Get(unreadCountQuestStr, "UTF-8");
            string countResult = result.Html;
            int count = -1;
            try
            {
                count = int.Parse(countResult.Replace("{", "").Replace("\"", "").Replace("}", "").Split(':')[1]); //{"total_unread_count": 0}

            }
            catch (Exception ex)
            {
                Console.WriteLine("countResult:" + countResult + ex.Message);
            }
            if (hhh.bError)
            {
                storeInfo.TotalErrorTime++;
            }
            else
            {
                storeInfo.TotalErrorTime = 0;
            }
            return count;
        }

        public int GetUnReadMessageCountSC1(Store storeInfo)
        {
            //https://seller.my.shopee.cn/webchat/api/v1/sc/messages/unread-count?SPC_CDS=eff87c8b-cdb9-4b10-a524-2c7319752c06&SPC_CDS_VER=2
            HtmlHttpHelper hhh = storeInfo.Hhh;
            Guid SPC_CDS = storeInfo.SPC_CDS;
            string unreadCountQuestStr = storeInfo.ServerURL + "/webchat/api/v1/sc/messages/unread-count?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2"; ;
           
            hhh.Authorization = "Bearer " + storeInfo.Token;
            HttpResult result = hhh.Get(unreadCountQuestStr, "UTF-8");
            string countResult = result.Html;
            int count = -1;
            try
            {
                count = int.Parse(countResult.Replace("{", "").Replace("\"", "").Replace("}", "").Split(':')[1]); //{"total_unread_count": 0}

            }
            catch (Exception ex)
            {
                Console.WriteLine("countResult:" + countResult + ex.Message);
            }
            if (hhh.bError)
            {
                storeInfo.TotalErrorTime++;
            }
            return count;
        }
        public string UploadMessageImage(Store store, string imageFile)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //https://seller.my.shopee.cn/webchat/api/v1/images?_uid=0-176119092&_v=2.9.0
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/webchat/api/v1/images?_uid=" + store.ShopInfo.user.id + "&_v=2.9.0";
                if (File.Exists(imageFile))
                {
                    try
                    {
                        byte[] imageBytes;
                        using (FileStream stream = new FileStream(imageFile, FileMode.Open))
                        {
                            imageBytes = new byte[stream.Length];
                            stream.Read(imageBytes, 0, (int)stream.Length);
                        }
                        store.Hhh.Referer = store.ServerURL + "/webchat/conversations";

                        if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                        {
                            store.Hhh.sCookies += "SPC_CDS=" + store.SPC_CDS.ToString() + ";";
                        }
                        store.Hhh.iTimeOut = 100000;
                        //调用HTTP请求，
                        HttpResult spcresult = store.Hhh.PostImage(querURL, imageBytes);
                        //store.Hhh.iTimeOut = 0;
                        store.Hhh.Referer = "";
                        //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                        if (spcresult.Html != null )
                        {
                            MessageImageUpload img = MessageImageUpload.FromJsonString(spcresult.Html);
                            if(img.url != "")
                            {
                                //打印调试信息，返回成功标志
                                Console.WriteLine(store.UserName + ":上传图像数据成功！"+img.url);
                                return img.url;
                            }
                        }
                        Console.WriteLine(store.UserName + ":上传图像数据失败！" + spcresult.Html);
                    }
                    catch (Exception xe)
                    {
                        Console.WriteLine(store.UserName + ":上传图像数据失败！" + xe.Message);
                    }
                }

                Console.WriteLine(store.UserName + ":上传图像数据失败！文件不存在");
            }
            //返回错误标识
            return "";
        }
        
        public void SendMessage(Store store, ShopCustomerInfo customer, String text)
        {
            //https://seller.shopee.co.id/webchat/api/v1/messages
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = store.ServerURL + "/webchat/api/v1/messages";
            TextMessageContent content = new TextMessageContent(text);
            ChatMessageInfo msg = new ChatMessageInfo(store, customer, content);
            HttpResult result = hhh.Post(msgQuestStr, msg.ToJson());
            if (result.Header != null)
            {
                Console.WriteLine(result.Html);
            }
            Console.WriteLine(result.Html);
        }
        public void SendMessage(Store store,long toUserID, MessageContent content)
        {
            //https://seller.shopee.co.id/webchat/api/v1/messages
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = store.ServerURL + "/webchat/api/v1/messages";
            ChatMessageInfo msg = new ChatMessageInfo(store, toUserID,content);
            HttpResult result = hhh.Post(msgQuestStr, msg.ToJson());
            if (result.Header != null)
            {
                Console.WriteLine(result.Html);
            }
            Console.WriteLine(result.Html);
        }
        public void SendMessage(Store store, OrderInfoV3 orderinfo, MessageContent content)
        {
            //https://seller.shopee.co.id/webchat/api/v1/messages
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = store.ServerURL + "/webchat/api/v1/messages";
            ChatMessageInfo msg = new ChatMessageInfo(store, orderinfo.buyer_user.user_id, orderinfo.order_id, content);
            HttpResult result = hhh.Post(msgQuestStr, msg.ToJson());
            if (result.Header != null)
            {
                Console.WriteLine(result.Html);
            }
            Console.WriteLine(result.Html);
        }
       
        public void SendMessage(Store store, OrderInfoV3 orderinfo, String text)
        {
            //https://seller.shopee.co.id/webchat/api/v1/messages
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = store.ServerURL + "/webchat/api/v1/messages";
            TextMessageContent content = new TextMessageContent(text);
            ChatMessageInfo msg = new ChatMessageInfo(store, orderinfo.buyer_user.user_id,orderinfo.order_id, content);
            HttpResult result = hhh.Post(msgQuestStr, msg.ToJson());
            if (result.Header != null)
            {
                Console.WriteLine(result.Html);
            }
            Console.WriteLine(result.Html);
        }
        public void SendMessageToUserId(Store store, long userID, String text)
        {
            //https://seller.shopee.co.id/webchat/api/v1/messages
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = store.ServerURL + "/webchat/api/v1/messages";
            TextMessageContent content = new TextMessageContent(text);
            ChatMessageInfo msg = new ChatMessageInfo(store, userID, content);
            HttpResult result = hhh.Post(msgQuestStr, msg.ToJson());
            if (result.Header != null)
            {
                Console.WriteLine(result.Html);
            }
            Console.WriteLine(result.Html);
        }
        public void SendMessageToOderUser(Store store, long userID,long orderid, String text)
        {
            //https://seller.shopee.co.id/webchat/api/v1/messages
            HtmlHttpHelper hhh = store.Hhh;
            string msgQuestStr = store.ServerURL + "/webchat/api/v1/messages";
            TextMessageContent content = new TextMessageContent(text);
            ChatMessageInfo msg = new ChatMessageInfo(store, userID, orderid, content);
            HttpResult result = hhh.Post(msgQuestStr, msg.ToJson());
            if (result.Header != null)
            {
                Console.WriteLine(result.Html);
            }
            Console.WriteLine(result.Html);
        }
    }
    public class MessageImageUpload
    {
        public string url = "";

        static public MessageImageUpload FromJsonString(string str)
        {
            MessageImageUpload img = new MessageImageUpload();
            try
            {
                img = JsonConvert.DeserializeObject<MessageImageUpload>(str);
            }
            catch(Exception xe)
            {
                Console.WriteLine(xe.Message);
            }
            return img;
        }
    }
}
