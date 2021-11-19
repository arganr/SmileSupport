using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.SPD
{
    public class PerjalananDinasEntity
    {
        private PerjalananDinasDB mySPDcommand;
        internal DataSet myDataSet;
        private DataRow myRow;
        private DataTable myHeaderTable;
        private DataTable myDetailTable;
        private DataTable myBudgetTable;
        private DataTable myApprovalTable;
        private DXSSAction myAction;
        public string strErrorGenTicket;
        private string myAppNote;

        internal DataRow Row
        {
            get { return myRow; }
        }
        public PerjalananDinasDB SPDcommand
        {
            get
            {
                return this.mySPDcommand;
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
        public DataTable DataTableBudget
        {
            get
            {
                return this.myBudgetTable;
            }
        }
        public DataTable DataTableApproval
        {
            get
            {
                return this.myApprovalTable;
            }
        }
        public DataSet ApplicationDataSet
        {
            get
            {
                return this.myDataSet;
            }
        }
        public string ApprovalNote
        {
            get
            {
                return this.myAppNote;
            }
            set
            {
                this.myAppNote = value;
            }
        }

        public PerjalananDinasEntity(PerjalananDinasDB aPerjalananDinasDB, DataSet ds, DXSSAction action)
        {
            mySPDcommand = aPerjalananDinasDB;
            myDataSet = ds;
            this.myAction = action;
            this.myHeaderTable = this.myDataSet.Tables["Header"];
            this.myDetailTable = this.myDataSet.Tables["Detail"];
            this.myBudgetTable = this.myDataSet.Tables["Budget"];
            this.myApprovalTable = this.myDataSet.Tables["Approval"];
            myRow = myHeaderTable.Rows[0];
            this.myHeaderTable.ColumnChanged += new DataColumnChangeEventHandler(this.myHeaderTable_ColumnChanged);
        }

        private void myHeaderTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {

        }

        public DataTable LoadDocNoFormatTable()
        {
            DataTable mytable = new DataTable();
            mytable = mySPDcommand.DBSetting.GetDataTable("select * from DocNoFormat where DOCTYPE='SPD'", false);
            return mytable;
        }



        public DXSSAction Action
        {
            get
            {
                return this.myAction;
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
                if (flag)
                {
                    this.myRow["MOD_BY"] = (object)userID;
                    this.myRow["MOD_DATE"] = (object)this.mySPDcommand.LocalDBSetting.GetServerTime();
                    if (this.myRow["CRE_BY"].ToString().Length == 0)
                        this.myRow["CRE_BY"] = this.myRow["MOD_BY"];
                    this.myRow.EndEdit();
                    mySPDcommand.SaveEntity(this, saveaction, userID, userName);
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

        internal DataRow[] ValidApprovalRows
        {
            get
            {
                return this.myApprovalTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
            }
        }
        public ApprovalRecord AddApprovals()
        {
            if (this.myAction == DXSSAction.View)
                throw new Exception("Cannot edit read-only Application Lines");
            else
                return this.SPDAddApprovals(SeqUtils.GetLastSeq(this.ValidApprovalRows));
        }
        private ApprovalRecord SPDAddApprovals(int seq)
        {
            DataRow row = this.myApprovalTable.NewRow();
            DateTime myDate = mySPDcommand.LocalDBSetting.GetServerTime();
            string iUserID = mySPDcommand.DBSession.LoginUserID;
            row["DtlAppKey"] = mySPDcommand.DtlAppKeyUniqueKey();
            row["DocKey"] = this.myRow["DocKey"];
            row["Seq"] = (object)seq;
            row["Name"] = DBNull.Value;
            row["Jabatan"] = DBNull.Value;
            row["IsDecision"] = DBNull.Value;
            row["DecisionState"] = DBNull.Value;
            row["DecisionDate"] = DBNull.Value;
            row["DecisionNote"] = DBNull.Value;
            row["Email"] = DBNull.Value;
            row.EndEdit();
            this.myApprovalTable.Rows.Add(row);
            return new ApprovalRecord(row, this);
        }
        public bool IsApprovalModified()
        {
            if (this.myApprovalTable.GetChanges() != null)
                return true;
            else
                return false;
        }



        internal DataRow[] ValidBudgetRows
        {
            get
            {
                return this.myBudgetTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
            }
        }
        public BudgetRecord AddBudgets()
        {
            if (this.myAction == DXSSAction.View)
                throw new Exception("Cannot edit read-only Application Lines");
            else
                return this.SPDAddBudgets(SeqUtils.GetLastSeq(this.ValidBudgetRows));
        }
        private BudgetRecord SPDAddBudgets(int seq)
        {
            DataRow row = this.myBudgetTable.NewRow();
            DateTime myDate = mySPDcommand.LocalDBSetting.GetServerTime();
            string iUserID = mySPDcommand.DBSession.LoginUserID;
            row["DtlAppKey"] = mySPDcommand.DtlBudgetKeyUniqueKey();
            row["DocKey"] = this.myRow["DocKey"];
            row["Seq"] = (object)seq;
            row["TypeSPD"] = DBNull.Value;
            row["TypeBudget"] = DBNull.Value;
            row["BudgetDesc"] = DBNull.Value;
            row["BudgetAmount"] = DBNull.Value;
            row.EndEdit();
            this.myBudgetTable.Rows.Add(row);
            return new BudgetRecord(row, this);
        }
        public bool IsBudgetModified()
        {
            if (this.myBudgetTable.GetChanges() != null)
                return true;
            else
                return false;
        }

        public DetailRecord AddDetailSPD()
        {
            if (this.myAction == DXSSAction.View)
                throw new Exception("Cannot edit read-only Application Lines");
            else
                return this.SPDAddDetail();
        }
        private DetailRecord SPDAddDetail()
        {
            DataRow row = this.myDetailTable.NewRow();
            DateTime myDate = mySPDcommand.LocalDBSetting.GetServerTime();
            string iUserID = mySPDcommand.DBSession.LoginUserID;
            row["DtlKey"] = mySPDcommand.DtlKeyUniqueKey();
            row["DocKey"] = this.myRow["DocKey"];
            row["TypeSPD"] = DBNull.Value;
            row["StartDate"] = DBNull.Value;
            row["EndDate"] = DBNull.Value;
            row["JumlahHari"] = DBNull.Value;
            row["Kendaraan"] = DBNull.Value;
            row["Remarks"] = DBNull.Value;
            row.EndEdit();
            this.myDetailTable.Rows.Add(row);
            return new DetailRecord(row, this);
        }
        public bool IsDetailModified()
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
        public object Status
        {
            get { return myRow["Status"]; }
            set { myRow["Status"] = value; }
        }
        public object NIK
        {
            get { return myRow["NIK"]; }
            set { myRow["NIK"] = value; }
        }
        public object Name
        {
            get { return myRow["Name"]; }
            set { myRow["Name"] = value; }
        }
        public object Dept
        {
            get { return myRow["Dept"]; }
            set { myRow["Dept"] = value; }
        }
        public object Jabatan
        {
            get { return myRow["Jabatan"]; }
            set { myRow["Jabatan"] = value; }
        }
        public object TipeTunjangan
        {
            get { return myRow["TipeTunjangan"]; }
            set { myRow["TipeTunjangan"] = value; }
        }
        public object Tujuan
        {
            get { return myRow["Tujuan"]; }
            set { myRow["Tujuan"] = value; }
        }
        public object CRE_BY
        {
            get { return myRow["CRE_BY"]; }
            set { myRow["CRE_BY"] = value; }
        }
        public object CRE_DATE
        {
            get { return myRow["CRE_DATE"]; }
            set { myRow["CRE_DATE"] = value; }
        }
        public object MOD_BY
        {
            get { return myRow["MOD_BY"]; }
            set { myRow["MOD_BY"] = value; }
        }
        public object MOD_DATE
        {
            get { return myRow["MOD_DATE"]; }
            set { myRow["MOD_DATE"] = value; }
        }

        public DataTable SPDtable
        {
            get { return myDataSet.Tables[0]; }
        }


    }
}