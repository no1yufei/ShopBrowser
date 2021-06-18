using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.WebSocket
{
    public class ChatMessage
    {
        public WSMessage Message;
        public int ResponseMsgNo = -1;
        public bool IsBlock = false;
        public bool IsRequest = false;
        public bool IsResponse = false;

        public override string ToString()
        {
            return Message.ToMessageString();
        }
    }
    public class TokenMessage13: WSMessage
    {
        public string token;
        public string country;

        public TokenMessage13(string country)
            :base(42,13)
        {
            this.token = Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", ""))); ;
            this.country = country.ToUpper();
        }
    }

    public class LastMssageID74:WSMessage
    {
        public long last_msgid;
        public long inapp_timestamp;
        public LastMssageID74(long msgId = -1)
           : base(42, 74)
        {
            this.last_msgid = msgId;
            inapp_timestamp = Tool.GetTimeStampSeconds();
        }
        
    }
    public class MachineInfo67:WSMessage
    {
        public long userid;//":12403613,
        public string machine_code = "shopee_web_chat";
        public string deviceid;//8650ad1221941b3a3918b0b51b7fde5\\\"
        public MachineInfo67(long userid,string devid)
          : base(42, 67)
        {
            this.userid = userid;
            this.deviceid = devid;
        }
    }
    //string lastTimeStamp = "42[\"message\",\"201{\\\"last_timestamp\\\":-1,\\\"start_from_old\\\":true,\\\"from_webchat\\\":true}\"]";
    public class LastTimeStamp201 : WSMessage
    {
        public long last_timestamp;//":12403613,
        public string start_from_old = "true";
        public string from_webchat = "true";
      
        public LastTimeStamp201(long timestamp=-1)
          : base(42, 201)
        {
            this.last_timestamp = timestamp;
        }
    }


}
