using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.EKYC.Models
{
    public class ResultEKYC
    {
        public int timestamp { get; set; }
        public int status { get; set; }
        public ResultError errors { get; set; }
        public ResultData data { get; set; }
        public string trx_id { get; set; }
        public string ref_id { get; set; }

        public class ResultError
        {
            public string identity_photo { get; set; }
        }

        public class ResultData
        {
            public bool name { get; set; }
            public bool birthdate { get; set; }
            public bool birthplace { get; set; }
            public string address { get; set; }
        }
    }
}