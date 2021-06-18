using CsharpHttpHelper;
using Newtonsoft.Json;
using ShopeeChat.Shopee.API.Data;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
        /// <summary>
        /// 更新产品信息
        /// https://seller.xiapi.shopee.cn/api/v3/product/update_product/?SPC_CDS=7ccf078a-1032-4f92-9808-cd90b1744e9d&SPC_CDS_VER=2
        /// </summary>
        /// <param name="store"></param>
        /// <param name="productInfo"></param>
        /// <returns></returns>
        public bool UpdateProductInfo(Store store,ProductDetailBaseInfo[] productInfos)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/product/update_product/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                string dataStr =JsonConvert.SerializeObject(productInfos) ;
              
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + store.SPC_CDS.ToString() + ";";
                }
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Post(querURL, dataStr);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":产品更新数据成功！");
                        return true;
                    
                }
                Console.WriteLine(store.UserName + ":产品更新数据失败！" + spcresult.Html);
            }
            //返回错误标识
            return false;
        }
        public bool UpdateProductInfo(Store store,string postData)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/product/update_product/?version=3.1.0&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                string dataStr = postData;
                //store.Hhh.Referer = store.ServerURL + "/portal/product/" + productid + "/";
                store.Hhh.Referer = store.ServerURL + "/portal/product/list/active";

                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + store.SPC_CDS.ToString() + ";";
                }
               // store.Hhh.Get(store.ServerURL + "/portal/product/list/all");
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Post(querURL, dataStr);

                //store.Hhh.Referer = "";
                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    //打印调试信息，返回成功标志
                    Console.WriteLine(store.UserName + ":产品下架成功！");
                    return true;

                }
                Console.WriteLine(store.UserName + ":产品下架失败！" + spcresult.Html);
            }
            //返回错误标识
            return false;
        }

        //下架产品
        //Request URL: https://seller.shopee.sg/api/v3/product/update_product/?SPC_CDS=5fc1c89c-75bc-40f6-95cb-2d7af622fec4&SPC_CDS_VER=2
        //Request Method: POST
        //[{id:2678598196,unlisted:true},{id:2678598196,unlisted:true},{id:2678598196,unlisted:true}]
        //https://seller.xiapi.shopee.cn/api/v3/product/delete_product/?version=3.1.0&SPC_CDS=e06a1277-a8b5-4a57-9786-c0d3988c927b&SPC_CDS_VER=2

        public bool DeleteProduct(Store store, string postContent)
        {
            //Request Method: POST
            //{"product_id_list": [1627679475, 1627287839]}
            if (this.IsLogin(store))
            {
                string requestDelUrl = store.ServerURL + "/api/v3/product/delete_product/?version=3.1.0&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult accessProductPage = store.Hhh.Get(store.ServerURL + "/portal/product/list/all");
                HttpResult spcresult = store.Hhh.Post(requestDelUrl, postContent);
                if (spcresult.Html.Contains("message") && spcresult.Html.Contains("success"))
                {
                    Console.WriteLine(store.DisplayName + "删除产品成功");
                    return true;
                }
            }
            return false;
        }


        public bool DismissInvalidProducts(Store store, string postContent)
        {
            //Request Method: POST
            //{"product_id_list": [1627679475, 1627287839]}
            if (this.IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/product/dismiss_invalid_products?SPC_CDS=4fd8d0f3-1120-47f8-ad2c-1b451f838b4e&SPC_CDS_VER=2
                string requestDelUrl = store.ServerURL + "/api/v3/product/dismiss_invalid_products/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                //HttpResult accessProductPage = store.Hhh.Get(store.ServerURL + "/portal/product/list/all");
                HttpResult spcresult = store.Hhh.Post(requestDelUrl, postContent);
                if (spcresult.Html.Contains("message") && spcresult.Html.Contains("success"))
                {
                    Console.WriteLine(store.DisplayName + "删除产品成功");
                    return true;
                }
            }
            return false;
        }

    }
}
