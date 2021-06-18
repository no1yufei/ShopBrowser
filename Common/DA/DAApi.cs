using CommonData;
using CommonData.APPData;
using CommonData.SysData;
using CsharpHttpHelper;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Renci.SshNet.Messages;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DA
{
    public class DAApi
    {
        //private static string host = "http://localhost:5000";
        private static string host = "http://49.234.107.25:5000";
        private static string uihost = "http://119.23.185.128";

        private string oauthBaseAddrss = host + "/api/oauth/";
        private string baseArrder = host + "/api/scp/";

        public string DaAccess(DA da, string username, string password)
        {
           return uihost + "/#/login?ln="+ username + "&pw="+ password + "&r=home";
        }
        public bool PushOrder(DA da, DAOrder order)
        {
            bool ret = false;
            if (IsLogin(da))
            {
                string url = baseArrder + "WokerOrder/push";

                HttpResult spcresult = da.Hhh.Post(url,ToJson<DAOrder>(order));
                DAMessageData<String> msgData = FromJson<DAMessageData<String>>(spcresult.Html);
                if (null != msgData)
                {
                    if (msgData.code == 200)
                    {
                        ret = true;
                        Console.WriteLine("推送订单成功！" + order.SOrderSN);
                    }
                    else
                    {

                        Console.WriteLine("推送订单失败！" + msgData.message);
                    }
                }
                else
                {
                    Console.WriteLine("网络错误！");
                }
            }
            return ret;
        }
        public String UploadFile(DA da, string filePath)
        {
            string ret = "";
            if (IsLogin(da))
            {
                string url = baseArrder + "file/UploadImg";
                byte[] imageBytes;
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    imageBytes = new byte[stream.Length];
                    stream.Read(imageBytes, 0, (int)stream.Length);
                }
                HttpResult spcresult = da.Hhh.PostImage(url, imageBytes,Path.GetFileName(filePath));
                DAMessageData<String> msgData = FromJson<DAMessageData<String>>(spcresult.Html);
                if (null != msgData)
                {
                    if (msgData.code == 200)
                    {
                        ret = msgData.data;
                        Console.WriteLine("推送面单文件成功！" + ret);
                    }
                    else
                    {

                        Console.WriteLine("推送面单文件失败！" + msgData.message);
                    }
                }
                else
                {
                    Console.WriteLine("网络错误！");
                }
            }
            return ret;
        }

        public UserConfig GetConfig(DA da,string token ,out string msg)
        {
            SCPResponseData<UserConfig> ret = null;
            //if (IsLogin(da))
            {
               da.Hhh.Authorization = "" == token? da.Hhh.Authorization : token;
                string deviceId = MachineInfoHelper.GetMachineID();
                da.Hhh.iTimeOut = 30000 * 5;
                string url = baseArrder + "config/get?v=" + AccessControl.Instance.Version.Replace(".", "_");
                //string url = baseArrder + "config/get" ;
                HttpResult spcresult = da.Hhh.Get(url);

                ret = SCPResponseData<UserConfig>.FromJson(spcresult.Html);
                if (null != ret)
                {
                    msg = ret.message;
                    if (ret.code == 200)
                    {
                        ret.data.Token = da.Hhh.Authorization;
                        Console.WriteLine("获取配置文件:成功！");
                        return ret.data;
                    }
                    else 
                    {
                        Console.WriteLine("获取配置文件:失败！" + ret.message);
                    }
                }
                else
                {
                    msg = "获取配置文件:网络错误！" + spcresult.Html;
                    Console.WriteLine("获取配置文件:网络错误！" + spcresult.Html);
                }
            }
            return null;
        }
        public bool UploadConfig(DA da, GroupConfig config)
        {
            SCPResponseData<UserConfig> ret = null;
            //if (IsLogin(da))
            {
            
                string url = baseArrder + "config/upload";
                HttpResult spcresult = da.Hhh.Post(url,(new SCPRequestData<GroupConfig>(config)).ToJson());

                ret = SCPResponseData<UserConfig>.FromJson(spcresult.Html);
                if (null != ret)
                {
                    if (ret.code == 200)
                    {
                        Console.WriteLine("上传配置文件成功！");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("上传配置文件失败！" + ret.message);
                    }
                }
                else
                {
                    Console.WriteLine("上传配置文件失败！" + "网络错误！");
                }
            }
            return false;
        }
        public bool UploadDeviceInfo(DA da, DeviceInfo devInfo)
        {
            SCPResponseData<String> ret = null;
            //if (IsLogin(da))
            {

                string url = baseArrder + "config/uploaddeviceinfo";
                HttpResult spcresult = da.Hhh.Post(url, (new SCPRequestData<DeviceInfo>(devInfo)).ToJson());

                ret = SCPResponseData<String>.FromJson(spcresult.Html);
                if (null != ret)
                {
                    if (ret.code == 200)
                    {
                        Console.WriteLine("上传登录信息成功！");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("上传登录信息失败！" + ret.message);
                    }
                }
                else
                {
                    Console.WriteLine("上传登录信息失败！" + "网络错误！");
                }
            }
            return false;
        }

        public bool UploadBackUpImgs(DA da, List<AppImageBakup>  imgs)
        {
            SCPResponseData<String> ret = null;
            //if (IsLogin(da))
            {

                string url = baseArrder + "Holiday/ImgBakup";
                HttpResult spcresult = da.Hhh.Post(url, (new SCPRequestData<List<AppImageBakup>>(imgs)).ToJson());

                ret = SCPResponseData<String>.FromJson(spcresult.Html);
                if (null != ret)
                {
                    if (ret.code == 200)
                    {
                        Console.WriteLine("上传图像备份信息成功！");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("上传图像备份信息失败！" + ret.message);
                    }
                }
                else
                {
                    Console.WriteLine("上传图像备份信息失败！" + "网络错误！");
                }
            }
            return false;
        }
        public bool Auth(DA da, string username, string password,out string msg,out int code)
        {
            SCPResponseData<String> ret = null;
            //if (IsLogin(da))
            {
                string deviceId = MachineInfoHelper.GetMachineID();
                //string url = oauthBaseAddrss + "auth?username=" + username + "&deviceid=" + deviceId + "&password=" + password+"&logtime="+ logtime.ToString();
                string url = oauthBaseAddrss + "auth";
                LoginData ldata = new LoginData();
                ldata.DeviceId = deviceId;
                ldata.Password = password;
                ldata.UserName = username;
                ldata.Version = AccessControl.Instance.Version;
                da.Hhh.iTimeOut = 30000 * 5;

                HttpResult spcresult = da.Hhh.Post(url,(new SCPRequestData<LoginData>(ldata)).ToJson());

                ret = SCPResponseData<String>.FromJson(spcresult.Html);
                if (null != ret)
                {
                    msg = ret.message;
                    code = ret.code;
                    if (ret.code == 200)
                    {
                        da.Hhh.Authorization = " Bearer " + ret.data;
                        da.Token = da.Hhh.Authorization;
                      
                        Console.WriteLine("登录成功！");
                        return true;
                    }
                    else if (ret.code == 999)
                    {
                        //ret = ErrorCode.NonUser;
                        Console.WriteLine("登陆失败！" + ret.message);
                    }
                    else 
                    {
                       // ret = ErrorCode.Error;
                        Console.WriteLine("登陆失败！" + ret.message);
                    }
                }
                else
                {
                    msg = "登陆网络错误！" + spcresult.Html+spcresult.StatusCode;
                    code = -1;
                    Console.WriteLine("登陆网络错误！" + spcresult.Html + spcresult.StatusCode);
                }
            }
            return false;
        }


        public bool Register(DA da, string username, string password, string displayname)
        {
            bool ret = false;
            //if (IsLogin(da))
            {
                string url = baseArrder + "rbac/user/create";
                UserCreateViewModel ucm = new UserCreateViewModel();
                ucm.LoginName = username;
                ucm.Password = password;
                ucm.DisplayName = displayname;
                HttpResult spcresult = da.Hhh.Post(url, ToJson<UserCreateViewModel>(ucm));
                if (spcresult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ret = true;
                    Console.WriteLine("注册成功！");
                }
                else
                {
                    Console.WriteLine("注册失败！" + spcresult.Html);
                }
            }

            return ret;
        }


        public bool IsLogin(DA da)
        {
            return da.Token != null && da.Token != "";
        }
        public T FromJson<T>(string jsonStr)
        {
            T dataTemplate = default(T);
            try
            {
                dataTemplate = JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
        public  string ToJson<T>(T obj)
        {
            string dataTemplate = "";
            try
            {
                dataTemplate = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
    }
    
    public class DAMessageData<T>
    {
        public int code;//: 200
        public T data;//: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImRsdCIsInVzZXJJbmRleCI6IjI1Iiwid2hJbmRleCI6IjAiLCJndWlkIjpbImY1ZDQ4MTQ4LTlmN2YtNDQ4YS04NWE3LTdkZmQ3MGJkYjFmNiIsImY1ZDQ4MTQ4LTlmN2YtNDQ4YS04NWE3LTdkZmQ3MGJkYjFmNiJdLCJhdmF0YXIiOiIiLCJkaXNwbGF5TmFtZSI6IuW6l-iBiumAmui0puWPtyIsImxvZ2luTmFtZSI6ImRsdCIsImVtYWlsQWRkcmVzcyI6IiIsInVzZXJUeXBlIjoiMiIsIm5iZiI6MTU5MjA0MjE2MiwiZXhwIjoxNTkyNjQ2OTYyLCJpYXQiOjE1OTIwNDIxNjJ9.YmZiFvIpxQIkgXpGGOVxJWpqZXX5nLB-eODfMGS7yps"
        public string message;//: "操作成功"
    }
    public class UserCreateViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        public int Status = 1;
        /// <summary>
        /// 
        /// </summary>
        public int UserType = 2;

        public string Description = "店聊通自动创建用户";
        public int WHIndex = 0;
    }

    public enum ErrorCode
    {
        Succuess = 0,
        Error = 1,
        NetError = 2,
        NonUser = 3
    }
}
