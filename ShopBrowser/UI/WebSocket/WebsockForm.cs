using CsharpHttpHelper;
using ShopeeChat.Shopee.API;
using ShopeeChat.Shopee.WebSocket;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace ShopeeChat
{
    public partial class WebsockForm : Form
    {
        WebSocket ws1;
        WebSocket ws2;
        public WebsockForm()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        delegate void setTextdete(string text);
        void  setText(string text)
        {
            if(richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new setTextdete(setText),new object[]{ text});
            }
            else
            {
                richTextBox1.Text +=DateTime.Now.ToString()+":"+ text + Environment.NewLine;
            }
        }

        delegate void setMsgTextdete(string text);
        void setMsgText(string text)
        {
            if (richTextBox2.InvokeRequired)
            {
                richTextBox2.Invoke(new setMsgTextdete(setMsgText), new object[] { text });
            }
            else
            {
                richTextBox2.AppendText(DateTime.Now.ToString() + ":" + text + Environment.NewLine);
            }
        }
        bool first = true;
        string cmd208 = "42[\"message\",\"208{\\\"userid\\\":[35906380]}\"]";
        string cmd72 = "42[\"message\",\"72{\\\"chatid\\\":0,\\\"cursor\\\":0,\\\"userid\\\":35906380,\\\"limit\\\":10}\"]";
        string cmd73="42[\"message\",\"73{\\\"msgid\\\":[1254628437816180842,1254555923091947637,1254555864873894008,1254555822113685636]}\"]";
        private void button1_Click(object sender, EventArgs e)
        {
                ws2.Send(cmd208);
                ws2.Send(cmd72);
                ws2.Send(cmd72);
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            if(null != ws1)
            {
                ws1.Close();
            }
            if(null != ws2)
            {
                ws2.Close();
            }
            
            ws1 = null;
            ws2 = null;
        }
        /// <summary>  
        /// 获取时间戳  13位
        /// </summary>  
        /// <returns></returns>  
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        string token;// = Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", "")));
        string tokenMessage;
        string lastMssageID;
        
        string machineInfo =  "42[\"message\",\"67{\\\"userid\\\":12403613,\\\"machine_code\\\":\\\"shopee_web_chat\\\",\\\"deviceid\\\":\\\"18650ad1221941b3a3918b0b51b7fde5\\\"}\"]";
        string lastTimeStamp ="42[\"message\",\"201{\\\"last_timestamp\\\":-1,\\\"start_from_old\\\":true,\\\"from_webchat\\\":true}\"]";
        
        bool initInfo = false;
        String gotCookies;
        String countToken;
        MessageCommmand curCommand;
        Store store;
        private void button3_Click(object sender, EventArgs e)
        {
            if (null != ws2)
            {
                initInfo = false;
                ws2.Close();
            }
            //HtmlHttpHelper hhh = new HtmlHttpHelper();
            //string sessionQuestStr = "https://seller.shopee.co.id/webchat/api/v1/sessions";
            //Guid deviceid = Guid.NewGuid();
            //string postData = "{\"username\": \"anygogo01\", \"password\": \"Liu189689\", \"device_id\": \""+deviceid.ToString()+"\"}";

            store = new Store(1,"ID","vvbuy3.id", "FZ3344shopee");
            StoreGroup group = new StoreGroup();
            ShopeeAPI api = new ShopeeAPI();
            api.Login(group,store);
            api.GetCustomerList(store);

            //hhh.sCookies = "_ga=GA1.2.940952754.1557648319; _gid=GA1.2.620539493.1557648319; _gat=1";
            //HttpResult  result = hhh.Post(sessionQuestStr,postData,"UTF-8");
            //if(null != result.Html)
            //{
            //    //{"status": "verified", "p_token": "PeG2WlPc9nKsu7/NojiQNNirEQcjBKcwY9bxUWUNCwQ=", "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFueWdvZ28wMSIsImNyZWF0ZV90aW1lIjoxNTU3NjQ4MzYyLCJpZCI6ImM5MWIzYzk4NzQ4YzExZTk5M2NhY2NiYmZlZjc2MzVkIiwiZGV2aWNlX2lkIjoiOTg4NDk4MjEtNzE4Ni00MTEwLWE1ZDItNzAwZDJiYTMyNTlmIn0.L_03-sNhVMR_tOxERG9nJiJHG-1Znkc595BCo_gi9To", "user": {"username": "anygogo01", "rating": 0, "uid": "0-61937185", "city": null, "locale": "zh-Hans", "gender": "unknown", "created_at": "2018-03-23T15:39:23+08:00", "distribution_status": null, "updated_at": "2019-05-12T15:42:18+08:00", "logined_at": "2019-05-12T12:19:11+08:00", "age": 49, "shop_id": 61935741, "status": "normal", "avatar": "https://cfshopeetw-a.akamaihd.net/file/9510e04ba0c437081f6c2748a58fefde", "country": "TW", "is_blocked": false, "type": "seller", "id": 61937185}}
            //    foreach(String nameValue in result.Html.Replace("{","").Replace("}","").Replace("\"","").Split(','))
            //    {
            //        if(nameValue.Split(':')[0].Trim().StartsWith("token"))
            //        {
            //            countToken = nameValue.Split(':')[1].Trim();
            //            break;
            //        }
            //    }
            //}
            if(store.Token != null)
            {
                HtmlHttpHelper hhh = store.Hhh;
                gotCookies = hhh.sCookies;
                string[] cookies = hhh.sCookies.Split(';');//result.Cookie.Split(',');
                ws2 = new WebSocket("wss://chat-ws.shopee.co.id/socket.io/?EIO=3&transport=websocket");
                //ws2.Origin = "https://seller.shopee.cn";
                foreach(String cookie in cookies)
                {
                    if(cookie != "")
                    {
                        int length = cookie.Split(';')[0].Length;
                        int position = cookie.Split(';')[0].IndexOf('=');
                        string sName = cookie.Split(';')[0].Substring(0, position);
                        string sValue = cookie.Split(';')[0].Substring(position + 1, length - position - 1);

                        if (sName == "SPC_EC" && sValue != "")
                        {
                            token = sValue;
                        }
                        
                        ws2.SetCookie(new Cookie(sName, sValue));
                    }
                }

                //ws2.SetCookie(new Cookie("SPC_EC", "i9ayDk2YzlS0OVRp0tG+EDyUoSOzurT0kgCw5Um9M9k6d4ag73XFpqfS5PQaXqvRfps2wJL0w9+qfqWs/543Ap6U76zuc5+N7k8/0W0dxnZtlizK4a3uz1Jc5UGXO5IIArjWwEk1eg5nDAlVqHE8Vkd+yNqR5KFp9kiS1WbIIFY="));
                //ws2.SetCookie(new Cookie("SPC_SC_TK", "55cd28958cf7dff72f91a15b76ec5758"));
                //ws2.SetCookie(new Cookie("SPC_SC_UD", "12403613"));
                token = Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().ToLower().Replace("-", "")));

                token = Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString("N")));
                //token = "NDhmNjg4MGMxY2JmNGU3MGE5MzExMDA2YjI0NWY4M2U=";
                tokenMessage = "42[\"message\",\"13{\\\"token\\\":\\\"" + token + "\\\",\\\"country\\\":\\\"ID\\\"}\"]";
                //tokenMessage = "42[\"message\",\"13{\\\"username\\\":\\\"anygogo01\\\",\\\"token\\\":\\\"%{token}%\\\",\\\"password\\\":\\\"\\\",\\\"country\\\":\\\"TW\\\",\\\"requestid\\\":\\\"%{timestamp}%-22534\\\"}\"]";
                //tokenMessage = tokenMessage.Replace("%{token}%", token).Replace("%{timestamp}%", Tool.GetTimeStampMilseconds().ToString()); ;
                //curCommand = new ConnectCommand(ws2,"TW", 12403613,deviceid.ToString().Replace("-",""));
                //tokenMessage = "42[\"message\",\"13{\\\"username\\\":\\\"mostbuy.tw\\\",\\\"token\\\":\\\"v39YbsLtDMwj+mZrA2syPf38Brv/g9xNCNujwC7spL+z/DWnI05CSg5jjNk5Ba7SSb9hSUZXsdpFkhETn7zM7Bl64Achl7d+A7SYHzrd8raC8XBc4FOSwcj8lVunNkwQNltoRBMoR14iuewfZxolgZBhhiCAF5WxISyLFoEing0=\\\",\\\"password\\\":\\\"\\\",\\\"country\\\":\\\"TW\\\",\\\"requestid\\\":\\\"" + Tool.GetTimeStampMilseconds() + "-11503\\\"}\"] ";
                 ws2.OnMessage += ws2_OnMessage; ;
                //ws2.OnOpen += ws2_OnOpen;
               // ws2.OnMessage += ws2_OnMessageNew;
                ws2.Connect();
            }
            
        }

        private void ws2_OnOpen(object sender, EventArgs e)
        {
             string s = e.ToString();
            //throw new NotImplementedException();
        }

        void ws1_OnMessage(object sender, MessageEventArgs e)
        {

            //richTextBox1.Text += e.Data + Environment.NewLine;
            WebSocket ws = (WebSocket)sender;
            setText("ws1->" + e.Data);
            if(e.Data != "0")
            {
                Timer timer = new Timer();
                timer.Tick += timer1_Tick;
                timer.Interval = 2300;
                timer.Start();
            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            timer.Stop();
            ws1.Send("2");
        }
        void ws2_OnMessageNew(object sender, MessageEventArgs e)
        {
            Console.WriteLine("接收:" + e.Data);
            if (e.Data == "2")
            {
                ws2.Send("2");
                //Timer timer = new Timer();
                //timer.Tick += timer2_Tick;
                //timer.Interval = 1500;
                //timer.Start();
            }
            else if(!e.Data.StartsWith("0"))
            {
                if(e.Data=="40")
                {
                    Console.WriteLine(e.Data);
                    curCommand.SendRequest();
                }
                else if(e.Data.StartsWith("42"))
                {
                    curCommand.ReceiveReponse(WSMessage.GetMessageNo(e.Data),e.Data);
                }
                
            }
        }
        string msg208;
        bool response208 = false;
        void ws2_OnMessage(object sender, MessageEventArgs e)
        {
            WebSocket ws = (WebSocket)sender;
            setText("ws2->" + e.Data);
            setText(Environment.NewLine);

            if (e.Data == "40" )
            {
                setText("ws2<--" + tokenMessage);
                setText(Environment.NewLine);
                ws.Send(tokenMessage);
            }
            else if(e.Data.StartsWith("42") && !initInfo )
            //if(e.IsPing)
            {
                lastMssageID = "42[\"message\",\"74{\\\"last_msgid\\\":-1,\\\"inapp_timestamp\\\":" + GetTimeStamp() + "}\"]"; ;
                ws.Send(lastMssageID);
                setText("ws2<--" + lastMssageID);

                ws.Send(machineInfo);
                setText("ws2<--" + machineInfo);
                ws.Send(lastTimeStamp);
                setText("ws2<--" + lastTimeStamp);

                initInfo = true;
            }
            else if(e.Data=="2")
            {
                ws2.Send("2");
                //Timer timer = new Timer();
                //timer.Tick += timer2_Tick;
                //timer.Interval = 1500;
                //timer.Start();
            }
            else if(e.Data.StartsWith("42[\"message\",\"72"))
            {
                setMsgText("ws2->" + e.Data);
                //ws2.Send(cmd73);
            }
            else if (e.Data.StartsWith("42[\"message\",\"73"))
            {
                setMsgText(e.Data);
            }
            else if (e.Data.StartsWith("42[\"message\",\"208"))
            {
              
                setMsgText("ws2->" + e.Data);
                if (response208)
                {
                    response208 = false;
                    ws2.Send(msg208);
                    setMsgText("ws2<--" + msg208);
                }
                
            }
        }
        void timer2_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            timer.Stop();
            ws2.Send("2");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HtmlHttpHelper hhh = new HtmlHttpHelper();
            string unreadCountQuestStr = "https://seller.xiapi.shopee.cn/webchat/api/v1/messages/unread-count";
            hhh.Authorization ="Bearer " + countToken;
            //hhh.sCookies = "_ga=GA1.2.940952754.1557648319; _gid=GA1.2.620539493.1557648319; _gat=1";
            HttpResult result = hhh.Get(unreadCountQuestStr, "UTF-8");
            string countResult = result.Html;
            int count = int.Parse(countResult.Replace("{", "").Replace("\"", "").Replace("}", "").Split(':')[1]); //{"total_unread_count": 0}

        }

        private void addUserBtn_Click(object sender, EventArgs e)
        {
            string mes72 = "42[\"message\", \"72{\\\"chatid\\\":0,\\\"cursor\\\":0,\\\"userid\\\":%{userid}%,\\\"limit\\\":10}\"]";
            string mes208 = "42[\"message\", \"208{\\\"userid\\\":[%{userid}%]}\"]";

            ws2.Send(mes208.Replace("%{userid}%", store.CustomerInfos[0].to_id.ToString()));
            setMsgText(mes208.Replace("%{userid}%", store.CustomerInfos[0].to_id.ToString()));
            ws2.Send(mes72.Replace("%{userid}%", uerIdTBox.Text));
            setMsgText(mes72.Replace("%{userid}%", uerIdTBox.Text));
            msg208 = mes208.Replace("%{userid}%", uerIdTBox.Text);
            response208 = true;

        }

        private void sendmsgBtn_Click(object sender, EventArgs e)
        {
            ShopeeAPI api = new ShopeeAPI();
            api.SendMessageToUserId(store, long.Parse(uerIdTBox.Text),msgTxtBox.Text);

            string mesg = "42[\"message\", \"59{\\\"text_content\\\":\\\"{\\\"text\\\":\\\""+ msgTxtBox.Text + "\\\"}\\\",\\\"to_userid\\\":"+ uerIdTBox.Text + ",\\\"from_userid\\\":12537983,\\\"type\\\":0,\\\"opt\\\":8,\\\"read\\\":true,\\\"requestid\\\":\\\"ZZZZ-"+Tool.GetTimeStampMilseconds()+"-1\\\"}\"]";
            ws2.Send(mesg);
            setMsgText("ws2<=" + mesg);

        }
    }
}
