using CsharpHttpHelper;
using ShopeeChat.Shopee.API;
using ShopeeChat.Shopee.API.Data;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using CommonData.Security;
using Common.Brower;
using Common.Browser;

namespace ShopeeChat.Shopee
{
    public partial class APITestForm : Form
    {
        ShopeeAPI api = new ShopeeAPI();
        Store store = new Store();
        public APITestForm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            StoreGroup group = groupCBox.SelectedItem as  StoreGroup;

            HtmlHttpHelper hhh = store.Hhh;
            if (null == hhh)
            {
                hhh = new HtmlHttpHelper();
                store.Hhh = hhh;
            }
            hhh.sCookies = "";
            hhh.Authorization = "";
            store.Token = null;
            hhh.bProxy = group.IsProxy;
            if (hhh.bProxy)
            {
                hhh.sProxyIP = group.ProxyIP;
                hhh.sProxyPort = group.Port.ToString();
                hhh.sProxyUserName = group.ProxyUserName;
                hhh.sProxyPassWord = group.Password;
            }
            Guid SPC_CDS = Guid.NewGuid();
            store.SPC_CDS = SPC_CDS;
            string querURL = comURL.Text;

            String dataPattern = dataRTBox.Text;
            store.Hhh.sCookies = cookieRTBox.Text;

            //store.Hhh.Origin = store.ServerURL;
            //store.Hhh.Referer = store.ServerURL + "/account/signin?next=%2F";
            store.Hhh.sContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            HttpResult spcresult  = store.Hhh.Post(querURL, dataPattern);


        }

        private void getBtn_Click(object sender, EventArgs e)
        {
            if(cookieRTBox.Text != "")
            {
                if (addCookieCBox.Checked)
                {
                    store.Hhh.sCookies = cookieRTBox.Text + store.Hhh.sCookies;
                }
                else
                {
                    store.Hhh.sCookies = cookieRTBox.Text;
                }
            }
            //WebBrowser web = new WebBrowser();
            //web.Navigate(commBaseURLTBox.Text + comURL.Text);
            //web.Navigated += Web_Navigated;
            //store.Hhh.bProxy = true;
            //store.Hhh.sProxyIP = "127.0.0.1";
            //store.Hhh.sProxyPort = "54110";
            resultRTBox.Text = store.Hhh.Get(commBaseURLTBox.Text + comURL.Text).Html + Environment.NewLine;
            resultRTBox.Text += store.Hhh.sCookies;
        }

        private void Web_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            WebBrowser web = (WebBrowser)sender;
            string joson = web.DocumentText;
        }

        private void postBtn_Click(object sender, EventArgs e)
        {
            if (cookieRTBox.Text != "")
            {
                if(addCookieCBox.Checked)
                {
                    store.Hhh.sCookies = cookieRTBox.Text + store.Hhh.sCookies;
                }
                else
                {
                    store.Hhh.sCookies = cookieRTBox.Text;
                }
                
            }
            //store.Hhh.bProxy = true;
            //store.Hhh.sProxyIP = "127.0.0.1";
            //store.Hhh.sProxyPort = "54110";
            //store.Hhh.Referer = "https://shopee.tw/shop/42974981/followers/?__classic__=1";
            store.Hhh.Header = headerRTBox.Text;
            resultRTBox.Text = store.Hhh.Post(commBaseURLTBox.Text + comURL.Text,dataRTBox.Text).Html + Environment.NewLine;
            resultRTBox.Text += store.Hhh.sCookies;
        }

        private void searchKeyBtn_Click(object sender, EventArgs e)
        {
            //string keyword = headerRTBox.Text;
            //store.Hhh.bProxy = true;
            //store.Hhh.sProxyIP = "127.0.0.1";
            //store.Hhh.sProxyPort = "54110";

            //store.UserName = userNameTBox.Text;
            //store.Password = passwordTBox.Text;
            //store.ServerURL = baseURLTBox.Text;
            //Region re = new Region("tw", "台湾", "https://seller.shopee.tw", "https://shopee.tw");
            //ShopeeAPI api = new ShopeeAPI();
            //SearchedProductInfo info = api.GetProductInfoByKeyword(re, store, keyword);
            //resultRTBox.Text = info.items.Count().ToString() + Environment.NewLine;
            //foreach (ProductItem item in info.items)
            //{
            //   RatingCustomInfos rating =  api.GetRatingCustoms(re,store,item.shopid, item.itemid, item.item_rating.rating_count[0]);
            //    resultRTBox.Text += rating.data.ratings.Count();
            //}
            if (cookieRTBox.Text != "")
            {
                if (addCookieCBox.Checked)
                {
                    store.Hhh.sCookies = cookieRTBox.Text + store.Hhh.sCookies;
                }
                else
                {
                    store.Hhh.sCookies = cookieRTBox.Text;
                }

            }
            //store.Hhh.Header = headerRTBox.Text;
            //resultRTBox.Text = store.Hhh.Put(commBaseURLTBox.Text + comURL.Text, dataRTBox.Text).Html + Environment.NewLine;
            ShopeeAPI api = new ShopeeAPI();
            resultRTBox.Text += api.UnFollowUser(store, headerRTBox.Text, dataRTBox.Text);
        }

        private void putBtn_Click(object sender, EventArgs e)
        {
            if (cookieRTBox.Text != "")
            {
                if (addCookieCBox.Checked)
                {
                    store.Hhh.sCookies = cookieRTBox.Text + store.Hhh.sCookies;
                }
                else
                {
                    store.Hhh.sCookies = cookieRTBox.Text;
                }

            }
            //store.Hhh.Header = headerRTBox.Text;
            //resultRTBox.Text = store.Hhh.Put(commBaseURLTBox.Text + comURL.Text, dataRTBox.Text).Html + Environment.NewLine;
            ShopeeAPI api = new ShopeeAPI();
            resultRTBox.Text += api.FollowUser(store, headerRTBox.Text, dataRTBox.Text);
        }

        private void orderSummaryBtn_Click(object sender, EventArgs e)
        {
            if (cookieRTBox.Text != "")
            {
                if (addCookieCBox.Checked)
                {
                    store.Hhh.sCookies = cookieRTBox.Text + store.Hhh.sCookies;
                }
                else
                {
                    store.Hhh.sCookies = cookieRTBox.Text;
                }

            }
            //store.Hhh.Header = headerRTBox.Text;
            //resultRTBox.Text = store.Hhh.Put(commBaseURLTBox.Text + comURL.Text, dataRTBox.Text).Html + Environment.NewLine;
            ShopeeAPI api = new ShopeeAPI();
            resultRTBox.Text += api.UpdateSummaryOrderInfo(store);
        }

        private void detailOrderBtn_Click(object sender, EventArgs e)
        {
            if (cookieRTBox.Text != "")
            {
                if (addCookieCBox.Checked)
                {
                    store.Hhh.sCookies = cookieRTBox.Text + store.Hhh.sCookies;
                }
                else
                {
                    store.Hhh.sCookies = cookieRTBox.Text;
                }

            }
            //store.Hhh.Header = headerRTBox.Text;
            //resultRTBox.Text = store.Hhh.Put(commBaseURLTBox.Text + comURL.Text, dataRTBox.Text).Html + Environment.NewLine;
            ShopeeAPI api = new ShopeeAPI();
            resultRTBox.Text += api.UpdateOrderInfo(store);
        }

        private void showOrderBtn_Click(object sender, EventArgs e)
        {
            StoreGroup shopGroup = new StoreGroup();
            List<StoreRegion> regions = initRegionData();
            regions.First(p => p.RegionName == "台湾").Stores.Add(store);
            shopGroup.Regions = regions;
           //OrderForm orderForm = new OrderForm();
           // orderForm.ShowDialog();
        }
        private List<StoreRegion> initRegionData()
        {
            List<StoreRegion> tempmerchantAccountInfos = new List<StoreRegion>();
            if (Path.GetFileName(Assembly.GetExecutingAssembly().Location).ToLower().Contains("tw"))
            {
                tempmerchantAccountInfos.Add(new StoreRegion("tw", "台湾", "https://seller.shopee.tw", "https://shopee.tw"));
                this.Text += "TW";
                Console.WriteLine("使用tw链接");
            }
            else
            {
                tempmerchantAccountInfos.Add(new StoreRegion("tw", "台湾", "https://seller.xiapi.shopee.cn", "https://xiapi.xiapibuy.com"));
                Console.WriteLine("使用cn链接");
            }
            tempmerchantAccountInfos.Add(new StoreRegion("id", "印度尼西亚", "https://seller.shopee.co.id", "https://shopee.co.id"));
            tempmerchantAccountInfos.Add(new StoreRegion("th", "泰国", "https://seller.shopee.co.th", "https://seller.shopee.co.th"));
            tempmerchantAccountInfos.Add(new StoreRegion("sg", "新加坡", "https://seller.shopee.sg", "https://shopee.sg"));
            tempmerchantAccountInfos.Add(new StoreRegion("ph", "菲律宾", "https://seller.shopee.ph", "https://shopee.ph"));
            tempmerchantAccountInfos.Add(new StoreRegion("my", "马来西亚", "https://seller.shopee.com.my", "https://shopee.com.my"));
            tempmerchantAccountInfos.Add(new StoreRegion("vn", "越南", "https://banhang.shopee.vn", "https://shopee.vn"));
            return tempmerchantAccountInfos;
        }

        private void downLoadBtn_Click(object sender, EventArgs e)
        {
            if (cookieRTBox.Text != "")
            {
                if (addCookieCBox.Checked)
                {
                    store.Hhh.sCookies = cookieRTBox.Text + store.Hhh.sCookies;
                }
                else
                {
                    store.Hhh.sCookies = cookieRTBox.Text;
                }
            }
            //WebBrowser web = new WebBrowser();
            //web.Navigate(commBaseURLTBox.Text + comURL.Text);
            //web.Navigated += Web_Navigated;
            //store.Hhh.bProxy = true;
            //store.Hhh.sProxyIP = "127.0.0.1";
            //store.Hhh.sProxyPort = "54110";
            resultRTBox.Text = store.Hhh.DownLoad(commBaseURLTBox.Text + comURL.Text, "G:\\test.pdf").Html + Environment.NewLine;
            resultRTBox.Text += store.Hhh.sCookies;
        }

        private void pdfMergeBtn_Click(object sender, EventArgs e)
        {
            List<String> files = new List<string>();
            files.Add(headerRTBox.Text);
            files.Add(cookieRTBox.Text);
            PDFHelper.MergePDFFiles(files.ToArray(), "E:\\1.PDF");
        }

        private void translateBtn_Click(object sender, EventArgs e)
        {
            GoogleTrans gTrans = new GoogleTrans();
            headerRTBox.Text = gTrans.Translate(cookieRTBox.Text,"th");
        }

        private void productSumBtn_Click(object sender, EventArgs e)
        {
            ShopeeAPI api = new ShopeeAPI();
            ProductStatisticalData sdata=  api.GetProductStatisticalData(store);
            if(null != sdata)
            {
                resultRTBox.Text += "共计产品:+" + sdata.product_count_for_limit + "/" + sdata.count_for_limit;

            }

        }

        ProductPageListInfo productInfos;
        private void queryProductInfoBtn_Click(object sender, EventArgs e)
        {
            ShopeeAPI api = new ShopeeAPI();
            productInfos = api.GetProductPageListInfo(store,1);
            foreach(ProductDetailBaseInfo info in productInfos.list)
            {
                resultRTBox.Text += info.name + Environment.NewLine;
            }
        }

        private void modifyInfoBtn_Click(object sender, EventArgs e)
        {
            //store.Hhh.Authorization = "";
            //store.Hhh.sCookies = cookieRTBox.Text;
            //store.Hhh.Referer = comURL.Text ;
            //HttpResult result = store.Hhh.Post(headerRTBox.Text,dataRTBox.Text);
            //resultRTBox.Text = result.Html;


            ShopeeAPI api = new ShopeeAPI();
            ProductDetailBaseInfo dtail = api.GetProductDetailInfo(store, productInfos.list[0].id);
            dtail.name += "[新]";
            dtail.description = "[新]" + dtail.description;
            api.UpdateProductInfo(store,new ProductDetailBaseInfo[] { dtail });

            //ShopeeAPI api = new ShopeeAPI();

            //resultRTBox.Text = api.UpdateProductInfo(store,headerRTBox.Text,dataRTBox.Text).ToString();
        }

        private void softwareLogBtn_Click(object sender, EventArgs e)
        {
            if(AccessControl.Instance.Login(logUserName.Text,logpassword.Text))
            {
                GroupConfigHelper.Instatce.Initialize(logUserName.Text);
                BrowerHelper.Instatce.Initialize(logUserName.Text);
                GroupConfigHelper.Instatce.StartStoreInfoTask();
                groupCBox.DataSource = GroupConfigHelper.Instatce.Groups;
            }
           
        }

        private void groupCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StoreGroup group = groupCBox.SelectedItem as StoreGroup;
            regionCBox.DataSource = group.Regions;
        }

        private void storeCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            store = storeCBox.SelectedItem as Store;
        }

        private void regionCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StoreRegion region = regionCBox.SelectedItem as StoreRegion;
            storeCBox.DataSource = region.Stores;
        }

        private void likeBtn_Click(object sender, EventArgs e)
        {
            ShopeeAPI aPI = new ShopeeAPI();
            store.Hhh.sCookies = cookieRTBox.Text;
            store.Hhh.Referer = commBaseURLTBox.Text;
            api.LikeProduct(regionCBox.SelectedItem as StoreRegion, store, long.Parse(dataRTBox.Text), long.Parse(headerRTBox.Text));
        }

        private void swLoginTestBtn_Click(object sender, EventArgs e)
        {
            AccessControl.Instance.Login(logUserName.Text,logpassword.Text);
        }
        int i = 8;
        private void createProductBtn_Click(object sender, EventArgs e)
        {
            //List<ProductCreateInfo> products = new List<ProductCreateInfo>();
            //ProductCreateInfo prodInfo = ProductCreateInfo.FromJson(dataRTBox.Text);
            //products.Add(prodInfo);

            List<ProductCreateInfo> products = ProductCreateInfo.FromJson<List<ProductCreateInfo>>(dataRTBox.Text);
           
            ShopeeAPI api = new ShopeeAPI();
            if(null != products)
            {
                api.CreateProducts(store, products.ToArray());
                
            }
            api.CreateProducts(store, (new List<ProductCreateInfo>()).ToArray(), dataRTBox.Text);
        }

        private void uploadImageBtn_Click(object sender, EventArgs e)
        {
            //if(null != headerRTBox.Text)
            //{
            //    ShopeeAPI api = new ShopeeAPI();
            //    cookieRTBox.Text = api.UploadImage(store, headerRTBox.Text);
            //}
        }

        private void collect1688Btn_Click(object sender, EventArgs e)
        {
            HtmlHttpHelper hhh = new HtmlHttpHelper();
            HttpResult result = hhh.Get(headerRTBox.Text,"GBK");
            resultRTBox.Text = result.Html;
        }

        private void bacthChannelBtn_Click(object sender, EventArgs e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.RunWorkerAsync(store);
            this.Enabled = false;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgwork = sender as BackgroundWorker;
            ShopeeAPI api = new ShopeeAPI();
            Store current_store = (Store)e.Argument;

            int page = 1;
            ProductPageListInfo pglist = api.GetProductPageListInfo(current_store, page++);
            int numprod = 0;
            while (pglist.list.Count() > 0)
            {
                Console.WriteLine("\n采集当前第" + pglist.page_info.page_number + "页，共计：" + (pglist.page_info.total / pglist.page_info.page_size + (pglist.page_info.total % pglist.page_info.page_size > 0 ? 1 : 0)) + "页，共" + pglist.page_info.total + "个产品.\n");
                foreach (ProductDetailBaseInfo baseInfo in pglist.list)
                {
                    ProductDetailInfo detailInfo = api.GetProductDetailInfo(current_store, baseInfo.id);
                    if (detailInfo != null)
                    {
                        //解析并插入到表格中//解析文件内容到表格哦中
                        try
                        {
   
                            //重量，运费及运输方式
                            double weight = 0.5;
                            Console.WriteLine("重量，运费及运输方式,默认重量(KG)：" + weight);
                            detailInfo.logistics_channels = api.GetProductLogisticsChannel(current_store, detailInfo.category_path[detailInfo.category_path.Length-1], weight);
                            detailInfo.weight = weight.ToString();
                            api.UpdateProductInfo(current_store, new ProductDetailBaseInfo[] { detailInfo });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("解析有错，忽略继续." + ex.Message.ToString());
                        }
                        //if (numprod % 5 == 0)
                        //{
                        //    bgwork.ReportProgress(numprod % 100);
                        //}
                    }
                    else
                    {
                        Console.WriteLine(baseInfo.name + "的产品详情获取错误！无法打开物流！");
                    }
                }
                //bgwork.ReportProgress(page);
                pglist = api.GetProductPageListInfo(current_store, page++);
            }

            
        }

        private void createPromotionBtn_Click(object sender, EventArgs e)
        {
            string name = dataRTBox.Text;
            api.CreateDiscountItem(store,"测试打折", DateTime.Now, DateTime.Now.AddDays(1));
        }

        private void stopProductDiscountBtn_Click(object sender, EventArgs e)
        {
            //store.Hhh.sCookies = cookieRTBox.Text;
            api.DeleteProductDiscount(store, long.Parse(dataRTBox.Text), long.Parse(headerRTBox.Text));
        }

        private void discountItemBtn_Click(object sender, EventArgs e)
        {
            api.UpdateProductDiscount(store, 1062338886, 7226830545, 80, 9);
        }

        private void sha256Btn_Click(object sender, EventArgs e)
        {
            resultRTBox.Text += HashHelper.GetMD5(dataRTBox.Text)+Environment.NewLine;
            resultRTBox.Text += HashHelper.GetSHA256(dataRTBox.Text) + Environment.NewLine;
            //resultRTBox.Text += HashHelper.GetSHA256( dataRTBox.Text ) + Environment.NewLine;
        }
    }
}
