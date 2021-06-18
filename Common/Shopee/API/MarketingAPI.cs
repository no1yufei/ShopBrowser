using Common.Shopee.API.Data;
using CsharpHttpHelper;
using Newtonsoft.Json;
using ShopeeChat.Shopee.API.Data;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Shopee.API.Data.BundleDeals;
using static Common.Shopee.API.Data.DiscountList;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
        //public bool IsLogin(Store store)
        //{
        //    if (store.Token != null && store.Token != string.Empty)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        public bool DeleteProductDiscount(Store store,long discountID,long productID)
        {
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                // https://seller.xiapi.shopee.cn/api/marketing/v3/discount/nominate/?SPC_CDS=3771811b-e68f-41f5-87ae-7901b89b2a2f&SPC_CDS_VER=2
                // https://seller.xiapi.shopee.cn/api/marketing/v3/discount/nominate/?SPC_CDS=3ee31950-765e-4772-9663-3c99436a67cd&SPC_CDS_VER=2 
                string deletedURL = store.ServerURL + "/api/marketing/v3/discount/nominate/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + store.SPC_CDS.ToString() + ";";
                }
                //调用HTTP请求，
                store.Hhh.Origin = store.ServerURL;
                store.Hhh.Referer = store.ServerURL + "/portal/marketing/discount/" + discountID+"/";
                string deleteProduct = "{\"discount_id\":" + discountID + ", \"itemid\":" + productID + "}";
                
                HttpResult spcresult = store.Hhh.Delete(deletedURL, deleteProduct,"UTF-8");
                store.Hhh.Referer = "";
                store.Hhh.Origin = "";
                if (spcresult.Html != null && spcresult.Html.Contains("{}"))
                {
                    Console.WriteLine(store.UserName + ":删除折扣商品成功！" + productID);
                    return true;
                }
                Console.WriteLine(store.UserName + ":删除折扣商品失败！" + spcresult.Html);
            }
            return false;
        }
        public bool DeleteProductDiscount(Store store,long productID)
        {
            if (this.IsLogin(store))
            {
                ProductDetailInfo detailInof = GetProductDetailInfo(store, productID);
                if(detailInof!=null&&detailInof.in_promotion)
                {
                    long promotionId = 0;
                    if (detailInof.self_discount_id > 0)
                    {
                        promotionId = detailInof.self_discount_id;
                    }
                    else if (detailInof.model_list.Length > 0)
                    {
                        promotionId = detailInof.model_list[0].promotion_id;
                    }
                    DeleteProductDiscount(store, promotionId, detailInof.id);
                }
                
            }
            return false;
        }
        public DiscountList GetDiscountList(Store store,int page)
        {
            if (this.IsLogin(store))
            {
                int limit = 100;
                int offset = page * limit;
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                //Request URL: https://seller.xiapi.shopee.cn/api/marketing/v3/discount/list/?SPC_CDS=3771811b-e68f-41f5-87ae-7901b89b2a2f&SPC_CDS_VER=2&limit=100&offset=0&discount_type=ongoing
                //Request Method: GET
                //referer: https://seller.shopee.co.th/portal/marketing/list/discount?type=ongoing
                string querURL = store.ServerURL + "/api/marketing/v3/discount/list/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&limit="+ limit + "&offset="+offset+ "&discount_type=ongoing";
                //string referURL = store.ServerURL + "/portal/marketing/list/discount?type=ongoing";
                //调用HTTP请求，
                store.Hhh.Get(store.ServerURL + "/portal/marketing/list/discount?type=ongoing");
                HttpResult result = store.Hhh.Get(querURL);
                if (result.Html != null)
                {
                    MessageWithCode<DiscountList> msgDlist = MessageWithCode<DiscountList>.FromJson(result.Html);
                    if(null != msgDlist && null != msgDlist.data && msgDlist.data.total_count > 0)
                    return msgDlist.data;
                }
                Console.WriteLine(store.UserName + "获取打折信息失败！" + result.Html);
            }
            return null;
        }

        public DiscountProductList GetDiscountProductListInfo(Store store,string discountID, int page)
        {
            if (this.IsLogin(store))
            {
                int limit = 100;
                int offset = page * limit;
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                //https://seller.xiapi.shopee.cn/api/marketing/v3/discount/nominate/?SPC_CDS=3ee31950-765e-4772-9663-3c99436a67cd&SPC_CDS_VER=2&offset=0&limit=1&discount_id=1059804959
                //Request URL: https://seller.shopee.co.th/api/v2/discounts/1036754803/nominate/?SPC_CDS=45dffc33-ea73-4ae1-8f2e-944718220421&SPC_CDS_VER=2&discountId=1036754803&offset=0&limit=100
                //Request Method: GET
                //referer: https://seller.shopee.co.th/portal/marketing/discount/1036754803
                string queryURL = store.ServerURL + "/api/marketing/v3/discount/nominate/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&discount_id=" + discountID + "&offset="+ offset + "&limit="+ limit;
                
                //调用HTTP请求，
                store.Hhh.Get(store.ServerURL + "/portal/marketing/discount/" + discountID);
                HttpResult result = store.Hhh.Get(queryURL);
                if (result.Html != null )
                {
                    MessageWithCode<DiscountProductList>  dpl = MessageWithCode<DiscountProductList>.FromJson(result.Html);
                    if(null != dpl && null != dpl.data)
                    {
                        return dpl.data;
                    }
                    
                }
                Console.WriteLine(store.UserName + "获取折扣商品详情失败！" + result.Html);
            }
            return null;
        }

        public ShopeeDiscountItem CreateDiscountItem(Store store, String name, DateTime startTime, DateTime endTime)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/marketing/v3/discount/?SPC_CDS=3946910a-6dec-4880-a538-8cb8057fc3a9&SPC_CDS_VER=2
                string querURL = store.ServerURL + "/api/marketing/v3/discount/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";

                //组装数据，如果有，这里没有
                startTime = startTime > DateTime.Now.AddSeconds(10) ? startTime : DateTime.Now.AddSeconds(10);
                endTime = endTime > startTime ? endTime : startTime.AddHours(1);
                if ((endTime - startTime).Days >= 180)
                {
                    endTime = startTime.AddDays(179);
                }
                string data = "{\"fe_status\":\"upcoming\",\"highlight\":\"\",\"title\":\"" + name + "\",\"start_time\":" + Tool.GetUTCTimeStampSeconds(startTime) + ",\"end_time\":" + Tool.GetUTCTimeStampSeconds(endTime) + ",\"status\":1}";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + store.SPC_CDS.ToString() + ";";
                }
                
                //调用HTTP请求，这里是Get请求，传入组装的URL，HttpResult是返回的结果， store.Hhh.bError是根据HTTP状态码判断返回是否有错误的标志，具体需要和
                //业务结合，根据数据来判断。
                store.Hhh.Referer = store.ServerURL + "/portal/marketing/discount/create";
                store.Hhh.Origin = store.ServerURL;
                HttpResult spcresult = store.Hhh.Post(querURL, data);
                store.Hhh.Referer = "";
                store.Hhh.Origin = "";

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    MessageWithCode<ShopeeDiscountItem> msgItem = MessageWithCode<ShopeeDiscountItem>.FromJson(spcresult.Html);
                    if (null != msgItem && msgItem.code == 0 )
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.DisplayName + ":创建打折项目成功！");
                        msgItem.data.end_time = Tool.GetUTCTimeStampSeconds(endTime);
                        msgItem.data.start_time = Tool.GetUTCTimeStampSeconds(startTime);
                        msgItem.data.status = 1;
                        msgItem.data.title = name;
                        return msgItem.data;
                    }
                }
            }
            //返回错误标识
            return null;
        }
        public bool UpdateProductDiscount(Store store, long promotionId, long productid, int discount, int limitNum = 0)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store) && promotionId > 0)
            {
                ProductDetailInfo detailInfo = GetProductDetailInfo(store, productid);
                if(detailInfo.in_promotion)
                {
                    if(detailInfo.self_discount_id > 0)
                    {
                        DeleteProductDiscount(store, detailInfo.self_discount_id, productid);
                    }
                    else if(detailInfo.model_list.Length > 0)
                    {
                        DeleteProductDiscount(store, detailInfo.model_list[0].promotion_id, productid);
                    }
                }

            //https://seller.xiapi.shopee.cn/api/marketing/v3/discount/nominate/?SPC_CDS=3ee31950-765e-4772-9663-3c99436a67cd&SPC_CDS_VER=2
                //https://seller.my.shopee.cn/api/v2/discounts/1036913587/nominate/?SPC_CDS=daca40f3-64d0-4477-a0db-72bdab4f95f7&SPC_CDS_VER=2
                string querURL = store.ServerURL + "/api/marketing/v3/discount/nominate/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";

                List<DiscountModel> modelList = new List<DiscountModel>();
                
                foreach (ProductModel productModel in detailInfo.model_list)
                {
                    DiscountModel discountModel = new DiscountModel();
                    discountModel.itemid = productid;
                    discountModel.modelid = productModel.id;
                    discountModel.promotion_price = Math.Round(double.Parse(productModel.price) * (100 - discount) / 100, 2).ToString();
                    discountModel.user_item_limit = limitNum;
                    discountModel.status = 1;
                    modelList.Add(discountModel);
                }
                if(detailInfo.model_list.Count() < 1)
                {
                    DiscountModel discountModel = new DiscountModel();
                    discountModel.itemid = productid;
                    discountModel.modelid = 0;
                    discountModel.promotion_price = Math.Round(double.Parse(detailInfo.price) * (100 - discount) / 100, 2).ToString();
                    discountModel.user_item_limit = limitNum;
                    discountModel.status = 1;
                    modelList.Add(discountModel);
                }
                DiscountModels models = new DiscountModels();
                models.discount_model_list = modelList;
                models.discount_id = promotionId;
               
                //组装数据，如果有，这里没有
                string data = JsonConvert.SerializeObject(models); ;//= string.Format("{\"discount\":{\"title\":\"{0}\",\"start_time\":{1},\"end_time\":{2}}}", name, Tool.GetTimeStampSeconds(startTime), Tool.GetTimeStampSeconds(endTime));
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + store.SPC_CDS.ToString() + ";";
                }
                ////调用HTTP请求，这里是Get请求，传入组装的URL，HttpResult是返回的结果， store.Hhh.bError是根据HTTP状态码判断返回是否有错误的标志，具体需要和
                ////业务结合，根据数据来判断。
                store.Hhh.Referer = store.ServerURL + "/portal/marketing/discount/" + promotionId;
                store.Hhh.Origin = store.ServerURL;
                HttpResult spcresult = store.Hhh.Put(querURL, data);
                store.Hhh.Referer = "";
                store.Hhh.Origin = "";

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    MessageWithCode<DiscountMsg> dmsg = MessageWithCode<DiscountMsg>.FromJson(spcresult.Html);
                    //打印调试信息，返回成功标志
                    if(dmsg != null && dmsg.data != null && dmsg.data.success > 0)
                    {
                        Console.WriteLine(store.DisplayName + ":商品打折成功！");
                        if(dmsg.data.success < dmsg.data.total)
                        {
                            Console.WriteLine(store.DisplayName + ":商品打折成功！但有部分失败："+ spcresult.Html);
                        }
                        return true;
                    }
                    
                }
                    Console.WriteLine(store.DisplayName + ":商品打折失败！错误信息:"+ spcresult.Html);
            }
            //返回错误标识
            return false;
        }
        public class DiscountMsg
        {
            public int total;//":32,"
            public int success;//":32
        }

        public ShopeeDiscountItem GetDiscountItem(Store store)
        {
            global::Common.Shopee.API.Data.ShopeeDiscountItem item = null;

            if (this.IsLogin(store))
            {
                string prix = "店聊通自动打折";
                DiscountList discounts = GetDiscountList(store, 0);
                if (null != discounts && discounts.discount_list.Count > 0)
                {
                    long maxStartTime = 0;
                    foreach (ShopeeDiscountItem p in discounts.discount_list)
                    {
                        if(p.start_time > maxStartTime)
                        {
                            item = null;
                        }
                        if (p.title.Contains(prix)
                            && p.total_product < 900
                            && p.end_time > Tool.GetUTCTimeStampSeconds(DateTime.Now.AddMonths(5))
                            && p.start_time>maxStartTime
                          )
                        {
                            item = p;
                                maxStartTime = item.start_time;
                        }
                    }
                }
                if (null == item)
                {
                    int i = 0;
                    while (null != discounts && null != discounts.discount_list.FirstOrDefault(p => p.title.Contains(prix + i)))
                    {
                        i++;
                    }
                    string name = prix + i;
                    item = CreateDiscountItem(store, name, DateTime.Now, DateTime.Now.AddYears(5));
                }
            }
            return item;
        }

        public bool MisleadingDiscount(Store store,string promotionId, ItemModelIdList itemmodelids)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
               //https://seller.xiapi.shopee.cn/api/v2/marketing/misleading_discount/?SPC_CDS=8e63f9c6-df25-4499-956f-15200a7d2d81&SPC_CDS_VER=2 
                string querURL = store.ServerURL + "/api/v2/marketing/misleading_discount/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                List<DiscountModel> modelList = new List<DiscountModel>();

                //组装数据，如果有，这里没有
                string data = JsonConvert.SerializeObject(itemmodelids); ;//= string.Format("{\"discount\":{\"title\":\"{0}\",\"start_time\":{1},\"end_time\":{2}}}", name, Tool.GetTimeStampSeconds(startTime), Tool.GetTimeStampSeconds(endTime));
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + store.SPC_CDS.ToString() + ";";
                }
                ////调用HTTP请求，这里是Get请求，传入组装的URL，HttpResult是返回的结果， store.Hhh.bError是根据HTTP状态码判断返回是否有错误的标志，具体需要和
                ////业务结合，根据数据来判断。
                store.Hhh.Referer = store.ServerURL + "/portal/marketing/discount/" + promotionId;
                store.Hhh.Origin = store.ServerURL;
                HttpResult spcresult = store.Hhh.Post(querURL, data);
                store.Hhh.Referer = "";
                store.Hhh.Origin = "";

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    //打印调试信息，返回成功标志
                    Console.WriteLine(store.DisplayName + ":MisleadingDiscount！");
                    return true;
                }
                else
                {
                    Console.WriteLine(store.DisplayName + ":商品打折失败！错误信息:" + spcresult.Html);
                }
            }
            //返回错误标识
            return false;
        }
        public BundleDeals GetBundleDeals(Store store,int limit = 99,int offset = 0)
        {
            if (this.IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v2/bundle_deals/?SPC_CDS=d44ca3bf-49c6-4459-9205-bd48b245004c&SPC_CDS_VER=2&status=3&limit=10&offset=0
                string queryURL = store.ServerURL + "/api/v2/bundle_deals/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&status=3&limit=" + limit + "&offset=" + offset;
                //调用HTTP请求，
                HttpResult result = store.Hhh.Get(queryURL);
                if (result.Html != null && result.Html.Contains("bundle-deals"))
                {
                    BundleDeals db = JsonConvert.DeserializeObject<BundleDeals>(result.Html.Replace("bundle-deals", "bundle_deals"));
                    return db;
                }
                Console.WriteLine(store.DisplayName + "获取捆绑销售商品信息失败！" + result.Html);
            }
            return null;
        }

        public void DeleteBundleDealsProduct(Store store,List<string> productId)
        {
            //https://seller.xiapi.shopee.cn/api/v2/bundle_deals/949490/items/batch/?SPC_CDS=d44ca3bf-49c6-4459-9205-bd48b245004c&SPC_CDS_VER=2
            BundleDeals bd = GetBundleDeals(store, 99, 0);
            if (bd != null)
            {
                foreach (Bundle_dealsItem bdi in bd.bundle_deals)
                {
                    long bd_id = bdi.id;
                    string deleteURL = store.ServerURL + "/api/v2/bundle_deals/"+ bd_id + "/items/batch/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                    if(bdi.extinfo != null && bdi.extinfo.itemid_list != null)
                    {
                        foreach (long productID in bdi.extinfo.itemid_list)
                        {
                            foreach (string pid in productId)
                            {
                                if (pid.Equals(productID))
                                {
                                    string deleteString = @"{""item_ids"":[" + productID + "]}";
                                    store.Hhh.Origin = store.ServerURL;
                                    store.Hhh.Referer = store.ServerURL + "/portal/marketing/bundle/" + bd_id;
                                    HttpResult result = store.Hhh.Delete(deleteURL, deleteString);
                                    store.Hhh.Referer = "";
                                    store.Hhh.Origin = "";
                                    if (result.Html != null && result.Html.Contains("{}"))
                                    {
                                        Console.WriteLine(store.UserName + ":删除绑定销售商品成功！" + productID);
                                    }
                                    else
                                    {
                                        Console.WriteLine(store.UserName + ":删除绑定销售商品失败！" + result.Html);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public bool DeleteBundleDealsProduct(Store store, long productId, long bundleId)
        {
            if (this.IsLogin(store))
            {
                string deleteURL = store.ServerURL + "/api/v2/bundle_deals/" + bundleId + "/items/batch/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                string deleteString = @"{""item_ids"":[" + productId + "]}";
                store.Hhh.Origin = store.ServerURL;
                store.Hhh.Referer = store.ServerURL + "/portal/marketing/bundle/" + bundleId;
                HttpResult result = store.Hhh.Delete(deleteURL, deleteString);
                store.Hhh.Referer = "";
                store.Hhh.Origin = "";
                if (result.Html != null && result.Html.Contains("{}"))
                {
                    Console.WriteLine(store.UserName + ":删除绑定销售商品成功！" + productId);
                    return true;
                }
                else
                {
                    Console.WriteLine(store.UserName + ":删除绑定销售商品失败！" + result.Html);
                }
            }
            return false;
        }
    }
}

