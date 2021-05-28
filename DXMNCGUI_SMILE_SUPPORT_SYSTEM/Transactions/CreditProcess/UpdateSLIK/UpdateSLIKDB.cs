using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.UpdateSLIK
{
    public class UpdateSLIKDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        protected DataTable myBrowseTableHistory;
        protected DBRegistry myDBReg;
        protected DataTable myDataTableAllMaster;

        internal UpdateSLIKDB()
        {
            myBrowseTable = new DataTable();
            myBrowseTableHistory = new DataTable();
        }
        public SqlDBSetting SqlDBSetting
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
        public static UpdateSLIKDB Create(SqlDBSetting dbSetting, SqlLocalDBSetting dblocalSetting, SqlDBSession dbSession)
        {
            UpdateSLIKDB aUpdateSLIK = (UpdateSLIKDB)null;
            aUpdateSLIK = new UpdateSLIKSql();
            aUpdateSLIK.myDBSetting = dbSetting;
            aUpdateSLIK.myLocalDBSetting = dblocalSetting;
            aUpdateSLIK.myDBSession = dbSession;
            return aUpdateSLIK;
        }
        public DataTable BrowseTable
        {
            get { return myBrowseTable; }
        }
        public virtual DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            return null;
        }
        public UpdateSLIKEntity Entity(DXSSType type)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            DataSet dataSet = LoadData(0);
            DataRow row = dataSet.Tables["Header"].NewRow();
            this.InitHeaderRow(row, type);
            dataSet.Tables["Header"].Rows.Add(row);
            return new UpdateSLIKEntity(this, dataSet, DXSSAction.New);
        }
        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new UpdateSLIKDocKey());
        }
        public long DtlKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new UpdateSLIKDtlKey());
        }
        private void InitHeaderRow(DataRow row, DXSSType type)
        {
            row.BeginEdit();
            DateTime mydate = myLocalDBSetting.GetServerTime();
            row["DocKey"] = DocKeyUniqueKey();
            row["DocNo"] = "New";
            row["DocDate"] = DBNull.Value;
            row["RefNo"] = DBNull.Value;
            row["Remark"] = DBNull.Value;
            row["CreatedBy"] = DBNull.Value;
            row["CreatedDateTime"] = DBNull.Value;
            row["Cancel"] = DBNull.Value;
            row["CancelBy"] = DBNull.Value;
            row["CancelDateTime"] = DBNull.Value;
            row.EndEdit();
        }
        public UpdateSLIKEntity GetEntity(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);

            DataSet ds = LoadData(headerid);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return new UpdateSLIKEntity(this, ds, DXSSAction.Edit);
        }
        public UpdateSLIKEntity Edit(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public UpdateSLIKEntity View(long headerid)
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
        private UpdateSLIKEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (UpdateSLIKEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new UpdateSLIKEntity(this, newDataSet, DXSSAction.View);
            }
        }
        private UpdateSLIKEntity InternalEdit(DataSet newDataSet, DXSSAction action)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (UpdateSLIKEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new UpdateSLIKEntity(this, newDataSet, action);
            }
        }
        public void SaveEntity(UpdateSLIKEntity entity, SaveAction saveaction, string strID, string strName)
        {
            if (entity.DocKey.ToString().Length == 0)
                throw new EmptyMCodeException();


            SaveData(entity, entity.myDataSet, saveaction, strID, strName);
            LoadBrowseTable(false, myDBSession.LoginUserID);
            try
            {
                if (myBrowseTable.Rows.Count > 0)
                {
                    DataRow r = myBrowseTable.Rows.Find(entity.DocNo);
                    if (r == null)
                    {
                        r = myBrowseTable.NewRow();
                        foreach (DataColumn col in entity.UpdateSLIKTable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.Row[col];
                        }
                        myBrowseTable.Rows.Add(r);
                    }
                    else
                    {
                        foreach (DataColumn col in entity.UpdateSLIKTable.Columns)
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
        protected virtual void SaveData(UpdateSLIKEntity UpdateSLIK, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
        }
        protected virtual void SaveDetail(DataSet ds, SaveAction saveaction)
        { }
        protected virtual void ClearDetail(UpdateSLIKEntity UpdateSLIK, SaveAction saveaction)
        { }
    }
}