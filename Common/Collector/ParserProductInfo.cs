using Common.Collector.T1688;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Collector
{
    public class ParserProductInfo
    {

        /// <summary>
        /// 来源平台上的产品原始ID
        /// </summary>
        public string PID = "";
        /// <summary>
        /// 上传到虾皮平台对应的ID；
        /// </summary>
        public long SID = 0;
        /// <summary>
        /// 该产品对应的SKU，作为某一个用户判断产品的标识。
        /// </summary>
        public string SKU;
       
        public string Name;
        public string Price;
        public string Status = ParserStatus.StatusUnHandle;
        public string MainImageUrl;
        /// <summary>
        /// 来源平台的产品详情的URL
        /// </summary>
        public string URL;
        /// <summary>
        /// 页面的HTML字符串
        /// </summary>
        public string HTML;

        public bool Selected = false;

        public override bool Equals(object obj)
        {
            if(obj is ParserProductInfo)
            {
                return (obj as ParserProductInfo).PID == this.PID;
            }
            return base.Equals(obj);
        }
    }
}
