using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.UpdateNoSPPH
{
    public partial class UpdateNoSPPHList : BasePage
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
        protected UpdateNoSPPHDB myUpdateNoSPPHDB
        {
            get { isValidLogin(false); return (UpdateNoSPPHDB)HttpContext.Current.Session["myUpdateNoSPPHDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUpdateNoSPPHDB" + this.ViewState["_PageID"]] = value; }
        }
        protected UpdateNoSPPHEntity myUpdateNoSPPHEntity
        {
            get { isValidLogin(false); return (UpdateNoSPPHEntity)HttpContext.Current.Session["myUpdateNoSPPHEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUpdateNoSPPHEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myIncentiveTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myIncentiveTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myIncentiveTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myAllIncentiveTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myAllIncentiveTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAllIncentiveTable" + this.ViewState["_PageID"]] = value; }
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
                myIncentiveTable = new DataTable();
                myAllIncentiveTable = new DataTable();
                this.myUpdateNoSPPHDB = UpdateNoSPPHDB.Create(myDBSetting, myLocalDBSetting, dbsession);

                myMainTable = this.myUpdateNoSPPHDB.LoadBrowseTable(true, myDBSession.LoginUserID);
                gvMain.DataBind();

                if (this.Request.QueryString["DocKey"] != null)
                {
                    int indexVal = gvMain.FindVisibleIndexByKeyValue(this.Request.QueryString["DocKey"]);
                    gvMain.FocusedRowIndex = indexVal;
                }
                else
                {
                    gvMain.FocusedRowIndex = -1;
                }

                accessable();
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
            myMainTable = this.myUpdateNoSPPHDB.LoadBrowseTable(false, myDBSession.LoginUserID);
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
                myUpdateNoSPPHEntity = myUpdateNoSPPHDB.Entity(DXSSType.SPPH);
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                updatedQueryString = "?" + nameValues.ToString();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
            Response.Redirect("~/Transactions/Syariah/UpdateNoSPPH/UpdateNoSPPHEntry.aspx" + updatedQueryString);
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
                    myUpdateNoSPPHEntity = myUpdateNoSPPHDB.View(System.Convert.ToInt32(myrow["DocKey"]));
                    Response.Redirect("~/Transactions/Syariah/UpdateNoSPPH/UpdateNoSPPHEntry.aspx" + updatedQueryString);
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
                    myUpdateNoSPPHEntity = myUpdateNoSPPHDB.Edit(System.Convert.ToInt32(myrow["DocKey"]), DXSSAction.Edit);
                    Response.Redirect("~/Transactions/Syariah/UpdateNoSPPH/UpdateNoSPPHEntry.aspx" + updatedQueryString);
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
        protected void btnAllIncentive_Click(object sender, EventArgs e)
        {
            myAllIncentiveTable = this.myUpdateNoSPPHDB.LoadAllIncentiveTable();
            gvAllIncentive.DataBind();

            apcViewAllIncentive.ShowOnPageLoad = true;
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {

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
        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvMain_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void gvIncentive_Init(object sender, EventArgs e)
        {

        }
        protected void gvIncentive_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myIncentiveTable;
        }
        protected void gvIncentive_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            string[] callbackParam = e.Parameters.ToString().Split(';');
            gridView.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1].ToUpper();
            switch (callbackParam[0].ToUpper())
            {
                case "LOAD":
                    myIncentiveTable = this.myUpdateNoSPPHDB.LoadIncentiveTable(paramValue.ToString());
                    gvIncentive.DataBind();
                    break;
            }
        }

        protected void gvAllIncentive_Init(object sender, EventArgs e)
        {

        }
        protected void gvAllIncentive_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myAllIncentiveTable;
        }
        protected void gvAllIncentive_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvAllIncentive_Load(object sender, EventArgs e)
        {

        }
    }
}