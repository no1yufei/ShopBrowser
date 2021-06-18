using CsharpHttpHelper;
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

        public OngoingPenalty GetOngoingPenalty(Store store)
        {
            if (this.IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v2/shops/ongoing_penalty/?language=ZH-CN&SPC_CDS=fe8f4ec3-e5d6-4307-b83f-f68ac5fb209f&SPC_CDS_VER=2
                string querURL = store.ServerURL + "/api/v2/shops/ongoing_penalty/?language=ZH-CN&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("punishment_type"))
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    OngoingPenalty opm = OngoingPenalty.FromJson(spcresult.Html.Replace("\n", ""));
                    if (null != opm)
                    {
                        Console.WriteLine(store.UserName + ":统计数据成功！");
                        return opm;
                    }
                }
            }
            return null;
        }

        public bool GetPenaltyHistory(Store store,int page,int limit=10)
        {
            int offset = page * limit;
            if (this.IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v2/shops/penalty_history/?offset=0&limit=10&language=ZH-CN&SPC_CDS=fe8f4ec3-e5d6-4307-b83f-f68ac5fb209f&SPC_CDS_VER=2
                string querURL = store.ServerURL + "/api/v2/shops/penalty_history/?offset="+ offset + "&limit="+ limit + "&language=ZH-CN&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("punishment_type"))
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    PenaltyHistory ph = PenaltyHistory.FromJson(spcresult.Html.Replace("\n", ""));
                    if (null != ph)
                    {
                        Console.WriteLine(store.UserName + ":统计数据成功！");
                        return true;
                    }
                }
            }
            return false;
        }

        public PenaltyPoints GetPenaltyPoints(Store store, string start_date, string end_date,int page=0,int limit=10)
        {
            if (this.IsLogin(store))
            {
                int offset = page * limit;
                //https://seller.xiapi.shopee.cn/api/v2/shops/penalty_points/?start_date=2019-10-07&end_date=2020-01-05&offset=0&limit=10&language=ZH-CN&SPC_CDS=fe8f4ec3-e5d6-4307-b83f-f68ac5fb209f&SPC_CDS_VER=2
                string querURL = store.ServerURL + "/api/v2/shops/penalty_points/?start_date="+ start_date + "&end_date="+ end_date + "&offset="+ offset + "&limit="+limit+"&language=ZH-CN&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("violation_type"))
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    PenaltyPoints pp = PenaltyPoints.FromJson(spcresult.Html.Replace("\n", ""));
                    if (null != pp)
                    {
                        Console.WriteLine(store.UserName + ":统计数据成功！");
                        return pp;
                    }
                }
            }

            return null;
        }

        public bool GetPenaltyScoreList(Store store,string start_date,string end_date)
        {
            if (this.IsLogin(store))
            {

                //https://seller.xiapi.shopee.cn/api/v2/shops/penalty_score_list/?start_date=2019-10-07&end_date=2020-01-05&SPC_CDS=fe8f4ec3-e5d6-4307-b83f-f68ac5fb209f&SPC_CDS_VER=2
                string querURL = store.ServerURL + "/api/v2/shops/penalty_score_list/?start_date="+ start_date + "&end_date="+ end_date + "&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("penalty_score"))
                {
                    //把收到的Json数据转换成我们定义的数据结构，供程序使用，每个类都定义了个FromJson的静态方法来转换数据
                    PenaltyScoreList psl = PenaltyScoreList.FromJson(spcresult.Html);
                    if (null != psl)
                    {
                        Console.WriteLine(store.UserName + ":统计数据成功！");
                        return true;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// logid是GetPenaltyPoints请求回来的id
        /// </summary>
        /// <param name="store"></param>
        /// <param name="logid"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public PenaltyOrderDetail GetPenaltyOrderDetail(Store store,string logid,int page=0,int limit=6)
        {
            if (this.IsLogin(store))
            {
                int offset = page * limit;
                //https://seller.xiapi.shopee.cn/api/v2/shops/penalty_detail/?language=ZH-CN&logid=126631100921356624&limit=6&offset=0&SPC_CDS=fe8f4ec3-e5d6-4307-b83f-f68ac5fb209f&SPC_CDS_VER=2
                string querURL = store.ServerURL + "/api/v2/shops/penalty_detail/?language=ZH-CN&logid="+ logid + "&limit="+ limit + "&offset="+ offset + "&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("reference_no"))
                {
                    PenaltyOrderDetail pod = PenaltyOrderDetail.FromJson(spcresult.Html);
                    if (null != pod)
                    {
                        Console.WriteLine(store.UserName + ":统计数据成功！");
                        return pod;
                    }
                }
            }

            return null;
        }
        public PenaltyProductDetail GetPenaltyProductDetail(Store store, string logid, int page = 0, int limit = 6)
        {
            if (this.IsLogin(store))
            {
                int offset = page * limit;
                //https://seller.xiapi.shopee.cn/api/v2/shops/penalty_detail/?language=ZH-CN&logid=126631100921356624&limit=6&offset=0&SPC_CDS=fe8f4ec3-e5d6-4307-b83f-f68ac5fb209f&SPC_CDS_VER=2
                string querURL = store.ServerURL + "/api/v2/shops/penalty_detail/?language=ZH-CN&logid=" + logid + "&limit=" + limit + "&offset=" + offset + "&SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("reference_no"))
                {
                    PenaltyProductDetail ppd = PenaltyProductDetail.FromJson(spcresult.Html);
                    if (null != ppd)
                    {
                        Console.WriteLine(store.UserName + ":统计数据成功！");
                        return ppd;
                    }
                }
            }
            return null;
        }
    }
}
