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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.PeminjamanDocument
{
    public partial class PeminjamanDocList : BasePage
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
        protected DataTable myDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myApprovalTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApprovalTable" + this.ViewState["_PageID"]] = value; }
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
                myDtTable = new DataTable();

                bool UserAccessable = IsAccessable();
                if (UserAccessable)
                {
                    btnMasterDocLoc.ClientVisible = true;
                }

                myDtTable = GetPeminjamanList();
                gvMain.DataSource = myDtTable;
                gvMain.DataBind();

                object obj = this.myLocalDBSetting.ExecuteScalar("select Count(1) from  [INFORMA].[dbo].[PeminjamanDokumen] a inner join [INFORMA].[dbo].[UserDokumenCategory] b on a.DocCategory = b.Category where b.UserID = ? and b.IsApprover = 'T' and a.Status = 'WAITING CUSTODIAN'", this.UserID);
                if (obj != null && obj != DBNull.Value)
                {
                    if (Convert.ToInt32(obj) != 0)
                        btnApprovalList.Text += " (" + Convert.ToString(obj) + ")";
                }
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

            switch (callbackParam[0].ToUpper())
            {

                case "SHOW":
                    try
                    {
                        string updatedQueryString = "";
                        DataRow myapprovalrow = gvApprovalList.GetDataRow(gvApprovalList.FocusedRowIndex);
                        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                        nameValues.Set("Key", Convert.ToString(myapprovalrow["DocKey"]));
                        updatedQueryString = "?" + nameValues.ToString();
                        ASPxWebControl.RedirectOnCallback("~/Transactions/CreditProcess/PeminjamanDocument/PeminjamanDocEntry.aspx" + updatedQueryString);
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        return;
                    }
                    break;
            }
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myDtTable;
        }

        protected DataTable GetPeminjamanList()
        {
            string ssql = @"SELECT a.DocKey, a.DocNo, a.DocDate, b.DocID, b.Description, a.Status, a.DocCategory, a.TglPeminjaman, a.TglPengembalian, d.USER_NAME CreatedBy, a.CreatedDateTime 
                FROM [INFORMA].[dbo].[PeminjamanDokumen] a left join[INFORMA].[dbo].[PeminjamanDokumenDetail] b on a.DocKey = b.DocKey left join [INFORMA].[dbo].[UserDokumenCategory] c on a.DocCategory = c.Category
                left join [SSS].[dbo].[MASTER_USER] d on a.CreatedBy = d.USER_ID
                WHERE a.Status NOT IN('CLOSE','WAITING CUSTODIAN','REJECT BY CUSTODIAN') AND c.UserID =?";
            DataTable resDT = new DataTable();
            resDT = myLocalDBSetting.GetDataTable(ssql, false, UserID);
            return resDT;
        }

        protected DataTable getHistoryByDocID(string id)
        {
            string ssql = "select * from [INFORMA].[dbo].[PeminjamanDokumenHistory] where DocID = ?";
            DataTable resDT = new DataTable();
            resDT = myLocalDBSetting.GetDataTable(ssql, false, id);
            return resDT;
        }

        protected bool IsAccessable()
        {
            bool res = false;
            string ssql = "select Category from [INFORMA].[dbo].[UserDokumenCategory] where UserID = ?";
            DataTable resDT = new DataTable();
            resDT = myLocalDBSetting.GetDataTable(ssql, false, UserID);
            if(resDT.Rows.Count > 0)
            {
                res = true;
            }

            return res;
        }

        protected void btnMasterDocLoc_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterDocLoacation/DocLocationList.aspx");
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
                    nameValues.Set("Key", myrow["DocKey"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();
                    Response.Redirect("~/Transactions/CreditProcess/PeminjamanDocument/PeminjamanDocEntry.aspx" + updatedQueryString);
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

        protected void btnApprovalList_Click(object sender, EventArgs e)
        {
            myApprovalTable = GetApprovalList();
            gvApprovalList.DataSource = myApprovalTable;
            gvApprovalList.DataBind();

            apcApproval.ShowOnPageLoad = true;
        }

        protected void gvApprovalList_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myApprovalTable;
        }

        protected DataTable GetApprovalList()
        {
            string ssql = "select DocKey, DocNo, DocCategory, Department from [INFORMA].[dbo].[PeminjamanDokumen] where Status = 'WAITING CUSTODIAN'";
            DataTable resDT = new DataTable();
            resDT = myLocalDBSetting.GetDataTable(ssql, false, UserID);
            return resDT;
        }
    }
}