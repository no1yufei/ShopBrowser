using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    public class UserDetailInfo
    {
        //ba_check_status: 2
        //birth_timestamp: 0
        //ctime: 1505380997
        //delivery_address_id: 0
        //delivery_order_count: 0
        //delivery_succ_count: 0
        //disable_new_device_login_otp: false
        //email: "anygogoo@sina.com"
        //fbid: ""
        //feed_private: false
        //followed: false
        public int following_count;//: 1251
//gender: 0
//hide_likes: 0
//holiday_mode: false
//holiday_mode_mtime: 1550111819
public long id;//: 34797586
               //id_address_limit_info: "{}"
               //kyc_consent: false
               //language: "zh-Hant"
               //last_active_time: 1565875536
               //machine_code: "shopee_web_chat"
               //phone: "88613910070929"
               //phone_public: false
               //pn_option: 4294965247
               //portrait: "360aaecb3b0f014e6a53b4b6f3a0dbb8"
               //products: -1
               //rating_count: []
               //        rating_star: 0
               //score: -1
        public long shopid;//: 34796202
                           //smid_status: "not_verified"
                           //status: 1
                           //tos_accepted: true
        public string username;//: "anygogo"
                               //wallet_setting: 2
                               //wholesale_setting: 1
        public bool IsFollowed = true;
        public override bool Equals(object obj)
        {
            if(obj is UserDetailInfo)
            {
                return (obj as UserDetailInfo).id == id;
            }
            return base.Equals(obj);
        }
    }
}
