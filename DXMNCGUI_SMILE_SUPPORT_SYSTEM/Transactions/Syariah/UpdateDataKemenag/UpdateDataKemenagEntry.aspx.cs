using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.UpdateDataKemenag
{
    public partial class UpdateDataKemenagEntry : BasePage
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
        protected DataTable myKemenagTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myKemenagTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myKemenagTable" + this.ViewState["_PageID"]] = value; }
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
            ASPxGridView gridKemenag = luKemenag.GridView;
            object value1 = gridKemenag.GetRowValues(gridKemenag.FocusedRowIndex, new string[] { "SUPP_CODE" });

            myDBSetting.ExecuteNonQuery("UPDATE LS_APPLICATION SET SUPP_CODE=? WHERE APPLICNO=?", value1, luAppNo.Value);

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
            urlsave = "~/Transactions/Syariah/UpdateDataKemenag/UpdateDataKemenagEntry.aspx";
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

        protected void luKemenag_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myKemenagTable;
        }
        protected void luKemenag_Init(object sender, EventArgs e)
        {
            ASPxGridLookup lookup = (ASPxGridLookup)sender;
            ASPxGridView gridView = lookup.GridView;
            gridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(luKemenag_CustomCallback);
        }
        void luKemenag_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = luAppNo.GridView;
            object value = grid.GetRowValues(grid.FocusedRowIndex, new string[] { "C_CODE" });

            myKemenagTable = new DataTable();
            myKemenagTable = myDBSetting.GetDataTable(@"SELECT SUPP_CODE, SUPP_NAME FROM SYS_TBLSUPPLIER WHERE C_CODE=?", false, value);
            luKemenag.DataSource = myKemenagTable;
            luKemenag.DataBind();
        }
    }
}