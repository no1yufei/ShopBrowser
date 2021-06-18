using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    public class DataListMessageWithCode<T>
    {
        public MessageDataList<T> data;
        public int code;//: 0
        public string message; //: null
        public string user_message;//: "31cbf95f4442d0d21404379c0aaf69f4"

        static  public DataListMessageWithCode<T> FromJson(String str)
        {
            DataListMessageWithCode<T> ret = default(DataListMessageWithCode<T>);
            try
            {
                ret = JsonConvert.DeserializeObject<DataListMessageWithCode<T>>(str);

            }
            catch(Exception xe)
            {
                Console.WriteLine(typeof(T) + "转换Json失败：" +str);
            }
            return ret;
        }
    }
    public class MessageDataList<T>
    {
        public T[] list;
    }

    public class MessageWithCode<T>
    {
        public T data;
        public int code;//: 0
        public string message; //: null
        public string user_message;//: "31cbf95f4442d0d21404379c0aaf69f4"

        static public MessageWithCode<T> FromJson(String str)
        {
            MessageWithCode<T> ret = default(MessageWithCode<T>);
            try
            {
                ret = JsonConvert.DeserializeObject<MessageWithCode<T>>(str);

            }
            catch (Exception xe)
            {
                Console.WriteLine(typeof(T) + "转换Json失败：" + str);
            }
            return ret;
        }
    }
    
    public class ErrorMessage
    {
        public string err_message;//: ""
        public string message ="";//: "error_require_captcha"

        public LoginStatus ErrType
        {
            get {
                LoginStatus type = LoginStatus.UnLog;
                switch (message)
                {
                    case "error_need_otp":
                    case "error_otp":
                        {
                            type = LoginStatus.OTP_Req;
                            break;
                        }
                    case "error_perm":
                        {
                            type = LoginStatus.PasswordErr;
                            break;
                        }
                    case "error_notfound":
                        {
                            type = LoginStatus.UserNameErr;
                            break;
                        }
                    case "error_require_captcha":
                    case "error_captcha":
                        {
                            type = LoginStatus.Captcha_Req;
                            break;
                        }

                }
                return type;
            }
        }
        static public ErrorMessage FromJson(String str)
        {
            ErrorMessage ret = null;
            try
            {
                ret = JsonConvert.DeserializeObject<ErrorMessage>(str);

            }
            catch (Exception xe)
            {
                Console.WriteLine(typeof(ErrorMessage) + "转换Json失败：" + str);
            }
            return ret;
        }

    }
    public enum LoginStatus
    {
       UnLog = -1,
       Log_Succuss = 0,
       OTP_Req =1,
       PasswordErr =2,
       UserNameErr = 3,
       Captcha_Req = 4,

    }
    public class ErrStrMsg
    {
        Dictionary<LoginStatus, string> msgString = new Dictionary<LoginStatus, string>
        {{LoginStatus.UnLog,"未登录" },
            { LoginStatus.Log_Succuss,"登录成功" },
            {LoginStatus.OTP_Req,"短信验证码" },
            {LoginStatus.PasswordErr,"密码错误" },
            {LoginStatus.UserNameErr,"用户名错误" },
            {LoginStatus.Captcha_Req,"图形验证码" }
        };
        public string this[LoginStatus status]
        {
            get { return msgString[status]; }
        }

    }
}
