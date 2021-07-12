using NPOI.OpenXmlFormats.Dml;
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
       

        private readonly static Dictionary<int, List<StoreRegion>> platfomregions = 
            new Dictionary<int, List<StoreRegion>>{
                { 0,new List<StoreRegion>() {
                       new StoreRegion(0,"gl", "中国", "http://www.uc.dianliaotong.com","http://www.dianliaotong.com"),
                    }
                },
                { 1,new List<StoreRegion>() {
                       new StoreRegion(1,"gl", "中国卖家全球账号", "https://seller.shopee.cn","https://xiapi.xiapibuy.com"),
                       new StoreRegion(1,"tw", "台湾", "https://seller.xiapi.shopee.cn","https://xiapi.xiapibuy.com"),
                       new StoreRegion(1,"sg", "新加坡", "https://seller.sg.shopee.cn", "https://shopee.sg"),
                       new StoreRegion(1,"my", "马来西亚", "https://seller.my.shopee.cn", "https://shopee.com.my"),
                       new StoreRegion(1,"th", "泰国", "https://seller.th.shopee.cn", "https://shopee.co.th"),
                       new StoreRegion(1,"id", "印度尼西亚", "https://seller.id.shopee.cn", "https://shopee.co.id"),
                       new StoreRegion(1,"ph", "菲律宾", "https://seller.ph.shopee.cn", "https://shopee.ph"),
                       new StoreRegion(1,"vn", "越南", "https://seller.vn.shopee.cn", "https://shopee.vn"),
                       new StoreRegion(1,"br","巴西","https://seller.br.shopee.cn","https://br.xiapibuy.com")
                    }
                },
                { 2,new List<StoreRegion>() {
                       new StoreRegion(2,"gl", "lazada跨境", "https://sellercenter-my.lazada-seller.cn/","https://www.lazada.sg/"),
                    }
                },
                { 3,new List<StoreRegion>() {
                       new StoreRegion(3,"tb", "淘宝中国", "https://myseller.taobao.com","https://www.taobao.com")

                    }
                },
                { 4,new List<StoreRegion>() {
                       new StoreRegion(4,"gl", "中国", "http://www.uc.dianliaotong.com","http://www.dianliaotong.com"),
                    }
                }
            };
      
        static public List<StoreRegion> GetRegionList(int plateform = 1)
        {
            return platfomregions[plateform];
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
        static public String GetRegionID(String regionName,int platfrom=1)
        {
            return platfomregions[platfrom].First(p => p.RegionName.ToLower() == regionName.ToLower()).RegionID;
        }
       static public String GetSellerURL(string id, int platfrom = 1)
        {
            return platfomregions[platfrom].First(p=>p.RegionID.ToLower() == id.ToLower()).GetSellerURL();
        }
        static public String GetBuyerURL(string id, int platfrom = 1)
        {
            return platfomregions[platfrom].First(p => p.RegionID.ToLower() == id.ToLower()).GetBuyerUrl();
        }
        static public String[] GetBuyerURLs(string id, int platfrom = 1)
        {
            if (id == "tw")
            {
                return new string[] {  "https://xiapi.xiapibuy.com",StoreRegionMap.GetBuyerURL("tw") };
            }
            else
            {
                return new string[] { platfomregions[platfrom].First(p => p.RegionID.ToLower() == id.ToLower()).GetBuyerUrl() };
            }
        }
        static public String GetRegionName(string id, int platfrom = 1)
        {
            return platfomregions[platfrom].First(p => p.RegionID.ToLower() == id.ToLower()).RegionName;
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
