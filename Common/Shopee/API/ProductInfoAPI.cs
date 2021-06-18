using Common.Shopee.API.Data;
using Common.Shopee.API.Data.Product;
using CsharpHttpHelper;
using Newtonsoft.Json;
using ShopeeChat.Shopee.API.Data;
using ShopeeChat.Shopee.API.Data.Product;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
        /// <summary>
        /// 获取店铺产品的统计信息
        /// https://seller.xiapi.shopee.cn/api/v3/product/get_product_statistical_data/?SPC_CDS=7ccf078a-1032-4f92-9808-cd90b1744e9d&SPC_CDS_VER=2
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public ProductStatisticalData GetProductStatisticalData(Store store)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/product/get_product_statistical_data/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    ResponseProductRequest<ProductStatisticalData>  sum = ResponseProductRequest<ProductStatisticalData>.FromJson(spcresult.Html.Replace("\n", ""));
                    if (null != sum && sum.data != null)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":产品统计数据成功！");
                        return sum.data;
                    }
                }
                Console.WriteLine(store.UserName + ":产品统计数据失败！"+ spcresult.Html);
            }
            else
            {
                Console.WriteLine(store.UserName + ":产品统计数据失败！店铺未登录。" );
            }
            //返回错误标识
            return null;
        }
        /// <summary>
        /// 获得产品的可编辑的信息
        /// https://seller.xiapi.shopee.cn/api/v3/product/get_product_detail/?SPC_CDS=7ccf078a-1032-4f92-9808-cd90b1744e9d&SPC_CDS_VER=2&product_id=574971716
        /// </summary>
        /// <param name="store"></param>
        /// <param name="productid"></param>
        /// <returns></returns>
        public ProductDetailInfo GetProductDetailInfo(Store store, double productid)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/product/get_product_detail/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&product_id="+productid;
                //调用HTTP请求，
              
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    ResponseProductRequest<ProductDetailInfo> data = ResponseProductRequest<ProductDetailInfo>.FromJson(spcresult.Html);
                    if(null != data && null != data.data)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":获取产品详细数据成功！");
                        return data.data;
                    }
                }
                Console.WriteLine(store.UserName + ":获取产品详细数据失败！"+ spcresult.Html);
            }
            //返回错误标识
            return null;
        }
        /// <summary>
        /// 获取产品列表.https://seller.xiapi.shopee.cn/api/v3/product/page_product_list/?SPC_CDS=7ccf078a-1032-4f92-9808-cd90b1744e9d&SPC_CDS_VER=2&page_number=66&page_size=48&list_type=
        /// </summary>
        /// <param name="store"></param>
        /// <param name="page"></param>
        /// <param name="numPerPage"></param>
        /// <param name="listType">空为全部商品，live为加上商品</param>
        /// <returns></returns>
        public ProductPageListInfo GetProductPageListInfo(Store store,int page,int numPerPage = 99,string listType = "")
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/product/page_product_list/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&page_number=" + page+ "&page_size="+numPerPage+ "&list_type="+ listType;
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    ResponseProductRequest<ProductPageListInfo> data = ResponseProductRequest<ProductPageListInfo>.FromJson(spcresult.Html);
                    if (null !=data && null != data.data)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":检索产品数据成功！第"+data.data.page_info.page_number+"页，每页"+data.data.page_info.page_size+"个，共"+data.data.page_info.total+"个.");
                        return data.data;
                    }
                }
                Console.WriteLine(store.UserName + ":检索产品数据失败！" + spcresult.Html);
            }
          
            //返回错误标识
            return null;
        }

        public ProductPageListInfo GetProductPageListInfo(Store store, string listType ,int page, int numPerPage = 48)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //https://seller.shopee.co.th   /api/v3/product/page_product_list/?SPC_CDS=ebba164f-e492-4bcb-abc2-be7f82d72c14&SPC_CDS_VER=2&page_number=1&page_size=48&list_order_type=list_time_asc&search_type=name&list_type=banned&source=seller_center
                //https://seller.xiapi.shopee.cn/api/v3/product/page_product_list/?SPC_CDS=624cb2dc-59a6-474d-a1e5-7061a74f85b3&SPC_CDS_VER=2&page_number=1&page_size=24&                                               list_type=live&search_type=name&source=seller_center
                //string querURL = store.ServerURL + "/api/v3/product/page_product_list/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&page_number=" + page + "&page_size=" + numPerPage + "&list_order_type=list_time_asc&search_type=name&list_type="+ listType + "&source=seller_center";
                //https://seller.xiapi.shopee.cn/api/v3/product/page_product_list/?SPC_CDS=624cb2dc-59a6-474d-a1e5-7061a74f85b3&SPC_CDS_VER=2&page_number=1&page_size=24&list_type=unlisted&search_type=name&source=seller_center
                string querURL = store.ServerURL + "/api/v3/product/page_product_list/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&page_number=" + page + "&page_size=" + numPerPage + "&list_type=" + listType + "&search_type=name&source=seller_center";
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);
                //store.Hhh.Referer = store.ServerURL + referUrl + "page="+ page + "&size="+ numPerPage;//https://seller.shopee.co.th/portal/product/list/banned/action?page=1&size=48
                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    ResponseProductRequest<ProductPageListInfo> data = ResponseProductRequest<ProductPageListInfo>.FromJson(spcresult.Html);
                    if (null != data && null != data.data)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":检索产品数据成功！第" + data.data.page_info.page_number + "页，每页" + data.data.page_info.page_size + "个，共" + data.data.page_info.total + "个.");
                        return data.data;
                    }
                }
                Console.WriteLine(store.UserName + ":检索产品数据失败！" + spcresult.Html);
            }

            //返回错误标识
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        /// <param name="searchType">选项有：name,sku,msku,variation,id</param>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ProductPageListInfo SearchProductPageListInfo(Store store, string searchType, string keyword,string list_type="", int page = 1, int numPerPage = 96)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                if(keyword==null || keyword=="")
                {
                    return GetProductPageListInfo(store, page, numPerPage, list_type);
                }
                //https://seller.xiapi.shopee.cn/api/v3/product/search_product_list/?SPC_CDS=b6c1adba-7031-47eb-9ae8-c17c2a92fddb&SPC_CDS_VER=2&page_number=1&page_size=24&list_type=&search_type=name&keyword=D&version=3.2.0
                string querURL = store.ServerURL + "/api/v3/product/search_product_list/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&page_number=" + page + "&page_size=" + numPerPage + "&list_type="+ list_type + "&search_type="+searchType+ "&keyword="+keyword + "&version=3.2.0";
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null)
                {
                    MessageWithCode<ProductPageListInfo> msg = MessageWithCode<ProductPageListInfo>.FromJson(spcresult.Html);
                    if (null != msg && null != msg.data)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":检索产品数据成功！第" + msg.data.page_info.page_number + "页，每页" + msg.data.page_info.page_size + "个，共" + msg.data.page_info.total + "个.");
                        return msg.data;
                    }
                }
                Console.WriteLine(store.UserName + ":检索产品数据失败！" + spcresult.Html);
            }

            //返回错误标识
            return null;
        }
        public ProductPageListInfo GetProductListFromProductIds(Store store,List<string> ids)
        {
            //https://seller.xiapi.shopee.cn/api/v3/product/get_product_list/?product_ids=5506218603,5705783295,5904096087,6206660066&SPC_CDS=1fd9176b-7099-4aa3-838d-b708b4073517&SPC_CDS_VER=2
            if (this.IsLogin(store)&&ids!=null&&ids.Count>0)
            {
                string productIds = "";// String.Join(",", ids.ToArray());
                foreach (string id in ids)
                {
                    productIds = productIds + id.Trim() + ",";
                }
                productIds = Regex.Replace(productIds, ",$", "");
                string querURL = store.ServerURL + "/api/v3/product/get_product_list/?product_ids=" + productIds + "&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    ResponseProductRequest<ProductPageListInfo> data = ResponseProductRequest<ProductPageListInfo>.FromJson(spcresult.Html);
                    if (null != data && null != data.data)
                    {
                        Console.WriteLine(store.UserName + "获取产品信息成功: " + productIds);
                        return data.data;
                    }
                }
                Console.WriteLine(store.UserName + ":GetProductListFromProductIds产品数据失败！" + spcresult.Html);
            }
            return null;
        }
        //https://seller.th.shopee.cn/api/v3/logistics/get_product_channels/?version=3.1.0&SPC_CDS=b514ff52-9521-40d1-a6c9-c582852cf1fd&SPC_CDS_VER=2&product_id=5362018894
        public LogisticsChannelList GetProductLogisticsChannels(Store store,long productId)
        {
            //LogisticsChannel lc = null;
            if (this.IsLogin(store) && productId > 0)
            {                
                string querURL = store.ServerURL + "/api/v3/logistics/get_product_channels/?product_id=" + productId + "&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    string jsonString = spcresult.Html.Replace("channel_id", "channelid").Replace("size_id", "sizeid");
                    ResponseProductRequest<LogisticsChannelList> data = ResponseProductRequest<LogisticsChannelList>.FromJson(jsonString);
                    if (null != data&& data.data!=null)
                    {
                        Console.WriteLine(store.UserName + "GetProductLogisticsChannels产品物流信息成功: " + productId);
                        return data.data;
                    }
                }
                Console.WriteLine(store.UserName + "GetProductLogisticsChannels产品物流失败！" + spcresult.Html);
            }
            return null;
        }
    }
}
