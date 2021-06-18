using Common.Shopee.API.Data;
using CsharpHttpHelper;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
        public void GetCustomerList(Store storeInfo)
        {
            //https://seller.shopee.co.id/webchat/api/v1/conversations?next_timestamp=-1&direction=older
            HtmlHttpHelper hhh = storeInfo.Hhh;
            //https://seller.shopee.co.th/webchat/api/v1/conversations?next_timestamp=-1&direction=older
            string customerQuestStr = storeInfo.ServerURL + "/webchat/api/v1/conversations?next_timestamp=-1&direction=older";

            HttpResult result = hhh.Get(customerQuestStr, "UTF-8");
            if (result.Header != null && null != result.Html)
            {
                storeInfo.CustomerInfos = ShopCustomerInfo.ListFromJson(result.Html);
            }
        }

        public List<ShopCustomerInfo> GetCustomerList(Store storeInfo,long next_timestamp = -1)
        {
            //https://seller.shopee.co.id/webchat/api/v1/conversations?next_timestamp=-1&direction=older
            HtmlHttpHelper hhh = storeInfo.Hhh;
            //https://seller.shopee.co.th/webchat/api/v1/conversations?next_timestamp=-1&direction=older
            int num = 150;
            List<ShopCustomerInfo> customers = new List<ShopCustomerInfo>();
            string customerQuestStrPatten  = storeInfo.ServerURL + "/webchat/api/v1/conversations?next_timestamp={0}&direction=older";
            while(num >= 150)
            {
                num = 0;
                HttpResult result = hhh.Get(customerQuestStrPatten.Replace("{0}", next_timestamp.ToString()), "UTF-8");
                if (result.Header != null && null != result.Html)
                {
                    List<ShopCustomerInfo> curCustoms  =  ShopCustomerInfo.ListFromJson(result.Html);
                    num = curCustoms.Count;
                    customers.AddRange(curCustoms);
                    next_timestamp = curCustoms[curCustoms.Count - 1].next_timestamp;
                }
            }
            
            return customers;
        }
        public bool GetSummaryData(Store store)
        {
            if (store.Token != null && store.Token != string.Empty && store.SPC_CDS != null)
            {
                if (store.LastUpdateSummaryTime != null && (DateTime.UtcNow - store.LastUpdateSummaryTime).TotalMinutes < 60)
                {
                    return true;
                }
                long endtime = Tool.GetTimeStampSeconds() / 3600 * 3600 - 28800;
                long starttime = (endtime / (3600 * 24)) * 3600 * 24 - 28800;

                string querURL = store.ServerURL + "/api/mydata/v1/metrics/shop/summary/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&start_time="
                    + starttime + "&end_time=" + endtime + "&period=real_time";
                HttpResult spcresult = store.Hhh.Get(querURL);

                if (spcresult.Html != null && spcresult.Html.Contains("value"))
                {
                    ShopSummaryInfo sum = ShopSummaryInfo.FromJson(spcresult.Html.Replace("\n", ""));
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
        public ShopSummaryInfo GetSummaryData(Store store,int period)
        {
            if (period > 3 || period < 0)
            {
                return null;
            }
            long endtime = Tool.GetTimeStampSeconds() / 3600 * 3600 - 28800;//当前时间
            long todaytime = (endtime / (3600 * 24)) * 3600 * 24 - 28800;//今天开始时间
            string querURL = store.ServerURL + "/api/mydata/v1/metrics/shop/summary/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
            if (period == 0)
            {
                querURL = querURL + "&start_time=" + todaytime + "&end_time=" + endtime + "&period=real_time";
            }
            else if (period == 1)
            {
                long starttime = Tool.GetTimeStampSeconds(DateTime.Now.AddDays(-1)) / 3600 * 3600 - 28800;
                starttime = (starttime / (3600 * 24)) * 3600 * 24 - 28800;
                querURL = querURL + "&start_time=" + starttime + "&end_time=" + todaytime + "&period=yesterday";
            }
            else if (period == 2)
            {
                long starttime = Tool.GetTimeStampSeconds(DateTime.Now.AddDays(-7)) / 3600 * 3600 - 28800;
                starttime = (starttime / (3600 * 24)) * 3600 * 24 - 28800;
                querURL = querURL + "&start_time=" + starttime + "&end_time=" + todaytime + "&period=past7days";
            }
            else if (period == 3)
            {
                long starttime = Tool.GetTimeStampSeconds(DateTime.Now.AddDays(-30)) / 3600 * 3600 - 28800;
                starttime = (starttime / (3600 * 24)) * 3600 * 24 - 28800;
                querURL = querURL + "&start_time=" + starttime + "&end_time=" + todaytime + "&period=past30days";
            }
            HttpResult spcresult = store.Hhh.Get(querURL);
            if (spcresult.Html != null && spcresult.Html.Contains("value"))
            {
                ShopSummaryInfo sum = ShopSummaryInfo.FromJson(spcresult.Html.Replace("\n", ""));
                return sum;
            }
            return null;
        }

        public ShopCustomersInfo GetCustomersData(Store store, int period)
        {
            if (period > 3 || period < 0)
            {
                return null;
            }
            long endtime = Tool.GetTimeStampSeconds() / 3600 * 3600 - 28800;//当前时间
            long todaytime = (endtime / (3600 * 24)) * 3600 * 24 - 28800;//今天开始时间
            string querURL = store.ServerURL + "/api/mydata/v1/metrics/shop/customers/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
            if (period == 0)
            {
                querURL = querURL + "&start_time=" + todaytime + "&end_time=" + endtime + "&period=real_time";
            }
            else if (period == 1)
            {
                long starttime = Tool.GetTimeStampSeconds(DateTime.Now.AddDays(-1)) / 3600 * 3600 - 28800;
                starttime = (starttime / (3600 * 24)) * 3600 * 24 - 28800;
                querURL = querURL + "&start_time=" + starttime + "&end_time=" + todaytime + "&period=yesterday";
            }
            else if (period == 2)
            {
                long starttime = Tool.GetTimeStampSeconds(DateTime.Now.AddDays(-7)) / 3600 * 3600 - 28800;
                starttime = (starttime / (3600 * 24)) * 3600 * 24 - 28800;
                querURL = querURL + "&start_time=" + starttime + "&end_time=" + todaytime + "&period=past7days";
            }
            else if (period == 3)
            {
                long starttime = Tool.GetTimeStampSeconds(DateTime.Now.AddDays(-30)) / 3600 * 3600 - 28800;
                starttime = (starttime / (3600 * 24)) * 3600 * 24 - 28800;
                querURL = querURL + "&start_time=" + starttime + "&end_time=" + todaytime + "&period=past30days";
            }
            HttpResult spcresult = store.Hhh.Get(querURL);
            if (spcresult.Html != null && spcresult.Html.Contains("repeat_purchase_rate_change"))
            {
                ShopCustomersInfo sum = ShopCustomersInfo.FromJson(spcresult.Html.Replace("\n", ""));
                return sum;
            }
            return null;
        }

        public ProductShowInfo GetMetricsItems(Store store, int period,int offset,string sort_by= "pv.desc")
        {
            if (period > 3 || period < 0)
            {
                return null;
            }
            if (offset < 0 || offset > 50)
            {
                return null;
            }
            long endtime = Tool.GetTimeStampSeconds() / 3600 * 3600 - 28800;//当前时间
            long todaytime = (endtime / (3600 * 24)) * 3600 * 24 - 28800;//今天开始时间
            string querURL = store.ServerURL + "/api/mydata/v1/metrics/items/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";            //&release_time=2019-11-28
            if (period == 0)
            {
                querURL = querURL + "&start_time=" + todaytime + "&end_time=" + endtime + "&period=real_time&metric_ids=all&sort_by="+ sort_by + "&offset=" + offset + "&limit=18"; 
            }
            else if (period == 1)
            {
                long starttime = Tool.GetTimeStampSeconds(DateTime.Now.AddDays(-1)) - 28800;
                starttime = (starttime / (3600 * 24)) * 3600 * 24 - 28800;
                querURL = querURL + "&start_time=" + starttime + "&end_time=" + todaytime + "&period=yesterday&metric_ids=all&sort_by" + sort_by + "&offset=" + offset + "&limit=18";
            }
            else if (period == 2)
            {
                long starttime = Tool.GetTimeStampSeconds(DateTime.Now.AddDays(-7)) - 28800;
                starttime = (starttime / (3600 * 24)) * 3600 * 24 - 28800;
                querURL = querURL + "&start_time=" + starttime + "&end_time=" + todaytime + "&period=past7days&metric_ids=all&sort_by=" + sort_by + "&offset=" + offset + "&limit=18";
            }
            else if (period == 3)
            {
                long starttime = Tool.GetTimeStampSeconds(DateTime.Now.AddDays(-30)) - 28800;
                starttime = (starttime / (3600 * 24)) * 3600 * 24 - 28800;
                querURL = querURL + "&start_time=" + starttime + "&end_time=" + todaytime + "&period=past30days&metric_ids=all&sort_by=" + sort_by + "&offset=" + offset + "&limit=18";
            }
            //querURL = store.ServerURL + "/api/mydata/v1/metrics/items/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&start_time=1569081600&end_time=1571673600&period=past30days&metric_ids=all&sort_by=pv.desc&offset=0&limit=18";
            HttpResult spcresult = store.Hhh.Get(querURL);
            if (spcresult.Html != null && spcresult.Html.Contains("total items"))
            {

                ProductShowInfo psi = ProductShowInfo.FromJson(spcresult.Html.Replace("\n", "").Replace("total items", "total_items"));
                return psi;
            }

            return null;
        }
        public StoreInfo GetStoreDetailInfo(Store store)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //这里业务上的刷新逻辑，按照实际业务逻辑自行编写
                //https://seller.xiapi.shopee.cn/api/v3/general/get_shop/?SPC_CDS=3ee31950-765e-4772-9663-3c99436a67cd&SPC_CDS_VER=2
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/general/get_shop/?SPC_CDS=/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                //组装数据，如果有，这里没有

                //调用HTTP请求，这里是Get请求，传入组装的URL，HttpResult是返回的结果， store.Hhh.bError是根据HTTP状态码判断返回是否有错误的标志，具体需要和
                //业务结合，根据数据来判断。
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null )
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    MessageWithCode<StoreInfo> msgStoreInfo = MessageWithCode<StoreInfo>.FromJson(spcresult.Html);
                    if (null != msgStoreInfo && msgStoreInfo.code ==0)
                    {
                        Console.WriteLine(store.DisplayName + ":用户信息取得成功！");
                        return msgStoreInfo.data;
                    }
                    Console.WriteLine(store.DisplayName + ":用户信息取得失败！"+ spcresult.Html);
                }
            }
            //返回错误标识
            return null;
        }
        public List<SIPStoreInfo> GetSIPStoreDetailInfo(Store store)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //这里业务上的刷新逻辑，按照实际业务逻辑自行编写
                //https://seller.xiapi.shopee.cn/api/sip/v2/shops/primary/?SPC_CDS=3ee31950-765e-4772-9663-3c99436a67cd&SPC_CDS_VER=2
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/sip/v2/shops/primary/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                //组装数据，如果有，这里没有

                //调用HTTP请求，这里是Get请求，传入组装的URL，HttpResult是返回的结果， store.Hhh.bError是根据HTTP状态码判断返回是否有错误的标志，具体需要和
                //业务结合，根据数据来判断。
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    MessageWithCode<SIPSotreInfoResultResp> msgStoreInfo = MessageWithCode<SIPSotreInfoResultResp>.FromJson(spcresult.Html);
                    if (null != msgStoreInfo && msgStoreInfo.code == 0)
                    {
                        Console.WriteLine(store.DisplayName + ":SIP用户信息取得成功！");
                        return msgStoreInfo.data.result.affi_shops;
                    }
                    Console.WriteLine(store.DisplayName + ":SIP用户信息取得失败！" + spcresult.Html);
                }
            }
            //返回错误标识
            return null;
        }
    }
}
