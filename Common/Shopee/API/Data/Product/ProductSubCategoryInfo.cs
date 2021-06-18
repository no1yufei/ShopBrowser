using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data.Product
{
    public class ProductSubCategoryList
    {
        public ProductCategoryInfo[][] list;
    }
    public class ProductCategoryInfo
    {
        public string display_name;//: "女生衣著"
        public bool enable_size_chart;//: false
        public bool has_active_children;//: true
        public bool has_children;//: true
        public int id;//: 62
        public int is_default;//: 0
        public int low_stock_value;//: 0
        public string name;//: "Women's Apparel"
        public int parent_id;//: 0
    }
}
