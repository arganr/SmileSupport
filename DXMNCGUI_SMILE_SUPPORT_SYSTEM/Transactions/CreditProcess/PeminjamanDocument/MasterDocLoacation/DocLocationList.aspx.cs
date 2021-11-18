using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.PeminjamanDocument.MasterDocLoacation
{
    public partial class DocLocationList : BasePage
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
        protected DataTable mainDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mainDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mainDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable histDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["histDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["histDtTable" + this.ViewState["_PageID"]] = value; }
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
                mainDtTable = new DataTable();
                histDtTable = new DataTable();

                mainDtTable = GetListData();
                gvMain.DataSource = mainDtTable;
                gvMain.DataBind();

                //histDtTable = GetHistData("");
                //gvHistory.DataSource = histDtTable;
                //gvHistory.DataBind();

            }
            if (!IsCallback)
            {

            }
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            isValidLogin(false);
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            string ccode = "";

            switch (callbackParam[0].ToUpper())
            {
                case "HIST":
                    ccode = callbackParam[1].ToString();
                    histDtTable = GetHistData(ccode);
                    break;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("DocLocationEntry.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            if (myrow != null)
            {
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("Key", myrow["DocKey"].ToString());
                updatedQueryString = "?" + nameValues.ToString();
                Response.Redirect("DocLocationEntry.aspx" + updatedQueryString);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                return;
            }
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = mainDtTable;
        }

        protected DataTable GetListData()
        {
            DataTable resDT = new DataTable();
            string ssql = @"select a.*, ISNULL(b.USER_NAME,'') [USER_NAME] from [SSS].[dbo].[mstDocLocation] a left join [SSS].[dbo].[MASTER_USER] b on a.CRE_BY = b.USER_ID 
                left join [INFORMA].[dbo].[UserDokumenCategory] c on a.DocCategory = c.Category where c.UserID =? order by a.CRE_DATE desc";

            resDT = myLocalDBSetting.GetDataTable(ssql, true, UserID);

            return resDT;
        }

        protected DataTable GetHistData(string value)
        {
            DataTable resDT = new DataTable();
            string ssql = "select a.*, b.USER_NAME from [SSS].[dbo].[mstDocLocation_log] a left join [SSS].[dbo].[MASTER_USER] b on a.CRE_BY = b.USER_ID where a.DocID = ?";

            resDT = myLocalDBSetting.GetDataTable(ssql, false, value);
            return resDT;
        }

        protected void gvHistory_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = histDtTable;
        }

        protected void btnApprovalList_Click(object sender, EventArgs e)
        {
            //myApprovalTable = GetApprovalList();
            //gvApprovalList.DataSource = myApprovalTable;
            //gvApprovalList.DataBind();

            //apcApproval.ShowOnPageLoad = true;
        }
    }
}