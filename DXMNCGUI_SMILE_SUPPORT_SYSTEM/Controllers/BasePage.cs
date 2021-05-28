using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Web.Security;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {

        }
        #region "Property"
        protected Guid SessionID
        {
            get { return (Guid)HttpContext.Current.Session["SessionID"]; }
            set { HttpContext.Current.Session["SessionID"] = value; }
        }
        protected Int32 OrgID
        {
            get { return (Int32)HttpContext.Current.Session["OrgID"]; }
            set { HttpContext.Current.Session["OrgID"] = value; }
        }
        protected string CompanyName
        {
            get { return (string)HttpContext.Current.Session["CompanyName"]; }
            set { HttpContext.Current.Session["CompanyName"] = value; }
        }
        protected string UserID
        {
            get { return (string)HttpContext.Current.Session["UserID"]; }
            set { HttpContext.Current.Session["UserID"] = value; }
        }
        protected string FullName
        {
            get { return (string)HttpContext.Current.Session["FullName"]; }
            set { HttpContext.Current.Session["FullName"] = value; }
        }
        protected string UserName
        {
            get { return (string)HttpContext.Current.Session["UserName"]; }
            set { HttpContext.Current.Session["UserName"] = value; }
        }
        protected Int32 EmployeeID
        {
            get { return (Int32)HttpContext.Current.Session["EmployeeID"]; }
            set { HttpContext.Current.Session["EmployeeID"] = value; }
        }
        protected Int32 PersonPartyID
        {
            get { return (Int32)HttpContext.Current.Session["PersonPartyID"]; }
            set { HttpContext.Current.Session["PersonPartyID"] = value; }
        }
        protected DataTable RespTable
        {
            get { return (DataTable)HttpContext.Current.Session["RespTable"]; }
            set { HttpContext.Current.Session["RespTable"] = value; }
        }
        protected string Pwd
        {
            get { return (string)HttpContext.Current.Session["Password"]; }
            set { HttpContext.Current.Session["Password"] = value; }
        }
        protected string Fullname
        {
            get { return (string)HttpContext.Current.Session["Fullname"]; }
            set { HttpContext.Current.Session["Fullname"] = value; }
        }
        protected SqlDBSetting dbsetting
        {
            get { return (SqlDBSetting)HttpContext.Current.Session["SqlDBSetting"]; }
            set { HttpContext.Current.Session["SqlDBSetting"] = value; }
        }
        protected SqlLocalDBSetting localdbsetting
        {
            get { return (SqlLocalDBSetting)HttpContext.Current.Session["SqlLocalDBSetting"]; }
            set { HttpContext.Current.Session["SqlLocalDBSetting"] = value; }
        }
        protected MySqlDBSetting mysqldbsetting
        {
            get { return (MySqlDBSetting)HttpContext.Current.Session["MySqlDBSetting"]; }
            set { HttpContext.Current.Session["MySqlDBSetting"] = value; }
        }
        protected SqlDBSession dbsession
        {
            get { return (SqlDBSession)HttpContext.Current.Session["SqlDBSession"]; }
            set { HttpContext.Current.Session["SqlDBSession"] = value; }
        }
        protected AccesRight accessright
        {
            get { return (AccesRight)HttpContext.Current.Session["accessright"]; }
            set { HttpContext.Current.Session["accessright"] = value; }
        }
        #endregion
        #region "GetConnectionString"
        protected string GetSqlConnectionString()
        {
            string Conn = null;
            Conn = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ToString();
            return Conn;
        }
        protected string GetLocalConnectionString()
        {
            string Conn = null;
            Conn = ConfigurationManager.ConnectionStrings["SqlLocalConnectionString"].ToString();
            return Conn;
        }
        protected string GetMySqlConnectionString()
        {
            string Conn = null;
            Conn = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();
            return Conn;
        }
        protected string GetDefaultConnectionString()
        {
            string Conn = null;
            Conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            return Conn;
        }
        #endregion
        protected void isValidLogin(bool bHome)
        {
            string url = "~/Account/Login.aspx";
            string updatedQueryString = "";
            var nameValues = HttpUtility.ParseQueryString("");
            nameValues.Set("SessionExpired", "Your session has expired...");
            updatedQueryString = bHome ? "" : "?" + nameValues.ToString();
            if (HttpContext.Current.Session["Username"] == DBNull.Value || HttpContext.Current.Session["Username"] == null)
            {

                HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
                try
                {
                    Response.Redirect(url + updatedQueryString);
                }
                catch
                {
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(url + updatedQueryString);
                }
            }
            else
            {
                if (HttpContext.Current.Session["Username"].ToString() == "")
                {
                    HttpContext.Current.Session.Abandon();
                    FormsAuthentication.SignOut();
                    try
                    {
                        Response.Redirect(url + updatedQueryString);
                    }
                    catch
                    {
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback(url + updatedQueryString);
                    }
                }
            }
            if (HttpContext.Current.Session["SessionID"] == null || HttpContext.Current.Session["SessionID"] == DBNull.Value)
            {
                HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
                try
                {
                    Response.Redirect(url + updatedQueryString);
                }
                catch
                {
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(url + updatedQueryString);
                }
            }
            else
            {

                if (HttpContext.Current.Session["SessionID"].ToString() == "")
                {
                    HttpContext.Current.Session.Abandon();
                    FormsAuthentication.SignOut();
                    try
                    {
                        Response.Redirect(url + updatedQueryString);
                    }
                    catch
                    {
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback(url + updatedQueryString);
                    }
                }
            }
            if (HttpContext.Current.Session["UserID"] == null)
            {
                HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
                try
                {
                    Response.Redirect(url + updatedQueryString);
                }
                catch
                {
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(url + updatedQueryString);
                }
            }
        }
        protected void isValidLogin()
        {
            string url = "~/Account/Login.aspx";
            string updatedQueryString = "";
            var nameValues = HttpUtility.ParseQueryString("");
            nameValues.Set("SessionExpired", "Your session has expired...");
            updatedQueryString = "?" + nameValues.ToString();
            if (HttpContext.Current.Session["Username"] == DBNull.Value || HttpContext.Current.Session["Username"] == null)
            {
                HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
                try
                {
                    Response.Redirect(url + updatedQueryString);
                }
                catch
                {
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(url + updatedQueryString);
                }
            }
            else
            {
                if (HttpContext.Current.Session["Username"].ToString() == "")
                {
                    HttpContext.Current.Session.Abandon();
                    FormsAuthentication.SignOut();
                    try
                    {
                        Response.Redirect(url + updatedQueryString);
                    }
                    catch
                    {
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback(url + updatedQueryString);
                    }
                }
            }
            if (HttpContext.Current.Session["SessionID"] == null || HttpContext.Current.Session["SessionID"] == DBNull.Value)
            {
                HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
                try
                {
                    Response.Redirect(url + updatedQueryString);
                }
                catch
                {
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(url + updatedQueryString);
                }
            }
            else
            {

                if (HttpContext.Current.Session["SessionID"].ToString() == "")
                {
                    HttpContext.Current.Session.Abandon();
                    FormsAuthentication.SignOut();
                    try
                    {
                        Response.Redirect(url + updatedQueryString);
                    }
                    catch
                    {
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback(url + updatedQueryString);
                    }
                }
            }
            if (HttpContext.Current.Session["UserID"] == null)
            {
                HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
                try
                {
                    Response.Redirect(url + updatedQueryString);
                }
                catch
                {
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(url + updatedQueryString);
                }
            }
        }
    }
}