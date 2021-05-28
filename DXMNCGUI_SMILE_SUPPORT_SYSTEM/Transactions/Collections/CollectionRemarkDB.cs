using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections
{
    public class CollectionRemarkDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myNoKontrakTable;
        protected Controllers.Registry.DBRegistry myDBReg;
        internal CollectionRemarkDB()
        {
            myNoKontrakTable = new DataTable();
        }
        public static CollectionRemarkDB Create(SqlDBSetting dbSetting, SqlDBSession dbSession)
        {
            CollectionRemarkDB aCollRemark = (CollectionRemarkDB)null; ;
            aCollRemark = new CollectionRemarkSQL();
            aCollRemark.myDBSetting = dbSetting;
            aCollRemark.myDBSession = dbSession;
            return aCollRemark;
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
        public virtual DataTable LoadDataKontrak()
        {
            return null;
        }
        public virtual void Submit(string sID, string slsagree, int iactionid, string sremark, string spromisetopay)
        {
           
        }
    }
}