using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.WebSocket
{
    public class WSMessage
    {
        static public string MessageHeader = "42[\"message\",\"";
        [Newtonsoft.Json.JsonIgnore]
        public int MessageType = 42;
        [Newtonsoft.Json.JsonIgnore]
        public int MessageNo;
        public WSMessage(int type,int no)
        {
            this.MessageType = type;
            this.MessageNo = no;
        }
        public String ToMessageString()
        {
            String jsonStr = string.Empty;
            try
            {
                jsonStr = JsonConvert.SerializeObject(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            jsonStr = jsonStr.Replace("\"", "\\\"");
            return MessageHeader + MessageNo + jsonStr + "\"]";
            //return MessageHeader + MessageNo + jsonStr + "\"]";
        }
        static public int GetMessageNo(string message)
        {
            string msg = message.Replace(MessageHeader, "");
            string no = msg.Substring(0, msg.IndexOf('{'));
            return int.Parse(no);
        }
    }
    
    }

