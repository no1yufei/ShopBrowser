using Common.Shopee.API.Data;
using ShopeeChat.Shopee;
using ShopeeChat.Shopee.API;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace ShopeeChat.SysData
{
    public class GroupConfigHelper
    {


        public List<StoreGroup> Groups = null;
       
        string basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string configFileName = "\\win32.dll";

        static private GroupConfigHelper instance;
        static public GroupConfigHelper Instatce
        {
            get
            {
                if (null == instance)
                {
                    instance = new GroupConfigHelper();
                }
                return instance;
            }
        }
      
        public void Initialize(string username)
        {
            
            {
                loadMerchantAccountInfoAPI();
                //loadMerchantAccountInfoDB(username, password);
            }
        }

        public void Save()
        {
            saveMerchantAccountInfoAPI();
        }
        public void ClearStoreData()
        {
            foreach (StoreGroup group in Groups)
            {
                foreach (StoreRegion region in group.Regions)
                {
                    region.Stores.Clear();
                }
            }
        }
        public StoreGroup GetStoreGroup(Store storeINfo)
        {
            foreach (StoreGroup group in Groups)
            {
                foreach (StoreRegion region in group.Regions)
                {
                    foreach (Store store in region.Stores)
                    {
                        if (store.UserName == storeINfo.UserName)
                        {
                            return group;
                        }
                    }
                }
            }
            return null;
        }
        public StoreRegion GetStoreRegion(Store storeINfo)
        {
            foreach (StoreGroup group in Groups)
            {
                foreach (StoreRegion region in group.Regions)
                {
                    foreach (Store store in region.Stores)
                    {
                        if (store.UserName == storeINfo.UserName)
                        {
                            return region;
                        }
                    }
                }
            }
            return null;
        }
        public StoreGroup GetShopGroupByName(string groupName)
        {
            return Groups.FirstOrDefault(p => p.GroupName == groupName);
        }
        public int GetGroupCount()
        {
            return Groups.Count;
        }
        public int GetStoreCount()
        {
            int count = 0;
            foreach (StoreGroup group in Groups)
            {
                foreach (StoreRegion region in group.Regions)
                {
                    count += region.Stores.Count;
                }
            }
            return count;
        }

        //private List<StoreRegion> loadMerchantAccountInfoLocal()
        //{
        //    List<StoreRegion> merchantAccountInfos = new List<StoreRegion>();
        //    try
        //    {
        //        string filePath = basePath + configFileName;
        //        if (!System.IO.File.Exists(filePath))
        //        {
        //            throw new ArgumentNullException(filePath + " not Exists");
        //        }
        //        using (System.IO.StreamReader reader = new System.IO.StreamReader(filePath))
        //        {
        //            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(List<StoreRegion>));
        //            merchantAccountInfos = (List<StoreRegion>)xs.Deserialize(reader);
        //        }
        //        Console.WriteLine("配置文件已经载入：" + filePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        Groups = new List<StoreGroup>();
        //        StoreGroup group = new StoreGroup();
        //        group.Regions = StoreRegionMap.GetRegionList();
        //        Groups.Add(group);
        //        //saveMerchantAccountInfoDB();
        //        Console.WriteLine("配置文件载入错误:" + ex.Message);
        //    }
        //    return merchantAccountInfos;
        //}
        //private void saveMerchantAccountInfoLocal(List<StoreRegion> merchantAccountInfos)
        //{
        //    try
        //    {
        //        string filePath = basePath + configFileName;

        //        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(File.Open(filePath, FileMode.OpenOrCreate)))
        //        {
        //            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(List<StoreRegion>));
        //            xs.Serialize(writer, merchantAccountInfos);
        //        }
        //        Console.WriteLine("配置文件已经写入：" + filePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private List<StoreGroup> loadMerchantAccountInfoAPI()
        {
            try
            {
                string decodeConfStr = AccessControl.Instance.GetGroupConfigStr();
                if ("" == decodeConfStr)
                {
                    throw new Exception("还没有绑定虾皮账户，请登录后，绑定您的虾皮账号！");
                }
                decodeConfStr = decodeConfStr.Substring(decodeConfStr.IndexOf("\n"));
                using (StringReader sr = new StringReader(decodeConfStr))
                {
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(List<StoreGroup>));

                    try
                    {
                        Groups = (List<StoreGroup>)xs.Deserialize(sr);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show("配置文件载入错误:编码有误" + ex.Message);

                        using (StringReader sr1 = new StringReader(decodeConfStr))
                        {
                            Groups = (List<StoreGroup>)xs.Deserialize(sr1);
                            saveMerchantAccountInfoAPI();
                        }
                    }
                }
                Console.WriteLine("配置文件已经载入：API");
            }
            catch (Exception ex)
            {
                Groups = new List<StoreGroup>();
                StoreGroup group = new StoreGroup();
                group.Regions = StoreRegionMap.GetRegionList();
                Groups.Add(group);
                Console.WriteLine("配置文件载入错误:" + ex.Message);
                MessageBox.Show("非VIP用户更换电脑登录后，请重新绑定店铺数据：" + ex.Message, "配置文件载入错误");
            }
            return Groups;
        }

        private void saveMerchantAccountInfoAPI()
        {
            try
            {
                using (StringWriter writer = new StringWriter())
                {
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(List<StoreGroup>));
                    xs.Serialize(writer, Groups);
                    string str = writer.ToString();
                    AccessControl.Instance.UpdateGroupConfig(str);
                }
                Console.WriteLine("保存配置API:店铺数量" + GroupConfigHelper.instance.GetGroupCount());
            }
            catch (Exception ex)
            {
                Console.WriteLine("保存配置API错误:" + ex.Message);
                MessageBox.Show(ex.Message, "保存配置API错误");
            }
        }

        public delegate void notifyStoreInfoUpdaeDele(StoreGroup group);
        private notifyStoreInfoUpdaeDele storeinfoUpdateHandler;
        bool bStoreInfoTask = false;
        Thread messageThread = null;
        Thread infoupdateThread = null;
        public void RegistStoreInfoUpdateHanlder(notifyStoreInfoUpdaeDele handler)
        {
            storeinfoUpdateHandler = handler;
        }
        public void StartStoreInfoTask()
        {
            StopStoreInfoTask();
            bStoreInfoTask = true;
            messageThread = new Thread(unReadMessageTask);
            infoupdateThread = new Thread(storeInfoUpdateTask);
            messageThread.Start();
            infoupdateThread.Start();
        }
        public void StopStoreInfoTask()
        {
            bStoreInfoTask = false;
            if (null != messageThread)
            {
                messageThread.Abort();
                messageThread = null;
            }
            if (null != infoupdateThread)
            {
                infoupdateThread.Abort();
                infoupdateThread = null;
            }
        }
        private void unReadMessageTask(object state)
        {
            ShopeeAPI api = new ShopeeAPI();
            while (bStoreInfoTask)
            {
                DateTime startTime = DateTime.Now;
                try
                {
                    DateTime updateTime = DateTime.Now;
                    StoreGroup group = GroupConfigHelper.Instatce.Groups.FirstOrDefault(p => p.MessageUpdateTime != updateTime);
                    while (null != group)
                    {
                        group.MessageUpdateTime = updateTime;
                        if (group.Plateform != 1)
                        {
                            group = GroupConfigHelper.Instatce.Groups.FirstOrDefault(p => p.MessageUpdateTime != updateTime);
                            Thread.Sleep(2000);
                            continue;
                        }
                       
                        bool isNewMesage = false;
                        bool isUpdate = false;
                        StoreRegion region = group.Regions.FirstOrDefault(p => p.MessageUpdateTime != updateTime);
                        while (null != region)
                        {
                            region.MessageUpdateTime = updateTime;
                            if (region.Stores.Count <= 0 || region.RegionID == "gl")
                            {
                                region = group.Regions.FirstOrDefault(p => p.MessageUpdateTime != updateTime);
                                continue;
                            }
                            Store storeInfo = region.Stores.FirstOrDefault(p => p.MessageUpdateTime != updateTime);
                            while (null != storeInfo)
                            {
                                storeInfo.MessageUpdateTime = updateTime;
                                bool isNewLogin = false;
                                try
                                {
                                    if (!api.IsLogin(storeInfo) && (storeInfo.LogStatus == LoginStatus.UnLog || storeInfo.LogStatus == LoginStatus.Log_Succuss))
                                    {
                                        api.Login(group, storeInfo);
                                        isNewLogin = true;
                                        isUpdate = true;
                                    }
                                    if (api.IsLogin(storeInfo))
                                    {
                                        int count = api.GetUnReadMessageCountMini(storeInfo);
                                        if (count > storeInfo.UnReadCount && count > 0)
                                        {
                                            isNewMesage = true;
                                        }
                                        if (storeInfo.UnReadCount != count)
                                        {
                                            isUpdate = true;
                                        }
                                        storeInfo.UnReadCount = count;

                                        if (isNewLogin)
                                        {
                                            //if (storeInfo.CustomerInfos.Count <= 0)
                                            //{
                                            //    api.GetCustomerList(storeInfo);
                                            //}
                                            api.GetSummaryData(storeInfo);

                                            OrderSummaryInfo odrSumInfo = api.UpdateSummaryOrderInfo(storeInfo);
                                            if (null != odrSumInfo)
                                            {
                                                storeInfo.OrderSummaryInfo = odrSumInfo;
                                            }
                                            StoreInfo store = api.GetStoreDetailInfo(storeInfo);
                                            if (null != store && store.is_sip_primary)
                                            {
                                                storeInfo.IsSIP = true;
                                                storeInfo.SIPStores = api.GetSIPStoreDetailInfo(storeInfo);
                                                foreach (SIPStoreInfo sipStore in storeInfo.SIPStores)
                                                {
                                                    odrSumInfo = api.UpdateSummaryOrderInfo(storeInfo, sipStore.country, sipStore.shopid);
                                                    if (!storeInfo.SIPOrderSummaryInfos.Keys.Contains(sipStore.country))
                                                    {
                                                        storeInfo.SIPOrderSummaryInfos.Add(sipStore.country, null);
                                                    }
                                                    storeInfo.SIPOrderSummaryInfos[sipStore.country] = odrSumInfo;
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                if (!bStoreInfoTask)
                                {
                                    break;
                                }
                                Thread.Sleep(isUpdate ? 2000 : 1000);
                                storeInfo = region.Stores.FirstOrDefault(p => p.MessageUpdateTime != updateTime);
                            }
                            if (!bStoreInfoTask)
                            {
                                break;
                            }
                            region = group.Regions.FirstOrDefault(p => p.MessageUpdateTime != updateTime);
                        }
                        if (!bStoreInfoTask)
                        {
                            break;
                        }
                        if (isNewMesage || isUpdate)
                        {
                            storeinfoUpdateHandler?.Invoke(group);
                            try
                            {
                                if (isNewMesage)
                                {

                                    isNewMesage = false;
                                    SoundPlayer player = new SoundPlayer();
                                    player.SoundLocation = System.AppDomain.CurrentDomain.BaseDirectory + "\\tip.wav";
                                    player.Load();
                                    player.PlaySync();
                                }
                            }
                            catch (Exception xe)
                            {
                                Console.WriteLine("播放声音：" + xe.Message);
                            }
                        }
                        Thread.Sleep(2000);
                        group = GroupConfigHelper.Instatce.Groups.FirstOrDefault(p => p.MessageUpdateTime != updateTime);
                    }
                }
                catch (Exception xe)
                {
                    Console.WriteLine(xe.Message);
                }
                if ((DateTime.Now - startTime).Seconds < 30)
                {
                    Thread.Sleep(30 * 1000);
                }
            }
        }
        private void unReadMessageTask_Old(object state)
        {
            ShopeeAPI api = new ShopeeAPI();
            while (bStoreInfoTask)
            {
                DateTime startTime = DateTime.Now;
                try
                {
                    foreach (StoreGroup group in GroupConfigHelper.Instatce.Groups)
                    {
                        bool isNewMesage = false;
                        bool isUpdate = false;

                        foreach (StoreRegion region in group.Regions)
                        {
                            if (region.Stores.Count <= 0)
                            {
                                continue;
                            }
                            foreach (Store storeInfo in region.Stores)
                            {
                                bool isNewLogin = false;
                                try
                                {
                                    if (!api.IsLogin(storeInfo) && (storeInfo.LogStatus==LoginStatus.UnLog || storeInfo.LogStatus == LoginStatus.Log_Succuss))
                                    {
                                        api.Login(group, storeInfo);
                                        isNewLogin = true;
                                        isUpdate = true;
                                    }
                                    if (api.IsLogin(storeInfo))
                                    {
                                        int count = api.GetUnReadMessageCountMini(storeInfo);
                                        if (count > storeInfo.UnReadCount && count > 0)
                                        {
                                            isNewMesage = true;
                                        }
                                        if (storeInfo.UnReadCount != count)
                                        {
                                            isUpdate = true;
                                        }
                                        storeInfo.UnReadCount = count;

                                        if (isNewLogin)
                                        {
                                            //if (storeInfo.CustomerInfos.Count <= 0)
                                            //{
                                            //    api.GetCustomerList(storeInfo);
                                            //}
                                            api.GetSummaryData(storeInfo);

                                            OrderSummaryInfo odrSumInfo = api.UpdateSummaryOrderInfo(storeInfo);
                                            if (null != odrSumInfo)
                                            {
                                                storeInfo.OrderSummaryInfo = odrSumInfo;
                                            }
                                            StoreInfo store = api.GetStoreDetailInfo(storeInfo);
                                            if (null != store && store.is_sip_primary)
                                            {
                                                storeInfo.IsSIP = true;
                                                storeInfo.SIPStores = api.GetSIPStoreDetailInfo(storeInfo);
                                                foreach (SIPStoreInfo sipStore in storeInfo.SIPStores)
                                                {
                                                    odrSumInfo = api.UpdateSummaryOrderInfo(storeInfo, sipStore.country, sipStore.shopid);
                                                    if (!storeInfo.SIPOrderSummaryInfos.Keys.Contains(sipStore.country))
                                                    {
                                                        storeInfo.SIPOrderSummaryInfos.Add(sipStore.country, null);
                                                    }
                                                    storeInfo.SIPOrderSummaryInfos[sipStore.country] = odrSumInfo;
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                if (!bStoreInfoTask)
                                {
                                    break;
                                }
                                Thread.Sleep(isUpdate ? 2000 : 1000);
                            }
                            if (!bStoreInfoTask)
                            {
                                break;
                            }
                        }
                        if (!bStoreInfoTask)
                        {
                            break;
                        }
                        if (isNewMesage || isUpdate)
                        {
                            storeinfoUpdateHandler?.Invoke(group);
                            try
                            {
                                if (isNewMesage)
                                {

                                    isNewMesage = false;
                                    SoundPlayer player = new SoundPlayer();
                                    player.SoundLocation = System.AppDomain.CurrentDomain.BaseDirectory + "\\tip.wav";
                                    player.Load();
                                    player.PlaySync();
                                }
                            }
                            catch (Exception xe)
                            {
                                Console.WriteLine("播放声音：" + xe.Message);
                            }
                        }
                        Thread.Sleep(2000);
                    }
                }
                catch (Exception xe)
                {
                    Console.WriteLine(xe.Message);
                }
                if ((DateTime.Now - startTime).Seconds < 30)
                {
                    Thread.Sleep(30 * 1000);
                }
            }
        }
        private void storeInfoUpdateTask(object state)
        {
            ShopeeAPI api = new ShopeeAPI();
            
            while (bStoreInfoTask)
            {
                try
                {
                    DateTime updateTime = DateTime.Now;
                    StoreGroup group = GroupConfigHelper.Instatce.Groups.FirstOrDefault(p => p.OrderUpdateTime != updateTime);
                    while(null != group)
                    {
                        group.MessageUpdateTime = updateTime;
                        if (group.Plateform != 1)
                        {
                            group = GroupConfigHelper.Instatce.Groups.FirstOrDefault(p => p.MessageUpdateTime != updateTime);
                            Thread.Sleep(2000);
                            continue;
                        }

                        group.OrderUpdateTime = updateTime;
                        bool isNewOrder = false;
                        bool isUpdate = false;
                        StoreRegion region = group.Regions.FirstOrDefault(p => p.OrderUpdateTime != updateTime);
                        while (null != region)
                        {
                            region.OrderUpdateTime = updateTime;
                            if (region.Stores.Count <= 0 || region.RegionID == "gl")
                            {
                                region = group.Regions.FirstOrDefault(p => p.OrderUpdateTime != updateTime);
                                continue;
                            }
                            Store storeInfo = region.Stores.FirstOrDefault(p => p.OrderUpdateTime != updateTime);
                            while (null != storeInfo)
                            {
                                storeInfo.OrderUpdateTime = updateTime;
                                try
                                {
                                    if (api.IsLogin(storeInfo))
                                    {
                                        if (storeInfo.CustomerInfos.Count <= 0)
                                        {
                                            api.GetCustomerList(storeInfo);
                                        }
                                        api.GetSummaryData(storeInfo);

                                        int orderNum = storeInfo.OrderSummaryInfo != null ? storeInfo.OrderSummaryInfo.toship : 0;
                                        OrderSummaryInfo orderSummay = api.UpdateSummaryOrderInfo(storeInfo);
                                        if (null != orderSummay)
                                        {
                                            storeInfo.OrderSummaryInfo = orderSummay;
                                            if (orderSummay.toship != orderNum)
                                            {
                                                isUpdate = true;
                                            }
                                            if (orderSummay.toship > orderNum)
                                            {
                                                isNewOrder = true;
                                            }
                                            if (storeInfo.IsSIP && storeInfo.SIPStores != null)
                                            {
                                                foreach (SIPStoreInfo sipStore in storeInfo.SIPStores)
                                                {
                                                    orderSummay = api.UpdateSummaryOrderInfo(storeInfo, sipStore.country, sipStore.shopid);
                                                    if (null != orderSummay)
                                                    {
                                                        if (!storeInfo.SIPOrderSummaryInfos.Keys.Contains(sipStore.country))
                                                        {
                                                            storeInfo.SIPOrderSummaryInfos.Add(sipStore.country, null);
                                                        }
                                                        if (null != storeInfo.SIPOrderSummaryInfos[sipStore.country])
                                                            if (storeInfo.SIPOrderSummaryInfos[sipStore.country].toship < orderSummay.toship)
                                                            {
                                                                isNewOrder = true;
                                                            }
                                                            else if (storeInfo.SIPOrderSummaryInfos[sipStore.country].toship != orderSummay.toship)
                                                            {
                                                                isUpdate = true;
                                                            }
                                                        storeInfo.SIPOrderSummaryInfos[sipStore.country] = orderSummay;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                if (!bStoreInfoTask)
                                {
                                    break;
                                }
                                Thread.Sleep(5000);
                                storeInfo = region.Stores.FirstOrDefault(p => p.OrderUpdateTime != updateTime);
                            }
                            if (!bStoreInfoTask)
                            {
                                break;
                            }
                            region = group.Regions.FirstOrDefault(p => p.OrderUpdateTime != updateTime);
                        }
                        if (!bStoreInfoTask)
                        {
                            break;
                        }
                        Thread.Sleep(10000);
                        if (isNewOrder || isUpdate)
                        {
                            isUpdate = false;
                            storeinfoUpdateHandler?.Invoke(group);
                            if (isNewOrder)
                            {
                                try
                                {
                                    isNewOrder = false;
                                    SoundPlayer player = new SoundPlayer();
                                    player.SoundLocation = System.AppDomain.CurrentDomain.BaseDirectory + "\\neworder.wav";
                                    player.Load();
                                    player.PlaySync();

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("声音提示异常：" + ex.Message);
                                }
                            }
                        }
                        group = GroupConfigHelper.Instatce.Groups.FirstOrDefault(p => p.OrderUpdateTime != updateTime);
                    }
                }
                catch (Exception xe)
                {
                    Console.WriteLine(xe.Message);
                }
                Thread.Sleep(5 * 60000);
            }
        }
        private void storeInfoUpdateTask_Old(object state)
        {
            ShopeeAPI api = new ShopeeAPI();

            while (bStoreInfoTask)
            {
                try
                {
                    foreach (StoreGroup group in GroupConfigHelper.Instatce.Groups)
                    {
                        bool isNewOrder = false;
                        bool isUpdate = false;
                        foreach (StoreRegion region in group.Regions)
                        {
                            if (region.Stores.Count <= 0)
                            {
                                continue;
                            }
                            foreach (Store storeInfo in region.Stores)
                            {
                                try
                                {
                                    if (api.IsLogin(storeInfo))
                                    {
                                        if (storeInfo.CustomerInfos.Count <= 0)
                                        {
                                            api.GetCustomerList(storeInfo);
                                        }
                                        api.GetSummaryData(storeInfo);

                                        int orderNum = storeInfo.OrderSummaryInfo != null ? storeInfo.OrderSummaryInfo.toship : 0;
                                        OrderSummaryInfo orderSummay = api.UpdateSummaryOrderInfo(storeInfo);
                                        if (null != orderSummay)
                                        {
                                            storeInfo.OrderSummaryInfo = orderSummay;
                                            if (orderSummay.toship !=  orderNum)
                                            {
                                                isUpdate = true;
                                            }
                                                if (orderSummay.toship > orderNum)
                                            {
                                                isNewOrder = true;
                                            }
                                            if (storeInfo.IsSIP && storeInfo.SIPStores != null)
                                            {
                                                foreach (SIPStoreInfo sipStore in storeInfo.SIPStores)
                                                {
                                                    orderSummay = api.UpdateSummaryOrderInfo(storeInfo, sipStore.country, sipStore.shopid);
                                                    if (null != orderSummay)
                                                    {
                                                        if (!storeInfo.SIPOrderSummaryInfos.Keys.Contains(sipStore.country))
                                                        {
                                                            storeInfo.SIPOrderSummaryInfos.Add(sipStore.country, null);
                                                        }
                                                        if (null != storeInfo.SIPOrderSummaryInfos[sipStore.country] )
                                                        if(storeInfo.SIPOrderSummaryInfos[sipStore.country].toship < orderSummay.toship)
                                                        {
                                                            isNewOrder = true;
                                                        }
                                                        else if (storeInfo.SIPOrderSummaryInfos[sipStore.country].toship != orderSummay.toship)
                                                        {
                                                            isUpdate = true;
                                                        }
                                                        storeInfo.SIPOrderSummaryInfos[sipStore.country] = orderSummay;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                if (!bStoreInfoTask)
                                {
                                    break;
                                }
                                Thread.Sleep(5000);
                            }
                            if (!bStoreInfoTask)
                            {
                                break;
                            }
                        }
                        if (!bStoreInfoTask)
                        {
                            break;
                        }
                        Thread.Sleep(10000);
                        if (isNewOrder || isUpdate)
                        {
                            isUpdate = false;
                            storeinfoUpdateHandler?.Invoke(group);
                            if(isNewOrder)
                            {
                                try
                                {
                                    isNewOrder = false;
                                    SoundPlayer player = new SoundPlayer();
                                    player.SoundLocation = System.AppDomain.CurrentDomain.BaseDirectory + "\\neworder.wav";
                                    player.Load();
                                    player.PlaySync();

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("声音提示异常：" + ex.Message);
                                }
                            }
                        }
                    }
                }
                catch (Exception xe)
                {
                    Console.WriteLine(xe.Message);
                }
                Thread.Sleep(5 * 60000);
            }
        }
    }
}
