using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.FPPVerification
{
    public partial class FPPVerificationEntry : BasePage
    {
        protected MySqlDBSetting myMySqlDBSetting
        {
            get { isValidLogin(); return (MySqlDBSetting)HttpContext.Current.Session["myMySqlDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myMySqlDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
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
        protected DataSet myds
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myds" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myds" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myHeaderTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myHeaderTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myHeaderTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDetailTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]] = value; }
        }
        protected string strKey
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]] = value; }
        }
        protected string strInJob
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strInJob" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strInJob" + this.ViewState["_PageID"]] = value; }
        }
        protected string strSqlCommand
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strSqlCommand" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strSqlCommand" + this.ViewState["_PageID"]] = value; }
        }
        protected int FileDocID
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["FileDocID" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["FileDocID" + this.ViewState["_PageID"]] = value; }
        }

        public void DownloadDoc(int i)
        {
            object obj = myMySqlDBSetting.ExecuteScalar("SELECT file_path FROM cust_prospect_doc WHERE cust_prospect_doc_id=?", i);
            if (obj != null && obj != DBNull.Value)
            {
                try
                {
                    string strURL = obj.ToString().Replace("/", @"\").Replace(@"\\172.31.215.10", @"D:");

                    FileInfo file = new FileInfo(strURL);
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.ClearContent();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "text/plain";
                        Response.Flush();
                        Response.TransmitFile(file.FullName);
                        Response.End();
                    }
                    else
                    { throw new Exception("file doest not exists"); }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myDBSetting = dbsetting;
                myMySqlDBSetting = mysqldbsetting;
                myDBSession = dbsession;
                if (this.Request.QueryString["cust_prospect_id"] != null)
                {
                    this.myFPPVerificationDB = FPPVerificationDB.Create(myMySqlDBSetting, myDBSession);
                    myFPPVerificationEntity = this.myFPPVerificationDB.View(Convert.ToInt32(this.Request.QueryString["cust_prospect_id"]));
                }
                strInJob = "";
                myds = new DataSet();
                myHeaderTable = new DataTable();
                myDetailTable = new DataTable();
                this.myFPPVerificationDB = FPPVerificationDB.Create(myMySqlDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((FPPVerificationEntity)HttpContext.Current.Session["myFPPVerificationEntity" + strKey]);

                myDetailTable = myMySqlDBSetting.GetDataTable("SELECT * FROM cust_prospect_doc WHERE cust_prospect_id=?", true, myFPPVerificationEntity.cust_prospect_id);
                gvUploadDoc.DataBind();

                if (Convert.ToInt32(this.Request.QueryString["cust_prospect_doc_id"]) != 0)
                {
                    DownloadDoc(Convert.ToInt32(this.Request.QueryString["cust_prospect_doc_id"]));
                }
            }
        }
        private void SetApplication(FPPVerificationEntity FPPVerificationEntity)
        {
            if (this.myFPPVerificationEntity != FPPVerificationEntity)
            {
                if (FPPVerificationEntity != null)
                {
                    this.myFPPVerificationEntity = FPPVerificationEntity;
                }
                myds = myFPPVerificationEntity.myDataSet;
                myHeaderTable = myds.Tables[0];
                myDetailTable = myds.Tables[1];
                cvMain.DataSource = myHeaderTable;
                cvMain.DataBind();
                gvUploadDoc.DataSource = myDetailTable;
                gvUploadDoc.DataBind();
            }
        }
        private bool SaveProceed(SaveAction saveAction)
        {
            bool bSave = true;
            DataRow myrow = gvUploadDoc.GetDataRow(gvUploadDoc.FocusedRowIndex);
            MySqlConnection myconn = new MySqlConnection(myMySqlDBSetting.ConnectionString);

            MySqlCommand mysqlCommand = new MySqlCommand(@"
                UPDATE cust_prospect_doc
                    SET 
                        doc_stat=@doc_stat,
                        upd_by=@upd_by,
                        upd_dt=@upd_dt
                    WHERE
                        cust_prospect_doc_id=@cust_prospect_doc_id");

            MySqlCommand mysqlCommandHeader = new MySqlCommand(@"
               UPDATE cust_prospect
                    SET 
                        status_aplikasi=@status_aplikasi,
                        upd_by=@upd_by,
                        upd_dt=@upd_dt
                    WHERE
                        cust_prospect_id=@cust_prospect_id");

            mysqlCommand.Connection = myconn;
            mysqlCommandHeader.Connection = myconn;
            try
            {
                myconn.Open();

                MySqlParameter mysqlParameter1 = mysqlCommand.Parameters.Add("@doc_stat", MySqlDbType.VarChar, 20);
                mysqlParameter1.Value = saveAction == SaveAction.Approve ? "APPROVE" : "REJECT";
                mysqlParameter1.Direction = ParameterDirection.Input;

                MySqlParameter mysqlParameter2 = mysqlCommand.Parameters.Add("@upd_by", MySqlDbType.VarChar, 50);
                mysqlParameter2.Value = Convert.ToString(this.UserName);
                mysqlParameter2.Direction = ParameterDirection.Input;

                MySqlParameter mysqlParameter3 = mysqlCommand.Parameters.Add("@upd_dt", MySqlDbType.DateTime);
                mysqlParameter3.Value = myMySqlDBSetting.GetServerTime();
                mysqlParameter3.Direction = ParameterDirection.Input;

                MySqlParameter mysqlParameter4 = mysqlCommand.Parameters.Add("@cust_prospect_doc_id", MySqlDbType.Int32);
                mysqlParameter4.Value = Convert.ToInt32(myrow["cust_prospect_doc_id"]);
                mysqlParameter4.Direction = ParameterDirection.Input;

                mysqlCommand.ExecuteNonQuery();

                MySqlParameter mysqlParameterHeader1 = mysqlCommandHeader.Parameters.Add("@status_aplikasi", MySqlDbType.VarChar, 20);
                mysqlParameterHeader1.Value = "ON PROGRESS";
                mysqlParameterHeader1.Direction = ParameterDirection.Input;

                MySqlParameter mysqlParameterHeader2 = mysqlCommandHeader.Parameters.Add("@upd_by", MySqlDbType.VarChar, 50);
                mysqlParameterHeader2.Value = Convert.ToString(this.UserName);
                mysqlParameterHeader2.Direction = ParameterDirection.Input;

                MySqlParameter mysqlParameterHeader3 = mysqlCommandHeader.Parameters.Add("@upd_dt", MySqlDbType.DateTime);
                mysqlParameterHeader3.Value = myMySqlDBSetting.GetServerTime();
                mysqlParameterHeader3.Direction = ParameterDirection.Input;

                MySqlParameter mysqlParameterHeader4 = mysqlCommandHeader.Parameters.Add("@cust_prospect_id", MySqlDbType.Int32);
                mysqlParameterHeader4.Value = Convert.ToInt32(myFPPVerificationEntity.cust_prospect_id);
                mysqlParameterHeader4.Direction = ParameterDirection.Input;

                mysqlCommandHeader.ExecuteNonQuery();
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

                switch (Convert.ToString(myFPPVerificationEntity.group_mnc_cust).ToUpper())
                {
                    case "GROUP":
                        strInJob = "9";
                        break;
                    case "NON GROUP":
                        strInJob = "1";
                        switch (Convert.ToString(myFPPVerificationEntity.jenis_profesi).ToUpper())
                        {
                            case "WIRASWASTA":
                                strInJob = "2";
                                break;
                            case "PROFESSIONAL":
                                strInJob = "3";
                                break;
                        }
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
                    sqlParameter3.Value = Convert.ToString(myFPPVerificationEntity.gender_cust);
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@p_education", SqlDbType.VarChar, 10);
                    sqlParameter4.Value = Convert.ToString(myFPPVerificationEntity.education_cust);
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@p_marital", SqlDbType.NVarChar, 2);
                    sqlParameter5.Value = Convert.ToString(myFPPVerificationEntity.marital_stat_cust);
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
                    sqlParameter14.Value = myDBSetting.GetServerTime(); ;
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
                    sqlParameter21.Value = Convert.ToDateTime(myFPPVerificationEntity.tanggal_lahir_cust);
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
                    sqlParameter28.Value = "KONTRAK";
                SqlParameter sqlParameter29 = sqlCommand.Parameters.Add("@p_lamakerja", SqlDbType.SmallInt);
                    sqlParameter29.Value = Convert.ToInt16(myFPPVerificationEntity.pengalaman_kerja);
                SqlParameter sqlParameter30 = sqlCommand.Parameters.Add("@p_injob ", SqlDbType.SmallInt);
                    sqlParameter30.Value = strInJob;
                SqlParameter sqlParameter31 = sqlCommand.Parameters.Add("@p_contactname", SqlDbType.NVarChar, 150);
                    sqlParameter31.Value = Convert.ToString(myFPPVerificationEntity.nama_ec);
                SqlParameter sqlParamete32 = sqlCommand.Parameters.Add("@p_contacthp", SqlDbType.NVarChar, 25);
                    sqlParamete32.Value = Convert.ToString(myFPPVerificationEntity.hp_ec);
                SqlParameter sqlParameter33 = sqlCommand.Parameters.Add("@p_contactrelation", SqlDbType.NVarChar, 2);
                    sqlParameter33.Value = Convert.ToString(myFPPVerificationEntity.relationship_ec);
                SqlParameter sqlParameter34 = sqlCommand.Parameters.Add("@p_contactemail", SqlDbType.NVarChar, 50);
                    sqlParameter34.Value = Convert.ToString(myFPPVerificationEntity.email_ec);
                SqlParameter sqlParameter35 = sqlCommand.Parameters.Add("@p_INSPOUNAME", SqlDbType.NVarChar, 100);
                    sqlParameter35.Value = Convert.ToString(myFPPVerificationEntity.nama_spouse);
                SqlParameter sqlParameter36 = sqlCommand.Parameters.Add("@p_kotalahirspouse", SqlDbType.NVarChar, 100);
                    sqlParameter36.Value = Convert.ToString(myFPPVerificationEntity.kota_ktp_spouse);
                SqlParameter sqlParameter37 = sqlCommand.Parameters.Add("@p_tgllahirspouse", SqlDbType.DateTime);
                    sqlParameter37.Value = Convert.ToDateTime(myFPPVerificationEntity.tanggal_lahir_spouse);
                SqlParameter sqlParameter38 = sqlCommand.Parameters.Add("@p_alamatspouse", SqlDbType.NVarChar, 250);
                    sqlParameter38.Value = Convert.ToString(myFPPVerificationEntity.alamat_ktp_spouse);
                SqlParameter sqlParameter39 = sqlCommand.Parameters.Add("@p_hpspouse", SqlDbType.NVarChar, 20);
                    sqlParameter39.Value = Convert.ToString(myFPPVerificationEntity.hp_spouse);
                SqlParameter sqlParameter40 = sqlCommand.Parameters.Add("@p_emailspouse", SqlDbType.NVarChar, 55);
                    sqlParameter40.Value = Convert.ToString(myFPPVerificationEntity.email_spouse);

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
            return bApprove;
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            cplMain.JSProperties["cpType"] = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            switch (callbackParam[0].ToUpper())
            {
                case "APPROVE":
                    SaveProceed(SaveAction.Approve);
                    cplMain.JSProperties["cpAlertMessage"] = "Document has been approved...";
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
                case "DOWNLOAD":
                    break;
            }
        }

        protected void cvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxCardView).DataSource = myHeaderTable;
        }
        protected void cvMain_CustomCallback(object sender, ASPxCardViewCustomCallbackEventArgs e)
        {

        }
        protected void cvMain_CustomColumnDisplayText(object sender, ASPxCardViewColumnDisplayTextEventArgs e)
        {

        }
        protected void cvMain_HtmlCardPrepared(object sender, ASPxCardViewHtmlCardPreparedEventArgs e)
        {

        }
        protected void cvMain_CustomButtonCallback(object sender, ASPxCardViewCustomButtonCallbackEventArgs e)
        {

        }
        protected void cvMain_CardLayoutCreated(object sender, ASPxCardViewCardLayoutCreatedEventArgs e)
        {
            ASPxCardView cardView = sender as ASPxCardView;
            if (e.VisibleIndex > -1)
            {
                foreach (object card in e.Properties.Items)
                {
                    if (card is CardViewColumnLayoutItem)
                    {
                        CardViewColumnLayoutItem colItem = card as CardViewColumnLayoutItem;
                        string strJenisPekerjaan = Convert.ToString(cardView.GetCardValues(e.VisibleIndex, "jenis_pekerjaan_cust"));
                        if (strJenisPekerjaan != "Professional")
                        {
                            if(colItem.ColumnName == "jenis_profesi")
                                colItem.Visible = false;
                            if (colItem.ColumnName == "bidang_usaha")
                                colItem.Visible = false;
                            if (colItem.ColumnName == "pengalaman_kerja")
                                colItem.Visible = false;
                        }
                        if (strJenisPekerjaan != "Wirausaha")
                        {
                            if (colItem.ColumnName == "jenis_profesi")
                                colItem.Visible = false;
                            if (colItem.ColumnName == "bidang_usaha")
                                colItem.Visible = false;
                            if (colItem.ColumnName == "pengalaman_kerja")
                                colItem.Visible = false;
                        }
                    }
                }
            }
        }

        protected void gvUploadDoc_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myDetailTable;
        }
        protected void gvUploadDoc_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void gvUploadDoc_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "btnGridUploadDocDownload")
            {
                try
                {
                    object obj = gvUploadDoc.GetRowValues(e.VisibleIndex, gvUploadDoc.KeyFieldName);
                    if (obj != null && obj != DBNull.Value)
                    {
                        FileDocID = System.Convert.ToInt32(obj);
                    }
                    ASPxWebControl.RedirectOnCallback(string.Format("FPPVerificationEntry.aspx?cust_prospect_id=" + myFPPVerificationEntity.cust_prospect_id + "&cust_prospect_doc_id=" + FileDocID.ToString()));
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }
        protected void gvUploadDoc_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.ButtonID == "btnProceed")
            {
                if (Convert.ToString(gvUploadDoc.GetRowValues(e.VisibleIndex, "doc_stat")) == "APPROVE")
                    e.Enabled = false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/Syariah/FPPVerification/FPPVerificationList.aspx");
        }

    }
}