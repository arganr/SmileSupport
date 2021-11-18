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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.Syariah
{
    public partial class SyariahCreditProcessList : BasePage
    {
        public List<int> MergedIndexList = new List<int>();
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
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
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myHistoryTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myHistoryTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myHistoryTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myCommentTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myCommentTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myCommentTable" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
                myMainTable = new DataTable();
                myHistoryTable = new DataTable();
                myCommentTable = new DataTable();

                myMainTable = this.myDBSetting.GetDataTable(@"SELECT DISTINCT
                    A.APPLICNO,
                    (SELECT TOP 1 STEP FROM MNCL_APP_TIME_STAMP WHERE MNCL_APP_TIME_STAMP.APPLICNO=A.APPLICNO ORDER BY ID DESC) STATUS,
                    ISNULL(D.USER_NAME, 'API') CRE_BY,
                    A.CRE_DATE,
                    C.C_NAME As BRANCH,
                    A.NAME DEBITUR_NAME,
                    A.CAMPAIGN_DESC,
                    A.CAMPAIGN_TENOR_DESC TENOR,
                    ISNULL(E.AMTLEASE,0) NTF
                    FROM LS_APPLICATION A
                    INNER JOIN MNCL_APP_TIME_STAMP B ON A.APPLICNO = B.APPLICNO
                    INNER JOIN SYS_COMPANY C ON A.C_CODE = C.C_CODE
                    LEFT JOIN MASTER_USER D ON A.CRE_BY = D.USER_ID
                    LEFT JOIN LS_AGREEMENT E ON A.APPLICNO = E.APPLICNO
                    WHERE A.CAMPAIGN_CODE='PCKGHJREG' AND A.CRE_DATE >= '2021-01-01'
                    ORDER BY A.CRE_DATE DESC", false);
                gvMain.DataBind();
                gvMain.FocusedRowIndex = -1;
            }
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                case "REFRESH":
                    break;
                case "PROCEED":
                    SaveAssign();
                    cplMain.JSProperties["cpAlertMessage"] = "";
                    cplMain.JSProperties["cplblActionButton"] = "PROCEED";
                    ASPxWebControl.RedirectOnCallback(Request.Url.AbsoluteUri);
                    break;
                case "PROCEED_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to proceed this application ?";
                    cplMain.JSProperties["cplblActionButton"] = "PROCEED";
                    break;
                case "SAVE_COMMENT":
                    SaveComment();
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
            }
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;

            return errorF;
        }
        private bool SaveAssign()
        {
            bool bSave = true;
            string myNextStep = "";
            int imyDiffTime = 0;
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);

            switch (myrow["STATUS"].ToString())
            {
                case "LEGAL-SIGNING CONTRACT":
                    myNextStep = "LEGAL-CHECKLIST";
                    break;
                case "LEGAL-CHECKLIST":
                    myNextStep = "LEGAL-FIRST PAYMENT";
                    break;
                case "LEGAL-FIRST PAYMENT":
                    myNextStep = "PEMBUKAAN BTH";
                    break;
                case "PEMBUKAAN BTH":
                    myNextStep = "READY-TO-GOLIVE";
                    break;
                case "DISBURSE":
                    myNextStep = "BPIH";
                    break;
                case "BPIH":
                    myNextStep = "SPPH";
                    break;
                case "SPPH":
                    myNextStep = "DONE";
                    break;
            }

            try
            {
                DateTime Mydate = myDBSetting.GetServerTime();
                DateTime Mydate2 = myDBSetting.GetServerTime();
                //Mydate2 = Convert.ToDateTime(myrow["CRE_DATE"]);
                Mydate2 = Convert.ToDateTime(myDBSetting.ExecuteScalar("SELECT TOP 1 CRE_DATE FROM MNCL_APP_TIME_STAMP WHERE APPLICNO=? ORDER BY CRE_DATE DESC", myrow["APPLICNO"]));
                imyDiffTime = Convert.ToInt32((Mydate - Mydate2).TotalMinutes);

                SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[MNCL_APP_TIME_STAMP] (APPLICNO, STEP, TIME_STAMP, DURATION, CRE_DATE, CRE_BY, NOTES) VALUES (@APPLICNO, @STEP, @TIME_STAMP, @DURATION, @CRE_DATE, @CRE_BY, @NOTES)");
                sqlCommand.Connection = myconn;
                try
                {
                    myconn.Open();
                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@APPLICNO", SqlDbType.NVarChar, 20);
                    sqlParameter1.Value = Convert.ToString(myrow["APPLICNO"]);
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@STEP", SqlDbType.NVarChar, 50);
                    sqlParameter2.Value = myNextStep;
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@TIME_STAMP", SqlDbType.DateTime);
                    sqlParameter3.Value = Mydate;
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@DURATION", SqlDbType.Int);
                    sqlParameter4.Value = imyDiffTime;
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@CRE_DATE", SqlDbType.DateTime);
                    sqlParameter5.Value = Mydate;
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@CRE_BY", SqlDbType.NVarChar, 15);
                    sqlParameter6.Value = this.UserID;
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@NOTES", SqlDbType.NVarChar, 500);
                    sqlParameter7.Value = mmDecisionNote.Value;
                    sqlParameter7.Direction = ParameterDirection.Input;
                    sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                catch (HttpUnhandledException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
            catch
            { }
            return bSave;
        }
        private void SaveComment()
        {
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            try
            {
                SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ApplicationSyariahCommentHistory] (DocNo, CommentBy, CommentNote, CommentDate) VALUES (@DocNo, @CommentBy, @CommentNote, @CommentDate)");
                sqlCommand.Connection = myconn;
                try
                {
                    myconn.Open();
                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocNo", SqlDbType.NVarChar, 20);
                    sqlParameter1.Value = Convert.ToString(myrow["APPLICNO"]);
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@CommentBy", SqlDbType.NVarChar, 50);
                    sqlParameter2.Value = this.UserName;
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@CommentNote", SqlDbType.NVarChar);
                    sqlParameter3.Value = mmComment.Value;
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@CommentDate", SqlDbType.DateTime);
                    sqlParameter4.Value = myDBSetting.GetServerTime();
                    sqlParameter4.Direction = ParameterDirection.Input;
                    sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                catch (HttpUnhandledException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
            catch
            { }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        protected void gvMain_Init(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();

                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
            }
        }
        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvMain_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void gvMain_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {

        }
        protected void gvMain_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.ButtonID == "GridbtnProceed")
            {
                if (Convert.ToString(gvMain.GetRowValues(e.VisibleIndex, "STATUS")) == "APPIN")
                    e.Enabled = false;
                if (Convert.ToString(gvMain.GetRowValues(e.VisibleIndex, "STATUS")) == "READY-TO-GOLIVE")
                    e.Enabled = false;
                if (Convert.ToString(gvMain.GetRowValues(e.VisibleIndex, "STATUS")) == "CREDIT-COMMITTEE")
                    e.Enabled = false;
                if (Convert.ToString(gvMain.GetRowValues(e.VisibleIndex, "STATUS")) == "CANCEL")
                    e.Enabled = false;
                if (Convert.ToString(gvMain.GetRowValues(e.VisibleIndex, "STATUS")) == "DONE")
                    e.Enabled = false;
                if (Convert.ToString(gvMain.GetRowValues(e.VisibleIndex, "STATUS")) == "")
                    e.Enabled = false;
            }
        }

        protected void gvHistory_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myHistoryTable;
        }
        protected void gvHistory_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            string[] callbackParam = e.Parameters.ToString().Split(';');
            gridView.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1].ToUpper();
            switch (callbackParam[0].ToUpper())
            {
                case "LOAD_HISTORY":
                    myHistoryTable.Clear();
                    myHistoryTable = this.myDBSetting.GetDataTable(@"SELECT
                        A.*, ISNULL((SELECT USER_NAME FROM MASTER_USER WHERE USER_ID = A.CRE_BY),'SYSTEM') AS USER_NAME
                        FROM MNCL_APP_TIME_STAMP A
                        INNER JOIN LS_APPLICATION C ON A.APPLICNO = C.APPLICNO
                        WHERE C.PRODUCT_FACILITY_CODE='112' AND A.APPLICNO=? ORDER BY ID DESC", false, paramValue.ToString());
                    gvHistory.DataBind();
                    break;
            }
        }
        protected void gvHistory_CustomCellMerge(object sender, ASPxGridViewCustomCellMergeEventArgs e)
        {
            if (e.Column.FieldName == "USER_NAME")
            {
                e.Handled = true;
                if ((string)e.Value1 == (string)e.Value2)
                {
                    MergedIndexList.Add(e.RowVisibleIndex1);
                    e.Merge = true;
                }
            }
            else
            { e.Handled = true; e.Merge = false; }
        }
        protected void gvHistory_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "STEP")
            {
                //if (e.Value == (object)("READY-DISBURSE"))
                //{
                //    e.DisplayText = "READY TO GOLIVE";
                //}
                //if (e.Value == (object)("DISBURSE"))
                //{
                //    e.DisplayText = "GOLIVE";
                //}
            }
        }

        protected void gvComment_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myCommentTable;
        }
        protected void gvComment_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            string[] callbackParam = e.Parameters.ToString().Split(';');
            gridView.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1].ToUpper();
            switch (callbackParam[0].ToUpper())
            {
                case "LOAD_COMMENT":
                    myCommentTable = myLocalDBSetting.GetDataTable("SELECT * FROM [dbo].[ApplicationSyariahCommentHistory] WHERE DocNo=? ORDER BY CommentDate Desc", false, paramValue.ToString());
                    gvComment.DataBind();
                    break;
            }
        }
    }
}