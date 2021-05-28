using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.UpdateSLIK
{
    public class UpdateSLIKDetailRecord
    {
        private DataRow myRow;
        private UpdateSLIKEntity myUpdateSLIK;

        public DataRow Row
        {
            get
            {
                return this.myRow;
            }
        }
        public int DocKey
        {
            get
            {
                return Convert.ToInt32(this.myRow["DocKey"]);
            }
        }
        public int DtlKey
        {
            get
            {
                return Convert.ToInt32(this.myRow["DtlKey"]);
            }
        }
        public int Seq
        {
            get
            {
                return Convert.ToInt32(this.myRow["Seq"]);
            }
        }
        public string JenisPembiayaan
        {
            get
            {
                return System.Convert.ToString(this.myRow["JenisPembiayaan"]);
            }
            set
            {
                this.myRow["JenisPembiayaan"] = (object)(value);
            }
        }
        public string PerusahaanPembiayaan
        {
            get
            {
                return System.Convert.ToString(this.myRow["PerusahaanPembiayaan"]);
            }
            set
            {
                this.myRow["PerusahaanPembiayaan"] = (object)(value);
            }
        }
        public string AtasNama
        {
            get
            {
                return Convert.ToString(this.myRow["AtasNama"]);
            }
            set
            {
                this.myRow["AtasNama"] = (object)(value);
            }
        }
        public decimal Plafon
        {
            get
            {
                return Convert.ToDecimal(this.myRow["Plafon"]);
            }
            set
            {
                this.myRow["Plafon"] = (object)(value);
            }
        }
        public decimal BakiDebet
        {
            get
            {
                return Convert.ToDecimal(this.myRow["BakiDebet"]);
            }
            set
            {
                this.myRow["BakiDebet"] = (object)(value);
            }
        }
        public decimal Bunga
        {
            get
            {
                return Convert.ToDecimal(this.myRow["Bunga"]);
            }
            set
            {
                this.myRow["Bunga"] = (object)(value);
            }
        }
        public DateTime TglAkadAwal
        {
            get
            {
                return Convert.ToDateTime(this.myRow["TglAkadAwal"]);
            }
            set
            {
                this.myRow["TglAkadAwal"] = (object)(value);
            }
        }
        public DateTime TglAwalSisaTenor
        {
            get
            {
                return Convert.ToDateTime(this.myRow["TglAwalSisaTenor"]);
            }
            set
            {
                this.myRow["TglAwalSisaTenor"] = (object)(value);
            }
        }
        public DateTime TglJatuhTempo
        {
            get
            {
                return Convert.ToDateTime(this.myRow["TglJatuhTempo"]);
            }
            set
            {
                this.myRow["TglJatuhTempo"] = (object)(value);
            }
        }
        public decimal Angsuran
        {
            get
            {
                return Convert.ToDecimal(this.myRow["Angsuran"]);
            }
            set
            {
                this.myRow["Angsuran"] = (object)(value);
            }
        }
        public string Kolektibilitas
        {
            get
            {
                return Convert.ToString(this.myRow["Kolektibilitas"]);
            }
            set
            {
                this.myRow["Kolektibilitas"] = (object)(value);
            }
        }
        public string HistoryKolek
        {
            get
            {
                return Convert.ToString(this.myRow["HistoryKolek"]);
            }
            set
            {
                this.myRow["HistoryKolek"] = (object)(value);
            }
        }
        public string AktualOverDue
        {
            get
            {
                return Convert.ToString(this.myRow["AktualOverDue"]);
            }
            set
            {
                this.myRow["AktualOverDue"] = (object)(value);
            }
        }

        internal UpdateSLIKDetailRecord(DataRow row, UpdateSLIKEntity UpdateSLIKEntity)
        {
            this.myRow = row;
            this.myUpdateSLIK = UpdateSLIKEntity;
        }
    }
}