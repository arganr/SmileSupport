using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections
{
    public partial class FormCollectionRemarkEntry : BasePage
    {      
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]] = value; }
        }
        protected CollectionRemarkDB myCollectionRemarkDB
        {
            get { isValidLogin(false); return (CollectionRemarkDB)HttpContext.Current.Session["myCollectionRemarkDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myCollectionRemarkDB" + this.ViewState["_PageID"]] = value; }
        }
        protected CollectionRemarkEntity myCollectionRemarkEntity
        {
            get { isValidLogin(false); return (CollectionRemarkEntity)HttpContext.Current.Session["myCollectionRemarkEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myCollectionRemarkEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myAccNoTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myAccNoTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAccNoTable" + this.ViewState["_PageID"]] = value; }
        }
        public DataTable LoadCategorySubTable()
        {
            string strQuery = "SELECT APPLICNO, APPLICDT, NAME FROM LS_AGREEMENT ORDER BY APPLICDT DESC";
            myAccNoTable = myDBSetting.GetDataTable(strQuery, false, "");
            return myAccNoTable;
        }
        private void SetupLookupEdit()
        {

        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            myCollectionRemarkDB.Submit(UserID, Convert.ToString(luAccNo.Value), Convert.ToInt32(cbAction.Value), Convert.ToString(mmRemark.Value), Convert.ToString(dePromisePayDate.Value));
            return bSave;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myDBSession = dbsession;
                this.myCollectionRemarkDB = CollectionRemarkDB.Create(myDBSetting, dbsession);
                myAccNoTable = new DataTable();
                myAccNoTable = myCollectionRemarkDB.LoadDataKontrak();
                luAccNo.DataSource = myAccNoTable;
                luAccNo.DataBind();

                deCollDate.Value = dbsetting.GetServerTime();
                deCollDate.MaxDate = dbsetting.GetServerTime();
                deCollDate.MinDate = dbsetting.GetServerTime().AddDays(-4);

                dePromisePayDate.Value = DBNull.Value;
                dePromisePayDate.MinDate = dbsetting.GetServerTime().AddDays(-1);
            }
            if (!IsCallback)
            {

            }
        }    
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            bool focusF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            if (string.IsNullOrEmpty(luAccNo.Text))
            {
                errorF = true;
                luAccNo.IsValid = false;
                luAccNo.ErrorText = "Value can't be empty.";
                if (!focusF)
                {
                    luAccNo.Focus();
                    focusF = true;
                    strmessageError = "Contract No, can't be empty.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (string.IsNullOrEmpty(cbAction.Text))
            {
                errorF = true;
                cbAction.IsValid = false;
                cbAction.ErrorText = "Value can't be empty.";
                if (!focusF)
                {
                    cbAction.Focus();
                    focusF = true;
                    strmessageError = "Action, can't be empty.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (cbAction.Text.Contains("Promise to pay / Berjanji membayar pada tanggal tertentu"))
            {
                if (string.IsNullOrEmpty(dePromisePayDate.Text))
                {
                    errorF = true;
                    dePromisePayDate.IsValid = false;
                    dePromisePayDate.ErrorText = "Value can't be empty.";
                    if (!focusF)
                    {
                        dePromisePayDate.Focus();
                        focusF = true;
                        strmessageError = "Promise date, can't be empty.";
                        cplMain.JSProperties["cplActiveTabIndex"] = 1;
                    }
                }
            }
            return errorF;
        }       
        protected void luAccNo_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myAccNoTable;
        }
        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string urlsave = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            urlsave = "~/Default.aspx";
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];
            string hexColor = "#FFFF99";
            string roColor = "#EBEBEB";
            string strmessageError = string.Empty;
            DateTime mydate = myDBSetting.GetServerTime();
            System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(hexColor);
            System.Drawing.Color rocolor = System.Drawing.ColorTranslator.FromHtml(roColor);

            switch (callbackParam[0].ToUpper())
            {
                case "ACTION":
                    cplMain.JSProperties["cpVisible"] = null;
                    if (paramValue.ToString().ToUpper() == "121")
                    {
                        cplMain.JSProperties["cpVisible"] = true;
                    }
                    else
                    {
                        cplMain.JSProperties["cpVisible"] = false;
                    }
                    break;
                case "SAVECONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to submit this?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    if (ErrorInField(out strmessageError, SaveAction.Submit))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "SAVE":
                    Save(SaveAction.Submit);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
            }
        }
    }
}