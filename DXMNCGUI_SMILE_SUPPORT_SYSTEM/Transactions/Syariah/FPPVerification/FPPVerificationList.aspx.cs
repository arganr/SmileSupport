using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.FPPVerification
{
    public partial class FPPVerificationList : BasePage
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
        protected FPPVerificationDB myFPPVerificationDB
        {
            get { isValidLogin(false); return (FPPVerificationDB)HttpContext.Current.Session["myFPPVerificationDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myFPPVerificationDB" + this.ViewState["_PageID"]] = value; }
        }
        protected FPPVerificationEntity myFPPVerificationEntity
        {
            get { isValidLogin(false); return (FPPVerificationEntity)HttpContext.Current.Session["myFPPVerificationEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myFPPVerificationEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected string strSqlCommand
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strSqlCommand" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strSqlCommand" + this.ViewState["_PageID"]] = value; }
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
                this.myFPPVerificationDB = FPPVerificationDB.Create(myMySqlDBSetting, dbsession);

                myMainTable = this.myFPPVerificationDB.LoadBrowseTable(true, myDBSession.LoginUserID);
                gvMain.DataBind();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            ViewData();
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string updatedQueryString = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();

            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];

            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            switch (callbackParam[0].ToUpper())
            {
                case "VIEW":
                    try
                    {
                        if (myrow != null)
                        {
                            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                            nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                            updatedQueryString = "?" + nameValues.ToString();
                            myFPPVerificationEntity = myFPPVerificationDB.View(Convert.ToInt32(paramValue));
                            ASPxWebControl.RedirectOnCallback("~/Transactions/Syariah/FPPVerification/FPPVerificationEntry.aspx" + updatedQueryString);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        return;
                    }
                    break;
                case "PROCEED":
                    cplMain.JSProperties["cpNotification"] = "";

                    object obj = null;
                    if (myrow["nama_spouse"].ToString().Length > 0)
                    {
                        obj = myMySqlDBSetting.ExecuteScalar("SELECT COUNT(*) FROM cust_prospect_doc WHERE cust_prospect_id=? AND file_type = 'KTPSP'", Convert.ToInt32(paramValue));
                        if (obj != null || obj != DBNull.Value)
                        {
                            if (Convert.ToInt32(obj) <= 0)
                            { cplMain.JSProperties["cpNotification"] = "Dokumen pasangan wajib diupload."; return; }
                        }
                    }

                    obj = myMySqlDBSetting.ExecuteScalar("SELECT COUNT(*) FROM cust_prospect_doc WHERE cust_prospect_id=? AND doc_stat <> 'APPROVE'", Convert.ToInt32(paramValue));
                    if (obj != null || obj != DBNull.Value)
                    {
                        if (Convert.ToInt32(obj) >= 1)
                            { cplMain.JSProperties["cpNotification"] = "Tidak bisa proses, silahkan check kelengkapan dokumen."; return; }
                    }

                    obj = myMySqlDBSetting.ExecuteScalar("SELECT COUNT(*) FROM cust_prospect_doc WHERE cust_prospect_id=? AND doc_stat = 'APPROVE'", Convert.ToInt32(paramValue));
                    if (obj != null || obj != DBNull.Value)
                    {
                        if (Convert.ToInt32(obj) < 3)
                        { cplMain.JSProperties["cpNotification"] = "Tidak bisa proses, minimal kelengkapan dokumen berjumlah 3."; return; }
                    }
                    break;
                case "APPROVE":
                    SaveProceed(SaveAction.Approve);
                    cplMain.JSProperties["cpAlertMessage"] = "FPP telah di proses kedalam sistem SMILE. Terima kasih.";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    ASPxWebControl.RedirectOnCallback(Request.Url.AbsoluteUri);
                    break;
                case "APPROVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to approve this document ?";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    break;
                case "REJECT":
                    SaveProceed(SaveAction.Reject);
                    cplMain.JSProperties["cpAlertMessage"] = "Document has been rejected...";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    ASPxWebControl.RedirectOnCallback(Request.Url.AbsoluteUri);
                    break;
                case "REJECT_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to reject this document ?";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    break;
            }
        }

        private bool SaveProceed(SaveAction saveAction)
        {
            bool bSave = true;
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            if (myrow != null)
            {
                myFPPVerificationEntity = myFPPVerificationDB.View(Convert.ToInt32(myrow["cust_prospect_id"]));
            }

            MySqlConnection myconn = new MySqlConnection(myMySqlDBSetting.ConnectionString);
            MySqlCommand mysqlCommand = new MySqlCommand(@"
                UPDATE cust_prospect
                    SET 
                        status_aplikasi=@status_aplikasi,
                        upd_by=@upd_by,
                        upd_dt=@upd_dt
                    WHERE
                        cust_prospect_id=@cust_prospect_id");
            mysqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                MySqlParameter mysqlParameter1 = mysqlCommand.Parameters.Add("@status_aplikasi", MySqlDbType.VarChar, 20);
                mysqlParameter1.Value = saveAction == SaveAction.Approve ? "APPROVE" : "REJECT";
                mysqlParameter1.Direction = ParameterDirection.Input;

                MySqlParameter mysqlParameter2 = mysqlCommand.Parameters.Add("@upd_by", MySqlDbType.VarChar, 50);
                mysqlParameter2.Value = Convert.ToString(this.UserName);
                mysqlParameter2.Direction = ParameterDirection.Input;

                MySqlParameter mysqlParameter3 = mysqlCommand.Parameters.Add("@upd_dt", MySqlDbType.DateTime);
                mysqlParameter3.Value = myMySqlDBSetting.GetServerTime();
                mysqlParameter3.Direction = ParameterDirection.Input;

                MySqlParameter mysqlParameter4 = mysqlCommand.Parameters.Add("@cust_prospect_id", MySqlDbType.Int32);
                mysqlParameter4.Value = Convert.ToInt32(myrow["cust_prospect_id"]);
                mysqlParameter4.Direction = ParameterDirection.Input;

                mysqlCommand.ExecuteNonQuery();

                if (saveAction == SaveAction.Approve && myFPPVerificationEntity != null)
                {
                    SaveApprove(true, myFPPVerificationEntity);
                }

            }
            catch (MySqlException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
                myconn.Dispose();
            }
            return bSave;
        }
        private bool SaveApprove(bool bApprove, FPPVerificationEntity myFPPVerificationEntity)
        {
            bApprove = true;
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            //SqlConnection myconn = new SqlConnection("Data Source=192.168.1.10\\MGUISVR;Initial Catalog=IFINANCING_GOLIVE; Persist Security Info=True;User ID=mncl;Password=Mncleasing123");
            myconn.Open();
            try
            {
                string ipAdd = "";
                if (HttpContext.Current != null)
                {
                    var Request = HttpContext.Current.Request;
                    ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(ipAdd))
                    {
                        ipAdd = Request.ServerVariables["REMOTE_ADDR"];
                    }
                }

                string strInJob = "0";
                switch (Convert.ToString(myFPPVerificationEntity.group_mnc_cust).ToUpper())
                {
                    case "GROUP":
                        strInJob = "9";
                        break;
                    case "NONGROUP":
                        switch (Convert.ToString(myFPPVerificationEntity.jenis_profesi).ToUpper())
                        {
                            case "KARYAWAN":
                                strInJob = "1";
                                break;
                            case "WIRASWASTA":
                                strInJob = "2";
                                break;
                            case "PROFESSIONAL":
                                strInJob = "3";
                                break;
                        }
                        break;
                }

                string strInJobSpouse = "0";
                switch (Convert.ToString(myFPPVerificationEntity.jenis_pekerjaan_spouse).ToUpper())
                {
                    case "KARYAWAN":
                        strInJobSpouse = "1";
                        break;
                    case "WIRASWASTA":
                        strInJobSpouse = "2";
                        break;
                    case "PROFESSIONAL":
                        strInJobSpouse = "3";
                        break;
                    case "IRT":
                        strInJobSpouse = "4";
                        break;
                }

                string strGender = "0";
                switch (Convert.ToString(myFPPVerificationEntity.gender_cust).ToUpper())
                {
                    case "PRIA":
                        strGender = "1";
                        break;
                    case "WANITA":
                        strGender = "2";
                        break;
                }

                string strMaritalStatus = "0";
                switch (Convert.ToString(myFPPVerificationEntity.marital_stat_cust).ToUpper())
                {
                    case "SINGLE":
                        strMaritalStatus = "1";
                        break;
                    case "MENIKAH":
                        strMaritalStatus = "2";
                        break;
                    case "CERAIH":
                        strMaritalStatus = "3";
                        break;
                    case "CERAIM":
                        strMaritalStatus = "3";
                        break;
                }

                string strEcRelation = "0";
                switch (Convert.ToString(myFPPVerificationEntity.relationship_ec).ToUpper())
                {
                    case "ORTU":
                        strEcRelation = "1";
                        break;
                    case "ADIK":
                        strEcRelation = "2";
                        break;
                    case "SEPUPU":
                        strEcRelation = "3";
                        break;
                    case "OTHER":
                        strEcRelation = "5";
                        break;
                }

                string strInHouseStat = "0";
                switch (Convert.ToString(myFPPVerificationEntity.status_tinggal_cust).ToUpper())
                {
                    case "OWN":
                        strEcRelation = "1";
                        break;
                    case "RENT":
                        strEcRelation = "2";
                        break;
                    case "KOS":
                        strEcRelation = "2";
                        break;
                    case "FAM":
                        strEcRelation = "4";
                        break;
                    case "OTHER":
                        strEcRelation = "2";
                        break;
                }

                SqlCommand sqlCommand = new SqlCommand(@"sp_sys_client_insert_appentry_MNCL_FROM_MITRA");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = myconn;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@p_statusapproved", SqlDbType.VarChar, 20);
                sqlParameter1.Value = "APPROVE";
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@p_name", SqlDbType.NVarChar, 60);
                sqlParameter2.Value = Convert.ToString(myFPPVerificationEntity.nama_cust);
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@p_gender", SqlDbType.VarChar, 1);
                sqlParameter3.Value = strGender;
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@p_education", SqlDbType.VarChar, 10);
                sqlParameter4.Value = Convert.ToString(myFPPVerificationEntity.education_cust);
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@p_marital", SqlDbType.NVarChar, 2);
                sqlParameter5.Value = strMaritalStatus;
                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@p_address1", SqlDbType.NVarChar, 200);
                sqlParameter6.Value = Convert.ToString(myFPPVerificationEntity.alamat_ktp_cust);
                SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@p_kelurahan", SqlDbType.NVarChar, 40);
                sqlParameter7.Value = "KELURAHAN";
                SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@p_kecamatan", SqlDbType.NVarChar, 40);
                sqlParameter8.Value = "KECAMATAN";
                SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@p_area_code", SqlDbType.NVarChar, 6);
                sqlParameter9.Value = "99999";
                SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@p_phone", SqlDbType.NVarChar, 20);
                sqlParameter10.Value = Convert.ToString(myFPPVerificationEntity.no_telp_cust);
                SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@p_cre_date", SqlDbType.DateTime);
                sqlParameter11.Value = myDBSetting.GetServerTime();
                SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@p_cre_by", SqlDbType.NVarChar, 15);
                sqlParameter12.Value = Convert.ToString(this.UserName);
                SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@p_cre_ip_address", SqlDbType.NVarChar, 15);
                sqlParameter13.Value = ipAdd;
                SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@p_mod_date", SqlDbType.DateTime);
                sqlParameter14.Value = myDBSetting.GetServerTime();
                SqlParameter sqlParameter15 = sqlCommand.Parameters.Add("@p_mod_by", SqlDbType.NVarChar, 15);
                sqlParameter15.Value = Convert.ToString(this.UserName);
                SqlParameter sqlParameter16 = sqlCommand.Parameters.Add("@p_mod_ip_address", SqlDbType.NVarChar, 15);
                sqlParameter16.Value = ipAdd;
                SqlParameter sqlParameter17 = sqlCommand.Parameters.Add("@p_alamattingal", SqlDbType.NVarChar, 250);
                sqlParameter17.Value = Convert.ToString(myFPPVerificationEntity.alamat_tinggal_cust);
                SqlParameter sqlParameter18 = sqlCommand.Parameters.Add("@p_alamattagih", SqlDbType.NVarChar, 250);
                sqlParameter18.Value = Convert.ToString(myFPPVerificationEntity.alamat_tinggal_cust);
                SqlParameter sqlParameter19 = sqlCommand.Parameters.Add("@p_kota", SqlDbType.NVarChar, 100);
                sqlParameter19.Value = Convert.ToString(myFPPVerificationEntity.kota_tinggal_cust);
                SqlParameter sqlParameter20 = sqlCommand.Parameters.Add("@p_npwp", SqlDbType.NVarChar, 35);
                sqlParameter20.Value = Convert.ToString(myFPPVerificationEntity.npwp_cust);
                SqlParameter sqlParameter21 = sqlCommand.Parameters.Add("@p_inborndt", SqlDbType.DateTime);
                sqlParameter21.Value = (object)myFPPVerificationEntity.tanggal_lahir_cust;
                SqlParameter sqlParameter22 = sqlCommand.Parameters.Add("@p_inbornplc", SqlDbType.NVarChar, 50);
                sqlParameter22.Value = Convert.ToString(myFPPVerificationEntity.tempat_lahir_cust);
                SqlParameter sqlParameter23 = sqlCommand.Parameters.Add("@p_ibukandung", SqlDbType.NVarChar, 50);
                sqlParameter23.Value = Convert.ToString(myFPPVerificationEntity.mmn_cust);
                SqlParameter sqlParameter24 = sqlCommand.Parameters.Add("@p_inmailtelp", SqlDbType.NVarChar, 14);
                sqlParameter24.Value = Convert.ToString(myFPPVerificationEntity.hp_cust);
                SqlParameter sqlParameter25 = sqlCommand.Parameters.Add("@p_inktp", SqlDbType.NVarChar, 40);
                sqlParameter25.Value = Convert.ToString(myFPPVerificationEntity.ktp_cust);
                SqlParameter sqlParameter26 = sqlCommand.Parameters.Add("@p_coy_name", SqlDbType.NVarChar, 250);
                sqlParameter26.Value = Convert.ToString(myFPPVerificationEntity.nama_perusahaan_cust);
                SqlParameter sqlParameter27 = sqlCommand.Parameters.Add("@p_penghasilanutama ", SqlDbType.Decimal, 17);
                sqlParameter27.Value = Convert.ToDecimal(myFPPVerificationEntity.penghasilan_cust);
                SqlParameter sqlParameter28 = sqlCommand.Parameters.Add("@p_infax", SqlDbType.NVarChar, 50);
                sqlParameter28.Value = Convert.ToString(myFPPVerificationEntity.status_karyawan_cust);
                SqlParameter sqlParameter29 = sqlCommand.Parameters.Add("@p_lamakerja", SqlDbType.SmallInt);
                sqlParameter29.Value = Convert.ToInt16(myFPPVerificationEntity.pengalaman_kerja);
                SqlParameter sqlParameter30 = sqlCommand.Parameters.Add("@p_injob ", SqlDbType.SmallInt);
                sqlParameter30.Value = Convert.ToInt16(strInJob);
                SqlParameter sqlParameter31 = sqlCommand.Parameters.Add("@p_contactname", SqlDbType.NVarChar, 150);
                sqlParameter31.Value = Convert.ToString(myFPPVerificationEntity.nama_ec);
                SqlParameter sqlParamete32 = sqlCommand.Parameters.Add("@p_contacthp", SqlDbType.NVarChar, 25);
                sqlParamete32.Value = Convert.ToString(myFPPVerificationEntity.hp_ec);
                SqlParameter sqlParameter33 = sqlCommand.Parameters.Add("@p_contactrelation", SqlDbType.NVarChar, 2);
                sqlParameter33.Value = strEcRelation;
                SqlParameter sqlParameter34 = sqlCommand.Parameters.Add("@p_contactemail", SqlDbType.NVarChar, 50);
                sqlParameter34.Value = Convert.ToString(myFPPVerificationEntity.email_ec);
                SqlParameter sqlParameter35 = sqlCommand.Parameters.Add("@p_INSPOUNAME", SqlDbType.NVarChar, 100);
                sqlParameter35.Value = Convert.ToString(myFPPVerificationEntity.nama_spouse);
                SqlParameter sqlParameter36 = sqlCommand.Parameters.Add("@p_kotalahirspouse", SqlDbType.NVarChar, 100);
                sqlParameter36.Value = Convert.ToString(myFPPVerificationEntity.kota_ktp_spouse);
                SqlParameter sqlParameter37 = sqlCommand.Parameters.Add("@p_tgllahirspouse", SqlDbType.DateTime);
                sqlParameter37.Value = (object)myFPPVerificationEntity.tanggal_lahir_spouse;
                SqlParameter sqlParameter38 = sqlCommand.Parameters.Add("@p_alamatspouse", SqlDbType.NVarChar, 250);
                sqlParameter38.Value = Convert.ToString(myFPPVerificationEntity.alamat_ktp_spouse);
                SqlParameter sqlParameter39 = sqlCommand.Parameters.Add("@p_hpspouse", SqlDbType.NVarChar, 20);
                sqlParameter39.Value = Convert.ToString(myFPPVerificationEntity.hp_spouse);
                SqlParameter sqlParameter40 = sqlCommand.Parameters.Add("@p_emailspouse", SqlDbType.NVarChar, 55);
                sqlParameter40.Value = Convert.ToString(myFPPVerificationEntity.email_spouse);
                SqlParameter sqlParameter41 = sqlCommand.Parameters.Add("@p_kota_terbit_ktp", SqlDbType.NVarChar, 100);
                sqlParameter41.Value = Convert.ToString(myFPPVerificationEntity.kota_ktp_cust);
                SqlParameter sqlParameter42 = sqlCommand.Parameters.Add("@p_injumtg", SqlDbType.VarChar, 1);
                sqlParameter42.Value = Convert.ToString(myFPPVerificationEntity.jumlah_tanggungan);
                SqlParameter sqlParameter43 = sqlCommand.Parameters.Add("@p_inspouktp", SqlDbType.VarChar, 100);
                sqlParameter43.Value = Convert.ToString(myFPPVerificationEntity.ktp_spouse);
                SqlParameter sqlParameter44 = sqlCommand.Parameters.Add("@p_inspoujob", SqlDbType.VarChar, 1);
                sqlParameter44.Value = strInJobSpouse;
                SqlParameter sqlParameter45 = sqlCommand.Parameters.Add("@p_injamin", SqlDbType.VarChar, 100);
                sqlParameter45.Value = Convert.ToString(myFPPVerificationEntity.nama_penjamin);
                SqlParameter sqlParameter46 = sqlCommand.Parameters.Add("@p_injamadd1", SqlDbType.VarChar, 250);
                sqlParameter46.Value = Convert.ToString(myFPPVerificationEntity.curr_addr_penjamin);
                SqlParameter sqlParameter47 = sqlCommand.Parameters.Add("@p_injamktp", SqlDbType.VarChar, 100);
                sqlParameter47.Value = Convert.ToString(myFPPVerificationEntity.ktp_penjamin);
                SqlParameter sqlParameter48 = sqlCommand.Parameters.Add("@p_injamtelp", SqlDbType.VarChar, 20);
                sqlParameter48.Value = Convert.ToString(myFPPVerificationEntity.hp_penjamin);
                SqlParameter sqlParameter49 = sqlCommand.Parameters.Add("@p_inhouse", SqlDbType.VarChar, 1);
                sqlParameter49.Value = strInHouseStat;

                sqlCommand.ExecuteNonQuery();

                //strSqlCommand = sqlCommand.ToString();a
                //throw new Exception(ToReadableString(sqlCommand));
                //throw new ArgumentException("pening");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
            return bApprove;
        }

        public static string ToReadableString(IDbCommand command)
        {
            StringBuilder builder = new StringBuilder();
            if (command.CommandType == CommandType.StoredProcedure)
                builder.AppendLine("Stored procedure: " + command.CommandText);
            else
                builder.AppendLine("Sql command: " + command.CommandText);
            if (command.Parameters.Count > 0)
                builder.AppendLine("With the following parameters.");
            foreach (IDataParameter param in command.Parameters)
            {
                builder.AppendFormat(
                    "     Paramater {0}: {1}",
                    param.ParameterName,
                    (param.Value == null ?
                    "NULL" : param.Value.ToString())).AppendLine();
            }
            return builder.ToString();
        }

        protected void gvMain_Init(object sender, EventArgs e)
        {

        }
        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvMain_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void gvMain_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {

        }
        protected void gvMain_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.ButtonID == "btnGridProceed")
            {
                if (Convert.ToString(gvMain.GetRowValues(e.VisibleIndex, "status_aplikasi")) == "NEW")
                    e.Enabled = true;
                if (Convert.ToString(gvMain.GetRowValues(e.VisibleIndex, "status_aplikasi")) == "APPROVE")
                    e.Enabled = false;
                if (Convert.ToString(gvMain.GetRowValues(e.VisibleIndex, "status_aplikasi")) == "ON PROGRESS")
                    e.Enabled = true;
            }
        }
        protected void gvMain_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "btnGridView")
            {
                try
                {
                    ViewData();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }

        protected void ViewData()
        {
            string updatedQueryString = "";
            try
            {
                DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                if (myrow != null)
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();
                    myFPPVerificationEntity = myFPPVerificationDB.View(Convert.ToInt32(myrow["cust_prospect_id"]));
                    Response.Redirect("~/Transactions/Syariah/FPPVerification/FPPVerificationEntry.aspx" + updatedQueryString);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
        }

    }
}