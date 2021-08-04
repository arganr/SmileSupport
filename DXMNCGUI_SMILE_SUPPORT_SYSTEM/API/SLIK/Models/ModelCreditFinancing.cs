using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SLIK.Models
{
    public class ModelCreditFinancing
    {
        public string REFID { get; set; }
        public string LJK { get; set; }
        public string JENIS { get; set; }
        public string JENIS_PENGGUNAAN { get; set; }
        public double PLAFON { get; set; }
        public double BAKIDEBET { get; set; }
        public double BUNGA { get; set; }
        public DateTime AKADAWAL { get; set; }
        public DateTime TGLAWAL_SISATENOR { get; set; }
        public DateTime JATUHTEMPO { get; set; }
        public double JANGKA { get; set; }
        public double ANGSURAN { get; set; }
        public double SISATENOR { get; set; }
        public string KOLEKTIBILITAS { get; set; }
        public string HISTORY_KOLEKTIBILITAS { get; set; }
        public double TUNGGAKAN_POKOK { get; set; }
        public double TUNGGAKAN_BUNGA { get; set; }
        public double DENDA { get; set; }
        public int FREKUENSI_TUNGGAKAN { get; set; }
        public string AGUNAN_LIST { get; set; }
        public string KONDISI { get; set; }
    }
}