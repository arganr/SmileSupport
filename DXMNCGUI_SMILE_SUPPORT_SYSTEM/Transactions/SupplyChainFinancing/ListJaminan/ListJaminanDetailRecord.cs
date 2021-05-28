using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.SupplyChainFinancing.ListJaminan
{
    public class ListJaminanDetailRecord
    {
        private DataRow myRow;
        private ListJaminanEntity myListJaminan;

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
        public string ItemCode
        {
            get
            {
                return Convert.ToString(this.myRow["ItemCode"]);
            }
            set
            {
                this.myRow["ItemCode"] = (object)(value);
            }
        }
        public string ItemDesc
        {
            get
            {
                return Convert.ToString(this.myRow["ItemDesc"]);
            }
            set
            {
                this.myRow["ItemDesc"] = (object)(value);
            }
        }
        public string ItemCategory
        {
            get
            {
                return Convert.ToString(this.myRow["ItemCategory"]);
            }
            set
            {
                this.myRow["ItemCategory"] = (object)(value);
            }
        }
        public string ItemBrand
        {
            get
            {
                return Convert.ToString(this.myRow["ItemBrand"]);
            }
            set
            {
                this.myRow["ItemBrand"] = (object)(value);
            }
        }
        public decimal UOM
        {
            get
            {
                return Convert.ToDecimal(this.myRow["UOM"]);
            }
            set
            {
                this.myRow["UOM"] = (object)(value);
            }
        }
        public decimal Qty
        {
            get
            {
                return Convert.ToDecimal(this.myRow["Qty"]);
            }
            set
            {
                this.myRow["Qty"] = (object)(value);
            }
        }
        public decimal DBP
        {
            get
            {
                return Convert.ToDecimal(this.myRow["DBP"]);
            }
            set
            {
                this.myRow["DBP"] = (object)(value);
            }
        }
        public decimal RBP
        {
            get
            {
                return Convert.ToDecimal(this.myRow["RBP"]);
            }
            set
            {
                this.myRow["RBP"] = (object)(value);
            }
        }
        public decimal DBPSubTotal
        {
            get
            {
                return Convert.ToDecimal(this.myRow["DBPSubTotal"]);
            }
            set
            {
                this.myRow["DBPSubTotal"] = (object)(value);
            }
        }

        internal ListJaminanDetailRecord(DataRow row, ListJaminanEntity ListJaminanEntity)
        {
            this.myRow = row;
            this.myListJaminan = ListJaminanEntity;
        }
    }
}