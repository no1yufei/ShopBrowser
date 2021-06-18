using Common.Shopee.API.Data;
using CsharpHttpHelper;
using ShopeeChat.Shopee.API.Data;
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
       /// <summary>
       /// 获取用户的买家信息
       /// </summary>
       /// <param name="store"></param>
       /// <returns></returns>
        public BuyerBriefInfo GetBuyerBriefInfo(Store store,long shopid)
        {
            //必须判断，这个Store是否已经成功登陆
            //if (this.IsLogin(store))
            {
                //这里业务上的刷新逻辑，按照实际业务逻辑自行编写
                //http://xiapi.xiapibuy.cc/api/v2/shop/get?is_brief=1&shopid=7781602
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = StoreRegionMap.GetBuyerURL(store.RegionID) + "/api/v2/shop/get?is_brief=1&shopid=" + shopid;
                //组装数据，如果有，这里没有

                HtmlHttpHelper hhh = new HtmlHttpHelper();
                //调用HTTP请求，这里是Get请求，传入组装的URL，HttpResult是返回的结果， store.Hhh.bError是根据HTTP状态码判断返回是否有错误的标志，具体需要和
                //业务结合，根据数据来判断。
                HttpResult spcresult = hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null )
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    VersionMessage<BuyerBriefInfo> user = VersionMessage<BuyerBriefInfo>.FromJson(spcresult.Html);

                    if (null != user && user.data != null)
                    {
                        //Console.WriteLine(store.DisplayName + ":用户信息取得成功！");
                        return user.data;
                    }
                    Console.WriteLine(store.DisplayName + ":用户信息取得失败！"+ spcresult.Html);
                }
            }
            //返回错误标识
            return null;
        }
     
        /// <summary>
        /// 获取用户的买家信息
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public BuyerBriefInfo GetBuyerBriefInfo(Store store, string username)
        {
            //必须判断，这个Store是否已经成功登陆
            //if (this.IsLogin(store))
            {
                //这里业务上的刷新逻辑，按照实际业务逻辑自行编写
                //https://shopee.vn/api/v2/shop/get?username=minh.anh.shop.teen
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = StoreRegionMap.GetBuyerURL(store.RegionID) + "/api/v2/shop/get?is_brief=1&username=" + username;
                //组装数据，如果有，这里没有

                HtmlHttpHelper hhh = new HtmlHttpHelper();
                //调用HTTP请求，这里是Get请求，传入组装的URL，HttpResult是返回的结果， store.Hhh.bError是根据HTTP状态码判断返回是否有错误的标志，具体需要和
                //业务结合，根据数据来判断。
                HttpResult spcresult = hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null)
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    VersionMessage<BuyerBriefInfo> user = VersionMessage<BuyerBriefInfo>.FromJson(spcresult.Html);

                    if (null != user && user.data != null)
                    {
                        Console.WriteLine(store.DisplayName + ":用户信息取得成功！");
                        return user.data;
                    }
                }
            }
            //返回错误标识
            return null;
        }
    }
}
