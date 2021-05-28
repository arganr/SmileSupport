using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections.CollectionLetter
{
    public class CollectionLetterDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myCollLetterTable;
        protected DataTable myMainTable;
        protected Controllers.Registry.DBRegistry myDBReg;
        internal CollectionLetterDB()
        {
            myCollLetterTable = new DataTable();
            myMainTable = new DataTable();
        }
        public static CollectionLetterDB Create(SqlDBSetting dbSetting, SqlDBSession dbSession)
        {
            CollectionLetterDB aCollLetter = (CollectionLetterDB)null; ;
            aCollLetter = new CollectionLetterSQL();
            aCollLetter.myDBSetting = dbSetting;
            aCollLetter.myDBSession = dbSession;
            return aCollLetter;
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
        public virtual DataTable LoadDataCollLetter()
        {
            return null;
        }
        public virtual DataTable LoadDataMain()
        {
            return null;
        }
        public virtual void Submit(DataTable myTable)
        {

        }
    }
}