using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee
{
    public class ShopInfo
    {
        //public string status;//": "verified", 
        public string p_token;//": "zZJsp01atE7y9cUm+oCOTqVnV4W17AW2teBtDiXyqwc=", 
        public string token;//": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImJlc3RicmFuZC5pZCIsImNyZWF0ZV90aW1lIjoxNTU3OTA0OTY3LCJpZCI6IjNkODU3NTg4NzZlMjExZTk5ZWUzYjQ5NjkxNDRkZjNlIiwiZGV2aWNlX2lkIjoiNDExMjBkZGItZjIxZS00MDdmLWJkOWQtOWRiZmQ5MDc1MjNkIn0.pG55WRhnep4G7RqAmGP1-lT2FFYewYmKp9wrVgHlR2c", 
        public UserInfo user = new UserInfo();//":
        
        public class UserInfo
        {
            //public string username = "";//": "bestbrand.id", 
            //public double rating;//;": 0, 
            public string uid;//": "0-53754837", 
            //public string city;//": null, 
            public string locale;//": "zh-Hans", 
            //public string gender;//": "unknown", ;
            //public DateTime created_at;//": "2018-01-24T14:39:30+07:00", 
            //public string distribution_status;//": null, 
            //public DateTime updated_at;//": "2019-04-30T14:08:41+07:00", 
            //public DateTime logined_at;//": "2019-04-30T08:50:45+07:00", 
           //      public int age;//": 49,
            public long shop_id;//": 53753447, 
            public string status;//": "normal", 
            public string avatar="";//": "https://cf.shopee.co.id/file/5d6e2ecff84f9cc3f53ca9fb0038a17c", 
            public string country;//": "ID", 
            //public bool is_blocked;//": false, 
            public string type;//: "seller", 
            public long id;//
    }
        public static ShopInfo FromJson(String jsonStr)
        {
            ShopInfo store = null;
            try
            {
                store = JsonConvert.DeserializeObject<ShopInfo>(jsonStr);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return store;
        }
    }
    public class StoreInfo
    {
        public bool is_sip_primary;
    }
    public class SIPStoreInfo
    {
        public string country;//: "MY"
        public bool sc_displayed;//: true
        public long shopid;//: 203124285
        public double sip_discount;//: 0.82
                                   //sync_failed_cnt: 0
                                   //sync_succeed_cnt: 0
        public long userid;//: 203127468
    }
    public class SIPSotreInfoResult
    {
        // public    result: {is_global: true, go_on_sale_status: 3, cb_option: 1,…}
        public List<SIPStoreInfo> affi_shops;//: [{shopid: 203124285, userid: 203127468, country: "MY", sip_discount: 0.82, sc_displayed: true,…},…]
//0: {shopid: 203124285, userid: 203127468, country: "MY", sip_discount: 0.82, sc_displayed: true,…}

//cb_option: 1
//go_on_sale_status: 3
//is_global: true
//mst_sync_cnt: 0
//shop_manage_policy: {seller_set_price: false, use_ashop_settlement: false, allow_seller_alloc_stock: false}
    }
    public class SIPSotreInfoResultResp
    {
        public SIPSotreInfoResult result;//: {is_global: true, go_on_sale_status: 3, cb_option: 1,…}

    }
}

