using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.UpdateNoSPPH
{
    public partial class UpdateNoSPPHEntry : BasePage
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
        protected UpdateNoSPPHDB myUpdateNoSPPHDB
        {
            get { isValidLogin(false); return (UpdateNoSPPHDB)HttpContext.Current.Session["myUpdateNoSPPHDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUpdateNoSPPHDB" + this.ViewState["_PageID"]] = value; }
        }
        protected UpdateNoSPPHEntity myUpdateNoSPPHEntity
        {
            get { isValidLogin(false); return (UpdateNoSPPHEntity)HttpContext.Current.Session["myUpdateNoSPPHEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUpdateNoSPPHEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myHeaderTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myHeadeTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myHeadeTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myAgreementTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myAgreementTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAgreementTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myPengurusTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myPengurusTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myPengurusTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myPengurusTable2
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myPengurusTable2" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myPengurusTable2" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myUploadTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myUploadTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUploadTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myApproveAccesstable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApproveAccesstable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApproveAccesstable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataSet myds
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myds" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myds" + this.ViewState["_PageID"]] = value; }
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
        protected string myStatus
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["myStatus" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myStatus" + this.ViewState["_PageID"]] = value; }
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
                    this.myUpdateNoSPPHDB = UpdateNoSPPHDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                    myUpdateNoSPPHEntity = this.myUpdateNoSPPHDB.View(Convert.ToInt32(this.Request.QueryString["SourceKey"]));
                }
                myds = new DataSet();
                this.myUpdateNoSPPHDB = UpdateNoSPPHDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((UpdateNoSPPHEntity)HttpContext.Current.Session["myUpdateNoSPPHEntity" + strKey]);

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
        private void SetApplication(UpdateNoSPPHEntity UpdateNoSPPHEntity)
        {
            if (this.myUpdateNoSPPHEntity != UpdateNoSPPHEntity)
            {
                if (UpdateNoSPPHEntity != null)
                {
                    this.myUpdateNoSPPHEntity = UpdateNoSPPHEntity;
                }
                myAction = this.myUpdateNoSPPHEntity.Action;
                myDocType = this.myUpdateNoSPPHEntity.DocType;
                myds = myUpdateNoSPPHEntity.myDataSet;
                myStatus = this.myUpdateNoSPPHEntity.Status.ToString();
                myHeaderTable = myds.Tables[0];
                LoadApproveAccess(UserID);
                setuplookupedit();
                BindingMaster();
                Accessable();
            }
        }
        private void setuplookupedit()
        {
            if (myUpdateNoSPPHEntity != null)
            {
                string sCondition = "";
                if (Convert.ToString(myUpdateNoSPPHEntity.Status) != "APPROVE")
                    sCondition = @" and isnull(b.FAKTURNO,'') = ''";

                myAgreementTable = new DataTable();
                myAgreementTable = myDBSetting.GetDataTable(@"select c.C_NAME, a.LSAGREE, a.NAME, a.LSPERIOD TENOR, a.RENTAL INSTALLMENT, DISBURSEDT
                                                                from LS_AGREEMENT a with(NOLOCK)
                                                                left join LS_ASSETVEHICLE b with(NOLOCK) on a.LSAGREE = b.LSAGREE 
                                                                left join SYS_COMPANY c with(NOLOCK) on a.C_CODE = c.C_CODE
                                                                where PRODUCT_FACILITY_CODE = '112' and DISBURSEDT >= '2020/01/01' " + sCondition, false);

                luAgreement.DataSource = myAgreementTable;
                luAgreement.DataBind();

                if (myAction == DXSSAction.New)
                {
                    myPengurusTable = new DataTable();
                    myPengurusTable = myLocalDBSetting.GetDataTable(@"SELECT * FROM 
                                                                (select 
                                                                USER_ID as [ID], 
                                                                USER_NAME as [UserName],
                                                                'KARYAWAN' 'Tipe'
                                                                from MASTER_USER
                                                                where IS_ACTIVE_FLAG=1
                                                                UNION ALL
                                                                select 
                                                                MCode as [ID],
                                                                Nama as [UserName],
                                                                'MITRA' 'Tipe'
                                                                from Mitra
                                                                where IsActive='T') 
                                                                as tbl1
                                                                where 
                                                                Tipe=?", false, rbtPengurus.Value);
                }
                else
                {
                    myPengurusTable = new DataTable();
                    myPengurusTable = myLocalDBSetting.GetDataTable(@"SELECT * FROM 
                                                                (select 
                                                                USER_ID as [ID], 
                                                                USER_NAME as [UserName],
                                                                'KARYAWAN' 'Tipe'
                                                                from MASTER_USER
                                                                where IS_ACTIVE_FLAG=1
                                                                UNION ALL
                                                                select 
                                                                MCode as [ID],
                                                                Nama as [UserName],
                                                                'MITRA' 'Tipe'
                                                                from Mitra
                                                                where IsActive='T') 
                                                                as tbl1
                                                                where 
                                                                Tipe=?", false, myUpdateNoSPPHEntity.JenisPengurus);
                }
                luPengurus.DataSource = myPengurusTable;
                luPengurus.DataBind();

                myPengurusTable2 = new DataTable();
                myPengurusTable2 = myLocalDBSetting.GetDataTable(@"select USER_ID as [ID], 
                    USER_NAME as [UserName],
                    'KARYAWAN' 'Tipe'
                    from MASTER_USER
                    where IS_ACTIVE_FLAG=1", false);

                luPengurus2.DataSource = myPengurusTable2;
                luPengurus2.DataBind();

                luPengurus3.DataSource = myPengurusTable2;
                luPengurus3.DataBind();

                if (myAction == DXSSAction.Edit || myAction == DXSSAction.View)
                {
                    ASPxFormLayout.FindItemOrGroupByName("tbLayoutGroup").Visible = true;

                    myUploadTable = new DataTable();
                    myUploadTable = LoadUploadDoctable(myUpdateNoSPPHEntity.AgreementNo);
                    gvUploadDoc.DataSource = myUploadTable;
                    gvUploadDoc.DataBind();
                }
            }
        }
        private void Accessable()
        {
            txtBranch.ClientEnabled = false;
            txtBranch.BackColor = System.Drawing.Color.Transparent;
            txtBranch.ForeColor = System.Drawing.Color.Black;

            txtStatus.ClientEnabled = false;
            txtStatus.BackColor = System.Drawing.Color.Transparent;
            txtStatus.ForeColor = System.Drawing.Color.Black;

            txtDebitur.ClientEnabled = false;
            txtDebitur.BackColor = System.Drawing.Color.Transparent;
            txtDebitur.ForeColor = System.Drawing.Color.Black;

            txtInstallment.ClientEnabled = false;
            txtInstallment.BackColor = System.Drawing.Color.Transparent;
            txtInstallment.ForeColor = System.Drawing.Color.Black;

            txtTenor.ClientEnabled = false;
            txtTenor.BackColor = System.Drawing.Color.Transparent;
            txtTenor.ForeColor = System.Drawing.Color.Black;

            txtNamaPengurus.ClientEnabled = false;
            txtNamaPengurus.BackColor = System.Drawing.Color.Transparent;
            txtNamaPengurus.ForeColor = System.Drawing.Color.Black;

            txtNamaAdmin2.ClientEnabled = false;
            txtNamaAdmin2.BackColor = System.Drawing.Color.Transparent;
            txtNamaAdmin2.ForeColor = System.Drawing.Color.Black;

            txtNamaAdmin3.ClientEnabled = false;
            txtNamaAdmin3.BackColor = System.Drawing.Color.Transparent;
            txtNamaAdmin3.ForeColor = System.Drawing.Color.Black;

            deDisburseDate.ClientEnabled = false;
            deDisburseDate.BackColor = System.Drawing.Color.Transparent;
            deDisburseDate.ForeColor = System.Drawing.Color.Black;

            btnApprove.ClientVisible = false;
            btnReject.ClientVisible = false;

            if (myAction == DXSSAction.New)
            {
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUpdateNoSPPHEntry").Caption = "New Update No. SPPH Entry";
            }
            else if (myAction == DXSSAction.View)
            {
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUpdateNoSPPHEntry").Caption = "View Update No. SPPH Entry";

                luAgreement.ClientEnabled = false;
                luAgreement.BackColor = System.Drawing.Color.Transparent;
                luAgreement.ForeColor = System.Drawing.Color.Black;

                luPengurus.ClientEnabled = false;
                luPengurus.BackColor = System.Drawing.Color.Transparent;
                luPengurus.ForeColor = System.Drawing.Color.Black;

                rbtPengurus.ClientEnabled = false;
                rbtPengurus.BackColor = System.Drawing.Color.Transparent;
                rbtPengurus.ForeColor = System.Drawing.Color.Black;

                txtNoSPPH.ClientEnabled = false;
                txtNoSPPH.BackColor = System.Drawing.Color.Transparent;
                txtNoSPPH.ForeColor = System.Drawing.Color.Black;

                luPengurus2.ClientEnabled = false;
                luPengurus2.BackColor = System.Drawing.Color.Transparent;
                luPengurus2.ForeColor = System.Drawing.Color.Black;

                luPengurus3.ClientEnabled = false;
                luPengurus3.BackColor = System.Drawing.Color.Transparent;
                luPengurus3.ForeColor = System.Drawing.Color.Black;

                btnSave.ClientVisible = false;
                btnApprove.ClientVisible = false;
                btnReject.ClientVisible = false;
            }
            else if (myAction == DXSSAction.Edit)
            {
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupUpdateNoSPPHEntry").Caption = "Edit Update No. SPPH Entry";

                if (txtNamaPengurus.Text != null && txtNamaPengurus.Text != "")
                {
                    rbtPengurus.ClientEnabled = false;
                    rbtPengurus.BackColor = System.Drawing.Color.Transparent;
                    rbtPengurus.ForeColor = System.Drawing.Color.Black;

                    luPengurus.ClientEnabled = false;
                    luPengurus.BackColor = System.Drawing.Color.Transparent;
                    luPengurus.ForeColor = System.Drawing.Color.Black;
                }
                if (txtNamaAdmin2.Text != null && txtNamaAdmin2.Text != "")
                {
                    luPengurus2.ClientEnabled = false;
                    luPengurus2.BackColor = System.Drawing.Color.Transparent;
                    luPengurus2.ForeColor = System.Drawing.Color.Black;
                }
                if (txtNamaAdmin3.Text != null && txtNamaAdmin3.Text != "")
                {
                    luPengurus3.ClientEnabled = false;
                    luPengurus3.BackColor = System.Drawing.Color.Transparent;
                    luPengurus3.ForeColor = System.Drawing.Color.Black;
                }

                foreach (DataRow dr in myApproveAccesstable.Rows)
                {
                    if(dr["GROUP_CODE"].ToString().ToUpper() == "BR-BM")
                    {
                        btnApprove.ClientVisible = true;
                        btnReject.ClientVisible = true;
                    }
                }

                //if (accessright.IsAccessibleByUserID(UserID, "SPPH_CAN_APPROVE"))
                //{
                //    btnApprove.ClientVisible = true;
                //    btnReject.ClientVisible = true;
                //}

                if (Convert.ToString(myUpdateNoSPPHEntity.Status).Contains("SPPH"))
                {
                    btnApprove.ClientVisible = false;
                    btnReject.ClientVisible = false;
                }

                if (Convert.ToString(myUpdateNoSPPHEntity.Status).Contains("APPROVE") || Convert.ToString(myUpdateNoSPPHEntity.Status).Contains("REJECT"))
                {
                    luAgreement.ClientEnabled = false;
                    luAgreement.BackColor = System.Drawing.Color.Transparent;
                    luAgreement.ForeColor = System.Drawing.Color.Black;

                    luPengurus.ClientEnabled = false;
                    luPengurus.BackColor = System.Drawing.Color.Transparent;
                    luPengurus.ForeColor = System.Drawing.Color.Black;

                    luPengurus2.ClientEnabled = false;
                    luPengurus2.BackColor = System.Drawing.Color.Transparent;
                    luPengurus2.ForeColor = System.Drawing.Color.Black;

                    luPengurus3.ClientEnabled = false;
                    luPengurus3.BackColor = System.Drawing.Color.Transparent;
                    luPengurus3.ForeColor = System.Drawing.Color.Black;

                    rbtPengurus.ClientEnabled = false;
                    rbtPengurus.BackColor = System.Drawing.Color.Transparent;
                    rbtPengurus.ForeColor = System.Drawing.Color.Black;

                    txtNoSPPH.ClientEnabled = false;
                    txtNoSPPH.BackColor = System.Drawing.Color.Transparent;
                    txtNoSPPH.ForeColor = System.Drawing.Color.Black;

                    btnSave.ClientVisible = false;
                    btnApprove.ClientVisible = false;
                    btnReject.ClientVisible = false;
                }
            }
        }

        private void BindingMaster()
        {
            luAgreement.Value = myUpdateNoSPPHEntity.AgreementNo;
            txtStatus.Value = myUpdateNoSPPHEntity.Status;
            txtDebitur.Value = myUpdateNoSPPHEntity.DebiturName;
            txtTenor.Value = myUpdateNoSPPHEntity.Tenor;
            txtInstallment.Value = myUpdateNoSPPHEntity.Installment;
            txtBranch.Value = myUpdateNoSPPHEntity.Branch;
            deDisburseDate.Value = myUpdateNoSPPHEntity.DisburseDate;

            if (myUpdateNoSPPHEntity.JenisPengurus != DBNull.Value)
                rbtPengurus.Items.FindByValue(Convert.ToString(myUpdateNoSPPHEntity.JenisPengurus)).Selected = true;

            luPengurus.Value = myUpdateNoSPPHEntity.IDPengurus;
            txtNamaPengurus.Value = myUpdateNoSPPHEntity.NamaPengurus;
            txtNoSPPH.Value = myUpdateNoSPPHEntity.NoSPPH;

            luPengurus2.Value = myUpdateNoSPPHEntity.IDSalesAdmin;
            txtNamaAdmin2.Value = myUpdateNoSPPHEntity.NamaSalesAdmin;
            luPengurus3.Value = myUpdateNoSPPHEntity.IDMktHead;
            txtNamaAdmin3.Value = myUpdateNoSPPHEntity.NamaMktHead;

            if (myAction == DXSSAction.Edit)
            {
                
            }
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;

            myUpdateNoSPPHEntity.AgreementNo = luAgreement.Value;
            myUpdateNoSPPHEntity.NoSPPH = txtNoSPPH.Value;
            myUpdateNoSPPHEntity.JenisPengurus = rbtPengurus.SelectedItem.Value;
            myUpdateNoSPPHEntity.IDPengurus = luPengurus.Value;
            myUpdateNoSPPHEntity.NamaPengurus = txtNamaPengurus.Value;
            myUpdateNoSPPHEntity.Status = txtStatus.Value;
            myUpdateNoSPPHEntity.DebiturName = txtDebitur.Value;
            myUpdateNoSPPHEntity.Tenor = txtTenor.Value;
            myUpdateNoSPPHEntity.Installment = txtInstallment.Value;
            myUpdateNoSPPHEntity.Branch = txtBranch.Value;
            myUpdateNoSPPHEntity.IDSalesAdmin = luPengurus2.Value;
            myUpdateNoSPPHEntity.NamaSalesAdmin = txtNamaAdmin2.Text;
            myUpdateNoSPPHEntity.IDMktHead = luPengurus3.Value;
            myUpdateNoSPPHEntity.NamaMktHead = txtNamaAdmin3.Text;
            myUpdateNoSPPHEntity.DisburseDate = deDisburseDate.Date;
            
            if(txtNoSPPH.Value == null)
            {
                myUpdateNoSPPHEntity.Status = "WAITING FOR SPPH";
            }else
            {
                myUpdateNoSPPHEntity.Status = "NEW";
            }

            if (myAction == DXSSAction.New)
            {
                myUpdateNoSPPHEntity.CreatedBy = UserID;
                myUpdateNoSPPHEntity.CreatedDateTime = myLocalDBSetting.GetServerTime();
                myUpdateNoSPPHEntity.LastModifiedBy = UserID;
                myUpdateNoSPPHEntity.LastModifiedDateTime = myLocalDBSetting.GetServerTime();
            }
            myUpdateNoSPPHEntity.Save(this.UserID, this.UserName, saveAction);

            return bSave;
        }
        

        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;

            return errorF;
        }

        protected void luAgreement_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myAgreementTable;
        }

        protected void luPengurus_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myPengurusTable;
        }
        protected void luPengurus_Init(object sender, EventArgs e)
        {
            ASPxGridLookup lookup = (ASPxGridLookup)sender;
            ASPxGridView gridView = lookup.GridView;
            gridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(luPengurus_CustomCallback);
        }
        void luPengurus_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            myPengurusTable = new DataTable();
            myPengurusTable = myLocalDBSetting.GetDataTable(@"SELECT * FROM 
                                                                (select 
                                                                USER_ID as [ID], 
                                                                USER_NAME as [UserName],
                                                                'KARYAWAN' 'Tipe'
                                                                from MASTER_USER
                                                                where IS_ACTIVE_FLAG=1
                                                                UNION ALL
                                                                select 
                                                                MCode as [ID],
                                                                Nama as [UserName],
                                                                'MITRA' 'Tipe'
                                                                from Mitra
                                                                where IsActive='T') 
                                                                as tbl1
                                                                where 
                                                                Tipe=?", false, rbtPengurus.Value);
            luPengurus.DataSource = myPengurusTable;
            luPengurus.DataBind();

            //luPengurus2.DataSource = myPengurusTable;
            //luPengurus2.DataBind();

            //luPengurus3.DataSource = myPengurusTable;
            //luPengurus3.DataBind();
        }

        
        protected void luPengurus2_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myPengurusTable2;
        }
        protected void luPengurus2_Init(object sender, EventArgs e)
        {
            ASPxGridLookup lookup = (ASPxGridLookup)sender;
            ASPxGridView gridView = lookup.GridView;
            gridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(luPengurus2_CustomCallback);
        }
        void luPengurus2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            myPengurusTable2 = new DataTable();
            myPengurusTable2 = myLocalDBSetting.GetDataTable(@"select USER_ID as [ID], 
                USER_NAME as [UserName],
                'KARYAWAN' 'Tipe'
                from MASTER_USER
                where IS_ACTIVE_FLAG=1", false);
            luPengurus2.DataSource = myPengurusTable2;
            luPengurus2.DataBind();
        }

        protected void luPengurus3_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myPengurusTable2;
        }
        protected void luPengurus3_Init(object sender, EventArgs e)
        {
            ASPxGridLookup lookup = (ASPxGridLookup)sender;
            ASPxGridView gridView = lookup.GridView;
            gridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(luPengurus3_CustomCallback);
        }
        void luPengurus3_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            myPengurusTable2 = new DataTable();
            myPengurusTable2 = myLocalDBSetting.GetDataTable(@"select 
                USER_ID as [ID], 
                USER_NAME as [UserName],
                'KARYAWAN' 'Tipe'
                from MASTER_USER
                where IS_ACTIVE_FLAG=1", false);
            luPengurus3.DataSource = myPengurusTable2;
            luPengurus3.DataBind();
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string urlsave = "";
            urlsave = "~/Transactions/Syariah/UpdateNoSPPH/UpdateNoSPPHList.aspx";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlLocalDBSetting localdbsetting = this.myLocalDBSetting;

            string strmessageError = "";

            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];

            switch (callbackParam[0].ToUpper())
            {
                case "REFRESH":
                    break;
                case "ON_LOAD":
                    break;
                case "SAVE_CONFIRM":
                    object paramValue2 = callbackParam[2];
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to save this data?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }

                    if (myAction != DXSSAction.Edit)
                    {
                        object obj = localdbsetting.ExecuteScalar("SELECT COUNT(*) FROM UpdateSPPHNo WHERE AgreementNo=? ", paramValue2);
                        if (obj != null || obj != DBNull.Value)
                        {
                            if (Convert.ToInt32(obj) >= 1)
                            { cplMain.JSProperties["cplblmessageError"] = "Duplikat nomor agreement."; return; }
                        }

                        //object obj = localdbsetting.ExecuteScalar("SELECT COUNT(*) FROM UpdateSPPHNo WHERE AgreementNo=? AND Status = 'NEW' AND ISNULL(NoSPPH,'') <> ''", paramValue2);
                        //if (obj != null || obj != DBNull.Value)
                        //{
                        //    if (myAction == DXSSAction.New)
                        //    {
                        //        if (Convert.ToInt32(obj) >= 1)
                        //        { cplMain.JSProperties["cplblmessageError"] = "Duplikat nomor agreement, silahkan proses terlebih dahulu data sebelumnya."; return; }
                        //    }
                        //}

                        //obj = localdbsetting.ExecuteScalar("SELECT COUNT(*) FROM UpdateSPPHNo WHERE AgreementNo=? AND Status = 'APPROVE'", paramValue2);
                        //if (obj != null || obj != DBNull.Value)
                        //{
                        //    if (Convert.ToInt32(obj) >= 1)
                        //    { cplMain.JSProperties["cplblmessageError"] = "Duplikat nomor agreement, No SPPH sudah pernah diupdate sebelumnya."; return; }
                        //}
                    }


                    break;
                case "SAVE":
                    Save(SaveAction.Save);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
                case "APPROVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to approve this?";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    if (ErrorInField(out strmessageError, SaveAction.Approve))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "APPROVE":
                    Save(SaveAction.Approve);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been approve...";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
                case "REJECT_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to reject this?";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    if (ErrorInField(out strmessageError, SaveAction.Reject))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "REJECT":
                    Save(SaveAction.Reject);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been reject...";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
            }
        }

        protected DataTable LoadUploadDoctable(object strAgreeNo)
        {
            string SqlQuery = "";
            DataTable mytable = new DataTable();
            SqlConnection myconn = new SqlConnection(localdbsetting.ConnectionString);
            SqlQuery = @"SELECT 
                            [ID],[Name],[Type],
                            [Ext],[Remarks],[AppNo],
                            [CreatedBy],[CreatedDateTime],[DebiturName],
                            [AgreeNo],[Module] 
                        FROM [dbo].[DocumentFile] 
                        WHERE [AgreeNo] = @AgreeNo
                        ORDER BY CreatedDateTime DESC";
            using (SqlCommand cmdclientdata = new SqlCommand(SqlQuery, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdclientdata);
                cmdclientdata.CommandType = CommandType.Text;
                cmdclientdata.Parameters.Add("@AgreeNo", SqlDbType.NVarChar);
                cmdclientdata.Parameters["@AgreeNo"].Value = strAgreeNo;
                adapter.Fill(mytable);
            }
            return mytable;
        }

        protected DataTable LoadListAdmin(string strAgreeNo)
        {
            string ssql = "";
            DataTable mytable = new DataTable();
            SqlConnection myconn = new SqlConnection(localdbsetting.ConnectionString);
            ssql = @"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY DocKey ASC) AS rownumber, IDPengurus, NamaPengurus FROM SSS.dbo.UpdateSPPHNo WHERE AgreementNo = @AgreeNo) AS tblAdmin";
            using (SqlCommand cmdclientdata = new SqlCommand(ssql, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdclientdata);
                cmdclientdata.CommandType = CommandType.Text;
                cmdclientdata.Parameters.Add("@AgreeNo", SqlDbType.NVarChar);
                cmdclientdata.Parameters["@AgreeNo"].Value = strAgreeNo;
                adapter.Fill(mytable);
            }
            return mytable;
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
                    ASPxWebControl.RedirectOnCallback(string.Format("UpdateNoSPPHEntry.aspx?ID=" + FileDocID.ToString()));
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }
        protected void gvUploadDoc_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myUploadTable;
        }
        protected void gvUploadDoc_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (myUpdateNoSPPHEntity != null)
            {
                if (myAction != DXSSAction.Edit)
                    return;

                isValidLogin();
                myUploadTable = LoadUploadDoctable(myUpdateNoSPPHEntity.AgreementNo);
                gvUploadDoc.DataSource = myUploadTable;
                gvUploadDoc.DataBind();
            }
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

        
        
    }
}