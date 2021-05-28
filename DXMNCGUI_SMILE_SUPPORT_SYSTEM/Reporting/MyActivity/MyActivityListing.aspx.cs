using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.MyActivity
{
    public partial class MyActivityListing : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected MySqlDBSetting myMySqlDBSetting
        {
            get { isValidLogin(); return (MySqlDBSetting)HttpContext.Current.Session["myMySqlDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myMySqlDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]] = value; }
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
                myMySqlDBSetting = mysqldbsetting;
                myDBSession = dbsession;

                myMainTable = new DataTable();
                MySqlConnection myconn = new MySqlConnection("server=172.31.215.10;port=3308;database=activity_db;uid=mncleasing;password=Mncleasing123; convert zero datetime=True");
                using (MySqlCommand cmdheader = new MySqlCommand(@"SELECT * FROM activity_trx WHERE UserId=@UserId ORDER BY InputDt DESC", myconn))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdheader);
                    cmdheader.Parameters.Add("@UserId", MySqlDbType.Text);
                    cmdheader.Parameters["@UserId"].Value = UserName;
                    adapter.Fill(myMainTable);
                }
                gvMain.DataBind();
            }
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
    }
}