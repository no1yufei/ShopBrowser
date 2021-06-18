using Common.Tools.ProdFormater;
using CommonData.SysData.Enum;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Collector
{
    public class ParserHotProduct:Parser
    {
        public ParserHotProduct()
        {
            EnterURL = "";
            SearchURL = "";
            DetailURL = "https://detail.1688.com/offer/";//592237780063.html
            IsSupportBatch = true;
            IsWebbrower = false;
            IsDataBase = true;
            SKUPrefix = "DALI";//热卖品暂时是从阿里巴巴来的
        }
        public override string GetDetailPageById(string id)
        {
            return DetailURL + id + ".html";
        }
        #region 列名变量定义
       
        string idTextColumnName = "ID";
        string nameColumnName = "NAME_CN";
        string maxPriceColumnName = "MAX_PRICE";
      
        #endregion 列名变量定义
        public override string MakeSearchURL(string keyword, string province, int page = 1, int minPrice = 1, int maxPrice = 10000, int count = 50)
        {
            count = AccessControl.Instance.IsLevelRight(UserLevel.VIPUser) ? 200 : 50;
            string sql = "select " + idTextColumnName + "," + nameColumnName + "," + maxPriceColumnName;
             sql +=   " from ali_product_info where ID <> '' AND PSTATE = 0 ";
            sql += maxPrice == 0 ? "" : "   MAX_PRICE > " + minPrice + " and MAX_PRICE < " + maxPrice ;
            sql += province == "" ? "" : "  AND PROVINCE  like BINARY  '%" + province + "%' "; 
            sql += keyword == "" ? "" : "   AND NAME_CN  like BINARY  '%" + keyword + "%' ";
            sql += " ORDER BY ID ASC  ";
            sql += "  LIMIT " + count + ";";
            return sql;
        }
        override public ProdFormat PaserDetailPage(string html)
        {
            try
            {
                AliProdFormat aliProdFormat = new AliProdFormat();
                return aliProdFormat.WebSourceFormat(html);
            }
            catch (Exception xe)
            {
                Console.WriteLine("页面解析错误：" + xe.Message);
            }
            return null;
        }

        /// <summary>
        /// 从数据库读取数据组合成规定数据
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        override public List<ParserProductInfo> ParserOutlineInfosFromSearchPage(string sql)
        {
            List<ParserProductInfo> searchResult = new List<ParserProductInfo>();
            try
            {
                DataSet dsSku = DbHelperMySQL.Query(sql);


                string updateId = "";
                foreach (DataRow row in dsSku.Tables[0].Rows)
                {
                    string ID = row[1].ToString();
                    updateId = updateId + "'" + ID + "',";

                    ParserProductInfo bInfo = new ParserProductInfo();
                    bInfo.PID = (string)row[idTextColumnName];
                    bInfo.SKU = this.SKUPrefix + bInfo.PID;
                    bInfo.URL = this.GetDetailPageById(bInfo.PID);
                    bInfo.Name = (string)row[nameColumnName];
                    bInfo.Price = row[maxPriceColumnName].ToString();
                    bInfo.Selected = false;
                    bInfo.Status = ParserStatus.StatusUnHandle;
                    searchResult.Add(bInfo);
                }
                updateId = Regex.Replace(updateId, ",$", "");
                DbHelperMySQL.ExecuteSql("update ali_product_info set PSTATE = 1 where ID in (" + updateId + ")");

                string userName = AccessControl.Instance.UserName;
                String uuid = Guid.NewGuid().ToString();
                String updateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string insertSql = "INSERT INTO spc_user_record (UUID,USERID,USEINFO,UPDATE_TIME) value ('" + uuid + "','" + userName + "',1,'" + updateTime + "');";
                DbHelperMySQL.ExecuteSql(insertSql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据访问错误：" + ex.Message);
            }

           
            return searchResult;
        }


    }
}
