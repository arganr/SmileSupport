using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.UpdateApplicationClient
{
    public partial class UpdateApplicClient : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myDBSession = dbsession;
            }
            if (!IsCallback)
            {

            }
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            isValidLogin(false);
            string urlsave = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            //urlsave = "~Transaction/Syariah/UpdateRekeningClient/UpdateRekeningClientEntry.aspx";
            //urlsave = "~Transactions/Application/UpdateApplicationClient/UpdateApplicClient.aspx";
            urlsave = "UpdateApplicClient.aspx";
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];
            string hexColor = "#FFFF99";
            string roColor = "#EBEBEB";
            string strmessageError = string.Empty;
            DateTime mydate = myDBSetting.GetServerTime();
            System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(hexColor);
            System.Drawing.Color rocolor = System.Drawing.ColorTranslator.FromHtml(roColor);

            switch (callbackParam[0].ToUpper())
            {
                case "SAVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to save this data?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    if (ErrorInField(out strmessageError, SaveAction.Submit))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "SAVE":
                    Save(SaveAction.Submit);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
            }

        }

        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";

            return errorF;
        }

        private bool Save(SaveAction saveAction)
        {
            string str_username = HttpContext.Current.Session["Username"].ToString();
            bool bSave = true;
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(@"spSmileSupport_UpdateClient");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = myconn;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@applicno", SqlDbType.VarChar, 50);
                sqlParameter1.Value = luAppNo.Value;

                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@clientname", SqlDbType.VarChar, 200);
                sqlParameter2.Value = txtClient.Value;

                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@KTP", SqlDbType.VarChar, 50);
                sqlParameter3.Value = txtKTP.Value;

                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@IbuKandung", SqlDbType.VarChar, 200);
                sqlParameter4.Value = txtIbu.Value;

                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Income", SqlDbType.Decimal);
                sqlParameter5.Value = txtIncome.Value;

                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@jobstat", SqlDbType.VarChar, 50);
                sqlParameter6.Value = cbJobStat.SelectedItem.Value;

                SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@refname", SqlDbType.VarChar, 200);
                sqlParameter7.Value = txtRefName.Value;

                SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@addr1", SqlDbType.VarChar, 200);
                sqlParameter8.Value = txtAddress1.Value;

                SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@addr2", SqlDbType.VarChar, 200);
                sqlParameter9.Value = txtAddress2.Value;

                SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@addr3", SqlDbType.VarChar, 200);
                sqlParameter10.Value = txtAddress3.Value;

                SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@refidno", SqlDbType.VarChar, 50);
                sqlParameter11.Value = txtRefID.Value;

                SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@refphone", SqlDbType.VarChar, 50);
                sqlParameter12.Value = txtTelp.Value;

                SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@relation", SqlDbType.VarChar, 50);
                sqlParameter13.Value = txtRelation.Value;

                SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@emername", SqlDbType.VarChar, 200);
                sqlParameter14.Value = txtEmergencyName.Value;

                SqlParameter sqlParameter15 = sqlCommand.Parameters.Add("@spou_name", SqlDbType.VarChar, 200);
                sqlParameter15.Value = txtSpousName.Value;

                SqlParameter sqlParameter16 = sqlCommand.Parameters.Add("@spou_brtdate", SqlDbType.DateTime);
                sqlParameter16.Value = deSpousBrth.Value;

                SqlParameter sqlParameter17 = sqlCommand.Parameters.Add("@spou_brt_plc", SqlDbType.VarChar, 200);
                sqlParameter17.Value = txtSpousPlc.Value;

                SqlParameter sqlParameter18 = sqlCommand.Parameters.Add("@spou_nik", SqlDbType.VarChar, 50);
                sqlParameter18.Value = txtSpousKTP.Value;

                SqlParameter sqlParameter19 = sqlCommand.Parameters.Add("@spou_edu", SqlDbType.VarChar, 50);
                sqlParameter19.Value = cbSpousEdu.Value;

                SqlParameter sqlParameter20 = sqlCommand.Parameters.Add("@audit_user", SqlDbType.VarChar, 60);
                sqlParameter20.Value = str_username;

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
            return bSave;
        }
    }
}