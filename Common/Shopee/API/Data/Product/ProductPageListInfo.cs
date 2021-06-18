using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API.Data
{
    public class ProductPageListInfo
    {
        public ProductDetailBaseInfo[] list;
        public PageInfo page_info;
    }
    public class PageInfo
    {
        public int page_number;//: 66
        public int page_size;//: 48
        public int total;//: 3182
    }
}
