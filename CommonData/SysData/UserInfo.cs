using CommonData.SysData.Enum;
using System;

namespace CommonData.SysData
{
    public class UserConfig
    {
       
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName;
       
        [System.Xml.Serialization.XmlIgnore]
        /// <summary>
        /// 绑定的设备ID
        /// </summary>
        public string DeviceId;
        /// <summary>
        /// 用户的级别
        /// </summary>
        public UserLevel UserLevel;
       
        [System.Xml.Serialization.XmlIgnore]
        public string GroupConfig = "";
        public string Token = "";
        public UserStatus UserStatus = UserStatus.Normal;
        public UserConfig() { }

        public UserConfig(string userName, string passowrd, string deviceId)
        {
            this.UserName = userName;
            this.DeviceId = deviceId;
            this.UserLevel = UserLevel.FreeUser;
            this.GroupConfig = "";
        }


    }

    
}
