﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools.ProdFormater
{
    public class ProdFormatTextTool
    {
        public static string[] filterString = { "品牌","贴牌","代工","改版","条形码","建议零售价", "价格段", "价格", "包装价格多少", "大陆联邦特效价要求", "定做的价格", "价格是否含包装", "全国统一零售价", "淘宝限价",
        "营销价格", "加工定制", "加工方式", "加工工艺", "深圳加工生产厂家", "是否加工定制", "加工地址", "加工级别", "加工制作", "可加工", "可加工范围", "年剩余加工能力",
        "外贸订单加工", "印后加工", "产地", "原产地", "产地（国内）", "机芯产地", "厂家(产地)", "生产地址", "主面料产地是否进口", "品牌产地", "生产地", "是否支持一件代发",
        "OEM 一件代发", "可否代发", "是否一件代发", "是否支持代发", "一件代发", "支持一件代发", "是否外贸", "是否专供外贸", "外贸类型", "外贸", "外贸爆款平台",
        "外贸礼品定制", "外贸品质", "是否专利货源", "是否有专利", "专利号", "专利类型", "专利", "专利及著作权", "专利及著作权申请时间", "专利号或版权登记证号", "外观专利号",
        "产品专利号", "设计专利", "专利产品", "加印LOGO", "LOGO印刷", "丝印国外LOGO", "OEM", "OEM定制", "是否支持OEM", "可OEM", "可否OEM", "是否进口",
        "进口地", "进口", "进口芯片", "是否支持混批", "混批起批量", "批号", "深圳华强北批发实体店", "支持混批", "现货批发", "销售批发", "货源类别", "货源",
        "是否跨境电商货源", "货源类型", "发票", "售后服务", "物流服务", "特色服务", "服务", "免费服务", "增值服务", "FBA服务", "服务类型", "服务内容", "服务项目",
        "服务信息", "立方速贴心服务", "商家服务", "同城服务", "上市时间", "最快出货时间", "产品上市时间", "最晚发货时间", "交货时间", "打样时间", "货号", "3C证书编号",
        "产品编号", "贸易属性", "贸易类型", "发货物流公司", "快递物流", "是否支持代理加盟", "加盟分销门槛", "质保期", "质保", "保质期", "货期", "交货期", "质保年限",
        "商品类型", "开模定制", "是否库存", "库存类型", "是否支持分销", "是否出口", "销售序列号", "销售单位", "销售方式", "热销爆款平台", "主要销售渠道", "厂家直销",
        "工厂直销", "经销性质", "热销平台", "销售性质", "一件代销", "营销方式", "是否出口专用" };

        public static string[] titleFilter = { "新品现货批发", "厂家直销新款", "厂家现货批发", "一件代发", "现货批发", "批发零售", "批发代理", "广州批发", "定做批发", "大量现货", "厂家直销", "厂家批发", "中性", "新款", "批发", "国产", "包邮" };

        public static string replaceHtmlSpecChar(string html)
        {
            string newString = html;
            newString = newString.Replace("&quot;", "\"");
            newString = newString.Replace("&amp;", "&");
            newString = newString.Replace("&lt;", "<");
            newString = newString.Replace("&gt;", ">");
            newString = newString.Replace("&nbsp;", "");
            newString = newString.Replace("&cent;", "¢");
            newString = newString.Replace("&curren;", "¤");
            newString = newString.Replace("&brvbar;", "¦");
            newString = newString.Replace("&uml;", "¨");
            newString = newString.Replace("&ordf;", "ª");
            newString = newString.Replace("&not;", "¬");
            newString = newString.Replace("&reg;", "®");
            newString = newString.Replace("&deg;", "°");
            newString = newString.Replace("&sup2;", "²");
            newString = newString.Replace("&acute;", "´");
            newString = newString.Replace("&para;", "¶");
            newString = newString.Replace("&cedil;", "¸");
            newString = newString.Replace("&ordm;", "º");
            newString = newString.Replace("&frac14;", "¼");
            newString = newString.Replace("&frac34;", "¾");
            newString = newString.Replace("&Agrave;", "À");
            newString = newString.Replace("&Acirc;", "Â");
            newString = newString.Replace("&Auml;", "Ä");
            newString = newString.Replace("&AElig;", "Æ");
            newString = newString.Replace("&Egrave;", "È");
            newString = newString.Replace("&Ecirc;", "Ê");
            newString = newString.Replace("&Igrave;", "Ì");
            newString = newString.Replace("&Icirc;", "Î");
            newString = newString.Replace("&ETH;", "Ð");
            newString = newString.Replace("&Ograve;", "Ò");
            newString = newString.Replace("&Ocirc;", "Ô");
            newString = newString.Replace("&Ouml;", "Ö");
            newString = newString.Replace("&Oslash;", "Ø");
            newString = newString.Replace("&Uacute;", "Ú");
            newString = newString.Replace("&Uuml;", "Ü");
            newString = newString.Replace("&THORN;", "Þ");
            newString = newString.Replace("&agrave;", "à");
            newString = newString.Replace("&acirc;", "â");
            newString = newString.Replace("&auml;", "ä");
            newString = newString.Replace("&aelig;", "æ");
            newString = newString.Replace("&egrave;", "è");
            newString = newString.Replace("&ecirc;", "ê");
            newString = newString.Replace("&igrave;", "ì");
            newString = newString.Replace("&icirc;", "î");
            newString = newString.Replace("&eth;", "ð");
            newString = newString.Replace("&ograve;", "ò");
            newString = newString.Replace("&ocirc;", "ô");
            newString = newString.Replace("&ouml;", "ö");
            newString = newString.Replace("&oslash;", "ø");
            newString = newString.Replace("&uacute;", "ú");
            newString = newString.Replace("&uuml;", "ü");
            newString = newString.Replace("&thorn;", "þ");
            newString = newString.Replace("&OElig;", "Œ");
            newString = newString.Replace("&Scaron;", "Š");
            newString = newString.Replace("&Yuml;", "Ÿ");
            newString = newString.Replace("&circ;", "ˆ");
            newString = newString.Replace("&Alpha;", "Α");
            newString = newString.Replace("&Gamma;", "Γ");
            newString = newString.Replace("&Epsilon;", "Ε");
            newString = newString.Replace("&Eta;", "Η");
            newString = newString.Replace("&Iota;", "Ι");
            newString = newString.Replace("&Lambda;", "Λ");
            newString = newString.Replace("&Nu;", "Ν");
            newString = newString.Replace("&Omicron;", "Ο");
            newString = newString.Replace("&Rho;", "Ρ");
            newString = newString.Replace("&Tau;", "Τ");
            newString = newString.Replace("&Phi;", "Φ");
            newString = newString.Replace("&Psi;", "Ψ");
            newString = newString.Replace("&alpha;", "α");
            newString = newString.Replace("&gamma;", "γ");
            newString = newString.Replace("&epsilon;", "ε");
            newString = newString.Replace("&eta;", "η");
            newString = newString.Replace("&iota;", "ι");
            newString = newString.Replace("&lambda;", "λ");
            newString = newString.Replace("&nu;", "ν");
            newString = newString.Replace("&omicron;", "ο");
            newString = newString.Replace("&rho;", "ρ");
            newString = newString.Replace("&sigma;", "σ");
            newString = newString.Replace("&upsilon;", "υ");
            newString = newString.Replace("&chi;", "χ");
            newString = newString.Replace("&omega;", "ω");
            newString = newString.Replace("&upsih;", "ϒ");
            newString = newString.Replace("&ensp;", " ");
            newString = newString.Replace("&thinsp;", " ");
            newString = newString.Replace("&zwj;", "‍");
            newString = newString.Replace("&rlm;", "‏");
            newString = newString.Replace("&mdash;", "—");
            newString = newString.Replace("&rsquo;", "’");
            newString = newString.Replace("&ldquo;", "“");
            newString = newString.Replace("&bdquo;", "„");
            newString = newString.Replace("&Dagger;", "‡");
            newString = newString.Replace("&hellip;", "…");
            newString = newString.Replace("&prime;", "′");
            newString = newString.Replace("&lsaquo;", "‹");
            newString = newString.Replace("&oline;", "‾");
            newString = newString.Replace("&image;", "ℑ");
            newString = newString.Replace("&real;", "ℜ");
            newString = newString.Replace("&alefsym;", "ℵ");
            newString = newString.Replace("&larr;", "←");
            newString = newString.Replace("&rarr;", "→");
            newString = newString.Replace("&harr;", "↔");
            newString = newString.Replace("&crarr;", "↵");
            newString = newString.Replace("&uArr;", "⇑");
            newString = newString.Replace("&dArr;", "⇓");
            newString = newString.Replace("&forall;", "∀");
            newString = newString.Replace("&exist;", "∃");
            newString = newString.Replace("&nabla;", "∇");
            newString = newString.Replace("&notin;", "∉");
            newString = newString.Replace("&prod;", "∏");
            newString = newString.Replace("&minus;", "−");
            newString = newString.Replace("&radic;", "√");
            newString = newString.Replace("&infin;", "∞");
            newString = newString.Replace("&and;", "∧");
            newString = newString.Replace("&cap;", "∩");
            newString = newString.Replace("&int;", "∫");
            newString = newString.Replace("&sim;", "∼");
            newString = newString.Replace("&asymp;", "≈");
            newString = newString.Replace("&equiv;", "≡");
            newString = newString.Replace("&ge;", "≥");
            newString = newString.Replace("&sup;", "⊃");
            newString = newString.Replace("&sube;", "⊆");
            newString = newString.Replace("&oplus;", "⊕");
            newString = newString.Replace("&perp;", "⊥");
            newString = newString.Replace("&lceil;", "⌈");
            newString = newString.Replace("&lfloor;", "⌊");
            newString = newString.Replace("&loz;", "◊");
            newString = newString.Replace("&clubs;", "♣");
            newString = newString.Replace("&iexcl;", "¡");
            newString = newString.Replace("&pound;", "£");
            newString = newString.Replace("&yen;", "¥");
            newString = newString.Replace("&sect;", "§");
            newString = newString.Replace("&copy;", "©");
            newString = newString.Replace("&laquo;", "«");
            newString = newString.Replace("&shy;", "");
            newString = newString.Replace("&macr;", "¯");
            newString = newString.Replace("&plusmn;", "±");
            newString = newString.Replace("&sup3;", "³");
            newString = newString.Replace("&micro;", "µ");
            newString = newString.Replace("&middot;", "·");
            newString = newString.Replace("&sup1;", "¹");
            newString = newString.Replace("&raquo;", "»");
            newString = newString.Replace("&frac12;", "½");
            newString = newString.Replace("&iquest;", "¿");
            newString = newString.Replace("&Aacute;", "Á");
            newString = newString.Replace("&Atilde;", "Ã");
            newString = newString.Replace("&Aring;", "Å");
            newString = newString.Replace("&Ccedil;", "Ç");
            newString = newString.Replace("&Eacute;", "É");
            newString = newString.Replace("&Euml;", "Ë");
            newString = newString.Replace("&Iacute;", "Í");
            newString = newString.Replace("&Iuml;", "Ï");
            newString = newString.Replace("&Ntilde;", "Ñ");
            newString = newString.Replace("&Oacute;", "Ó");
            newString = newString.Replace("&Otilde;", "Õ");
            newString = newString.Replace("&times;", "×");
            newString = newString.Replace("&Ugrave;", "Ù");
            newString = newString.Replace("&Ucirc;", "Û");
            newString = newString.Replace("&Yacute;", "Ý");
            newString = newString.Replace("&szlig;", "ß");
            newString = newString.Replace("&aacute;", "á");
            newString = newString.Replace("&atilde;", "ã");
            newString = newString.Replace("&aring;", "å");
            newString = newString.Replace("&ccedil;", "ç");
            newString = newString.Replace("&eacute;", "é");
            newString = newString.Replace("&euml;", "ë");
            newString = newString.Replace("&iacute;", "í");
            newString = newString.Replace("&iuml;", "ï");
            newString = newString.Replace("&ntilde;", "ñ");
            newString = newString.Replace("&oacute;", "ó");
            newString = newString.Replace("&otilde;", "õ");
            newString = newString.Replace("&divide;", "÷");
            newString = newString.Replace("&ugrave;", "ù");
            newString = newString.Replace("&ucirc;", "û");
            newString = newString.Replace("&yacute;", "ý");
            newString = newString.Replace("&yuml;", "ÿ");
            newString = newString.Replace("&oelig;", "œ");
            newString = newString.Replace("&scaron;", "š");
            newString = newString.Replace("&fnof;", "ƒ");
            newString = newString.Replace("&tilde;", "˜");
            newString = newString.Replace("&Beta;", "Β");
            newString = newString.Replace("&Delta;", "Δ");
            newString = newString.Replace("&Zeta;", "Ζ");
            newString = newString.Replace("&Theta;", "Θ");
            newString = newString.Replace("&Kappa;", "Κ");
            newString = newString.Replace("&Mu;", "Μ");
            newString = newString.Replace("&Xi;", "Ξ");
            newString = newString.Replace("&Pi;", "Π");
            newString = newString.Replace("&Sigma;", "Σ");
            newString = newString.Replace("&Upsilon;", "Υ");
            newString = newString.Replace("&Chi;", "Χ");
            newString = newString.Replace("&Omega;", "Ω");
            newString = newString.Replace("&beta;", "β");
            newString = newString.Replace("&delta;", "δ");
            newString = newString.Replace("&zeta;", "ζ");
            newString = newString.Replace("&theta;", "θ");
            newString = newString.Replace("&kappa;", "κ");
            newString = newString.Replace("&mu;", "μ");
            newString = newString.Replace("&xi;", "ξ");
            newString = newString.Replace("&pi;", "π");
            newString = newString.Replace("&sigmaf;", "ς");
            newString = newString.Replace("&tau;", "τ");
            newString = newString.Replace("&phi;", "φ");
            newString = newString.Replace("&psi;", "ψ");
            newString = newString.Replace("&thetasym;", "ϑ");
            newString = newString.Replace("&piv;", "ϖ");
            newString = newString.Replace("&emsp;", " ");
            newString = newString.Replace("&zwnj;", "‌");
            newString = newString.Replace("&lrm;", "‎");
            newString = newString.Replace("&ndash;", "–");
            newString = newString.Replace("&lsquo;", "‘");
            newString = newString.Replace("&sbquo;", "‚");
            newString = newString.Replace("&rdquo;", "”");
            newString = newString.Replace("&dagger;", "†");
            newString = newString.Replace("&bull;", "•");
            newString = newString.Replace("&permil;", "‰");
            newString = newString.Replace("&Prime;", "″");
            newString = newString.Replace("&rsaquo;", "›");
            newString = newString.Replace("&frasl;", "⁄");
            newString = newString.Replace("&weierp;", "℘");
            newString = newString.Replace("&trade;", "™");
            newString = newString.Replace("&uarr;", "↑");
            newString = newString.Replace("&darr;", "↓");
            newString = newString.Replace("&lArr;", "⇐");
            newString = newString.Replace("&rArr;", "⇒");
            newString = newString.Replace("&hArr;", "⇔");
            newString = newString.Replace("&part;", "∂");
            newString = newString.Replace("&empty;", "∅");
            newString = newString.Replace("&isin;", "∈");
            newString = newString.Replace("&ni;", "∋");
            newString = newString.Replace("&sum;", "∑");
            newString = newString.Replace("&lowast;", "∗");
            newString = newString.Replace("&prop;", "∝");
            newString = newString.Replace("&ang;", "∠");
            newString = newString.Replace("&or;", "∨");
            newString = newString.Replace("&cup;", "∪");
            newString = newString.Replace("&there4;", "∴");
            newString = newString.Replace("&cong;", "≅");
            newString = newString.Replace("&ne;", "≠");
            newString = newString.Replace("&le;", "≤");
            newString = newString.Replace("&sub;", "⊂");
            newString = newString.Replace("&nsub;", "⊄");
            newString = newString.Replace("&supe;", "⊇");
            newString = newString.Replace("&otimes;", "⊗");
            newString = newString.Replace("&sdot;", "⋅");
            newString = newString.Replace("&rceil;", "⌉");
            newString = newString.Replace("&rfloor;", "⌋");
            newString = newString.Replace("&spades;", "♠");
            newString = newString.Replace("&hearts;", "♥");
            //=========中文标点替换
            newString = newString.Replace("，", ",");
            newString = newString.Replace("。", ".");
            newString = newString.Replace("；", ";");
            newString = newString.Replace("‘", "'");
            newString = newString.Replace("“", "\"");
            newString = newString.Replace("”", "\"");
            newString = newString.Replace("【", "[");
            newString = newString.Replace("】", "]");
            newString = newString.Replace("{", "{");
            newString = newString.Replace("}", "}");
            newString = newString.Replace("！", "!");
            newString = newString.Replace("￥", "$");
            newString = newString.Replace("（", "(");
            newString = newString.Replace("）", ")");
            newString = newString.Replace("+", "+");
            newString = newString.Replace("-", "-");
            newString = newString.Replace("&", "&");
            newString = newString.Replace("*", "*");
            return newString;
        }
    }
}
