using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.UpdateNoSPPH
{
    public class UpdateNoSPPHDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        protected DataTable myIncentiveTable;
        protected DataTable myAllIncentiveTable;
        protected DBRegistry myDBReg;
        protected DataTable myDataTableAllMaster;

        internal UpdateNoSPPHDB()
        {
            myBrowseTable = new DataTable();
            myIncentiveTable = new DataTable();
            myAllIncentiveTable = new DataTable();
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
        public static UpdateNoSPPHDB Create(SqlDBSetting dbsetting,SqlLocalDBSetting localdbSetting, SqlDBSession dbSession)
        {
            UpdateNoSPPHDB aUpdateNoSPPHDB = (UpdateNoSPPHDB)null; ;
            aUpdateNoSPPHDB = new UpdateNoSPPHSql();
            aUpdateNoSPPHDB.myDBSetting = dbsetting;
            aUpdateNoSPPHDB.myLocalDBSetting = localdbSetting;
            aUpdateNoSPPHDB.myDBSession = dbSession;
            return aUpdateNoSPPHDB;
        }
        public DataTable BrowseTable
        {
            get { return myBrowseTable; }
        }
        public DataTable IncentiveTable
        {
            get { return myIncentiveTable; }
        }
        public DataTable AllIncentiveTable
        {
            get { return myIncentiveTable; }
        }

        public virtual DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            return null;
        }
        public virtual DataTable LoadIncentiveTable(string sAgreementNo)
        {
            return null;
        }
        public virtual DataTable LoadAllIncentiveTable()
        {
            return null;
        }

        public UpdateNoSPPHEntity Entity(DXSSType type)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            DataSet dataSet = LoadData(0);
            DataRow row = dataSet.Tables["Header"].NewRow();
            this.InitHeaderRow(row, type);
            dataSet.Tables["Header"].Rows.Add(row);
            return new UpdateNoSPPHEntity(this, dataSet, DXSSAction.New);
        }
        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new UpdateSPPHNoDocKey());
        }

        private void InitHeaderRow(DataRow row, DXSSType type)
        {
            row.BeginEdit();
            DateTime mydate = myLocalDBSetting.GetServerTime();
            row["DocKey"] = DocKeyUniqueKey();
            row["AgreementNo"] = DBNull.Value;
            row["NoSPPH"] = DBNull.Value;
            row["JenisPengurus"] = DBNull.Value;
            row["IDPengurus"] = DBNull.Value;
            row["NamaPengurus"] = DBNull.Value;
            row["Status"] = "NEW";
            row["CreatedBy"] = myDBSession.LoginUserID;
            row["CreatedDateTime"] = myLocalDBSetting.GetServerTime();
            row["LastModifiedBy"] = myDBSession.LoginUserID;
            row["LastModifiedDateTime"] = myLocalDBSetting.GetServerTime();
            row["ApproveBy"] = DBNull.Value;
            row["ApproveDateTime"] = DBNull.Value;
            row["DebiturName"] = DBNull.Value;
            row["Tenor"] = 0;
            row["Installment"] = 0;
            row["Branch"] = DBNull.Value;
            row.EndEdit();
        }
        public UpdateNoSPPHEntity GetEntity(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);

            DataSet ds = LoadData(headerid);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return new UpdateNoSPPHEntity(this, ds, DXSSAction.Edit);
        }
        public UpdateNoSPPHEntity Edit(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public UpdateNoSPPHEntity Grab(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public UpdateNoSPPHEntity Approve(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public UpdateNoSPPHEntity View(long headerid)
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

        private UpdateNoSPPHEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (UpdateNoSPPHEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new UpdateNoSPPHEntity(this, newDataSet, DXSSAction.View);
            }
        }
        private UpdateNoSPPHEntity InternalEdit(DataSet newDataSet, DXSSAction action)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (UpdateNoSPPHEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new UpdateNoSPPHEntity(this, newDataSet, action);
            }
        }

        public void SaveEntity(UpdateNoSPPHEntity entity, SaveAction saveaction, string strID, string strName)
        {
            if (entity.AgreementNo.ToString().Length == 0)
                throw new EmptyDocNoException();


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
                        foreach (DataColumn col in entity.Applicationtable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.Row[col];
                        }
                        myBrowseTable.Rows.Add(r);
                    }
                    else
                    {
                        foreach (DataColumn col in entity.Applicationtable.Columns)
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

        protected virtual void SaveData(UpdateNoSPPHEntity Entity, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
        }
        protected virtual void UpdateSMILE(UpdateNoSPPHEntity Entity, DataSet ds, SaveAction saveaction, string userID)
        {
        }
        protected virtual void Exec_SP_SMILE(string sAgreeNo)
        {
        }
        public virtual void Delete(long headerid)
        {
        }
    }
}