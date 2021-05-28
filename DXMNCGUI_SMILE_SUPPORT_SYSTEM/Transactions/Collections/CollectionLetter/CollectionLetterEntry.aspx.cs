using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections.CollectionLetter
{
    public partial class CollectionLetterEntry : BasePage
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
        protected CollectionLetterDB myCollectionLetterDB
        {
            get { isValidLogin(false); return (CollectionLetterDB)HttpContext.Current.Session["myCollectionLetterDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myCollectionLetterDB" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myCollLatterNoTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myCollLatterNoTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myCollLatterNoTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            DataRow myRow;
            DataTable myTable = new DataTable();
            myTable = dbsetting.GetDataTable("SELECT TOP 0 * FROM [dbo].[LS_COLLECTION_LETTER_SEND]", false);
            
            myRow = myTable.NewRow();

            myRow["LETTER_NO"] = luDocNo.Text;
            myRow["LSAGREE"] = txtAgreementNo.Value;
            myRow["SEND_DATE"] = deSendDate.Value;
            myRow["NO_RESI"] = txtSendReNo.Value;
            myRow["KURIR"] = cbCourier.Value;
            myRow["FEE_AMT"] = seCost.Value;
            myRow["TUJUAN_KIRIM"] = cbSendDest.Value;
            myRow["REMARK"] = mmNote.Value;
            myRow["CRE_BY"] = UserID;
            myRow["CRE_DATE"] = dbsetting.GetServerTime();

            myTable.Rows.Add(myRow);

            myCollectionLetterDB.Submit(myTable);
            return bSave;
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            bool focusF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            if (string.IsNullOrEmpty(luDocNo.Text))
            {
                errorF = true;
                luDocNo.IsValid = false;
                luDocNo.ErrorText = "Value can't be empty.";
                if (!focusF)
                {
                    luDocNo.Focus();
                    focusF = true;
                    strmessageError = "Please select 'Surat Peringatan' first.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (string.IsNullOrEmpty(deCollDate.Text))
            {
                errorF = true;
                deCollDate.IsValid = false;
                deCollDate.ErrorText = "Value can't be empty.";
                if (!focusF)
                {
                    deCollDate.Focus();
                    focusF = true;
                    strmessageError = "'surat peringatan' date can't be empty.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (string.IsNullOrEmpty(txtSendReNo.Text))
            {
                errorF = true;
                txtSendReNo.IsValid = false;
                txtSendReNo.ErrorText = "Value can't be empty.";
                if (!focusF)
                {
                    txtSendReNo.Focus();
                    focusF = true;
                    strmessageError = "Sending Receipt No, can't be empty.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (System.Convert.ToDecimal(seCost.Value) == 0)
            {
                errorF = true;
                if (!focusF)
                {
                    seCost.Focus();
                    focusF = true;
                    strmessageError = "Cost value 0 is not allowed.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            //if (luDocNo.Value != null)
            //{
            //    object obj; 
            //    obj = dbsetting.ExecuteScalar("SELECT LETTER_NO FROM [LS_COLLECTION_LETTER_SEND] WHERE LETTER_NO=? AND LSAGREE=?", (object)luDocNo.Value, (object)txtAgreementNo.Value);
            //    if (obj != null && obj != DBNull.Value)
            //    {
            //        if (obj.ToString().Length > 0)
            //        {
            //            errorF = true;
            //            if (!focusF)
            //            {
            //                strmessageError = "duplicate for 'Surat Peringatan' number " + obj.ToString() + ", this number has already inserted.";
            //                cplMain.JSProperties["cplActiveTabIndex"] = 1;
            //            }
            //        }
            //    }
            //}
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
                this.myCollectionLetterDB = CollectionLetterDB.Create(myDBSetting, dbsession);
                myCollLatterNoTable = new DataTable();
                myMainTable = new DataTable();
                myCollLatterNoTable = myCollectionLetterDB.LoadDataCollLetter();
                myMainTable = myCollectionLetterDB.LoadDataMain();
                luDocNo.DataSource = myCollLatterNoTable;
                luDocNo.DataBind();
                gvMain.DataSource = myMainTable;
                gvMain.DataBind();

                deSendDate.Value = dbsetting.GetServerTime();
                deSendDate.MaxDate = dbsetting.GetServerTime();
                deSendDate.MinDate = dbsetting.GetServerTime().AddDays(-7);

                seCost.Value = 0;
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
        protected void luDocNo_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myCollLatterNoTable;
        }

        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }

        protected void gvMain_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
    }
}