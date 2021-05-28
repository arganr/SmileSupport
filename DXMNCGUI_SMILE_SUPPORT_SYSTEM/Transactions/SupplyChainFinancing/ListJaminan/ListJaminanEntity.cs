using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.SupplyChainFinancing.ListJaminan
{
    public class ListJaminanEntity
    {
        private ListJaminanDB myListJaminancommand;
        internal DataSet myDataSet;
        private DataRow myRow;
        private DataTable myHeaderTable;
        private DataTable myDetailTable;
        private DXSSAction myAction;
        private DXSSType myDocType;
        public string strErrorGenListJaminan;
        internal DataRow Row
        {
            get { return myRow; }
        }
        public ListJaminanDB ListJaminancommand
        {
            get
            {
                return this.myListJaminancommand;
            }
        }
        public DataTable DataTableHeader
        {
            get
            {
                return this.myHeaderTable;
            }
        }
        public DataTable DataTableDetail
        {
            get
            {
                return this.myDetailTable;
            }
        }
        public DataSet ListJaminanDataSet
        {
            get
            {
                return this.myDataSet;
            }
        }

        public ListJaminanEntity(ListJaminanDB aListJaminan, DataSet ds, DXSSAction action)
        {
            myListJaminancommand = aListJaminan;
            myDataSet = ds;
            this.myAction = action;
            this.myHeaderTable = this.myDataSet.Tables["Header"];
            this.myDetailTable = this.myDataSet.Tables["Detail"];
            myRow = myHeaderTable.Rows[0];
        }

        public DataTable LoadListJaminanCodeFormatTable()
        {
            DataTable mytable = new DataTable();
            mytable = myListJaminancommand.LocalDBSetting.GetDataTable("select * from DocNoFormat where DOCTYPE='LJ'", false);
            return mytable;
        }

        public DXSSAction Action
        {
            get
            {
                return this.myAction;
            }
        }
        public DXSSType DocType
        {
            get
            {
                return this.myDocType;
            }
        }

        internal DataRow[] ValidDetailLinesRows
        {
            get
            {
                return this.myDetailTable.Select("", "DtlKey", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
            }
        }
        public void Save(string userID, string userName, SaveAction saveaction)
        {
            if (saveaction == SaveAction.Cancel)
            {
                this.myAction = DXSSAction.Cancel;
            }
            {
                bool flag = this.myRow.RowState != DataRowState.Unchanged;
                foreach (DataRow dataRow in this.ValidDetailLinesRows)
                {
                    if (!flag && dataRow.RowState != DataRowState.Unchanged)
                        flag = true;
                }
                if (!flag && this.myDetailTable.Select("", "Seq", DataViewRowState.Deleted).Length > 0)
                    flag = true;
                if (flag)
                {
                    this.myRow["LastModifiedBy"] = (object)userID;
                    this.myRow["LastModifiedTime"] = (object)this.myListJaminancommand.LocalDBSetting.GetServerTime();
                    if (this.myRow["CreatedBy"].ToString().Length == 0)
                        this.myRow["CreatedBy"] = this.myRow["LastModifiedBy"];
                    this.myRow.EndEdit();
                    myListJaminancommand.SaveEntity(this, saveaction, userID, userName);
                }
                this.myAction = DXSSAction.View;
            }
        }
        public void Edit()
        {
            if (this.myAction == DXSSAction.View)
            {
                this.myAction = DXSSAction.Edit;
            }
        }
        public int LinesCount
        {
            get
            {
                return this.ValidDetailLinesRows.Length;
            }
        }
        public bool DeleteDetailLines(int index)
        {
            if (this.myAction == DXSSAction.View)
            {
                throw new Exception("Cannot Delete read-only Application");
            }
            else
            {
                DataRow[] validRows = this.ValidDetailLinesRows;
                if (index >= 0 && index < validRows.Length)
                {
                    validRows[index].Delete();
                    return true;
                }
                else
                    return false;
            }
        }
        public void ClearLines()
        {
            if (this.myAction == DXSSAction.View)
            {
                throw new Exception("Cannot edit read-only Application");
            }
            else
            {
                foreach (DataRow dataRow in this.ValidDetailLinesRows)
                    dataRow.Delete();
            }
        }
        public long DtlKeyUniqueKey()
        {
            return myListJaminancommand.DBReg.IncOne((Controllers.Registry.IRegistryID)new ListJaminanDetailDtlKey());
        }
        public ListJaminanDetailRecord AddDetail()
        {
            if (this.myAction == DXSSAction.View)
                throw new Exception("Cannot edit read-only Application Lines");
            else
                return this.InternalAddDetail(SeqUtils.GetLastSeq(this.ValidDetailLinesRows));
        }
        private ListJaminanDetailRecord InternalAddDetail(int seq)
        {
            DataRow row = this.myDetailTable.NewRow();
            DateTime myDate = myListJaminancommand.LocalDBSetting.GetServerTime();
            string iUserID = myListJaminancommand.DBSession.LoginUserID;
            row["DtlKey"] = (object)myListJaminancommand.DetailUniqueKey();
            row["DocKey"] = this.myRow["DocKey"];
            row["ItemCode"] = DBNull.Value;
            row["ItemDesc"] = DBNull.Value;
            row["ItemCategory"] = DBNull.Value;
            row["ItemBrand"] = DBNull.Value;
            row["UOM"] = DBNull.Value;
            row["Qty"] = 0;
            row["DBP"] = 0;
            row["RBP"] = 0;
            row["DBPSubTotal"] = 0;
            row.EndEdit();
            this.myDetailTable.Rows.Add(row);
            return new ListJaminanDetailRecord(row, this);
        }
        public bool IsModified()
        {
            if (this.myDetailTable.GetChanges() != null)
                return true;
            else
                return false;
        }

        public object DocKey
        {
            get { return myRow["DocKey"]; }
            set { myRow["DocKey"] = value; }
        }
        public object DocNo
        {
            get { return myRow["DocNo"]; }
            set { myRow["DocNo"] = value; }
        }
        public object DocDate
        {
            get { return myRow["DocDate"]; }
            set { myRow["DocDate"] = value; }
        }
        public object RefNo
        {
            get { return myRow["RefNo"]; }
            set { myRow["RefNo"] = value; }
        }
        public object AssetDesc
        {
            get { return myRow["AssetDesc"]; }
            set { myRow["AssetDesc"] = value; }
        }
        public object TotalJaminan
        {
            get { return myRow["TotalJaminan"]; }
            set { myRow["TotalJaminan"] = value; }
        }
        public object TotalPembiayaan
        {
            get { return myRow["TotalPembiayaan"]; }
            set { myRow["TotalPembiayaan"] = value; }
        }
        public object IsPost
        {
            get { return myRow["IsPost"]; }
            set { myRow["IsPost"] = value; }
        }
        public object CreatedBy
        {
            get { return myRow["CreatedBy"]; }
            set { myRow["CreatedBy"] = value; }
        }
        public object CreatedDateTime
        {
            get { return myRow["CreatedDateTime"]; }
            set { myRow["CreatedDateTime"] = value; }
        }
        public object LastModifiedBy
        {
            get { return myRow["LastModifiedBy"]; }
            set { myRow["LastModifiedBy"] = value; }
        }
        public object LastModifiedTime
        {
            get { return myRow["LastModifiedTime"]; }
            set { myRow["LastModifiedTime"] = value; }
        }
        public object Debitur
        {
            get { return myRow["Debitur"]; }
            set { myRow["Debitur"] = value; }
        }

        public DataTable ListJaminanTable
        {
            get { return myDataSet.Tables[0]; }
        }
    }
}