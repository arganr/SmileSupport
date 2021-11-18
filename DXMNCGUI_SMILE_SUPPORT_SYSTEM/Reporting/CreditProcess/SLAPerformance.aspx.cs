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
    public partial class SLAPerformance : BasePage
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
            sQuery = @"SELECT 
                        A.DocKey, A.DocNo, A.DocDate, B.Seq, A.Status,
                        ------------------------------------------------------------------------------------------------------------------------------------------ 						
                        (
	                        CASE WHEN 
		                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus=A.Status),0) < 1 
	                        THEN
		                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
	                        ELSE
		                        ISNULL((SELECT (SUM(DiffTime))/1440 FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus=A.Status),0)
	                        END
	                        +
	                        CASE WHEN
		                        ISNULL((SELECT COUNT(*) RowNumber FROM [dbo].[ApplicationHistory] WHERE DocKey=A.DocKey AND Status=A.Status),0) > 1
	                        THEN
		                        ISNULL((SELECT TOP 1(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey ORDER BY TransDate DESC),0)
	                        ELSE
		                        0
	                        END
                        )
                        AS LamaProses,
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        (
	                        CASE WHEN
			                        ISNULL((SELECT COUNT(*) RowNumber FROM [dbo].[ApplicationHistory] WHERE DocKey=A.DocKey AND Status=A.Status AND FromStatus <> ''),0) > 1
		                        THEN
			                        'ON RETURN/CAM'
		                        ELSE
			                        'NORMAL'
		                        END 
                        )
                        AS IsReturn,
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        ( CASE WHEN A.OnHold = 'F' THEN 'NO' ELSE 'YES' END ) AS OnHold,
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        A.Branch, A.ClientName, A.NTF, A.ObjectPembiayaan, A.JenisPengikatan,
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='BRH MGR'),0) < 1 AND A.Status = 'BRH MGR' 
		                        THEN
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='BRH MGR'),0)
						                        END AS 'BRH_MGR',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='APP IN'),0) < 1 AND A.Status = 'APP_IN' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='APP IN'),0) 
						                        END AS 'APP_IN',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CA'),0) < 1 AND A.Status = 'CA' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CA'),0) 
						                        END AS 'CA',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='HOLDING'),0) < 1 AND A.Status = 'HOLDING' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='HOLDING'),0) 
						                        END AS 'HOLDING',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='AM'),0) < 1 AND A.Status = 'AM' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='AM'),0) 
						                        END AS 'AM',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CA HEAD'),0) < 1 AND A.Status = 'CA HEAD' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CA HEAD'),0) 
						                        END AS 'CA_HEAD',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='SIRKULASI KK'),0) < 1 AND A.Status = 'SIRKULASI_KK' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='SIRKULASI KK'),0) 
						                        END AS 'SIRKULASI_KK',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CMO'),0) < 1 AND A.Status = 'CMO' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CMO'),0) 
						                        END AS 'CMO',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='COO'),0) < 1 AND A.Status = 'COO' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='COO'),0) 
						                        END AS 'COO',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CFO'),0) < 1 AND A.Status = 'CFO' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CFO'),0) 
						                        END AS 'CFO',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CEO'),0) < 1 AND A.Status = 'CEO' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CEO'),0) 
						                        END AS 'CEO',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CA SKK'),0) < 1 AND A.Status = 'CA_SKK' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CA SKK'),0) 
						                        END AS 'CA_SKK',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CEK SKK'),0) < 1 AND A.Status = 'CEK_SKK' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CEK SKK'),0) 
						                        END AS 'CEK_SKK',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='LEMBAR KONTROL'),0) < 1 AND A.Status = 'LEMBAR_KONTROL' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='LEMBAR KONTROL'),0) 
						                        END AS 'LEMBAR_KONTROL',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='PREPARE CETAK KONTRAK'),0) < 1 AND A.Status = 'PREPARE_CETAK_KONTRAK' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='PREPARE CETAK KONTRAK'),0) 
						                        END AS 'PREPARE_CETAK_KONTRAK',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='REVIEW KONTRAK'),0) < 1 AND A.Status = 'REVIEW_KONTRAK' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='REVIEW KONTRAK'),0) 
						                        END AS 'REVIEW_KONTRAK',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CETAK KONTRAK'),0) < 1 AND A.Status = 'CETAK_KONTRAK' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CETAK KONTRAK'),0) 
						                        END AS 'CETAK_KONTRAK',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='SIGNING'),0) < 1 AND A.Status = 'SIGNING' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='SIGNING'),0) 
						                        END AS 'SIGNING',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CEK KONTRAK'),0) < 1 AND A.Status = 'CEK_KONTRAK' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CEK KONTRAK'),0) 
						                        END AS 'CEK_KONTRAK',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CHECKLIST'),0) < 1 AND A.Status = 'CHECKLIST' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CHECKLIST'),0) 
						                        END AS 'CHECKLIST',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='PREPARE PO'),0) < 1 AND A.Status = 'PREPARE_PO' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='PREPARE PO'),0) 
						                        END AS 'PREPARE_PO',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='PO'),0) < 1 AND A.Status = 'PO' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='PO'),0)	
						                        END AS 'PO',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='TAGIHAN'),0) < 1 AND A.Status = 'TAGIHAN' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='TAGIHAN'),0) 
						                        END AS 'TAGIHAN',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='PREPARE DISBURSE'),0) < 1 AND A.Status = 'PREPARE_DISBURSE' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='PREPARE DISBURSE'),0) 
						                        END AS 'PREPARE_DISBURSE',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CREDAM'),0) < 1 AND A.Status = 'CREDAM' 
		                        THEN 
			                        (SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) 
				                        ELSE 
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='CREDAM'),0) 
						                        END AS 'CREDAM',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        CASE WHEN 
	                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='FINANCE'),0) < 1 AND A.Status = 'FINANCE' 
		                        THEN 
			                        ISNULL((SELECT SUM(DATEDIFF (DAYOFYEAR, TransDate, GETDATE())) * (24) * (60) FROM [dbo].[ApplicationHistory] WHERE Status=A.Status AND DocKey = A.DocKey) ,0)
				                        ELSE
					                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='FINANCE'),0)
						                        END AS 'FINANCE',
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        ISNULL((SELECT (SUM(DiffTime)) FROM [dbo].[ApplicationHistory] WHERE DocKey = A.DocKey AND FromStatus='DONE'),0) AS 'DONE'
                        ------------------------------------------------------------------------------------------------------------------------------------------
                        FROM [dbo].[Application] A 
                        LEFT JOIN [dbo].[ApplicationWorkflowScheme] B ON A.Status = B.StateDescription";
            #endregion
            sQuery = (myWhereString.Length > 0 ? sQuery + myWhereString : sQuery);
            myTable = myLocalDBSetting.GetDataTable(sQuery, false, "");
            return myTable;
        }
        private string GetFilterString()
        {
            string myString = "";
            if (deFrom.Value != null && deTo.Value != null)
            {
                myString += @" WHERE CAST(A.DocDate AS DATE) between '" + deFrom.Date + "' AND '" + deTo.Date + "'";
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
        protected void gvMain_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            myMainTable = LoadDataHeader();
            gvMain.DataBind();
        }

        protected void gvMain_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "TIME_TO_YES")
            {
                decimal dTimeToYes = 0;
                dTimeToYes = Convert.ToInt32(e.GetListSourceFieldValue("APP_IN"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("CA"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("HOLDING"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("AM"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("CA_HEAD"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("SIRKULASI_KK"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("CMO"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("COO"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("CFO"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("CEO"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("CA_SKK"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("CEK_SKK"));
                dTimeToYes += Convert.ToInt32(e.GetListSourceFieldValue("LEMBAR_KONTROL"));
                e.Value = dTimeToYes;
            }
            if (e.Column.FieldName == "TIME_TO_DISBURSE")
            {
                decimal dTimeToDisburse = 0;
                dTimeToDisburse = Convert.ToInt32(e.GetListSourceFieldValue("PREPARE_CETAK_KONTRAK"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("REVIEW_KONTRAK"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("CETAK_KONTRAK"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("SIGNING"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("CEK_KONTRAK"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("CHECKLIST"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("PREPARE_PO"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("PO"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("TAGIHAN"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("PREPARE_DISBURSE"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("CREDAM"));
                dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("FINANCE"));
                //dTimeToDisburse += Convert.ToInt32(e.GetListSourceFieldValue("DONE"));
                e.Value = dTimeToDisburse;
            }
        }
    }
}