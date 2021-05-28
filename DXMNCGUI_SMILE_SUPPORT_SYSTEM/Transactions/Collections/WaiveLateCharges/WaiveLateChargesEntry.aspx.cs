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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections.WaiveLateCharges
{
    public partial class WaiveLateChargesEntry : BasePage
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
        protected WaiveLateChargesDB myWaiveLateChargesDB
        {
            get { isValidLogin(false); return (WaiveLateChargesDB)HttpContext.Current.Session["myWaiveLateChargesDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myWaiveLateChargesDB" + this.ViewState["_PageID"]] = value; }
        }
        protected WaiveLateChargesEntity myWaiveLateChargesEntity
        {
            get { isValidLogin(false); return (WaiveLateChargesEntity)HttpContext.Current.Session["myWaiveLateChargesEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myWaiveLateChargesEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataSet myds
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myds" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myds" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myAgreementTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myAgreementTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAgreementTable" + this.ViewState["_PageID"]] = value; }
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
        protected DataTable myDetailTable2
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailTable2" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailTable2" + this.ViewState["_PageID"]] = value; }
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

        public void LoadDetailAgreement(string sAgreement)
        {
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(@"sp_MNCL_GetOSLC");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = myconn;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@LSAGREE", SqlDbType.NVarChar);
                sqlParameter1.Value = sAgreement;

                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@TOTALOSLCAMT", SqlDbType.Float);
                sqlParameter2.Direction = ParameterDirection.Output;

                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@TOTALWAIVELCAMT", SqlDbType.Float);
                sqlParameter3.Direction = ParameterDirection.Output;

                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@MAXWAIVEAMT", SqlDbType.Float);
                sqlParameter4.Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlCommand;
                da.Fill(ds);
                myDetailTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
        }
        public void LoadDetailWaive(string sAgreement)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM [dbo].[LateChargesWaive] WHERE RefNo=@RefNo AND Status='APPROVE' AND Cancelled='F'");
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@RefNo", SqlDbType.NVarChar);
                sqlParameter1.Value = sAgreement;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlCommand;
                da.Fill(ds);
                myDetailTable2 = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
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
                    this.myWaiveLateChargesDB = WaiveLateChargesDB.Create(myDBSetting , myLocalDBSetting, myDBSession);
                    myWaiveLateChargesEntity = this.myWaiveLateChargesDB.View(Convert.ToInt32(this.Request.QueryString["SourceKey"]));
                }       
                myds = new DataSet();
                this.myWaiveLateChargesDB = WaiveLateChargesDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                strKey = Request.QueryString["Key"];
                SetApplication((WaiveLateChargesEntity)HttpContext.Current.Session["myWaiveLateChargesEntity" + strKey]);
            }
            if (!IsCallback)
            {

            }
        }
        private void SetApplication(WaiveLateChargesEntity WaiveLateChargesEntity)
        {
            if (this.myWaiveLateChargesEntity != WaiveLateChargesEntity)
            {
                if (WaiveLateChargesEntity != null)
                {
                    this.myWaiveLateChargesEntity = WaiveLateChargesEntity;
                }
                myAction = this.myWaiveLateChargesEntity.Action;
                myDocType = this.myWaiveLateChargesEntity.DocType;
                myds = myWaiveLateChargesEntity.myDataSet;
                myStatus = this.myWaiveLateChargesEntity.Status.ToString();
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
            if (myWaiveLateChargesEntity != null)
            {
                deDocDate.Value = myDBSetting.GetServerTime();

                myAgreementTable = new DataTable();
                myAgreementTable = myDBSetting.GetDataTable(@"SELECT a.APPLICNO AS APPLICNO, a.LSAGREE[NO KONTRAK], a.LESSEE[CLIENT], UPPER(a.NAME)[DEBITUR]
                                                                FROM LS_AGREEMENT a
                                                                INNER JOIN SYS_COMPANY b ON b.C_CODE = a.C_CODE", false);
                luAgreement.DataSource = myAgreementTable;
                luAgreement.DataBind();
            }
        }
        private void Accessable()
        {
            txtDocNo.ReadOnly = true;
            txtDocNo.BackColor = System.Drawing.Color.Transparent;

            txtStatus.ReadOnly = true;
            txtStatus.BackColor = System.Drawing.Color.Transparent;

            deDocDate.ReadOnly = true;
            deDocDate.BackColor = System.Drawing.Color.Transparent;

            txtDebitur.ReadOnly = true;
            txtDebitur.BackColor = System.Drawing.Color.Transparent;

            if (accessright.IsAccessibleByUserID(UserID, "WCL_CAN_APPROVE"))
            {
                btnApprove.ClientEnabled = true;
                btnReject.ClientEnabled = true;
            }

            if (myAction == DXSSAction.New)
            {
                DataRow[] myrowDocNo = myLocalDBSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='WLC'", "", DataViewRowState.CurrentRows);
                if (myrowDocNo != null)
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApplicationEntry").Caption += " - Next Possible Number : " + Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myDBSetting.GetServerTime());
                }
            }
            else if (myAction == DXSSAction.View)
            {
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApplicationEntry").Caption = "View Waive Late Charges";
            }
            else if (myAction == DXSSAction.Edit)
            {
                ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApplicationEntry").Caption = "Edit Waive Late Charges";

                luAgreement.ClientEnabled = false;
                luAgreement.BackColor = System.Drawing.Color.Transparent;
                luAgreement.ForeColor = System.Drawing.Color.Black;

                if (Convert.ToString(myWaiveLateChargesEntity.Status).Contains("APPROVE") || Convert.ToString(myWaiveLateChargesEntity.Status).Contains("REJECT"))
                {
                    seAmount.ReadOnly = true;
                    seAmount.BackColor = System.Drawing.Color.Transparent;

                    mmRemark1.ClientEnabled = false;
                    mmRemark1.BackColor = System.Drawing.Color.Transparent;
                    mmRemark1.ForeColor = System.Drawing.Color.Black;

                    btnSave.ClientEnabled = false;
                    btnSave.ForeColor = System.Drawing.Color.LightSlateGray;

                    btnApprove.ClientEnabled = false;
                    btnApprove.ForeColor = System.Drawing.Color.LightSlateGray;

                    btnReject.ClientEnabled = false;
                    btnReject.ForeColor = System.Drawing.Color.LightSlateGray;
                }
            }    
        }
        private void BindingMaster()
        {
            txtDocNo.Value = myWaiveLateChargesEntity.DocNo;
            txtStatus.Value = myWaiveLateChargesEntity.Status;
            deDocDate.Value = myWaiveLateChargesEntity.DocDate;
            luAgreement.Value = myWaiveLateChargesEntity.RefNo;
            txtDebitur.Value = myWaiveLateChargesEntity.Client;
            seAmount.Value = myWaiveLateChargesEntity.WaiveAmount;
            mmRemark1.Value = myWaiveLateChargesEntity.Remark1;
            if (myAction == DXSSAction.Edit)
            {
                LoadDetailAgreement(Convert.ToString(luAgreement.Value));
                gvDetail.DataBind();

                LoadDetailWaive(Convert.ToString(luAgreement.Value));
                gvDetail2.DataBind();
            }
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;

            myWaiveLateChargesEntity.DocNo = txtDocNo.Value;
            myWaiveLateChargesEntity.DocDate = deDocDate.Value;
            myWaiveLateChargesEntity.RefNo = luAgreement.Value;
            myWaiveLateChargesEntity.Client = txtDebitur.Value;
            myWaiveLateChargesEntity.WaiveAmount = seAmount.Value;
            myWaiveLateChargesEntity.Remark1 = mmRemark1.Value;

            if (myAction == DXSSAction.New)
            {
                myWaiveLateChargesEntity.CreatedBy = UserID;
                myWaiveLateChargesEntity.CreatedDateTime = myLocalDBSetting.GetServerTime();
                myWaiveLateChargesEntity.LastModifiedDateTime = myLocalDBSetting.GetServerTime();
            }
            if (saveAction == SaveAction.Approve)
            {
                myWaiveLateChargesEntity.Status = "APPROVE";
                myWaiveLateChargesEntity.SubmitBy = UserID;
                myWaiveLateChargesEntity.SubmitDateTime = myLocalDBSetting.GetServerTime();
            }
            if (saveAction == SaveAction.Reject)
            {
                myWaiveLateChargesEntity.Status = "REJECT";
                myWaiveLateChargesEntity.SubmitBy = UserID;
                myWaiveLateChargesEntity.SubmitDateTime = myLocalDBSetting.GetServerTime();
            }
            myWaiveLateChargesEntity.Save(UserID, "WLC", saveAction);
            return bSave;
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            return errorF;
        }
        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string urlsave = "";
            urlsave = "~/Transactions/Collections/WaiveLateCharges/FormWaiveLateChargesMaint.aspx";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlConnection SQLConn = new SqlConnection(dbsetting.ConnectionString);
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                case "ON_LOAD":
                    break;
                case "AGREEMENT_ONCHANGE":
                    LoadDetailAgreement(Convert.ToString(luAgreement.Value));
                    gvDetail.DataBind();

                    LoadDetailWaive(Convert.ToString(luAgreement.Value));
                    gvDetail2.DataBind();
                    break;
                case "SAVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to save this document?";
                    cplMain.JSProperties["cplblActionButton"] = "SAVE";
                    if (ErrorInField(out strmessageError, SaveAction.Submit))
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
                case "APPROVE_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to approve this document?";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    if (ErrorInField(out strmessageError, SaveAction.Approve))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "APPROVE":
                    Save(SaveAction.Approve);
                    cplMain.JSProperties["cpAlertMessage"] = "Document has been approved...";
                    cplMain.JSProperties["cplblActionButton"] = "APPROVE";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
                case "REJECT_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to reject this document?";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    if (ErrorInField(out strmessageError, SaveAction.Approve))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "REJECT":
                    Save(SaveAction.Reject);
                    cplMain.JSProperties["cpAlertMessage"] = "Document has been rejected...";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
            }
        }
        protected void luAgreement_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myAgreementTable;
        }
        protected void gvDetail_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myDetailTable;
        }
        protected void gvDetail_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }
        protected void gvDetail2_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myDetailTable2;
        }
        protected void gvDetail2_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }
    }
}