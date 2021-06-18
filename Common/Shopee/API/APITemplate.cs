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
    //类名不要修改，所有API都是ShopeeAPI类下的方法，调用方式都一样，New ShopeeAPI，然后用实例调用方法
    public partial class ShopeeAPI
    {
        /*这是一个店铺汇总信息的例子，调用都是使用Store店铺实例的Hhh变量，他是一个httphelper的实例，可以直接按需求调用它的
         * get，post，put，delele 方法，Cookie，鉴权等都已经处理好，按照可以在浏览器里直接数据的Http请求组装好URL和数据
         * 
         */
        public bool GetSummaryDataTemp(Store store)
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
                if (spcresult.Html != null && spcresult.Html.Contains("value"))
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    ShopSummaryInfo sum = ShopSummaryInfo.FromJson(spcresult.Html.Replace("\n", ""));
                    if (null != sum)
                    {
                        //转换成功，根据具体业务逻辑处理，这个接口实例是在这里在店铺数中保存取得的汇总信息，并且更新时间
                        store.SummaryInfo = sum;
                        //store.LastUpdateSummaryTime = Tool.GetUTCDateTime(endtime);
                        //store.SPC_CDS = SPC_CDS;
                        //打印调试信息，返回成功标志
                        Console.WriteLine(store.UserName + ":统计数据成功！");
                        return true;
                    }
                }
            }
            //返回错误标识
            return false;
        }
    }
}
