using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.PDSB;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.PDSB.Models;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.PdsbAPI
{
    public partial class PdsbAPI_All : BasePage
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

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
            }
            if (!IsCallback)
            {

            }
        }

        protected async void btnAPI_Click(Object sender, EventArgs e)
        {
            API_PDSB APIClass = new API_PDSB();
            ModelAccount mdlAccount = new ModelAccount();

            string messageResult = "";
            string strNoNasabah = "";
            string strNoRek = "";
            string nokontrak = luKontrak.Value.ToString();
            string branchcode = luBranch.Value.ToString();
            string branchname = luBranch.Text;

            //get Token
            string token = await APIClass.GetToken();

            //get No Rek
            var dtACC = GetDataACC(nokontrak);
            if (dtACC.Rows.Count > 0)
            {
                strNoRek = await APIClass.Account_Registeration(token, dtACC, branchcode);

                if (strNoRek.Substring(0, 5) != "ERROR")
                {
                    messageResult = "No Rekening: " + strNoRek;

                    string statUpdateRek = UpdateNoRekClient(luKontrak.Value.ToString(), strNoRek, txtName.Text, "Panin Dubai Syariah", branchname, UserID);
                    if (statUpdateRek != "")
                    {
                        messageResult = "ERROR Update No Rek: " + statUpdateRek;
                    }
                }
                else
                {
                    messageResult = "ERROR No Rekening : " + strNoRek;
                }
            }
            else
            {
                messageResult = "ERROR: No Kontrak Not Found";
            }

            //txtResult.Text = messageResult;
            lblResult.InnerText = messageResult;
        }

        private DataTable GetDataACC(string NoKontrak)
        {
            //string ssql = "exec dbo.spGETPDSB_ParameterWSCIFRegistration '000316160100052'";
            string ssql = "exec dbo.spGETPDSB_ParameterWSAccountRegistration '" + NoKontrak + "'";
            DataTable resDT = new DataTable();
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(ssql);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;

                SqlDataReader reader = sqlCommand.ExecuteReader();
                resDT.Load(reader);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }

            return resDT;
        }

        private string UpdateNoRekClient(string nokontrak, string bank_acc_no, string bank_acc_name, string bank_name, string bank_branch, string user_id)
        {
            //EXEC SP_MNCL_UPDATE_REKENING_CLIENT '070320120200017', '762389153000', 'SYAFRIZAL FADLI', 'CIMB SYARIAH', 'BEKASI', '1812010'
            string strResult = "";
            string ssql = "EXEC dbo.SP_MNCL_UPDATE_REKENING_CLIENT '" + nokontrak + "', '" + bank_acc_no + "', '" + bank_acc_name + "', '" + bank_name + "', '" + bank_branch + "', '" + user_id + "'";
            DataTable resDT = new DataTable();
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(ssql);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
                //throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }

            return strResult;
        }

        private string GetNoKontrak(string NoKontrak)
        {
            string strResult = "";

            string strLeft = NoKontrak.Substring(0, 6);
            string strRight = NoKontrak.Substring(NoKontrak.Length - 6);
            strResult = strLeft + strRight;

            return strResult;
        }
    }
}