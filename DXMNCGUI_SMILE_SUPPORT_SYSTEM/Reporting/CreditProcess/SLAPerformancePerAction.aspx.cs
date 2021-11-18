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
    public partial class SLAPerformancePerAction : BasePage
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
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
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
            #region Query      
            sQuery = @"select A.*, B.DocNo, B.ClientName, LTRIM(LTRIM(A.Status)) as StatusTo, LTRIM(LTRIM(A.FromStatus)) as StatusFrom,
                        case when left( b.CIF,1) = 'C' then 'Perusahaan' else 'Perorangan' end as TipeDebitur,
                        Branch, ObjectPembiayaan, JenisPengikatan, SupplierName, NTF
                        from [dbo].[ApplicationHistory] A
                        inner join [dbo].[Application] B on A.DocKey = B.DocKey";
            #endregion
            sQuery = (myWhereString.Length > 0 ? sQuery + myWhereString : sQuery);
            sQuery += @" ORDER BY B.DocNo, A.TransDate";
            myTable = myLocalDBSetting.GetDataTable(sQuery, false, "");
            return myTable;
        }
        private string GetFilterString()
        {
            string myString = "";
            if (deFrom.Value != null && deTo.Value != null)
            {
                myString += @" WHERE CAST(A.TransDate AS DATE) between '" + deFrom.Date + "' AND '" + deTo.Date + "'";
            }
            return myString;
        }
        private void refreshdatagrid()
        {
            myWhereString = GetFilterString();
            myMainTable = LoadDataHeader();
            gvMain.DataSource = myMainTable;
            gvMain.DataBind();
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
                this.myApplicationDB = ApplicationDB.Create(myLocalDBSetting, dbsession);
                accessable();
                refreshdatagrid();
            }
        }
        protected void accessable()
        {
            if (!accessright.IsAccessibleByUserID(UserID, "CAN_REPORT_APP_SUM"))
            {

            }
        }
        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
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
        protected void gvMain_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridViewColumnDataEventArgs e)
        {

        }
    }
}