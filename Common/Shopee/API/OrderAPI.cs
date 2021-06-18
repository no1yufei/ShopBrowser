using Common.Shopee.API.Data;
using CsharpHttpHelper;

using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShopeeChat.Shopee.API
{
    public partial class ShopeeAPI
    {
        public OrderSummaryInfo UpdateSummaryOrderInfo(Store store,string sipRegion ="",long sipShopId=0)
        {
            if (IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_order_meta/?SPC_CDS=d07f1faf-0163-4efa-9051-467e6ec46c8e&SPC_CDS_VER=2
                //https://seller.xiapi.shopee.cn/api/v3/order/get_order_meta/?SPC_CDS=3ee31950-765e-4772-9663-3c99436a67cd&SPC_CDS_VER=2&sip_region=my&sip_shopid=203124285&is_massship=false

                Guid SPC_CDS = store.SPC_CDS; string querURL = store.ServerURL + "/api/v3/order/get_order_meta/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2&is_massship=false";
                if(sipRegion != "" && sipShopId != 0)
                {
                    querURL += "&sip_region=" + sipRegion + "&sip_shopid=" + sipShopId;
                }
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (store.Hhh.bError)
                {
                    store.TotalErrorTime++;
                }else
                {
                    store.TotalErrorTime = 0;
                }
                //Console.WriteLine("更新订单汇总" + querURL);
                if (spcresult.Html != null)
                {
                    MessageWithCode<OrderSummaryInfo> messageData = MessageWithCode<OrderSummaryInfo>.FromJson(spcresult.Html);
                    if(messageData != null )
                    {
                        if(messageData.data != null)
                        {
                            //store.OrderSummaryInfo = messageData.data;
                            Console.WriteLine("SPC 订单汇总成功!");
                            return messageData.data;
                        }
                        else if (messageData.code == 2)
                        {
                            ///token 失效，重新登录
                            store.Token = "";
                        }
                    }
                }
                Console.WriteLine("订单汇总失败:" + spcresult.Html);
            }
            return null;
        }
        public bool UpdateOrderInfo(Store store)
        {
            //if (store.Token != null && store.Token != string.Empty && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            //{
            //    //https://seller.xiapi.shopee.cn/api/v2/orders/?is_massship=false&limit=40&offset=0&type=toship&SPC_CDS=1a53c935-eb11-4196-9ad3-58be21b10889&SPC_CDS_VER=2

            //    Guid SPC_CDS = store.SPC_CDS; string querURL = store.ServerURL + "/api/v2/orders/?is_massship=false&limit=40&offset=0&type=toship&SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
            //    if (!store.Hhh.sCookies.Contains("SPC_CDS"))
            //    {
            //        store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
            //    }
            //    HttpResult spcresult = store.Hhh.Get(querURL);
            //    //Console.WriteLine("更新订单信息" + querURL);
            //    if (spcresult.Html != null && spcresult.Html.Contains("toship"))
            //    {
            //        store.OrderInfos = OrderInfos.FromJson(spcresult.Html);
            //        Console.WriteLine("更新订单信息!");
            //        return true;
            //    }
            //    Console.WriteLine("更新订单信息:" + spcresult.Html);
            //}
            return false;
        }
        public OrderIds[] GetSimpleOrderIds(Store store, int page = 0, string shipstatus = "", int limit = 40)
        {
            if (store.Token != null && store.Token != string.Empty)
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_simple_order_ids/?SPC_CDS=c2356822-1378-4ddc-8497-90b9f0dd54cc&SPC_CDS_VER=2&page_size=40&page_number=1&source=unpaid&total=0&flip_direction=ahead&page_sentinel=0,0&sort_by=create_date_desc&backend_offset=
                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v3/order/get_simple_order_ids/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                //querURL += "&limit=" + limit + "&offset=" + page * limit + "&order_by_create_date=desc&is_massship=false";
                querURL += "&page_size="+ limit + "&page_number="+ page + "&source="+ shipstatus + "&total=0&flip_direction=ahead&page_sentinel=0,0&sort_by=create_date_desc&backend_offset=";
                
                //if (listtype != "")
                //{
                //    querURL += "&list_type=" + listtype;
                //}
                //if (shipstatus != "")
                //{
                //    querURL += "&shipping_center_status=" + listtype;
                //}
                //    ;
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                //Console.WriteLine("更新订单信息" + querURL);
                if (spcresult.Html != null)
                {
                    MessageWithCode<ShopOrderIds> orderlistInofs = MessageWithCode<ShopOrderIds>.FromJson(spcresult.Html);
                    if (null != orderlistInofs && orderlistInofs.data != null && orderlistInofs.data.orders != null && orderlistInofs.data.orders.Length > 0)
                    {
                        //if(orderlistInofs.data.meta.total > orderlistInofs.data.meta.limit * offset)
                        {
                            Console.WriteLine("获取订单编号成功!");
                            return orderlistInofs.data.orders;
                        }
                    }
                    else
                    {
                        Console.WriteLine("获取订单编号失败:" + spcresult.Html);
                    }

                }
                Console.WriteLine("获取订单编号失败:" + spcresult.Html);
            }
            return null;
        }


        public OrderInfoV3[] GetOrderListInfo(Store store,int page = 0,string listtype = "",string shipstatus="",int limit=60)
        {
            if (store.Token != null && store.Token != string.Empty)
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_order_list/?SPC_CDS=d07f1faf-0163-4efa-9051-467e6ec46c8e&SPC_CDS_VER=2&limit=40&offset=0&list_type=toship&order_by_create_date=desc&is_massship=false

                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v3/order/get_order_list/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                querURL += "&limit="+ limit + "&offset="+ page* limit + "&order_by_create_date=desc&is_massship=false";
                if(listtype!="")
                {
                    querURL += "&list_type=" + listtype;
                }
                if (shipstatus != "")
                {
                    querURL += "&shipping_center_status=" + listtype;
                }
                    ;
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                //Console.WriteLine("更新订单信息" + querURL);
                if (spcresult.Html != null )
                {
                    MessageWithCode<OrderListInfo> orderlistInofs  = MessageWithCode<OrderListInfo>.FromJson(spcresult.Html);
                    if(null != orderlistInofs && orderlistInofs.data != null && orderlistInofs.data.orders != null && orderlistInofs.data.orders.Length > 0)
                    {
                        //if(orderlistInofs.data.meta.total > orderlistInofs.data.meta.limit * offset)
                        {
                            Console.WriteLine("获取订单信息成功!");
                            return orderlistInofs.data.orders;
                        }
                    }
                    else
                    {
                        Console.WriteLine("获取订单信息失败:" + spcresult.Html);
                    }
                   
                }
                Console.WriteLine("获取订单信息失败:" + spcresult.Html);
            }
            return null;
        }
        public int GetOrderTotalNumber(Store store)
        {
            if (store.Token != null && store.Token != string.Empty)
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_order_list/?SPC_CDS=d07f1faf-0163-4efa-9051-467e6ec46c8e&SPC_CDS_VER=2&limit=40&offset=0&list_type=toship&order_by_create_date=desc&is_massship=false

                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v3/order/get_order_list/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                querURL += "&limit=1&offset= 0 &order_by_create_date=desc&is_massship=false";
                ;
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                //Console.WriteLine("更新订单信息" + querURL);
                if (spcresult.Html != null)
                {
                    MessageWithCode<OrderListInfo> orderlistInofs = MessageWithCode<OrderListInfo>.FromJson(spcresult.Html);
                    if (null != orderlistInofs && orderlistInofs.data != null && orderlistInofs.data.orders != null && orderlistInofs.data.orders.Length > 0)
                    {
                        //if(orderlistInofs.data.meta.total > orderlistInofs.data.meta.limit * offset)
                        {
                            Console.WriteLine("获取订单信息成功!");
                            return orderlistInofs.data.meta.total;
                        }
                    }

                }
                Console.WriteLine("获取订单信息失败:" + spcresult.Html);
            }
            return 0;
        }
       
        public OrderInfoV3 GetOneOrderDetail(Store store, String orderid)
        {
            OrderInfoV3 orderInfo = null;
            if (IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_one_order?SPC_CDS=c5214926-42a3-4a08-82a8-b61df79bc8a8&SPC_CDS_VER=2&order_id=1975263324

                Guid SPC_CDS = store.SPC_CDS; string querURL = store.ServerURL + "/api/v3/order/get_one_order?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2&order_id="+orderid;
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("toship"))
                {
                    MessageWithCode<OrderInfoV3> orderMessge = MessageWithCode<OrderInfoV3>.FromJson(spcresult.Html);
                    if(orderMessge != null && orderMessge.code == 0)
                    {
                        orderInfo = orderMessge.data;
                    }
                    Console.WriteLine("获取订单信息" + store.DisplayName + "\t" + orderid);
                }
            }
            return orderInfo;
        }
        
        public ShopOrderLogistic[] GetShopOrderLogistics(Store store,String orderid)
        {
            ShopOrderLogistic[] orderLogistics = null;
            //https://seller.xiapi.shopee.cn/api/v2/tracking/logisticsHistories/1681689469/?SPC_CDS=b17023c5-a022-42c5-8248-b37be0ac6326&SPC_CDS_VER=2
            //https://seller.xiapi.shopee.cn/api/v3/logistics/get_logistics_tracking_history/?SPC_CDS=e472bab7-be77-452e-85e0-8873e6d98ca1&SPC_CDS_VER=2&order_id=2171891225
            //https://seller.xiapi.shopee.cn/api/v3/logistics/get_logistics_tracking_history/?SPC_CDS=c2356822-1378-4ddc-8497-90b9f0dd54cc&SPC_CDS_VER=2&order_id=44615071387781
            if (IsLogin(store))
            {
                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v3/logistics/get_logistics_tracking_history/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2&order_id="+ orderid;
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null)
                {
                    DataListMessageWithCode<ShopOrderLogistic> messagedata = DataListMessageWithCode<ShopOrderLogistic>.FromJson(spcresult.Html);
                    if(null != messagedata && messagedata.data != null && messagedata.data.list != null && messagedata.data.list.Length > 0)
                    {
                        orderLogistics = messagedata.data.list;
                        Console.WriteLine("获取订单物流" + store.DisplayName + "\t" + orderid + "\t" + "成功！");
                    }
                    else
                    {
                        Console.WriteLine("获取订单物流" + store.DisplayName + "\t" + orderid + "\t" + "失败！"+ spcresult.Html);
                    }
                    
                }
            }
            return orderLogistics;
        }
        public ShopOrderTransaction GetShopOrderTransaction(Store store,String orderid)
        {
            ShopOrderTransaction orderTransaction = null;
            //https://seller.xiapi.shopee.cn/api/v1/transactionDetails/1665137475/?SPC_CDS=b17023c5-a022-42c5-8248-b37be0ac6326&SPC_CDS_VER=2
            //https://seller.xiapi.shopee.cn/api/v3/finance/income_transaction_history_detail/?order_id=2171891225&SPC_CDS=e472bab7-be77-452e-85e0-8873e6d98ca1&SPC_CDS_VER=2
            if (store.Token != null && store.Token != string.Empty)
            {
                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v3/finance/income_transaction_history_detail/?order_id=" + orderid + "&SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("order_logs"))
                {
                    orderTransaction = ShopOrderTransaction.FromJson(spcresult.Html);
                    Console.WriteLine("获取订单进账" + store.DisplayName + "\t" + orderid);
                }
            }
            return orderTransaction;
        }
        public bool GenerateTraceNo(Store store, long  orderid,long channelid, string sipRegion = "", long sipShopId = 0)
        {
            if (IsLogin(store) && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            {
                //https://seller.id.shopee.cn/api/v3/shipment/init_order/?SPC_CDS=3bcbef62-cef2-438e-a65b-8f916eaa31ea&SPC_CDS_VER=2
                //https://seller.xiapi.shopee.cn/api/v3/shipment/init_order/?SPC_CDS=3946910a-6dec-4880-a538-8cb8057fc3a9&SPC_CDS_VER=2&sip_region=id&sip_shopid=191037456
                String data = "{\"channel_id\":"+ channelid + ",\"order_id\":"+ orderid + "}";
                Guid SPC_CDS = store.SPC_CDS; string querURL = store.ServerURL + "/api/v3/shipment/init_order/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                if (sipRegion != "" && sipShopId != 0)
                {
                    querURL += "&sip_region=" + sipRegion + "&sip_shopid=" + sipShopId;
                };
                HttpResult spcresult = store.Hhh.Post(querURL, data);
                //Console.WriteLine("申请跟踪号：" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    //store.OrderInfos = OrderInfos.FromJson(spcresult.Html);
                    Console.WriteLine("申请跟踪号成功!");
                    return true;
                }
                Console.WriteLine("申请跟踪号:" + spcresult.Html);
            }
            return false;
        }
        public bool GenerateTraceNoV2(Store store, String orderid)
        {
            if (store.Token != null && store.Token != string.Empty && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            {

                //https://seller.xiapi.shopee.cn/api/v2/orders/dropoffs/1411621647/?SPC_CDS=58b7eefa-fa8b-443d-8fcf-80a922ee51a2&SPC_CDS_VER=2
                String data = "{\"orderLogistic\":{\"userid\":0,\"orderid\":null,\"type\":0,\"status\":0,\"channelid\":0,\"channel_status\":\"\",\"consignment_no\":\"\",\"booking_no\":\"\",\"pickup_time\":0,\"actual_pickup_time\":0,\"deliver_time\":0,\"actual_deliver_time\":0,\"ctime\":0,\"mtime\":0,\"seller_realname\":\"\",\"branchid\":0,\"slug\":\"\",\"shipping_carrier\":\"\",\"logistic_command\":\"generate_tracking_no\",\"extra_data\":\"{ }\"}}";
                Guid SPC_CDS = store.SPC_CDS; string querURL = store.ServerURL + "/api/v2/orders/dropoffs/" + orderid + "/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Put(querURL, data);
                //Console.WriteLine("申请跟踪号：" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("{}"))
                {
                    //store.OrderInfos = OrderInfos.FromJson(spcresult.Html);
                    Console.WriteLine("申请跟踪号成功!");
                    return true;
                }
                Console.WriteLine("申请跟踪号:" + spcresult.Html);
            }
            return false;
        }
        public string DownloadPostLable(Store store,string orderId,string ordersn)
        {
            
            string basePath = Environment.CurrentDirectory + "\\aliyunOss\\";
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            if (store.Token != null && store.Token != string.Empty && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            {
                Guid SPC_CDS = store.SPC_CDS;
                string querURL = "";
                bool downloadSuccess = false;
                string labeFile = basePath + ordersn + ".pdf";

                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }

                ShopOrderLogisticslogsStatus logtistic = GetForderLogistics(store, long.Parse(orderId));
                if (null != logtistic)
                {
                    string forderId = logtistic.forder_id;
                    querURL = store.ServerURL + "/api/v3/logistics/get_waybill_new/?order_ids=" + orderId + "&order_forder_map={\"" + orderId + "\":{ \"forder_ids\":[\"" + forderId + "\"]}}";
                    if (downloadFile(store, querURL, labeFile))
                    {
                        downloadSuccess = true;
                        return labeFile;
                    }
                }

                if (!downloadSuccess)
                {
                    querURL = store.ServerURL + "/api/v2/orders/waybill/?orderids=[" + orderId + "]&language=tw&api_from=waybill";
                    if (downloadFile(store, querURL, labeFile))
                    {
                        //downloadSuccess = true;
                        return labeFile;
                    }

                }
            }
            return "";
        }

        public List<String> DownloadPostLable(Store store, List<String> orderids,SIPStoreInfo sipStore = null)
        {
            List<String> oderPdfList = new List<string>();
            if (IsLogin(store))
            {
                Guid id = Guid.NewGuid();
                string tempDic = id.ToString("N") + "\\";
                string basePath = Environment.CurrentDirectory + "\\Lable\\";
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                if (!Directory.Exists(basePath + tempDic))
                {
                    Directory.CreateDirectory(basePath + tempDic);
                }

                string sipRegion = "";
                long sipShopId = 0;

                if(null != sipStore)
                {
                    sipRegion = sipStore.country;
                    sipShopId = sipStore.shopid;
                }
                foreach (String orderid in orderids)
                {
                https://seller.xiapi.shopee.cn/api/v3/logistics/get_waybill_new/?order_ids=40756457431224&order_forder_map=%7B%2240756457431224%22%3A%7B%22forder_ids%22%3A%5B%222329934975721472456%22%5D%7D%7D&sip_shopid=191037456&sip_region=id

                    //https://seller.my.shopee.cn/api/v3/logistics/get_waybill_new/?order_ids=1966266304&order_forder_map={"1966266304":{"forder_ids":["1966266304"]}}
                    //https://seller.xiapi.shopee.cn/api/v2/orders/waybill/?orderids=[1411621647]&language=tw&api_from=waybill
                    Guid SPC_CDS = store.SPC_CDS;
                    string querURL = "";
                    bool downloadSuccess = false;
                    string labeFile = basePath + tempDic + orderid + ".pdf";

                    if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                    {
                        store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                    }
                    
                    ShopOrderLogisticslogsStatus logtistic = GetForderLogistics(store, long.Parse(orderid), sipRegion, sipShopId);
                    if (null != logtistic)
                    {
                        string forderId = logtistic.forder_id;
                        querURL = store.ServerURL + "/api/v3/logistics/get_waybill_new/?order_ids=" + orderid + "&order_forder_map={\"" + orderid + "\":{ \"forder_ids\":[\"" + forderId + "\"]}}";
                        if (sipRegion != "" && sipShopId != 0)
                        {
                            querURL += "&sip_region=" + sipRegion + "&sip_shopid=" + sipShopId;
                        };

                        if (downloadFile(store, querURL, labeFile))
                        {
                            oderPdfList.Add(labeFile);
                            downloadSuccess = true;
                        }
                    }

                    if (!downloadSuccess)
                    {
                        querURL = store.ServerURL + "/api/v2/orders/waybill/?orderids=[" + orderid + "]&language=tw&api_from=waybill";
                        if (downloadFile(store, querURL, labeFile))
                        {
                            oderPdfList.Add(labeFile);
                            downloadSuccess = true;
                        }

                    }
                }
            }
            return oderPdfList;
        }
        private bool downloadFile(Store store, string querURL, string labeFile)
        {
            HttpResult spcresult = store.Hhh.DownLoad(querURL, labeFile);
            Console.WriteLine("下载面单：" + querURL);

            System.IO.FileInfo fileInfo = null;
            try
            {
                fileInfo = new System.IO.FileInfo(labeFile);
                if (fileInfo.Exists && fileInfo.Length > 200)
                {
                    Console.WriteLine("下载面单成功!");
                    return true;
                }
                Console.WriteLine("下载面单失败:" + spcresult.Html);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // 其他处理异常的代码
            }
            return false;
        }
        public bool UpdateOrderNote(Store store, String orderid,string note)
        {
            if (store.Token != null && store.Token != string.Empty && store.OrderSummaryInfo != null && store.OrderSummaryInfo.toship > 0)
            {

                //https://seller.xiapi.shopee.cn/api/v3/order/update_note/?SPC_CDS=61c77997-47c9-456e-a625-3889c808ae66&SPC_CDS_VER=2
                String data = "{\"order_id\":"+ orderid + ", \"new_note\": \""+ note + "\"}";
                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v3/order/update_note/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                store.Hhh.Referer = store.ServerURL +"/portal/sale/" + orderid;
                HttpResult spcresult = store.Hhh.Post(querURL, data);
                store.Hhh.Referer = "";
                //Console.WriteLine("申请跟踪号：" + querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("success"))
                {
                    //store.OrderInfos = OrderInfos.FromJson(spcresult.Html);
                    Console.WriteLine("更新备注成功!");
                    return true;
                }
                Console.WriteLine("更新备注失败:" + spcresult.Html);
            }
            return false;
        }

        public ShopOrderLogisticslogsStatus GetForderLogistics(Store store, long orderid, string sipRegion = "", long sipShopId = 0)
        {
            ShopOrderLogisticslogsStatus logisticsInfo = null;
            if (IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_forder_logistics/?order_id=2159791550&SPC_CDS=3ade9aef-ae31-4fc8-b636-4979d572d0d4&SPC_CDS_VER=2
                //https://seller.xiapi.shopee.cn/api/v3/order/get_forder_logistics/?order_id=40756457431224&SPC_CDS=3946910a-6dec-4880-a538-8cb8057fc3a9&SPC_CDS_VER=2&sip_region=id&sip_shopid=191037456
                Guid SPC_CDS = store.SPC_CDS; string querURL = store.ServerURL + "/api/v3/order/get_forder_logistics/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2&order_id=" + orderid;
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                if (sipRegion != "" && sipShopId != 0)
                {
                    querURL += "&sip_region=" + sipRegion + "&sip_shopid=" + sipShopId;
                };
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null)
                {
                    DataListMessageWithCode<ShopOrderLogisticslogsStatus> logisticsMessge = DataListMessageWithCode<ShopOrderLogisticslogsStatus>.FromJson(spcresult.Html);
                    if (logisticsMessge != null && logisticsMessge.code == 0 && logisticsMessge.data.list != null && logisticsMessge.data.list.Length > 0)
                    {
                        logisticsInfo = logisticsMessge.data.list[0];
                    }
                    Console.WriteLine("获取物流状态" + store.DisplayName + "\t" + orderid);
                }
            }
            return logisticsInfo;
        }

        public ReturnDetail GetReturnDetail(Store store,long return_id)
        {
            if (IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_return_detail/?SPC_CDS=945835a5-5ffb-4618-ae12-74d95baae822&SPC_CDS_VER=2&return_id=19265637
                string querURL = store.ServerURL + "/api/v3/order/get_return_detail/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&return_id=" + return_id;
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null&& spcresult.Html.Contains("dispute_text_reason"))
                {
                    ReturnDetail rd = ReturnDetail.FromJson(spcresult.Html);
                    Console.WriteLine("获取退款详情" + store.DisplayName + "\t" + return_id);
                    return rd;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取退款退货订单列表
        /// </summary>
        /// <param name="store">店铺要登录状态，否则返回空</param>
        /// <param name="pageSize">最大99</param>
        /// <param name="pageNumer">最小1</param>
        /// <param name="refundStatus">空,refund_unprocessed,refund_processed</param>
        public ReturnList GetReturnList(Store store,int pageNumer = 1, string refundStatus = "", int pageSize = 99)
        {
            if (IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_return_list/?SPC_CDS=945835a5-5ffb-4618-ae12-74d95baae822&SPC_CDS_VER=2&page_size=40&page_number=1&refund_status=
                string querURL = store.ServerURL + "/api/v3/order/get_return_list/?SPC_CDS=" + store.SPC_CDS.ToString() + "&SPC_CDS_VER=2&page_size=" + pageSize + "&page_number="+ pageNumer + "&refund_status="+ refundStatus;
                HttpResult spcresult = store.Hhh.Get(querURL);
                if (spcresult.Html != null && spcresult.Html.Contains("return_sn"))
                {
                    ReturnList rl = ReturnList.FromJson(spcresult.Html);
                    Console.WriteLine("获取退款列表" + store.DisplayName);
                    return rl;
                }
            }
            return null;
        }
        public OrderIdList GetOrderIDList_Old(Store store, int pageNumber = 1, int pageSize = 40, string shipstatus = "toship",string sipRegion ="",long sipShopId = 0)
        {
            if (IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_order_ids/?SPC_CDS=3ee31950-765e-4772-9663-3c99436a67cd&SPC_CDS_VER=2&page_size=40&page_number=1&source=toship&total=0&flip_direction=ahead&page_sentinel=0,0&sort_by=ship_by_date_asc
                //https://seller.xiapi.shopee.cn/api/v3/order/get_order_ids/?SPC_CDS=3946910a-6dec-4880-a538-8cb8057fc3a9&SPC_CDS_VER=2&page_size=40&page_number=1&source=toship&total=0&flip_direction=ahead&page_sentinel=0,0&sort_by=ship_by_date_asc&sip_shopid=191037456&sip_region=id
                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v3/order/get_order_ids/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                querURL += "&page_size=" + pageSize + "&page_number=" + pageNumber + "&total=" + (pageNumber - 1) * pageSize + "&flip_direction=ahead&page_sentinel=0,0&sort_by=ship_by_date_asc";

                if (shipstatus != "")
                {
                    querURL += "&source=" + shipstatus;
                }
                if (sipRegion != "" && sipShopId != 0)
                {
                    querURL += "&sip_region=" + sipRegion + "&sip_shopid=" + sipShopId;
                };
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                //Console.WriteLine("更新订单信息" + querURL);
                if (spcresult.Html != null)
                {
                    MessageWithCode<OrderIdList> orderlistInofs = MessageWithCode<OrderIdList>.FromJson(spcresult.Html);
                    if (null != orderlistInofs && orderlistInofs.data != null && orderlistInofs.data.order_ids != null && orderlistInofs.data.order_ids.Length > 0)
                    {
                        //if(orderlistInofs.data.meta.total > orderlistInofs.data.meta.limit * offset)
                        {
                            Console.WriteLine("获取订单ID列表信息成功!");
                            return orderlistInofs.data;
                        }
                    }
                    else
                    {
                        Console.WriteLine("获取订单ID列表信息失败:" + spcresult.Html);
                    }

                }
                Console.WriteLine("获取订单ID列表信息失败:" + spcresult.Html);
            }
            return null;
        }
        public OrderIdList GetOrderIDList(Store store, int pageNumber = 1, int pageSize = 40, string shipstatus = "toship", string sipRegion = "", long sipShopId = 0)
        {
            if (IsLogin(store))
            {
                ForderList flist = GetForderList(store, pageNumber, pageSize, sipRegion, sipShopId);
                    if (null != flist && flist.forders != null)
                    {
                    OrderIdList olist = new OrderIdList();
                    olist.page_number = flist.page_number;
                    olist.page_size = flist.page_size;
                    olist.total = flist.total;
                    olist.order_ids = new long[flist.forders.Length];
                    for(int i = 0;i < olist.order_ids.Length;i++)
                    {
                        olist.order_ids[i] = flist.forders[i].order_id;
                    }
                   
                            Console.WriteLine("获取订单ID列表信息成功!");
                            return olist;
                        }
                    
                
                Console.WriteLine("获取订单ID列表信息失败" );
            }
            return null;
        }
        public ForderList GetForderList(Store store, int pageNumber = 1, int pageSize = 40, string sipRegion = "", long sipShopId = 0)
        {
            if (IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_forder_list/?SPC_CDS=b52248f0-8185-4e00-afae-c4362e4f45c7&SPC_CDS_VER=2&page_size=40&page_number=1&total=0&sort_by=ship_by_date_asc&sip_region_for_fulfillment=id&sip_shop_id_for_fulfillment=191037456
                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v3/order/get_forder_list/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                querURL += "&page_size=" + pageSize + "&page_number=" + pageNumber + "&total=" + (pageNumber - 1) * pageSize + "&sort_by=ship_by_date_asc";

                
                if (sipRegion != "" && sipShopId != 0)
                {
                    querURL += "&sip_region_for_fulfillment=" + sipRegion + "&sip_shop_id_for_fulfillment=" + sipShopId;
                };
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                //Console.WriteLine("更新订单信息" + querURL);
                if (spcresult.Html != null)
                {
                    MessageWithCode<ForderList> forderlistInofs = MessageWithCode<ForderList>.FromJson(spcresult.Html);
                    if (null != forderlistInofs && forderlistInofs.data != null && forderlistInofs.data.forders != null && forderlistInofs.data.forders.Length > 0)
                    {
                        //if(orderlistInofs.data.meta.total > orderlistInofs.data.meta.limit * offset)
                        {
                            Console.WriteLine("获取Forder订单ID列表信息成功!");
                            return forderlistInofs.data;
                        }
                    }
                    else
                    {
                        Console.WriteLine("获取Forder订单ID列表信息失败:" + spcresult.Html);
                    }

                }
                Console.WriteLine("获取Forder订单ID列表信息失败:" + spcresult.Html);
            }
            return null;
        }
        public OrderInfoV3[] PostOrderListByIDs(Store store, string postData)
        {
            if (IsLogin(store))
            {
                //https://seller.ph.shopee.cn/api/v3/order/get_order_list_by_order_ids_multi_shop/?SPC_CDS=6c8c4e7c-c26e-4c49-a215-01cd7d39e94c&SPC_CDS_VER=2
                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v3/order/get_order_list_by_order_ids_multi_shop/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2";
                
                HttpResult spcresult = store.Hhh.Post(querURL, postData);
                //HttpResult spcresult = store.Hhh.Get(querURL);
                //Console.WriteLine("更新订单信息" + querURL);
                if (spcresult.Html != null)
                {
                    MessageWithCode<Orders> orderlistInofs = MessageWithCode<Orders>.FromJson(spcresult.Html);
                    if (null != orderlistInofs && orderlistInofs.data != null && orderlistInofs.data.orders != null && orderlistInofs.data.orders.Length > 0)
                    {
                        //if(orderlistInofs.data.meta.total > orderlistInofs.data.meta.limit * offset)
                        {
                            Console.WriteLine("获取订单列表信息成功!");
                            return orderlistInofs.data.orders;
                        }
                    }
                    else
                    {
                        Console.WriteLine("获取订单列表信息失败:" + spcresult.Html);
                    }

                }
                Console.WriteLine("获取订单列表信息失败:" + spcresult.Html);
            }
            return null;


        }
        public OrderInfoV3[] GetOrderListByIDs(Store store, long[] orderids,string sipRegion ="",long sipShopId = 0)
        {
            if (IsLogin(store))
            {
                //https://seller.xiapi.shopee.cn/api/v3/order/get_order_list_by_order_ids/?SPC_CDS=3ee31950-765e-4772-9663-3c99436a67cd&SPC_CDS_VER=2
                //&order_ids=40556651896412,40567870946712,40721980886046,40736418419221&from_seller_data=true&source=toship
                //https://seller.xiapi.shopee.cn/api/v3/order/get_order_list_by_order_ids/?SPC_CDS=3946910a-6dec-4880-a538-8cb8057fc3a9&SPC_CDS_VER=2
                //&sip_region=id&sip_shopid=191037456&order_ids=40756457431224&from_seller_data=true&source=toship
                Guid SPC_CDS = store.SPC_CDS;
                string querURL = store.ServerURL + "/api/v3/order/get_order_list_by_order_ids/?SPC_CDS=" + SPC_CDS.ToString() + "&SPC_CDS_VER=2&from_seller_data=true";

                if (orderids != null)
                {
                    querURL += "&order_ids=";
                    foreach (long id in orderids)
                    {
                        querURL += id +",";
                    }
                    querURL.TrimEnd(',');
                }                if (sipRegion != "" && sipShopId != 0)
                {
                    querURL += "&sip_region=" + sipRegion + "&sip_shopid=" + sipShopId;
                }; ;
                if (!store.Hhh.sCookies.Contains("SPC_CDS"))
                {
                    store.Hhh.sCookies += "SPC_CDS=" + SPC_CDS.ToString() + ";";
                }
                HttpResult spcresult = store.Hhh.Get(querURL);
                //Console.WriteLine("更新订单信息" + querURL);
                if (spcresult.Html != null)
                {
                    MessageWithCode<Orders> orderlistInofs = MessageWithCode<Orders>.FromJson(spcresult.Html);
                    if (null != orderlistInofs && orderlistInofs.data != null && orderlistInofs.data.orders != null && orderlistInofs.data.orders.Length > 0)
                    {
                        //if(orderlistInofs.data.meta.total > orderlistInofs.data.meta.limit * offset)
                        {
                            Console.WriteLine("获取订单列表信息成功!");
                            return orderlistInofs.data.orders;
                        }
                    }
                    else
                    {
                        Console.WriteLine("获取订单列表信息失败:" + spcresult.Html);
                    }

                }
                Console.WriteLine("获取订单列表信息失败:" + spcresult.Html);
            }
            return null;
        }

    }
}
