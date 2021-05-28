using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.AddManualDeviation
{
    public partial class AddManualDeviation : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlLocalDBSetting myLocalDBSetting
        {
            get { isValidLogin(); return (SqlLocalDBSetting)HttpContext.Current.Session["myLocalDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myLocalDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
            }
            sdsApplicNo.ConnectionString = GetSqlConnectionString();
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;

            string ipAdd = "";
            if (HttpContext.Current != null)
            {
                var Request = HttpContext.Current.Request;
                ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAdd))
                {
                    ipAdd = Request.ServerVariables["REMOTE_ADDR"];
                }
            }

            DateTime mydt = myDBSetting.GetServerTime();
            SqlDBSetting mytransdbsetting = myDBSetting.StartTransaction();
            try
            {
                mytransdbsetting.ExecuteNonQuery(@"INSERT INTO LS_APPLIDEVIATION (APPLICNO, TYPE, DESCRIPTION, CRE_DATE, CRE_BY, CRE_IP_ADDRESS, MOD_DATE, MOD_BY, MOD_IP_ADDRESS) VALUES (?,?,?,?,?,?,?,?,?)", luAppNo.Value, cbJenisDeviasi.Value, mmDescription.Value, mydt, this.UserID, ipAdd, mydt, this.UserID, ipAdd);
                mytransdbsetting.Commit();
            }
            catch (Exception ex)
            {
                mytransdbsetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                mytransdbsetting.EndTransaction();
            }
            return bSave;
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            return errorF;
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string urlsave = "";
            urlsave = "~/Transactions/Syariah/AddManualDeviation/AddManualDeviation.aspx";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlLocalDBSetting localdbsetting = this.myLocalDBSetting;

            string strmessageError = "";

            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];

            switch (callbackParam[0].ToUpper())
            {
                case "SAVE_CONFIRM":
                    object paramValue2 = callbackParam[2];
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to save this data?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "SAVE":
                    Save(SaveAction.Save);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
            }
        }

        protected void luAppNo_DataBinding(object sender, EventArgs e)
        {

        }
        protected void luAppNo_Init(object sender, EventArgs e)
        {

        }
    }
}