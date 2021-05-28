using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections.WaiveLateCharges
{
    public class WaiveLateChargesCodeException : Exception
    {
        public WaiveLateChargesCodeException()
            : base("Empty DocNo  is not allowed.")
        {
        }
    }
}