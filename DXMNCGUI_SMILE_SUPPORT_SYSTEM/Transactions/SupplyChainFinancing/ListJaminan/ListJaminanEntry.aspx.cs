using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.SupplyChainFinancing.ListJaminan
{
    public partial class ListJaminanEntry : BasePage
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
        protected ListJaminanDB myListJaminanDB
        {
            get { isValidLogin(false); return (ListJaminanDB)HttpContext.Current.Session["myListJaminanDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myListJaminanDB" + this.ViewState["_PageID"]] = value; }
        }
        protected ListJaminanEntity myListJaminanEntity
        {
            get { isValidLogin(false); return (ListJaminanEntity)HttpContext.Current.Session["myListJaminanEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myListJaminanEntity" + this.ViewState["_PageID"]] = value; }
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
        protected DataSet myds
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myds" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myds" + this.ViewState["_PageID"]] = value; }
        }
        protected string strKey
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]] = value; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
                if (this.Request.QueryString["DocKey"] != null && this.Request.QueryString["Type"] != null)
                {
                    this.myListJaminanDB = ListJaminanDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                    myListJaminanEntity = this.myListJaminanDB.View(Convert.ToInt32(this.Request.QueryString["DocKey"]));
                }
                myHeaderTable = new DataTable();
                myDetailTable = new DataTable();
                myds = new DataSet();

                this.myListJaminanDB = ListJaminanDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((ListJaminanEntity)HttpContext.Current.Session["myListJaminanEntity" + strKey]);
            }
            if (!IsCallback)
            {

            }
        }

        private void SetApplication(ListJaminanEntity ListJaminanEntity)
        {
            if (this.myListJaminanEntity != ListJaminanEntity)
            {
                if (ListJaminanEntity != null)
                {
                    this.myListJaminanEntity = ListJaminanEntity;
                }
                myAction = this.myListJaminanEntity.Action;
                myds = myListJaminanEntity.myDataSet;
                myHeaderTable = myds.Tables[0];
                myDetailTable = myds.Tables[1];
                myds.Tables[1].DefaultView.Sort = "DtlKey";
                gvDetail.DataSource = myDetailTable;
                gvDetail.DataBind();
                setuplookupedit();
                BindingMaster();
                Accessable();
            }
        }
        private void setuplookupedit()
        {
        }
        private void Accessable()
        {
            bool bReadOnly = true;
            bool bEnable = true;
            bool bSubmit = false;
            DateTime mydate = myLocalDBSetting.GetServerTime();

            txtDocNo.ReadOnly = true;
            deDocDate.ReadOnly = true;

            txtDocNo.ClientEnabled = false;
            txtDocNo.ForeColor = System.Drawing.Color.Black;
            txtDocNo.BackColor = System.Drawing.Color.Transparent;

            deDocDate.ClientEnabled = false;
            deDocDate.DropDownButton.Visible = false;
            deDocDate.ForeColor = System.Drawing.Color.Black;
            deDocDate.BackColor = System.Drawing.Color.Transparent;

            txtDebitur.ClientEnabled = false;
            txtDebitur.ForeColor = System.Drawing.Color.Black;
            txtDebitur.BackColor = System.Drawing.Color.Transparent;

            if (chkSubmit.CheckState == CheckState.Checked)
            {
                myAction = DXSSAction.View;
            }

            if (myAction == DXSSAction.New)
            {
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupListJaminanEntry").Caption = "List Jaminan Entry";

                chkSubmit.ClientEnabled = false;

                gvDetail.Columns["colNo"].Visible = false;

                bReadOnly = false;
                bEnable = true;
                bSubmit = false;
            }
            else if (myAction == DXSSAction.View)
            {
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupListJaminanEntry").Caption = "View List Jaminan";
                lblHeader.Text = "View List Jaminan";

                chkSubmit.ClientEnabled = false;

                gvDetail.Columns["colNo"].Visible = true;
                gvDetail.Columns["ClmnCommand"].Visible = false;

                bReadOnly = true;
                bEnable = false;
                bSubmit = false;

                luAppNo.ClientEnabled = false;
                luAppNo.ForeColor = System.Drawing.Color.Black;
                luAppNo.BackColor = System.Drawing.Color.Transparent;

                mmAssetDesc.ClientEnabled = false;
                mmAssetDesc.ForeColor = System.Drawing.Color.Black;
                mmAssetDesc.BackColor = System.Drawing.Color.Transparent;

                seTotalJaminan.ClientEnabled = false;
                seTotalJaminan.ForeColor = System.Drawing.Color.Black;
                seTotalJaminan.BackColor = System.Drawing.Color.Transparent;

                seTotalPembiayaan.ClientEnabled = false;
                seTotalPembiayaan.ForeColor = System.Drawing.Color.Black;
                seTotalPembiayaan.BackColor = System.Drawing.Color.Transparent;

                btnSave.ClientEnabled = false;
            }
            else if (myAction == DXSSAction.Edit)
            {
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupListJaminanEntry").Caption = "Edit List Jaminan";
                lblHeader.Text = "Edit List Jaminan";

                gvDetail.Columns["colNo"].Visible = false;

                bReadOnly = false;
                bEnable = true;
                bSubmit = true;
            }           
            seTotalJaminan.ReadOnly = true;
        }
        private void BindingMaster()
        {
            chkSubmit.CheckState = (myListJaminanEntity.IsPost.ToString() == "T" ? CheckState.Checked : CheckState.Unchecked);
            txtDocNo.Value = myListJaminanEntity.DocNo;
            deDocDate.Value = myListJaminanEntity.DocDate;
            luAppNo.Value = myListJaminanEntity.RefNo;
            txtDebitur.Value = myListJaminanEntity.Debitur;
            mmAssetDesc.Value = myListJaminanEntity.AssetDesc;
            seTotalJaminan.Value = Convert.ToDecimal(myListJaminanEntity.TotalJaminan);
            seTotalPembiayaan.Value = Convert.ToDecimal(myListJaminanEntity.TotalPembiayaan);
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            bool focusF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            if (myDetailTable.Rows.Count == 0)
            {
                errorF = true;
                if (!focusF)
                {
                    gvDetail.Focus();
                    focusF = true;
                    strmessageError = "Please add detail asset, empty asset is not allowed.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (Convert.ToDecimal(seTotalPembiayaan.Value) == 0)
            {
                errorF = true;
                if (!focusF)
                {
                    seTotalPembiayaan.Focus();
                    focusF = true;
                    strmessageError = "Total Pembiayaan value 0 is not allowed.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (Convert.ToDecimal(seTotalPembiayaan.Value) > 0)
            {
                decimal dmaxvalue = 0;
                decimal dTotalJaminan = 0;
                decimal dTotalPembiayaan = 0;

                dTotalJaminan = Convert.ToDecimal(seTotalJaminan.Value);
                dTotalPembiayaan = Convert.ToDecimal(seTotalPembiayaan.Value);
                dmaxvalue = dTotalJaminan * 75 / 100;

                if (dTotalPembiayaan > dmaxvalue)
                {
                    errorF = true;
                    if (!focusF)
                    {
                        seTotalPembiayaan.Focus();
                        focusF = true;
                        strmessageError = "Maksimal total pembiayaan adalah 75% dari total jaminan atau maksimal Rp." + dmaxvalue;
                        cplMain.JSProperties["cplActiveTabIndex"] = 1;
                    }
                }
            }
            return errorF;
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;

            gvDetail.UpdateEdit();
            myListJaminanEntity.DocNo = txtDocNo.Value;
            myListJaminanEntity.DocDate = deDocDate.Value;
            myListJaminanEntity.RefNo = luAppNo.Value;
            myListJaminanEntity.Debitur = txtDebitur.Value;
            myListJaminanEntity.AssetDesc = mmAssetDesc.Value;
            myListJaminanEntity.TotalJaminan = seTotalJaminan.Value;
            myListJaminanEntity.TotalPembiayaan = seTotalPembiayaan.Value;
            if (myAction == DXSSAction.New)
            {
                myListJaminanEntity.CreatedBy = UserID;
                myListJaminanEntity.CreatedDateTime = myLocalDBSetting.GetServerTime();
                myListJaminanEntity.LastModifiedTime = myLocalDBSetting.GetServerTime();
            }
            myListJaminanEntity.IsPost = (chkSubmit.CheckState == CheckState.Checked ? "T" : "F");
            myListJaminanEntity.Save(UserID, UserName, saveAction);
            return bSave;
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string urlsave = "";
            urlsave = "~/Transactions/SupplyChainFinancing/ListJaminan/ListJaminanMaint.aspx";
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("DocKey", myListJaminanEntity.DocKey.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlConnection SQLConn = new SqlConnection(dbsetting.ConnectionString);
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                case "ON_APPNO_VALIDATION":
                    break;
                case "SAVE":
                    Save(SaveAction.Save);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "SAVECONFIRM":
                    #region SAVE ACTION
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to save this data?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    #endregion
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
            if (e.NewValues["ItemDesc"] == null) throw new Exception("Column 'Item Description' is mandatory.");
            if (e.NewValues["ItemCategory"] == null) throw new Exception("Column 'Category' is mandatory.");
            if (e.NewValues["ItemBrand"] == null) throw new Exception("Column 'Brand' is mandatory.");
            if (e.NewValues["UOM"] == null) throw new Exception("Column 'UOM' is mandatory.");
            if (e.NewValues["Qty"] == null) throw new Exception("Column 'Qty' is mandatory.");
            if (e.NewValues["DBP"] == null) throw new Exception("Column 'DBP' is mandatory.");
            if (e.NewValues["RBP"] == null) throw new Exception("Column 'RBP' is mandatory.");
            if (StrErrorMsg == "")
            {
                gvDetail.JSProperties["cpCmd"] = "INSERT";

                DataRow[] ValidLinesRows = myDetailTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
                int seq = SeqUtils.GetLastSeq(ValidLinesRows);

                myDetailTable.Rows.Add(myListJaminanEntity.ListJaminancommand.DetailUniqueKey(), myListJaminanEntity.DocKey, seq, DBNull.Value, e.NewValues["ItemDesc"], e.NewValues["ItemCategory"], e.NewValues["ItemBrand"], e.NewValues["UOM"], e.NewValues["Qty"], e.NewValues["DBP"], e.NewValues["RBP"], e.NewValues["DBPSubTotal"]);

                decimal vNetAmountTotal = decimal.Parse(seTotalJaminan.Value.ToString());
                vNetAmountTotal += decimal.Parse(e.NewValues["DBPSubTotal"].ToString());
                gvDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["ItemDesc"] == null) throw new Exception("Column 'Item Description' is mandatory.");
            if (e.NewValues["ItemCategory"] == null) throw new Exception("Column 'Category' is mandatory.");
            if (e.NewValues["ItemBrand"] == null) throw new Exception("Column 'Brand' is mandatory.");
            if (e.NewValues["UOM"] == null) throw new Exception("Column 'UOM' is mandatory.");
            if (e.NewValues["Qty"] == null) throw new Exception("Column 'Qty' is mandatory.");
            if (e.NewValues["DBP"] == null) throw new Exception("Column 'DBP' is mandatory.");
            if (e.NewValues["RBP"] == null) throw new Exception("Column 'RBP' is mandatory.");
            if (StrErrorMsg == "")
            {
                gvDetail.JSProperties["cpCmd"] = "UPDATE";
                int editingRowVisibleIndex = gvDetail.EditingRowVisibleIndex;
                int id = (int)gvDetail.GetRowValues(editingRowVisibleIndex, "DtlKey");
                DataRow dr = myDetailTable.Rows.Find(id);
                dr["ItemDesc"] = e.NewValues["ItemDesc"];
                dr["ItemCategory"] = e.NewValues["ItemCategory"];
                dr["ItemBrand"] = e.NewValues["ItemBrand"];
                dr["UOM"] = e.NewValues["UOM"];
                dr["Qty"] = e.NewValues["Qty"];
                dr["DBP"] = e.NewValues["DBP"];
                dr["RBP"] = e.NewValues["RBP"];
                dr["DBPSubTotal"] = e.NewValues["DBPSubTotal"];

                decimal vNetAmountTotal = decimal.Parse(seTotalJaminan.Value.ToString());
                vNetAmountTotal -= decimal.Parse(e.OldValues["DBPSubTotal"].ToString());
                vNetAmountTotal += decimal.Parse(e.NewValues["DBPSubTotal"].ToString());
                gvDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

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

            decimal vNetAmountTotal = decimal.Parse(seTotalJaminan.Value.ToString());
            vNetAmountTotal -= decimal.Parse(dr["DBPSubTotal"].ToString());
            gvDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

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
            if (e.Column.Caption == "No.")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }
        protected void gvDetail_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Qty" || e.Column.FieldName == "DBP")
            {
                (e.Editor as ASPxTextBox).AutoPostBack = false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/SupplyChainFinancing/ListJaminan/ListJaminanMaint.aspx");
        }

        protected void Upload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {

        }
    }
}