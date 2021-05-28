using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected SqlDBSetting myDBSetting
        {
            get { return (SqlDBSetting)HttpContext.Current.Session["HomeSqlDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["HomeSqlDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlLocalDBSetting myLocalDBSetting
        {
            get { return (SqlLocalDBSetting)HttpContext.Current.Session["HomeSqlLocalDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["HomeSqlLocalDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { return (SqlDBSession)HttpContext.Current.Session["HomeSqlDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["HomeSqlDBSession" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable mytable
        {
            get { return (DataTable)HttpContext.Current.Session["mytable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mytable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myparenttable
        {
            get { return (DataTable)HttpContext.Current.Session["myparenttable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myparenttable" + this.ViewState["_PageID"]] = value; }
        }
        protected AccesRight accessright
        {
            get { return (AccesRight)HttpContext.Current.Session["accessright"]; }
            set { HttpContext.Current.Session["accessright"] = value; }
        }
        protected string UserID
        {
            get { return (string)HttpContext.Current.Session["UserID"]; }
            set { HttpContext.Current.Session["UserID"] = value; }
        }

        public object MessageBox { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                mytable = new DataTable();
                myparenttable = new DataTable();
                if (HttpContext.Current.Session["SessionID"] == null)
                {
                    HttpContext.Current.Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Account/Login.aspx");
                }
                myDBSetting = (SqlDBSetting)HttpContext.Current.Session["SqlDBSetting"];
                myLocalDBSetting = (SqlLocalDBSetting)HttpContext.Current.Session["SqlLocalDBSetting"];
                myDBSession = (SqlDBSession)HttpContext.Current.Session["SqlDBSession"];

                if (!accessright.IsAccessibleByUserID(UserID, "GENERAL_MNTC_SHOW"))
                {
                    ASPxNavBar1.Groups.FindByName("nbgGeneralMaintenance").Visible = false;
                }
                if (!accessright.IsAccessibleByUserID(UserID, "AST_COLL_SHOW"))
                {
                    ASPxNavBar1.Groups.FindByName("nbgAssetCollateral").Visible = false;
                }
                if (!accessright.IsAccessibleByUserID(UserID, "SLIK_MNTC_SHOW"))
                {
                    ASPxNavBar1.Items.FindByName("nbiSLIKMntc").Visible = false;
                }
                if (UserID.Contains("OS"))
                {
                    ASPxNavBar1.Items.FindByName("nbiSLIKMntc").Visible = false;
                    ASPxNavBar1.Items.FindByName("Application1").Visible = false;
                    ASPxNavBar1.Items.FindByName("Application2").Visible = false;
                    ASPxNavBar1.Items.FindByName("Application3").Visible = false;
                    ASPxNavBar1.Items.FindByName("Application4").Visible = true;
                    ASPxNavBar1.Items.FindByName("Application5").Visible = false;
                    ASPxNavBar1.Items.FindByName("Application6").Visible = false;

                    ASPxNavBar1.Groups.FindByName("nbgAssetCollateral").Visible = false;
                    ASPxNavBar1.Groups.FindByName("nbgGeneralMaintenance").Visible = false;
                    ASPxNavBar1.Groups.FindByName("nbg1").Visible = false;
                    ASPxNavBar1.Groups.FindByName("nbg2").Visible = false;
                    ASPxNavBar1.Groups.FindByName("nbgInsurance").Visible = false;
                    ASPxNavBar1.Groups.FindByName("nbgSyariah").Visible = false;
                    ASPxNavBar1.Groups.FindByName("nbgFinance").Visible = false;
                }
            }
        }
    }
}