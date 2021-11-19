using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.SPD
{
    public class DetailRecord
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
        public DateTime StartDate
        {
            get
            {
                return Convert.ToDateTime(this.myRow["StartDate"]);
            }
            set
            {
                this.myRow["StartDate"] = Convert.ToDateTime(value);
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
                this.myRow["EndDate"] = Convert.ToDateTime(value);
            }
        }
        public int JumlahHari
        {
            get
            {
                return Convert.ToInt32(this.myRow["JumlahHari"]);
            }
            set
            {
                this.myRow["JumlahHari"] = Convert.ToInt32(value);
            }
        }
        public string Kendaraan
        {
            get
            {
                return Convert.ToString(this.myRow["Kendaraan"]);
            }
            set
            {
                this.myRow["Kendaraan"] = (object)(value);
            }
        }
        public string Remarks
        {
            get
            {
                return Convert.ToString(this.myRow["Remarks"]);
            }
            set
            {
                this.myRow["Remarks"] = (object)(value);
            }
        }
        internal DetailRecord(DataRow row, PerjalananDinasEntity PerjalananDinasEntity)
        {
            this.myRow = row;
            this.mySPD = PerjalananDinasEntity;
        }

    }
}