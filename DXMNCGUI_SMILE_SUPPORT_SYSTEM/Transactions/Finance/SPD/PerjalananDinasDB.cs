using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.SPD
{
    public class PerjalananDinasDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        protected DBRegistry myDBReg;
        DataTable myHeaderTable;
        DataTable myDetailTable;
        DataTable myBudgetTable;
        DataTable myApprovalTable;

        internal PerjalananDinasDB()
        {
            myBrowseTable = new DataTable();
            myHeaderTable = new DataTable();
            myDetailTable = new DataTable();
            myBudgetTable = new DataTable();
            myApprovalTable = new DataTable();
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
        public static PerjalananDinasDB Create(SqlDBSetting dbsetting, SqlLocalDBSetting localdbSetting, SqlDBSession dbSession)
        {
            PerjalananDinasDB aPerjalananDinasDB = (PerjalananDinasDB)null; ;
            aPerjalananDinasDB = new PerjalananDinasDB();
            aPerjalananDinasDB.myDBSetting = dbsetting;
            aPerjalananDinasDB.myLocalDBSetting = localdbSetting;
            aPerjalananDinasDB.myDBSession = dbSession;
            return aPerjalananDinasDB;
        }
        public DataTable BrowseTable
        {
            get { return myBrowseTable; }
        }
        public virtual DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            return null;
        }
        protected virtual DataSet LoadData(long headerid)
        {
            return null;
        }
        protected virtual void SaveData(PerjalananDinasEntity Entity, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
        }
        protected virtual void SaveDataApproval(DataSet ds, SaveAction saveaction)
        {
        }
        protected virtual void SaveDataDetail(DataSet ds, SaveAction saveaction)
        {
        }
        protected virtual void SaveDataDetailBudget(DataSet ds, SaveAction saveaction)
        {
        }
        public PerjalananDinasEntity Entity()
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            DataSet dataSet = LoadData(0);
            DataRow row = dataSet.Tables["Header"].NewRow();
            this.InitHeaderRow(row);
            dataSet.Tables["Header"].Rows.Add(row);
            return new PerjalananDinasEntity(this, dataSet, DXSSAction.New);
        }
        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new SPDDocKey());
        }
        public long DtlKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new SPDDtlKey());
        }
        public long DtlAppKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new SPDDtlAppKey());
        }
        public long DtlBudgetKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new SPDDtlBudgetKey());
        }
        private void InitHeaderRow(DataRow row)
        {
            row.BeginEdit();
            DateTime mydate = myLocalDBSetting.GetServerTime();
            row["DocKey"] = DocKeyUniqueKey();
            row["DocNo"] = DBNull.Value;
            row["DocDate"] = DBNull.Value;
            row["Status"] = "NEW";
            row["NIK"] = DBNull.Value;
            row["Dept"] = DBNull.Value;
            row["Jabatan"] = DBNull.Value;
            row["TipeTunjangan"] = DBNull.Value;
            row["Tujuan"] = myDBSession.LoginUserID;
            row["PembebananBiaya"] = myLocalDBSetting.GetServerTime();
            row["CRE_BY"] = myDBSession.LoginUserID;
            row["CRE_DATE"] = myLocalDBSetting.GetServerTime();
            row["MOD_BY"] = myDBSession.LoginUserID;
            row["MOD_DATE"] = myLocalDBSetting.GetServerTime();
            row.EndEdit();
        }
        public PerjalananDinasEntity GetEntity(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);

            DataSet ds = LoadData(headerid);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return new PerjalananDinasEntity(this, ds, DXSSAction.Edit);
        }
        public PerjalananDinasEntity Edit(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public PerjalananDinasEntity Grab(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public PerjalananDinasEntity Approve(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public PerjalananDinasEntity View(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalView(this.LoadData(headerid));
        }

        private PerjalananDinasEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (PerjalananDinasEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new PerjalananDinasEntity(this, newDataSet, DXSSAction.View);
            }
        }
        private PerjalananDinasEntity InternalEdit(DataSet newDataSet, DXSSAction action)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (PerjalananDinasEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new PerjalananDinasEntity(this, newDataSet, action);
            }
        }

        public void SaveEntity(PerjalananDinasEntity entity, SaveAction saveaction, string strID, string strName)
        {
            if (entity.DocNo.ToString().Length == 0)
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
                        foreach (DataColumn col in entity.SPDtable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.Row[col];
                        }
                        myBrowseTable.Rows.Add(r);
                    }
                    else
                    {
                        foreach (DataColumn col in entity.SPDtable.Columns)
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

        
    }
}