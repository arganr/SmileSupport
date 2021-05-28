using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.UpdateSLIK
{
    public class UpdateSLIKEntity
    {
        private UpdateSLIKDB myUpdateSLIKcommand;
        internal DataSet myDataSet;
        private DataRow myRow;
        private DataTable myHeaderTable;
        private DataTable myDetailTable;
        private DXSSAction myAction;
        private DXSSType myDocType;
        public string strErrorGenUpdateSLIK;
        internal DataRow Row
        {
            get { return myRow; }
        }
        public UpdateSLIKDB UpdateSLIKcommand
        {
            get
            {
                return this.myUpdateSLIKcommand;
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
        public DataSet UpdateSLIKDataSet
        {
            get
            {
                return this.myDataSet;
            }
        }

        public UpdateSLIKEntity(UpdateSLIKDB aUpdateSLIK, DataSet ds, DXSSAction action)
        {
            myUpdateSLIKcommand = aUpdateSLIK;
            myDataSet = ds;
            this.myAction = action;
            this.myHeaderTable = this.myDataSet.Tables["Header"];
            this.myDetailTable = this.myDataSet.Tables["Detail"];
            myRow = myHeaderTable.Rows[0];
            this.myHeaderTable.ColumnChanged += new DataColumnChangeEventHandler(this.myHeaderTable_ColumnChanged);
            this.myDetailTable.RowChanged += new DataRowChangeEventHandler(this.myDetailTable_RowChanged);
            this.myDetailTable.ColumnChanged += new DataColumnChangeEventHandler(this.DetailDataColumnChangeEventHandler);
            this.myDetailTable.RowDeleting += new DataRowChangeEventHandler(this.myDetailTable_RowDeleting);
            this.myDetailTable.RowDeleted += new DataRowChangeEventHandler(this.myDetailTableRowDeletedEventHandler);
            this.myDetailTable.ColumnChanging += new DataColumnChangeEventHandler(this.myDetailTable_ColumnChanging);
        }

        private void myHeaderTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {

        }
        private void myDetailTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {

        }
        private void myDetailTable_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {

        }
        private void myDetailTable_RowDeleting(object sender, DataRowChangeEventArgs e)
        {

        }
        private void myDetailTableRowDeletedEventHandler(object sender, DataRowChangeEventArgs e)
        {
        }
        private void DetailDataColumnChangeEventHandler(object sender, DataColumnChangeEventArgs e)
        {
        }

        public DataTable LoadMitraCodeFormatTable()
        {
            DataTable mytable = new DataTable();
            mytable = myUpdateSLIKcommand.LocalDBSetting.GetDataTable("select * from DocNoFormat where DOCTYPE='SLK'", false);
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
                if (flag)
                {
                    if (this.myRow["CreatedBy"].ToString().Length == 0)
                        this.myRow["CreatedBy"] = userID;
                    this.myRow.EndEdit();
                    myUpdateSLIKcommand.SaveEntity(this, saveaction, userID, userName);
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
            return myUpdateSLIKcommand.DBReg.IncOne((Controllers.Registry.IRegistryID)new UpdateSLIKDtlKey());
        }
        public UpdateSLIKDetailRecord AddDetail()
        {
            if (this.myAction == DXSSAction.View)
                throw new Exception("Cannot edit read-only Application Lines");
            else
                return this.InternalUpdateSLIKDetailRecord(SeqUtils.GetLastSeq(this.ValidDetailLinesRows));
        }
        private UpdateSLIKDetailRecord InternalUpdateSLIKDetailRecord(int seq)
        {
            DataRow row = this.myDetailTable.NewRow();
            DateTime myDate = myUpdateSLIKcommand.LocalDBSetting.GetServerTime();
            string iUserID = myUpdateSLIKcommand.DBSession.LoginUserID;
            row["DtlKey"] = (object)myUpdateSLIKcommand.DtlKeyUniqueKey();
            row["DocKey"] = this.myRow["DocKey"];
            row["Seq"] = DBNull.Value;
            row["JenisPembiayaan"] = DBNull.Value;
            row["PerusahaanPembiayaan"] = DBNull.Value;
            row["AtasNama"] = DBNull.Value;
            row["Plafon"] = 0;
            row["BakiDebet"] = 0;
            row["Bunga"] = 0;
            row["TglAkadAwal"] = myDate;
            row["TglAwalSisaTenor"] = myDate;
            row["TglJatuhTempo"] = myDate;
            row["Angsuran"] = 0;
            row["Kolektibilitas"] = DBNull.Value;
            row["HistoryKolek"] = DBNull.Value;
            row["AktualOverDue"] = DBNull.Value;
            row.EndEdit();
            this.myDetailTable.Rows.Add(row);
            return new UpdateSLIKDetailRecord(row, this);
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
        public object Remark
        {
            get { return myRow["Remark"]; }
            set { myRow["Remark"] = value; }
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
        public object Cancel
        {
            get { return myRow["Cancel"]; }
            set { myRow["Cancel"] = value; }
        }
        public object CancelBy
        {
            get { return myRow["CancelBy"]; }
            set { myRow["CancelBy"] = value; }
        }
        public object CancelDateTime
        {
            get { return myRow["CancelDateTime"]; }
            set { myRow["CancelDateTime"] = value; }
        }
        public object SLIKAvailable
        {
            get { return myRow["SLIKAvailable"]; }
            set { myRow["SLIKAvailable"] = value; }
        }
        public object CAChecking
        {
            get { return myRow["CAChecking"]; }
            set { myRow["CAChecking"] = value; }
        }
        public object Debitur
        {
            get { return myRow["Debitur"]; }
            set { myRow["Debitur"] = value; }
        }
        public DataTable UpdateSLIKTable
        {
            get { return myDataSet.Tables[0]; }
        }
    }
}