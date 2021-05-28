using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.IO.IsolatedStorage;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.UploadDocument
{
    public partial class UploadDocument : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlLocalDBSetting myDBLocalSetting
        {
            get { isValidLogin(false); return (SqlLocalDBSetting)HttpContext.Current.Session["myDBLocalSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBLocalSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]] = value; }
        }
        protected string resultFileName
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["resultFileName" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["resultFileName" + this.ViewState["_PageID"]] = value; }
        }
        protected string resultFilePath
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["resultFilePath" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["resultFilePath" + this.ViewState["_PageID"]] = value; }
        }
        protected string resultExtension
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["resultExtension" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["resultExtension" + this.ViewState["_PageID"]] = value; }
        }
        protected string sizeText
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["sizeText" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["sizeText" + this.ViewState["_PageID"]] = value; }
        }
        protected Stream myFs
        {
            get { isValidLogin(false); return (Stream)HttpContext.Current.Session["myFs" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myFs" + this.ViewState["_PageID"]] = value; } 
        }
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDocumentTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDocumentTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDocumentTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable mySubDocumentTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mySubDocumentTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mySubDocumentTable" + this.ViewState["_PageID"]] = value; }
        }
        protected int FileDocID
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["FileDocID" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["FileDocID" + this.ViewState["_PageID"]] = value; }
        }
        const string UploadDirectory = "~/Content/UploadControl/";
        string resultFileUrl = String.Empty;
        string name = String.Empty;
        string url = String.Empty;
        long sizeInKilobytes = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myDBLocalSetting = localdbsetting;
                myDBSession = dbsession;

                myMainTable = new DataTable();
                myDocumentTable = new DataTable();
                mySubDocumentTable = new DataTable();

                if (this.UserID.Contains("OS"))
                {
                    myMainTable = myDBLocalSetting.GetDataTable("SELECT [ID],[Name],[Type],[Ext],[Remarks],[AppNo],[CreatedBy],[CreatedDateTime],[DebiturName],[AgreeNo],[Module],[SubType],[Branch] FROM [dbo].[DocumentFile] WHERE [AppNo] like '07%' ORDER BY CreatedDateTime DESC", false);
                }
                else
                {
                    myMainTable = myDBLocalSetting.GetDataTable("SELECT [ID],[Name],[Type],[Ext],[Remarks],[AppNo],[CreatedBy],[CreatedDateTime],[DebiturName],[AgreeNo],[Module],[SubType],[Branch] FROM [dbo].[DocumentFile] ORDER BY CreatedDateTime DESC", false);
                }
                gvMain.DataSource = myMainTable;
                gvMain.DataBind();

                Setuplookupedit();

                if (Convert.ToInt32(this.Request.QueryString["ID"]) != 0)
                {
                    SqlConnection myconn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlLocalConnectionString"].ConnectionString);
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM DocumentFile WHERE ID=@ID", myconn);
                    sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(this.Request.QueryString["ID"]));
                    myconn.Open();
                    SqlDataReader dr = sqlCommand.ExecuteReader(); ;
                    if (dr.Read())
                    {
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.ContentType = dr["Type"].ToString() + dr["Ext"].ToString();
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + dr["Name"].ToString() + dr["AppNo"].ToString() + dr["Ext"].ToString());
                        HttpContext.Current.Response.Charset = "";
                        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        HttpContext.Current.Response.BinaryWrite((byte[])dr["FileDoc"]);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                    myconn.Close();
                }
            }
            if (!IsCallback)
            {

            }
        }
        private void Setuplookupedit()
        {
            myDocumentTable = myDBLocalSetting.GetDataTable("SELECT * FROM [dbo].[MasterDocumentDesc] ORDER BY DocumentDesc", false);
            cbDocument.DataSource = myDocumentTable;
            cbDocument.DataBind();
            FillSubDocument(Convert.ToString(cbDocument.Value));
        }
        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string urlsave = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            urlsave = "~/Reporting/CreditProcess/UploadDocument/UploadDocument.aspx";
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];
            string strmessageError = string.Empty;

            switch (callbackParam[0].ToUpper())
            {
                case "UPLOADCONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to upload this document?";
                    cplMain.JSProperties["cplblActionButton"] = "UPLOAD";
                    break;
                case "UPLOAD":
                    Save();
                    cplMain.JSProperties["cpAlertMessage"] = "Upload success...";
                    cplMain.JSProperties["cplblActionButton"] = "UPLOAD";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
            }
        }
        protected void UploadCtrl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            sizeText = "";
            resultExtension = Path.GetExtension(e.UploadedFile.FileName);
            resultFileName = "MyDoc_" + Path.ChangeExtension(Path.GetRandomFileName(), resultExtension);
            resultFileUrl = UploadDirectory + resultFileName;
            resultFilePath = MapPath(resultFileUrl);
            e.UploadedFile.SaveAs(resultFilePath);
            name = e.UploadedFile.FileName;
            url = ResolveClientUrl(resultFileUrl);
            sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
            sizeText = sizeInKilobytes.ToString() + " KB";
            e.CallbackData = resultFileName;
            myFs = e.UploadedFile.FileContent;
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadAtt();
        }

        private bool DownloadAtt()
        {
            bool bDownloadAtt = true;
            FileInfo file = new FileInfo(MapPath(UploadDirectory + resultFileName));
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
            return bDownloadAtt;
        }
        private bool Save()
        {
            bool bSave = true;
            BinaryReader br = new BinaryReader(myFs); 
            Byte[] bytes = br.ReadBytes((Int32)myFs.Length);

            SqlConnection myconn = new SqlConnection(localdbsetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                string[] luValue = { null, null, null, null };              
                string sQuery = @"INSERT INTO DocumentFile
                                    VALUES (@Name, @Type, @Ext, @Remarks, @FileDoc, @AppNo, @CreatedBy, @CreatedDateTime, @DebiturName, @AgreeNo, @Module, @MemoNo, @SubType, @Branch, @FileSize)";
                SqlCommand sqlCommand = new SqlCommand(sQuery);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;
                sqlCommand.Transaction = trans;

                luValue = luAppNo.Text.Split(';');

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
                sqlParameter1.Value = cbDocument.Text;

                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@Type", SqlDbType.NVarChar);
                sqlParameter2.Value = cbDocument.Value;

                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Ext", SqlDbType.NVarChar);
                sqlParameter3.Value = resultExtension;

                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                sqlParameter4.Value = mmRemark1.Value;

                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@FileDoc", SqlDbType.Binary);
                sqlParameter5.Value = bytes;

                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@AppNo", SqlDbType.NVarChar);
                sqlParameter6.Value = luAppNo.Value;

                SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar);
                sqlParameter7.Value = UserID;

                SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime);
                sqlParameter8.Value = myDBLocalSetting.GetServerTime();

                SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@DebiturName", SqlDbType.NVarChar);
                sqlParameter9.Value = luValue[3].ToUpper();

                SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@AgreeNo", SqlDbType.NVarChar);
                sqlParameter10.Value = luValue[2].ToUpper();

                SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@Module", SqlDbType.NVarChar);
                sqlParameter11.Value = luValue[4].ToUpper();

                SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@MemoNo", SqlDbType.NVarChar);
                sqlParameter12.Value = luAppNo.Value;

                SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@SubType", SqlDbType.NVarChar);
                sqlParameter13.Value = cbSubDocument.Value;

                SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@Branch", SqlDbType.NVarChar);
                sqlParameter14.Value = luValue[0].ToUpper();

                SqlParameter sqlParameter15 = sqlCommand.Parameters.Add("@FileSize", SqlDbType.NVarChar);
                sqlParameter15.Value = sizeText;

                sqlCommand.ExecuteNonQuery();
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

            return bSave;
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
        protected void gvMain_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "GridbtnDownload")
            {
                try
                {
                    object obj = gvMain.GetRowValues(e.VisibleIndex, gvMain.KeyFieldName);
                    if (obj != null && obj != DBNull.Value)
                    {
                        FileDocID = System.Convert.ToInt32(obj);
                    }
                    ASPxWebControl.RedirectOnCallback(string.Format("UploadDocument.aspx?ID=" + FileDocID.ToString()));
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }

        protected void cbDocument_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxComboBox).DataSource = myDocumentTable;
        }
        protected void cbSubDocument_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxComboBox).DataSource = mySubDocumentTable;
        }

        protected void cbSubDocument_Callback(object sender, CallbackEventArgsBase e)
        {
            FillSubDocument(e.Parameter);
        }
        protected void FillSubDocument(string strdocument)
        {
            object obj = myDBLocalSetting.ExecuteScalar("SELECT DocumentCode FROM MasterDocumentDesc WHERE DocumentDesc=?", strdocument);
            if (obj != null && obj != DBNull.Value)
            {
                if (string.IsNullOrEmpty(strdocument)) return;
                mySubDocumentTable.Clear();
                mySubDocumentTable = myDBLocalSetting.GetDataTable("SELECT * FROM [dbo].[MasterDocumentSubDesc] WHERE DocumentCode=? ORDER BY SubDocumentDesc", false, obj);
                cbSubDocument.DataSource = mySubDocumentTable;
                cbSubDocument.DataBind();
            }
        }
    }
}