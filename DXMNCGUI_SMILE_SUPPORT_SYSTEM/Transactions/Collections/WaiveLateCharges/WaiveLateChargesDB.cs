using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections.WaiveLateCharges
{
    public class WaiveLateChargesDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        protected Controllers.Registry.DBRegistry myDBReg;
        protected DataTable myDataTableAllMaster;

        internal WaiveLateChargesDB()
        {
            myBrowseTable = new DataTable();
        }
        public SqlDBSetting DBSetting
        {
            get { return myDBSetting; }
        }
        public SqlLocalDBSetting localDBSetting
        {
            get { return myLocalDBSetting; }
        }
        public SqlDBSession DBSession
        {
            get { return myDBSession; }
        }
        public Controllers.Registry.DBRegistry DBReg
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
        public static WaiveLateChargesDB Create(SqlDBSetting dbSetting, SqlLocalDBSetting localdbsetting, SqlDBSession dbSession)
        {
            WaiveLateChargesDB aWaiveLateCharges = (WaiveLateChargesDB)null;
            aWaiveLateCharges = new WaiveLateChargesSql();
            aWaiveLateCharges.myDBSetting = dbSetting;
            aWaiveLateCharges.myLocalDBSetting = localdbsetting;
            aWaiveLateCharges.myDBSession = dbSession;
            return aWaiveLateCharges;
        }
        public DataTable BrowseTable
        {
            get { return myBrowseTable; }
        }
        public virtual DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            return null;
        }
        public WaiveLateChargesEntity Entity(WaiveLateChargesType type)
        {
            myDBReg = Controllers.Registry.DBRegistry.Create(myLocalDBSetting);
            DataSet dataSet = LoadData(0);
            DataRow row = dataSet.Tables["Header"].NewRow();
            this.InitHeaderRow(row, type);
            dataSet.Tables["Header"].Rows.Add(row);
            return new WaiveLateChargesEntity(this, dataSet, DXSSAction.New);
        }
        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new WaiveLateChargesHeaderDocKey());
        }
        private void InitHeaderRow(DataRow row, WaiveLateChargesType type)
        {
            row.BeginEdit();
            DateTime mydate = myDBSetting.GetServerTime();
            row["DocKey"] = DocKeyUniqueKey();
            row["DocNo"] = "NEW";
            row["DocDate"] = mydate;
            row["RefNo"] = "";
            row["LateChargesAmount"] = 0;
            row["WaiveAmount"] = 0;
            row["Remark1"] = DBNull.Value;
            row["Remark2"] = DBNull.Value;
            row["Remark3"] = DBNull.Value;
            row["Remark4"] = DBNull.Value;
            row["Cancelled"] = "F";
            row["Status"] = WaiveLateChargesAction.New.ToString().ToUpper();
            row["CreatedBy"] = myDBSession.LoginUserID;
            row["CreatedDateTime"] =  myDBSetting.GetServerTime();
            row["SubmitBy"] = "";
            row["SubmitDateTime"] = DBNull.Value;
            row["LastModifiedBy"] = "";
            row["LastModifiedDateTime"] = DBNull.Value;
            row.EndEdit();
        }
        public WaiveLateChargesEntity GetEntity(long headerid)
        {
            myDBReg = Controllers.Registry.DBRegistry.Create(myLocalDBSetting);

            DataSet ds = LoadData(headerid);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return new WaiveLateChargesEntity(this, ds, DXSSAction.Edit);
        }
        public WaiveLateChargesEntity Edit(long headerid, DXSSAction action)
        {
            myDBReg = Controllers.Registry.DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public WaiveLateChargesEntity View(long headerid)
        {
            myDBReg = Controllers.Registry.DBRegistry.Create(myLocalDBSetting);
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
            if (this.myDataTableAllMaster.PrimaryKey.Length != 0)
            {
                DataRow dataRow = this.myDataTableAllMaster.Rows.Find((object)docKey);
                if (dataRow != null)
                    dataRow.Delete();
            }
        }
        private WaiveLateChargesEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (WaiveLateChargesEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new WaiveLateChargesEntity(this, newDataSet, DXSSAction.View);
            }
        }
        private WaiveLateChargesEntity InternalEdit(DataSet newDataSet, DXSSAction action)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (WaiveLateChargesEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new WaiveLateChargesEntity(this, newDataSet, action);
            }
        }
        public void SaveEntity(WaiveLateChargesEntity entity, string strDocName, SaveAction saveaction)
        {
            if (entity.DocNo.ToString().Length == 0)
                throw new WaiveLateChargesCodeException();


            SaveData(entity, entity.myDataSet, strDocName, saveaction);
            LoadBrowseTable(false, myDBSession.LoginUserID);
            try
            {
                if (myBrowseTable.Rows.Count > 0)
                {
                    DataRow r = myBrowseTable.Rows.Find(entity.DocKey);
                    if (r == null)
                    {
                        r = myBrowseTable.NewRow();
                        foreach (DataColumn col in entity.WaiveLateChargestable.Columns)
                        {
                            if (myBrowseTable.Columns.Contains(col.ColumnName))
                                r[col.ColumnName] = entity.Row[col];
                        }
                        myBrowseTable.Rows.Add(r);
                    }
                    else
                    {
                        foreach (DataColumn col in entity.WaiveLateChargestable.Columns)
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
        protected virtual void SaveData(WaiveLateChargesEntity WaiveLateCharges, DataSet ds, string strDocName, SaveAction saveaction)
        {
        }
    }
}