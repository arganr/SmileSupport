using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.PDSB.Models
{
    public class ResultCIF
    {
        public string ErrorMessage { get; set; }
        public ResponseDocs_CIF ResponseDocs { get; set; }
        public string ACCESSID { get; set; }
        public string ResponseCode { get; set; }
    }

    public class ResponseDocs_CIF
    {
        [DefaultValue("")]
        public string response_ref_number { get; set; }
        [DefaultValue("")]
        public string nomor_nasabah { get; set; }
    }
}