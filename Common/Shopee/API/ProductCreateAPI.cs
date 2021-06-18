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
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
       

        public ProductCreateResult CreateProducts(Store store, ProductCreateInfo[] products,string debugStr=null)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/product/create_product/?version=3.1.0&SPC_CDS=2a92fde9-0069-4758-aa01-c9b50c949904&SPC_CDS_VER=2
                //https://seller.xiapi.shopee.cn/api/v3/product/create_product/?SPC_CDS=ee3aa3f7-1f5f-451f-a2d6-8423c5d46978&SPC_CDS_VER=2
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                
                string querURL = store.ServerURL + "/api/v3/product/create_product/?version=3.1.0&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                string dataStr = ProductCreateInfo.ArrayToJson(products);
                store.Hhh.Referer = store.ServerURL + "/portal/product/new" ;
                store.Hhh.Origin = store.ServerURL;
                store.Hhh.sContentType = "application/json;charset=UTF-8";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + store.SPC_CDS.ToString() + ";";
                }
                if(debugStr != null)
                {
                    dataStr = debugStr;
                }
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Post(querURL, dataStr);
                store.Hhh.Referer = "";
                store.Hhh.Origin = "";
                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    MessageWithCode<ProductCreateResult> ret = MessageWithCode<ProductCreateResult>.FromJson(spcresult.Html);
                    if(null != ret && ret.data != null && ret.data.result != null)
                    {
                        //打印调试信息，返回成功标志
                        if(ret.code == 0)
                        {
                            Console.WriteLine(store.DisplayName + ":数据处理成功，返回结果：" + ret.message + "/" + ret.user_message);
                        }
                        else
                        {
                            Console.WriteLine(store.DisplayName + ":上传产品失败：" + spcresult.Html);
                        }

                        return ret.data;
                    }
                    Console.WriteLine(store.DisplayName + ":上传产品失败：" + spcresult.Html);
                }
                else
                {
                    Console.WriteLine(store.DisplayName + ":上传产品失败：" + spcresult.StatusCode);
                }

            }
            //返回错误标识
            return null;
        }
        public string UploadImage(Store store, string imageFile)
        {
            string id = "";
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //
                //https://seller.sg.shopee.cn/api/v1/images/?behavior=padding&aspect=1&SPC_CDS=76df83ad-422b-472f-9cc6-a925d9b967d8&SPC_CDS_VER=2
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/general/upload_image/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if(File.Exists(imageFile))
                {
                    try
                    {
                        byte[] imageBytes;
                        using (FileStream stream = new FileStream(imageFile, FileMode.Open))
                        {
                            imageBytes = new byte[stream.Length];
                            stream.Read(imageBytes, 0, (int)stream.Length);
                        }
                        store.Hhh.Referer = store.ServerURL + "/portal/product/new";

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
                            MessageWithCode<ImageUpload> imgUploadMsg = MessageWithCode<ImageUpload>.FromJson(spcresult.Html);
                            if(null != imgUploadMsg)
                            {
                                if (imgUploadMsg.code == 0)
                                {
                                    id = imgUploadMsg.data.resource_id;
                                    //打印调试信息，返回成功标志
                                    Console.WriteLine(store.UserName + ":上传图像数据成功！");
                                    return id;
                                }
                                Console.WriteLine(store.UserName + ":上传图像数据失败！" + imgUploadMsg.user_message);
                            }
                            else
                            {
                                Console.WriteLine(store.UserName + ":上传图像数据失败！" + spcresult.Html);
                            }
                        }
                        Console.WriteLine(store.UserName + ":上传图像数据失败！" + spcresult.StatusCode);
                    }
                    catch(Exception xe)
                    {
                        Console.WriteLine(store.UserName + ":上传图像数据失败！" +xe.Message);
                    }
                }

                Console.WriteLine(store.UserName + ":上传图像数据失败！文件不存在");
            }
            //返回错误标识
            return id;
        }
        /// <summary>
        /// 获取功能性能限制，https://seller.sg.shopee.cn/api/v2/feature_configs/get_configs/?SPC_CDS=b95abcdc-1d9a-4e56-82dd-5f66688c012b&SPC_CDS_VER=2
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public ProductInfoFeatureConfigs GetProductInfoFeatureConfigs(Store store)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v2/feature_configs/get_configs/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    ProductInfoFeatureConfigs configs = JsonConvert<ProductInfoFeatureConfigs>.FromJson(spcresult.Html);
                    if (null != configs)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":产品配置条件成功");
                        return configs;
                    }
                }
                Console.WriteLine(store.UserName + ":产品配置条件失败！" + spcresult.Html);
            }

            //返回错误标识
            return new ProductInfoFeatureConfigs();
        }
        /// <summary>
        /// 获取产品信息的限制条件，https://seller.xiapi.shopee.cn/api/v3/product/get_product_constraints/?SPC_CDS=6f122784-6b9c-4da5-b58f-963943d83fad&SPC_CDS_VER=2
        /// </summary>
        /// <param name="store"></param>
        /// <param name="page"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ProductInfoConstraints GetProductInfoConstraints(Store store)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/product/get_product_constraints/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    ResponseProductRequest<ProductInfoConstraints> data = ResponseProductRequest<ProductInfoConstraints>.FromJson(spcresult.Html);
                    if (null != data && null != data.data)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":产品限制条件成功");
                        return data.data;
                    }
                }
                Console.WriteLine(store.UserName + ":产品限制条件失败！" + spcresult.Html);
            }

            //返回错误标识
            return  new ProductInfoConstraints();
        }
        /// <summary>
        /// 获取所有品类的ID路径，https://seller.xiapi.shopee.cn/api/v3/category/get_leaf_category_path/?SPC_CDS=6f122784-6b9c-4da5-b58f-963943d83fad&SPC_CDS_VER=2
        /// </summary>
        /// <param name="store"></param>
        /// <param name="page"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ProductLeafCatePath GetProductLeafCatePath(Store store)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/category/get_leaf_category_path/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    ResponseProductRequest<ProductLeafCatePath> data = ResponseProductRequest<ProductLeafCatePath>.FromJson(spcresult.Html);
                    if (null != data && null != data.data)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":产品类目路径成功");
                        return data.data;
                    }
                }
                Console.WriteLine(store.UserName + ":产品类目路径成功失败！" + spcresult.Html);
            }

            //返回错误标识
            return null;
        }
        public List<ProductCategoryInfo> GetAllCategoryInfo(Store store)
        {
            List<ProductCategoryInfo> listProductCategoryInfo = new List<ProductCategoryInfo>();

            if (this.IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/category/get_all_category_list/?SPC_CDS=d07f1faf-0163-4efa-9051-467e6ec46c8e&SPC_CDS_VER=2&filter=blacklist
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/category/get_all_category_list/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&filter=blacklist";
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    DataListMessageWithCode<ProductCategoryInfo> messageData = DataListMessageWithCode<ProductCategoryInfo>.FromJson(spcresult.Html);
                    if (null != messageData && null != messageData.data && null != messageData.data.list && messageData.data.list.Length > 0)
                    {
                        foreach (ProductCategoryInfo item in messageData.data.list)
                        {
                            listProductCategoryInfo.Add(item);
                        }
                        return listProductCategoryInfo;
                    }
                }
                Console.WriteLine(store.UserName + ":产品类目路径成功失败！" + spcresult.Html);
            }
            return null;
        }
        public int[] GetDefaultCatalogPath(Store store)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/category/get_all_category_list/?SPC_CDS=d07f1faf-0163-4efa-9051-467e6ec46c8e&SPC_CDS_VER=2&filter=blacklist
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/category/get_all_category_list/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&filter=blacklist";
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null )
                {
                    DataListMessageWithCode<ProductCategoryInfo> messageData = DataListMessageWithCode<ProductCategoryInfo>.FromJson(spcresult.Html);
                    if (null != messageData && null != messageData.data && null != messageData.data.list && messageData.data.list.Length > 0)
                    {
                        bool isEnd = false;
                        List<int> defaultCata = new List<int>();
                        ProductCategoryInfo defaultCatainfo = messageData.data.list.FirstOrDefault(p =>  p.parent_id == 0);
                        if (null != defaultCatainfo)
                        {
                            int preCataId = 0;
                            defaultCata.Add(defaultCatainfo.id);
                            preCataId = defaultCatainfo.id;
                            while (!isEnd)
                            {
                                defaultCatainfo = messageData.data.list.FirstOrDefault(p => p.is_default == 1 && p.parent_id == preCataId);
                                if (null != defaultCatainfo)
                                {
                                    defaultCata.Add(defaultCatainfo.id);
                                    preCataId = defaultCatainfo.id;
                                }
                                else
                                {
                                    isEnd = true;
                                }
                            }
                            if (defaultCata.Count > 0)
                            {
                                //打印调试信息，返回成功标志
                                Console.WriteLine(store.UserName + ":产品类目路径成功");
                                return defaultCata.ToArray();
                            }
                        }
                            
                    }
                }
                Console.WriteLine(store.UserName + ":产品类目路径成功失败！" + spcresult.Html);
            }
            //返回错误标识
            return null;
        }
        /// <summary>
        /// 获取指定品类ID包含子类目的详情，https://seller.xiapi.shopee.cn/api/v3/category/get_sub_category_list/?SPC_CDS=6f122784-6b9c-4da5-b58f-963943d83fad&SPC_CDS_VER=2&category_ids=62,63,64,65,67,68,70,73,75,100,1611,1837,1859,2580,10076&fetch_depth=5
        /// </summary>
        /// <param name="store"></param>
        /// <param name="idlist">以逗号分隔的品类ID列表</param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ProductSubCategoryList GetProductSubCategoryList(Store store, string idlist)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/category/get_sub_category_list/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&category_ids=" + idlist + "&fetch_depth=5";
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    ResponseProductRequest<ProductSubCategoryList> data = ResponseProductRequest<ProductSubCategoryList>.FromJson(spcresult.Html);
                    if (null != data && null != data.data)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":产品类目信息成功");
                        return data.data;
                    }
                }
                Console.WriteLine(store.UserName + ":产品类目信息成功失败！" + spcresult.Html);
            }
            //返回错误标识
            return null;
        }
        /// <summary>
        /// 获取指定名称产品的推荐类目，https://seller.xiapi.shopee.cn/api/v2/categories/recommend/?SPC_CDS=6f122784-6b9c-4da5-b58f-963943d83fad&SPC_CDS_VER=2&title=
        /// <param name="store"></param>
        /// <param name="idlist">以逗号分隔的品类ID列表</param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public int[][] GetProductRecommendCateIdsV2(Store store, string keyword)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store) && keyword != "")
            {
                //https://seller.vn.shopee.cn/api/v3/category/get_recommend_category/?SPC_CDS=5274f7c1-679a-47a1-82d8-a62e132a8131&SPC_CDS_VER=2&name=%E8%BF%99%E6%98%AF%E4%B8%80%E4%B8%AA%E6%B5%8B%E8%AF%95%E8%80%8C%E7%94%9F%E6%88%90%E7%9A%84%E9%93%BE%E6%8E%A5%EF%BC%8C%E9%A9%AC%E4%B8%8A%E5%88%A0%E9%99%A4
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v2/categories/recommend/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&title=" + keyword;
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    ProductRecommedCateInfoV2 data = ProductRecommedCateInfoV2.FromJson(spcresult.Html);
                    if (null != data && null != data.cats_recommend)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":产品类目信息成功");
                        return data.cats_recommend;
                    }
                }
                Console.WriteLine(store.UserName + ":产品类目信息成功失败！" + spcresult.Html);
            }
            //返回错误标识
            return null;
        }
        public int[][] GetProductRecommendCateIds(Store store, string keyword)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store) && keyword != "")
            {
                //https://seller.vn.shopee.cn/api/v3/category/get_recommend_category/?SPC_CDS=5274f7c1-679a-47a1-82d8-a62e132a8131&SPC_CDS_VER=2&name=%E8%BF%99%E6%98%AF%E4%B8%80%E4%B8%AA%E6%B5%8B%E8%AF%95%E8%80%8C%E7%94%9F%E6%88%90%E7%9A%84%E9%93%BE%E6%8E%A5%EF%BC%8C%E9%A9%AC%E4%B8%8A%E5%88%A0%E9%99%A4
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/category/get_recommend_category/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&name=" + keyword;
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    MessageWithCode<ProductRecommedCateInfo>  messageData = MessageWithCode<ProductRecommedCateInfo>.FromJson(spcresult.Html);
                    if (null != messageData && null != messageData.data  && messageData.data.cats != null )
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":产品类目信息成功");
                        return messageData.data.cats;
                    }
                }
                Console.WriteLine(store.UserName + ":产品类目信息成功失败！" + spcresult.Html);
            }
            //返回错误标识
            return null;
        }
        /// <summary>
        /// 获取默认属性值，https://seller.xiapi.shopee.cn/api/v3/category/get_category_attributes/?SPC_CDS=6f122784-6b9c-4da5-b58f-963943d83fad&SPC_CDS_VER=2&category_ids=7698
        /// <param name="store"></param>
        /// <param name="idlist">以逗号分隔的品类ID列表</param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public AttributeModel GetProductCategoryAttributes(Store store,int cateId,string lang = "zh-Hant")
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v3/category/get_category_attributes/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&category_ids=" +cateId ;
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    ResponseProductRequest<ProductCateAttributes> data = ResponseProductRequest<ProductCateAttributes>.FromJson(spcresult.Html);
                    if (null != data && null != data.data)
                    {
                        AttributeModel attriModel = new AttributeModel();
                        List<ProductAttriuteItem> attriItems = new List<ProductAttriuteItem>();
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":属性信息信息成功");
                        foreach(CateAttributeModel cateAttributeModel in data.data.list)
                        {
                            attriModel.attribute_model_id = cateAttributeModel.attribute_model_id;
                            foreach(CateAttribute cateAttri in cateAttributeModel.attributes)
                            {
                                if(!cateAttri.mandatory && !cateAttri.mandatory_mall)
                                {
                                    continue;
                                }
                                ProductAttriuteItem attrItem = new ProductAttriuteItem();
                                attrItem.attribute_id = cateAttri.attribute_id;
                                 
                                attrItem.value = "No Set";
                                if(cateAttri.data != null && cateAttri.data.multi_lang.Length > 0)
                                {
                                    CateAttributeItemValue values = cateAttri.data.multi_lang.FirstOrDefault(p => p.lang == lang);
                                    if (null == values)
                                    {
                                        values = cateAttri.data.multi_lang[0];
                                    }
                                    
                                    if(null != values && values.value.Length > 0)
                                    {
                                        attrItem.value = values.value[0];
                                    }
                                }
                                attriItems.Add(attrItem);
                            }
                            break;
                        }

                        attriModel.attributes = attriItems.ToArray();
                        return attriModel;
                    }
                }
                Console.WriteLine(store.UserName + ":属性信息信息失败！" + spcresult.Html);
            }
            //返回错误标识
            return null;
        }
        //https://seller.xiapi.shopee.cn/api/v2/logisticsChannels/shipping_status/?SPC_CDS=6f122784-6b9c-4da5-b58f-963943d83fad&SPC_CDS_VER=2&channel_ids=[38020]&weight=0.45&width=150&length=30&height=15&category_id=7698
        public LogisticsChannelStatus GetProductShoppingStatusV2(Store store,long cateId, long channelid,double weight, ProductDimension dimension = null)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                //string querURL = store.ServerURL + "/api/v2/logisticsChannels/shipping_status/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&channel_ids=[" + channelid+ "]&weight="+weight+ "&width="+width+ "&length="+length+ "&category_id="+cateId;
                string querURL = store.ServerURL + "/api/v2/logisticsChannels/shipping_status/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&channel_ids=[" + channelid + "]&weight=" + weight +  "&category_id=" + cateId;
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    LogisticsChannelsStatus data = LogisticsChannelsStatus.FromJson(spcresult.Html);
                    if (null != data && null != data.channel_status && data.channel_status.Length>0)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":物流状态信息成功");
                       
                        return data.channel_status[0];
                    }
                }
                Console.WriteLine(store.UserName + "::物流状态信息失败！" + spcresult.Html);
            }

            //返回错误标识
            return null;
        }
        public LogisticsChannelStatus GetProductShoppingStatus(Store store, long cateId, long channelid, double weight, ProductDimension dimension = null)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {

            //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
            //https://seller.vn.shopee.cn/api/v3/logistics/get_shipping_status/?SPC_CDS=5274f7c1-679a-47a1-82d8-a62e132a8131&SPC_CDS_VER=2&channel_ids=58007&weight=5
                string querURL = store.ServerURL + "/api/v3/logistics/get_shipping_status/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&channel_ids=" + channelid + "&weight=" + weight ;
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    DataListMessageWithCode<LogisticsChannelStatus> messageData = DataListMessageWithCode<LogisticsChannelStatus>.FromJson(spcresult.Html);
                    if (null != messageData && null != messageData.data && null != messageData.data.list && messageData.data.list.Length > 0)
                    {
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":物流状态信息成功");
                        return messageData.data.list[0];
                    }
                }
                Console.WriteLine(store.UserName + "::物流状态信息失败！" + spcresult.Html);
            }

            //返回错误标识
            return null;
        }
        public LogisticsChannel[] GetProductLogisticsChannel(Store store,long cateid,double weight,ProductDimension dimension=null)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/logistics/get_channel_list/?SPC_CDS=0682cba5-0f4b-4a31-9675-9439f76f683d&SPC_CDS_VER=2
                //https://seller.xiapi.shopee.cn/api/v2/logisticsChannels/default/?SPC_CDS=fc2d1fa2-4676-44d5-b7c8-4c886c4c9038&SPC_CDS_VER=2

                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                //string querURL = store.ServerURL + "/api/v2/logisticsChannels/default/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                string querV3URL = store.ServerURL + "/api/v3/logistics/get_channel_list/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Get(querV3URL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    DataListMessageWithCode<ChannelInfo> messageData = DataListMessageWithCode<ChannelInfo>.FromJson(spcresult.Html);
                    if (null != messageData && null != messageData.data && null != messageData.data.list && messageData.data.list.Length  > 0)
                    {
                        List<LogisticsChannel> chanels = new List<LogisticsChannel>();
                        foreach(ChannelInfo channelInfo in messageData.data.list)
                        {
                            //if(channelInfo.limits.order_max_weight > weight)
                            if(channelInfo.enabled)
                            {
                                LogisticsChannelStatus channelStatus = GetProductShoppingStatus(store, cateid, channelInfo.channel_id, weight, dimension);
                                if(null != channelStatus)
                                {
                                    try
                                    {
                                        LogisticsChannel channel = new LogisticsChannel(channelInfo.channel_id, channelInfo.cover_shipping_fee > 0, true, "New-" + channelStatus.channel_id, channelInfo.flag, channelStatus.shipping_fee, 0.5, channelInfo.size_id);
                                        chanels.Add(channel);
                                    }
                                    catch(Exception xe)
                                    {
                                        Console.WriteLine("设置物流出错：" + channelInfo.name + xe.Message);
                                    }
                                }
                            }
                        }
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":设置物流信息成功");
                        return chanels.ToArray();
                    }
                }
                Console.WriteLine(store.UserName + "::设置物流信息失败！" + spcresult.Html);
            }
            //返回错误标识
            return new  LogisticsChannel[0];
        }
        public double GetCurrencyRate(string regionid)
        {
            //https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?query=1人民币等于多少美元&resource_id=6017
            //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
            string currencyName = RegionBaseInfo.GetRegionBaseInfo(regionid).CurrencyName;
            string querURL = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?query=1人民币等于多少" + currencyName + "&resource_id=6017";
            //调用HTTP请求，
            HtmlHttpHelper hhh = new HtmlHttpHelper();
            HttpResult spcresult = hhh.Get(querURL, "gbk");

            //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
            if (spcresult.Html != null)
            {
                CurrencyRateInfo data = JsonConvert<CurrencyRateInfo>.FromJson(spcresult.Html);
                if (null != data && null != data.data)
                {
                    foreach (CurrentRate rate in data.data)
                    {
                        if (rate.code2 == currencyName)
                        {
                            //打印调试信息，返回成功标志
                            Console.WriteLine("获取汇率成功：1人民币等于"+ rate.number2+ currencyName);
                            return double.Parse(rate.number2);
                        }
                    }
                }
            }
            Console.WriteLine("获取汇率失败！" + spcresult.Html);
            //返回错误标识
            return int.MaxValue;
        }
        class ImageUpload
        {
            public string resource_id;
        }
    }
}
