using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.Mitra
{
    public partial class MitraEntry : BasePage
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
        protected MitraDB myMitraDB
        {
            get { isValidLogin(false); return (MitraDB)HttpContext.Current.Session["myMitraDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMitraDB" + this.ViewState["_PageID"]] = value; }
        }
        protected MitraEntity myMitraEntity
        {
            get { isValidLogin(false); return (MitraEntity)HttpContext.Current.Session["myMitraEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMitraEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataSet myds
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myds" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myds" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myHeaderTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myHeadeTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myHeadeTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDetailTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myBankDetailTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myBankDetailTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myBankDetailTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myUploadDocDetailTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myUploadDocDetailTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUploadDocDetailTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myProvinsiTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myProvinsiTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myProvinsiTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myKotaKabupatenTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myKotaKabupatenTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myKotaKabupatenTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DXSSAction myAction
        {
            get { isValidLogin(false); return (DXSSAction)HttpContext.Current.Session["myAction" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAction" + this.ViewState["_PageID"]] = value; }
        }
        protected DXSSType myDocType
        {
            get { isValidLogin(false); return (DXSSType)HttpContext.Current.Session["myDocType" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDocType" + this.ViewState["_PageID"]] = value; }
        }
        protected string strKey
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]] = value; }
        }
        protected string sFilePathName
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["MitraFilePathName" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["MitraFilePathName" + this.ViewState["_PageID"]] = value; }
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

                if (this.Request.QueryString["SourceKey"] != null && this.Request.QueryString["Type"] != null)
                {
                    this.myMitraDB = MitraDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                    myMitraEntity = this.myMitraDB.View(Convert.ToInt32(this.Request.QueryString["SourceKey"]));
                }
                myds = new DataSet();
                myHeaderTable = new DataTable();
                myDetailTable = new DataTable();
                myBankDetailTable = new DataTable();
                myUploadDocDetailTable = new DataTable();
                myProvinsiTable = new DataTable();
                myKotaKabupatenTable = new DataTable();
                this.myMitraDB = MitraDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((MitraEntity)HttpContext.Current.Session["myMitraEntity" + strKey]);

                if (this.Request.QueryString["FileDoc"] != null)
                {
                    DownloadAtt(this.Request.QueryString["FileDoc"]);
                }
                if (!IsCallback)
                {

                }
            }
        }
        private void SetApplication(MitraEntity MitraEntity)
        {
            if (this.myMitraEntity != MitraEntity)
            {
                if (MitraEntity != null)
                {
                    this.myMitraEntity = MitraEntity;
                }
                myAction = this.myMitraEntity.Action;
                myDocType = this.myMitraEntity.DocType;
                myds = myMitraEntity.myDataSet;
                myHeaderTable = myds.Tables[0];
                myBankDetailTable = myds.Tables[1];
                myUploadDocDetailTable = myds.Tables[2];
                myds.Tables[1].DefaultView.Sort = "MBankKey";
                gvBankDetail.DataSource = myBankDetailTable;
                gvBankDetail.DataBind();
                gvUploadDoc.DataSource = myUploadDocDetailTable;
                gvUploadDoc.DataBind();
                BindingMaster();
                Accessable();
                Setuplookupedit();
            }
        }
        private void Setuplookupedit()
        {
            myProvinsiTable = myLocalDBSetting.GetDataTable("SELECT id, nama FROM [dbo].[WilayahProvinsi] ORDER BY nama", false);
            cbProvinsi.DataSource = myProvinsiTable;
            cbProvinsi.DataBind();
            FillKotaKabupaten(myMitraEntity.Provinsi.ToString());
        }
        private void Accessable()
        {
            txtMitraCode.ReadOnly = true;
            txtMitraCode.BackColor = System.Drawing.Color.Transparent;
            txtBranch.ReadOnly = true;
            txtBranch.BackColor = System.Drawing.Color.Transparent;

            if (myAction == DXSSAction.New)
            {
                gvBankDetail.Columns["colNo"].Visible = false;
                DataRow[] myrowDocNo = myLocalDBSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='MTR'", "", DataViewRowState.CurrentRows);
                if (myrowDocNo != null)
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupMitraEntry").Caption += " - Next Possible Mitra Code : " + Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myDBSetting.GetServerTime());
                }
            }
            else if (myAction == DXSSAction.View)
            {
                btnSave.Visible = false;
                gvBankDetail.Columns["colNo"].Visible = true;
                gvBankDetail.Columns["ClmnCommand"].Visible = false;
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupMitraEntry").Caption = "View Mitra";

                chkActive.ReadOnly = true;
                chkActive.BackColor = System.Drawing.Color.Transparent;

                txtNamaLengkap.ReadOnly = true;
                txtNamaLengkap.BackColor = System.Drawing.Color.Transparent;

                txtTempatLahir.ReadOnly = true;
                txtTempatLahir.BackColor = System.Drawing.Color.Transparent;

                deTglLahir.ReadOnly = true;
                deTglLahir.BackColor = System.Drawing.Color.Transparent;

                mmAddress.ReadOnly = true;
                mmAddress.BackColor = System.Drawing.Color.Transparent;

                txtContactPerson.ReadOnly = true;
                txtContactPerson.BackColor = System.Drawing.Color.Transparent;

                txtEmail.ReadOnly = true;
                txtEmail.BackColor = System.Drawing.Color.Transparent;

                txtNoTlp.ReadOnly = true;
                txtNoTlp.BackColor = System.Drawing.Color.Transparent;

                txtHp.ReadOnly = true;
                txtHp.BackColor = System.Drawing.Color.Transparent;

                txtNoWhatsApp.ReadOnly = true;
                txtNoWhatsApp.BackColor = System.Drawing.Color.Transparent;

                txtAktePendirian.ReadOnly = true;
                txtAktePendirian.BackColor = System.Drawing.Color.Transparent;

                txtNPWP.ReadOnly = true;
                txtNPWP.BackColor = System.Drawing.Color.Transparent;

                rbtjenisMitra.ReadOnly = true;
                rbtjenisMitra.BackColor = System.Drawing.Color.Transparent;

                rbtSubMitra.ReadOnly = true;
                rbtSubMitra.BackColor = System.Drawing.Color.Transparent;

                luMitra.ReadOnly = true;
                luMitra.DropDownButton.Visible = false;
                luMitra.BackColor = System.Drawing.Color.Transparent;

                cbTipeMitra.ReadOnly = true;
                cbTipeMitra.BackColor = System.Drawing.Color.Transparent;

                cbProvinsi.ReadOnly = true;
                cbProvinsi.BackColor = System.Drawing.Color.Transparent;

                cbKotaKabupaten.ReadOnly = true;
                cbKotaKabupaten.BackColor = System.Drawing.Color.Transparent;

                cbPIC.ReadOnly = true;
                cbPIC.BackColor = System.Drawing.Color.Transparent;

                mmProfile.ReadOnly = true;
                mmProfile.BackColor = System.Drawing.Color.Transparent;

                if (myMitraEntity.MKey != null)
                {
                    ASPxFormLayout.FindItemOrGroupByName("lgUploadTab").ClientVisible = true;
                }
            }
            else if (myAction == DXSSAction.Edit)
            {
                gvBankDetail.Columns["colNo"].Visible = false;
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupMitraEntry").Caption = "Edit Mitra";
                if (myMitraEntity.MKey != null)
                {
                    ASPxFormLayout.FindItemOrGroupByName("lgUploadTab").ClientVisible = true;
                }

                txtNamaLengkap.ReadOnly = true;
                txtNamaLengkap.BackColor = System.Drawing.Color.Transparent;
            }
        }
        private void BindingMaster()
        {
            txtMitraCode.Value = myMitraEntity.MCode;
            txtNamaLengkap.Value = myMitraEntity.Nama;
            txtTempatLahir.Value = myMitraEntity.TempatLahir;
            deTglLahir.Value = Convert.ToString(myMitraEntity.TanggalLahir) == "" ? DBNull.Value : myMitraEntity.TanggalLahir;
            mmAddress.Value = myMitraEntity.Address;
            txtContactPerson.Value = myMitraEntity.ContactPerson;
            txtEmail.Value = myMitraEntity.Email;
            txtNoTlp.Value = myMitraEntity.NoTlp;
            txtHp.Value = myMitraEntity.Hp;
            txtNoWhatsApp.Value = myMitraEntity.NoWhatsApp;
            txtNPWP.Value = myMitraEntity.NPWP;
            txtAktePendirian.Value = myMitraEntity.AktePendirian;
            chkActive.CheckState = Convert.ToString(myMitraEntity.IsActive) == "T" ? CheckState.Checked : CheckState.Unchecked;
            if(myMitraEntity.JenisMitra != DBNull.Value)
                rbtjenisMitra.Items.FindByValue(Convert.ToString(myMitraEntity.JenisMitra)).Selected = true;
            if (myMitraEntity.IsSubMitra != DBNull.Value)
                rbtSubMitra.Items.FindByValue(Convert.ToString(myMitraEntity.IsSubMitra)).Selected = true;
            luMitra.Value = myMitraEntity.SubMitra;
            txtBranch.Value = myMitraEntity.Branch;
            cbTipeMitra.Value = myMitraEntity.TipeMitra;
            cbProvinsi.Value = myMitraEntity.Provinsi;
            cbKotaKabupaten.Value = myMitraEntity.KotaKabupaten;
            cbPIC.Value = myMitraEntity.PIC;
            mmProfile.Value = myMitraEntity.Profile;
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;

            myMitraEntity.MCode = txtMitraCode.Value;
            myMitraEntity.Nama = txtNamaLengkap.Value;
            myMitraEntity.TempatLahir = txtTempatLahir.Value;
            myMitraEntity.TanggalLahir = deTglLahir.Text == "" ? DateTime.MinValue : deTglLahir.Value;
            myMitraEntity.Address = mmAddress.Value;
            myMitraEntity.ContactPerson = txtContactPerson.Value;
            myMitraEntity.Email = txtEmail.Value;
            myMitraEntity.NoTlp = txtNoTlp.Value;
            myMitraEntity.Hp = txtHp.Value;
            myMitraEntity.NoWhatsApp = txtNoWhatsApp.Value;
            myMitraEntity.JenisMitra = rbtjenisMitra.Value;
            myMitraEntity.NPWP = txtNPWP.Value;
            myMitraEntity.AktePendirian = txtAktePendirian.Value;
            myMitraEntity.IsSubMitra = rbtSubMitra.Value;
            myMitraEntity.IsActive = chkActive.Value;
            myMitraEntity.SubMitra = Convert.ToString(rbtSubMitra.Value) == "YA" ? luMitra.Value : "";
            myMitraEntity.Branch = txtBranch.Value;
            myMitraEntity.TipeMitra = cbTipeMitra.Value;
            myMitraEntity.Provinsi = cbProvinsi.Value;
            myMitraEntity.KotaKabupaten = cbKotaKabupaten.Value;
            myMitraEntity.PIC = cbPIC.Value;
            myMitraEntity.Profile = mmProfile.Value;

            if (myAction == DXSSAction.New)
            {
                myMitraEntity.CreatedBy = UserID;
                myMitraEntity.CreatedDateTime = myLocalDBSetting.GetServerTime();
                myMitraEntity.LastModifiedBy = UserID;
                myMitraEntity.LastModifiedTime = myLocalDBSetting.GetServerTime();
            }
            myMitraEntity.Save(this.UserID, this.UserName, SaveAction.Save);
            return bSave;
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            return errorF;
        }
        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string urlsave = "";
            urlsave = "~/Transactions/Syariah/Mitra/MitraMaint.aspx";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlConnection SQLConn = new SqlConnection(dbsetting.ConnectionString);
            string strmessageError = "";

            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];           

            switch (callbackParam[0].ToUpper())
            {
                case "REFRESH":
                    myUploadDocDetailTable.Clear();
                    myUploadDocDetailTable = myLocalDBSetting.GetDataTable("SELECT * FROM MitraDocumentDetail WHERE MKey=? ORDER BY DtlUploadKey", false, myMitraEntity.MKey);
                    gvUploadDoc.DataBind();
                    break;
                case "ON_LOAD":
                    break;
                case "SAVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to save this data?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "SAVE":
                    Save(SaveAction.Save);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
                case "SUBMITRA":
                    cplMain.JSProperties["cpVisible"] = null;
                    if (paramValue.ToString().ToUpper() == "YA")
                    {
                        cplMain.JSProperties["cpVisible"] = true;
                    }
                    else
                    {
                        cplMain.JSProperties["cpVisible"] = false;
                    }
                    break;
            }
        }

        protected void gvBankDetail_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myds.Tables[1];
        }
        protected void gvBankDetail_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }
        protected void gvBankDetail_Init(object sender, EventArgs e)
        {

        }
        protected void gvBankDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }
        protected void gvBankDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["BankName"] == null) throw new Exception("Column 'Bank Name' is mandatory.");
            if (e.NewValues["BankBranch"] == null) throw new Exception("Column 'Branch' is mandatory.");
            if (e.NewValues["BankAccNo"] == null) throw new Exception("Column 'Acc No' is mandatory.");
            if (e.NewValues["BankAccName"] == null) throw new Exception("Column 'Acc Name' is mandatory.");
            if (StrErrorMsg == "")
            {
                gvBankDetail.JSProperties["cpCmd"] = "INSERT";

                DataRow[] ValidLinesRows = myBankDetailTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
                int seq = SeqUtils.GetLastSeq(ValidLinesRows);

                myBankDetailTable.Rows.Add(myMitraEntity.Mitracommand.BankUniqueKey(), myMitraEntity.MKey, seq, e.NewValues["BankName"], e.NewValues["BankBranch"], e.NewValues["BankAccNo"], e.NewValues["BankAccName"]);

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvBankDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["BankName"] == null) throw new Exception("Column 'Bank Name' is mandatory.");
            if (e.NewValues["BankBranch"] == null) throw new Exception("Column 'Branch' is mandatory.");
            if (e.NewValues["BankAccNo"] == null) throw new Exception("Column 'Acc No' is mandatory.");
            if (e.NewValues["BankAccName"] == null) throw new Exception("Column 'Acc Name' is mandatory.");
            if (StrErrorMsg == "")
            {
                gvBankDetail.JSProperties["cpCmd"] = "UPDATE";
                int editingRowVisibleIndex = gvBankDetail.EditingRowVisibleIndex;
                int id = (int)gvBankDetail.GetRowValues(editingRowVisibleIndex, "MBankKey");
                DataRow dr = myBankDetailTable.Rows.Find(id);
                dr["BankName"] = e.NewValues["BankName"];
                dr["BankBranch"] = e.NewValues["BankBranch"];
                dr["BankAccNo"] = e.NewValues["BankAccNo"];
                dr["BankAccName"] = e.NewValues["BankAccName"];

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvBankDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            gvBankDetail.JSProperties["cpCmd"] = "DELETE";
            int id = (int)e.Keys["MBankKey"];
            DataRow dr = myBankDetailTable.Rows.Find(id);

            myDetailTable.Rows.Remove(dr);

            ASPxGridView grid = sender as ASPxGridView;
            grid.CancelEdit();
            e.Cancel = true;
        }

        protected void gvUploadDoc_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myUploadDocDetailTable;
        }    
        protected void gvUploadDoc_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }
        protected void gvUploadDoc_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void gvUploadDoc_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            string htmlId = string.Format("cell_{0}_{1}", e.VisibleIndex, e.DataColumn.FieldName);
            string cellClickHandler = string.Format(@"onCellClick(""{0}"", ""{1}"", ""{2}"")", e.DataColumn.FieldName, e.GetValue(e.DataColumn.FieldName), htmlId);
            e.Cell.Attributes.Add("onclick", cellClickHandler);
            e.Cell.Attributes.Add("id", htmlId);
        }
        protected void gvUploadDoc_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "GridbtnDownload")
            {
                try
                {
                    object obj = gvUploadDoc.GetRowValues(e.VisibleIndex, "DocPath");
                    if (obj != null && obj != DBNull.Value)
                    {
                        FileDoc = Convert.ToString(obj);
                    }
                    ASPxWebControl.RedirectOnCallback(string.Format("MitraEntry.aspx?FileDoc=" + FileDoc));
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }

        protected void CtlUpload_FilesUploadComplete(object sender, FilesUploadCompleteEventArgs e)
        {
            for (int i = 0; i < CtlUpload.UploadedFiles.Length; i++)
            {
                if (CtlUpload.UploadedFiles[i] != null)
                {
                    UploadedFile file = CtlUpload.UploadedFiles[i];
                    sFilePathName = HttpContext.Current.Session["UserID"].ToString() + "-" + Guid.NewGuid() + Path.GetExtension(CtlUpload.UploadedFiles[i].FileName);
                    string sfilePath = MapPath(@"~/Transactions/Syariah/MitraUploadDoc/" + sFilePathName);
                    string sRemark = CtlUpload.UploadedFiles[i].FileName;
                    file.SaveAs(sfilePath);

                    SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
                    myconn.Open();
                    SqlTransaction trans = myconn.BeginTransaction();
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[MitraDocumentDetail] (MKey, Remark, DocPath) VALUES (@MKey, @Remark, @DocPath)");
                        sqlCommand.Connection = myconn;
                        sqlCommand.Transaction = trans;

                        SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@MKey", SqlDbType.Int);
                        sqlParameter1.Value = myMitraEntity.MKey;
                        sqlParameter1.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@Remark", SqlDbType.NVarChar);
                        sqlParameter2.Value = sRemark;
                        sqlParameter2.Direction = ParameterDirection.Input;
                        SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@DocPath", SqlDbType.NVarChar);
                        sqlParameter3.Value = sFilePathName;
                        sqlParameter3.Direction = ParameterDirection.Input;
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
                }
            }
        }
        private void DownloadAtt(object myparam)
        {
            try
            {
                FileInfo file = new FileInfo(MapPath(@"~/Transactions/Syariah/MitraUploadDoc/" + myparam));
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
                {
                    Response.SuppressContent = false;
                    Response.Write("No file is found");
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void cbProvinsi_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxComboBox).DataSource = myProvinsiTable;
        }
        protected void cbKotaKabupaten_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxComboBox).DataSource = myKotaKabupatenTable;
        }
        protected void cbKotaKabupaten_Callback(object sender, CallbackEventArgsBase e)
        {
            FillKotaKabupaten(e.Parameter);
        }
        protected void FillKotaKabupaten(string strprovinsiid)
        {
            if (string.IsNullOrEmpty(strprovinsiid)) return;
            myKotaKabupatenTable.Clear();
            myKotaKabupatenTable = myLocalDBSetting.GetDataTable("SELECT * FROM [dbo].[WilayahKotaKabupaten] WHERE provinsi_id=?", false, strprovinsiid);
            cbKotaKabupaten.DataSource = myKotaKabupatenTable;
            cbKotaKabupaten.DataBind();
        }
    }
}