using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API.Data
{
    public class ProductStatisticalData
    {
        //: {"banned_count": 1, "unlisted_count": 0, "count_for_limit": 5000, "product_count_for_limit": 3182, "sold_out_count": 6},
        public int banned_count;//": 1, 
        public int unlisted_count;//": 0, 
        public int count_for_limit;//": 5000,
        public int product_count_for_limit;//": 3182,
        public int sold_out_count;//": 6},
    }
}
