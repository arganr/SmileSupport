using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditAnalyst
{
    public class AccountInquiryDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myMainTable;
        protected DataTable myDetailtable;
        protected Controllers.Registry.DBRegistry myDBReg;
        internal AccountInquiryDB()
        {
            myMainTable = new DataTable();
            myDetailtable = new DataTable();
        }
        public static AccountInquiryDB Create(SqlDBSetting dbSetting, SqlDBSession dbSession)
        {
            AccountInquiryDB aAccountInquiry = (AccountInquiryDB)null;
            aAccountInquiry = new AccountInquirySQL();
            aAccountInquiry.myDBSetting = dbSetting;
            aAccountInquiry.myDBSession = dbSession;
            return aAccountInquiry;
        }
        public SqlDBSetting DBSetting
        {
            get { return myDBSetting; }
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
        public virtual DataTable LoadMainTable()
        {
            return null;
        }
    }
}