using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.PDSB.Models
{
    public class ResultLogin
    {
        public string token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}