using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace ShopeeChat.Shopee.WebSocket
{
    public class ConnectCommand:MessageCommmand
    {
        
        public ConnectCommand(WebSocketSharp.WebSocket ws,string country,int userid,String devid)
        {
            base.ws = ws;
            base.MesageFifo = new List<ChatMessage>() {
              new ChatMessage{ Message= new TokenMessage13(country),IsBlock=true },
              new ChatMessage{ Message= new LastMssageID74(),ResponseMsgNo=13 },
              new ChatMessage{ Message= new MachineInfo67(userid,devid)},
              new ChatMessage{ Message= new LastTimeStamp201()},
            };
        }

    }
    public class MessageCommmand
    {
        protected WebSocketSharp.WebSocket ws;

        protected List<ChatMessage> MesageFifo = new List<ChatMessage>();
        public void ReceiveReponse(int msgNo,string msgData)
        {
            foreach (ChatMessage cMsg in MesageFifo)
            {
                if (!cMsg.IsRequest && cMsg.ResponseMsgNo == msgNo)
                {
                    ws.Send(cMsg.ToString());
                    Console.WriteLine("发送:" + cMsg.ToString());
                    cMsg.IsRequest = true;
                    if (cMsg.IsBlock)
                    {
                        return;
                    }
                }
            }
        }
        public void SendRequest()
        {
            foreach(ChatMessage cMsg in MesageFifo)
            {
                if(!cMsg.IsRequest && cMsg.ResponseMsgNo <= 0)
                {
                    ws.Send(cMsg.ToString());
                    Console.WriteLine("发送:"+ cMsg.ToString());
                    cMsg.IsRequest = true;
                    if(cMsg.IsBlock)
                    {
                        return;
                    }
                }
            }
        }
    }
}
