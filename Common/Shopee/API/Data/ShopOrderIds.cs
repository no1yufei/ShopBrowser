using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    public class ShopOrderIds
    {
        public PageInfo page_info;
        public OrderIds[] orders;
        public int PageRange;
    }
    public class PageInfo
    {
        public int page_size;// : 40;//: 40
        public int page_number;// : 1;//: 0
        public int total;//: 2
    }
    public class OrderIds
    {
        public long order_id;// : 45798828395927
        public long shop_id;// : 31505223
        public string region_id;// : "TW"
    }
}
