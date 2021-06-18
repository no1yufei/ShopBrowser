using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    class MarketingInfo
    {
    }
    public class DiscountList
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ShopeeDiscountItem> discount_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long total_count  { get; set; }

        
    }
    public class ShopeeDiscountItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long start_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long end_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> images { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long discount_id { get; set; }
        public long total_product { get; set; }
    }
    public class DiscountProductList
    {
        /// <summary>
        /// 
        /// </summary>
        public long total_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DiscountModel> discount_item_list { get; set; }
        

        
    }
    public class DiscountModel
    {
        /// <summary>
        /// 
        /// </summary>
        public long itemid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long user_item_limit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string promotion_price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long modelid { get; set; }
    }

    public class DiscountModels
    {
        public long discount_id;//: 1059804959
        public List<DiscountModel> discount_model_list;
    }
    public class ItemModelIdList
    {
       public  List<ItemModelId> item_list;
        public class ItemModelId
        {
            public ItemModelId(long itemid,long modelid)
            {
                this.itemid = itemid;
                this.modelid = modelid;
            }
            public long itemid;//: 6918974238;
            public long modelid;//: 14266631069
        }
    }

}
