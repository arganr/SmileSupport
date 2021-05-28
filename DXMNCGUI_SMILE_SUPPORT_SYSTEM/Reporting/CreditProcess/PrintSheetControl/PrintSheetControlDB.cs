using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.PrintSheetControl
{
    public class PrintSheetControlDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myControlSheetTable;
        protected DataTable myNoKontrakTable;
        protected DataTable myDocMandTable;
        protected DataTable myDocAddTable;
        protected DataTable mySheetControlTable;
        protected Controllers.Registry.DBRegistry myDBReg;
        internal PrintSheetControlDB()
        {
            myControlSheetTable = new DataTable();
            myNoKontrakTable = new DataTable();
            myDocMandTable = new DataTable();
            myDocAddTable = new DataTable();
            mySheetControlTable = new DataTable();
        }
        public static PrintSheetControlDB Create(SqlDBSetting dbSetting, SqlLocalDBSetting dblocalsetting, SqlDBSession dbSession)
        {
            PrintSheetControlDB aPrintSheetControl = (PrintSheetControlDB)null; ;
            aPrintSheetControl = new PrintSheetControlSQL();
            aPrintSheetControl.myDBSetting = dbSetting;
            aPrintSheetControl.myLocalDBSetting = dblocalsetting;
            aPrintSheetControl.myDBSession = dbSession;
            return aPrintSheetControl;
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
        public Controllers.Registry.DBRegistry DBReg
        {
            get
            {
                return this.myDBReg;
            }
        }
        public virtual DataTable LoadDataSheetControl()
        {
            return null;
        }
        public virtual DataTable LoadDataKontrak()
        {
            return null;
        }
        public virtual DataTable LoadDataDocMand()
        {
            return null;
        }
        public virtual DataTable LoadDataDocAdd()
        {
            return null;
        }
        public void Save(DataTable dt, DataTable dtClient, DataTable dtAkteNotaris, DataTable dtPengurus, DataTable dtPemegangSaham, DataTable dtDetailAsset, SaveAction saveaction, string userName)
        {
            SaveSheetControl(dt, dtClient, dtAkteNotaris, dtPengurus, dtPemegangSaham, dtDetailAsset, saveaction, userName);
        }
        public void Update(DataTable dt, DataTable dtClient, DataTable dtAkteNotaris, DataTable dtPengurus, DataTable dtPemegangSaham, DataTable dtDetailAsset, SaveAction saveaction, string userName)
        {
            UpdateSheetControl(dt, saveaction, userName);
            DeleteSheetControlDetail(dt, saveaction, userName);
            SaveSheetControl(dt, dtClient, dtAkteNotaris, dtPengurus, dtPemegangSaham, dtDetailAsset, saveaction, userName);
        }
        public void SendComment(DataTable dt, SaveAction saveaction, string userName)
        {
            SendCommentSheetControl(dt, saveaction, userName);
        }
        protected virtual void SaveSheetControl(DataTable dt, DataTable dtClient, DataTable dtAkteNotaris, DataTable dtPengurus, DataTable dtPemegangSaham, DataTable dtDetailAsset, SaveAction saveaction, string userName)
        { }
        protected virtual void UpdateSheetControl(DataTable dt, SaveAction saveaction, string userName)
        {}
        protected virtual void DeleteSheetControlDetail(DataTable dt, SaveAction saveaction, string userName)
        { }
        protected virtual void SendCommentSheetControl(DataTable dt, SaveAction saveaction, string userName)
        { }
        public virtual DataSet LoadDataClient(string ClientID)
        {
            return null;
        }
    }
}