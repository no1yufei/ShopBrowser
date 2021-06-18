using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData
{
    public class ServerMessageOld
    {
        public int ErrorCode = 0;
        public String Id;
        public String Message;
        public DateTime UpdateTime = DateTime.Now;
    }
    public class ServerMessage<T>
    {
        public T Data;
        public int ErrorCode = 0;
        public String Id;
        public String Message;
        public String UserMeessage;
        public DateTime UpdateTime = DateTime.Now;

        static public ServerMessage<T> FromJson(String str)
        {
            ServerMessage<T> ret = default(ServerMessage<T>);
            try
            {
                ret = JsonConvert.DeserializeObject<ServerMessage<T>>(str);

            }
            catch (Exception xe)
            {
                Console.WriteLine(typeof(T) + "转换Json失败：" + str);
            }
            return ret;
        }
    }
    public class RequestData
    {
        public string UserName;
        public string Password;
        public DateTime UpdateTime;
    }

    public class LoginMessageResponse : ServerMessageOld
    {
        public UserInfo Data;
    }
    public class LoginMessage : ServerMessageOld
    {
        public LoginData Data;
    }
    public class LoginData
    {
        public string UserName;
        public string Password;
        public string DeviceId;
    }
    ////{"d":{"__type":"ServerData.LoginMessageResponse","Data":null,"ErrorCode":1,"Id":"/ShopchatLogin","Message":"错误的用户名或密码！","UpdateTime":"\/Date(1565363777896)\/"}}
    // string msgstr = result.Html.Replace("{\"d\":{\"__type\":", "{").Replace("}}", "}");
    public class _LoginServerRepsonse
    {
        public LoginMessageResponse d;
    }


    public class DeviceInfoMessage : ServerMessageOld
    {
        public DeviceInofData Data;
    }

    public class DeviceInofData
    {
        public string UserName;
        public string Password;
        public string CPU = "0";
        public string IP = "0";
        public DateTime LoginTime = DateTime.Now;
        public bool LogType = false;
        public string Mac = " 0";
        public string MainBoard = "0";
        public string OS = "0";
        public string AddtionalInfo = "0";
        public string SoftVersion = "0";
    }
    public class GroupConfigDataMessage : ServerMessageOld
    {
        public GroupConfigData Data;
    }

    public class GroupConfigData
    {
        public string UserName;
        public string DeviceId;
        public string Password;
        public int GroupCount = 0;
        public int StoreCount = 0;
        public DateTime UpdateTime;
        public string GeneralConfig;
        public string LocalConfig;

    }
    public class _FollowDataRepsonse
    {
        public FollowDatadataMessage d;
    }

    public class FollowDatadataMessage : ServerMessageOld
    {
        public FollowData Data;
    }

    public class FollowData
    {
        public string UserName;
        public string Password;
        public DateTime UpdateTime;
        public int Count;
        public FollowUserData[] UserDatas;
    }
    public class PackOrder:RequestData
    {
        public String OrderSn;
    }
    public class FollowUserData
    {
        public long UserId;//
        public long Shopid;//
        public long Catid;
        public long Folleditemid;
        public long Followedshopid;
        public string Username;
        public string DisplayName;
        public override bool Equals(object obj)
        {
            if (obj is FollowUserData)
            {
                return (obj as FollowUserData).UserId == UserId;
            }
            return base.Equals(obj);
        }
    }
}
