using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.GeneralMaintenance.SchemaApplicationWorkflow
{
    public partial class SchemaApplicationWorkflowMaint : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvMain_Init(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();

                Session["ShemaApplicationWorkflowMaintenanceGuid"] = this.ViewState["_PageID"];
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
            }
            sdsApplicationWorkflowScheme.ConnectionString = GetLocalConnectionString();
        }

        protected void gvMain_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            object obj;
            obj = myLocalDBSetting.ExecuteScalar("SELECT  MAX(Seq) AS Seq FROM ApplicationWorkflowScheme ORDER BY Seq DESC");
            if (obj != null && obj != DBNull.Value)
            {
                e.NewValues["Seq"] = Convert.ToInt32(obj) + 1;
            }
            else
            {
                e.NewValues["Seq"] = 1;
            }
            e.NewValues["CanCam"] = false;
        }
    }
}