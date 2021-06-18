using Common.Shopee.API.Data;
using CsharpHttpHelper;
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

        public IncomeMeta GetIncomeMeta(Store store)
        {
            if (this.IsLogin(store))
            {
                //https://seller.ph.shopee.cn/api/v3/finance/get_income_meta/?SPC_CDS=75eeecb1-8562-47f4-bb4a-88f616209fa6&SPC_CDS_VER=2
                string querURL = store.ServerURL + "/api/v3/finance/get_income_meta/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("frozen"))
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    IncomeMeta im = IncomeMeta.FromJson(spcresult.Html.Replace("\n", ""));
                    if (null != im)
                    {
                        Console.WriteLine(store.UserName + ":统计拨款数据成功！");
                        return im;
                    }
                }
            }
            return null;
        }
    }
}
