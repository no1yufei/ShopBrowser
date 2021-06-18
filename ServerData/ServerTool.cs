using ShopeeChat.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData
{
    public class ServerTool
    {
        public static string GetPasswordStr(string username, string password, DateTime updateTime)
        {
            string userpasswordStr = username + updateTime.ToUniversalTime().ToString("yymmddMMss") + password;
            return HashHelper.GetMD5(userpasswordStr);
        }
        public static string GetAESIvFromTime(DateTime ivTime)
        {
            return "0" + ivTime.ToUniversalTime().ToString("yymmddMMss");
        }
    }
}
