using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DA
{
    public class DAOrder
    {
        public string BuyerUserName { get; set; }
        public string Region { get; set; }
    
        public string SOrderSN { get; set; }
      
        public string SWayBill { get; set; }
           
        public DateTime HandleTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public Guid PayUserID { get; set; }
        public double PayAmount { get; set; }
    
        public List<DAOrderItem> list = new List<DAOrderItem>();
    }


    public class DAOrderItem
    {
        public string WItemName { get; set; }
        public string WItemRemark { get; set; }
        public string Waybillnumber { get; set; }
     
        public int WItemCount { get; set; }
        public string ImageString { get; set; }
    }
   
}
