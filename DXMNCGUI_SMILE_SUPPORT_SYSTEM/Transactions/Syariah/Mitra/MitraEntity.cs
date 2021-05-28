using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.Mitra
{
    public class MitraEntity
    {
        private MitraDB myMitracommand;
        internal DataSet myDataSet;
        private DataRow myRow;
        private DataTable myHeaderTable;
        private DataTable myDetailTable;
        private DataTable myBankTable;
        private DXSSAction myAction;
        private DXSSType myDocType;
        public string strErrorGenMitra;
        internal DataRow Row
        {
            get { return myRow; }
        }
        public MitraDB Mitracommand
        {
            get
            {
                return this.myMitracommand;
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
        public DataTable DataTableBank
        {
            get
            {
                return this.myBankTable;
            }
        }
        public DataSet MitraDataSet
        {
            get
            {
                return this.myDataSet;
            }
        }

        public MitraEntity(MitraDB aMitra, DataSet ds, DXSSAction action)
        {
            myMitracommand = aMitra;
            myDataSet = ds;
            this.myAction = action;
            this.myHeaderTable = this.myDataSet.Tables["Header"];
            this.myBankTable = this.myDataSet.Tables["BankDetail"];
            myRow = myHeaderTable.Rows[0];
            this.myHeaderTable.ColumnChanged += new DataColumnChangeEventHandler(this.myHeaderTable_ColumnChanged);
            this.myBankTable.RowChanged += new DataRowChangeEventHandler(this.myBankTable_RowChanged);
            this.myBankTable.ColumnChanged += new DataColumnChangeEventHandler(this.DetailDataColumnChangeEventHandler);
            this.myBankTable.RowDeleting += new DataRowChangeEventHandler(this.myBankTable_RowDeleting);
            this.myBankTable.RowDeleted += new DataRowChangeEventHandler(this.BankDataRowDeletedEventHandler);
            this.myBankTable.ColumnChanging += new DataColumnChangeEventHandler(this.myBankTable_ColumnChanging);
        }

        private void myHeaderTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {

        }
        private void myBankTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {

        }
        private void myBankTable_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {

        }
        private void myBankTable_RowDeleting(object sender, DataRowChangeEventArgs e)
        {

        }
        private void BankDataRowDeletedEventHandler(object sender, DataRowChangeEventArgs e)
        {
        }
        private void DetailDataColumnChangeEventHandler(object sender, DataColumnChangeEventArgs e)
        {
        }

        public DataTable LoadMitraCodeFormatTable()
        {
            DataTable mytable = new DataTable();
            mytable = myMitracommand.LocalDBSetting.GetDataTable("select * from DocNoFormat where DOCTYPE='MTR'", false);
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
                return this.myBankTable.Select("", "MBankKey", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
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
                    this.myRow["LastModifiedTime"] = (object)this.myMitracommand.LocalDBSetting.GetServerTime();
                    if (this.myRow["CreatedBy"].ToString().Length == 0)
                        this.myRow["CreatedBy"] = this.myRow["LastModifiedBy"];
                    this.myRow.EndEdit();
                    myMitracommand.SaveEntity(this, saveaction, userID, userName);
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
            return myMitracommand.DBReg.IncOne((Controllers.Registry.IRegistryID)new MitraBankKey());
        }
        public MitraBankRecord AddBank()
        {
            if (this.myAction == DXSSAction.View)
                throw new Exception("Cannot edit read-only Application Lines");
            else
                return this.InternalAddBank(SeqUtils.GetLastSeq(this.ValidDetailLinesRows));
        }
        private MitraBankRecord InternalAddBank(int seq)
        {
            DataRow row = this.myDetailTable.NewRow();
            DateTime myDate = myMitracommand.LocalDBSetting.GetServerTime();
            string iUserID = myMitracommand.DBSession.LoginUserID;
            row["MBankKey"] = (object)myMitracommand.BankUniqueKey();
            row["MKey"] = this.myRow["MKey"];
            row["BankName"] = DBNull.Value;
            row["BankBranch"] = DBNull.Value;
            row["BankAccNo"] = myDate.Year;
            row["BankAccName"] = 0;
            row.EndEdit();
            this.myDetailTable.Rows.Add(row);
            return new MitraBankRecord(row, this);
        }
        public bool IsModified()
        {
            if (this.myDetailTable.GetChanges() != null)
                return true;
            else
                return false;
        }

        public object MKey
        {
            get { return myRow["MKey"]; }
            set { myRow["MKey"] = value; }
        }
        public object MCode
        {
            get { return myRow["MCode"]; }
            set { myRow["MCode"] = value; }
        }
        public object Nama
        {
            get { return myRow["Nama"]; }
            set { myRow["Nama"] = value; }
        }
        public object TempatLahir
        {
            get { return myRow["TempatLahir"]; }
            set { myRow["TempatLahir"] = value; }
        }
        public object TanggalLahir
        {
            get { return myRow["TanggalLahir"]; }
            set { myRow["TanggalLahir"] = value; }
        }
        public object Address
        {
            get { return myRow["Address"]; }
            set { myRow["Address"] = value; }
        }
        public object Email
        {
            get { return myRow["Email"]; }
            set { myRow["Email"] = value; }
        }
        public object NoTlp
        {
            get { return myRow["NoTlp"]; }
            set { myRow["NoTlp"] = value; }
        }
        public object Hp
        {
            get { return myRow["Hp"]; }
            set { myRow["Hp"] = value; }
        }
        public object NoWhatsApp
        {
            get { return myRow["NoWhatsApp"]; }
            set { myRow["NoWhatsApp"] = value; }
        }
        public object JenisMitra
        {
            get { return myRow["JenisMitra"]; }
            set { myRow["JenisMitra"] = value; }
        }
        public object IsSubMitra
        {
            get { return myRow["IsSubMitra"]; }
            set { myRow["IsSubMitra"] = value; }
        }
        public object IsActive
        {
            get { return myRow["IsActive"]; }
            set { myRow["IsActive"] = value; }
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
        public object ContactPerson
        {
            get { return myRow["ContactPerson"]; }
            set { myRow["ContactPerson"] = value; }
        }
        public object NPWP
        {
            get { return myRow["NPWP"]; }
            set { myRow["NPWP"] = value; }
        }
        public object AktePendirian
        {
            get { return myRow["AktePendirian"]; }
            set { myRow["AktePendirian"] = value; }
        }
        public object SubMitra
        {
            get { return myRow["SubMitra"]; }
            set { myRow["SubMitra"] = value; }
        }
        public object Branch
        {
            get { return myRow["Branch"]; }
            set { myRow["Branch"] = value; }
        }
        public object TipeMitra
        {
            get { return myRow["TipeMitra"]; }
            set { myRow["TipeMitra"] = value; }
        }
        public object Provinsi
        {
            get { return myRow["Provinsi"]; }
            set { myRow["Provinsi"] = value; }
        }
        public object KotaKabupaten
        {
            get { return myRow["KotaKabupaten"]; }
            set { myRow["KotaKabupaten"] = value; }
        }
        public object PIC
        {
            get { return myRow["PIC"]; }
            set { myRow["PIC"] = value; }
        }
        public object Password
        {
            get { return myRow["Password"]; }
            set { myRow["Password"] = value; }
        }
        public object Profile
        {
            get { return myRow["Profile"]; }
            set { myRow["Profile"] = value; }
        }
        public DataTable MitraTable
        {
            get { return myDataSet.Tables[0]; }
        }
    }
}