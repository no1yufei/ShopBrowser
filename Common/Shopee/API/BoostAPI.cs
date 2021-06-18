using CsharpHttpHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    //类名不要修改，所有API都是ShopeeAPI类下的方法，调用方式都一样，New ShopeeAPI，然后用实例调用方法
    public partial class ShopeeAPI
    {

        public List<string> GetBoostProductIdList(Store store)
        {
            List<string> productIdList = new List<string>();
            if (this.IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/product/get_boost_product_id_list/?SPC_CDS=2cd4c123-d235-4197-8e06-b4ff6e241a66&SPC_CDS_VER=2&version=3.1.0
                string queryURL = store.ServerURL + "/api/v3/product/get_boost_product_id_list/?SPC_CDS=" + store.SPC_CDS + "&SPC_CDS_VER=2&version=3.1.0";
                HttpResult boostListHr = store.Hhh.Get(queryURL);
                if (boostListHr.Html != null && boostListHr.Html.Contains("data") && boostListHr.Html.Contains("list"))
                {
                    JObject jo = (JObject)JsonConvert.DeserializeObject(boostListHr.Html);
                    JArray ja = (JArray)jo["data"]["list"];
                    for (int i = 0; i < ja.Count; i++)
                    {
                        string productId = (string)ja[i];
                        productIdList.Add(productId);
                    }
                }
            }
            return productIdList;
        }


        /// <summary>
        /// 产品置顶
        /// </summary>
        /// <param name="store"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int BoostProduct(Store store,long itemId)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //置顶
                //记录
                String postdata = "{\"id\":" + itemId + "}";
               
                string queryURL = store.ServerURL + "/api/v3/product/boost_product/?SPC_CDS=" + store.SPC_CDS + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + store.SPC_CDS.ToString() + ";";
                }
        
                //HttpResult accessProductPage = store.Hhh.Get(store.ServerURL + "/portal/product/list/all");
                HttpResult spcresult = store.Hhh.Post(queryURL, postdata);// si.Hhh.Put(querURL, data);
                                                                       //Console.WriteLine(si.Hhh.Authorization);
                                                                       //Console.WriteLine(si.Hhh.Referer);
                                                                       //Console.WriteLine(si.Hhh.org);
                if (spcresult.Html.Contains("code")
                    && spcresult.Html.Contains("320302")
                    && spcresult.Html.Contains("boost")
                    && spcresult.Html.Contains("limit"))
                {
                    return 2;
                }
                Console.WriteLine(spcresult.Html);
                //Console.WriteLine("关注：" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    return 0;
                }
            }
            //返回错误标识
            return 1;
        }
    }
}
