using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.SysData
{
    public enum PayType
    {
        ReCharge = 0,
        HandleFee = 1,
        RateAdjust = 2,
        AmountAdjust = 3
    }
    public enum PayStatus
    {
        Precharge = 0,
        Comfirmed = 1,
        Revoked = 2,
    }
}
