using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Tools
{
    class TemplateModelHelp
    {
        public DataTable CreatShopeeTemp()
        {
            //dt.Columns.Add(new DataColumn("整数值 ", typeof(Int32)));
            //dt.Columns.Add(new DataColumn("字符串值 ", typeof(string)));
            //dt.Columns.Add(new DataColumn("日期时间值 ", typeof(DateTime)));
            //dt.Columns.Add(new DataColumn("布尔值 ", typeof(bool)));

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Category ID", typeof(double)));
            dt.Columns.Add(new DataColumn("Product Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Product Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Stock", typeof(double)));
            dt.Columns.Add(new DataColumn("Product Weight", typeof(double)));
            dt.Columns.Add(new DataColumn("Ship out in", typeof(double)));
            dt.Columns.Add(new DataColumn("Parent SKU Reference No.", typeof(string)));
            dt.Columns.Add(new DataColumn("-", typeof(string)));

           

            dt.Columns.Add(new DataColumn("Variation 1: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 1: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 1: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 1: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 2: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 2: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 2: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 2: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 3: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 3: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 3: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 3: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 4: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 4: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 4: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 4: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 5: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 5: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 5: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 5: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 6: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 6: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 6: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 6: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 7: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 7: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 7: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 7: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 8: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 8: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 8: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 8: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 9: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 9: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 9: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 9: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 10: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 10: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 10: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 10: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 11: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 11: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 11: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 11: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 12: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 12: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 12: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 12: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 13: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 13: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 13: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 13: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 14: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 14: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 14: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 14: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 15: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 15: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 15: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 15: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 16: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 16: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 16: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 16: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 17: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 17: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 17: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 17: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 18: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 18: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 18: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 18: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 19: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 19: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 19: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 19: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Variation 20: SKU Ref. No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 20: Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Variation 20: Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Variation 20: Stock", typeof(double)));

            dt.Columns.Add(new DataColumn("Image 1", typeof(string)));
            dt.Columns.Add(new DataColumn("Image 2", typeof(string)));
            dt.Columns.Add(new DataColumn("Image 3", typeof(string)));
            dt.Columns.Add(new DataColumn("Image 4", typeof(string)));
            dt.Columns.Add(new DataColumn("Image 5", typeof(string)));
            dt.Columns.Add(new DataColumn("Image 6", typeof(string)));
            dt.Columns.Add(new DataColumn("Image 7", typeof(string)));
            dt.Columns.Add(new DataColumn("Image 8", typeof(string)));
            dt.Columns.Add(new DataColumn("Image 9", typeof(string)));



            return dt;
        }
    }

    
}
