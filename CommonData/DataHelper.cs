using CommonData.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonData
{
    public class DataHelper
    {
        public static string GetPasswordStr(string username, string password)
        {
            string userpasswordStr = username + DateTime.Now.ToUniversalTime().ToString("yyMMddhh") + password;
            return HashHelper.GetMD5(userpasswordStr);
        }
        public static string GetAESIvFromTime(DateTime ivTime)
        {
            return "0" + ivTime.ToUniversalTime().ToString("yymmddMMss");
        }
    }
}
