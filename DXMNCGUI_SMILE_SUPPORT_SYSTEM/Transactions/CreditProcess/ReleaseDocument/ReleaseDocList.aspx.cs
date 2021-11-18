using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.ReleaseDocument
{
    public partial class ReleaseDocList : BasePage
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
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myApprovalTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myWaiveTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myWaiveTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myWaiveTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myReleaseTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myReleaseTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myReleaseTable" + this.ViewState["_PageID"]] = value; }
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
                myApprovalTable = new DataTable();
                myWaiveTable = new DataTable();
                myReleaseTable = new DataTable();

                GetMainTable();
                gvMain.DataBind();

                GetApprovalTable();
                gvApprovalList.DataBind();

                if(myApprovalTable.Rows.Count > 0)
                {
                    btnApprovalList.Text += " (" + Convert.ToString(myApprovalTable.Rows.Count) + ")";
                }
                if (myWaiveTable.Rows.Count > 0)
                {
                    btnWaiveList.Text += " (" + Convert.ToString(myWaiveTable.Rows.Count) + ")";
                }
                if (myReleaseTable.Rows.Count > 0)
                {
                    btnRelease.Text += " (" + Convert.ToString(myReleaseTable.Rows.Count) + ")";
                }


            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            if (myrow != null)
            {
                if(myrow["ReleaseStat"].ToString() == "REQ" && myrow["CreatedBy"].ToString() == UserID)
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", myrow["DocKey"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();

                    nameValues.Set("Action", "Edit");
                    updatedQueryString = "?" + nameValues.ToString();
                    Response.Redirect("RequestReleaseDoc.aspx" + updatedQueryString);
                }else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Cannot Edit this row.." + "');", true);
                    return;
                }
                
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                return;
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            if (myrow != null)
            {
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("Key", myrow["DocKey"].ToString());
                updatedQueryString = "?" + nameValues.ToString();

                nameValues.Set("Action", "View");
                updatedQueryString = "?" + nameValues.ToString();
                Response.Redirect("RequestReleaseDoc.aspx" + updatedQueryString);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                return;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestReleaseDoc.aspx");
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

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }

        protected void GetMainTable()
        {
            //string ssql = "select * from [dbo].[trxReleaseDoc]";
            string ssql = @"select 
	            a.*, 
	            b.USER_NAME [ApproveByName],
	            c.USER_NAME [WaiveApproveByName],
	            d.USER_NAME [CreatedByName]
            from dbo.trxReleaseDoc a
            left join dbo.master_user b on a.ApproveBy = b.USER_ID and b.IS_ACTIVE_FLAG = 1
            left join dbo.master_user c on a.WaiveApproveBy = c.USER_ID and c.IS_ACTIVE_FLAG = 1
            left join dbo.master_user d on a.CreatedBy = d.USER_ID and d.IS_ACTIVE_FLAG = 1
            order by a.CreatedDate desc";
            myMainTable = myLocalDBSetting.GetDataTable(ssql, false);
        }

        protected void GetApprovalTable()
        {
            string ssql = "exec dbo.spSmileSupport_GetApprovalListReleaseDoc ?, ?";

            //ssql = "select *, b.USER_NAME as CreatedUser from trxReleaseDoc a left join MASTER_USER b on a.CreatedBy = b.USER_ID where ReleaseStat = 'REQ'";
            //myApprovalTable = myLocalDBSetting.GetDataTable(ssql, false);

            //ssql = "select *, b.USER_NAME as CreatedUser from trxReleaseDoc a left join MASTER_USER b on a.CreatedBy = b.USER_ID where ReleaseStat = 'WAIVE REQ'";
            //myWaiveTable = myLocalDBSetting.GetDataTable(ssql, false);

            //ssql = "select *, b.USER_NAME as CreatedUser from trxReleaseDoc a left join MASTER_USER b on a.CreatedBy = b.USER_ID where ReleaseStat = 'APPROVED'";
            //myReleaseTable = myLocalDBSetting.GetDataTable(ssql, false);
            
            myApprovalTable = myDBSetting.GetDataTable(ssql, false, "APPROVAL", UserID);
            
            myWaiveTable = myDBSetting.GetDataTable(ssql, false, "WAIVE", UserID);
            
            myReleaseTable = myDBSetting.GetDataTable(ssql, false, "RELEASE", UserID);
        }

        protected void gvApprovalList_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myApprovalTable;
        }

        protected void gvApprovalList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void btnApprovalList_Click(object sender, EventArgs e)
        {
            GetApprovalTable();
            gvApprovalList.DataSource = myApprovalTable;
            gvApprovalList.DataBind();

            apcApproval.ShowOnPageLoad = true;
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string updatedQueryString = "";
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
                case "SHOW":
                    try
                    {
                        DataRow myapprovalrow = gvApprovalList.GetDataRow(gvApprovalList.FocusedRowIndex);

                        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                        nameValues.Set("Key", myapprovalrow["DocKey"].ToString());
                        updatedQueryString = "?" + nameValues.ToString();

                        nameValues.Set("Action", "Approval");
                        updatedQueryString = "?" + nameValues.ToString();
                        //Response.Redirect("RequestReleaseDoc.aspx" + updatedQueryString);
                        ASPxWebControl.RedirectOnCallback("~/Transactions/CreditProcess/ReleaseDocument/RequestReleaseDoc.aspx" + updatedQueryString);
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        return;
                    }
                    break;
                case "SHOW_WAIVE":
                    try
                    {
                        DataRow myapprovalrow = gvWaiveApprovalList.GetDataRow(gvWaiveApprovalList.FocusedRowIndex);

                        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                        nameValues.Set("Key", myapprovalrow["DocKey"].ToString());
                        updatedQueryString = "?" + nameValues.ToString();

                        nameValues.Set("Action", "Approval");
                        updatedQueryString = "?" + nameValues.ToString();
                        //Response.Redirect("RequestReleaseDoc.aspx" + updatedQueryString);
                        ASPxWebControl.RedirectOnCallback("~/Transactions/CreditProcess/ReleaseDocument/RequestReleaseDoc.aspx" + updatedQueryString);
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        return;
                    }
                    break;
                case "SHOW_RELEASE":
                    try
                    {
                        DataRow myapprovalrow = gvReleaseList.GetDataRow(gvReleaseList.FocusedRowIndex);

                        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                        nameValues.Set("Key", myapprovalrow["DocKey"].ToString());
                        updatedQueryString = "?" + nameValues.ToString();

                        nameValues.Set("Action", "Approval");
                        updatedQueryString = "?" + nameValues.ToString();
                        //Response.Redirect("RequestReleaseDoc.aspx" + updatedQueryString);
                        ASPxWebControl.RedirectOnCallback("~/Transactions/CreditProcess/ReleaseDocument/RequestReleaseDoc.aspx" + updatedQueryString);
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        return;
                    }
                    break;
            }
        }

        protected void gvWaiveApprovalList_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myWaiveTable;
        }

        protected void gvWaiveApprovalList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void gvReleaseList_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myReleaseTable;
        }

        protected void gvReleaseList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void btnWaiveList_Click(object sender, EventArgs e)
        {
            GetApprovalTable();
            gvWaiveApprovalList.DataSource = myWaiveTable;
            gvWaiveApprovalList.DataBind();

            apcWaive.ShowOnPageLoad = true;
        }

        protected void btnRelease_Click(object sender, EventArgs e)
        {
            GetApprovalTable();
            gvReleaseList.DataSource = myReleaseTable;
            gvReleaseList.DataBind();

            apcRelease.ShowOnPageLoad = true;
        }
    }
}