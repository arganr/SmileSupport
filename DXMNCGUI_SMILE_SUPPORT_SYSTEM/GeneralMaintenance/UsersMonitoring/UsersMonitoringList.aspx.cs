using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.GeneralMaintenance.UsersMonitoring
{
    public partial class UsersMonitoringList : BasePage
    {
        string SqlQuery = string.Empty;
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(true); return (SqlDBSetting)HttpContext.Current.Session["HomeSqlDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["HomeSqlDBSetting" + this.ViewState["_PageID"]] = value; }
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
        protected DataTable myMonitoringTable
        {
            get { isValidLogin(true); return (DataTable)HttpContext.Current.Session["myMonitoringTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMonitoringTable" + this.ViewState["_PageID"]] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(true);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                myMonitoringTable = new DataTable();
                myMonitoringTable = LoadWorkListTable();
                gvUsersMonitoring.DataSource = myMonitoringTable;
                gvUsersMonitoring.DataBind();
            }
        }
        protected DataTable LoadWorkListTable()
        {
            DataTable mytable = new DataTable();
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlQuery = @"SELECT A.NIK + ' - ' + B.USER_NAME AS NIK, MAX(TimeStart) AS TimeStart, MAX(TimeEnd) AS TimeEnd,
                            CASE WHEN MAX(TimeEnd) IS NULL THEN 'Online' ELSE 'Offline' END AS Status 
                            FROM [dbo].[Session] A
                            INNER JOIN [dbo].[MASTER_USER] B ON A.NIK = B.USER_ID
                            GROUP BY A.NIK, B.USER_NAME";
            using (SqlCommand cmdheader = new SqlCommand(SqlQuery, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdheader);
                adapter.Fill(mytable);
            }
            return mytable;
        }

        protected void gvUsersMonitoring_DataBinding(object sender, EventArgs e)
        {

        }
    }
}