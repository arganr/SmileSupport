using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SLIK;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.eKYC.SLIKChecking
{
    public partial class MasterClientSLIKCheck : BasePage
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
        protected DataTable clientDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["clientDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["clientDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable tmpDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["tmpDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["tmpDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable debDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["debDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["debDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable detailDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["detailDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["detailDtTable" + this.ViewState["_PageID"]] = value; }
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
                clientDtTable = new DataTable();
                tmpDtTable = new DataTable();
                debDtTable = new DataTable();
                detailDtTable = new DataTable();

                clientDtTable = GetListClient();
                gvClient.DataSource = clientDtTable;
                gvClient.DataBind();

                tmpDtTable = GetTempData("");
                gvTempData.DataSource = tmpDtTable;
                gvTempData.DataBind();

                debDtTable = GetDebData("",0);
                gvDebSLIK.DataSource = debDtTable;
                gvDebSLIK.DataBind();

                detailDtTable = GetDetailData("");
                gvDetailSLIK.DataSource = detailDtTable;
                gvDetailSLIK.DataBind();

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
            string ccode = "";
            int ctype = 0;

            switch (callbackParam[0].ToUpper())
            {
                case "LOAD":
                    detailDtTable.Clear();
                    debDtTable.Clear();
                    ccode = callbackParam[1].ToString();
                    tmpDtTable = GetTempData(ccode);
                    var isAuth = GetUserRoleAuth();
                    //var isAuth = 1;
                    if (isAuth > 0)
                    {
                        cplMain.JSProperties["cpEnableBtn"] = "enable";
                    }
                    break;
                case "DEB":
                    detailDtTable.Clear();
                    ccode = callbackParam[1].ToString();
                    ctype = Convert.ToInt32(callbackParam[2].ToString());
                    debDtTable = GetDebData(ccode, ctype);
                    break;
            }
        }



        DataTable GetListClient()
        {
            string ssql = "select " +
                            "CLIENT[CIF], " +
                            "NAME[Client Name], " +
                            "INKTP[No KTP], " +
                            "INBORNDT[Tgl Lahir], " +
                            "INBORNPLC[Tempat Lahir], " +
                            "LTRIM(RTRIM(ISNULL(ADDRESS1, '')))[Alamat] " +
                        "from[dbo].[SYS_CLIENT] " +
                        "order by [Client Name]";

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

        DataTable GetTempData(string value)
        {
            string ssql = "exec [dbo].[spMNCL_getClientSLIK] '" + value + "'";

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

        DataTable GetDebData(string value, int id)
        {
            string ssql = "select TRXID, ISNULL(REFID,'') [REFID], CLIENT, SID_PENGURUSID, NAME, KTP, NPWP, DOB, CRE_BY, CRE_DATE, CUSTTYPE, REQSTATUS from [dbo].[trxRequestSLIK] " +
                "where CLIENT = '" + value + "' AND SID_PENGURUSID=" + id.ToString();

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

        DataTable GetDetailData(string value)
        {
            string ssql = "select *, ROUND(JANGKA,1) [RoundJangka], ROUND(SISATENOR,1) [RoundSisaTenor] from trxFinancingCreditSLIK where REFID = '" + value + "'";

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

        Int32 GetUserRoleAuth()
        {
            int countAuth = 0;
            //string ssql = "select Count(1) Auth from MASTER_USER_COMPANY_GROUP where GROUP_CODE like'%HO-CRD%' and USER_ID = '" + UserID + "'";
            string ssql = "select Count(1) Auth from MASTER_USER_COMPANY_GROUP where GROUP_CODE like'%HO-CRD%' and USER_ID = '" + UserID + "'";
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
                foreach (DataRow row in resDT.Rows)
                {
                    countAuth = Convert.ToInt32(row["Auth"]);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }

            if (UserID == "2009023")
            {
                countAuth = 1;
            }

            return countAuth;

        }

        Int32 CheckReferenceId(string value)
        {
            int countRefData = 0;
            string ssql = "select count(1) as RefData from [dbo].[trxFinancingCreditSLIK] where refid = '" + value + "'";

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
                foreach (DataRow row in resDT.Rows)
                {
                    countRefData = Convert.ToInt32(row["RefData"]);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }

            return countRefData;
        }

        Int32 CheckRequestCount(string client, int id)
        {
            int countData = 0;
            string ssql = "select Count(1) [CountData] from [dbo].[trxRequestSLIK] where CLIENT = '" + client + "' and SID_PENGURUSID = " + id.ToString() + " and REQSTATUS = 'Data on Process'";

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
                foreach (DataRow row in resDT.Rows)
                {
                    countData = Convert.ToInt32(row["CountData"]);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }

            return countData;
        }

        public void UpdateReqStatus(string reffid, string status)
        {
            string ssql = "update [dbo].[trxRequestSLIK] set REQSTATUS = '" + status + "' where REFID = '" + reffid + "'";
            using (SqlConnection conn = new SqlConnection(myDBSetting.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(ssql, conn))
            {
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                conn.Close();
            }
        }

        protected void gvClient_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = clientDtTable;
        }

        protected void gvTempData_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = tmpDtTable;
            (sender as ASPxGridView).FocusedRowIndex = -1;
        }

        protected void gvDebSLIK_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = debDtTable;
            //(sender as ASPxGridView).FocusedRowIndex = -1;
        }

        protected void gvDetailSLIK_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = detailDtTable;
        }

        protected async void btnRequestConfirm_Click(object sender, EventArgs e)
        {
            var isAuth = GetUserRoleAuth();
            if (isAuth > 0)
            {
                btnProgress.ClientVisible = true;
            }

            DataRow myrow = gvTempData.GetDataRow(gvTempData.FocusedRowIndex);
            if (myrow != null)
            {
                string clientcode = myrow["CLIENT"].ToString();
                int pengurusid = Convert.ToInt32(myrow["SID_PENGURUSID"]);
                int CheckReqCount = CheckRequestCount(clientcode, pengurusid);
                if (CheckReqCount == 0)
                {
                    var dtFindDeb = from row in tmpDtTable.AsEnumerable()
                                    where row.Field<string>("CLIENT") == clientcode
                                    && row.Field<Int32>("SID_PENGURUSID") == pengurusid
                                    select row;
                    var dtCheckSLIK = dtFindDeb.CopyToDataTable();


                    API_SLIK APIClass = new API_SLIK();
                    var reqSLIK = await APIClass.RequestSLIK(dtCheckSLIK);

                    if (reqSLIK != "")
                    {
                        apcalert.Text = reqSLIK;
                        apcalert.ShowOnPageLoad = true;
                    }
                }
                else
                {
                    apcalert.Text = "Debitur already request SLIK checking, please view latest SLIK checking below";
                    apcalert.ShowOnPageLoad = true;
                }

                //var isAuth = GetUserRoleAuth();
                //if (isAuth > 0)
                //{
                //    btnProgress.ClientVisible = true;
                //}

                //debDtTable = GetDebData(clientcode, pengurusid);
                //gvDebSLIK.DataSource = debDtTable;
                //gvDebSLIK.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                return;
            }

            
        }

        protected async void btnView_Click(object sender, EventArgs e)
        {
            var isAuth = GetUserRoleAuth();
            if (isAuth > 0)
            {
                btnProgress.ClientVisible = true;
            }

            DataRow myrow = gvDebSLIK.GetDataRow(gvDebSLIK.FocusedRowIndex);
            if (myrow != null)
            {
                string refcode = myrow["REFID"].ToString();
                string ccode = myrow["CLIENT"].ToString();
                int pengurusid = Convert.ToInt32(myrow["SID_PENGURUSID"]);
                int ctype = Convert.ToInt32(myrow["CUSTTYPE"]);

                int checkRef = CheckReferenceId(refcode);
                if (checkRef == 0)
                {
                    API_SLIK APIClass = new API_SLIK();
                    string checkSLIK = "";
                    if (ctype == 1)
                    {
                        checkSLIK = await APIClass.GetSLIK(refcode);
                    }
                    else
                    {
                        checkSLIK = await APIClass.GetCompanySLIK(refcode);
                    }
                    
                    if (checkSLIK != "")
                    {
                        UpdateReqStatus(refcode, checkSLIK);
                        debDtTable = GetDebData(ccode, pengurusid);
                        gvDebSLIK.DataSource = debDtTable;
                        gvDebSLIK.DataBind();

                        apcalert.Text = checkSLIK;
                        apcalert.ShowOnPageLoad = true;
                    }else
                    {
                        UpdateReqStatus(refcode, "DONE");

                        debDtTable = GetDebData(ccode, pengurusid);
                        gvDebSLIK.DataSource = debDtTable;
                        gvDebSLIK.DataBind();
                    }
                }
                
                detailDtTable = GetDetailData(refcode);
                gvDetailSLIK.DataSource = detailDtTable;
                gvDetailSLIK.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                return;
            }

            
        }

        
    }
}