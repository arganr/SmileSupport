using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.UpdateNoSPPH
{
    public class UpdateNoSPPHEntity
    {
        private UpdateNoSPPHDB myApplicationcommand;
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
        public UpdateNoSPPHDB Applicationcommand
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
        public UpdateNoSPPHEntity(UpdateNoSPPHDB aUpdateNoSPPHDB, DataSet ds, DXSSAction action)
        {
            myApplicationcommand = aUpdateNoSPPHDB;
            myDataSet = ds;
            this.myAction = action;
            this.myHeaderTable = this.myDataSet.Tables["Header"];
            myRow = myHeaderTable.Rows[0];
            this.myHeaderTable.ColumnChanged += new DataColumnChangeEventHandler(this.myHeaderTable_ColumnChanged);
        }

        private void myHeaderTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {

        }

        public DataTable LoadKaryawanTable()
        {
            DataTable mytable = new DataTable();
            mytable = myApplicationcommand.LocalDBSetting.GetDataTable("select * from DocNoFormat where DOCTYPE='CD'", false);
            return mytable;
        }
        public DataTable LoadMitraTable()
        {
            DataTable mytable = new DataTable();
            mytable = myApplicationcommand.LocalDBSetting.GetDataTable("select Category from [dbo].[Category] where DocType='CD'", false);
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
                    this.myRow["LastModifiedBy"] = (object)userID;
                    this.myRow["LastModifiedDateTime"] = (object)this.myApplicationcommand.LocalDBSetting.GetServerTime();
                    if (this.myRow["CreatedBy"].ToString().Length == 0)
                        this.myRow["CreatedBy"] = this.myRow["LastModifiedUser"];
                    this.myRow.EndEdit();
                    myApplicationcommand.SaveEntity(this, saveaction, userID, userName);
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
        public object AgreementNo
        {
            get { return myRow["AgreementNo"]; }
            set { myRow["AgreementNo"] = value; }
        }
        public object NoSPPH
        {
            get { return myRow["NoSPPH"]; }
            set { myRow["NoSPPH"] = value; }
        }
        public object JenisPengurus
        {
            get { return myRow["JenisPengurus"]; }
            set { myRow["JenisPengurus"] = value; }
        }
        public object IDPengurus
        {
            get { return myRow["IDPengurus"]; }
            set { myRow["IDPengurus"] = value; }
        }
        public object NamaPengurus
        {
            get { return myRow["NamaPengurus"]; }
            set { myRow["NamaPengurus"] = value; }
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
        public object LastModifiedDateTime
        {
            get { return myRow["LastModifiedDateTime"]; }
            set { myRow["LastModifiedDateTime"] = value; }
        }
        public object DebiturName
        {
            get { return myRow["DebiturName"]; }
            set { myRow["DebiturName"] = value; }
        }
        public object Tenor
        {
            get { return myRow["Tenor"]; }
            set { myRow["Tenor"] = value; }
        }
        public object Installment
        {
            get { return myRow["Installment"]; }
            set { myRow["Installment"] = value; }
        }
        public object Branch
        {
            get { return myRow["Branch"]; }
            set { myRow["Branch"] = value; }
        }

        public DataTable Applicationtable
        {
            get { return myDataSet.Tables[0]; }
        }

    }
}