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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.ReleaseDocument
{
    public partial class RequestReleaseDoc : BasePage
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
        protected DataTable myCashierTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myCashierTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myCashierTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DXSSAction myAction
        {
            get { isValidLogin(false); return (DXSSAction)HttpContext.Current.Session["myAction" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAction" + this.ViewState["_PageID"]] = value; }
        }
        protected string strKey
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]] = value; }
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
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                myHeaderTable = new DataTable();
                myAgreementTable = new DataTable();
                myCashierTable = new DataTable();

                myAction = DXSSAction.New;

                
                //ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApproval").Visible = false;

                if (this.Request.QueryString["Key"] != null)
                {
                    strKey = this.Request.QueryString["Key"].ToString();
                    string actionType = this.Request.QueryString["Action"].ToString();
                    switch (actionType)
                    {
                        case "Edit":
                            myAction = DXSSAction.Edit;
                            break;
                        case "Approval":
                            myAction = DXSSAction.Approve;
                            break;
                        case "View":
                            myAction = DXSSAction.View;
                            break;
                    }
                    
                }
                setuplookupedit();
                BindingMaster();
                Accessable();
            }
            if (!IsCallback)
            {

            }
        }
        private void setuplookupedit()
        {
            GetAgreementTable();
            luAgreement.DataBind();

            GetCashierTable();
            luCashierNo.DataBind();

            if (myAction == DXSSAction.New)
            {
                deReleaseDate.Value = DateTime.Now;
                txtStatus.Value = "REQ";
            }
        }
        private void BindingMaster()
        {
            if(myAction != DXSSAction.New)
            {
                DataTable dtReleaseDoc = getViewReleaseDoc(strKey);
                if(dtReleaseDoc.Rows.Count > 0)
                {
                    DataRow dr = dtReleaseDoc.Rows[0];
                    luAgreement.Value = dr["AgreementNo"];
                    txtStatus.Value = dr["ReleaseStat"];
                    txtDaysDiff.Value = dr["DaysDiff"];
                    deReleaseDate.Value = dr["ReleaseDate"];
                    deLastPaidDate.Value = dr["LastInsPaidDate"];
                    txtFeePenitipan.Value = dr["FeePenitipanDoc"];
                    cbWaiveReason.SelectedItem = cbWaiveReason.Items.FindByValue(dr["WaiveReason"].ToString());
                    txtWaiveFeePenitipan.Value = dr["WaiveFeePenitipanDoc"];
                    
                    if(dr["WaiveReason"].ToString() != "Not Waive")
                    {
                        txtWaiveFeePenitipan.ClientEnabled = true;
                    }

                    if (dr["ApproveDecision"] != null && dr["ApproveDecision"] != DBNull.Value)
                    {
                        txtApprovalNote.Value = dr["ApproveNote"];
                        txtApproveBy.Value = dr["ApproveByName"];
                        txtApproveDecision.Value = dr["ApproveDecision"];
                        deApproveDate.Value = dr["ApproveDate"];
                    }

                    if (dr["WaiveApproveDecision"] != null && dr["WaiveApproveDecision"] != DBNull.Value)
                    {
                        txtApproveWaiveAmt.Value = dr["WaiveApproveFeeAmt"];
                        txtWaiveApprovalNote.Value = dr["WaiveApproveNote"];
                        txtWaiveApproveBy.Value = dr["WaiveApproveByName"];
                        txtWaiveApprovalDecision.Value = dr["WaiveApproveDecision"];
                        deWaiveApproveDate.Value = dr["WaiveApproveDate"];
                    }

                    if (dr["ReleaseDocBy"] != null && dr["ReleaseDocBy"] != DBNull.Value)
                    {
                        luCashierNo.Value = dr["CashierNo"];
                        txtReleaseNote.Value = dr["ReleaseNote"];
                        txtReleaseExecBy.Value = dr["ReleaseDocByName"];
                        deReleaseExecDate.Value = dr["ReleaseDocDate"];

                        if(dr["WaiveDocFile"] != null && dr["WaiveDocFile"] != DBNull.Value)
                        {
                            byte[] fileDoc = (byte[])dr["WaiveDocFile"];
                            myFs = new MemoryStream(fileDoc);
                            resultExtension = dr["WaiveDocExt"].ToString();
                        }
                    }
                }
            }
            
        }
        private void Accessable()
        {
            btnApprove.Visible = false;
            btnReject.Visible = false;

            ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApproval").Visible = false;
            ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApprovalDetail").Visible = false;

            ASPxFormLayout.FindItemOrGroupByName("LayoutGroupWaiveApproval").Visible = false;
            ASPxFormLayout.FindItemOrGroupByName("LayoutGroupWaiveApprovalDetail").Visible = false;

            ASPxFormLayout.FindItemOrGroupByName("LayoutGroupReleaseDoc").Visible = false;
            ASPxFormLayout.FindItemOrGroupByName("LayoutGroupReleaseDetail").Visible = false;

            txtName.ClientEnabled = false;
            txtName.BackColor = System.Drawing.Color.Transparent;
            txtName.ForeColor = System.Drawing.Color.Black;

            txtStatus.ClientEnabled = false;
            txtStatus.BackColor = System.Drawing.Color.Transparent;
            txtStatus.ForeColor = System.Drawing.Color.Black;

            deReleaseDate.ClientEnabled = false;
            deReleaseDate.BackColor = System.Drawing.Color.Transparent;
            deReleaseDate.ForeColor = System.Drawing.Color.Black;

            deLastPaidDate.ClientEnabled = false;
            deLastPaidDate.BackColor = System.Drawing.Color.Transparent;
            deLastPaidDate.ForeColor = System.Drawing.Color.Black;

            txtDaysDiff.ClientEnabled = false;
            txtDaysDiff.BackColor = System.Drawing.Color.Transparent;
            txtDaysDiff.ForeColor = System.Drawing.Color.Black;

            txtFeePenitipan.ClientEnabled = false;
            txtFeePenitipan.BackColor = System.Drawing.Color.Transparent;
            txtFeePenitipan.ForeColor = System.Drawing.Color.Black;

            if (myAction == DXSSAction.View)
            {
                luAgreement.ClientEnabled = false;
                luAgreement.BackColor = System.Drawing.Color.Transparent;
                luAgreement.ForeColor = System.Drawing.Color.Black;

                txtFeePenitipan.ClientEnabled = false;
                txtFeePenitipan.BackColor = System.Drawing.Color.Transparent;
                txtFeePenitipan.ForeColor = System.Drawing.Color.Black;

                cbWaiveReason.ClientEnabled = false;
                cbWaiveReason.BackColor = System.Drawing.Color.Transparent;
                cbWaiveReason.ForeColor = System.Drawing.Color.Black;

                txtWaiveFeePenitipan.ClientEnabled = false;
                txtWaiveFeePenitipan.BackColor = System.Drawing.Color.Transparent;
                txtWaiveFeePenitipan.ForeColor = System.Drawing.Color.Black;

                btnSave.ClientEnabled = false;
                btnSave.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f0f0");

                txtApprovalNote.ClientEnabled = false;
                txtApprovalNote.BackColor = System.Drawing.Color.Transparent;
                txtApprovalNote.ForeColor = System.Drawing.Color.Black;

                txtApproveWaiveAmt.ClientEnabled = false;
                txtApproveWaiveAmt.BackColor = System.Drawing.Color.Transparent;
                txtApproveWaiveAmt.ForeColor = System.Drawing.Color.Black;

                txtWaiveApprovalNote.ClientEnabled = false;
                txtWaiveApprovalNote.BackColor = System.Drawing.Color.Transparent;
                txtWaiveApprovalNote.ForeColor = System.Drawing.Color.Black;

                luCashierNo.ClientEnabled = false;
                luCashierNo.BackColor = System.Drawing.Color.Transparent;
                luCashierNo.ForeColor = System.Drawing.Color.Black;

                txtReleaseNote.ClientEnabled = false;
                txtReleaseNote.BackColor = System.Drawing.Color.Transparent;
                txtReleaseNote.ForeColor = System.Drawing.Color.Black;

                if(txtStatus.Text == "WAIVE REQ" || txtStatus.Text == "REJECTED")
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApproval").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApprovalDetail").Visible = true;
                }

                if (txtStatus.Text == "APPROVED" || txtStatus.Text == "REJECTED")
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApproval").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApprovalDetail").Visible = true;

                    if (cbWaiveReason.Text != "Not Waive")
                    {
                        ASPxFormLayout.FindItemOrGroupByName("LayoutGroupWaiveApproval").Visible = true;
                        ASPxFormLayout.FindItemOrGroupByName("LayoutGroupWaiveApprovalDetail").Visible = true;
                    }
                }

                if(txtStatus.Text == "RELEASE")
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApproval").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApprovalDetail").Visible = true;

                    if (cbWaiveReason.Text != "Not Waive")
                    {
                        ASPxFormLayout.FindItemOrGroupByName("LayoutGroupWaiveApproval").Visible = true;
                        ASPxFormLayout.FindItemOrGroupByName("LayoutGroupWaiveApprovalDetail").Visible = true;
                    }

                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupReleaseDoc").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupReleaseDetail").Visible = true;

                    UploadCtrl.ClientVisible = false;
                    btnDownload.ClientVisible = false;
                    

                    if(myFs != null)
                    {
                        btnView.ClientVisible = true;
                    }
                }
            }

            if (myAction == DXSSAction.Edit)
            {
                luAgreement.ClientEnabled = false;
                luAgreement.BackColor = System.Drawing.Color.Transparent;
                luAgreement.ForeColor = System.Drawing.Color.Black;
            }

            if (myAction == DXSSAction.Approve)
            {
                luAgreement.ClientEnabled = false;
                luAgreement.BackColor = System.Drawing.Color.Transparent;
                luAgreement.ForeColor = System.Drawing.Color.Black;

                txtFeePenitipan.ClientEnabled = false;
                txtFeePenitipan.BackColor = System.Drawing.Color.Transparent;
                txtFeePenitipan.ForeColor = System.Drawing.Color.Black;

                cbWaiveReason.ClientEnabled = false;
                cbWaiveReason.BackColor = System.Drawing.Color.Transparent;
                cbWaiveReason.ForeColor = System.Drawing.Color.Black;

                txtWaiveFeePenitipan.ClientEnabled = false;
                txtWaiveFeePenitipan.BackColor = System.Drawing.Color.Transparent;
                txtWaiveFeePenitipan.ForeColor = System.Drawing.Color.Black;

                btnApprove.Visible = true;
                btnReject.Visible = true;

                btnSave.ClientEnabled = false;
                btnSave.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f0f0");

                if(txtStatus.Text == "REQ")
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApproval").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApprovalDetail").Visible = false;
                }

                if (txtStatus.Text == "WAIVE REQ")
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupWaiveApproval").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupWaiveApprovalDetail").Visible = false;

                    txtApproveWaiveAmt.Text = txtWaiveFeePenitipan.Text;
                }

                if (txtStatus.Text == "APPROVED")
                {
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApproval").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupApprovalDetail").Visible = true;

                    txtApprovalNote.ClientEnabled = false;
                    txtApprovalNote.BackColor = System.Drawing.Color.Transparent;
                    txtApprovalNote.ForeColor = System.Drawing.Color.Black;
                    
                    if (cbWaiveReason.Text != "Not Waive")
                    {
                        ASPxFormLayout.FindItemOrGroupByName("LayoutGroupWaiveApproval").Visible = true;
                        ASPxFormLayout.FindItemOrGroupByName("LayoutGroupWaiveApprovalDetail").Visible = true;

                        txtApproveWaiveAmt.ClientEnabled = false;
                        txtApproveWaiveAmt.BackColor = System.Drawing.Color.Transparent;
                        txtApproveWaiveAmt.ForeColor = System.Drawing.Color.Black;

                        txtWaiveApprovalNote.ClientEnabled = false;
                        txtWaiveApprovalNote.BackColor = System.Drawing.Color.Transparent;
                        txtWaiveApprovalNote.ForeColor = System.Drawing.Color.Black;
                    }

                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupReleaseDoc").Visible = true;
                    ASPxFormLayout.FindItemOrGroupByName("LayoutGroupReleaseDetail").Visible = false;

                    if(Convert.ToInt32(txtFeePenitipan.Value) == 0)
                    {
                        luCashierNo.ClientEnabled = false;
                        luCashierNo.BackColor = System.Drawing.Color.Transparent;
                        luCashierNo.ForeColor = System.Drawing.Color.Black;
                    }

                    if (Convert.ToInt32(txtApproveWaiveAmt.Value) == 0)
                    {
                        UploadCtrl.ClientEnabled = false;
                    }

                    UploadCtrl.ClientVisible = true;
                    btnView.ClientVisible = false;

                    btnReject.ClientEnabled = false;
                    btnReject.ForeColor = default(System.Drawing.Color);
                    btnReject.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f0f0");

                    btnApprove.Text = "Release";
                    
                }
            }
        }

        protected void luAgreement_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myAgreementTable;
        }

        protected void GetAgreementTable()
        {
            //string ssql = @"select a.LSAGREE, c.C_NAME, a.NAME, a.LSPERIOD TENOR, a.RENTAL INSTALLMENT, d.LASTPAID,
            //                case when DATEDIFF(day,d.LASTPAID,getdate()) > 30 then DATEDIFF(day,d.LASTPAID,getdate()) * 15000 else 0 end FEE
            //                from LS_AGREEMENT a with(NOLOCK)
            //                left join LS_ASSETVEHICLE b with(NOLOCK) on a.LSAGREE = b.LSAGREE 
            //                left join SYS_COMPANY c with(NOLOCK) on a.C_CODE = c.C_CODE
            //                inner join (SELECT MAX(VALUEDATE) [LASTPAID], LSAGREE FROM LS_LEDGERRENTAL WHERE PAYMENT < 0 GROUP BY LSAGREE) d on a.LSAGREE = d.LSAGREE";

            string ssql = @"exec dbo.spSmileSupport_GetAgreementListReleaseDoc";
            myAgreementTable = myDBSetting.GetDataTable(ssql, false);
        }

        protected void GetCashierTable()
        {
            string ssql = @"exec dbo.spSmileSupport_GetCashierNoListReleaseDoc ?";
            myCashierTable = myDBSetting.GetDataTable(ssql, false, luAgreement.Text);
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string urlsave = "";
            urlsave = "~/Transactions/CreditProcess/ReleaseDocument/ReleaseDocList.aspx";
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

                    object obj = localdbsetting.ExecuteScalar("SELECT COUNT(*) FROM UpdateSPPHNo WHERE AgreementNo=? AND Status = 'NEW' AND ISNULL(NoSPPH,'') <> ''", paramValue2);
                    if (obj != null || obj != DBNull.Value)
                    {
                        if (myAction == DXSSAction.New)
                        {
                            if (Convert.ToInt32(obj) >= 1)
                            { cplMain.JSProperties["cplblmessageError"] = "Duplikat nomor agreement, silahkan proses terlebih dahulu data sebelumnya."; return; }
                        }
                    }

                    obj = localdbsetting.ExecuteScalar("SELECT COUNT(*) FROM UpdateSPPHNo WHERE AgreementNo=? AND Status = 'APPROVE'", paramValue2);
                    if (obj != null || obj != DBNull.Value)
                    {
                        if (Convert.ToInt32(obj) >= 1)
                        { cplMain.JSProperties["cplblmessageError"] = "Duplikat nomor agreement, No SPPH sudah pernah diupdate sebelumnya."; return; }
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
                    Approve(SaveAction.Approve);
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
                    Reject(SaveAction.Reject);
                    cplMain.JSProperties["cpAlertMessage"] = "Transaction has been reject...";
                    cplMain.JSProperties["cplblActionButton"] = "REJECT";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
            }
        }

        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            if(myAction == DXSSAction.New)
            {
                InsertRequest();
            }else
            {
                UpdateData();
            }
            
            return bSave;
        }

        private bool Approve(SaveAction saveAction)
        {
            bool bSave = true;

            if(txtStatus.Text == "REQ")
            {
                if(cbWaiveReason.Text != "Not Waive")
                {
                    UpdateApproval("APPROVED", "WAIVE REQ");
                }else
                {
                    UpdateApproval("APPROVED", "APPROVED");
                }
            }

            if (txtStatus.Text == "WAIVE REQ")
            {
                UpdateWaiveApproval("APPROVED", "APPROVED");
            }

            if (txtStatus.Text == "APPROVED")
            {
                if (luCashierNo.ClientEnabled)
                {
                    UpdateReleaseExecution(luCashierNo.Text, "RELEASE");
                }else
                {
                    UpdateReleaseExecution("", "RELEASE");
                }

                if (UploadCtrl.ClientEnabled)
                {
                    UploadWaiveDocument();
                }
            }

            return bSave;
        }

        private bool Reject(SaveAction saveAction)
        {
            bool bSave = true;

            if (txtStatus.Text == "REQ")
            {
                UpdateApproval("REJECTED", "REJECTED");
            }

            if (txtStatus.Text == "WAIVE REQ")
            {
                UpdateWaiveApproval("REJECTED", "REJECTED");
            }

            return bSave;
        }

        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";

            if(myAction == DXSSAction.New)
            {
                if(luAgreement.Text == "")
                {
                    errorF = true;
                    strmessageError = "Agreement No. is empty !";
                }else
                {
                    if (IsAgreementExist())
                    {
                        errorF = true;
                        strmessageError = "This Agreement No. is already input !";
                    }
                }
            }

            if(myAction == DXSSAction.New || myAction == DXSSAction.Edit)
            {
                if (cbWaiveReason.Text != "Not Waive" && Convert.ToInt32(txtWaiveFeePenitipan.Value) == 0)
                {
                    errorF = true;
                    strmessageError = "Please input Waive biaya penitipan !";
                }

                if (Convert.ToInt32(txtWaiveFeePenitipan.Value) > Convert.ToInt32(txtFeePenitipan.Value))
                {
                    errorF = true;
                    strmessageError = "Waive request is more than fee amount !";
                }
            }

            if(myAction == DXSSAction.Approve)
            {
                if(txtStatus.Text == "REQ")
                {
                    if(txtApprovalNote.Text == "")
                    {
                        errorF = true;
                        strmessageError = "Please input approval note !";
                    }
                }

                if (txtStatus.Text == "WAIVE REQ")
                {
                    if (txtWaiveApprovalNote.Text == "")
                    {
                        errorF = true;
                        strmessageError = "Please input approval note !";
                    }
                    if(txtApproveWaiveAmt.Text == "")
                    {
                        errorF = true;
                        strmessageError = "Please input waive amount !";
                    }else
                    {
                        if (Convert.ToInt32(txtApproveWaiveAmt.Value) > Convert.ToInt32(txtWaiveFeePenitipan.Value))
                        {
                            errorF = true;
                            strmessageError = "Approval Waive amount is more than request waive amount !";
                        }
                    }
                }

                if(txtStatus.Text == "APPROVED")
                {
                    string returnValidate = IsValidateBySP();
                    if (returnValidate == "")
                    {
                        if (txtReleaseNote.Text == "")
                        {
                            errorF = true;
                            strmessageError = "Please input release note !";
                        }

                        if (Convert.ToInt32(txtFeePenitipan.Value) > 0 && Convert.ToInt32(txtApproveWaiveAmt.Value) < Convert.ToInt32(txtFeePenitipan.Value) && luCashierNo.Text == "")
                        {
                            errorF = true;
                            strmessageError = "Please input Cashier No !";
                        }

                        if (Convert.ToInt32(txtApproveWaiveAmt.Value) > 0 && myFs == null)
                        {
                            errorF = true;
                            strmessageError = "Please input waive document !";
                        }
                    }
                    else
                    {
                        errorF = true;
                        strmessageError = returnValidate;
                    }
                }
            }

            return errorF;


        }

        protected bool IsAgreementExist()
        {
            object objLSAGREE = myLocalDBSetting.ExecuteScalar("select AgreementNo from dbo.trxReleaseDoc where ReleaseStat <> 'REJECTED' and AgreementNo=?", luAgreement.Text);
            if (objLSAGREE != null && objLSAGREE != DBNull.Value)
            {
                return true;
            }else
            {
                return false;
            }
        }

        protected string IsValidateBySP()
        {
            string resError = "";

            using (SqlConnection conn = new SqlConnection(myDBSetting.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("dbo.spSmileSupport_ValidateReleaseDocument", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // set up the parameters
                cmd.Parameters.Add("@LSAGREE", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@CASHIERNUM", SqlDbType.VarChar, 20);

                cmd.Parameters["@LSAGREE"].Value = luAgreement.Text;
                cmd.Parameters["@CASHIERNUM"].Value = luCashierNo.Text;

                conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    resError = ex.Message;
                    conn.Close();
                }
            }

            return resError;

        }

        protected void UploadWaiveDocument()
        {
            BinaryReader br = new BinaryReader(myFs);
            Byte[] bytes = br.ReadBytes((Int32)myFs.Length);

            UpdateWaiveDocFile(bytes);
        }

        protected void InsertRequest()
        {
            string ssql = @"INSERT INTO trxReleaseDoc (AgreementNo, ReleaseStat, ReleaseDate, LastInsPaidDate, FeePenitipanDoc, WaiveReason, WaiveFeePenitipanDoc, CreatedBy, CreatedDate, ClientName, DaysDiff)
                VALUES(@AgreementNo, @ReleaseStat, @ReleaseDate, @LastInsPaidDate, @FeePenitipanDoc, @WaiveReason, @WaiveFeePenitipanDoc, @CreatedBy, GETDATE(), @ClientName, @DaysDiff)";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(ssql);
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@AgreementNo", SqlDbType.VarChar);
            sqlParameter1.Value = luAgreement.Text;
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@ReleaseStat", SqlDbType.VarChar);
            sqlParameter2.Value = txtStatus.Text;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@ReleaseDate", SqlDbType.DateTime);
            sqlParameter3.Value = deReleaseDate.Date;
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@LastInsPaidDate", SqlDbType.DateTime);
            sqlParameter4.Value = deLastPaidDate.Date;
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@FeePenitipanDoc", SqlDbType.Decimal);
            sqlParameter5.Value = txtFeePenitipan.Text;
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@WaiveReason", SqlDbType.VarChar);
            sqlParameter6.Value = cbWaiveReason.SelectedItem.Text;
            sqlParameter6.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@WaiveFeePenitipanDoc", SqlDbType.Decimal);
            sqlParameter7.Value = txtWaiveFeePenitipan.Text;
            sqlParameter7.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.VarChar);
            sqlParameter8.Value = UserID;
            sqlParameter8.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@ClientName", SqlDbType.VarChar);
            sqlParameter9.Value = txtName.Text;
            sqlParameter9.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@DaysDiff", SqlDbType.Int);
            sqlParameter10.Value = txtDaysDiff.Text;
            sqlParameter10.Direction = ParameterDirection.Input;


            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        protected void UpdateData()
        {
            string ssql = "UPDATE trxReleaseDoc SET FeePenitipanDoc=?, WaiveReason=?, WaiveFeePenitipanDoc=?, ModdifiedBy=?, ModdifiedDate=Getdate() WHERE DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, txtFeePenitipan.Text, cbWaiveReason.Text, txtWaiveFeePenitipan.Text, UserID, strKey);
        }

        protected void UpdateApproval(string strDecision, string strStatus)
        {
            string ssql = "UPDATE trxReleaseDoc SET ReleaseStat=?, ApproveDecision=?, ApproveNote=?, ApproveBy=?, ApproveDate=Getdate() WHERE DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, strStatus, strDecision, txtApprovalNote.Text, UserID, strKey);
        }

        protected void UpdateWaiveApproval(string strDecision, string strStatus)
        {
            string ssql = "UPDATE trxReleaseDoc SET ReleaseStat=?, WaiveApproveFeeAmt=?, WaiveApproveDecision=?, WaiveApproveNote=?, WaiveApproveBy=?, WaiveApproveDate=Getdate() WHERE DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, strStatus, txtApproveWaiveAmt.Text, strDecision, txtWaiveApprovalNote.Text, UserID, strKey);
        }

        protected void UpdateReleaseExecution(string strCashierNo, string strStatus)
        {
            string ssql = "UPDATE trxReleaseDoc SET ReleaseStat=?, CashierNo=?, ReleaseNote=?, ReleaseDocBy=?, ReleaseDocDate=Getdate() WHERE DocKey=?";
            myLocalDBSetting.ExecuteNonQuery(ssql, strStatus, strCashierNo, txtReleaseNote.Text, UserID, strKey);
        }

        protected void UpdateWaiveDocFile(byte[] btDocFile)
        {
            string ssql = @"UPDATE trxReleaseDoc SET WaiveDocFile=@WaiveDocFile, WaiveDocExt=@WaiveDocExt WHERE DocKey=@DocKey";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(ssql);
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@WaiveDocFile", SqlDbType.VarBinary);
            sqlParameter1.Value = btDocFile;
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@WaiveDocExt", SqlDbType.VarChar);
            sqlParameter2.Value = resultExtension;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.VarChar);
            sqlParameter3.Value = strKey;
            sqlParameter3.Direction = ParameterDirection.Input;


            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        protected DataTable getViewReleaseDoc(string docKey)
        {
            string ssql = @"select 
	            a.*, 
	            b.USER_NAME [ApproveByName],
	            c.USER_NAME [WaiveApproveByName],
	            d.USER_NAME [ReleaseDocByName]
            from dbo.trxReleaseDoc a
            left join dbo.master_user b on a.ApproveBy = b.USER_ID and b.IS_ACTIVE_FLAG = 1
            left join dbo.master_user c on a.WaiveApproveBy = c.USER_ID and c.IS_ACTIVE_FLAG = 1
            left join dbo.master_user d on a.ReleaseDocBy = d.USER_ID and d.IS_ACTIVE_FLAG = 1
            where a.DocKey=?";

            DataTable res = new DataTable();
            res = myLocalDBSetting.GetDataTable(ssql, false, docKey);

            return res;
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

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadAtt();
        }

        protected void DownloadFromStream()
        {
            BinaryReader br = new BinaryReader(myFs);
            Byte[] bytes = br.ReadBytes((Int32)myFs.Length);

            byte[] FileBuffer = bytes;
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.AppendHeader("content-disposition", string.Format("attachment;FileName=\"{0}\"", "WaiveDocument_" + luAgreement.Text + resultExtension));
                Response.BinaryWrite(FileBuffer);
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            DownloadFromStream();
        }

        protected void luCashierNo_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myCashierTable;
        }
    }
}