using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData.SysData.Enum
{
    public enum UserStatus
    {
        /// <summary>
        /// 未指定
        /// </summary>
        All = -1,
        /// <summary>
        /// 已禁用
        /// </summary>
        Forbidden = 0,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 等待激活。
        /// </summary>
        AvactivePending=2,
        /// <summary>
        /// 欠费
        /// </summary>
        Arrears=3,
    }
}
