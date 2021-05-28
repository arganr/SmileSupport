using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Insurance
{
    public class PolisAsuransiFADB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        protected DataTable myBrowseTableHistory;
        protected DBRegistry myDBReg;
        protected DataTable myDataTableAllMaster;
        protected DataTable myMaskapaiTable;
        protected DataTable myCoverageTable;

        internal PolisAsuransiFADB()
        {
            myBrowseTable = new DataTable();
            myBrowseTableHistory = new DataTable();
            myMaskapaiTable = new DataTable();
            myCoverageTable = new DataTable();
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
        public static PolisAsuransiFADB Create(SqlDBSetting dbsetting, SqlLocalDBSetting localdbSetting, SqlDBSession dbSession)
        {
            PolisAsuransiFADB aPolisAsuransiFA = (PolisAsuransiFADB)null;
            aPolisAsuransiFA = new PolisAsuransiFASql();
            aPolisAsuransiFA.myDBSetting = dbsetting;
            aPolisAsuransiFA.myLocalDBSetting = localdbSetting;
            aPolisAsuransiFA.myDBSession = dbSession;
            return aPolisAsuransiFA;
        }
        public DataTable BrowseTable
        {
            get { return myBrowseTable; }
        }
        public virtual DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            return null;
        }
        public PolisAsuransiFAEntity Entity(DXSSType type)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            DataSet dataSet = LoadData(0);
            DataRow row = dataSet.Tables["Header"].NewRow();
            this.InitHeaderRow(row, type);
            dataSet.Tables["Header"].Rows.Add(row);
            return new PolisAsuransiFAEntity(this, dataSet, DXSSAction.New);
        }
        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new PolisAsuransiFADocKey());
        }
        public long DtlKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new PolisAsuransiFADtlKey());
        }
        private void InitHeaderRow(DataRow row, DXSSType type)
        {
            row.BeginEdit();
            DateTime mydate = myLocalDBSetting.GetServerTime();
            row["DocKey"] = DocKeyUniqueKey();
            row["DocNo"] = "New";
            row["DocDate"] = mydate;
            row["AssetDesc"] = "";
            row["NoPolisi"] = DBNull.Value;
            row["NoRangKa"] = DBNull.Value;
            row["NoMesin"] = DBNull.Value;
            row["CreatedBy"] = DBNull.Value;
            row["CreatedDateTime"] = mydate;
            row["LastModifiedBy"] = DBNull.Value;
            row["LastModifiedDateTime"] = mydate;
            row.EndEdit();
        }
        public PolisAsuransiFAEntity GetEntity(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);

            DataSet ds = LoadData(headerid);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return new PolisAsuransiFAEntity(this, ds, DXSSAction.Edit);
        }
        public PolisAsuransiFAEntity Edit(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public PolisAsuransiFAEntity View(long headerid)
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
        private PolisAsuransiFAEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (PolisAsuransiFAEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new PolisAsuransiFAEntity(this, newDataSet, DXSSAction.View);
            }
        }
        private PolisAsuransiFAEntity InternalEdit(DataSet newDataSet, DXSSAction action)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (PolisAsuransiFAEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new PolisAsuransiFAEntity(this, newDataSet, action);
            }
        }
        public virtual DataTable LoadMaskapai()
        {
            return null;
        }
        public virtual DataTable LoadCoverage()
        {
            return null;
        }
        public void SaveEntity(PolisAsuransiFAEntity entity, SaveAction saveaction, string strID, string strName)
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
                        foreach (DataColumn col in entity.PolisAsuransiFATable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.Row[col];
                        }
                        myBrowseTable.Rows.Add(r);
                    }
                    else
                    {
                        foreach (DataColumn col in entity.PolisAsuransiFATable.Columns)
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
        protected virtual void SaveData(PolisAsuransiFAEntity PolisAsuransiFA, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
        }
        protected virtual void SaveDetail(DataSet ds, SaveAction saveaction)
        { }
        protected virtual void ClearDetail(PolisAsuransiFAEntity PolisAsuransiFA, SaveAction saveaction)
        { }
        protected virtual void PostToSmile(DataSet ds, SaveAction saveaction, string userID, string userName)
        {}
    }
}