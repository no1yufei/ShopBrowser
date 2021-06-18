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
        public UserDetailInfo GetUserDetailInfo(Store store)
        {
            //必须判断，这个Store是否已经成功登陆
            if (this.IsLogin(store))
            {
                //这里业务上的刷新逻辑，按照实际业务逻辑自行编写
                //https://seller.xiapi.shopee.cn/api/v2/users/34797586/?SPC_CDS=af0dd52f-95c9-4acb-8de5-e561d3075b5d&SPC_CDS_VER=2
                //组装URL，注意，ServerRUL是店铺所在国家访问的基地址
                string querURL = store.ServerURL + "/api/v2/users/" + store.ShopInfo.user.uid + "/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                //组装数据，如果有，这里没有

                //调用HTTP请求，这里是Get请求，传入组装的URL，HttpResult是返回的结果， store.Hhh.bError是根据HTTP状态码判断返回是否有错误的标志，具体需要和
                //业务结合，根据数据来判断。
                HttpResult spcresult = store.Hhh.Get(querURL);

                //处理返回的数据，Html就是返回的Jason数据，文本，网页，文件，根据你请求业务自行确定，这里判断返回必须含 value才是一个正确的Json值
                if (spcresult.Html != null && spcresult.Html.Contains("user"))
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    UserDetailInfoReponse user = UserDetailInfoReponse.FromJson(spcresult.Html);
                    if (null != user && user.users.Count() > 0)
                    {
                        Console.WriteLine(store.DisplayName + ":用户信息取得成功！");
                        return user.users[0];
                    }
                }
            }
            //返回错误标识
            return null;
        }
    }
}
