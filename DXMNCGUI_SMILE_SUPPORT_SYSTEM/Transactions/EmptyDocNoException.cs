using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions
{
    public class EmptyDocNoException : Exception
    {
        public EmptyDocNoException()
            : base("Empty DocNo  is not allowed.")
        {
        }
    }
    public class EmptyMCodeException : Exception
    {
        public EmptyMCodeException()
            : base("Empty Mitra Code  is not allowed.")
        {
        }
    }
}