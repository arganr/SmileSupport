using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance
{
    public partial class UploadPaymentTextFile : BasePage
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
        protected string sFilePathName
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["FilePathName" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["FilePathName" + this.ViewState["_PageID"]] = value; }
        }
        protected string FileDoc
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["FileDoc" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["FileDoc" + this.ViewState["_PageID"]] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                if (!IsCallback)
                {

                }
            }
        }

        protected void CtlUpload_FilesUploadComplete(object sender, DevExpress.Web.FilesUploadCompleteEventArgs e)
        {
            for (int i = 0; i < CtlUpload.UploadedFiles.Length; i++)
            {
                if (CtlUpload.UploadedFiles[i] != null)
                {
                    UploadedFile file = CtlUpload.UploadedFiles[i];
                    sFilePathName = HttpContext.Current.Session["UserID"].ToString() + "-" + Guid.NewGuid() + Path.GetExtension(CtlUpload.UploadedFiles[i].FileName);
                    string sfilePath = MapPath(@"~/Transactions/Finance/TempFile/" + sFilePathName);
                    file.SaveAs(sfilePath);

                    DataTable mydt = new DataTable();
                    mydt = GetDataSourceFromFile(sfilePath);

                    SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
                    myconn.Open();
                    SqlTransaction trans = myconn.BeginTransaction();
                    try
                    {
                        foreach (DataRow dataRow in mydt.Rows)
                        {

                            SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[UploadPaymentTextFile] 
                                                                    (Seq, OrderNumber, Tanggal, NomorKontrak, AyoconnectPrice, PotonganAyoconnect, Disburse, Status, PaidOn, PaymentMode, UploadBy, UploadDateTime, UploadStatus, JasaPembayaran) 
                                                                        VALUES 
                                                                        (@Seq, @OrderNumber, @Tanggal, @NomorKontrak, @AyoconnectPrice, @PotonganAyoconnect, @Disburse, @Status, @PaidOn, @PaymentMode, @UploadBy, @UploadDateTime, @UploadStatus, @JasaPembayaran)");
                            sqlCommand.Connection = myconn;
                            sqlCommand.Transaction = trans;

                            sqlCommand.Parameters.AddWithValue("@Seq", dataRow["No"]);
                            sqlCommand.Parameters.AddWithValue("@OrderNumber", dataRow["OrderNumber"]);
                            sqlCommand.Parameters.AddWithValue("@Tanggal", dataRow["Tanggal"]);
                            sqlCommand.Parameters.AddWithValue("@NomorKontrak", dataRow["NomorKontrak"]);
                            sqlCommand.Parameters.AddWithValue("@AyoconnectPrice", dataRow["AyoconnectPrice"]);
                            sqlCommand.Parameters.AddWithValue("@PotonganAyoconnect", dataRow["PotonganAyoconnect"]);
                            sqlCommand.Parameters.AddWithValue("@Disburse", dataRow["Disburse"]);
                            sqlCommand.Parameters.AddWithValue("@Status", dataRow["Status"]);
                            sqlCommand.Parameters.AddWithValue("@PaidOn", dataRow["PaidOn"]);
                            sqlCommand.Parameters.AddWithValue("@PaymentMode", dataRow["PaymentMode"]);
                            sqlCommand.Parameters.AddWithValue("@UploadBy", (object)this.UserName);
                            sqlCommand.Parameters.AddWithValue("@UploadDateTime", (object)myLocalDBSetting.GetServerTime());
                            sqlCommand.Parameters.AddWithValue("@UploadStatus", (object)"NEW");
                            sqlCommand.Parameters.AddWithValue("@JasaPembayaran", (object)cbPenyediaJasa.Value);
                            sqlCommand.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new ArgumentException(ex.Message);
                    }
                    finally
                    {
                        myconn.Close();
                    }
                    File.Delete(sfilePath);
                }
            }
        }

        public DataTable GetDataSourceFromFile(string fileName)
        {
            DataTable dt = new DataTable();
            string[] columns = null;

            var lines = File.ReadAllLines(fileName);

            // assuming the first row contains the columns information
            if (lines.Count() > 0)
            {
                columns = lines[0].Replace(" ", "").Split(new char[] { '|' });

                foreach (var column in columns)
                    dt.Columns.Add(column);
            }

            // reading rest of the data
            for (int i = 1; i < lines.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                string[] values = lines[i].Replace(".", "").Split(new char[] { '|' });

                for (int j = 0; j < values.Count() && j < columns.Count(); j++)
                    dr[j] = values[j];

                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            isValidLogin(false);
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;

            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];

            switch (callbackParam[0].ToUpper())
            {
                case "UPLOAD_FINISH":
                    break;
            }
        }
    }
}