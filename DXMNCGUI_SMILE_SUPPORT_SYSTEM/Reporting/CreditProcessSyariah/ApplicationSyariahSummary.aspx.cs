using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcessSyariah
{
    public partial class ApplicationSyariahSummary : BasePage
    {
        DateTime datenow = DateTime.Now;
        public List<int> MergedIndexList = new List<int>();
        protected SqlLocalDBSetting myLocalDBSetting
        {
            get { isValidLogin(); return (SqlLocalDBSetting)HttpContext.Current.Session["myLocalDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myLocalDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myStatusTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myStatusTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myStatusTable" + this.ViewState["_PageID"]] = value; }
        }
        protected string myWhereString
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["myWhereString" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myWhereString" + this.ViewState["_PageID"]] = value; }
        }

        DataTable LoadDataHeader()
        {
            string sQuery = "";
            DataTable myTable = new DataTable();
            sQuery = @"CREATE TABLE #MyTempTable01
                        (
                        APPLICNO Varchar(100),
                        MKTCODE Varchar(20),
                        [STATUS] Varchar(50),
                        [BRANCH]  Varchar(100)
                        )

                        INSERT INTO #MyTempTable01
                        SELECT 
                        T1A.APPLICNO,
                        T1A.MKTCODE,
                        (SELECT TOP 1 STEP FROM MNCL_APP_TIME_STAMP WHERE APPLICNO=T1A.APPLICNO ORDER BY MNCL_APP_TIME_STAMP.CRE_DATE DESC) AS STATUS,
                        T1C.C_NAME
                        FROM 
                        LS_APPLICATION T1A
                        INNER JOIN SYS_TBLEMPLOYEE T1B ON T1A.MKTCODE=T1B.CODE
                        INNER JOIN SYS_COMPANY T1C ON T1B.C_CODE = T1C.C_CODE
                        WHERE T1A.CRE_DATE > '2021/01/01' AND T1A.CAMPAIGN_CODE='PCKGHJREG'

                        SELECT 
                        C.C_NAME AS CABANG,
                        COUNT(T1.APPLICNO) AS 'JUMLAH-APLIKASI',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='APPIN') AS 'APPIN',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='SLIK-CHECKING') AS 'SLIK-CHECKING',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='CREDIT-COMMITTEE') AS 'CREDIT-COMMITTEE',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='LEGAL-SIGNING CONTRACT') AS 'LEGAL-SIGNING CONTRACT',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='LEGAL-CHECKLIST') AS 'LEGAL-CHECKLIST',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='LEGAL-FIRST-PAYMENT') AS 'LEGAL-FIRST-PAYMENT',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='PEMBUKAAN BTH') AS 'PEMBUKAAN BTH',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='READY-TO-GOLIVE') AS 'READY-TO-GOLIVE',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='GOLIVE') AS 'GOLIVE',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='READY-DISBURSE') AS 'READY-DISBURSE',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='DISBURSE') AS 'DISBURSE',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='BPIH') AS 'BPIH',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='SPPH') AS 'SPPH',
                        (SELECT ISNULL(COUNT(STATUS),0) FROM #MyTempTable01 WHERE BRANCH=C.C_NAME AND STATUS='DONE') AS 'DONE'
                        FROM
                        SYS_TBLEMPLOYEE A
                        INNER JOIN
                        (
                        SELECT 
                        T1A.APPLICNO,
                        T1A.MKTCODE,
                        (SELECT TOP 1 STEP FROM MNCL_APP_TIME_STAMP WHERE APPLICNO=T1A.APPLICNO ORDER BY MNCL_APP_TIME_STAMP.CRE_DATE DESC) AS STATUS,
                        T1C.C_NAME
                        FROM 
                        LS_APPLICATION T1A
                        INNER JOIN SYS_TBLEMPLOYEE T1B ON T1A.MKTCODE=T1B.CODE
                        INNER JOIN SYS_COMPANY T1C ON T1A.C_CODE = T1C.C_CODE
                        WHERE T1A.CRE_DATE > '2021/01/01' AND T1A.CAMPAIGN_CODE='PCKGHJREG'
                        ) 
                        T1 ON A.CODE = T1.MKTCODE
                        INNER JOIN SYS_COMPANY C ON A.C_CODE = C.C_CODE
                        GROUP BY C.C_NAME

                        DROP TABLE #MyTempTable01";
            myTable = myDBSetting.GetDataTable(sQuery, false, "");
            return myTable;
        }
        DataTable LoadStatusTable()
        {
            string sQuery = "";
            DataTable myTable = new DataTable();
            sQuery = @"SELECT STEP FROM  MNCL_APP_TIME_STAMP
                       WHERE CRE_DATE >= '2021/01/01'
                       GROUP BY STEP";
            myTable = myDBSetting.GetDataTable(sQuery, false, "");
            return myTable;
        }
        private string GetFilterString()
        {
            string myString = "";
            return myString;
        }
        private void refreshdatagrid()
        {
            myMainTable = LoadDataHeader();
            gvMain.DataBind();
        }
        private void setPageControl()
        {
            myStatusTable = LoadStatusTable();
            cbStatus.DataSource = myStatusTable;
            cbStatus.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
                myMainTable = new DataTable();
                //myWhereString = GetFilterString();
                refreshdatagrid();
                //setPageControl();
            }
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            isValidLogin(false);
            string[] callbackParam = e.Parameter.ToString().Split(';');
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];
            string strmessageError = string.Empty;
            switch (callbackParam[0].ToUpper())
            {
                case "INQUIRY":
                    myWhereString = GetFilterString();
                    break;
            }
        }

        protected void cbStatus_DataBinding(object sender, EventArgs e)
        {

        }

        protected void gvMain_Init(object sender, EventArgs e)
        {

        }
        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvMain_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

        }
    }
}