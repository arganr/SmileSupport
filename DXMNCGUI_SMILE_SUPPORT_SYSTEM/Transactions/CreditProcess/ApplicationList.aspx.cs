using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess
{
    public partial class ApplicationList : BasePage
    {
        DateTime datenow = DateTime.Now;
        public List<int> MergedIndexList = new List<int>();
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
        protected ApplicationDB myApplicationDB
        {
            get { isValidLogin(false); return (ApplicationDB)HttpContext.Current.Session["myApplicationDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApplicationDB" + this.ViewState["_PageID"]] = value; }
        }
        protected ApplicationEntity myApplicationEntity
        {
            get { isValidLogin(false); return (ApplicationEntity)HttpContext.Current.Session["myApplicationEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApplicationEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myHistoryTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myHistoryTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myHistoryTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myCommentTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myCommentTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myCommentTable" + this.ViewState["_PageID"]] = value; }
        }
        protected void gvMain_Init(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();

                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
                myMainTable = new DataTable();
                this.myApplicationDB = ApplicationDB.Create(myLocalDBSetting, dbsession);
                if (!accessright.IsAccessibleByUserID(UserID, "VIEW_ALL_APPLICATION"))
                {
                    myMainTable = this.myApplicationDB.LoadBrowseTable(true, myDBSession.LoginUserID);
                }
                else
                {
                    myMainTable = this.myApplicationDB.LoadBrowseTable(true, myDBSession.LoginUserID);
                }
                gvMain.DataBind();
                accessable();

                if (this.Request.QueryString["DocKey"] != null)
                {
                    int indexVal = gvMain.FindVisibleIndexByKeyValue(this.Request.QueryString["DocKey"]);
                    gvMain.FocusedRowIndex = indexVal;
                }
                else
                {
                    gvMain.FocusedRowIndex = -1;
                }
                refreshdatagrid();
                setEnabledButton();
            }
        }
        protected void accessable()
        {
            if (!accessright.IsAccessibleByUserID(UserID, "TICKET_CAN_GRAB"))
            {

            }
        }
        private void refreshdatagrid()
        {
            myMainTable = this.myApplicationDB.LoadBrowseTable(false, myDBSession.LoginUserID);
            gvMain.DataBind();
        }
        private void setEnabledButton()
        {
   
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                myApplicationEntity = myApplicationDB.Entity(DXSSType.APP);
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                updatedQueryString = "?" + nameValues.ToString();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
            Response.Redirect("~/Transactions/CreditProcess/ApplicationEntry.aspx" + updatedQueryString);
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                if (myrow != null)
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();
                    myApplicationEntity = myApplicationDB.View(System.Convert.ToInt32(myrow["DocKey"]));
                    Response.Redirect("~/Transactions/CreditProcess/ApplicationEntry.aspx" + updatedQueryString);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                if (myrow != null)
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();
                    myApplicationEntity = myApplicationDB.Edit(System.Convert.ToInt32(myrow["DocKey"]), DXSSAction.Edit);
                    Response.Redirect("~/Transactions/CreditProcess/ApplicationEntry.aspx" + updatedQueryString);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
        }
        //protected void btnRefresh_Click(object sender, EventArgs e)
        //{
            
        //}
        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
        protected void gvMain_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void gvHistory_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myHistoryTable;
        }
        protected void gvHistory_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            string[] callbackParam = e.Parameters.ToString().Split(';');
            gridView.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1].ToUpper();
            switch (callbackParam[0].ToUpper())
            {
                case "LOAD_HISTORY":
                    myHistoryTable = this.myApplicationDB.LoadBrowseTableHistory(paramValue.ToString());
                    gvHistory.DataBind();
                    break;
            }
        }       
        protected void gvHistory_CustomCellMerge(object sender, ASPxGridViewCustomCellMergeEventArgs e)
        {
            if (e.Column.FieldName == "TransBy")
            {
                e.Handled = true;
                if ((string)e.Value1 == (string)e.Value2)
                {
                    MergedIndexList.Add(e.RowVisibleIndex1);
                    e.Merge = true;
                }
            }
            else
            { e.Handled = true; e.Merge = false; }
        }
        protected void gvComment_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            string[] callbackParam = e.Parameters.ToString().Split(';');
            gridView.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1].ToUpper();
            switch (callbackParam[0].ToUpper())
            {
                case "LOAD_COMMENT":
                    myCommentTable = this.myApplicationDB.LoadBrowseTableComment(paramValue.ToString());
                    gvComment.DataBind();
                    break;
            }
        }
        protected void gvComment_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myCommentTable;
        }
        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();

            switch (callbackParam[0].ToUpper())
            {
                case "REFRESH":
                    refreshdatagrid();
                    break;
            }
        }
    }
}