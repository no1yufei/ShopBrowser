using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData
{
    public class Tools
    {
        /// <summary>  
        /// 获取时间戳  13位
        /// </summary>  
        /// <returns></returns>  
        public static long GetTimeStampSeconds()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        public static long GetTimeStampMilseconds()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        public static DateTime GetUTCDateTime(long stamp)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(stamp);
        }
    }
}
