using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.SPD
{
    public class BudgetRecord
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
        public int DtlKey
        {
            get
            {
                return Convert.ToInt32(this.myRow["DtlKey"]);
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
        public string TypeSPD
        {
            get
            {
                return Convert.ToString(this.myRow["TypeSPD"]);
            }
            set
            {
                this.myRow["TypeSPD"] = (object)(value);
            }
        }
        public string TypeBudget
        {
            get
            {
                return Convert.ToString(this.myRow["TypeBudget"]);
            }
            set
            {
                this.myRow["TypeBudget"] = (object)(value);
            }
        }
        public string BudgetDesc
        {
            get
            {
                return Convert.ToString(this.myRow["BudgetDesc"]);
            }
            set
            {
                this.myRow["BudgetDesc"] = (object)(value);
            }
        }
        public decimal BudgetAmount
        {
            get
            {
                return Convert.ToDecimal(this.myRow["BudgetAmount"]);
            }
            set
            {
                this.myRow["BudgetAmount"] = (object)(value);
            }
        }

        internal BudgetRecord(DataRow row, PerjalananDinasEntity PerjalananDinasEnity)
        {
            this.myRow = row;
            this.mySPD = PerjalananDinasEnity;
        }
    }
}