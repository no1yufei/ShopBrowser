using CsharpHttpHelper;
using Newtonsoft.Json;
using ShopeeChat.Shopee.API.Data;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
        /// <summary>
        /// 点赞指定商品
        /// https://shopee.co.th/api/v0/buyer/like/shop/42027538/item/957595327/
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
         public bool  LikeProduct(StoreRegion region,Store store,long storeid,long itemid)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = region.GetBuyerUrl() + "/api/v0/buyer/like/shop/"+ storeid + "/item/"+ itemid + "/";

                //调用HTTP请求，
                HttpResult spcresult = store.Hhh.Post(querURL,"{}","UTF-8",false);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                   
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":点赞成功！");
                        return true;
                                   }
                Console.WriteLine(store.UserName + ":点赞成功失败！" + spcresult.Html);
            }
            //返回错误标识
            return false;
        }
        
    }
}
