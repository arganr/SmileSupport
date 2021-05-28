using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess
{
    public class ApplicationLinesRecord
    {
        private DataRow myRow;
        private ApplicationEntity myApplication;

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
        public int Seq
        {
            get
            {
                return Convert.ToInt32(this.myRow["Seq"]);
            }
        }
        public string AssetDesc
        {
            get
            {
                return System.Convert.ToString(this.myRow["AssetDesc"]);
            }
            set
            {
                this.myRow["AssetDesc"] = (object)(value);
            }
        }
        public decimal OTR
        {
            get
            {
                return Convert.ToDecimal(this.myRow["OTR"]);
            }
            set
            {
                this.myRow["OTR"] = Convert.ToDecimal(value);
            }
        }
        public decimal YEAR
        {
            get
            {
                return Convert.ToDecimal(this.myRow["YEAR"]);
            }
            set
            {
                this.myRow["YEAR"] = Convert.ToDecimal(value);
            }
        }
        public decimal COUNT
        {
            get
            {
                return Convert.ToDecimal(this.myRow["COUNT"]);
            }
            set
            {
                this.myRow["COUNT"] = Convert.ToDecimal(value);
            }
        }
        public string Condition
        {
            get
            {
                return Convert.ToString(this.myRow["Condition"]);
            }
            set
            {
                this.myRow["Condition"] = (object)(value);
            }
        }
        public string AssetTypeDetail
        {
            get
            {
                return Convert.ToString(this.myRow["AssetTypeDetail"]);
            }
            set
            {
                this.myRow["AssetTypeDetail"] = (object)(value);
            }
        }
        internal ApplicationLinesRecord(DataRow row, ApplicationEntity ApplicationEntity)
        {
            this.myRow = row;
            this.myApplication = ApplicationEntity;
        }
    }
}