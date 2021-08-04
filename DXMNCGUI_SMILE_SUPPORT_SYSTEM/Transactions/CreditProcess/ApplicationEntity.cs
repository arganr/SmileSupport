using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess
{
    public class ApplicationEntity
    {
        private ApplicationDB myApplicationcommand;
        internal DataSet myDataSet;
        private DataRow myRow;
        private DataTable myHeaderTable;
        private DataTable myDetailTable;
        private DXSSAction myAction;
        private DXSSType myDocType;
        public string strErrorGenTicket;
        private string myAppNote;

        internal DataRow Row
        {
            get { return myRow; }
        }
        public ApplicationDB Applicationcommand
        {
            get
            {
                return this.myApplicationcommand;
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
        public ApplicationEntity(ApplicationDB aApplication, DataSet ds, DXSSAction action)
        {
            myApplicationcommand = aApplication;
            myDataSet = ds;
            this.myAction = action;
            this.myHeaderTable = this.myDataSet.Tables["Header"];
            this.myDetailTable = this.myDataSet.Tables["Lines"];
            myRow = myHeaderTable.Rows[0];
            this.myHeaderTable.ColumnChanged += new DataColumnChangeEventHandler(this.myHeaderTable_ColumnChanged);
            this.myDetailTable.RowChanged += new DataRowChangeEventHandler(this.myDetailTable_RowChanged);
            this.myDetailTable.ColumnChanged += new DataColumnChangeEventHandler(this.DetailDataColumnChangeEventHandler);
            this.myDetailTable.RowDeleting += new DataRowChangeEventHandler(this.myDetailTable_RowDeleting);
            this.myDetailTable.RowDeleted += new DataRowChangeEventHandler(this.DetailDataRowDeletedEventHandler);
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
        private void DetailDataRowDeletedEventHandler(object sender, DataRowChangeEventArgs e)
        {
        }
        private void DetailDataColumnChangeEventHandler(object sender, DataColumnChangeEventArgs e)
        {
        }

        public DataTable LoadDocNoFormatTable()
        {
            DataTable mytable = new DataTable();
            mytable = myApplicationcommand.LocalDBSetting.GetDataTable("select * from DocNoFormat where DOCTYPE='CD'", false);
            return mytable;
        }
        public DataTable LoadCategoryTable()
        {
            DataTable mytable = new DataTable();
            mytable = myApplicationcommand.LocalDBSetting.GetDataTable("select Category from [dbo].[Category] where DocType='CD'", false);
            return mytable;
        }
        public DataTable LoadCategorySubTable(string category)
        {
            DataTable mytable = new DataTable();
            string strQuery = "select Category, SubCategory from CategorySub where Category=? order by SubCategory";
            mytable = myApplicationcommand.LocalDBSetting.GetDataTable(strQuery, false, category);
            return mytable;
        }
        public DataTable LoadApproverTable(string sID)
        {
            object obj = null;
            string sFirstUpline = "", sSecondUpline = "", sThirdUpline = "";
            string strQuery = "select HEAD from Users where NIK=?";
            DataTable mytable = new DataTable();

            obj = myApplicationcommand.LocalDBSetting.ExecuteScalar(strQuery, sID);
            if (obj != null && obj != DBNull.Value)
            {
                sFirstUpline = obj.ToString();
            }
            obj = myApplicationcommand.LocalDBSetting.ExecuteScalar(strQuery, sFirstUpline);
            if (obj != null && obj != DBNull.Value)
            {
                sSecondUpline = obj.ToString();
            }
            obj = myApplicationcommand.LocalDBSetting.ExecuteScalar(strQuery, sSecondUpline);
            if (obj != null && obj != DBNull.Value)
            {
                sThirdUpline = obj.ToString();
            }

            strQuery = "select HEAD, HEADNAME from Users where NIK=? \n";
            strQuery += "UNION ALL \n";
            strQuery += "select HEAD, HEADNAME from Users where NIK=? \n";
            strQuery += "UNION ALL \n";
            strQuery += "select HEAD, HEADNAME from Users where NIK=? \n";
            strQuery += "UNION ALL \n";
            strQuery += "select HEAD, HEADNAME from Users where NIK=? \n";
            mytable = myApplicationcommand.LocalDBSetting.GetDataTable(strQuery, false, sID, sFirstUpline, sSecondUpline, sThirdUpline);
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
                return this.myDetailTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
            }
        }
        public void Save(string userID, string userName, string strDocName, SaveAction saveaction, string sCurrentStat)
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
                    this.myRow["LastModifiedTime"] = (object)this.myApplicationcommand.LocalDBSetting.GetServerTime();
                    if (this.myRow["CreatedBy"].ToString().Length == 0)
                        this.myRow["CreatedBy"] = this.myRow["LastModifiedUser"];
                    this.myRow.EndEdit();
                    myApplicationcommand.SaveEntity(this, strDocName, saveaction, sCurrentStat, userID, userName);
                }
                this.myAction = DXSSAction.View;
            }
        }
        public void SaveComment(string userName, string sComment, SaveAction saveaction, DateTime distDate)
        {
            myApplicationcommand.SaveCommentEntity(this, saveaction, userName, sComment, distDate);
            this.myAction = DXSSAction.View;
        }
        public void SaveAssign(string userName, string sAssignTo, SaveAction saveaction)
        {
            myApplicationcommand.SaveAssignEntity(this, saveaction, userName, sAssignTo);
            this.myAction = DXSSAction.View;
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
            return myApplicationcommand.DBReg.IncOne((DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry.IRegistryID)new ApplicationDetailLinesDtlKey());
        }
        public ApplicationLinesRecord AddLines()
        {
            if (this.myAction == DXSSAction.View)
                throw new Exception("Cannot edit read-only Application Lines");
            else
                return this.InternalAddLines(SeqUtils.GetLastSeq(this.ValidDetailLinesRows));
        }
        private ApplicationLinesRecord InternalAddLines(int seq)
        {
            DataRow row = this.myDetailTable.NewRow();
            DateTime myDate = myApplicationcommand.LocalDBSetting.GetServerTime();
            string iUserID = myApplicationcommand.DBSession.LoginUserID;
            row["DtlKey"] = (object)myApplicationcommand.DtlKeyUniqueKey();
            row["DocKey"] = this.myRow["DocKey"];
            row["Seq"] = (object)seq;
            row["Condition"] = DBNull.Value;
            row["ItemDescription"] = DBNull.Value;
            row["Year"] = myDate.Year;
            row["UnitPrice"] = 0;
            row["Qty"] = 0;
            row["SubTotal"] = 0;
            row.EndEdit();
            this.myDetailTable.Rows.Add(row);
            return new ApplicationLinesRecord(row, this);
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
        public object DocumentType
        {
            get { return myRow["DocumentType"]; }
            set { myRow["DocumentType"] = value; }
        }
        public object Note
        {
            get { return myRow["Note"]; }
            set { myRow["Note"] = value; }
        }
        public object Remark1
        {
            get { return myRow["Remark1"]; }
            set { myRow["Remark1"] = value; }
        }
        public object Remark2
        {
            get { return myRow["Remark2"]; }
            set { myRow["Remark2"] = value; }
        }
        public object Remark3
        {
            get { return myRow["Remark3"]; }
            set { myRow["Remark3"] = value; }
        }
        public object Remark4
        {
            get { return myRow["Remark4"]; }
            set { myRow["Remark4"] = value; }
        }
        public object Branch
        {
            get { return myRow["Branch"]; }
            set { myRow["Branch"] = value; }
        }
        public object ObjectPembiayaan
        {
            get { return myRow["ObjectPembiayaan"]; }
            set { myRow["ObjectPembiayaan"] = value; }
        }
        public object Facility
        {
            get { return myRow["Facility"]; }
            set { myRow["Facility"] = value; }
        }
        public object JenisPengikatan
        {
            get { return myRow["JenisPengikatan"]; }
            set { myRow["JenisPengikatan"] = value; }
        }
        public object Package
        {
            get { return myRow["Package"]; }
            set { myRow["Package"] = value; }
        }
        public object CIF
        {
            get { return myRow["CIF"]; }
            set { myRow["CIF"] = value; }
        }
        public object ClientName
        {
            get { return myRow["ClientName"]; }
            set { myRow["ClientName"] = value; }
        }
        public object SupplierName
        {
            get { return myRow["SupplierName"]; }
            set { myRow["SupplierName"] = value; }
        }
        public object SupplierBranch
        {
            get { return myRow["SupplierBranch"]; }
            set { myRow["SupplierBranch"] = value; }
        }
        public object MarketingSupplier
        {
            get { return myRow["MarketingSupplier"]; }
            set { myRow["MarketingSupplier"] = value; }
        }
        public object OTR
        {
            get { return myRow["OTR"]; }
            set { myRow["OTR"] = value; }
        }
        public object NTF
        {
            get { return myRow["NTF"]; }
            set { myRow["NTF"] = value; }
        }
        public object DP
        {
            get { return myRow["DP"]; }
            set { myRow["DP"] = value; }
        }
        public object Tenor
        {
            get { return myRow["Tenor"]; }
            set { myRow["Tenor"] = value; }
        }
        public object EffRate
        {
            get { return myRow["EffRate"]; }
            set { myRow["EffRate"] = value; }
        }
        public object Status
        {
            get { return myRow["Status"]; }
            set { myRow["Status"] = value; }
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
        public object Submit
        {
            get { return myRow["Submit"]; }
            set { myRow["Submit"] = value; }
        }
        public object SubmitBy
        {
            get { return myRow["SubmitBy"]; }
            set { myRow["SubmitBy"] = value; }
        }
        public object SubmitDateTime
        {
            get { return myRow["SubmitDateTime"]; }
            set { myRow["SubmitDateTime"] = value; }
        }
        public object Cancelled
        {
            get { return myRow["Cancelled"]; }
            set { myRow["Cancelled"] = value; }
        }
        public object CancelledDateTime
        {
            get { return myRow["CancelledDateTime"]; }
            set { myRow["CancelledDateTime"] = value; }
        }
        public object CancelledType
        {
            get { return myRow["CancelledType"]; }
            set { myRow["CancelledType"] = value; }
        }
        public object CancelledNote
        {
            get { return myRow["CancelledNote"]; }
            set { myRow["CancelledNote"] = value; }
        }
        public object OnHold
        {
            get { return myRow["OnHold"]; }
            set { myRow["OnHold"] = value; }
        }
        public DataTable Applicationtable
        {
            get { return myDataSet.Tables[0]; }
        }
    }
}