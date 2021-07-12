using Common.Shopee.API.Data;
using Common.Tools;
using ShopeeChat.Shopee;
using ShopeeChat.Shopee.API;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ShopeeChat.SysData
{
    public class Store
    {
        public int Plateform = 1;
        public String Country;
        string regionid;

        public String RegionID
        {
            set { regionid = value; }
            get {
                if (regionid == null)
                {
                    return StoreRegionMap.GetRegionID(Country);
                }
                return regionid;
            }
        }


        public String ServerURL
        {
            get { return StoreRegionMap.GetSellerURL(RegionID, Plateform); }
        }
        string displayName;
        /// <summary>
        /// 显示用户名的保密字符串。
        /// </summary>
        public String DisplayName
        {
            get { return displayName; }
        }
        string userName;
        public String UserName
        {
            set
            {
                userName = value;
                displayName = value;
                string[] nameSegs = value.Split('.');
                try
                {
                    if (nameSegs[0].Length >= 4)
                    {
                        displayName = value.Split('.')[0].Remove(1, 1).Insert(1, "***");
                    }
                    else if (nameSegs.Count() > 1)
                    {
                        displayName = "***" + nameSegs[1];
                    }
                    else
                    {
                        displayName = displayName + "***";
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            get { return userName; }
        }
        public String Password;
        public long UserID;
        public long ShopID;
        public bool IsSIP = false;
        [System.Xml.Serialization.XmlIgnore]
        public List<SIPStoreInfo> SIPStores = new List<SIPStoreInfo>();

        [System.Xml.Serialization.XmlIgnore]
        public String Token;
        string cookies = "";
        string reg = @"[0-9a-fA-F]{8}(-[0-9a-fA-F]{4}){3}-[0-9a-fA-F]{12}";
        public String Cookies
        {
            get { return cookies; }
            set { cookies = Regex.Replace(value, @"SPC_CDS="+reg+";", ""); }
        }
        
        [System.Xml.Serialization.XmlIgnore]
        public string CcrsfToken;
      
        public Guid SPC_CDS;
        [System.Xml.Serialization.XmlIgnore]
        public ShopSummaryInfo SummaryInfo;
        [System.Xml.Serialization.XmlIgnore]
        public DateTime LastUpdateSummaryTime;
        [System.Xml.Serialization.XmlIgnore]
        public int TotalErrorTime = 0;
        [System.Xml.Serialization.XmlIgnore]
        public int UnReadCount = 0;

        [System.Xml.Serialization.XmlIgnore]
        public ShopInfo ShopInfo;

        [System.Xml.Serialization.XmlIgnore]
        public List<ShopCustomerInfo> CustomerInfos = new List<ShopCustomerInfo>();

        [System.Xml.Serialization.XmlIgnore]
        public OrderSummaryInfo OrderSummaryInfo = new OrderSummaryInfo();
        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, OrderSummaryInfo> SIPOrderSummaryInfos = new Dictionary<string, OrderSummaryInfo>();


        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<String, List<OrderInfoV3>> Orders = new Dictionary<string, List<OrderInfoV3>>();


        [System.Xml.Serialization.XmlIgnore]
        public HtmlHttpHelper Hhh;

        [System.Xml.Serialization.XmlIgnore]
        public DateTime OrderUpdateTime = DateTime.MinValue;
        [System.Xml.Serialization.XmlIgnore]
        public DateTime MessageUpdateTime = DateTime.MinValue;

        public int[] DefaultCataIds;
        public string TitleFormat = "[title]";
        public string DescriptionFormat = "[description]";

        public bool IsLocalAccount = false;
        public bool OnlyCookie = false;

        [System.Xml.Serialization.XmlIgnore]
        public LoginStatus LogStatus = LoginStatus.UnLog;
        [System.Xml.Serialization.XmlIgnore]
        public ErrStrMsg LogMessage = new ErrStrMsg();
        [System.Xml.Serialization.XmlIgnore]
        public bool IsLogin
        {
            get {
                ShopeeAPI api = new ShopeeAPI();
                return api.IsLogin(this);
            }
        }

        public Store(int plateform, string regionid, string userName, String pawd)
        {
            this.RegionID = regionid;
            this.UserName = userName;
            this.Password = pawd;
            this.Plateform = plateform;
        }
        public void SetValue(int plateform, string regionid,string userName,String pawd)
        {
            this.RegionID = regionid;
            this.UserName = userName;
            this.Password = pawd;
            this.Plateform = plateform;
        }
        public Store() {
            //Hhh.sCookies = Cookies;
            //Hhh.Authorization = "Bearer " + Token;
        }

        string shopinfconfig = "\\shopinfconfig.cfg";
       
        public override string ToString()
        {
            return UserName;
        }
        public override bool Equals(object obj)
        {
            if(obj is Store)
            {
                return (obj as Store).userName == userName;
            }
            return base.Equals(obj);
        }
    }
}
