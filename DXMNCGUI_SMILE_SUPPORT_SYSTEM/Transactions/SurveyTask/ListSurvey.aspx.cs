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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.SurveyTask
{
    public partial class ListSurvey : BasePage
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


        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myDBSession = dbsession;
                myLocalDBSetting = localdbsetting;

                gvMain.DataBind();
            }
            if (!IsCallback)
            {

            }
        }

        protected void gvMain_Init(object sender, EventArgs e)
        {

        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = GetTableSurvey();
        }

        protected void gvMain_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            if (myrow != null)
            {
                var appNoValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                appNoValues.Set("AppNo", myrow["Application No"].ToString());

                var objValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                objValues.Set("Obj", myrow["Object"].ToString());

                updatedQueryString = "?" + appNoValues.ToString() + "&" + objValues.ToString();
                Response.Redirect("~/Transactions/SurveyTask/SurveyTask.aspx" + updatedQueryString);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/SurveyTask/SurveyTask.aspx");
        }

        DataTable GetTableSurvey()
        {
            string ssql = "exec sp_MNCL_SurveyTrxList";
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

        DataTable GetTableReportPDF(string appNo)
        {
            string ssql = "exec sp_MNCL_SurveyReportPDF '" + appNo + "'";
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

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {

        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            if (myrow != null)
            {
                gvAllDtl.DataSource = GetTableReportPDF(myrow["Application No"].ToString());
                gvAllDtl.DataBind();
                gvAllDtl.ExportPdfToResponse();
            }




        }
    }
}