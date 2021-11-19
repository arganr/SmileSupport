using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.SPD
{
    public partial class PerjalananDinasEntry : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlLocalDBSetting myLocalDBSetting
        {
            get { isValidLogin(false); return (SqlLocalDBSetting)HttpContext.Current.Session["myLocalDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myLocalDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]] = value; }
        }
        protected PerjalananDinasDB myPerjalananDinasDB
        {
            get { isValidLogin(false); return (PerjalananDinasDB)HttpContext.Current.Session["myPerjalananDinasDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myPerjalananDinasDB" + this.ViewState["_PageID"]] = value; }
        }
        protected PerjalananDinasEntity myPerjalananDinasEntity
        {
            get { isValidLogin(false); return (PerjalananDinasEntity)HttpContext.Current.Session["myPerjalananDinasEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myPerjalananDinasEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myHeaderTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myHeadeTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myHeadeTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDetailTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myBudgetTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myBudgetTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myBudgetTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myApprovalTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataSet myds
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myds" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myds" + this.ViewState["_PageID"]] = value; }
        }
        protected DXSSAction myAction
        {
            get { isValidLogin(false); return (DXSSAction)HttpContext.Current.Session["myAction" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAction" + this.ViewState["_PageID"]] = value; }
        }
        protected string strKey
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]] = value; }
        }
        protected string strStatus
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strStatus" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strStatus" + this.ViewState["_PageID"]] = value; }
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

                if (this.Request.QueryString["SourceKey"] != null && this.Request.QueryString["Type"] != null)
                {
                    this.myPerjalananDinasDB = PerjalananDinasDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                    myPerjalananDinasEntity = this.myPerjalananDinasDB.View(Convert.ToInt32(this.Request.QueryString["SourceKey"]));
                }
                myds = new DataSet();
                this.myPerjalananDinasDB = PerjalananDinasDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((PerjalananDinasEntity)HttpContext.Current.Session["myPerjalananDinasEntity" + strKey]);
            }
            if (!IsCallback)
            {

            }
        }

        private void SetApplication(PerjalananDinasEntity PerjalananDinasEntity)
        {
            if (this.myPerjalananDinasEntity != PerjalananDinasEntity)
            {
                if (PerjalananDinasEntity != null)
                {
                    this.myPerjalananDinasEntity = PerjalananDinasEntity;
                }
                myAction = this.myPerjalananDinasEntity.Action;
                myds = myPerjalananDinasEntity.myDataSet;
                myHeaderTable = myds.Tables[0];
                myDetailTable = myds.Tables[1];
                myBudgetTable = myds.Tables[2];
                myApprovalTable = myds.Tables[3];
            }
        }

        private void BindingMaster()
        {
            txtName.Value = myPerjalananDinasEntity.Name;
            txtNIK.Value = myPerjalananDinasEntity.NIK;
            txtDept.Value = myPerjalananDinasEntity.Dept;

            if (myAction == DXSSAction.Edit)
            {

            }
        }

        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            myPerjalananDinasEntity.Name = txtName.Value;
            myPerjalananDinasEntity.NIK = txtNIK.Value;
            myPerjalananDinasEntity.Dept = txtDept.Value;
            myPerjalananDinasEntity.Status = strStatus;
            
            if (myAction == DXSSAction.New)
            {
                myPerjalananDinasEntity.CRE_BY = UserID;
                myPerjalananDinasEntity.CRE_DATE = myLocalDBSetting.GetServerTime();
                myPerjalananDinasEntity.MOD_BY = UserID;
                myPerjalananDinasEntity.MOD_DATE = myLocalDBSetting.GetServerTime();
            }
            myPerjalananDinasEntity.Save(this.UserID, this.UserName, saveAction);

            return bSave;
        }
        
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;

            return errorF;
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            isValidLogin(false);
            string urlsave = "";
            urlsave = "~/Transactions/Finance/SPD/PerjalananDinasList.aspx";
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
                case "REFRESH":
                    break;
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
                case "APPROVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to approve this?";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    if (ErrorInField(out strmessageError, SaveAction.Approve))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "APPROVE":
                    Save(SaveAction.Approve);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been approve...";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
                case "REJECT_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to reject this?";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    if (ErrorInField(out strmessageError, SaveAction.Reject))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "REJECT":
                    Save(SaveAction.Reject);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been reject...";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
            }
        }
    }
}