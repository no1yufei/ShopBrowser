using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.SysData
{
    public class StoreRegion
    {
        public string  RegionName;
        public string RegionID;
        private string sellerURL;

        [System.Xml.Serialization.XmlIgnore]
        public DateTime OrderUpdateTime = DateTime.MinValue;
        [System.Xml.Serialization.XmlIgnore]
        public DateTime MessageUpdateTime = DateTime.MinValue;

        public string GetSellerURL()
        {
            if(sellerURL == null)
            {
                sellerURL = StoreRegionMap.GetSellerURL(RegionID);
            }
            return sellerURL; 
        }
        private string buyerUrl;
        public string  GetBuyerUrl()
        {
            if(null == buyerUrl)
            {
                buyerUrl = StoreRegionMap.GetBuyerURL(RegionID);
            }
             return buyerUrl; 
        }
        public List<Store> Stores = new List<Store>();
        public StoreRegion(String regionId,string region,string sellerUrl,string buyerUrl)
        {
            RegionID = regionId;
            RegionName = region;
            this.sellerURL = sellerUrl;
            this.buyerUrl = buyerUrl;
        }
        public StoreRegion() {
        }
        public StoreRegion(StoreRegion region)
        {
            RegionID = region.RegionID;
            RegionName = region.RegionName;
            this.sellerURL = region.sellerURL;
            this.buyerUrl = region.buyerUrl;
        }
        public override string ToString()
        {
            return RegionName;
        }
        
    }
}
