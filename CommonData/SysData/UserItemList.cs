using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData.SysData
{
    public class UserItemList
    {
        public Guid UserID { get; set; }
        public Guid ItemID { get; set; }
        public string ItemName { get; set; }
        public double StandardPrice { get; set; }
        public double SalePrice { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
