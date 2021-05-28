using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System.Media;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Account
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["SessionExpired"] != null)
                {
                    string strPage = "";
                    strPage = Request.QueryString["SessionExpired"].ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Sorry , " + strPage + "...');", true);
                }
                Session.Abandon();
                this.mInitialize();
                object value = Cache["SCH_Company"];
            }
        }
        private void mInitialize()
        {
            String strClientScript = "if(event.keyCode==13){ " + Page.ClientScript.GetPostBackEventReference(this, "Login", false) + "; }";
            txtID.Attributes.Add("onkeydown", strClientScript);
            txtPassword.Attributes.Add("onkeydown", strClientScript);
            lblMessage.Text = "";
            txtID.Focus();
            this.mExtractQueryString();
            Connection();
        }
        private void Connection()
        {
            this.dbsetting = new SqlDBSetting(DBServerType.SQL2000, GetSqlConnectionString(), 180); //"User Id=apps;Password=apps;Data Source= (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.2.213)(PORT=1529))(CONNECT_DATA=(SERVICE_NAME=DEV2)))", 30);
            this.localdbsetting = new SqlLocalDBSetting(DBServerType.SQL2000, GetLocalConnectionString(), 180); //"User Id=apps;Password=apps;Data Source= (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.2.213)(PORT=1529))(CONNECT_DATA=(SERVICE_NAME=DEV2)))", 30);
            this.mysqldbsetting = new MySqlDBSetting(DBServerType.MySQL, GetMySqlConnectionString(), 180);
        }
        private Boolean fIsEntryValid()
        {
            if ("" == txtID.Text)
            {
                lblMessage.Text = "User name must be filled!";
                txtID.Focus();
                return false;
            }
            else if ("" == txtPassword.Text)
            {
                lblMessage.Text = "Password must be filled!";
                txtPassword.Focus();
                return false;
            }
            return true;
        }
        private void mExtractQueryString()
        {
            String strMessageID = Server.UrlDecode(Request.QueryString["MsgID"]);

            switch (strMessageID)
            {
                case "1":
                    lblMessage.Text = "Sorry, your session expired.";
                    break;

                default:
                    break;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {

            Connection();
            if (!fIsEntryValid()) { return; }
            this.dbsession = SqlDBSession.Create(dbsetting, localdbsetting);
            this.dbsession.UseEncryptedPassword = true;

            if (this.dbsession.Login(txtID.Text, txtPassword.Text))
            {
                this.UserID = this.dbsession.LoginUserID.ToUpper();
                this.dbsession.LoginNoUrut = txtID.Text.ToUpper();
                this.SessionID = Guid.NewGuid();
                accessright = AccesRight.Create(localdbsetting, this.UserID);
                object obj = dbsetting.ExecuteScalar("SELECT USER_NAME FROM [dbo].[MASTER_USER] WHERE USER_ID=?", (object)this.UserID);
                if (obj != null && obj != DBNull.Value)
                {
                    this.UserName = obj.ToString();
                }

                if (IsValid)
                {
                    if (this.Request.QueryString["SourceType"] != null && this.Request.QueryString["SourceKey"] != null)
                    {
                        string sourceType = this.Request.QueryString["SourceType"].ToString();
                        string sourceKey = this.Request.QueryString["SourceKey"].ToString();
                        return;
                    }
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }

            }
            else
            {
                lblMessage.Text = "User name or password invalid!";
                txtPassword.Focus();
                return;
            }
        }
        private void userexists(string strUserName, string strPassword)
        {
            try
            {
                MembershipUser userexists = Membership.GetUser(strUserName.ToUpper(), false);
                if (userexists == null)
                {
                    MembershipUser newuser = Membership.CreateUser(strUserName, strPassword, "www." + strUserName + "@gmail.com");
                }
                else
                {
                    userexists.ChangePassword(userexists.ResetPassword(), strPassword);
                }
            }
            catch { }
        }
        protected void lbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void lbCompany_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
        {

        }
        protected void lbCompany_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string[] callbackParam = e.Parameter.ToString().Split(';');
        }
        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string[] callbackParam = e.Parameter.ToString().Split(';');
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/WebForm1.aspx");
        }
    }
}