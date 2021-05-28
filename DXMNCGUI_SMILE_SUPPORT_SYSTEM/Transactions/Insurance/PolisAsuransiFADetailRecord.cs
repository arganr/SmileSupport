using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Insurance
{
    public class PolisAsuransiFADetailRecord
    {
        private DataRow myRow;
        private PolisAsuransiFAEntity myPolisAsuransiFA;

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
        public string Maskapai
        {
            get
            {
                return System.Convert.ToString(this.myRow["Maskapai"]);
            }
            set
            {
                this.myRow["Maskapai"] = (object)(value);
            }
        }
        public string NoPolis
        {
            get
            {
                return System.Convert.ToString(this.myRow["NoPolis"]);
            }
            set
            {
                this.myRow["NoPolis"] = (object)(value);
            }
        }
        public DateTime StartDate
        {
            get
            {
                return Convert.ToDateTime(this.myRow["StartDate"]);
            }
            set
            {
                this.myRow["StartDate"] = (object)(value);
            }
        }
        public DateTime EndDate
        {
            get
            {
                return Convert.ToDateTime(this.myRow["EndDate"]);
            }
            set
            {
                this.myRow["EndDate"] = (object)(value);
            }
        }
        public string Coverage
        {
            get
            {
                return Convert.ToString(this.myRow["Coverage"]);
            }
            set
            {
                this.myRow["Coverage"] = (object)(value);
            }
        }

        internal PolisAsuransiFADetailRecord(DataRow row, PolisAsuransiFAEntity PolisAsuransiFAEntity)
        {
            this.myRow = row;
            this.myPolisAsuransiFA = PolisAsuransiFAEntity;
        }
    }
}