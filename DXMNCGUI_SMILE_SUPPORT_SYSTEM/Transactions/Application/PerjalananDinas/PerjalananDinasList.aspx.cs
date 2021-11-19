using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.PerjalananDinas
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
                

                GetMainTable();
                gvMain.DataBind();


            }
        }

        protected void GetMainTable()
        {
            //string ssql = "select * from [dbo].[trxReleaseDoc]";
            string ssql = @"select * from [dbo].[trxPerjalananDinas]";
            myMainTable = myLocalDBSetting.GetDataTable(ssql, false);
        }


        protected void gvMain_Init(object sender, EventArgs e)
        {

        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }
    }
}