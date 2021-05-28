using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.WorkingList
{
    public partial class MyWorkingListMaint : BasePage
    {
        string SqlQuery = string.Empty;
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(true); return (SqlDBSetting)HttpContext.Current.Session["HomeSqlDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["HomeSqlDBSetting" + this.ViewState["_PageID"]] = value; }
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
        protected DataTable myWorklisttable
        {
            get { isValidLogin(true); return (DataTable)HttpContext.Current.Session["myWorklisttable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myWorklisttable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myAssigntable
        {
            get { isValidLogin(true); return (DataTable)HttpContext.Current.Session["myAssigntable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAssigntable" + this.ViewState["_PageID"]] = value; }
        }
        protected Int32 iVisibleIndex
        {
            get { isValidLogin(true); return (Int32)HttpContext.Current.Session["iVisibleIndex" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["iVisibleIndex" + this.ViewState["_PageID"]] = value; }
        }
        protected Int32 iWorklistID
        {
            get { isValidLogin(true); return (Int32)HttpContext.Current.Session["iWorklistID" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["iWorklistID" + this.ViewState["_PageID"]] = value; }
        }
        protected ApplicationDB myApplicationDB
        {
            get { isValidLogin(true); return (ApplicationDB)HttpContext.Current.Session["myApplicationDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApplicationDB" + this.ViewState["_PageID"]] = value; }
        }
        protected ApplicationEntity myApplicationEntity
        {
            get { isValidLogin(true); return (ApplicationEntity)HttpContext.Current.Session["myApplicationEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApplicationEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myApproveAccesstable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myApproveAccesstable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myApproveAccesstable" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(true);
            if (!Page.IsPostBack)
            {
                string sQuery = "", sWhere = "";
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                #region LoadMainTable
                myApproveAccesstable = new DataTable();
                myApproveAccesstable = LoadApproveAccess(dbsession.LoginUserID);
                sQuery = @"SELECT A.*, A.CreatedBy + ' - ' + B.USER_NAME AS FULLNAME 
                            FROM [dbo].[Application] 
                            A inner join MASTER_USER B on B.USER_ID=A.CreatedBy 
                            WHERE A.OnHold='F' AND Status <> 'DONE' ";
                int ilastitem = myApproveAccesstable.Rows.Count - 1;
                for (int i = 0; i < myApproveAccesstable.Rows.Count; i++)
                {
                    DataRow myrow = myApproveAccesstable.Rows[i];
                    if (i != ilastitem)
                    { 
                        DataTable dtWhereList = new DataTable();
                        dtWhereList = localdbsetting.GetDataTable("SELECT StateDescription FROM [dbo].[ApplicationWorkflowAccess] WHERE GroupAccessCode=?",false, myrow["GROUP_CODE"].ToString());
                        int ilastwhereitem = dtWhereList.Rows.Count - 1;
                        for (int j = 0; j < dtWhereList.Rows.Count; j++)
                        {
                            DataRow mywhererow = dtWhereList.Rows[j];
                            if (j != ilastwhereitem)
                            {
                                sWhere += "'" + mywhererow["StateDescription"].ToString() + "',";
                            }
                            else
                            {
                                sWhere += "'" + mywhererow["StateDescription"].ToString() + "'";
                            }
                        }
                    }
                    else
                    {
                        DataTable dtWhereList = new DataTable();
                        dtWhereList = localdbsetting.GetDataTable("SELECT StateDescription FROM [dbo].[ApplicationWorkflowAccess] WHERE GroupAccessCode=?", false, myrow["GROUP_CODE"].ToString());
                        int ilastwhereitem = dtWhereList.Rows.Count - 1;
                        for (int j = 0; j < dtWhereList.Rows.Count; j++)
                        {
                            DataRow mywhererow = dtWhereList.Rows[j];
                            if (j != ilastwhereitem)
                            {
                                sWhere += "'" + mywhererow["StateDescription"].ToString() + "',";
                            }
                            else
                            {
                                sWhere += "'" + mywhererow["StateDescription"].ToString() + "'";
                            }
                        }
                    }
                }
                if (sWhere.Length > 1)
                {
                    sQuery += @" AND Status IN (" + sWhere + ")";
                }
                sQuery += @" ORDER BY DocDate";
                myWorklisttable = new DataTable();
                myWorklisttable = localdbsetting.GetDataTable(sQuery, false);
                gvWorkingList.DataSource = myWorklisttable;
                gvWorkingList.DataBind();
                #endregion

                myAssigntable = new DataTable();
                myAssigntable = myDBSetting.GetDataTable(@"SELECT DISTINCT A.USER_ID, UPPER(USER_NAME) AS USER_NAME FROM MASTER_USER A 
                                                            INNER JOIN MASTER_USER_COMPANY_GROUP B ON B.USER_ID=A.USER_ID 
                                                            INNER JOIN MASTER_GROUP C ON C.USERGROUP=B.GROUP_CODE 
                                                            WHERE A.IS_ACTIVE_FLAG=1
                                                            ORDER BY UPPER(USER_NAME)", false, "");
                luPIC.DataSource = myAssigntable;
                luPIC.DataBind();

                iWorklistID = -1;
                iVisibleIndex = -1;
                if (Request.QueryString["Page"] != null)
                {
                    string strPage = "";
                    strPage = Request.QueryString["Page"].ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Access denied to open " + strPage + "...');", true);
                }
                if (this.Request.QueryString["Source"] != null)
                {
                    int indexVal = gvWorkingList.FindVisibleIndexByKeyValue(this.Request.QueryString["Source"]);
                    gvWorkingList.FocusedRowIndex = indexVal;
                }
                else
                {
                    gvWorkingList.FocusedRowIndex = -1;
                }
            }
        }
        protected DataTable LoadWorkListTable(string strStatus)
        {
            DataTable mytable = new DataTable();
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlQuery = @"SELECT A.*, A.CreatedBy + ' - ' + B.USER_NAME AS FULLNAME FROM [dbo].[Application] 
                            A inner join MASTER_USER B on B.USER_ID=A.CreatedBy 
                            WHERE A.OnHold='F' AND Status=@Status\n";
            using (SqlCommand cmdheader = new SqlCommand(SqlQuery, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdheader);
                cmdheader.CommandType = CommandType.Text;
                cmdheader.Parameters.Add("@Status", SqlDbType.NVarChar);
                cmdheader.Parameters["@Status"].Value = strStatus;
                adapter.Fill(mytable);
            }
            return mytable;
        }
        public DataTable LoadApproveAccess(string sID)
        {
            string strQuery = @"SELECT DISTINCT B.GROUP_CODE FROM MASTER_USER A 
                                INNER JOIN MASTER_USER_COMPANY_GROUP B ON B.USER_ID=A.USER_ID 
                                INNER JOIN MASTER_GROUP C ON C.USERGROUP=B.GROUP_CODE 
                                WHERE A.IS_ACTIVE_FLAG=1 AND A.USER_ID=?";
            myApproveAccesstable = myDBSetting.GetDataTable(strQuery, false, sID);
            return myApproveAccesstable;
        }
        protected void gvWorkingList_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myWorklisttable;
        }
        protected void gvWorkingList_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvWorkingList_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] callbackParam = e.Parameters.ToString().Split(';');
            (sender as ASPxGridView).JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            object paramName = callbackParam[0].ToUpper();
            object paramValue = callbackParam[1].ToUpper();
            switch (callbackParam[0].ToUpper())
            {
                case "INDEX":
                    iVisibleIndex = System.Convert.ToInt16(paramValue);
                    (sender as ASPxGridView).JSProperties["cpVisibleIndex"] = iVisibleIndex;
                    object obj = (sender as ASPxGridView).GetRowValues(iVisibleIndex, "ID");
                    if (obj != null && obj != DBNull.Value)
                    {
                        iWorklistID = System.Convert.ToInt32(obj);
                    }
                    break;
                case "REFRESH":
                    try
                    {
                        isValidLogin();
                        myWorklisttable = LoadWorkListTable(dbsession.LoginUserID);
                        gvWorkingList.DataSource = myWorklisttable;
                        gvWorkingList.DataBind();
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        return;
                    }
                    break;
            }
        }
        protected void gvWorkingList_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridViewColumnDataEventArgs e)
        {

        }
        protected void gvWorkingList_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "btnShow")
            {
                try
                {
                    DataRow myrow = (sender as ASPxGridView).GetDataRow((sender as ASPxGridView).FocusedRowIndex);
                    if (myrow != null)
                    {
                        object obj = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "ID");
                        if (obj != null && obj != DBNull.Value)
                        {
                            iWorklistID = System.Convert.ToInt32(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }
        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string updatedQueryString = "";
            cplMain.JSProperties["cpType"] = "";
            long docKey = -1;
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            switch (callbackParam[0].ToUpper())
            {
                case "APPROVAL":
                    bool validF = false;
                    try
                    {
                        if (myLocalDBSetting.ExecuteScalar("SELECT DocKey FROM [dbo].[Application] WHERE DocKey=?", Convert.ToInt64(callbackParam[1])) == null) return;
                        docKey = Convert.ToInt64(callbackParam[1]);
                        if (docKey > 0)
                        {
                            validF = true;
                            try
                            {
                                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                                nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                                updatedQueryString = "?" + nameValues.ToString();
                                myApplicationDB = ApplicationDB.Create(myLocalDBSetting, dbsession);
                                myApplicationEntity = myApplicationDB.Edit(docKey, DXSSAction.Edit);
                                ASPxWebControl.RedirectOnCallback("~/Transactions/CreditProcess/ApplicationEntry.aspx" + updatedQueryString);
                            }
                            catch (Exception ex)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                                return;
                            }
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                        validF = false;
                    }
                    cplMain.JSProperties["cpValidF"] = validF;
                    cplMain.JSProperties["cpDocKey"] = docKey;
                    break;
                case "ASSIGN":
                    SaveAssign(SaveAction.Assign);
                    cplMain.JSProperties["cpAlertMessage"] = "";
                    cplMain.JSProperties["cplblActionButton"] = "ASSIGN";
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback(Request.Url.AbsoluteUri);
                    break;
                case "ASSIGN_CONFIRM":
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to assign this credit process ?";
                    cplMain.JSProperties["cplblActionButton"] = "ASSIGN";
                    break;
            }
        }
        private bool SaveAssign(SaveAction saveAction)
        {
            bool bSave = true;
            saveAction = SaveAction.Assign;

            DataRow myrow = gvWorkingList.GetDataRow(gvWorkingList.FocusedRowIndex);
            try
            {
                this.myApplicationDB = ApplicationDB.Create(myLocalDBSetting, myDBSession);
                myApplicationEntity = this.myApplicationDB.View(Convert.ToInt32(myrow["DocKey"]));
            }
            catch
            { }

            myApplicationEntity.SaveAssign(UserName, luPIC.Text, saveAction);
            return bSave;
        }
        protected void luPIC_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myAssigntable;
        }
    }
}