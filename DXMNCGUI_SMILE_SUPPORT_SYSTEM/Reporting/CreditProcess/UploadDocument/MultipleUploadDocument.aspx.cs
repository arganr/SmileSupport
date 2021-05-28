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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.UploadDocument
{
    public partial class MultipleUploadDocument : BasePage
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
        protected DataTable dtDetailUpload
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["dtDetailUpload" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["dtDetailUpload" + this.ViewState["_PageID"]] = value; }
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
        protected long sizeInKilobytes
        {
            get { isValidLogin(false); return (long)HttpContext.Current.Session["sizeInKilobytes" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["sizeInKilobytes" + this.ViewState["_PageID"]] = value; }
        }
        protected Stream myFs
        {
            get { isValidLogin(false); return (Stream)HttpContext.Current.Session["myFs" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myFs" + this.ViewState["_PageID"]] = value; }
        }

        const string UploadDirectory = "~/Content/UploadControl/";
        string resultFileUrl = String.Empty;
        string name = String.Empty;
        string url = String.Empty;
        string sizeText = String.Empty;
        string docid = String.Empty;

        private DataTable LoadDatatable()
        {
            string StrErrorMsg = "";
            SqlLocalDBSetting localdbSetting = this.myDBLocalSetting;
            SqlConnection SQLConn = new SqlConnection(localdbSetting.ConnectionString);
            DataTable dt = new DataTable();
            try
            {

                SQLConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"select ID, Type, SubType, Remarks, cast('' as varbinary(max)) as Attachment, 
                                                            Name, Ext, FileSize
                                                                from [dbo].[DocumentFile] WHERE ID=0", SQLConn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.FillSchema(dt, SchemaType.Source);
                    dt.Columns["ID"].DataType = System.Type.GetType("System.Int32");
                    dt.Columns["ID"].AutoIncrement = true;
                    dt.Columns["ID"].AutoIncrementSeed = 1000;
                    dt.Columns["ID"].AutoIncrementStep = 1;
                    da.Fill(dt);
                    da.Dispose();

                    dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };

                    foreach (DataColumn col in dt.Columns) col.ReadOnly = false;
                }

            }
            catch (Exception ex)
            {
                StrErrorMsg = "Sorry, Error :" + ex.Message;
                //throw new ArgumentException(ex.Message);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + StrErrorMsg + "');", true);
            }
            finally
            {
                SQLConn.Close();
            }
            return dt;
        }

        private DataTable LoadSubDocumentDatatable(string DocumentCode)
        {
            string StrErrorMsg = "";
            DataTable dtSubType = new DataTable();
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlConnection SQLConn = new SqlConnection(localdbsetting.ConnectionString);
            try
            {
                SQLConn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[MasterDocumentSubDesc] WHERE DocumentCode=@DocumentCode", SQLConn))
                {
                    cmd.Parameters.AddWithValue("DocumentCode", DocumentCode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtSubType);
                }
            }
            catch (Exception ex)
            {
                StrErrorMsg = "Sorry, Error :" + ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + StrErrorMsg + "');", true);
            }
            finally
            {
                SQLConn.Close();
            }
            return dtSubType;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin(false);


                myDBSetting = dbsetting;
                myDBLocalSetting = localdbsetting;
                myDBSession = dbsession;

                dtDetailUpload = LoadDatatable();
                gvMain.DataSource = dtDetailUpload;
                gvMain.DataBind();

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();

                myDBSetting = dbsetting;
                myDBLocalSetting = localdbsetting;
                myDBSession = dbsession;
            }
            sdsMasterDocument.ConnectionString = localdbsetting.ConnectionString;
        }

        protected void gvMain_Init(object sender, EventArgs e)
        {

        }
        protected void gvMain_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }
        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = dtDetailUpload;
        }
        protected void gvMain_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            BinaryReader br = new BinaryReader(myFs);
            Byte[] bytes = br.ReadBytes((Int32)myFs.Length);

            if (e.NewValues["Type"] == null)
            {
                StrErrorMsg = "Document type harus diisi.";
                throw new Exception(StrErrorMsg);
            }
            else if (e.NewValues["SubType"] == null)
            {
                StrErrorMsg = "Sub document type harus diisi.";
                throw new Exception(StrErrorMsg);
            }
            else if (e.NewValues["Remarks"] == null)
            {
                StrErrorMsg = "Remark harus diisi.";
                throw new Exception(StrErrorMsg);
            }
            else if (bytes == null)
            {
                StrErrorMsg = "Attachment harus diisi.";
                throw new Exception(StrErrorMsg);
            }
            if (StrErrorMsg == "")
            {
                dtDetailUpload.Rows.Add(
                null, 
                e.NewValues["Type"], 
                e.NewValues["SubType"], 
                e.NewValues["Remarks"],
                bytes, e.NewValues["Type"], resultExtension, sizeInKilobytes);

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvMain_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            BinaryReader br = new BinaryReader(myFs);
            Byte[] bytes = br.ReadBytes((Int32)myFs.Length);

            if (e.NewValues["Type"] == null)
            {
                StrErrorMsg = "Document type harus diisi.";
                throw new Exception(StrErrorMsg);
            }
            else if (e.NewValues["SubType"] == null)
            {
                StrErrorMsg = "Sub document type harus diisi.";
                throw new Exception(StrErrorMsg);
            }
            else if (e.NewValues["Remarks"] == null)
            {
                StrErrorMsg = "Remark harus diisi.";
                throw new Exception(StrErrorMsg);
            }
            else if (bytes == null)
            {
                StrErrorMsg = "Attachment harus diisi.";
                throw new Exception(StrErrorMsg);
            }
            if (StrErrorMsg == "")
            {
                int editingRowVisibleIndex = gvMain.EditingRowVisibleIndex;
                int id = (int)gvMain.GetRowValues(editingRowVisibleIndex, "ID");
                DataRow dr = dtDetailUpload.Rows.Find(id);

                dr["Type"] = e.NewValues["Type"];
                dr["SubType"] = e.NewValues["SubType"];
                dr["Remarks"] = e.NewValues["Remarks"];
                dr["Attachment"] = bytes;
                dr["Ext"] = resultExtension;
                dr["FileSize"] = sizeInKilobytes;

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvMain_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int id = (int)e.Keys["ID"];
            DataRow dr = dtDetailUpload.Rows.Find(id);
            dtDetailUpload.Rows.Remove(dr);

            ASPxGridView grid = sender as ASPxGridView;
            grid.CancelEdit();
            e.Cancel = true;
        }
        protected void gvMain_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            gvMain.JSProperties["cpCallBack"] = true;
            if (gvMain.IsNewRowEditing == true)
            {
                if (String.IsNullOrEmpty(e.Parameters.ToString()) == false)
                {
                    string[] strParam = e.Parameters.Split('|');
                    string strtype = strParam[0];
                    string Param2 = strParam[1];
                    string Param3 = strParam[2];
                }
            }
        }
        protected void gvMain_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }
        protected void gvMain_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "SubType")
            {
                var cb = (ASPxComboBox)e.Editor;
                cb.Callback += new CallbackEventHandlerBase(SubType_Callback);

                var grid = e.Column.Grid;
                if (!cb.IsCallback)
                {
                    if (!grid.IsNewRowEditing)
                    FillSubTypeComboBox(cb, Convert.ToString(grid.GetRowValues(e.VisibleIndex, "Type")));
                }
            }
        }

        private void SubType_Callback(object sender, CallbackEventArgsBase e)
        {
            if (String.IsNullOrEmpty(e.Parameter.ToString()) == false)
            {
                FillSubTypeComboBox(sender as ASPxComboBox, e.Parameter);
            }
        }

        protected void FillSubTypeComboBox(ASPxComboBox combo, string documentID)
        {
            combo.DataSource = LoadSubDocumentDatatable(documentID);
            combo.DataBindItems();
        }

        protected void gvMain_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {

        }

        protected void UploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            sizeInKilobytes = 0;
            resultExtension = Path.GetExtension(e.UploadedFile.FileName);
            resultFileName = Path.ChangeExtension(Path.GetRandomFileName(), resultExtension);
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
            string StrErrorMsg = "";
            string[] luValue = { null, null, null, null };
            SqlConnection myconn = new SqlConnection(localdbsetting.ConnectionString);
            SqlTransaction trans = null;
            try
            {
                myconn.Open();
                trans = myconn.BeginTransaction();
                luValue = luAppNo.Text.Split(';');
                using (SqlCommand cmd = new SqlCommand("", myconn, trans))
                {
                    if (dtDetailUpload.Rows.Count == 0)
                    {
                        lblerror.Text = "Please input data before submit.";
                    }

                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
                                            INSERT INTO DocumentFile 
                                                (Name, Type, Ext, Remarks, FileDoc, AppNo, CreatedBy, CreatedDateTime, DebiturName, AgreeNo, Module, MemoNo, SubType, Branch, FileSize)
                                                    VALUES 
                                                        (@Name, @Type, @Ext, @Remarks, @FileDoc, @AppNo, @CreatedBy, @CreatedDateTime, @DebiturName, @AgreeNo, @Module, @MemoNo, @SubType, @Branch, @FileSize)";
                    cmd.Parameters.AddWithValue("Name", null);
                    cmd.Parameters.AddWithValue("Type", null);
                    cmd.Parameters.AddWithValue("Ext", null);
                    cmd.Parameters.AddWithValue("Remarks", null);
                    cmd.Parameters.AddWithValue("FileDoc", null);
                    cmd.Parameters.AddWithValue("AppNo", luAppNo.Value);
                    cmd.Parameters.AddWithValue("CreatedBy", UserID);
                    cmd.Parameters.AddWithValue("CreatedDateTime", localdbsetting.GetServerTime());
                    cmd.Parameters.AddWithValue("DebiturName", luValue[3].ToUpper());
                    cmd.Parameters.AddWithValue("AgreeNo", luValue[2].ToUpper());
                    cmd.Parameters.AddWithValue("Module", luValue[4].ToUpper());
                    cmd.Parameters.AddWithValue("MemoNo", luAppNo.Value);
                    cmd.Parameters.AddWithValue("SubType", null);
                    cmd.Parameters.AddWithValue("Branch", luValue[0].ToUpper());
                    cmd.Parameters.AddWithValue("FileSize", 0);
                    foreach (DataRow dr in dtDetailUpload.Rows)
                    {
                        object obj = myDBLocalSetting.ExecuteScalar("SELECT DocumentDesc FROM [dbo].[MasterDocumentDesc] WHERE DocumentCode=?", dr.Field<string>("Type"));
                        if (obj != null && obj != DBNull.Value)
                        {
                            cmd.Parameters["Name"].Value = obj;
                            cmd.Parameters["Type"].Value = obj;
                        }
                        cmd.Parameters["Ext"].Value = dr.Field<string>("Ext");
                        cmd.Parameters["Remarks"].Value = dr.Field<string>("Remarks");
                        cmd.Parameters["FileDoc"].Value = dr.Field<Byte[]>("Attachment");
                        cmd.Parameters["SubType"].Value = dr.Field<string>("SubType");
                        cmd.Parameters["FileSize"].Value = dr.Field<string>("FileSize");
                        cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                StrErrorMsg = "Sorry, Error : " + ex.Message;
                if (trans != null)
                { trans.Rollback(); }
                lblerror.Text = StrErrorMsg;
            }
            finally
            {
                myconn.Close();
            }
            return bSave;
        }
    }
}