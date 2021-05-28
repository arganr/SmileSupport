using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Insurance
{
    public partial class PolisAsuransiFAEntry : BasePage
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
        protected PolisAsuransiFADB myPolisAsuransiFADB
        {
            get { isValidLogin(false); return (PolisAsuransiFADB)HttpContext.Current.Session["myPolisAsuransiFADB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myPolisAsuransiFADB" + this.ViewState["_PageID"]] = value; }
        }
        protected PolisAsuransiFAEntity myPolisAsuransiFAEntity
        {
            get { isValidLogin(false); return (PolisAsuransiFAEntity)HttpContext.Current.Session["myPolisAsuransiFAEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myPolisAsuransiFAEntity" + this.ViewState["_PageID"]] = value; }
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
                    this.myPolisAsuransiFADB = PolisAsuransiFADB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                    myPolisAsuransiFAEntity = this.myPolisAsuransiFADB.View(Convert.ToInt32(this.Request.QueryString["SourceKey"]));
                }
                myds = new DataSet();
                this.myPolisAsuransiFADB = PolisAsuransiFADB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((PolisAsuransiFAEntity)HttpContext.Current.Session["myPolisAsuransiFAEntity" + strKey]);
            }
            if (!IsCallback)
            {

            }
        }
        private void SetApplication(PolisAsuransiFAEntity PolisAsuransiFAEntity)
        {
            if (this.myPolisAsuransiFAEntity != PolisAsuransiFAEntity)
            {
                if (PolisAsuransiFAEntity != null)
                {
                    this.myPolisAsuransiFAEntity = PolisAsuransiFAEntity;
                }
                myAction = this.myPolisAsuransiFAEntity.Action;
                myDocType = this.myPolisAsuransiFAEntity.DocType;
                myds = myPolisAsuransiFAEntity.myDataSet;
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

            if (myAction == DXSSAction.New)
            {
                deDocDate.Value = myDBSetting.GetServerTime();
                gvDetail.Columns["colNo"].Visible = false;
                gvDetail.Columns["ClmnCommand2"].Visible = false;
            }
            else if (myAction == DXSSAction.View)
            {
                luAssetNo.ReadOnly = true;
                luAssetNo.BackColor = System.Drawing.Color.Transparent;

                btnSave.Visible = false;
                gvDetail.Columns["colNo"].Visible = true;
                gvDetail.Columns["ClmnCommand"].Visible = false;
                gvDetail.Columns["ClmnCommand2"].Visible = false;
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupPAFAEntry").Caption = "View Polis Asuransi Fixed Asset";
            }
            else if (myAction == DXSSAction.Edit)
            {
                luAssetNo.ReadOnly = true;
                luAssetNo.BackColor = System.Drawing.Color.Transparent;

                gvDetail.Columns["colNo"].Visible = false;
                gvDetail.Columns["ClmnCommand2"].Visible = false;
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupPAFAEntry").Caption = "Edit Polis Asuransi Fixed Asset";
            }
        }
        private void BindingMaster()
        {
            luAssetNo.Value = myPolisAsuransiFAEntity.DocNo;
            deDocDate.Value = myPolisAsuransiFAEntity.DocDate;
            txtPlat.Value = myPolisAsuransiFAEntity.NoPolisi;
            txtAssetDesc.Value = myPolisAsuransiFAEntity.AssetDesc;
            txtNoRangka.Value = myPolisAsuransiFAEntity.NoRangka;
            txtNoMesin.Value = myPolisAsuransiFAEntity.NoMesin;
        }

        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;

            myPolisAsuransiFAEntity.DocNo = luAssetNo.Value;
            myPolisAsuransiFAEntity.DocDate = deDocDate.Value;
            myPolisAsuransiFAEntity.AssetDesc = txtAssetDesc.Value;
            myPolisAsuransiFAEntity.NoPolisi = txtPlat.Value;
            myPolisAsuransiFAEntity.NoRangka = txtNoRangka.Value;
            myPolisAsuransiFAEntity.NoMesin = txtNoMesin.Value;

            if (myAction == DXSSAction.New)
            {
                myPolisAsuransiFAEntity.CreatedBy = UserID;
                myPolisAsuransiFAEntity.CreatedDateTime = myLocalDBSetting.GetServerTime();
            }
            myPolisAsuransiFAEntity.Save(this.UserID, this.UserName, SaveAction.Save);
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
            string urlsave = "";
            urlsave = "~/Transactions/Insurance/PolisAsuransiFAMaint.aspx";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            SqlDBSetting dbSetting = this.myDBSetting;
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                case "ON_ASSETNO_VALIDATION":
                    if (myAction != DXSSAction.New)
                        return;
                    cplMain.JSProperties["cpStrErrorMsg"] = "";
                    if (luAssetNo.Text.Length > 0)
                    {
                        object obj = myLocalDBSetting.ExecuteScalar("SELECT COUNT(DocNo) FROM [dbo].[PolisAsuransiFA] WHERE DocNo=?", luAssetNo.Value);
                        if (obj != null && obj != DBNull.Value)
                        {
                            if (Convert.ToInt32(obj) > 0)
                            {
                                cplMain.JSProperties["cpStrErrorMsg"] = "No Asset : " + luAssetNo.Value.ToString() + " ini sudah terdaftar.";
                            }
                        }
                    }
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
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
            }
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
            if (e.NewValues["Maskapai"] == null) throw new Exception("Column 'Maskapai' is mandatory.");
            if (e.NewValues["NoPolis"] == null) throw new Exception("Column 'No Polis' is mandatory.");
            if (e.NewValues["StartDate"] == null) throw new Exception("Column 'Start Date' is mandatory.");
            if (e.NewValues["EndDate"] == null) throw new Exception("Column 'End Date' is mandatory.");
            if (e.NewValues["Coverage"] == null) throw new Exception("Column 'Coverage' is mandatory.");
            if (StrErrorMsg == "")
            {
                gvDetail.JSProperties["cpCmd"] = "INSERT";

                DataRow[] ValidLinesRows = myDetailTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
                int seq = SeqUtils.GetLastSeq(ValidLinesRows);

                myDetailTable.Rows.Add(myPolisAsuransiFAEntity.PolisAsuransiFAcommand.DtlKeyUniqueKey(), myPolisAsuransiFAEntity.DocKey, seq, e.NewValues["Maskapai"], e.NewValues["NoPolis"], e.NewValues["StartDate"], e.NewValues["EndDate"], e.NewValues["Coverage"]);

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["Maskapai"] == null) throw new Exception("Column 'Maskapai' is mandatory.");
            if (e.NewValues["NoPolis"] == null) throw new Exception("Column 'No Polis' is mandatory.");
            if (e.NewValues["StartDate"] == null) throw new Exception("Column 'Start Date' is mandatory.");
            if (e.NewValues["EndDate"] == null) throw new Exception("Column 'End Date' is mandatory.");
            if (e.NewValues["Coverage"] == null) throw new Exception("Column 'Coverage' is mandatory.");
            if (StrErrorMsg == "")
            {
                gvDetail.JSProperties["cpCmd"] = "UPDATE";
                int editingRowVisibleIndex = gvDetail.EditingRowVisibleIndex;
                int id = (int)gvDetail.GetRowValues(editingRowVisibleIndex, "DtlKey");

                DataRow dr = myDetailTable.Rows.Find(id);
                dr["Maskapai"] = e.NewValues["Maskapai"];
                dr["NoPolis"] = e.NewValues["NoPolis"];
                dr["StartDate"] = e.NewValues["StartDate"];
                dr["EndDate"] = e.NewValues["EndDate"];
                dr["Coverage"] = e.NewValues["Coverage"];

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
        protected void gvDetail_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID != "ctmbtnView") return;
            gvDetail.StartEdit(e.VisibleIndex);
        }
        protected void gvDetail_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            if (e.Column.FieldName == "Maskapai")
            {
                isValidLogin(false);
                if (Page.IsCallback)
                {
                    DataTable myitem = new DataTable();
                    myitem = myPolisAsuransiFADB.LoadMaskapai();
                    ASPxComboBox cmb = (ASPxComboBox)e.Editor;
                    cmb.DataSource = myitem;
                    cmb.DataBindItems();
                }
            }
            if (e.Column.FieldName == "Coverage")
            {
                isValidLogin(false);
                if (Page.IsCallback)
                {
                    DataTable myitem = new DataTable();
                    myitem = myPolisAsuransiFADB.LoadCoverage();
                    ASPxComboBox cmb = (ASPxComboBox)e.Editor;
                    cmb.DataSource = myitem;
                    cmb.DataBindItems();
                }
            }
        }
        protected void gvDetail_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
        }
        protected void gvDetail_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == ColumnCommandButtonType.Delete) { e.Visible = false; }
            if (e.ButtonType == ColumnCommandButtonType.Edit) { e.Enabled = false; }
            if (e.VisibleIndex == gvDetail.VisibleRowCount - 1) { if (e.ButtonType == ColumnCommandButtonType.Edit){ e.Enabled = false; } }
        }
    }
}