using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.EKYC.Models
{
    public class ModelEKYC
    {
        public string trx_id { get; set; }
        public string nik { get; set; }
        public string name { get; set; }
        public string birthdate { get; set; }
        public string birthplace { get; set; }
        public string address { get; set; }
        public string identity_photo { get; set; }
    }
}