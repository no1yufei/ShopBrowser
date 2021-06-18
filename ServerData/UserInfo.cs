
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData
{
    /// <summary>
    /// 登录用户信息，包含绑定设备和用户级别以及
    /// </summary>
    public class UserInfo
    {
        public Guid UserId;
        /// <summary>
        /// 用户名
        /// </summary>
        public string  UserName;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string PassWord;
        [System.Xml.Serialization.XmlIgnore]
        /// <summary>
        /// 绑定的设备ID
        /// </summary>
        public string  DeviceId;
        /// <summary>
        /// 用户的级别
        /// </summary>
        public UserLevel UserLevel;
        /// <summary>
        /// 推荐码
        /// </summary>
        public Guid ReferralCode;
        [System.Xml.Serialization.XmlIgnore]
        public string GroupConfig = "";
        public DateTime RegisteTime;
        public DateTime LogTime = DateTime.Now;
        public string DaPassword ="";

        public UserInfo(){}

        public UserInfo(string userName,string passowrd,string deviceId)
        {
            this.UserName = userName;
            this.DeviceId = deviceId;
            this.UserLevel = UserLevel.FreeUser;
            this.ReferralCode = Guid.Empty;
            this.PassWord = passowrd;
            this.GroupConfig = "";
            this.UserId = Guid.NewGuid();
    }

       
    }

    public enum UserLevel
    {
        /// <summary>
        /// 免费用户
        /// </summary>
        FreeUser = 0,
        /// <summary>
        /// 普通用户
        /// </summary>
        CommonUser =1,
        /// <summary>
        /// VIP用户
        /// </summary>
        VIPUser =2,
        ALLUser
    }
}
