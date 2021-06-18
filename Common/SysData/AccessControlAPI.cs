using Common.DA;
using Common.Shopee.API.Data;
using CommonData;
using CommonData.SysData;
using CommonData.SysData.Enum;
using CsharpHttpHelper;
using Newtonsoft.Json;
using ServerData;
using ShopeeChat.Shopee.API;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;
using LoginData = ServerData.LoginData;

namespace ShopeeChat.SysData
{
    public class AccessControl
    {
        string basePath = Environment.SystemDirectory;
        string configFileName = "\\shopchatplusv2.cfg";
        private string registerURL = "http://www.dianliaotong.bjepower.com";
      
        static private AccessControl instance;
        public DA DA = new DA();
        DAApi dApi = new DAApi();
        public string Version;
        static public AccessControl Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new AccessControl();
                }
                return instance;
            }
        }
        public void Initialize()
        {
        }
        public bool IsLevelRight(CommonData.SysData.Enum.UserLevel userLevel)
        {
            return (int)userLevel < (int)userInfo.UserLevel || (int)userLevel == (int)userInfo.UserLevel;
        }
        private CommonData.SysData.UserConfig userInfo = null;
        public string UserLeveLName()
        {
            string userLN = "无";
            switch ((int)userInfo.UserLevel)
            {
                case 0:
                    userLN = "免费用户";
                    break;
                case 1:
                    userLN = "高级用户";
                    break;
                case 2:
                    userLN = "VIP用户";
                    break;
                default:
                    userLN = "无";
                    break;
            }

            return userLN;
        }
        public string UserName
        {
            get
            {
                if (userInfo == null)
                {
                    MessageBox.Show("用户名称未能正确创建，请报告软件作者！");
                }
                return userInfo.UserName;
            }
        }
        public UserStatus UserStatus
        {
            get
            {
                return (null == userInfo) ? UserStatus.All : userInfo.UserStatus;
            }
           
        }
       
        public string DeviceID
        {
            get
            {
                if (null == userInfo)
                {
                    if (userInfo == null)
                    {
                        MessageBox.Show("设备标识未能正确取得1，请报告软件作者！");
                    }
                    return MachineInfoHelper.GetMachineID();
                }
                else
                {
                    return userInfo.DeviceId;
                }
            }
        }
        public string Token
        {
            get
            {
                return userInfo == null ? "" : userInfo.Token;
            }
        }


        public string DecryptString(String str)
        {
            return AES.AESDecrypt(str, DeviceID, UserName);
        }
        public string EncryptString(String str)
        {
            return AES.AESEncrypt(str, DeviceID, UserName);
        }
        public string EncryptString(String str, string ps)
        {
            return AES.AESEncrypt(str, ps, DateTime.MinValue.ToString("yyyymmddhhMMss"));
        }
        public string DecryptString(String str, string ps)
        {
            return AES.AESDecrypt(str, ps, DateTime.MinValue.ToString("yyyymmddhhMMss"));
        }
        public string GetGroupConfigStr()
        {
            if (userInfo == null)
            {
                MessageBox.Show("未能正确登录并读取配置，请报告软件作者！");
            }
            return userInfo.GroupConfig == "" ? "" : DecryptString(userInfo.GroupConfig,userInfo.DeviceId);
        }
        public bool UpdateGroupConfig(string configStr)
        {
            try
            {
                CommonData.SysData.GroupConfig data = new CommonData.SysData.GroupConfig();
                data.UserName = UserName;

                string generalStr = EncryptString(configStr, MachineInfoHelper.GetMachineID());
                string localStr = EncryptString(configStr, MachineInfoHelper.GetMachineID());
                data.GeneralConfig = generalStr;
                data.LocalConfig = localStr;
                data.GroupCount = GroupConfigHelper.Instatce.GetGroupCount();
                data.StoreCount = GroupConfigHelper.Instatce.GetStoreCount();
                data.DeviceId = MachineInfoHelper.GetMachineID();

               
                dApi.UploadConfig(DA, data);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("当前网络环境不适宜Shopee账号健康，请考虑更换硬件和网络环境。" + ex.Message);
            }
            return false;
        }
        public bool Login(string username, string passowrd)
        {
            bool isLogSuccess = false;
            DateTime logTime = DateTime.Now;
            string Md5Password = DataHelper.GetPasswordStr(username, passowrd);
            isLogSuccess = verifyUserByAuth(username, Md5Password);
            return isLogSuccess;
        }
        public bool Login(CommonData.SysData.UserConfig userinfo)
        {
            bool isLogSuccess = false;
            string username = userinfo.UserName;
            isLogSuccess = verifyUserByToken(userinfo.UserName,userinfo.Token);
            return isLogSuccess;
        }
      
        private bool verifyUserByAuth(string username, string md5password)
        {
            bool isLogSuccess = false;
            
            UserConfig userconfig = null;
            try
            {
                    string logmsg = "准备登录";
                    int code = -1;
                    bool authRet = dApi.Auth(DA, username, md5password, out logmsg, out code);
                    if(authRet)
                    {
                        userconfig = dApi.GetConfig(DA, "",out logmsg);
                  
                        if (null == userconfig)
                        {
                            code = -1;
                        }
                    }

                    if (200 == code)
                    {
                    userconfig.UserName = username;
                    userInfo = userconfig;
                        isLogSuccess = true;
                        Console.WriteLine("登录成功：" + logmsg);
                    }
                    else
                    {
                        Console.WriteLine("登录失败:" + logmsg);
                        MessageBox.Show("登录失败:" + logmsg);
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("登录异常："+ex.Message);
                MessageBox.Show(ex.Message, "登录异常");
            }
            if (isLogSuccess)
            {
                //上传当前登录环境
                ThreadPool.QueueUserWorkItem(new WaitCallback(uploadDeviceInfo), username);
            }
            return isLogSuccess;
        }
        private bool verifyUserByToken(string username,string token)
        {
            bool isLogSuccess = false;
            string msg = "";
            UserConfig userconfig = null;
            try
            {
                if (string.Empty != token)
                {
                    userconfig = dApi.GetConfig(DA, token, out msg);
                }
                if (null == userconfig)
                {
                        Console.WriteLine("登录失效，请重新登录！" + msg);
                        MessageBox.Show("本地保存登录信息已经过期！请重新验证密码！" + msg);
                }
                else
                {
                    userconfig.UserName = username;
                    userInfo = userconfig;
                    isLogSuccess = true;
                    Console.WriteLine("登录成功" );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("登录异常：" + ex.Message);
                MessageBox.Show(ex.Message, "登录异常"+msg);
            }
            if (isLogSuccess)
            {
                //上传当前登录环境
                ThreadPool.QueueUserWorkItem(new WaitCallback(uploadDeviceInfo), username);
            }
            return isLogSuccess;
        }
        private void saveUserInfo()
        {
            try
            {
                string filePath = basePath + configFileName;

                using (FileStream fsstream = File.Open(filePath, FileMode.Create))
                {
                    using (StringWriter writer = new StringWriter())
                    {
                        System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(CommonData.SysData.UserConfig));
                        xs.Serialize(writer, userInfo);
                        string str = writer.ToString();
                        str = EncryptString(str,MachineInfoHelper.GetMachineID());
                        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
                        fsstream.Write(bytes, 0, bytes.Length);
                    }
                }
                Console.WriteLine("配置文件已经写入：" + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("加载本地配置：" + ex.Message);
            }
        }
        public CommonData.SysData.UserConfig LoadUserUserInfoFromLocal()
        {
            CommonData.SysData.UserConfig userinfo = null;
            try
            {
                string filePath = basePath + configFileName;
                using (FileStream fs = File.OpenRead(filePath))
                {
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                    string encodeConfStr = System.Text.Encoding.UTF8.GetString(bytes);
                    string decodeConStr = DecryptString(encodeConfStr, MachineInfoHelper.GetMachineID());
                    using (StringReader sr = new StringReader(decodeConStr))
                    {
                        System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(CommonData.SysData.UserConfig));
                        userinfo = (CommonData.SysData.UserConfig)xs.Deserialize(sr);
                    }
                }
                Console.WriteLine("配置文件已经载入");
            }
            catch (Exception ex)
            {
                Console.WriteLine("配置文件载入错误:" + ex.Message);
            }
            return userinfo;
        }
        public bool Regist(string username, string passowrd, string referralcode)
        {
            System.Diagnostics.Process.Start(registerURL); ;
            return true;

        }
        public DeviceInfo buildDeviceInfo(string username)
        {
            DeviceInfo data = new DeviceInfo();
            try
            {
                data.SoftVersion = Version;
                //data.UserName = username;
                //data.Password = userInfo.PassWord;
                data.CPU = MachineInfo.Instance.CPUInfo;
                string[] ips = MachineInfo.Instance.GetExtenalIpAddress();
                data.IP = ips[1] + ":" + ips[0];
                data.LoginTime = DateTime.Now;
                data.LogType = false;
                data.Mac = MachineInfo.Instance.LocalMac;
                data.MainBoard = MachineInfo.Instance.BoardInfo;
                data.OS = MachineInfo.Instance.OSInfo;
                data.AddtionalInfo = DeviceID + "||" + MachineInfoHelper.GetMachineID() + "|" + MachineInfo.Instance.GetHardDiskSerialNumber() + "|" + MachineInfo.Instance.GetDiskSerialNumber();
            }
            catch (Exception ex)
            {
                MessageBox.Show("当前网络环境不适宜Shopee账号健康，请考虑更换硬件和网络环境。" + ex.Message);
            }
            return data;
        }
        private void uploadDeviceInfo(object usernameOjb)
        {
            try
            {
                string username = (string)usernameOjb;
                DeviceInfo devInfo = buildDeviceInfo(username);
                dApi.UploadDeviceInfo(DA, devInfo);
                saveUserInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("当前网络环境不适宜Shopee账号健康，请考虑更换硬件和网络环境。" + ex.Message);
            }
        }

        public void LoginStore()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(loginGroupStore));
        }
        private void loginGroupStore(object state)
        {
            ShopeeAPI api = new ShopeeAPI();
            bool fetchAllFlag = false;
            while (!fetchAllFlag)
            {
                try
                {
                    fetchAllFlag = true;
                    foreach (StoreGroup group in GroupConfigHelper.Instatce.Groups)
                    {
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
                                    if ((!api.IsLogin(storeInfo) 
                                        && (storeInfo.LogStatus == LoginStatus.UnLog || storeInfo.LogStatus == LoginStatus.Log_Succuss))
                                        || storeInfo.TotalErrorTime > 5
                                        )
                                    {
                                        api.Login(group, storeInfo);
                                    }
                                    if (api.IsLogin(storeInfo))
                                    {
                                        int count = api.GetUnReadMessageCount(storeInfo);

                                        storeInfo.UnReadCount = count;
                                        if (storeInfo.CustomerInfos.Count <= 0)
                                        {
                                            api.GetCustomerList(storeInfo);
                                        }
                                        api.GetSummaryData(storeInfo);
                                        api.UpdateSummaryOrderInfo(storeInfo);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                Thread.Sleep(5000);
                            }
                        }
                        Thread.Sleep(10000);
                    }
                }
                catch (Exception xe)
                {
                    Console.WriteLine(xe.Message);
                }
                //Thread.Sleep(5 * 60000);
            }
        }
        
    }
}

