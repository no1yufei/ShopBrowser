using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Tools
{
    public class Domain
    {
        public static string GetBaseDomain(string host)
        {
            List<string> list = new List<string>(".com|.co|.info|.net|.org|.me|.mobi|.us|.biz|.xxx|.ca|.co.jp|.com.cn|.net.cn|.org.cn|.mx|.tv|.ws|.ag|.com.ag|.net.ag|.org.ag|.am|.asia|.at|.be|.com.br|.net.br|.bz|.com.bz|.net.bz|.cc|.com.co|.net.co|.nom.co|.de|.es|.com.es|.nom.es|.org.es|.eu|.fm|.fr|.gs|.in|.co.in|.firm.in|.gen.in|.ind.in|.net.in|.org.in|.it|.jobs|.jp|.ms|.com.mx|.nl|.nu|.co.nz|.net.nz|.org.nz|.se|.tc|.tk|.tw|.com.tw|.idv.tw|.org.tw|.hk|.co.uk|.me.uk|.org.uk|.vg".Split('|'));
            string[] hs = host.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (hs.Length > 2)
            {
                //传入的host地址至少有三段
                int p2 = host.LastIndexOf('.');                 //最后一次“.”出现的位置
                int p1 = host.Substring(0, p2).LastIndexOf('.');//倒数第二个“.”出现的位置
                string s1 = host.Substring(p1);
                if (!list.Contains(s1))
                    return s1.TrimStart('.');

                //域名后缀为两段（有用“.”分隔）
                if (hs.Length > 3)
                    return host.Substring(host.Substring(0, p1).LastIndexOf('.'));
                else
                    return host.TrimStart('.');
            }
            else if (hs.Length == 2)
            {
                return host.TrimStart('.');
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
