using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.UpdateSLIK
{
    public partial class UpdateSLIKEntry : BasePage
    {
        string SqlQuery = string.Empty;
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
        protected UpdateSLIKDB myUpdateSLIKDB
        {
            get { isValidLogin(false); return (UpdateSLIKDB)HttpContext.Current.Session["myUpdateSLIKDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUpdateSLIKDB" + this.ViewState["_PageID"]] = value; }
        }
        protected UpdateSLIKEntity myUpdateSLIKEntity
        {
            get { isValidLogin(false); return (UpdateSLIKEntity)HttpContext.Current.Session["myUpdateSLIKEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUpdateSLIKEntity" + this.ViewState["_PageID"]] = value; }
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
        protected DataTable myClientTable
        {
            get { isValidLogin(true); return (DataTable)HttpContext.Current.Session["myClientTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myClientTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myUploadDocTable
        {
            get { isValidLogin(true); return (DataTable)HttpContext.Current.Session["myUploadDocTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUploadDocTable" + this.ViewState["_PageID"]] = value; }
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
        protected int FileDocID
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["FileDocID" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["FileDocID" + this.ViewState["_PageID"]] = value; }
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
                    this.myUpdateSLIKDB = UpdateSLIKDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                    myUpdateSLIKEntity = this.myUpdateSLIKDB.View(Convert.ToInt32(this.Request.QueryString["SourceKey"]));
                }
                myds = new DataSet();
                myClientTable = new DataTable();
                myUploadDocTable = new DataTable();
                this.myUpdateSLIKDB = UpdateSLIKDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((UpdateSLIKEntity)HttpContext.Current.Session["myUpdateSLIKEntity" + strKey]);

                if (Convert.ToInt32(this.Request.QueryString["ID"]) != 0)
                {
                    SqlConnection myconn = new SqlConnection(localdbsetting.ConnectionString);
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
        private void SetApplication(UpdateSLIKEntity UpdateSLIKEntity)
        {
            if (this.myUpdateSLIKEntity != UpdateSLIKEntity)
            {
                if (UpdateSLIKEntity != null)
                {
                    this.myUpdateSLIKEntity = UpdateSLIKEntity;
                }
                myAction = this.myUpdateSLIKEntity.Action;
                myDocType = this.myUpdateSLIKEntity.DocType;
                myds = myUpdateSLIKEntity.myDataSet;
                myHeaderTable = myds.Tables[0];
                myDetailTable = myds.Tables[1];
                myds.Tables[1].DefaultView.Sort = "Seq";
                gvDetail.DataSource = myDetailTable;
                gvDetail.DataBind();
                BindingMaster();
                Accessable();
            }
        }
        private void setuplookupedit()
        {
        }
        private void Accessable()
        {

            txtDocNo.ReadOnly = true;
            txtDocNo.BackColor = System.Drawing.Color.Transparent;

            deDocDate.Enabled = false;
            deDocDate.BackColor = System.Drawing.Color.Transparent;

            txtDebitur.Enabled = true;
            txtDebitur.BackColor = System.Drawing.Color.Transparent;

            if (myAction == DXSSAction.New)
            {
                deDocDate.Value = myDBSetting.GetServerTime();
                gvDetail.Columns["colNo"].Visible = false;
                gvDetail.Columns["ClmnCommand2"].Visible = false;
                DataRow[] myrowDocNo = myLocalDBSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='SLK'", "", DataViewRowState.CurrentRows);
                if (myrowDocNo != null)
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApplicationEntry").Caption += " - Next Possible Document Number : " + Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myDBSetting.GetServerTime());
                }
            }
            else if (myAction == DXSSAction.View)
            {
                btnSave.Visible = false;
                gvDetail.Columns["colNo"].Visible = true;
                gvDetail.Columns["ClmnCommand"].Visible = false;
                gvDetail.Columns["ClmnCommand2"].Visible = true;
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApplicationEntry").Caption = "View SLIK Update";

                deDocDate.ReadOnly = true;
                deDocDate.BackColor = System.Drawing.Color.Transparent;

                luAppNo.ReadOnly = true;
                luAppNo.BackColor = System.Drawing.Color.Transparent;

                cbSlikCheck.ReadOnly = true;
                cbSlikCheck.BackColor = System.Drawing.Color.Transparent;

                chkSubmit.ReadOnly = true;
                chkSubmit.BackColor = System.Drawing.Color.Transparent;

                mmRemark1.ReadOnly = true;
                mmRemark1.BackColor = System.Drawing.Color.Transparent;

            }
            else if (myAction == DXSSAction.Edit)
            {
                gvDetail.Columns["colNo"].Visible = false;
                gvDetail.Columns["ClmnCommand2"].Visible = false;
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApplicationEntry").Caption = "Edit SLIK Update";
            }
        }
        private void BindingMaster()
        {
            txtDocNo.Value = myUpdateSLIKEntity.DocNo;
            deDocDate.Value = myUpdateSLIKEntity.DocDate;
            luAppNo.Value = myUpdateSLIKEntity.RefNo;
            mmRemark1.Value = myUpdateSLIKEntity.Remark;
            cbSlikCheck.Value = myUpdateSLIKEntity.SLIKAvailable;
            chkSubmit.Value = myUpdateSLIKEntity.CAChecking;
            txtDebitur.Value = myUpdateSLIKEntity.Debitur;

            if (luAppNo.Value != null)
            {
                myClientTable = LoadClienttable(luAppNo.Value);
                vcDetail.DataSource = myClientTable;
                vcDetail.DataBind();

                myUploadDocTable = LoadUploadDoctable(luAppNo.Value);
                gvUploadDoc.DataSource = myUploadDocTable;
                gvUploadDoc.DataBind();
            }
        }

        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;

            myUpdateSLIKEntity.DocNo = txtDocNo.Value;
            myUpdateSLIKEntity.DocDate = deDocDate.Value;
            myUpdateSLIKEntity.RefNo = luAppNo.Value;
            myUpdateSLIKEntity.Remark = mmRemark1.Value;
            myUpdateSLIKEntity.SLIKAvailable = cbSlikCheck.Value;
            myUpdateSLIKEntity.CAChecking = chkSubmit.Value;
            myUpdateSLIKEntity.Debitur = txtDebitur.Text;

            if (myAction == DXSSAction.New)
            {
                myUpdateSLIKEntity.CreatedBy = UserID;
                myUpdateSLIKEntity.CreatedDateTime = myLocalDBSetting.GetServerTime();
            }
            myUpdateSLIKEntity.Save(this.UserID, this.UserName, SaveAction.Save);
            return bSave;
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            return errorF;
        }

        protected void gvDetail_Init(object sender, EventArgs e)
        {

        }
        protected void gvDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }
        protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["JenisPembiayaan"] == null) throw new Exception("Column 'Jenis Pembiayaan' is mandatory.");
            if (e.NewValues["PerusahaanPembiayaan"] == null) throw new Exception("Column 'Perusahaan Pembiayaan / Bank' is mandatory.");
            if (e.NewValues["AtasNama"] == null) throw new Exception("Column 'Atas Nama' is mandatory.");
            if (e.NewValues["Plafon"] == null) throw new Exception("Column 'Plafon' is mandatory.");
            if (e.NewValues["BakiDebet"] == null) throw new Exception("Column 'Baki Debet' is mandatory.");
            if (e.NewValues["Bunga"] == null) throw new Exception("Column 'Bunga' is mandatory.");
            if (e.NewValues["TglAkadAwal"] == null) throw new Exception("Column 'Akad Awal' is mandatory.");
            if (e.NewValues["TglAwalSisaTenor"] == null) throw new Exception("Column 'Awal Sisa tenor' is mandatory.");
            if (e.NewValues["TglJatuhTempo"] == null) throw new Exception("Column 'Jatuh Tempo' is mandatory.");
            if (e.NewValues["Kolektibilitas"] == null) throw new Exception("Column 'Kolektibilitas' is mandatory.");
            if (e.NewValues["HistoryKolek"] == null) throw new Exception("Column 'History Kolek' is mandatory.");
            if (e.NewValues["AktualOverDue"] == null) throw new Exception("Column 'Aktual Overdue' is mandatory.");
            if (StrErrorMsg == "")
            {
                gvDetail.JSProperties["cpCmd"] = "INSERT";

                DataRow[] ValidLinesRows = myDetailTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
                int seq = SeqUtils.GetLastSeq(ValidLinesRows);

                decimal dAngsuran = 0;
                decimal dJangka = 0, dSisaTenor = 0;
                decimal dPlafon = 0, dBakiDebet = 0, dBunga = 0; 

                DateTime a = Convert.ToDateTime(e.NewValues["TglJatuhTempo"]);
                DateTime b = Convert.ToDateTime(e.NewValues["TglAkadAwal"]);
                DateTime c = Convert.ToDateTime(e.NewValues["TglAwalSisaTenor"]);

                dPlafon = Convert.ToDecimal(e.NewValues["Plafon"]);
                dBakiDebet = Convert.ToDecimal(e.NewValues["BakiDebet"]);
                dBunga = Convert.ToDecimal(e.NewValues["Bunga"])/100;
                dJangka = Convert.ToDecimal((a - b).TotalDays) / 365;
                dSisaTenor = Convert.ToDecimal((a - c).TotalDays) / 365;

                if(dBunga != 0 || dBakiDebet != 0)
                {
                    if (Convert.ToDecimal(e.NewValues["JenisPembiayaan"]) == 1)
                    {
                        if (dPlafon > dBakiDebet)
                        {
                            dAngsuran = (((dBunga / Convert.ToDecimal(1.7) * dJangka) + 1) * dPlafon) / (dJangka * 12);
                        }
                        else
                        {
                            dAngsuran = (((dBunga / Convert.ToDecimal(1.7) * dSisaTenor) + 1) * dPlafon) / (dSisaTenor * 12);
                        }
                    }
                    if (Convert.ToDecimal(e.NewValues["JenisPembiayaan"]) == 2)
                    {
                        dAngsuran = (dBakiDebet * Convert.ToDecimal(0.1));
                    }
                    if (Convert.ToDecimal(e.NewValues["JenisPembiayaan"]) == 3)
                    {
                        dAngsuran = (dBakiDebet * dBunga / 12);
                    }
                }


                int x = Convert.ToInt32(Math.Round(dAngsuran, 0));
                int result = x % 1000 >= 100 ? x + 1000 - x % 1000 : x - x % 1000;

                e.NewValues["Angsuran"] = result;
                myDetailTable.Rows.Add(myUpdateSLIKEntity.UpdateSLIKcommand.DtlKeyUniqueKey(), myUpdateSLIKEntity.DocKey, seq, e.NewValues["JenisPembiayaan"], e.NewValues["PerusahaanPembiayaan"], e.NewValues["AtasNama"], e.NewValues["Plafon"], e.NewValues["BakiDebet"], e.NewValues["Bunga"], e.NewValues["TglAkadAwal"], e.NewValues["TglAwalSisaTenor"], e.NewValues["TglJatuhTempo"], e.NewValues["Angsuran"], e.NewValues["Kolektibilitas"], e.NewValues["HistoryKolek"], e.NewValues["AktualOverDue"]);

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["JenisPembiayaan"] == null) throw new Exception("Column 'Jenis Pembiayaan' is mandatory.");
            if (e.NewValues["PerusahaanPembiayaan"] == null) throw new Exception("Column 'Perusahaan Pembiayaan / Bank' is mandatory.");
            if (e.NewValues["AtasNama"] == null) throw new Exception("Column 'Atas Nama' is mandatory.");
            if (e.NewValues["Plafon"] == null) throw new Exception("Column 'Plafon' is mandatory.");
            if (e.NewValues["BakiDebet"] == null) throw new Exception("Column 'Baki Debet' is mandatory.");
            if (e.NewValues["Bunga"] == null) throw new Exception("Column 'Bunga' is mandatory.");
            if (e.NewValues["TglAkadAwal"] == null) throw new Exception("Column 'Akad Awal' is mandatory.");
            if (e.NewValues["TglAwalSisaTenor"] == null) throw new Exception("Column 'Awal Sisa tenor' is mandatory.");
            if (e.NewValues["TglJatuhTempo"] == null) throw new Exception("Column 'Jatuh Tempo' is mandatory.");
            if (e.NewValues["Kolektibilitas"] == null) throw new Exception("Column 'Kolektibilitas' is mandatory.");
            if (e.NewValues["HistoryKolek"] == null) throw new Exception("Column 'History Kolek' is mandatory.");
            if (e.NewValues["AktualOverDue"] == null) throw new Exception("Column 'Aktual Overdue' is mandatory.");
            if (StrErrorMsg == "")
            {
                gvDetail.JSProperties["cpCmd"] = "UPDATE";
                int editingRowVisibleIndex = gvDetail.EditingRowVisibleIndex;
                int id = (int)gvDetail.GetRowValues(editingRowVisibleIndex, "DtlKey");

                decimal dAngsuran = 0;
                decimal dJangka = 0, dSisaTenor = 0;
                decimal dPlafon = 0, dBakiDebet = 0, dBunga = 0;

                DateTime a = Convert.ToDateTime(e.NewValues["TglJatuhTempo"]);
                DateTime b = Convert.ToDateTime(e.NewValues["TglAkadAwal"]);
                DateTime c = Convert.ToDateTime(e.NewValues["TglAwalSisaTenor"]);

                dPlafon = Convert.ToDecimal(e.NewValues["Plafon"]);
                dBakiDebet = Convert.ToDecimal(e.NewValues["BakiDebet"]);
                dBunga = Convert.ToDecimal(e.NewValues["Bunga"]) / 100;
                dJangka = Convert.ToDecimal((a - b).TotalDays) / 365;
                dSisaTenor = Convert.ToDecimal((a - c).TotalDays) / 365;

                if(dBunga != 0 || dBakiDebet != 0)
                {
                    if (Convert.ToDecimal(e.NewValues["JenisPembiayaan"]) == 1)
                    {
                        if (dPlafon > dBakiDebet)
                        {
                            dAngsuran = (((dBunga / Convert.ToDecimal(1.7) * dJangka) + 1) * dPlafon) / (dJangka * 12);
                        }
                        else
                        {
                            dAngsuran = (((dBunga / Convert.ToDecimal(1.7) * dSisaTenor) + 1) * dPlafon) / (dSisaTenor * 12);
                        }
                    }
                    if (Convert.ToDecimal(e.NewValues["JenisPembiayaan"]) == 2)
                    {
                        dAngsuran = (dBakiDebet * Convert.ToDecimal(0.1));
                    }
                    if (Convert.ToDecimal(e.NewValues["JenisPembiayaan"]) == 3)
                    {
                        dAngsuran = (dBakiDebet * dBunga / 12);
                    }
                }

                

                int x = Convert.ToInt32(Math.Round(dAngsuran, 0));
                int result = x % 1000 >= 100 ? x + 1000 - x % 1000 : x - x % 1000;

                e.NewValues["Angsuran"] = result;

                DataRow dr = myDetailTable.Rows.Find(id);
                dr["JenisPembiayaan"] = e.NewValues["JenisPembiayaan"];
                dr["PerusahaanPembiayaan"] = e.NewValues["PerusahaanPembiayaan"];
                dr["AtasNama"] = e.NewValues["AtasNama"];
                dr["Plafon"] = e.NewValues["Plafon"];
                dr["BakiDebet"] = e.NewValues["BakiDebet"];
                dr["Bunga"] = e.NewValues["Bunga"];
                dr["TglAkadAwal"] = e.NewValues["TglAkadAwal"];
                dr["TglAwalSisaTenor"] = e.NewValues["TglAwalSisaTenor"];
                dr["TglJatuhTempo"] = e.NewValues["TglJatuhTempo"];
                dr["Angsuran"] = result;
                dr["Kolektibilitas"] = e.NewValues["Kolektibilitas"];
                dr["HistoryKolek"] = e.NewValues["HistoryKolek"];
                dr["AktualOverDue"] = e.NewValues["AktualOverDue"];

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            gvDetail.JSProperties["cpCmd"] = "DELETE";
            int id = (int)e.Keys["DtlKey"];
            DataRow dr = myDetailTable.Rows.Find(id);

            myDetailTable.Rows.Remove(dr);

            ASPxGridView grid = sender as ASPxGridView;
            grid.CancelEdit();
            e.Cancel = true;
        }
        protected void gvDetail_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myds.Tables[1];
        }
        protected void gvDetail_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string urlsave = "";
            urlsave = "~/Transactions/CreditProcess/UpdateSLIK/UpdateSLIKMaint.aspx";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlConnection SQLConn = new SqlConnection(dbsetting.ConnectionString);
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                case "ON_APPNO_VALIDATION":
                    if (myAction != DXSSAction.New)
                        return;
                    cplMain.JSProperties["cpStrErrorMsg"] = "";
                    if (luAppNo.Text.Length > 0)
                    {
                        object obj = myLocalDBSetting.ExecuteScalar("SELECT COUNT(DocNo) FROM [dbo].[UpdateSLIK] WHERE RefNo=?", luAppNo.Value);
                        if (obj != null && obj != DBNull.Value)
                        {
                            if (Convert.ToInt32(obj) > 0)
                            {
                                cplMain.JSProperties["cpStrErrorMsg"] = "No App : " + luAppNo.Value.ToString() + " ini sudah terdaftar.";
                            }
                        }
                    }
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
            }
        }

        protected void gvDetail_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID != "ctmbtnView") return;
            gvDetail.StartEdit(e.VisibleIndex);
        }

        protected DataTable LoadClienttable(object strAppNo)
        {
            DataTable mytable = new DataTable();
            SqlConnection myconn = new SqlConnection(dbsetting.ConnectionString);
            SqlQuery = @"select b.CLIENT, b.NAME NAMA_CUST, 
              case b.INGENDER
                     when '1' then 'Pria'
                     else 'Wanita'
              end GENDER, b.INKTP NO_KTP, b.NPWP,b.INBORNPLC KOTA_LAHIR, b.INBORNDT TGLLAHIR, IBUKANDUNG, b.ADDRESS1 ALAMAT_KTP, b.KOTA, b.INMAILTELP MOBILEPHONE,
              b.INCOMPANY NAMA_PERUSAHAAN,
              case b.INJOB
                     when '1' then 'Karyawan Non MNC Group'
                     when '9' then 'Karyawan MNC Group'
                     when '2' then 'Pengusaha'
                     when '3' then 'Profesional'
                     when '4' then 'Others'
              end PEKERJAAN, b.INSALARY GAJI, b.INPERYEAR LAMA_TAHUN_KERJA,
              b.INSPOUNAME NAMA_SPOUSE, b.INSPOUPLC KOTA_LAHIR_SPOUSE, b.INSPOUBRDT TGL_LAHIR_ISTRI, b.INSPOUADD1 ALAMAT_KTP_ISTRI, b.INSPOU_KOTA KOTA_KTP_ISTRI, b.INSPOUTELP HP_SPOUSE,
              b.INEMERNAME Nama_Contact, b.INEMERTELP Mobilephone_contact, 
              case b.INEMERJAB
                     when '1' then 'Orangtua/anak'
                     when '2' then 'Saudara Kandung'
                     when '3' then 'Sepupu'
                     when '4' then 'Paman/Bibi'
                     when '5' then 'Tidak ada Hub Keluarga'
              end Hubungan, b.INJAMIN, b.INJAMADD1, b.INJAMKTP, b.INJAMTELP, 
              case b.INJAMSTAT
                     when '1' then 'Rujukan'
                     when '2' then 'Penjamin'
                     else '-'
              end INJAMSTAT,
              b.INJAMHUB
            from LS_APPLICATION a
            inner join SYS_CLIENT b on a.LESSEE = b.CLIENT
            where APPLICNO = @APPLICNO";
            using (SqlCommand cmdclientdata = new SqlCommand(SqlQuery, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdclientdata);
                cmdclientdata.CommandType = CommandType.Text;
                cmdclientdata.Parameters.Add("@APPLICNO", SqlDbType.NVarChar);
                cmdclientdata.Parameters["@APPLICNO"].Value = strAppNo;
                adapter.Fill(mytable);
            }
            return mytable;
        }
        protected DataTable LoadUploadDoctable(object strAppNo)
        {
            DataTable mytable = new DataTable();
            SqlConnection myconn = new SqlConnection(localdbsetting.ConnectionString);
            SqlQuery = @"SELECT 
                            [ID],[Name],[Type],
                            [Ext],[Remarks],[AppNo],
                            [CreatedBy],[CreatedDateTime],[DebiturName],
                            [AgreeNo],[Module] 
                        FROM [dbo].[DocumentFile] 
                        WHERE [AppNo] = @AppNo
                        ORDER BY CreatedDateTime DESC";
            using (SqlCommand cmdclientdata = new SqlCommand(SqlQuery, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdclientdata);
                cmdclientdata.CommandType = CommandType.Text;
                cmdclientdata.Parameters.Add("@AppNo", SqlDbType.NVarChar);
                cmdclientdata.Parameters["@AppNo"].Value = strAppNo;
                adapter.Fill(mytable);
            }
            return mytable;
        }
        protected void vcDetail_CustomCallback(object sender, ASPxVerticalGridCustomCallbackEventArgs e)
        {
            isValidLogin();
            myClientTable = LoadClienttable(luAppNo.Value);
            vcDetail.DataSource = myClientTable;
            vcDetail.DataBind();
        }
        protected void vcDetail_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxVerticalGrid).DataSource = myClientTable;
        }

        protected void gvUploadDoc_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            isValidLogin();
            myUploadDocTable = LoadUploadDoctable(luAppNo.Value);
            gvUploadDoc.DataSource = myUploadDocTable;
            gvUploadDoc.DataBind();
        }
        protected void gvUploadDoc_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myUploadDocTable;
        }
        protected void gvUploadDoc_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "GridbtnDownload")
            {
                try
                {
                    object obj = gvUploadDoc.GetRowValues(e.VisibleIndex, gvUploadDoc.KeyFieldName);
                    if (obj != null && obj != DBNull.Value)
                    {
                        FileDocID = Convert.ToInt32(obj);
                    }
                    ASPxWebControl.RedirectOnCallback(string.Format("UpdateSLIKEntry.aspx?ID=" + FileDocID.ToString()));
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }
    }
}