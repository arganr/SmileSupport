using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.SupplyChainFinancing.ListJaminan
{
    public class ListJaminanDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        protected DataTable myBrowseTableHistory;
        protected DataTable myBrowseTableComment;
        protected DBRegistry myDBReg;
        protected DataTable myDataTableAllMaster;

        internal ListJaminanDB()
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
        public static ListJaminanDB Create(SqlDBSetting dbsetting, SqlLocalDBSetting localdbSetting, SqlDBSession dbSession)
        {
            ListJaminanDB aListJaminan = (ListJaminanDB)null;
            aListJaminan = new ListJaminanSql();
            aListJaminan.myDBSetting = dbsetting;
            aListJaminan.myLocalDBSetting = localdbSetting;
            aListJaminan.myDBSession = dbSession;
            return aListJaminan;
        }
        public DataTable BrowseTable
        {
            get { return myBrowseTable; }
        }
        public virtual DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            return null;
        }
        public ListJaminanEntity Entity(DXSSType type)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            DataSet dataSet = LoadData(0);
            DataRow row = dataSet.Tables["Header"].NewRow();
            this.InitHeaderRow(row, type);
            dataSet.Tables["Header"].Rows.Add(row);
            return new ListJaminanEntity(this, dataSet, DXSSAction.New);
        }
        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new ListJaminanDocKey());
        }
        public long DetailUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new ListJaminanDetailDtlKey());
        }
        private void InitHeaderRow(DataRow row, DXSSType type)
        {
            row.BeginEdit();
            DateTime mydate = myLocalDBSetting.GetServerTime();
            row["DocKey"] = DocKeyUniqueKey();
            row["DocNo"] = "New";
            row["DocDate"] = mydate;
            row["RefNo"] = DBNull.Value;
            row["AssetDesc"] = DBNull.Value;
            row["TotalJaminan"] = 0;
            row["TotalPembiayaan"] = 0;
            row["IsPost"] = "F";
            row["CreatedBy"] = DBNull.Value;
            row["CreatedDateTime"] = DBNull.Value;
            row["LastModifiedBy"] = DBNull.Value;
            row["LastModifiedTime"] = DBNull.Value;
            row["Debitur"] = DBNull.Value;
            row.EndEdit();
        }
        public ListJaminanEntity GetEntity(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);

            DataSet ds = LoadData(headerid);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return new ListJaminanEntity(this, ds, DXSSAction.Edit);
        }
        public ListJaminanEntity Edit(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public ListJaminanEntity View(long headerid)
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
        private ListJaminanEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (ListJaminanEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new ListJaminanEntity(this, newDataSet, DXSSAction.View);
            }
        }
        private ListJaminanEntity InternalEdit(DataSet newDataSet, DXSSAction action)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (ListJaminanEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new ListJaminanEntity(this, newDataSet, action);
            }
        }
        public void SaveEntity(ListJaminanEntity entity, SaveAction saveaction, string strID, string strName)
        {
            if (entity.DocKey.ToString().Length == 0)
                throw new EmptyMCodeException();


            SaveData(entity, entity.myDataSet, saveaction, strID, strName);
            LoadBrowseTable(false, myDBSession.LoginUserID);
            try
            {
                if (myBrowseTable.Rows.Count > 0)
                {
                    DataRow r = myBrowseTable.Rows.Find(entity.DocKey);
                    if (r == null)
                    {
                        r = myBrowseTable.NewRow();
                        foreach (DataColumn col in entity.ListJaminanTable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.Row[col];
                        }
                        myBrowseTable.Rows.Add(r);
                    }
                    else
                    {
                        foreach (DataColumn col in entity.ListJaminanTable.Columns)
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
        protected virtual void SaveData(ListJaminanEntity ListJaminan, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
        }
        protected virtual void SaveDetail(DataSet ds, SaveAction saveaction)
        { }
        protected virtual void ClearDetail(ListJaminanEntity ListJaminan, SaveAction saveaction)
        { }
    }
}