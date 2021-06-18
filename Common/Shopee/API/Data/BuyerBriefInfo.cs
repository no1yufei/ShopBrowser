using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    public class BuyerBriefInfo
    {
        public BuyerAcount account;
        public BuyerRating buyer_rating;
        public bool chat_disabled;//: false
        public long last_active_time;//: 1568291934
        public long mtime;//: 1488630109
        public long ctime;//: 1536128960
        public long shopid;//: 7781602
        public long userid;//: 7782895
        public int status;//: 1
        public int follower_count;//: 0
    }
    public class BuyerAcount
    {
        public bool email_verified;//: true
        public string fbid;//: ""
                           //public feed_account_info: null
        public int following_count;//: 118
        public int? hide_likes;//: 0
        public bool is_seller;//: false
        public bool phone_verified;//: true
        public string portrait;//: "59b874388fe6fee52163c4517620933d"
        float totalavgstar = 0;
        public float? total_avg_star
        {
            set { totalavgstar = (float)(value == null ? 0 : value); }
            get { return totalavgstar; }
        }//: 5
        public string username;//: "eoll92111"
    }
    public class BuyerRating
    {
        int[] ratingcount =new  int[]{0, 0, 0, 0, 0, 0};
        public int[] rating_count//: [0, 0, 0, 0, 0, 270]
        {
            set
            {
                if (null != value)
                {
                    ratingcount = value;
                }
            }
            get { return ratingcount; }
        }

        float ratingstar = 0;
        public float? rating_star//: 5
        {
            set { ratingstar = (float)(value == null ? 0 : value); }
            get { return ratingstar; }
        }
    }
}
