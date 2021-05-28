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
    public partial class EditCrossCollateral : BasePage
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
        protected DataTable ccDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["ccDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["ccDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable newDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["newDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["newDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable tmpDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["tmpDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["tmpDtTable" + this.ViewState["_PageID"]] = value; }
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
                ccDtTable = new DataTable();
                newDtTable = new DataTable();
                tmpDtTable = new DataTable();

                ccDtTable = GetListCC();
                gvCrossColl.DataSource = ccDtTable;
                gvCrossColl.DataBind();

                newDtTable = GetListNoApp("");
                gvNewItem.DataSource = newDtTable;
                gvNewItem.DataBind();

                //loadDataExist("");
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
                    strNoApp = gvNewItem.Text;

                    cplMain.JSProperties["cplblmessageError"] = "";
                    
                    if (gvCrossColl.Text == "")
                    {
                        cplMain.JSProperties["cplblmessageError"] = "error";
                        cplMain.JSProperties["cpAlertMessage"] = "Cross Coll is Empty...";
                        cplMain.JSProperties["cplblActionButton"] = "OK";
                    }
                    else
                    {
                        bool isExsit = checkExistNoApp(strNoApp);
                        if (strNoApp == "")
                        {
                            cplMain.JSProperties["cplblmessageError"] = "error";
                            cplMain.JSProperties["cpAlertMessage"] = "Agreement is Empty...";
                            cplMain.JSProperties["cplblActionButton"] = "OK";
                        }
                        else if (isExsit)
                        {
                            cplMain.JSProperties["cplblmessageError"] = "error";
                            cplMain.JSProperties["cpAlertMessage"] = "Agreement is Exist...";
                            cplMain.JSProperties["cplblActionButton"] = "OK";
                        }
                        else
                        {
                            AddNewRow(strNoApp.ToString());
                        }
                    }
                    break;
                case "LOAD":
                    var ccode = callbackParam[1].ToString();
                    tmpDtTable = GetDataExist(ccode);
                    newDtTable = GetListNoApp(ccode);

                    break;
                case "DELETE":
                    var delNoApp = callbackParam[1].ToString();
                    DataRow[] drr = tmpDtTable.Select("LSAGREE='" + delNoApp + "' ");
                    for (int i = 0; i < drr.Length; i++)
                        drr[i].Delete();
                    tmpDtTable.AcceptChanges();
                    break;
                case "SAVE":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    if (tmpDtTable.Rows.Count > 1)
                    {
                        string joinval = joinNoAgreement();
                        string crosscode = gvCrossColl.Text;
                        updateCrossColl(joinval, crosscode);
                        cplMain.JSProperties["cpAlertMessage"] = "Update Crosscoll Success...";
                        cplMain.JSProperties["cplblActionButton"] = "OK";
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback("EditCrossCollateral.aspx");
                    }
                    else
                    {
                        cplMain.JSProperties["cpAlertMessage"] = "Error: Total crosscol must be at least 2...";
                        cplMain.JSProperties["cplblActionButton"] = "OK";
                    }
                    break;
            }
        }

        protected void gvCrossColl_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = ccDtTable;
        }

        protected void gvNewItem_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = newDtTable;
        }

        protected void gvTempData_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = tmpDtTable;
        }

        DataTable GetListCC()
        {
            //string ssql = "select CODE, DESCRIPTION from LS_CROSS_COLLATERAL_H";
            string ssql = "select a.CODE, a.DESCRIPTION [CROSSCOLL], ISNULL(b.LSAGREE,'') [LSAGREE], b.NAME [DETAIL] " +
                            "from LS_CROSS_COLLATERAL_H a left join LS_CROSS_COLLATERAL_D b on a.CODE = b.CODE";


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

        DataTable GetListNoApp(string ccode)
        {
            string ssql = "select LSAGREE, NAME, C_NAME BRANCH, DISBURSEDT, MODULE, DESCS " +
                            "from LS_AGREEMENT a with(NOLOCK) inner join SYS_COMPANY b with(NOLOCK) on a.C_CODE = b.C_CODE " +
                            "where CONTRACT_STATUS = 'GOLIVE' and MODULE not in ('6') and LSAGREE not in (select LSAGREE from LS_CROSS_COLLATERAL_D with(NOLOCK) where CODE <> '" + ccode + "')";

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

        DataTable GetDataExist(string value)
        {
            string ssql = "select LSAGREE, NAME, ASSET_DESCS from LS_CROSS_COLLATERAL_D where code = '" + value + "'";

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

        protected bool checkExistNoApp(string value)
        {
            bool res = false;
            DataRow[] dtRes = tmpDtTable.Select("LSAGREE = '" + value + "'");
            if (dtRes.Length > 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }

        void AddNewRow(string value)
        {
            DataRow[] res = newDtTable.Select("LSAGREE = '" + value + "'");

            DataRow dtRow;
            dtRow = tmpDtTable.NewRow();
            dtRow["LSAGREE"] = res[0][0];
            dtRow["NAME"] = res[0][1];
            dtRow["ASSET_DESCS"] = res[0][5];

            tmpDtTable.Rows.Add(dtRow);
        }

        protected string joinNoAgreement()
        {
            string res = "";

            for (int i = 0; i < tmpDtTable.Rows.Count; i++)
            {
                DataRow row = tmpDtTable.Rows[i];
                res += "''" + row["LSAGREE"].ToString() + "''";
                if (i != tmpDtTable.Rows.Count - 1)
                {
                    res += ",";
                }
            }

            return res;

        }

        void updateCrossColl(string lsagree, string ccode)
        {
            string ssql = "EXEC SP_MNCL_UPDATE_CROSS_MASTER '" + lsagree + "', '" + UserID + "', '" + ccode + "'";
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
    }
}