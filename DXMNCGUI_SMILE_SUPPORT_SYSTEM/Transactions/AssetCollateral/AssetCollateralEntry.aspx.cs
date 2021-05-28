using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.AssetCollateral
{
    public partial class AssetCollateralEntry : BasePage
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
        protected AssetCollateralDB myAssetCollateralDB
        {
            get { isValidLogin(false); return (AssetCollateralDB)HttpContext.Current.Session["myAssetCollateralDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAssetCollateralDB" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myNoPolTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myNoPolTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myNoPolTable" + this.ViewState["_PageID"]] = value; }
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            DataRow myInsertRow;
            DataRow myUpdateRow;
            DataTable myInsertTable = new DataTable();
            DataTable myUpdateTable = new DataTable();
            myInsertTable = dbsetting.GetDataTable("SELECT * FROM [dbo].[ASSET_COLLATERAL_LOCATION_HIST] WHERE AST_CODE=?", false, "");
            myUpdateTable = dbsetting.GetDataTable("SELECT * FROM [dbo].[ASSET_COLLATERAL_LOCATION] WHERE AST_CODE=?", false, "");

            myInsertRow = myInsertTable.NewRow();
            myInsertRow["AST_CODE"] = txtAssetCode.Value;
            myInsertRow["FROM_LOC"] = txtFromLoc.Value;
            myInsertRow["TO_LOC"] = cbToLoc.Value;
            myInsertRow["CRE_DATE"] = dbsetting.GetServerTime();
            myInsertRow["CRE_BY"] = this.UserName;
            myInsertRow["DATE"] = deAcceptDate.Value;
            myInsertRow["PROMISE_DATE"] = dePromiseDate.Value != null ? dePromiseDate.Value : DBNull.Value;
            myInsertTable.Rows.Add(myInsertRow);

            myUpdateRow = myUpdateTable.NewRow();
            myUpdateRow["AST_CODE"] = txtAssetCode.Value;
            myUpdateRow["LOCATION"] = cbToLoc.Value;
            myUpdateRow["DETAIL_LOCATION"] = mmDetailLoc.Value;
            myUpdateRow["DATE"] = deAcceptDate.Value;
            myUpdateRow["REMARKS"] = mmRemark.Value;
            myUpdateRow["PROMISE_DATE"] = dePromiseDate.Value != null ? dePromiseDate.Value : DBNull.Value;
            myUpdateRow["CRE_DATE"] = DBNull.Value;
            myUpdateRow["CRE_BY"] = DBNull.Value;
            myUpdateRow["MOD_DATE"] = dbsetting.GetServerTime();
            myUpdateRow["MOD_BY"] = this.UserName;
            myUpdateTable.Rows.Add(myUpdateRow);

            myAssetCollateralDB.Save(myInsertTable, myUpdateTable);
            return bSave;
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            bool focusF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            if (string.IsNullOrEmpty(luNoPol.Text))
            {
                errorF = true;
                luNoPol.IsValid = false;
                luNoPol.ErrorText = "Value can't be empty.";
                if (!focusF)
                {
                    luNoPol.Focus();
                    focusF = true;
                    strmessageError = "Please select police license first.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (string.IsNullOrEmpty(cbToLoc.Text))
            {
                errorF = true;
                cbToLoc.IsValid = false;
                cbToLoc.ErrorText = "Value can't be empty.";
                if (!focusF)
                {
                    cbToLoc.Focus();
                    focusF = true;
                    strmessageError = "Please select new location first.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (txtFromLoc.Text == cbToLoc.Text)
            {
                errorF = true;
                cbToLoc.IsValid = false;
                if (!focusF)
                {
                    cbToLoc.Focus();
                    focusF = true;
                    strmessageError = "New location must be different with current location.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (cbToLoc.Text != "LOCKER HO")
            {
                if(dePromiseDate.Text == "")
                {
                    errorF = true;
                    dePromiseDate.IsValid = false;
                    if (!focusF)
                    {
                        dePromiseDate.Focus();
                        focusF = true;
                        strmessageError = "Promise return date is mandatory for any location except 'LOCKER HO'.";
                        cplMain.JSProperties["cplActiveTabIndex"] = 1;
                    }
                }
            }
            return errorF;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myDBSession = dbsession;
                this.myAssetCollateralDB = AssetCollateralDB.Create(myDBSetting, dbsession);
                myNoPolTable = new DataTable();
                myNoPolTable = myAssetCollateralDB.LoadDataNoPol();
                luNoPol.DataSource = myNoPolTable;
                luNoPol.DataBind();
                deAcceptDate.Value = dbsetting.GetServerTime();
            }
            if (!IsCallback)
            {

            }
        }
        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
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
                    break;
                case "SAVECONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to save?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    if (ErrorInField(out strmessageError, SaveAction.Submit))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "SAVE":
                    Save(SaveAction.Save);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
            }
        }
        protected void luNoPol_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myNoPolTable;
        }
    }
}