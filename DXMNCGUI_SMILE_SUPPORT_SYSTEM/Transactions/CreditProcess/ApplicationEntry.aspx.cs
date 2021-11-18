using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess
{
    public partial class ApplicationEntry : BasePage
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
        protected ApplicationDB myApplicationDB
        {
            get { isValidLogin(false); return (ApplicationDB)HttpContext.Current.Session["myApplicationDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApplicationDB" + this.ViewState["_PageID"]] = value; }
        }
        protected ApplicationEntity myApplicationEntity
        {
            get { isValidLogin(false); return (ApplicationEntity)HttpContext.Current.Session["myApplicationEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApplicationEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected int igridindex
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["Applicationigridindex" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["Applicationigridindex" + this.ViewState["_PageID"]] = value; }
        }
        protected int iLineIndex
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["ApplicationiLineIndex" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["ApplicationiLineIndex" + this.ViewState["_PageID"]] = value; }
        }
        protected int iLineID
        {
            get { isValidLogin(false); return (Int32)HttpContext.Current.Session["ApplicationiLINE_ID" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["ApplicationiLINE_ID" + this.ViewState["_PageID"]] = value; }
        }
        protected string strDocName
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["ApplicationstrDocName" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["ApplicationstrDocName" + this.ViewState["_PageID"]] = value; }
        }
        protected string strKey
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]] = value; }
        }
        protected string myStatus
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["myStatus" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myStatus" + this.ViewState["_PageID"]] = value; }
        }
        protected string sFieldNameLines
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["ApplicationFieldNameLines" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["ApplicationFieldNameLines" + this.ViewState["_PageID"]] = value; }
        }
        protected DataSet myds
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myds" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myds" + this.ViewState["_PageID"]] = value; }
        }
        protected DataRow myItemRow
        {
            get { isValidLogin(false); return (DataRow)HttpContext.Current.Session["myItemRow" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myItemRow" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDebiturTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDebiturTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDebiturTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable mySupplierTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mySupplierTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mySupplierTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myBrancTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myBrancTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myBrancTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myApproveAccesstable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApproveAccesstable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApproveAccesstable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myCommentTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myCommentTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myCommentTable" + this.ViewState["_PageID"]] = value; }
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
        protected DataTable myDocNoFormatTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDocNoFormatTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDocNoFormatTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable mySectionTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mySectionTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mySectionTable" + this.ViewState["_PageID"]] = value; }
        }
        protected IContainer components
        {
            get { isValidLogin(false); return (IContainer)HttpContext.Current.Session["components" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["components" + this.ViewState["_PageID"]] = value; }
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

        public DataTable LoadDebitur()
        {
            string strQuery = @"SELECT CLIENT AS DEBITUR_CODE, [NAME] AS DEBITUR_NAME FROM SYS_CLIENT ORDER BY NAME";
            myDebiturTable = myDBSetting.GetDataTable(strQuery, false, "");
            return myDebiturTable;
        }
        public DataTable LoadSupplier()
        {
            string strQuery = @"SELECT SUPP_CODE, SUPP_NAME FROM SYS_TBLSUPPLIER WHERE IS_ACTIVE=1 ORDER BY SUPP_NAME";
            mySupplierTable = myDBSetting.GetDataTable(strQuery, false, "");
            return mySupplierTable;
        }
        public DataTable LoadBranch(string sID)
        {
            string strQuery = @"SELECT DISTINCT a.USER_ID, a.USER_NAME, b.C_CODE, c.C_NAME
                                FROM MASTER_USER a
                                INNER JOIN MASTER_USER_COMPANY b on b.USER_ID = a.USER_ID
                                INNER JOIN SYS_COMPANY c on c.C_CODE = b.C_CODE
                                WHERE a.USER_ID=?
                                ORDER BY a.USER_ID, b.C_CODE";
            myBrancTable = myDBSetting.GetDataTable(strQuery, false, sID);
            return myBrancTable;
        }
        public DataTable LoadApproveAccess(string sID)
        {
            string strQuery = @"SELECT DISTINCT B.GROUP_CODE FROM MASTER_USER A 
                                INNER JOIN MASTER_USER_COMPANY_GROUP B ON B.USER_ID=A.USER_ID 
                                INNER JOIN MASTER_GROUP C ON C.USERGROUP=B.GROUP_CODE 
                                WHERE A.IS_ACTIVE_FLAG=1 AND A.USER_ID=?";
            myApproveAccesstable = myDBSetting.GetDataTable(strQuery, false, sID);
            return myApproveAccesstable;
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
                    this.myApplicationDB = ApplicationDB.Create(myLocalDBSetting, myDBSession);
                    myApplicationEntity = this.myApplicationDB.View(Convert.ToInt32(this.Request.QueryString["SourceKey"]));
                }
                myHeaderTable = new DataTable();
                myDetailTable = new DataTable();
                myDocNoFormatTable = new DataTable();
                mySectionTable = new DataTable();
                myDebiturTable = new DataTable();
                mySupplierTable = new DataTable();
                myBrancTable = new DataTable();
                myCommentTable = new DataTable();
                strDocName = "";
                iLineIndex = -1;
                igridindex = -1;
                iLineID = -1;
                myds = new DataSet();
                this.myApplicationDB = ApplicationDB.Create(myLocalDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((ApplicationEntity)HttpContext.Current.Session["myApplicationEntity" + strKey]);
            }
            if (!IsCallback)
            {

            }
        }
        private void SetApplication(ApplicationEntity ApplicationEntity)
        {
            if (this.myApplicationEntity != ApplicationEntity)
            {
                if (ApplicationEntity != null)
                {
                    this.myApplicationEntity = ApplicationEntity;
                }
                myAction = this.myApplicationEntity.Action;
                myDocType = this.myApplicationEntity.DocType;
                myds = myApplicationEntity.myDataSet;
                myStatus = this.myApplicationEntity.Status.ToString();
                myHeaderTable = myds.Tables[0];
                myDetailTable = myds.Tables[1];
                myds.Tables[1].DefaultView.Sort = "DtlKey";
                gvDetail.DataSource = myDetailTable;
                gvDetail.DataBind();
                myCommentTable = LoadComment(System.Convert.ToInt32(myApplicationEntity.DocKey));
                dvComment.DataSource = myCommentTable;
                dvComment.DataBind();
                LoadApproveAccess(UserID);
                setuplookupedit();
                BindingMaster();
                Accessable();
            }
        }
        private void setuplookupedit()
        {
            if (myApplicationEntity != null)
            {
                DataView dv = new DataView(myApplicationEntity.LoadDocNoFormatTable());
                myDocNoFormatTable = dv.ToTable();

                cbAppType.Items.Add(new ListEditItem("Conventional", "Conventional"));
                cbAppType.Items.Add(new ListEditItem("Syariah", "Syariah"));

                cbObjectPembiayaan.Items.Add(new ListEditItem("Heavy Equipment", "Heavy Equipment"));
                cbObjectPembiayaan.Items.Add(new ListEditItem("Commercial Vehicle", "Commercial Vehicle"));
                cbObjectPembiayaan.Items.Add(new ListEditItem("Medical Equipment", "Medical Equipment"));
                cbObjectPembiayaan.Items.Add(new ListEditItem("Machinery Equipment", "Machinery Equipment"));
                cbObjectPembiayaan.Items.Add(new ListEditItem("Invoice", "Invoice"));
                cbObjectPembiayaan.Items.Add(new ListEditItem("Stock", "Stock"));

                cbFacility.Items.Add(new ListEditItem("Kredit Invetasi", "Kredit Invetasi"));
                cbFacility.Items.Add(new ListEditItem("Kredit Modal Kerja", "Kredit Modal Kerja"));
                cbFacility.Items.Add(new ListEditItem("Kredit Multi Guna", "Kredit Multi Guna"));

                cbJenisPengikatan.Items.Add(new ListEditItem("Financial Lease", "Financial Lease"));
                cbJenisPengikatan.Items.Add(new ListEditItem("Installment Finance", "Installment Finance"));
                cbJenisPengikatan.Items.Add(new ListEditItem("Sales & Lease Back", "Sales & Lease Back"));
                cbJenisPengikatan.Items.Add(new ListEditItem("Factoring", "Factoring"));
                cbJenisPengikatan.Items.Add(new ListEditItem("Fasilitas Modal Usaha", "Fasilitas Modal Usaha"));
                cbJenisPengikatan.Items.Add(new ListEditItem("Inventory Financing", "Inventory Financing"));

                cbPackage.Items.Add(new ListEditItem("General", "General"));
                cbPackage.Items.Add(new ListEditItem("Special Package", "Special Package"));
                cbPackage.Items.Add(new ListEditItem("Supply Chain Financing", "Supply Chain Financing"));

                myDebiturTable = LoadDebitur();
                luDebitur.DataSource = myDebiturTable;
                luDebitur.DataBind();

                mySupplierTable = LoadSupplier();
                luSupplier.DataSource = mySupplierTable;
                luSupplier.DataBind();

                myBrancTable = LoadBranch(UserID);
                cbBranch.DataSource = myBrancTable;
                cbBranch.DataBind();
            }
        }
        private DataTable LoadComment(int iMyDocKey)
        {
            DataTable mytable = new DataTable();
            mytable = localdbsetting.GetDataTable(@"SELECT * FROM [dbo].[ApplicationCommentHistory] WHERE SourceDocKey=? ORDER BY CommentDate DESC", false, iMyDocKey);
            return mytable;
        }
        private void Accessable()
        {
            object obj = null;
            bool bReadOnly = true;
            bool bEnable = true;
            bool bCancel = true;
            bool bApprove = false;
            bool bReturn = false;
            bool bHoldRelease = false;
            bool bReject = false;
            bool bSubmit = false;
            DateTime mydate = myLocalDBSetting.GetServerTime();

            txtCIF.ReadOnly = true;
            txtDocNo.ReadOnly = true;
            deDocDate.ReadOnly = true;

            txtCIF.ClientEnabled = false;
            txtCIF.ForeColor = System.Drawing.Color.Black;
            txtCIF.BackColor = System.Drawing.Color.Transparent;

            txtDocNo.ClientEnabled = false;
            txtDocNo.ForeColor = System.Drawing.Color.Black;
            txtDocNo.BackColor = System.Drawing.Color.Transparent;

            deDocDate.ClientEnabled = false;
            deDocDate.ForeColor = System.Drawing.Color.Black;
            deDocDate.BackColor = System.Drawing.Color.Transparent;

            if (myAction == DXSSAction.New)
            {
                ASPxFormLayout1.FindItemOrGroupByName("LayoutGroupApplicationEntry").Caption = "Application Entry";

                lblStatus.ClientVisible = false;
                chkSubmit.ClientEnabled = false;

                gvDetail.Columns["colNo"].Visible = false;

                bReadOnly = false;
                bEnable = true;
                bCancel = false;
                bSubmit = false;
            }
            else if (myAction == DXSSAction.View)
            {
                ASPxFormLayout1.FindItemOrGroupByName("LayoutGroupApplicationEntry").Caption = "View Application";

                chkSubmit.ClientEnabled = false;

                gvDetail.Columns["colNo"].Visible = true;

                bReadOnly = true;
                bEnable = false;
                bCancel = false;
                bApprove = false;
                bReturn = false;
                bHoldRelease = false;
                bReject = false;
                bSubmit = false;
            }
            else if (myAction == DXSSAction.Edit)
            {
                ASPxFormLayout1.FindItemOrGroupByName("LayoutGroupApplicationEntry").Caption = "Edit Application";

                gvDetail.Columns["colNo"].Visible = false;

                bReadOnly = false;
                bEnable = true;
                bCancel = false;
                bSubmit = true;

                if (accessright.IsAccessibleByUserID(UserID, "APP_CAN_CANCEL"))
                {
                    bCancel = true;
                }
                if (myApplicationEntity.Submit.ToString() == "T")
                {
                    bReadOnly = true;
                    bEnable = false;
                    bSubmit = false;

                    foreach (DataRow dr in myApproveAccesstable.Rows)
                    {
                        DataRow result = localdbsetting.GetFirstDataRow("SELECT * FROM [dbo].[ApplicationWorkflowAccess] WHERE GroupAccessCode=? AND StateDescription=?", dr["GROUP_CODE"].ToString(), myApplicationEntity.Status);
                        DataRow resultFinance = localdbsetting.GetFirstDataRow("SELECT * FROM [dbo].[ApplicationWorkflowAccess] WHERE GroupAccessCode=? AND StateDescription=?", dr["GROUP_CODE"].ToString(), "FINANCE");

                        if(resultFinance != null)
                        {
                            deDistDate.ClientEnabled = true;
                            //LayoutItem it = ASPxFormLayout.FindItemOrGroupByName("lytDisbDate") as LayoutItem;
                            //it.Visible = true;
                        }

                        if (result != null)
                        {
                            if (Convert.ToString(myApplicationEntity.OnHold) == "F")
                            {
                                bApprove = true;
                                obj = localdbsetting.ExecuteScalar("SELECT CanReturn FROM [dbo].[ApplicationWorkflowScheme] WHERE StateDescription=?", myApplicationEntity.Status);
                                if (obj != null && obj != DBNull.Value)
                                {
                                    if (obj.ToString().Contains("T"))
                                        bHoldRelease = true;
                                }
                            }
                            else
                            {
                                bApprove = false;
                                bHoldRelease = false;
                            }
                        }

                        if (Convert.ToString(myApplicationEntity.OnHold) == "T")
                        {
                            bApprove = false;
                            obj = localdbsetting.ExecuteScalar("SELECT COUNT(*) FROM [dbo].[ApplicationWorkflowScheme] WHERE StateDescription=? AND ReleaseAccess=?", myApplicationEntity.Status, dr["GROUP_CODE"].ToString());
                            if (obj != null && obj != DBNull.Value)
                            {
                                if(Convert.ToInt32(obj) > 0)
                                    bHoldRelease = true;
                            }
                        }
                    }
                    // disable by adhi - add fitur hold release
                    //if (accessright.IsAccessibleByUserID(UserID, "APP_CAN_APPROVE"))
                    //{
                    //    bApprove = true;
                    //}
                    // end disable.
                    if (accessright.IsAccessibleByUserID(UserID, "APP_CAN_REJECT"))
                    {
                        bReject = true;
                    }
                    if (accessright.IsAccessibleByUserID(UserID, "APP_CAN_RETURN"))
                    {
                        bReturn = true;
                    }
                }
                if (myApplicationEntity.Status.ToString().Contains("CANCELLED"))
                {
                    bReadOnly = true;
                    bEnable = false;
                    bSubmit = false;
                    bApprove = false;
                    bReturn = false;
                    bHoldRelease = false;
                    bCancel = false;
                    bReject = false;
                }
                if (myApplicationEntity.Status.ToString().Contains("REJECTED"))
                {
                    bReadOnly = true;
                    bEnable = false;
                    bSubmit = false;
                    bApprove = false;
                    bReturn = false;
                    bHoldRelease = false;
                    bCancel = false;
                    bReject = false;
                }
            }
            #region Accessable MY CONTROL
            luDebitur.ReadOnly = bReadOnly;
            txtDebitur2.ReadOnly = bReadOnly;
            luSupplier.ReadOnly = bReadOnly;
            txtSupplierBranch.ReadOnly = bReadOnly;
            txtMarketingSupplier.ReadOnly = bReadOnly;

            luDebitur.ClientEnabled = bEnable;
            luDebitur.ForeColor = System.Drawing.Color.Black;
            luDebitur.BackColor = luDebitur.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            txtDebitur2.ClientEnabled = bEnable;
            txtDebitur2.ForeColor = System.Drawing.Color.Black;
            txtDebitur2.BackColor = System.Drawing.Color.Transparent;

            luSupplier.ClientEnabled = bEnable;
            luSupplier.ForeColor = System.Drawing.Color.Black;
            luSupplier.BackColor = System.Drawing.Color.Transparent;
            luSupplier.BackColor = luSupplier.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            txtSupplierBranch.ClientEnabled = bEnable;
            txtSupplierBranch.ForeColor = System.Drawing.Color.Black;
            txtSupplierBranch.BackColor = txtSupplierBranch.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            txtMarketingSupplier.ClientEnabled = bEnable;
            txtMarketingSupplier.ForeColor = System.Drawing.Color.Black;
            txtMarketingSupplier.BackColor = txtMarketingSupplier.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            cbAppType.ReadOnly = bReadOnly;
            cbBranch.ReadOnly = bReadOnly;
            cbObjectPembiayaan.ReadOnly = bReadOnly;
            cbFacility.ReadOnly = bReadOnly;
            cbJenisPengikatan.ReadOnly = bReadOnly;
            cbPackage.ReadOnly = bReadOnly;

            chkSubmit.ClientEnabled = bSubmit;

            cbAppType.ClientEnabled = bEnable;
            cbAppType.ForeColor = System.Drawing.Color.Black;
            cbAppType.BackColor = cbAppType.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            cbBranch.ClientEnabled = bEnable;
            cbBranch.ForeColor = System.Drawing.Color.Black;
            cbBranch.BackColor = cbBranch.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            cbObjectPembiayaan.ClientEnabled = bEnable;
            cbObjectPembiayaan.ForeColor = System.Drawing.Color.Black;
            cbObjectPembiayaan.BackColor = cbObjectPembiayaan.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            cbFacility.ClientEnabled = bEnable;
            cbFacility.ForeColor = System.Drawing.Color.Black;
            cbFacility.BackColor = cbFacility.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            cbJenisPengikatan.ClientEnabled = bEnable;
            cbJenisPengikatan.ForeColor = System.Drawing.Color.Black;
            cbJenisPengikatan.BackColor = cbJenisPengikatan.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            cbPackage.ClientEnabled = bEnable;
            cbPackage.ForeColor = System.Drawing.Color.Black;
            cbPackage.BackColor = cbPackage.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            mmNote.ReadOnly = bReadOnly;
            mmNote.ClientEnabled = bEnable;
            mmNote.ForeColor = System.Drawing.Color.Black;
            mmNote.BackColor = mmNote.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            gvDetail.Columns["ClmnCommand"].Visible = bEnable;

            seDP.ReadOnly = bReadOnly;
            seTenor.ReadOnly = bReadOnly;
            seEffRate.ReadOnly = bReadOnly;

            seOTR.ClientEnabled = false;
            seOTR.ForeColor = System.Drawing.Color.Black;
            seOTR.BackColor = System.Drawing.Color.Transparent;

            seNTF.ClientEnabled = false;
            seNTF.ForeColor = System.Drawing.Color.Black;
            seNTF.BackColor = System.Drawing.Color.Transparent;

            seDP.ClientEnabled = bEnable;
            seDP.ForeColor = System.Drawing.Color.Black;
            seDP.BackColor = seDP.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            seTenor.ClientEnabled = bEnable;
            seTenor.ForeColor = System.Drawing.Color.Black;
            seTenor.BackColor = seTenor.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            seEffRate.ClientEnabled = bEnable;
            seEffRate.ForeColor = System.Drawing.Color.Black;
            seEffRate.BackColor = seEffRate.ClientEnabled == bEnable ? System.Drawing.Color.White : System.Drawing.Color.Transparent;

            btnSave.ClientEnabled = bEnable;
            btnApprove.ClientEnabled = bApprove;
            btnReturn.ClientEnabled = bReturn;
            btnHoldRelease.ClientEnabled = bHoldRelease;
            btnReject.ClientEnabled = bReject;
            btnCancel.ClientEnabled = bCancel;

            btnAddComment.ClientEnabled = myAction == DXSSAction.New ? false : true;
            btnCam.ClientEnabled = accessright.IsAccessibleByUserID(UserID, "APP_CAN_CAM") == true ? true : false;

            if (Convert.ToString(myApplicationEntity.OnHold) != "T")
            {
                btnHoldRelease.Text = "Hold";
            }
            if (Convert.ToString(myApplicationEntity.OnHold) != "F")
            {
                btnHoldRelease.Text = "Release";
            }
            #endregion
            seOTR.ReadOnly = true;
            seNTF.ReadOnly = true;

            if (accessright.IsAccessibleByUserID(UserID, "IS_SUPER_ADMIN"))
            {
                gvDetail.Columns["ClmnCommand"].Visible = true;

                seDP.ClientEnabled = true;
                seDP.ReadOnly = false;

                seTenor.ClientEnabled = true;
                seTenor.ReadOnly = false;

                seEffRate.ClientEnabled = true;
                seEffRate.ReadOnly = false;

                cbFacility.ClientEnabled = true;
                cbFacility.ReadOnly = false;

                cbJenisPengikatan.ClientEnabled = true;
                cbJenisPengikatan.ReadOnly = false;

                cbObjectPembiayaan.ClientEnabled = true;
                cbObjectPembiayaan.ReadOnly = false;

                if(Convert.ToString(myApplicationEntity.OnHold) != "T")
                    btnSave.ClientEnabled = true;
            }
        }
        private void BindingMaster()
        {
            txtDocNo.Value = myApplicationEntity.DocNo.ToString();
            deDocDate.Value = myApplicationEntity.DocDate;
            txtCIF.Value = myApplicationEntity.CIF;
            luDebitur.Text = myApplicationEntity.ClientName.ToString();
            luSupplier.Text = myApplicationEntity.SupplierName.ToString();
            txtSupplierBranch.Value = myApplicationEntity.SupplierBranch.ToString();
            txtMarketingSupplier.Value = myApplicationEntity.MarketingSupplier.ToString();
            cbAppType.Value = myApplicationEntity.DocumentType.ToString();
            cbBranch.Text = myApplicationEntity.Branch.ToString();
            cbObjectPembiayaan.Value = myApplicationEntity.ObjectPembiayaan.ToString();
            cbFacility.Value = myApplicationEntity.Facility.ToString();
            cbJenisPengikatan.Value = myApplicationEntity.JenisPengikatan.ToString();
            cbPackage.Value = myApplicationEntity.Package.ToString();
            mmNote.Value = myApplicationEntity.Note.ToString();
            seOTR.Value = Convert.ToDecimal(myApplicationEntity.OTR);
            seDP.Value = Convert.ToDecimal(myApplicationEntity.DP);
            seNTF.Value = Convert.ToDecimal(myApplicationEntity.NTF);
            seTenor.Value = Convert.ToDecimal(myApplicationEntity.Tenor);
            seEffRate.Value = Convert.ToDecimal(myApplicationEntity.EffRate);
            lblStatus.Text = myApplicationEntity.Status.ToString();
            if (myApplicationEntity.OnHold.ToString() == "T")
                lblStatus.Text += " - ON HOLD";
            if (Convert.ToString(myApplicationEntity.Cancelled) == "T")
            {
                btnCancel.Text = "UnCancel";
            }
            else
            {
                btnCancel.Text = "Cancel";
            }
            chkSubmit.CheckState = (myApplicationEntity.Submit.ToString() == "T" ? CheckState.Checked : CheckState.Unchecked);
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
            if (System.Convert.ToDecimal(seTenor.Value) == 0)
            {
                errorF = true;
                if (!focusF)
                {
                    seTenor.Focus();
                    focusF = true;
                    strmessageError = "Tenor value 0 is not allowed.";
                    cplMain.JSProperties["cplActiveTabIndex"] = 1;
                }
            }
            if (saveaction == SaveAction.Cam)
            {
                object obj;
                obj = localdbsetting.ExecuteScalar("SELECT CanCam FROM [dbo].[ApplicationWorkflowScheme] WHERE StateDescription=?", myApplicationEntity.Status);
                if (obj != null && obj != DBNull.Value)
                {
                    if (obj.ToString().Contains("F"))
                    {
                        errorF = true;
                        if (!focusF)
                        {
                            strmessageError = "can't 'CAM' on this status, please see schema application workflow.";
                            cplMain.JSProperties["cplActiveTabIndex"] = 1;
                        }
                    }
                }
            }
            return errorF;
        }
        protected void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose();
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            DataTable dtCopyApp = new DataTable();

            gvDetail.UpdateEdit();
            myApplicationEntity.DocNo = txtDocNo.Value;
            myApplicationEntity.DocDate = deDocDate.Value;
            myApplicationEntity.CIF = txtCIF.Value;
            myApplicationEntity.ClientName = luDebitur.Value;
            myApplicationEntity.SupplierName = luSupplier.Value;
            myApplicationEntity.SupplierBranch = txtSupplierBranch.Value;
            myApplicationEntity.MarketingSupplier = txtMarketingSupplier.Value;
            myApplicationEntity.DocumentType = cbAppType.Value;
            myApplicationEntity.Branch = cbBranch.Value;
            myApplicationEntity.ObjectPembiayaan = cbObjectPembiayaan.Value;
            myApplicationEntity.Facility = cbFacility.Value;
            myApplicationEntity.JenisPengikatan = cbJenisPengikatan.Value;
            myApplicationEntity.Package = cbPackage.Value;
            myApplicationEntity.Note = mmNote.Value;
            myApplicationEntity.OTR = seOTR.Value;
            myApplicationEntity.DP = seDP.Value;
            myApplicationEntity.NTF = seNTF.Value;
            myApplicationEntity.Tenor = seTenor.Value;
            myApplicationEntity.EffRate = seEffRate.Value;
            if (myAction == DXSSAction.New)
            {
                myApplicationEntity.CreatedBy = UserID;
                myApplicationEntity.CreatedDateTime = myLocalDBSetting.GetServerTime();
                myApplicationEntity.LastModifiedTime = myLocalDBSetting.GetServerTime();
            }
            myApplicationEntity.Submit = (chkSubmit.CheckState == CheckState.Checked ? "T" : "F");
            myApplicationEntity.Save(UserID, UserName, "APP", saveAction, myApplicationEntity.Status.ToString());
            return bSave;
        }
        private bool SaveComment(SaveAction saveAction)
        {
            bool bSave = true;
            myApplicationEntity.SaveComment(UserName, mmComment.Value.ToString(), saveAction, deDistDate.Date);
            return bSave;
        }
        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string urlsave = "";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            urlsave = "~/Transactions/CreditProcess/ApplicationList.aspx";
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("DocKey", myApplicationEntity.DocKey.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlLocalDBSetting dbLocalSetting = this.myLocalDBSetting;
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                case "CALC_NTF":
                    break;
                case "DEBITUR_ONCHANGE":
                    break;
                #region SAVE ACTION
                case "SAVE":
                    Save(SaveAction.Save);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "SAVECONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to save this application?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                #endregion
                #region APPROVE ACTION
                case "APPROVE":
                    Save(SaveAction.Approve);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been approve...";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "APPROVECONFIRM":
                    if (myDetailTable.Rows.Count == 0)
                    {
                        strmessageError = "Please add detail asset";
                    }
                    else
                    {
                        cplMain.JSProperties["cplblmessageError"] = "";
                        cplMain.JSProperties["cplblmessage"] = "are you sure want to approve this Application?";
                        cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                        if (ErrorInField(out strmessageError, SaveAction.Approve))
                        {
                            cplMain.JSProperties["cplblmessageError"] = strmessageError;
                        }
                    }
                    break;
                #endregion
                #region HOLD RELEASE ACTION
                case "HOLD_RELEASE":
                    if (Convert.ToString(myApplicationEntity.OnHold) == "F")
                    {
                        Save(SaveAction.OnHold);
                        cplMain.JSProperties["cpAlertMessage"] = "Transaction has been Hold...";
                    }
                    else
                    {
                        Save(SaveAction.Release);
                        cplMain.JSProperties["cpAlertMessage"] = "Transaction has been Release...";
                    }
                    cplMain.JSProperties["cplblActionButton"] = "HOLD_RELEASE";
                    ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "HOLD_RELEASE_CONFIRM":
                    if (myDetailTable.Rows.Count == 0)
                    {
                        strmessageError = "Please add detail asset";
                    }
                    else
                    {
                        cplMain.JSProperties["cplblmessageError"] = "";
                        if (Convert.ToString(myApplicationEntity.OnHold) == "F")
                            cplMain.JSProperties["cplblmessage"] = "are you sure want to Hold this Application?";
                        if (Convert.ToString(myApplicationEntity.OnHold) == "T")
                            cplMain.JSProperties["cplblmessage"] = "are you sure want to Release this Application?";
                        cplMain.JSProperties["cplblActionButton"] = "HOLD_RELEASE";

                        if (ErrorInField(out strmessageError, SaveAction.OnHold) && Convert.ToString(myApplicationEntity.OnHold) == "F")
                        {
                            cplMain.JSProperties["cplblmessageError"] = strmessageError;
                        }
                        if (ErrorInField(out strmessageError, SaveAction.Release) && Convert.ToString(myApplicationEntity.OnHold) == "T")
                        {
                            cplMain.JSProperties["cplblmessageError"] = strmessageError;
                        }
                    }
                    break;
                #endregion
                #region REJECT ACTION
                case "REJECT":
                    Save(SaveAction.Reject);
                    cplMain.JSProperties["cpAlertMessage"] = "Application has been rejected..";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "REJECTCONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to reject this application ?";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    if (ErrorInField(out strmessageError, SaveAction.SaveComment))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                #endregion
                #region SAVE COMMENT ACTION
                case "SAVE_COMMENT":
                    SaveComment(SaveAction.SaveComment);
                    cplMain.JSProperties["cpAlertMessage"] = "Comment has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE_COMMENT";
                    ASPxWebControl.RedirectOnCallback(Request.Url.AbsoluteUri);
                    break;
                case "SAVE_COMMENT_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to save this comment ?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE_COMMENT";
                    if (ErrorInField(out strmessageError, SaveAction.SaveComment))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                #endregion
                #region CAM ACTION
                case "CAM":
                    Save(SaveAction.Cam);
                    cplMain.JSProperties["cpAlertMessage"] = "Comment has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "CAM";
                    ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "CAM_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to CAM this application ?, it will send to CA back.";
                    cplMain.JSProperties["cplblActionButton"] = "CAM";
                    if (ErrorInField(out strmessageError, SaveAction.Cam))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                #endregion
                #region CANCEL ACTION
                case "CANCEL":
                    Save(SaveAction.Cancel);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been cancel...";
                    cplMain.JSProperties["cplblActionButton"] = "CANCEL";
                    ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "CANCEL_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to cancel this application ?";
                    cplMain.JSProperties["cplblActionButton"] = "CANCEL";
                    if (ErrorInField(out strmessageError, SaveAction.Cancel))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                #endregion
                #region RETURN ACTION
                case "RETURN":
                    Save(SaveAction.Return);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been return...";
                    cplMain.JSProperties["cplblActionButton"] = "RETURN";
                    ASPxWebControl.RedirectOnCallback(urlsave + updatedQueryString);
                    break;
                case "RETURN_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to return this application ?";
                    cplMain.JSProperties["cplblActionButton"] = "RETURN";
                    if (ErrorInField(out strmessageError, SaveAction.Return))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                #endregion
            }
        }
        protected void gvDetail_Init(object sender, EventArgs e)
        {

        }
        protected void gvDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }
        protected void gvDetail_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myds.Tables[1];
        }
        protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["ItemDescription"] == null) throw new Exception("Column 'Item Description' is mandatory.");
            if (e.NewValues["UnitPrice"] == null) throw new Exception("Column 'Unit Price' is mandatory.");
            if (e.NewValues["Year"] == null) throw new Exception("Column 'Year' is mandatory.");
            if (e.NewValues["Qty"] == null) throw new Exception("Column 'Qty' is mandatory.");
            if (e.NewValues["Condition"] == null) throw new Exception("Column 'Condition' is mandatory.");
            if (e.NewValues["AssetTypeDetail"] == null) throw new Exception("Column 'Asset Type' is mandatory.");
            if (StrErrorMsg == "")
            {
                gvDetail.JSProperties["cpCmd"] = "INSERT";

                DataRow[] ValidLinesRows = myDetailTable.Select("", "Seq", DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.ModifiedCurrent);
                int seq = SeqUtils.GetLastSeq(ValidLinesRows);

                myDetailTable.Rows.Add(myApplicationEntity.Applicationcommand.DtlKeyUniqueKey(), myApplicationEntity.DocKey, seq, e.NewValues["Condition"], e.NewValues["ItemDescription"], e.NewValues["Year"], e.NewValues["UnitPrice"], e.NewValues["Qty"], e.NewValues["SubTotal"], e.NewValues["AssetTypeDetail"]);

                decimal vNetAmountTotal = decimal.Parse(seOTR.Value.ToString());
                vNetAmountTotal += decimal.Parse(e.NewValues["SubTotal"].ToString());
                gvDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

                ASPxGridView grid = sender as ASPxGridView;
                grid.CancelEdit();
                e.Cancel = true;
            }
        }
        protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string StrErrorMsg = "";
            if (e.NewValues["ItemDescription"] == null) throw new Exception("Column 'Item Description' is mandatory.");
            if (e.NewValues["UnitPrice"] == null) throw new Exception("Column 'Unit Price' is mandatory.");
            if (e.NewValues["Year"] == null) throw new Exception("Column 'Year' is mandatory.");
            if (e.NewValues["Qty"] == null) throw new Exception("Column 'Qty' is mandatory.");
            if (e.NewValues["Condition"] == null) throw new Exception("Column 'Condition' is mandatory.");
            if (e.NewValues["AssetTypeDetail"] == null) throw new Exception("Column 'Asset Type' is mandatory.");
            if (StrErrorMsg == "")
            {
                gvDetail.JSProperties["cpCmd"] = "UPDATE";
                int editingRowVisibleIndex = gvDetail.EditingRowVisibleIndex;
                int id = (int)gvDetail.GetRowValues(editingRowVisibleIndex, "DtlKey");
                DataRow dr = myDetailTable.Rows.Find(id);
                dr["Condition"] = e.NewValues["Condition"];
                dr["ItemDescription"] = e.NewValues["ItemDescription"];
                dr["Year"] = e.NewValues["Year"];
                dr["UnitPrice"] = e.NewValues["UnitPrice"];
                dr["Qty"] = e.NewValues["Qty"];
                dr["SubTotal"] = e.NewValues["SubTotal"];
                dr["AssetTypeDetail"] = e.NewValues["AssetTypeDetail"];

                decimal vNetAmountTotal = decimal.Parse(seOTR.Value.ToString());
                vNetAmountTotal -= decimal.Parse(e.OldValues["SubTotal"].ToString());
                vNetAmountTotal += decimal.Parse(e.NewValues["SubTotal"].ToString());
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

            decimal vNetAmountTotal = decimal.Parse(seOTR.Value.ToString());
            vNetAmountTotal -= decimal.Parse(dr["SubTotal"].ToString());
            gvDetail.JSProperties["cpTotal"] = (vNetAmountTotal).ToString();

            myDetailTable.Rows.Remove(dr);

            ASPxGridView grid = sender as ASPxGridView;
            grid.CancelEdit();
            e.Cancel = true;
        }
        protected void gvDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void gvDetail_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Qty" || e.Column.FieldName == "UnitPrice")
            {
                (e.Editor as ASPxTextBox).AutoPostBack = false;
            }
        }
        protected void gvDetail_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }
        protected void luDebitur_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myDebiturTable;
        }
        protected void cbSupplier_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = mySupplierTable;
        }
        protected void cbBranch_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxComboBox).DataSource = myBrancTable;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/CreditProcess/ApplicationList.aspx");
        }
        protected void dvComment_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myCommentTable;
        }
    }
}