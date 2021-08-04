using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess
{
    public class ApplicationDB
    {
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        protected DataTable myBrowseTableHistory;
        protected DataTable myBrowseTableComment;
        protected DBRegistry myDBReg;
        protected DataTable myDataTableAllMaster;
        internal ApplicationDB()
        {
            myBrowseTable = new DataTable();
            myBrowseTableHistory = new DataTable();
            myBrowseTableComment = new DataTable();
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
        public static ApplicationDB Create(SqlLocalDBSetting dbSetting, SqlDBSession dbSession)
        {
            ApplicationDB aApplication = (ApplicationDB)null; ;
            aApplication = new ApplicationSql();
            aApplication.myLocalDBSetting = dbSetting;
            aApplication.myDBSession = dbSession;
            return aApplication;
        }
        public DataTable BrowseTable
        {
            get { return myBrowseTable; }
        }
        public DataTable BrowseTableHistory
        {
            get { return myBrowseTableHistory; }
        }
        public DataTable BrowseTableComment
        {
            get { return myBrowseTableComment; }
        }
        public virtual void Sendmail(string strapprovalID, string strapprovalName, ApplicationEntity Application, string strsubject, string strbody, SqlDBSetting dbsetting, bool bsender, bool breject, string strrejectnote, string traveltype, Int64 itravelKey)
        {
        }
        public virtual DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            return null;
        }
        public virtual DataTable LoadBrowseTableHistory(string sDocNo)
        {
            return null;
        }
        public virtual DataTable LoadBrowseTableComment(string sDocNo)
        {
            return null;
        }
        public ApplicationEntity Entity(DXSSType type)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            DataSet dataSet = LoadData(0);
            DataRow row = dataSet.Tables["Header"].NewRow();
            this.InitHeaderRow(row, type);
            dataSet.Tables["Header"].Rows.Add(row);
            return new ApplicationEntity(this, dataSet, DXSSAction.New);
        }
        public long DocKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new ApplicationHeaderDocKey());
        }
        public long DtlKeyUniqueKey()
        {
            return this.myDBReg.IncOne((IRegistryID)new ApplicationDetailLinesDtlKey());
        }
        private void InitHeaderRow(DataRow row, DXSSType type)
        {
            row.BeginEdit();
            DateTime mydate = myLocalDBSetting.GetServerTime();
            row["DocKey"] = DocKeyUniqueKey();
            row["DocNo"] = "New";
            row["DocDate"] = mydate;
            row["DocumentType"] = "";
            row["Note"] = DBNull.Value;
            row["Remark1"] = DBNull.Value;
            row["Remark2"] = DBNull.Value;
            row["Remark3"] = DBNull.Value;
            row["Remark4"] = DBNull.Value;
            row["Branch"] = DBNull.Value;
            row["ObjectPembiayaan"] = DBNull.Value;
            row["Facility"] = DBNull.Value;
            row["JenisPengikatan"] = DBNull.Value;
            row["Package"] = DBNull.Value;
            row["CIF"] = DBNull.Value;
            row["ClientName"] = DBNull.Value;
            row["SupplierName"] = DBNull.Value;
            row["SupplierBranch"] = DBNull.Value;
            row["MarketingSupplier"] = DBNull.Value;
            row["OTR"] = 0;
            row["NTF"] = 0;
            row["DP"] = 0;
            row["Tenor"] = 0;
            row["EffRate"] = 0;
            row["Status"] = "PROSPECT";
            row["CreatedBy"] = myDBSession.LoginUserID;
            row["CreatedDateTime"] = myLocalDBSetting.GetServerTime();
            row["LastModifiedBy"] = myDBSession.LoginUserID;
            row["LastModifiedTime"] = myLocalDBSetting.GetServerTime();
            row["Submit"] = "F";
            row["SubmitBy"] = DBNull.Value;
            row["SubmitDateTime"] = DBNull.Value;
            row["Cancelled"] = "F";
            row["CancelledDateTime"] = DBNull.Value;
            row["CancelledType"] = DBNull.Value;
            row["CancelledNote"] = DBNull.Value;
            row["OnHold"] = "F";
            row.EndEdit();
        }
        public ApplicationEntity GetEntity(long headerid)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);

            DataSet ds = LoadData(headerid);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return new ApplicationEntity(this, ds, DXSSAction.Edit);
        }
        public ApplicationEntity Edit(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public ApplicationEntity Grab(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public ApplicationEntity Approve(long headerid, DXSSAction action)
        {
            myDBReg = DBRegistry.Create(myLocalDBSetting);
            return this.InternalEdit(this.LoadData(headerid), action);
        }
        public ApplicationEntity View(long headerid)
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
        private ApplicationEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (ApplicationEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new ApplicationEntity(this, newDataSet, DXSSAction.View);
            }
        }
        private ApplicationEntity InternalEdit(DataSet newDataSet, DXSSAction action)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (ApplicationEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["DocKey"]);
                return new ApplicationEntity(this, newDataSet, action);
            }
        }
        public void SaveEntity(ApplicationEntity entity, string strDocName, SaveAction saveaction, string strUpline, string strID, string strName)
        {
            if (entity.DocNo.ToString().Length == 0)
                throw new EmptyDocNoException();


            SaveData(entity, entity.myDataSet, strDocName, saveaction, strUpline, strID, strName);
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
        public void SaveCommentEntity(ApplicationEntity entity, SaveAction saveaction, string strID, string strComment, DateTime distDate)
        {
            if (entity.DocNo.ToString().Length == 0)
                throw new EmptyDocNoException();
            SaveComment(entity, saveaction, strID, strComment, distDate);
        }
        public void SaveAssignEntity(ApplicationEntity entity, SaveAction saveaction, string strID, string strAssignTo)
        {
            if (entity.DocNo.ToString().Length == 0)
                throw new EmptyDocNoException();
            SaveAssign(entity, saveaction, strID, strAssignTo);
        }
        protected virtual DataSet LoadData(long headerid)
        {
            return null;
        }
        public virtual void Delete(long headerid)
        {
        }
        protected virtual void SaveData(ApplicationEntity Application, DataSet ds, string strDocName, SaveAction saveaction, string strUpline, string userID, string userName)
        {
        }
        protected virtual void SaveDetail(DataSet ds, SaveAction saveaction)
        { }
        protected virtual void SaveApplicationHistory(ApplicationEntity Application, DataSet ds, SaveAction saveaction, string userID, string userName, DateTime myLastApprove, string myLastState)
        {
        }
        protected virtual void SaveComment(ApplicationEntity Application, SaveAction saveaction, string myUserName, string myComment, DateTime distDate)
        { }
        protected virtual void SaveAssign(ApplicationEntity Application, SaveAction saveaction, string userFullName, string userAssign)
        { }
        protected virtual void DeleteWorkingList(ApplicationEntity Application, string myID)
        { }
        protected virtual void UpdateWorkingList()
        { }
        protected virtual string GetNextStatus(string myStatus)
        {
            return null;
        }
        protected virtual string GetPreviousStatus(string myStatus)
        {
            return null;
        }
        protected virtual void ClearDetail(ApplicationEntity Application, SaveAction saveaction)
        { }
    }
}