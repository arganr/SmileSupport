using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.FPPVerification
{
    public class FPPVerificationDB
    {
        protected internal MySqlDBSetting myMySqlDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myBrowseTable;
        internal FPPVerificationDB()
        {
            myBrowseTable = new DataTable();
        }
        public DataTable BrowseTable
        {
            get { return myBrowseTable; }
        }
        public MySqlDBSetting MySqlDBSetting
        {
            get { return myMySqlDBSetting; }
        }
        public static FPPVerificationDB Create(MySqlDBSetting dbSetting, SqlDBSession dbSession)
        {
            FPPVerificationDB aFPPVerification = (FPPVerificationDB)null;
            aFPPVerification = new FPPVerificationSql();
            aFPPVerification.myMySqlDBSetting = dbSetting;
            aFPPVerification.myDBSession = dbSession;
            return aFPPVerification;
        }
        public FPPVerificationEntity View(long headerid)
        {
            return this.InternalView(this.LoadData(headerid));
        }
        private FPPVerificationEntity InternalView(DataSet newDataSet)
        {
            if (newDataSet.Tables["Header"].Rows.Count == 0)
            {
                return (FPPVerificationEntity)null;
            }
            else
            {
                long docKey = Convert.ToInt64(newDataSet.Tables["Header"].Rows[0]["cust_prospect_id"]);
                return new FPPVerificationEntity(this, newDataSet, DXSSAction.View);
            }
        }
        public virtual DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            return null;
        }
        protected virtual DataSet LoadData(long headerid)
        {
            return null;
        }
    }
}