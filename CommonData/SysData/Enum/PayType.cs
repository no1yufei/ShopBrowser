using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData.SysData.Enum
{
    public  class PayTypeInt
    {
        /// <summary>
        /// 充值。
        /// </summary>
        public static int ReCharge = 0;
        /// <summary>
        /// 处理费
        /// </summary>
        public static int HandleFee = 1;
        /// <summary>
        /// 费率调整
        /// </summary>
        public static int RateAdjust = 2;
        /// <summary>
        /// 账户余额调整
        /// </summary>
        public static int AmountAdjust = 3;
    }
    public class PayStatusInt
    {
        /// <summary>
        /// 预充值，等待对账
        /// </summary>
        public static int Precharge = 0;
        /// <summary>
        /// 充值确认
        /// </summary>
        public static int Comfirmed = 1;
        /// <summary>
        /// 退回
        /// </summary>
        public static int Revoked = 2;
    }
}
