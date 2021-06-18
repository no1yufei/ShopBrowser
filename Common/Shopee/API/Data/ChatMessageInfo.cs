using Newtonsoft.Json;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee
{
    public class ChatMessageInfo
    {
        public MessageContent content; //;//"{\"content\":{\"text\":\"%{text}%\"},
        public long to_id;//\":%{to_id}%,
        public string  type;//\":\"text\",
        public Guid request_id;//\":\"%{ request_id}%\",
        public String translateStatus = "pending";
        public Guid id;
        public long from_id;//\":12448208}";
        public long order_id = 0;

        public ChatMessageInfo(Store sstore,ShopCustomerInfo scustomer,MessageContent scontent)
        {
            to_id = scustomer.to_id;
            type = scontent.type;
            //shop_id = sstore.ShopInfo.user.shop_id;
            from_id = sstore.ShopInfo.user.id;
            this.content = scontent;
            request_id = Guid.NewGuid();
            id = request_id;
        }

        public ChatMessageInfo(Store sstore, long userID, MessageContent scontent)
        {
            to_id = userID;
            type = scontent.type;
            //shop_id = sstore.ShopInfo.user.shop_id;
            from_id = sstore.ShopInfo.user.id;
            this.content = scontent;
            request_id = Guid.NewGuid();
            id = request_id;
        }
        public ChatMessageInfo(Store sstore, long userID,long orderid, MessageContent scontent)
        {
            order_id = orderid;
            to_id = userID;
            type = scontent.type;
            //shop_id = sstore.ShopInfo.user.shop_id;
            from_id = sstore.ShopInfo.user.id;
            this.content = scontent;
            request_id = Guid.NewGuid();
            id = request_id;
        }
        public String ToJson()
        {
            string json = "";
            try
            {
                json = JsonConvert.SerializeObject(this);
            }
           catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return json;
        }
    }
    
    public class MessageContent
    {
        [NonSerialized]
        public string type;
    }
    public class TextMessageContent: MessageContent
    {
        
        public String text;
        public TextMessageContent()
        { }
        public TextMessageContent(String text)
        {
            this.text = text;
            type = "text";
        }
        public override string ToString()
        {
            return text;
        }
    }
    public class ImageMessageContent : MessageContent
    {

        public String url;
        public ImageMessageContent()
        { }
        public ImageMessageContent(String url)
        {
            this.url = url;
            type = "image";
        }
        public override string ToString()
        {
            return type + url;
        }
    }
    public class ProductMessageContent : MessageContent
    {

        public long product_id;//: 3600659313, 
        public long shop_id;//: 176116588
        public ProductMessageContent()
        { }
        public ProductMessageContent(long productid,long shopid)
        {
            this.product_id = productid;
            this.shop_id = shopid;
            this.type = "product";
        }
        public override string ToString()
        {
            return type + product_id +"|"+shop_id;
        }
    }

}
