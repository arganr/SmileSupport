using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Insurance
{
    public class PolisAsuransiFAEntity
    {
        private PolisAsuransiFADB myPolisAsuransiFAcommand;
        internal DataSet myDataSet;
        private DataRow myRow;
        private DataTable myHeaderTable;
        private DataTable myDetailTable;
        private DXSSAction myAction;
        private DXSSType myDocType;
        public string strErrorGenPolisAsuransiFA;

        internal DataRow Row
        {
            get { return myRow; }
        }
        public PolisAsuransiFADB PolisAsuransiFAcommand
        {
            get
            {
                return this.myPolisAsuransiFAcommand;
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
        public DataSet PolisAsuransiFADataSet
        {
            get
            {
                return this.myDataSet;
            }
        }

        public PolisAsuransiFAEntity(PolisAsuransiFADB aPolisAsuransiFA, DataSet ds, DXSSAction action)
        {
            myPolisAsuransiFAcommand = aPolisAsuransiFA;
            myDataSet = ds;
            this.myAction = action;
            this.myHeaderTable = this.myDataSet.Tables["Header"];
            this.myDetailTable = this.myDataSet.Tables["Detail"];
            myRow = myHeaderTable.Rows[0];
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
                if (flag)
                {
                    this.myRow["LastModifiedBy"] = (object)userID;
                    this.myRow["LastModifiedDateTime"] = (object)this.myPolisAsuransiFAcommand.LocalDBSetting.GetServerTime();
                    if (this.myRow["CreatedBy"].ToString().Length == 0)
                        this.myRow["CreatedBy"] = this.myRow["LastModifiedBy"];
                    this.myRow.EndEdit();
                    myPolisAsuransiFAcommand.SaveEntity(this, saveaction, userID, userName);
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
            return myPolisAsuransiFAcommand.DBReg.IncOne((Controllers.Registry.IRegistryID)new PolisAsuransiFADtlKey());
        }
        public PolisAsuransiFADetailRecord AddDetail()
        {
            if (this.myAction == DXSSAction.View)
                throw new Exception("Cannot edit read-only Application Lines");
            else
                return this.InternalAddDetail(SeqUtils.GetLastSeq(this.ValidDetailLinesRows));
        }
        private PolisAsuransiFADetailRecord InternalAddDetail(int seq)
        {
            DataRow row = this.myDetailTable.NewRow();
            DateTime myDate = myPolisAsuransiFAcommand.LocalDBSetting.GetServerTime();
            string iUserID = myPolisAsuransiFAcommand.DBSession.LoginUserID;
            row["DtlKey"] = (object)myPolisAsuransiFAcommand.DtlKeyUniqueKey();
            row["DocKey"] = this.myRow["DocKey"];
            row["Seq"] = DBNull.Value;
            row["Maskapai"] = DBNull.Value;
            row["NoPolis"] = DBNull.Value;
            row["StartDate"] = myDate;
            row["StartDate"] = myDate;
            row["Coverage"] = DBNull.Value;
            row.EndEdit();
            this.myDetailTable.Rows.Add(row);
            return new PolisAsuransiFADetailRecord(row, this);
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
        public object AssetDesc
        {
            get { return myRow["AssetDesc"]; }
            set { myRow["AssetDesc"] = value; }
        }
        public object NoPolisi
        {
            get { return myRow["NoPolisi"]; }
            set { myRow["NoPolisi"] = value; }
        }
        public object NoRangka
        {
            get { return myRow["NoRangka"]; }
            set { myRow["NoRangka"] = value; }
        }
        public object NoMesin
        {
            get { return myRow["NoMesin"]; }
            set { myRow["NoMesin"] = value; }
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
        public object LastModifiedDateTime
        {
            get { return myRow["LastModifiedDateTime"]; }
            set { myRow["LastModifiedDateTime"] = value; }
        }

        public DataTable PolisAsuransiFATable
        {
            get { return myDataSet.Tables[0]; }
        }
    }
}