using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections.WaiveLateCharges
{
    public class WaiveLateChargesEntity
    {
        private WaiveLateChargesDB myWaiveLateChargescommand;
        internal DataSet myDataSet;
        private DataRow myRow;
        private DataTable myHeaderTable;
        private DXSSAction myAction;
        private DXSSType myDocType;
        public string strErrorGenTicket;
        private string myAppNote;

        internal DataRow Row
        {
            get { return myRow; }
        }

        public WaiveLateChargesDB WaiveLateChargescommand
        {
            get
            {
                return this.myWaiveLateChargescommand;
            }
        }
        public DataTable DataTableHeader
        {
            get
            {
                return this.myHeaderTable;
            }
        }
        public DataSet WaiveLateChargesDataSet
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
        public WaiveLateChargesEntity(WaiveLateChargesDB aTicket, DataSet ds, DXSSAction action)
        {
            myWaiveLateChargescommand = aTicket;
            myDataSet = ds;
            this.myAction = action;
            this.myHeaderTable = this.myDataSet.Tables["Header"];
            myRow = myHeaderTable.Rows[0];
            this.myHeaderTable.ColumnChanged += new DataColumnChangeEventHandler(this.myHeaderTable_ColumnChanged);
        }
        private void myLinesTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {

        }
        private void myLinesTable_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {

        }
        private void myHeaderTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {

        }
        private void DetailDataRowDeletedEventHandler(object sender, DataRowChangeEventArgs e)
        {
        }
        private void myLinesTable_RowDeleting(object sender, DataRowChangeEventArgs e)
        {

        }
        private void DetailDataColumnChangeEventHandler(object sender, DataColumnChangeEventArgs e)
        {
        }
        public DataTable LoadDocNoFormatTable()
        {
            DataTable mytable = new DataTable();
            mytable = myWaiveLateChargescommand.DBSetting.GetDataTable("select * from DocNoFormat where DOCTYPE='LCW'", false);
            return mytable;
        }
        public DataTable LoadCategoryTable()
        {
            DataTable mytable = new DataTable();
            mytable = myWaiveLateChargescommand.DBSetting.GetDataTable("select Category from [dbo].[Category] where DocType='LCW'", false);
            return mytable;
        }
        public DataTable LoadCategorySubTable(string category)
        {
            DataTable mytable = new DataTable();
            string strQuery = "select Category, SubCategory from CategorySub where Category=? order by SubCategory";
            mytable = myWaiveLateChargescommand.DBSetting.GetDataTable(strQuery, false, category);
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
        public void Save(string userID, string strDocName, SaveAction saveaction)
        {
            if (saveaction == SaveAction.Cancel)
            {
                this.myAction = DXSSAction.Cancel;
            }
            {

                bool flag = this.myRow.RowState != DataRowState.Unchanged;

                if (flag)
                {
                    this.myRow["LastModifiedBy"] = (object)userID;
                    this.myRow["LastModifiedDateTime"] = (object)this.myWaiveLateChargescommand.DBSetting.GetServerTime();
                    if (this.myRow["CreatedBy"].ToString().Length == 0)
                        this.myRow["CreatedBy"] = this.myRow["LastModifiedBy"];
                    this.myRow.EndEdit();
                    myWaiveLateChargescommand.SaveEntity(this, strDocName, saveaction);
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
        public object LateChargesAmount
        {
            get { return myRow["LateChargesAmount"]; }
            set { myRow["LateChargesAmount"] = value; }
        }
        public object WaiveAmount
        {
            get { return myRow["WaiveAmount"]; }
            set { myRow["WaiveAmount"] = value; }
        }
        public object Client
        {
            get { return myRow["Client"]; }
            set { myRow["Client"] = value; }
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
        public object Cancelled
        {
            get { return myRow["Cancelled"]; }
            set { myRow["Cancelled"] = value; }
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
        public DataTable WaiveLateChargestable
        {
            get { return myDataSet.Tables[0]; }
        }
    }
}