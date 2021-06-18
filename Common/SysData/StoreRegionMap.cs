using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopeeChat.SysData
{
    public class StoreRegionMap
    {
        private static List<StoreRegion> Regions = GetRegionList();
        static public List<StoreRegion> GetRegionList()
        {
            string strFullPath = Application.ExecutablePath;
            string filename = Path.GetFileName(strFullPath);

            if (strFullPath.ToLower().Contains("tw"))
            {
                Console.WriteLine("启动台湾翻墙设置！");
                return new List<StoreRegion>() {
                    new StoreRegion("tw", "台湾", "https://seller.xiapi.shopee.cn","https://xiapi.xiapibuy.com"),
                    new StoreRegion("sg", "新加坡", "https://seller.sg.shopee.cn", "https://shopee.sg"),
                    new StoreRegion("my", "马来西亚", "https://seller.my.shopee.cn", "https://shopee.com.my"),
                    new StoreRegion("th", "泰国", "https://seller.th.shopee.cn", "https://shopee.co.th"),
                    new StoreRegion("id", "印度尼西亚", "https://seller.id.shopee.cn", "https://shopee.co.id"),
                    new StoreRegion("ph", "菲律宾", "https://seller.ph.shopee.cn", "https://shopee.ph"),
                    new StoreRegion("vn", "越南", "https://seller.vn.shopee.cn", "https://shopee.vn"),
                    new StoreRegion("br","巴西","https://seller.br.shopee.cn","https://br.xiapibuy.com")
                };
            }
            else
            {
                return new List<StoreRegion>() {
                    new StoreRegion("tw", "台湾", "https://seller.xiapi.shopee.cn", "https://xiapi.xiapibuy.com"),
                    new StoreRegion("sg", "新加坡", "https://seller.sg.shopee.cn", "https://shopee.sg"),
                    new StoreRegion("my", "马来西亚", "https://seller.my.shopee.cn", "https://shopee.com.my"),
                    new StoreRegion("th", "泰国", "https://seller.th.shopee.cn", "https://shopee.co.th"),
                    new StoreRegion("id", "印度尼西亚", "https://seller.id.shopee.cn", "https://shopee.co.id"),
                    new StoreRegion("ph", "菲律宾", "https://seller.ph.shopee.cn", "https://shopee.ph"),
                    new StoreRegion("vn", "越南", "https://seller.vn.shopee.cn", "https://shopee.vn"),
                    new StoreRegion("br","巴西","https://seller.br.shopee.cn","https://br.xiapibuy.com")
                };
            }
        }
        static string[][] descriptionLangMap = new string[][]{new string[] {"tw","zh-TW","zh-Hant"},
                                                               new string[]{"sg","en","en"},
                                                               new string[]{"my","en","ms"},
                                                               new string[]{"th","en","th"},
                                                              new string[] {"id","en","id"},
                                                              new string[] {"ph","en","tl"},
                                                              new string[] {"vn","en","en"},
                                                              new string[] {"br","en","pt"}
                                                               };
       
        static public string GetDescriptionFirstLang(string reginId)
        {
            return descriptionLangMap.First(p=>p[0]== reginId)[1];
        }
        static public string GetDescriptionSecondeeLang(string reginId)
        {
            return descriptionLangMap.First(p => p[0] == reginId)[2];
        }
        static public String GetRegionID(String regionName)
        {
            return Regions.First(p => p.RegionName.ToLower() == regionName.ToLower()).RegionID;
        }
       static public String GetSellerURL(string id)
        {
            return Regions.First(p=>p.RegionID.ToLower() == id.ToLower()).GetSellerURL();
        }
        static public String GetBuyerURL(string id)
        {
            return Regions.First(p => p.RegionID.ToLower() == id.ToLower()).GetBuyerUrl();
        }
        static public String[] GetBuyerURLs(string id)
        {
            if (id == "tw")
            {
                return new string[] {  "https://xiapi.xiapibuy.com",StoreRegionMap.GetBuyerURL("tw") };
            }
            else
            {
                return new string[] { Regions.First(p => p.RegionID.ToLower() == id.ToLower()).GetBuyerUrl() };
            }
        }
        static public String GetRegionName(string id)
        {
            return Regions.First(p => p.RegionID.ToLower() == id.ToLower()).RegionName;
        }
    }
    public class RegionBaseInfo
    {
        public static List<RegionBaseInfo> BaseInofs = new List<RegionBaseInfo>
        {
            new RegionBaseInfo("tw","zh-TW","en","zh-TW","新台币",25),
            new RegionBaseInfo("sg","en","en","en","新加坡元",5.5),
            new RegionBaseInfo("my","en","ms","ms","马来西亚林吉特",7.5),
            new RegionBaseInfo("th","en","th","th","泰铢",100),
            new RegionBaseInfo("id","en","id","id","印尼卢比",60000),
            new RegionBaseInfo("ph","en","tl","tl","菲律宾比索",226),
            new RegionBaseInfo("vn","en","en","vn","越南盾",4500),
            new RegionBaseInfo("br","en","pt","es","巴西雷亚尔",85)
        };
        static public double GetPostFee(string regionid)
        {
            return BaseInofs.First(p => p.RegionID == regionid).PostFee;
        }
        static public RegionBaseInfo GetRegionBaseInfo(string regionid)
        {
            return BaseInofs.First(p => p.RegionID == regionid);
        }
        public string RegionID;
        public string FirstDescLang;
        public string SendDescLang;
        public string ChatLang;
        public string CurrencyName;
        public double PostFee;
        public RegionBaseInfo(string regionid,string firstLng,string secondLang,string chatlang,string currencyName,double postfee)
        {
            this.RegionID = regionid;
            this.FirstDescLang = firstLng;
            this.SendDescLang = secondLang;
            this.ChatLang = chatlang;
            this.CurrencyName = currencyName;
            this.PostFee = postfee;
        }

    }
}
