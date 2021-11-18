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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application
{
    public partial class InputCrossColateral : BasePage
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
        protected DataTable myDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable NoAppDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["NoAppDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["NoAppDtTable" + this.ViewState["_PageID"]] = value; }
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
                myDtTable = new DataTable();
                NoAppDtTable = new DataTable();

                NoAppDtTable = GetListNoApp();
                gvNoApp.DataSource = NoAppDtTable;
                gvNoApp.DataBind();

                CreateColumn();
            }
            if (!IsCallback)
            {
                
            }
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            isValidLogin(false);
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;

            switch (callbackParam[0].ToUpper())
            {
                case "ADD":
                    string strNoApp = "";
                    strNoApp = gvNoApp.Text;

                    cplMain.JSProperties["cplblmessageError"] = "";
                    bool isExsit = checkExistNoApp(strNoApp);
                    if(strNoApp == "")
                    {
                        cplMain.JSProperties["cplblmessageError"] = "error";
                        cplMain.JSProperties["cpAlertMessage"] = "No App is Empty...";
                        cplMain.JSProperties["cplblActionButton"] = "OK";
                    }
                    else if (isExsit)
                    {
                        cplMain.JSProperties["cplblmessageError"] = "error";
                        cplMain.JSProperties["cpAlertMessage"] = "No App is Exist...";
                        cplMain.JSProperties["cplblActionButton"] = "OK";
                    }
                    else
                    {
                        InsertApp(strNoApp);
                    }
                    break;
                case "DELETE":
                    var delNoApp = callbackParam[1].ToString();
                    DataRow[] drr = myDtTable.Select("NoApp='" + delNoApp + "' ");
                    for (int i = 0; i < drr.Length; i++)
                        drr[i].Delete();
                    myDtTable.AcceptChanges();
                    break;
                case "SAVE":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    if(myDtTable.Rows.Count > 1)
                    {
                        string joinval = joinNoAgreement();
                        saveCrossColl(joinval);
                        cplMain.JSProperties["cpAlertMessage"] = "Crosscoll Success...";
                        cplMain.JSProperties["cplblActionButton"] = "OK";
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback("InputCrossColateral.aspx");
                    }
                    else
                    {
                        cplMain.JSProperties["cpAlertMessage"] = "Error: Total crosscol must be at least 2...";
                        cplMain.JSProperties["cplblActionButton"] = "OK";
                    }
                    break;
            }
        }

        protected void InsertApp(string strNoApp)
        {
            AddNewRow(strNoApp.ToString());
        }

        protected void gvNoApp_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = NoAppDtTable;
        }

        protected void gvTempData_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myDtTable;
        }

        DataTable GetListNoApp()
        {
            //string ssql = "select LSAGREE, NAME, C_NAME BRANCH, DISBURSEDT, MODULE " +
            //                "from LS_AGREEMENT a with(NOLOCK) inner join SYS_COMPANY b with(NOLOCK) on a.C_CODE = b.C_CODE " +
            //                "where CONTRACT_STATUS = 'GOLIVE' and MODULE not in ('6') and LSAGREE not in (select LSAGREE from LS_CROSS_COLLATERAL_D with(NOLOCK))";

            string ssql = "select a.LSAGREE, NAME, C_NAME BRANCH, DISBURSEDT, MODULE " +
                            "from LS_AGREEMENT a with(NOLOCK) inner join SYS_COMPANY b with(NOLOCK) on a.C_CODE = b.C_CODE " +
                            "left join LS_AGREEASSET c with(NOLOCK)on a.LSAGREE = c.LSAGREE " +
                            "where MODULE not in ('6') and ISNULL(c.STATUS,'') <> 'CU' " +
                            "and a.LSAGREE not in (select LSAGREE from LS_CROSS_COLLATERAL_D with(NOLOCK)) " +
                            "and NAME not in (select SUBSTRING(DESCRIPTION, 12, 999) [Name] from LS_CROSS_COLLATERAL_H with(NOLOCK)) ";

            DataTable resDT = new DataTable();
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(ssql);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;

                SqlDataReader reader = sqlCommand.ExecuteReader();
                resDT.Load(reader);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }

            return resDT;
        }

        void saveCrossColl(string value)
        {
            string ssql = "EXEC SP_MNCL_CREATE_CROSS_MASTER '" + value + "', '" + UserID + "'";
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(ssql);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;
                sqlCommand.ExecuteNonQuery();
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

        void CreateColumn()
        {
            DataTable dtVal = new DataTable();
            DataColumn appColumn = new DataColumn();
            appColumn.DataType = System.Type.GetType("System.String");
            appColumn.AllowDBNull = false;
            appColumn.Caption = "Application No";
            appColumn.ColumnName = "NoApp";
            appColumn.DefaultValue = "";

            DataColumn nameColumn = new DataColumn();
            nameColumn.DataType = System.Type.GetType("System.String");
            nameColumn.AllowDBNull = false;
            nameColumn.Caption = "Nama";
            nameColumn.ColumnName = "Name";
            nameColumn.DefaultValue = "";

            DataColumn cabColumn = new DataColumn();
            cabColumn.DataType = System.Type.GetType("System.String");
            cabColumn.AllowDBNull = false;
            cabColumn.Caption = "Cabang";
            cabColumn.ColumnName = "Branch";
            cabColumn.DefaultValue = "";

            // Add the column to the table.   
            myDtTable.Columns.Add(appColumn);
            myDtTable.Columns.Add(nameColumn);
            myDtTable.Columns.Add(cabColumn);
        }

        void AddNewRow(string value)
        {
            DataRow[] res = NoAppDtTable.Select("LSAGREE = '" + value + "'");

            DataRow dtRow;
            dtRow = myDtTable.NewRow();
            dtRow["NoApp"] = res[0][0];
            dtRow["Name"] = res[0][1];
            dtRow["Branch"] = res[0][2];

            myDtTable.Rows.Add(dtRow);
        }

        protected bool checkExistNoApp(string value)
        {
            bool res = false;
            DataRow[] dtRes = myDtTable.Select("NoApp = '" + value + "'");
            if(dtRes.Length > 0)
            {
                res = true;
            }
            else {
                res = false;
            }
            return res;
        }

        protected string joinNoAgreement()
        {
            string res = "";

            for (int i = 0; i < myDtTable.Rows.Count; i++)
            {
                DataRow row = myDtTable.Rows[i];
                res += "''" + row["NoApp"].ToString() + "''";
                if (i != myDtTable.Rows.Count - 1)
                {
                    res += ",";
                }
            }

            return res;

        }

    }
}