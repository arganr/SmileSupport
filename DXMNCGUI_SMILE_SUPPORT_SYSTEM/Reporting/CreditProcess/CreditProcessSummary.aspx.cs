using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess
{
    public partial class CreditProcessSummary : BasePage
    {
        DateTime datenow = DateTime.Now;
        public List<int> MergedIndexList = new List<int>();
        protected SqlLocalDBSetting myLocalDBSetting
        {
            get { isValidLogin(); return (SqlLocalDBSetting)HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]] = value; }
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
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDetailTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myMainTable2
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable2" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable2" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDetailTable2
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailTable2" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailTable2" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myStatusTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myStatusTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myStatusTable" + this.ViewState["_PageID"]] = value; }
        }
        protected string myWhereString
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["myWhereString" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myWhereString" + this.ViewState["_PageID"]] = value; }
        }

        DataTable LoadDataHeader()
        {
            string sQuery = "";
            DataTable myTable = new DataTable();
            sQuery = @"SELECT
                            A.Status, COUNT(A.DocNo) AS AppsCount,SUM(A.OTR) AS TotalOTR, SUM(A.DP) AS TotalDP, SUM(A.NTF) AS TotalNTF
                            FROM Application A
                            LEFT JOIN [dbo].[ApplicationWorkflowScheme] C ON A.Status = C.StateDescription
                            WHERE A.Status NOT IN ('CANCELLED','REJECTED') AND (A.Status IS NULL OR A.Status <>'')";
            sQuery = (myWhereString.Length > 0 ? sQuery + myWhereString : sQuery);
            sQuery += @" GROUP BY A.Status, C.Seq 
                        ORDER BY C.Seq";
            myTable = myLocalDBSetting.GetDataTable(sQuery, false, "");
            return myTable;
        }
        DataTable LoadDataDetail(string sParam)
        {
            string sQuery = "";
            DataTable myTable = new DataTable();
            sQuery = @"SELECT 
                        A.Status, A.Branch, A.DocNo, A.DocDate, A.ObjectPembiayaan, A.JenisPengikatan, A.OTR, A.DP, A.NTF, A.ClientName
                        FROM Application A
                        LEFT JOIN [dbo].[ApplicationWorkflowScheme] C ON A.Status = C.StateDescription
                        WHERE A.Status=?";
            sQuery = (myWhereString.Length > 0 ? sQuery + myWhereString : sQuery);
            sQuery += @" ORDER BY A.DocDate DESC";
            myTable = myLocalDBSetting.GetDataTable(sQuery, false, sParam);
            return myTable;
        }
        DataTable LoadDataHeader2()
        {
            string sQuery = "";
            DataTable myTable = new DataTable();
            sQuery = @"SELECT
                            A.Status, COUNT(A.DocNo) AS AppsCount,SUM(A.OTR) AS TotalOTR, SUM(A.DP) AS TotalDP, SUM(A.NTF) AS TotalNTF
                            FROM Application A
                            LEFT JOIN [dbo].[ApplicationWorkflowScheme] C ON A.Status = C.StateDescription
                            WHERE (A.Status = 'REJECTED' OR A.Status='CANCELLED') AND (A.Status IS NULL OR A.Status <>'')";
            sQuery = (myWhereString.Length > 0 ? sQuery + myWhereString : sQuery);
            sQuery += @" GROUP BY A.Status, C.Seq 
                        ORDER BY C.Seq";
            myTable = myLocalDBSetting.GetDataTable(sQuery, false, "");
            return myTable;
        }
        DataTable LoadDataDetail2(string sParam)
        {
            string sQuery = "";
            DataTable myTable = new DataTable();
            sQuery = @"SELECT 
                        A.Status, A.Branch, A.DocNo, A.DocDate, A.ObjectPembiayaan, A.JenisPengikatan, A.OTR, A.DP, A.NTF, A.ClientName
                        FROM Application A
                        LEFT JOIN [dbo].[ApplicationWorkflowScheme] C ON A.Status = C.StateDescription
                        WHERE A.Status=?";
            sQuery = (myWhereString.Length > 0 ? sQuery + myWhereString : sQuery);
            sQuery += @" ORDER BY A.DocDate DESC";
            myTable = myLocalDBSetting.GetDataTable(sQuery, false, sParam);
            return myTable;
        }
        DataTable LoadStatusTable()
        {
            string sQuery = "";
            DataTable myTable = new DataTable();
            sQuery = @"SELECT StateDescription FROM [dbo].[ApplicationWorkflowScheme] ORDER BY Seq";
            myTable = myLocalDBSetting.GetDataTable(sQuery, false, "");
            return myTable;
        }
        private string GetFilterString()
        {
            string myString = "";
            if (deFrom.Value != null && deTo.Value != null)
            {
                myString += @" AND CAST(A.DocDate AS DATE) between '" + deFrom.Date + "' AND '" + deTo.Date + "'";
            }
            if (cbStatus.Value != null)
            {
                myString += @" AND A.Status='" + cbStatus.Value + "'";
            }
            return myString;
        }
        private void refreshdatagrid()
        {
            myMainTable = LoadDataHeader();
            myMainTable2 = LoadDataHeader2();
            gvMain.DataBind();
            gvMain2.DataBind();
        }
        private void setEnabledButton()
        {

        }
        private void setPageControl()
        {
            myStatusTable = LoadStatusTable();
            cbStatus.DataSource = myStatusTable;
            cbStatus.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
                myMainTable = new DataTable();
                myMainTable2 = new DataTable();
                this.myApplicationDB = ApplicationDB.Create(myLocalDBSetting, dbsession);
                gvMain.DataBind();
                myWhereString = GetFilterString();
                accessable();
                refreshdatagrid();
                setEnabledButton();
                setPageControl();
            }
        }
        protected void accessable()
        {
            if (!accessright.IsAccessibleByUserID(UserID, "CAN_REPORT_APP_SUM"))
            {

            }
        }
        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string[] callbackParam = e.Parameter.ToString().Split(';');
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            string updatedQueryString = "?" + nameValues.ToString();
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1];
            string strmessageError = string.Empty;
            switch (callbackParam[0].ToUpper())
            {
                case "INQUIRY":
                    myWhereString = GetFilterString();
                    break;
            }
        }
        protected void gvMain_Init(object sender, EventArgs e)
        {

        }
        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvMain_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            myMainTable = LoadDataHeader();
            gvMain.DataBind();
        }
        protected void gvDetail_Init(object sender, EventArgs e)
        {
            
        }
        protected void gvDetail_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myDetailTable;
        }
        protected void gvDetail_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void gvDetail_BeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["Status"] = (sender as ASPxGridView).GetMasterRowKeyValue();
            myDetailTable = LoadDataDetail(Session["Status"].ToString());
        }
        protected void gvDetail_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {

        }      
        protected void gvMain2_Init(object sender, EventArgs e)
        {

        }
        protected void gvMain2_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable2;
        }
        protected void gvMain2_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvMain2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            myMainTable2 = LoadDataHeader2();
            gvMain2.DataBind();
        }
        protected void gvDetail2_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myDetailTable2;
        }
        protected void gvDetail2_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvDetail2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void gvDetail2_BeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["Status"] = (sender as ASPxGridView).GetMasterRowKeyValue();
            myDetailTable2 = LoadDataDetail2(Session["Status"].ToString());
        }
        protected void gvDetail2_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {

        }
        protected void cbStatus_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxComboBox).DataSource = myStatusTable;
        }
    }
}