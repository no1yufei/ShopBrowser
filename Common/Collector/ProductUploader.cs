using Common.Shopee.API.Data;
using Common.Shopee.API.Data.Product;
using Common.Tools;
using Common.Tools.ProdFormater;
using CommonData.SysData.Enum;
using ShopeeChat.Shopee.API;
using ShopeeChat.Shopee.API.Data;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.Shopee.API.Data.DiscountList;

namespace Common.Collector
{
    public class ProductUploader
    {
        public delegate void ShowLogDele(string str);
        public ShowLogDele showLog;

        public delegate String GetHTMLDele(string url);
        public GetHTMLDele GetHtml;
        public bool isTanslate = true;
        public bool isModifyPrice = true;
        public bool isDescAddTags = true;
        public bool isTitleAddTags = true;
        string firstLang;// = StoreRegionMap.GetDescriptionFirstLang(store.RegionID);
        string sendLang;// = StoreRegionMap.GetDescriptionFirstLang(store.RegionID);
        ShopeeAPI api = new ShopeeAPI();
        double currencyRate;//  = api.GetCurrencyRate(store.RegionID);
        double postFee;//  = RegionBaseInfo.GetPostFee(store.RegionID);
        ProductInfoConstraints contraints;// = api.GetProductInfoConstraints(store);
        ProductInfoFeatureConfigs configs;// = api.GetProductInfoFeatureConfigs(store);
        ProductStatisticalData productStatisticalData;// = api.GetProductStatisticalData(store);
        //int remainProductNum;// = productStatisticalData.count_for_limit - productStatisticalData.product_count_for_limit;

        long promotionId = 0;
        Store store;
        GoogleTrans gtrans = new GoogleTrans();
        public ProductUploader(ShowLogDele showlog)
        {
            this.showLog = showlog;
        }
        public ProductUploader()
        {
            this.showLog = ShowLog;
        }

        public void IninPublicInformation(Store store)
        {
            //获取公共配置数据
            showLog("获取公共配置数据");
            firstLang = StoreRegionMap.GetDescriptionFirstLang(store.RegionID);
            sendLang = StoreRegionMap.GetDescriptionSecondeeLang(store.RegionID);
            api = new ShopeeAPI();
            currencyRate = api.GetCurrencyRate(store.RegionID);
            postFee = RegionBaseInfo.GetPostFee(store.RegionID);
            configs = api.GetProductInfoFeatureConfigs(store);
            contraints = api.GetProductInfoConstraints(store); 
            productStatisticalData = api.GetProductStatisticalData(store);
            //remainProductNum = productStatisticalData.count_for_limit - productStatisticalData.product_count_for_limit;
            updatePromotionId(store);
            if(store.DefaultCataIds == null)
            {
                store.DefaultCataIds = api.GetDefaultCatalogPath(store);
            }
            this.store = store;
        }
        private void updatePromotionId(Store store)
        {
            ShopeeDiscountItem discountitem = api.GetDiscountItem(store);
            if (null != discountitem)
            {
                promotionId = discountitem.discount_id;
            }
        }
        void ShowLog(string str)
        {
            Console.WriteLine(str);
        }
        char[] spliteChars = new char[] { ' ', ',', '.', ';', '，', '。', '；', '、', '\r', '\n', '|' };
        private bool getRecommendCateIds(Store store, string[] keyword, string title, string description, out int[][] outCatids)
        {

            bool ret = true;
            ShopeeAPI api = new ShopeeAPI();
            int[][] cataids = null;
            if (null == cataids || cataids.Length <= 0)
            {
                //cataids = api.GetProductRecommendCateIds(store, keyword);
                showLog("根据关键词推荐类目:" + keyword);
                if (null == cataids || cataids.Length <= 0)
                {
                    foreach (string word in keyword)
                    {
                        cataids = api.GetProductRecommendCateIds(store, word);
                        if (null != cataids && cataids.Length > 0)
                        {
                            break;
                        }
                    }
                }
            }

            if (null == cataids || cataids.Length <= 0)
            {
                showLog("根据标题推荐类目:" + title);
                cataids = api.GetProductRecommendCateIds(store, title);
                if (null == cataids || cataids.Length <= 0)
                {
                    foreach (string word in title.Split(spliteChars))
                    {
                        cataids = api.GetProductRecommendCateIds(store, word);
                        if (null != cataids && cataids.Length > 0)
                        {
                            ret = false;
                            break;
                        }
                    }
                }
            }
            if (null == cataids || cataids.Length <= 0)
            {
                ret = false;
                showLog("根据描述推荐类目");
                foreach (string word in description.Split(spliteChars))
                {
                    cataids = api.GetProductRecommendCateIds(store, word);
                    if (null != cataids && cataids.Length > 0)
                    {
                        break;
                    }
                }
            }
            outCatids = cataids;
            return ret;
        }


        private string[] translateStringList(GoogleTrans trans, String[] strs, string to, string from = "auto")
        {
            string f = "";
            foreach (string s in strs)
            {
                f += s + "|";
            }
            f = f.Trim('|');
            return trans.Translate(f, to, from).Split('|');
        }
        private bool isInStore(ParserProductInfo binfo)
        {
            string SKU = binfo.SKU;
            ProductPageListInfo pginfo = api.SearchProductPageListInfo(store, "sku", SKU, "",1, 1);
            if (null != pginfo && pginfo.page_info.total > 0)
            {
                showLog("当前商品已经在该店铺当中上架！" + binfo.Name);
                binfo.Status = ParserStatus.StatusListed;
                binfo.SID = pginfo.list[0].id;
                return true;
            }
            pginfo = api.SearchProductPageListInfo(store, "sku", "JTAL" + binfo.PID, "",1, 1);
            if (null != pginfo && pginfo.page_info.total > 0)
            {
                showLog("当前商品已经在该店铺当中上架！" + binfo.Name);
                binfo.Status = ParserStatus.StatusListed;
                binfo.SID = pginfo.list[0].id;
                return true;
            }
            return false;
        }
        private double computeLocalPrice(double collectPrice,double postfeem,double addtionalPrice,double currency,double multiPrice)
        {
            double price = collectPrice;
            if (isModifyPrice)
            {
                price = ((collectPrice + addtionalPrice) * currency + postfeem) * multiPrice;
                if (store.RegionID.Equals("vn")|| store.RegionID.Equals("id"))
                {                    
                    if (price > 1000)
                    {
                        int tempPrice = (int)price / 1000;
                        price = tempPrice * 1000;
                    }
                }
            }

            return Math.Round(price,2);
        }
        public long UploadDataToShopee(bool tans,bool isMp,bool isDis,bool isTitleAdd,bool isDescAdd, ProdFormat detailData, string eeSKU,double multiplePrice, double addtionalPrice, int discount, int limitNum, int wholeDiscount, int wholeLowNum, string txtMark = "", string imgMark = "", int imgRisizePercent = 98)
        {
            long shopee_product_id = 0;
            this.isTanslate = tans;
            this.isModifyPrice = isMp;
            this.isTitleAddTags = isTitleAdd;
            this.isDescAddTags = isDescAdd;
            //整理还要判断是否曾经上架过，由于sku编写规则未统一，暂时未判断。
            //此处的multiplePrice和addtionalPrice构成的公式,后期建议升级为解析字符串公式，传入公式字符串再解析 参考 ExpressionEvaluator
            shopee_product_id = uploadDataToShopee(out ParserStatus.StatusFailToUpload, eeSKU, detailData, multiplePrice, addtionalPrice,wholeDiscount,wholeLowNum ,txtMark, imgMark, imgRisizePercent);
            if (isDis)
            {
                if (shopee_product_id > 0 && discount > 0 && (wholeDiscount <= 50 || detailData.beginAmount < 2 || null != detailData.skuMap.FirstOrDefault(p => p.price != detailData.pMaxPrice)))
                {
                    api.UpdateProductDiscount(store, promotionId, shopee_product_id, discount, limitNum);
                }
            }

            return shopee_product_id;
        }
        private long uploadDataToShopee(out string status,string SKU, ProdFormat detailData, double multiplePrice, double addtionalPrice, int wholeDiscount, int wholeLowNum, string txtMark = "", string imgMark = "", int imgRisizePercent = 98)
        {
            status = ParserStatus.StatusFailToUpload;
            long id = 0;
            List<ProductCreateInfo> prodInfos = new List<ProductCreateInfo>();
            gtrans.TimeSpan = 5000;
            Console.WriteLine("开始处理当前数据");
            ProductCreateInfo prodInfo = new ProductCreateInfo();
            if (store.RegionID.Equals("tw"))
            {
                prodInfo.days_to_ship = 3 ;
            }
            string ID = detailData.offerid;
            string title = detailData.pTitle;

            string description = detailData.pDescription;
            double maxPrice = Math.Round(detailData.pMaxPrice,2);

            List<string> slideImg = detailData.pSlideImages;
            List<string> variImg = detailData.pVariImages;


            showLog("开始处理采集品：" + title);
            //showLog("检查是否有重复上架商品：" + title);

            //处理类目
            showLog("获取推荐类目");
            showLog("翻译关键词");
            string[] local = isTanslate ? translateStringList(gtrans, detailData.wKeywords, firstLang, "zh-cn") : detailData.wKeywords;
            //翻译标题
            showLog("翻译标题");
            prodInfo.name = isTanslate ? gtrans.Translate(title, firstLang, "zh-cn") : title;
            //翻译描述.
            showLog("翻译描述");
            prodInfo.description = isTanslate ? gtrans.Translate(description, firstLang, "zh-cn") : description;
            int[][] cataids = null;
            //取得推荐类目，并判断是否直接上架
            showLog("取得推荐类目，并判断是否直接上架");
            bool unlisted = !getRecommendCateIds(store, local, prodInfo.name, prodInfo.description, out cataids);
            if (null == cataids || cataids.Length <= 0)
            {
                if(null == store.DefaultCataIds)
                {
                    showLog("不能找到推荐的类目,不能找到默认类目,无法上传");
                    status = ParserStatus.StatusFailToUpload;
                    return id;
                }
                else
                {
                    showLog("*****使用系统默认类目，请上传后手动修改！********");
                    cataids = new int[][] { store.DefaultCataIds };
                    unlisted = true;
                }
               
            }
            prodInfo.category_path = cataids[0];
            int cateId = cataids[0][cataids[0].Length - 1];

            showLog("标题翻译为" + firstLang + ":" + title);
            //处理属性
            showLog("处理产品属性");
            prodInfo.attribute_model = api.GetProductCategoryAttributes(store, cateId,sendLang);//根据本地语言获取属性的字，对非中文网站属性才能正确
            prodInfo.brand = "No Brand";

            //调整标题长度
            showLog("调整标题长度");
            if (isTitleAddTags)
            {
                foreach (string str in local)
                {
                    prodInfo.name = prodInfo.name + " " + str;
                }
            }
            while (prodInfo.name.Length < contraints.title_length_min)
            {
                prodInfo.name += prodInfo.name + "[标题过短]";
                unlisted = true;
            }

            prodInfo.name = store.TitleFormat.Replace("[title]", prodInfo.name);
            prodInfo.name = prodInfo.name.Substring(0, Math.Min(prodInfo.name.Length, contraints.title_length_max)).Trim();


            prodInfo.description = store.DescriptionFormat.Replace("[description]", prodInfo.description);
            //添加标签
            if (isDescAddTags)
            {
                showLog("添加标签");
                prodInfo.description += "\r\n";
                foreach (string str in local)
                {
                    if (str.Length > 0)
                    {
                        //prodInfo.description += "\r\n" + "#" + str;
                        prodInfo.description += "#" + str;//标签要求放在一排
                    }
                }
            }


            //调整描述长度
            showLog("调整长度描述");

            while (prodInfo.description.Length < contraints.description_length_min)
            {
                prodInfo.description += prodInfo.description + "[描述过短]";
                unlisted = true;
            }

            prodInfo.description = prodInfo.description.Substring(0, Math.Min(prodInfo.description.Length, contraints.description_length_max));

            //产品尺寸
            showLog("产品尺寸");
            prodInfo.dimension = new ProductDimension(30, 20, 30);

            //重量，运费及运输方式
            double weight = 0.5;
            if (store.RegionID.ToLower().Equals("vn"))
            {
                weight = 500;
            }
            showLog("重量，运费及运输方式,默认重量(KG)：" + weight);
            prodInfo.logistics_channels = api.GetProductLogisticsChannel(store, cateId, weight);
            prodInfo.weight = weight.ToString();

            //计算价格
            showLog("计算价格");
            double price = computeLocalPrice(maxPrice, postFee, addtionalPrice, currencyRate, multiplePrice);//毛利控制
            prodInfo.price = price.ToString();
            //prodInfo.price_before_discount = price.ToString();
            showLog("计算价格:" + price.ToString());

            prodInfo.unlisted = unlisted;
            prodInfo.parent_sku = SKU;
            prodInfo.stock = (int)contraints.stock_max - 1;

            //解决淘宝规格图像在规格二的问题
            if(detailData.skuProps.Count > 1 
                && null == detailData.skuProps[0].value.FirstOrDefault(p=>p.imageUrl != null)
                && null != detailData.skuProps[1].value.FirstOrDefault(p => p.imageUrl != null))
            {
                SkuPropsItem tem = detailData.skuProps[0];
                detailData.skuProps.RemoveAt(0);
                detailData.skuProps.Insert(1, tem);
            }

            //解析规格一
            showLog("解析规格");
            int maxVarationOne = configs.variation_limit.one_tier_limit - 1;
            int maxVarationTotal = configs.variation_limit.total_limit - 1;
            List<VariationTheme> theme1s = new List<VariationTheme>();
            int theme1MaxLen = 0;
            if (detailData.skuProps != null && detailData.skuProps.Count > 0)
            {
                string themeName = detailData.skuProps[0].prop;

                SkuPropsItemValue[] propValues = detailData.skuProps[0].value.ToArray();
                for (int i = 0; i < propValues.Length; i = i + maxVarationOne)
                {
                    //每个最大变体规格
                    int curLen = propValues.Length - i > maxVarationOne ? maxVarationOne : propValues.Length - i;
                    //保存变体一的最大长度，便于计算变体二的长度，变体1,2组合，不能超过最大值
                    theme1MaxLen = curLen > theme1MaxLen ? curLen : theme1MaxLen;
                    VariationTheme theme = new VariationTheme();
                    theme.name = themeName;
                    //theme.options = 
                    List<string> options = new List<string>();
                    List<string> imgs = new List<string>();
                    for (int j = 0; j < curLen; j++)
                    {
                        options.Add(propValues[i + j].name);
                        if (propValues[i + j].imageUrl != null && propValues[i + j].imageUrl.Contains("http"))
                        {
                            imgs.Add(propValues[i + j].imageUrl);
                        }
                        else
                        {
                            imgs.Add("");
                        }
                    }
                    theme.options = options.ToArray();
                    if (options.Count == imgs.Count)
                    {
                        theme.images = imgs.ToArray();
                    }
                    theme1s.Add(theme);
                }
            }
            showLog("解析规格一，共：" + theme1s.Count);

            //解析规格2
            List<VariationTheme> theme2s = new List<VariationTheme>();
            if (detailData.skuProps != null &&  detailData.skuProps.Count > 1)
            {
                string themeName = detailData.skuProps[1].prop;

                SkuPropsItemValue[] propValues = detailData.skuProps[1].value.ToArray();
                int theme2maxLen = Math.Min(maxVarationTotal / theme1MaxLen, maxVarationOne);
                for (int i = 0; i < propValues.Length; i = i + theme2maxLen)
                {
                    //每个最大变体规格
                    int curLen = propValues.Length - i > theme2maxLen ? theme2maxLen : propValues.Length - i;

                    VariationTheme theme = new VariationTheme();
                    theme.name = themeName;
                    //theme.options = 
                    List<string> options = new List<string>();
                    List<string> imgs = new List<string>();
                    for (int j = 0; j < curLen; j++)
                    {
                        options.Add(propValues[i + j].name);
                        if (propValues[i + j].imageUrl != null && propValues[i + j].imageUrl != "" && propValues[i + j].imageUrl.Contains("http"))
                        {
                            imgs.Add(propValues[i + j].imageUrl);
                        }
                    }
                    theme.options = options.ToArray();
                    if (options.Count == imgs.Count)
                    {
                        theme.images = imgs.ToArray();
                    }
                    theme2s.Add(theme);
                }
            }
            showLog("解析规格二，共：" + theme2s.Count);


            //创建model 这是组合后的变体
            showLog("创建model组合变体");
            bool bWholesale = true;
            List<List<ModelInfo>> modelLists = new List<List<ModelInfo>>();
            List<List<VariationTheme>> themeVariations = new List<List<VariationTheme>>();
            //每个变体必须交叉组合，如果原商品没有这个组合，这里组合后，库存直接设置为0
            if (theme2s.Count > 0)//有两个变体规格
            {
                foreach (VariationTheme theme1 in theme1s)
                {

                    foreach (VariationTheme theme2 in theme2s)
                    {

                        List<VariationTheme> themeVariation = new List<VariationTheme>();
                        themeVariation.Add(theme1);
                        themeVariation.Add(theme2);

                        List<ModelInfo> models = new List<ModelInfo>();
                        int t1 = 0;
                        foreach (string theme1Option in theme1.options)
                        {
                            if (theme1.options == null || theme2.options == null)
                            {
                                continue;
                            }
                            int t2 = 0;
                            foreach (string theme2Option in theme2.options)
                            {
                                ModelInfo model = new ModelInfo();
                                model.tier_index = new int[2];
                                model.tier_index[0] = t1;
                                model.tier_index[1] = t2++;
                                string modelStr = theme1Option + "_" + theme2Option;
                                model.name = modelStr;
                                model.sku = SKU + "@" + theme1Option + "_" + theme2Option;// modelStr.Replace(">", "_");
                                
                                model.stock = 0;
                                model.price = price.ToString();
                                SkuMapItem skuMapItem = (detailData.skuMap.FirstOrDefault(p => p.name == theme1Option + ">" + theme2Option));
                                if (null == skuMapItem)
                                {
                                    skuMapItem = (detailData.skuMap.FirstOrDefault(p => p.name == theme2Option + ">" + theme1Option));
                                }
                                if (null != skuMapItem)
                                {
                                    double variprice = computeLocalPrice(skuMapItem.price, postFee, addtionalPrice, currencyRate, multiplePrice);// skuMapItem.price * multiplePrice + addtionalPrice;//毛利控制
                                    model.price = variprice.ToString();
                                    model.stock = (int)Math.Min(skuMapItem.canBookCount, contraints.stock_max - 1);
                                }
                                bWholesale = (model.price == price.ToString()) && bWholesale;
                                showLog("变体名：" + model.name + ";SKU：" + model.sku+";价格:"+model.price);
                                models.Add(model);
                            }
                            t1++;
                        }
                        modelLists.Add(models);
                        themeVariations.Add(themeVariation);
                    }
                }
            }
            else
            {
                foreach (VariationTheme theme1 in theme1s)
                {
                    if (theme1.options == null)
                    {
                        continue;
                    }
                    List<ModelInfo> models = new List<ModelInfo>();
                    List<VariationTheme> themeVariation = new List<VariationTheme>();
                    themeVariation.Add(theme1);
                    int t1 = 0;
                    foreach (string theme1Option in theme1.options)
                    {
                        ModelInfo model = new ModelInfo();
                        model.tier_index = new int[1];
                        model.tier_index[0] = t1++;
                        string modelStr = theme1Option;
                        model.name = modelStr;
                        model.sku = SKU + "@" + theme1Option;// modelStr.Replace(">", "_");
                        showLog("变体名：" + model.name + "变体SKU：" + model.sku);
                        model.stock = 0;
                        model.price = price.ToString();
                        SkuMapItem skuMapItem = (detailData.skuMap.FirstOrDefault(p => p.name == theme1Option));
                        if (null != skuMapItem)
                        {
                            double variprice = computeLocalPrice(skuMapItem.price, postFee, addtionalPrice, currencyRate, multiplePrice);// skuMapItem.price * multiplePrice + addtionalPrice;//毛利控制
                            model.price = variprice.ToString();
                            model.stock = (int)Math.Min(skuMapItem.canBookCount,contraints.stock_max -1);
                        }
                        bWholesale = (model.price == price.ToString()) && bWholesale;
                        models.Add(model);
                    }
                    modelLists.Add(models);
                    themeVariations.Add(themeVariation);
                }
            }
            if(bWholesale && wholeDiscount > 50)
            {
                int min = Math.Max(detailData.beginAmount, wholeLowNum);
                prodInfo.wholesale_list = new WholesaleInfo[] { new WholesaleInfo(min, 2* min, price * wholeDiscount/100) };
            }
            else
            {
                prodInfo.wholesale_list = new WholesaleInfo[0];
            }
            //处理幻灯片图片
            string downPicFolder = Guid.NewGuid().ToString();
            showLog("处理幻灯片图片" + slideImg.Count);
            List<string> slidImageIdList = new List<string>();
            int imageIndex = 0;
            foreach (string item in slideImg)
            {
                showLog("正在处理幻灯" + item.Trim());
                if (item.Trim().Length > 0 && item.Trim().Contains("http") && slidImageIdList.Count < 9)// && item.Trim().Contains(".jpg")
                {
                    string file = Tool.DownloadImage(downPicFolder.ToString(), item);
                    if (null != file)
                    {
                        if (imageIndex > 1)
                        {
                            ImageHelper.AddWaterMark(file, txtMark, imgMark);
                        }

                        ImageHelper.ResizeImage(file, imgRisizePercent);
                        string imgId = api.UploadImage(store, file);
                        if (imgId.Length > 5)
                        {
                            slidImageIdList.Add(imgId);

                        }
                    }
                }
            }
            prodInfo.images = slidImageIdList.ToArray();

            //处理变体图片
            showLog("处理变体图片,并翻译变体名称");
            imageIndex = 0;
            foreach (List<VariationTheme> themes in themeVariations)
            {
                foreach (VariationTheme theme in themes)
                {
                    if (isTanslate)
                    {
                        theme.name = gtrans.Translate(theme.name, firstLang, "zh-cn");
                    }
                    
                    //
                    theme.name = theme.name.Substring(0, Math.Min(14, theme.name.Length));
                    List<String> strs = new List<string>();
                    strs.AddRange(theme.options);
                    if (isTanslate)
                    {
                        theme.options = translateStringList(gtrans, strs.ToArray(), firstLang, "zh-cn");
                    }
                    

                    for (int i = 0; i < theme.options.Length; i++)
                    {
                        //theme.options[i] = gtrans.Translate(theme.options[i], firstLang, "zh-cn");
                        //防止翻译后过长，截断后使得多有的变体选项名称变成一样。
                        if(theme.options[i].Length >  20)
                        {
                            Regex regNum = new Regex("^[0-9]");
                            string addtionalStr = (i + 1).ToString();
                            if (regNum.IsMatch(theme.options[i]))
                            {
                                addtionalStr +="|";
                            }
                            theme.options[i] = (addtionalStr + theme.options[i]).Substring(0, 20);
                        }
                        
                    }
                    if(theme.images == null)
                    {
                        theme.images = new string[0];
                    }
                    else
                    {
                        for (int i = 0; i < theme.images.Length; i++)
                        {

                            string item = theme.images[i];

                            if (item.Trim().Length > 0 && item.Trim().Contains("http"))// && item.Trim().Contains(".jpg")
                            {
                                showLog("正在处理变体图片" + item.Trim());
                                string file = Tool.DownloadImage(downPicFolder.ToString(), item);
                                if (null != file)
                                {
                                    //ImageHelper.AddWaterMark(file, txtMark, imgMax);
                                    ImageHelper.ResizeImage(file, imgRisizePercent);
                                    string imgId = api.UploadImage(store, file);
                                    if (imgId.Length > 5)
                                    {
                                        theme.images[i] = imgId;
                                    }
                                    else
                                    {
                                        theme.images[i] = "";
                                    }
                                }
                                else
                                {
                                    theme.images[i] = "";
                                }
                            }
                        }
                    }
                    
                }
            }

            //清除图片临时缓存
            Tool.ClearImage(downPicFolder.ToString());
            showLog("清除图片临时缓存");
            showLog("准备上传");

            //没有变体的需要进入循环一次
            if (modelLists.Count < 1)
            {
                modelLists.Insert(0, new List<ModelInfo>());
                themeVariations.Insert(0, new List<VariationTheme>());
            }

            string tempTile = prodInfo.name;
            for (int i = 0; i < modelLists.Count && i < themeVariations.Count; i++)
            {
                List<ModelInfo> models = modelLists[i];
                List<VariationTheme> thems = themeVariations[i];

                if (modelLists.Count > 1)
                {
                    prodInfo.name = "[" + (i + 1) + "/" + modelLists.Count + "]" + tempTile;
                    if (prodInfo.name.Length > contraints.title_length_max)
                    {
                        prodInfo.name = prodInfo.name.Substring(0, contraints.title_length_max).Trim();
                    }
                }
                if (models == null || models.Count < 1 || models[0].name == "" || thems == null || thems.Count < 1 || thems[0].name == "")
                {
                    prodInfo.model_list = new ModelInfo[0];
                    prodInfo.tier_variation = new VariationTheme[0];
                }
                else
                {
                    prodInfo.model_list = models.ToArray();
                    prodInfo.tier_variation = thems.ToArray();
                }

                prodInfos.Add(prodInfo);

                ProductCreateResult ret = api.CreateProducts(store, prodInfos.ToArray());
                if (ret == null || ret.result == null || null != ret.result.FirstOrDefault(p => p.code != 0))
                {
                    string message = ret != null ? ret.result[0].message + "/" + ret.result[0].user_message : "返回空值";
                    showLog("失败上架：" + (ret != null ? ret.result[0].message + "/" + ret.result[0].user_message : "返回空值"));
                    status = ParserStatus.StatusFailToUpload;
                    if(message.Contains("product count has reached upper limit"))
                    {
                        showLog("店铺货品已经达到上限，退出上传！");
                        return -1;
                    }
                    
                    if (prodInfos[0].description.Contains("****翻译错误"))
                    {
                        showLog("\n翻译到达限额，本条翻译失败！将等待30秒后重试！");
                        Thread.Sleep(30000);
                    }
                }
                else
                {
                    id = ret.result[0].data.product_id;
                    showLog("上传成功：" + ret.result[0].data.product_id);
                    if (unlisted)
                    {
                        showLog("上传成功但未上架，需要进一步核实产品信息，尤其是分类。" + ret.result[0].data.product_id);
                        status = ParserStatus.StatusUnListed;
                    }
                    else
                    {
                        status = ParserStatus.StatusListed;
                    }
                }
                prodInfos.Clear();
            }
            showLog("产品:" + status + "，可以去后台查看再修改");
            return id;
        }

        public long UploadShopeeProduct(Parser parser, ParserProductInfo binfo, double multiplePrice, double addtionalPrice, int discount, int limitNum, int wholeDiscount, int wholeLowNum, string txtMark = "", string imgMark = "", int imgRisizePercent = 98)
        {

            if (multiplePrice == 0)
            {
                MessageBox.Show("提价的倍数不能为零");
                return binfo.SID;
            }
            List<ProductCreateInfo> prodInfos = new List<ProductCreateInfo>();
            gtrans.TimeSpan = 5000;

            //没有选择，或者已经处理过的，不再上传
            if (!binfo.Selected || binfo.PID == null || binfo.PID == "" || binfo.Status != ParserStatus.StatusUnHandle)
            {
                showLog("当前行已经被处理，退出上传");
                return binfo.SID;
            }
            showLog("开始处理采集品：" + binfo.Name);
            showLog("检查是否有重复上架商品：" + binfo.Name);
            if (isInStore(binfo))
            {
                showLog("当前商品已经在该店铺当中上架，不再重新采集上架！" + binfo.Name);
                binfo.Status = ParserStatus.StatusListed;
                return binfo.SID;
            }
            showLog("开始提取并解析：" + binfo.URL);
            string html = parser.GetPageHtml(binfo);
            ProdFormat detailData = parser.PaserDetailPage(html);
           
            if (detailData == null || detailData.offerid == null)
            {
                if(null != GetHtml)
                {
                    html = GetHtml(binfo.URL);
                    detailData = parser.PaserDetailPage(html);
                }
                if (detailData == null || detailData.offerid == null)
                {
                    showLog("提取详情页面失败：" + binfo.PID);
                    return binfo.SID;
                }
                binfo.HTML = html;   
            }
            if (detailData.NetError)
            {
                showLog("提取详情页面失败：出现网络错误，或当前页面需要验证！");
                return -1000;
            }
            binfo.SID = uploadDataToShopee(out binfo.Status, binfo.SKU,detailData, multiplePrice, addtionalPrice, wholeDiscount, wholeLowNum, txtMark, imgMark, imgRisizePercent);
            if(binfo.Status == ParserStatus.StatusFailToUpload)
            {
                showLog("产品上传失败,请拷贝并反馈此链接："+ binfo.URL);
            }
            if (binfo.SID > 0 && discount > 0 && (wholeDiscount <= 50 || detailData.beginAmount < 2 || null != detailData.skuMap.FirstOrDefault(p=>p.price != detailData.pMaxPrice)))
            {
                updatePromotionId(store);
                api.UpdateProductDiscount(store, promotionId, binfo.SID, discount, limitNum);
            }
            showLog("产品上传" + binfo.Status + "可以去后台查看再修改");
            return binfo.SID;
        }
        public void UploadShopeeProducts(Parser parser, double multiplePrice, double addtionalPrice,int discount = 0 ,int limitNum = 0,int wholeDiscount = 0, int wholeLowNum = 0, string txtMark = "", string imgMark = "", int imgRisizePercent = 98)
        {

            if (multiplePrice == 0)
            {
                MessageBox.Show("提价的倍数不能为零");
                return;
            }
            List<ParserProductInfo> productDatas = parser.ParserProductInfos;
   
            int num = 0;
            foreach (ParserProductInfo binfo in productDatas)
            {
                try
                {
                    //没有选择，或者已经处理过的，不再上传
                    if (!binfo.Selected || binfo.PID == null || binfo.PID == "" || binfo.Status != ParserStatus.StatusUnHandle)
                    {
                        //showLog("当前行已经被处理跳到下一行");
                        continue;
                    }
                    num++;
                    Console.WriteLine("开始处理第" + num + "条数据");

                    if (num == 2 && firstLang.ToLower() != "zh-tw")
                    {
                        MessageBox.Show("由于翻译接口访问的限制，一次操作上货超过一条，速度将会被调整到*分钟上传一条。");
                        gtrans.TimeSpan = 30000;
                    }
                    long sid = UploadShopeeProduct(parser, binfo, multiplePrice, addtionalPrice,discount,limitNum, wholeDiscount, wholeLowNum, txtMark, imgMark, imgRisizePercent);
                    if(sid < 0)
                    {
                        break;
                    }
                    if(!AccessControl.Instance.IsLevelRight(UserLevel.VIPUser) && num > 5)
                    {
                            Console.WriteLine("当前用户单次最大上传限额为5个！");
                            break;
                    }
                    if(sid < 0)
                    {
                        break;
                    }
                }
                catch(Exception xe)
                {
                    showLog("上传产品出现异常：" + xe.Message);
                }
            }
            foreach (ParserProductInfo binfo in productDatas)
            {
                if (binfo.Status == ParserStatus.StatusFailToUpload && binfo.Selected)
                {
                    showLog("产品上传失败,请拷贝并反馈此链接：" + binfo.URL);
                }
            }
                
        }
    }
}
