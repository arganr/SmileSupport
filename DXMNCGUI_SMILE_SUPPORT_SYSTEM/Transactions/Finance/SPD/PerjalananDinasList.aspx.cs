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
    public partial class PerjalananDinasList : BasePage
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
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
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
                
                this.myPerjalananDinasDB = PerjalananDinasDB.Create(myDBSetting, myLocalDBSetting, dbsession);

                myMainTable = this.myPerjalananDinasDB.LoadBrowseTable(true, myDBSession.LoginUserID);
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
            }
        }



        protected void btnNew_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                myPerjalananDinasEntity = myPerjalananDinasDB.Entity();
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                updatedQueryString = "?" + nameValues.ToString();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
            Response.Redirect("~/Transactions/Finance/SPD/PerjalananDinasEntry.aspx" + updatedQueryString);
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
    }
}