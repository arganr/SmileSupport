using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.SPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.SPD
{
    public class ApprovalRecord
    {
        private DataRow myRow;
        private PerjalananDinasEntity mySPD;

        public DataRow Row
        {
            get
            {
                return this.myRow;
            }
        }
        public int DtlAppKey
        {
            get
            {
                return Convert.ToInt32(this.myRow["DtlAppKey"]);
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
        public string Name
        {
            get
            {
                return Convert.ToString(this.myRow["Name"]);
            }
            set
            {
                this.myRow["Name"] = (object)(value);
            }
        }
        public string Jabatan
        {
            get
            {
                return Convert.ToString(this.myRow["Jabatan"]);
            }
            set
            {
                this.myRow["Jabatan"] = (object)(value);
            }
        }
        public string IsDecision
        {
            get
            {
                return Convert.ToString(this.myRow["IsDecision"]);
            }
            set
            {
                this.myRow["IsDecision"] = (object)(value);
            }
        }
        public string DecisionState
        {
            get
            {
                return Convert.ToString(this.myRow["DecisionState"]);
            }
            set
            {
                this.myRow["DecisionState"] = (object)(value);
            }
        }
        public DateTime DecisionDate
        {
            get
            {
                return Convert.ToDateTime(this.myRow["DecisionDate"]);
            }
            set
            {
                this.myRow["DecisionDate"] = Convert.ToDateTime(value);
            }
        }
        public string DecisionNote
        {
            get
            {
                return Convert.ToString(this.myRow["DecisionNote"]);
            }
            set
            {
                this.myRow["DecisionNote"] = (object)(value);
            }
        }
        public string Email
        {
            get
            {
                return Convert.ToString(this.myRow["Email"]);
            }
            set
            {
                this.myRow["Email"] = (object)(value);
            }
        }

        internal ApprovalRecord(DataRow row, PerjalananDinasEntity PerjalananDinasEnity)
        {
            this.myRow = row;
            this.mySPD = PerjalananDinasEnity;
        }
    }
}