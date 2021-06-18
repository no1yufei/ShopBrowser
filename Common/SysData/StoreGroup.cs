using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.SysData
{
    public class StoreGroup
    {
        public Guid ID = Guid.NewGuid();
        public string GroupName = "默认店群";
        public bool IsProxy;
        public string ProxyIP = "ieproxy";
        public long Port;
        public string ProxyUserName;
        public string Password;
        public List<StoreRegion> Regions = new List<StoreRegion>();
        [System.Xml.Serialization.XmlIgnore]
        public DateTime OrderUpdateTime = DateTime.MinValue;
        [System.Xml.Serialization.XmlIgnore]
        public DateTime MessageUpdateTime = DateTime.MinValue;

        public override string ToString()
        {
            return GroupName;
        }
        public override bool Equals(object obj)
        {
            if(obj is StoreGroup)
            {
                return (obj as StoreGroup).ID == ID;
            }
            return base.Equals(obj);
        }
    }
}
