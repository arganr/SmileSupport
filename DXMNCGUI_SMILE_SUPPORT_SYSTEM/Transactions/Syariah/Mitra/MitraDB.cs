using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.Mitra
{
    public class MitraDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        protected DataTable myBrowseTableHistory;
        protected DataTable myBrowseTableComment;
        protected DBRegistry myDBReg;
        protected DataTable myDataTableAllMaster;

        internal MitraDB()
        {
            myBrowseTable = new DataTable();
            myBrowseTableHistory = new DataTable();
            myBrowseTableComment = new DataTable();
        }
        public SqlDBSetting DBSetting
        {
            get { return myDBSetting; }
        }
        public SqlLocalDBSetting LocalDBSetting
        {
            get { return myLocalDBSetting; }
        }
        public SqlDBSession DBSession
        {
            get { return myDBSession; }
        }
        public DBRegistry DBReg
        {
            get
            {
                return this.myDBReg;
            }
        }
        public DataTable DataTableAllMaster
        {
            get
            {
                return this.myDataTableAllMaster;
            }
        }
        public static MitraDB Create(SqlDBSetting dbsetting, SqlLocalDBSetting localdbSetting, SqlDBSession dbSession)
        {
            MitraDB aMitra = (MitraDB)null;
            aMitra = new MitraSql();
            aMitra.myDBSetting = dbsetting;
            aMitra.myLocalDBSetting = localdbSetting;
            aMitra.myDBSession = dbSession;
            return aMitra;
        }
        public DataTable BrowseTable
        {
            get { return myBrowseTable; }
        }
        public virtual DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            return null;
        }
        public MitraEntity Entity(DXSSType type)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            DataSet dataSet = LoadData(0);
            DataRow row = dataSet.Tables["Header"].NewRow();
            this.InitHeaderRow(row, type);
            dataSet.Tables["Header"].Rows.Add(row);
            return new MitraEntity(this, dataSet, DXSSAction.New);
        }
        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new MitraDockey());
        }
        public long BankUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new MitraBankKey());
        }
        private void InitHeaderRow(DataRow row, DXSSType type)
        {
            row.BeginEdit();
            DateTime mydate = myLocalDBSetting.GetServerTime();
            row["MKey"] = DocKeyUniqueKey();
            row["MCode"] = "New";
            row["Nama"] = "";
            row["TempatLahir"] = "";
            row["TanggalLahir"] = DBNull.Value;
            row["Address"] = DBNull.Value;
            row["Email"] = DBNull.Value;
            row["NoTlp"] = DBNull.Value;
            row["Hp"] = DBNull.Value;
            row["NoWhatsApp"] = DBNull.Value;
            row["JenisMitra"] = DBNull.Value;
            row["IsSubMitra"] = DBNull.Value;
            row["IsActive"] = DBNull.Value;
            row["CreatedBy"] = DBNull.Value;
            row["CreatedDateTime"] = DBNull.Value;
            row["LastModifiedBy"] = DBNull.Value;
            row["LastModifiedTime"] = DBNull.Value;
            row["ContactPerson"] = DBNull.Value;
            row["NPWP"] = DBNull.Value;
            row["AktePendirian"] = DBNull.Value;
            row["Branch"] = DBNull.Value;
            row["TipeMitra"] = DBNull.Value;
            row["Provinsi"] = DBNull.Value;
            row["KotaKabupaten"] = DBNull.Value;
            row.EndEdit();
        }
        public MitraEntity GetEntity(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);

            DataSet ds = LoadData(headerid);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return new MitraEntity(this, ds, DXSSAction.Edit);
        }
        public MitraEntity Edit(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public MitraEntity View(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalView(this.LoadData(headerid));
        }
        public void UpdateAllMaster(DataTable sourceTable)
        {
            if (this.myDataTableAllMaster.PrimaryKey.Length != 0)
            {
                DataRow row = this.myDataTableAllMaster.Rows.Find(sourceTable.Rows[0]["DocKey"]) ?? this.myDataTableAllMaster.NewRow();
                foreach (DataColumn index1 in (InternalDataCollectionBase)row.Table.Columns)
                {
                    int index2 = sourceTable.Columns.IndexOf(index1.ColumnName);
                    if (index2 >= 0)
                        row[index1] = sourceTable.Rows[0][index2];
                }
                row.EndEdit();
                if (row.RowState == DataRowState.Detached)
                    this.myDataTableAllMaster.Rows.Add(row);
            }
        }
        public void DeleteAllMaster(long docKey)
        {
            if (myDataTableAllMaster.PrimaryKey.Length != 0)
            {
                DataRow dataRow = this.myDataTableAllMaster.Rows.Find((object)docKey);
                if (dataRow != null)
                    dataRow.Delete();
            }
        }
        private MitraEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (MitraEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new MitraEntity(this, newDataSet, DXSSAction.View);
            }
        }
        private MitraEntity InternalEdit(DataSet newDataSet, DXSSAction action)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (MitraEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["MKey"]);
                return new MitraEntity(this, newDataSet, action);
            }
        }
        public void SaveEntity(MitraEntity entity, SaveAction saveaction, string strID, string strName)
        {
            if (entity.MCode.ToString().Length == 0)
                throw new EmptyMCodeException();


            SaveData(entity, entity.myDataSet, saveaction, strID, strName);
            LoadBrowseTable(false, myDBSession.LoginUserID);
            try
            {
                if (myBrowseTable.Rows.Count > 0)
                {
                    DataRow r = myBrowseTable.Rows.Find(entity.MKey);
                    if (r == null)
                    {
                        r = myBrowseTable.NewRow();
                        foreach (DataColumn col in entity.MitraTable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.Row[col];
                        }
                        myBrowseTable.Rows.Add(r);
                    }
                    else
                    {
                        foreach (DataColumn col in entity.MitraTable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.Row[col];
                        }
                    }
                    myBrowseTable.AcceptChanges();
                }
            }
            catch { }
        }
        protected virtual DataSet LoadData(long headerid)
        {
            return null;
        }
        public virtual void Delete(long headerid)
        {
        }
        protected virtual void SaveData(MitraEntity Application, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
        }
        protected virtual void SaveBankDetail(DataSet ds, SaveAction saveaction)
        { }
        protected virtual void ClearBankDetail(MitraEntity Mitra, SaveAction saveaction)
        { }
    }
}