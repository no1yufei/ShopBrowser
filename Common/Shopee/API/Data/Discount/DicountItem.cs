using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Shopee.API.Data.DiscountList;

namespace Common.Shopee.API.Data.Discount
{
    //public class DiscountItems
    //{
    //    //public DiscountsItem[] discounts;
    //}
    public class DiscountItem
    {
        public string country;//: "TW"
        public long ctime;//: 1571558042
        public long end_time;//: 1572512400
        public int fe_status;//: 2
        public long id;//: 1039284303
                       //images: []
        public long mtime;//: 1571558042
        public long shopid;// 183178911
        public long start_time;//: 1571558400
        public int status;//: 1
        public string title;//: "测试打折"
        public int total;//: 0
        public long userid;//: 183181582
    }
    public class DiscountModels
    {
        public DiscountModel[] discountModels; 
    }
    public class DiscountModel
    {
        public int discount;//: 6
        //public string id;//: "1036913587-2825431571-7056247185"
        public long itemid;//: 2825431571
        public string model_name;//: "white,One Size"
        public long modelid;//: 7056247185
        public string price_before_discount;//: "26.00"
        public string promotion_price;//: "24.44"
        public long promotionid;//: 1036913587
        public string rebate_price = "0.00";
        public bool selected = true;
        public int status = 1;
        public int total_item_limit = 0;
        public int user_item_limit = 3;
    }
}
