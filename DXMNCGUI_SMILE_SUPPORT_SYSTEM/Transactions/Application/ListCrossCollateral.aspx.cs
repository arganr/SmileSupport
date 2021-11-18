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
    public partial class ListCrossCollateral : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlLocalDBSetting myLocalDBSetting
        {
            get { isValidLogin(); return (SqlLocalDBSetting)HttpContext.Current.Session["myLocalDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myLocalDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myHistTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myHistTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myHistTable" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myLocalDBSetting = localdbsetting;
                myDBSetting = dbsetting;
                myDBSession = dbsession;
                myMainTable = new DataTable();
                myHistTable = new DataTable();

                myMainTable = GetListCrossCol();
                gvMain.DataSource = myMainTable;
                gvMain.DataBind();

                //myHistTable = GetHistCrossCol("","");
                //gvHistory.DataSource = myHistTable;
                //gvHistory.DataBind();
            }
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            isValidLogin(false);
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;
            string ccode = "";
            string agreeno = "";

            switch (callbackParam[0].ToUpper())
            {
                case "HIST":
                    ccode = callbackParam[1].ToString();
                    agreeno = callbackParam[2].ToString();
                    myHistTable = GetHistCrossCol(ccode, agreeno);
                    break;
            }
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
        protected void gvHistory_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myHistTable;
        }

        DataTable GetListCrossCol()
        {
            string ssql = "select a.CODE,b.LSAGREE,b.NAME,b.ASSET_DESCS,ISNULL(c.USER_NAME,'') [USER_NAME], b.CRE_DATE [CRE_DATE] " +
                "from LS_CROSS_COLLATERAL_H a left join  LS_CROSS_COLLATERAL_D b on a.CODE = b.CODE left join MASTER_USER c on b.CRE_BY = c.USER_ID where b.LSAGREE is not null ORDER BY b.NAME";

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

        DataTable GetHistCrossCol(string id, string noagree)
        {
            //string ssql = "select * from LS_CROSS_COLLATERAL_APPROVAL_LOG where id_crosscol = '" + id + "' and no_agreement = '" + noagree + "'";
            string ssql = "select a.*, ISNULL(b.USER_NAME,'') [USER_NAME] from LS_CROSS_COLLATERAL_APPROVAL_LOG a left join MASTER_USER b on a.CRE_BY = b.USER_ID where a.id_crosscol = '" + id + "' ";

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

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/Application/InputCrossColateral.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                if (myrow != null)
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("CODE", myrow["CODE"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();
                    Response.Redirect("~/Transactions/Application/EditCrossCollateral.aspx" + updatedQueryString);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
        }
    }
}