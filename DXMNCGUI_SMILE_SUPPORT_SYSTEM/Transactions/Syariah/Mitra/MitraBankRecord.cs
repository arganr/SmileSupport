using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.Mitra
{
    public class MitraBankRecord
    {
        private DataRow myRow;
        private MitraEntity myMitra;

        public DataRow Row
        {
            get
            {
                return this.myRow;
            }
        }
        public int MKey
        {
            get
            {
                return Convert.ToInt32(this.myRow["MKey"]);
            }
        }
        public int MBankKey
        {
            get
            {
                return Convert.ToInt32(this.myRow["MBankKey"]);
            }
        }
        public int Seq
        {
            get
            {
                return Convert.ToInt32(this.myRow["Seq"]);
            }
        }
        public string BankName
        {
            get
            {
                return System.Convert.ToString(this.myRow["BankName"]);
            }
            set
            {
                this.myRow["BankName"] = (object)(value);
            }
        }
        public string BankBranch
        {
            get
            {
                return Convert.ToString(this.myRow["BankBranch"]);
            }
            set
            {
                this.myRow["BankBranch"] = (object)(value);
            }
        }
        public string BankAccNo
        {
            get
            {
                return Convert.ToString(this.myRow["BankAccNo"]);
            }
            set
            {
                this.myRow["BankAccNo"] = (object)(value);
            }
        }
        public string BankAccName
        {
            get
            {
                return Convert.ToString(this.myRow["BankAccName"]);
            }
            set
            {
                this.myRow["BankAccName"] = (object)(value);
            }
        }

        internal MitraBankRecord(DataRow row, MitraEntity MitraEntity)
        {
            this.myRow = row;
            this.myMitra = MitraEntity;
        }
    }
}