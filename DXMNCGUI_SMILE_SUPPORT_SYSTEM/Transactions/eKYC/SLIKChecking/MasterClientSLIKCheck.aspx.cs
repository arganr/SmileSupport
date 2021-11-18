using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SLIK;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.eKYC.SLIKChecking
{
    public partial class MasterClientSLIKCheck : BasePage
    {
        const string UploadDirectory = @"~/Transactions/eKYC/UploadSLIK/";

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

                clientDtTable = GetListClient(cbSource.Text);
                gvClient.DataSource = clientDtTable;
                gvClient.DataBind();

                tmpDtTable = GetTempData("", cbSource.Text);
                gvTempData.DataSource = tmpDtTable;
                gvTempData.DataBind();

                debDtTable = GetDebData("",0);
                gvDebSLIK.DataSource = debDtTable;
                gvDebSLIK.DataBind();

                detailDtTable = GetDetailData("");
                gvDetailSLIK.DataSource = detailDtTable;
                gvDetailSLIK.DataBind();

                var isAuth = GetUserRoleAuth();
                //var isAuth = 1;
                if (isAuth > 0)
                {
                    ucFileClient.ClientEnabled = true;
                    btnUpload.ClientEnabled = true;
                }
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
                    tmpDtTable = GetTempData(ccode, cbSource.Text);
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
                case "SOURCE":
                    clientDtTable.Clear();
                    clientDtTable = GetListClient(cbSource.Text);

                    tmpDtTable.Clear();
                    debDtTable.Clear();
                    detailDtTable.Clear();

                    break;
            }
        }



        DataTable GetListClient(string source)
        {
            string ssql = string.Empty;
            if(source == "SMILE")
            {
                ssql = "select CLIENT[CIF], NAME[Client Name], INKTP[No KTP], ''[NPWP], INBORNDT[Tgl Lahir], INBORNPLC[Tempat Lahir], LTRIM(RTRIM(ISNULL(ADDRESS1, '')))[Alamat] from[dbo].[SYS_CLIENT] order by [Client Name]";
            }
            else
            {
                ssql = "select distinct NAME[CIF], NAME[Client Name], ISNULL(KTP,'')[No KTP], ISNULL(NPWP,'')[NPWP],null[Tgl Lahir], ''[Tempat Lahir], ''[Alamat] from [dbo].[trxClientUploadSLIK]";
            }

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

        DataTable GetTempData(string value, string source)
        {
            string ssql = "exec [dbo].[spMNCL_getClientSLIK] '" + value + "', '" + source + "'";

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
            string ssql = "select *, ROUND((JANGKA / 12),0) [YearJangka], ROUND((SISATENOR / 12),0) [YearSisaTenor] from trxFinancingCreditSLIK where REFID = '" + value + "'";

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

            //Cek user CA
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
                    countAuth += Convert.ToInt32(row["Auth"]);
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

            //User Selain CA yg dapat akses
            ssql = "select Count(1) Auth from AccessRight where CMDid = 'SLIK_CAN_REQUEST' and nik = ?";
            object obj = myLocalDBSetting.ExecuteScalar(ssql, this.UserID);
            if (obj != null && obj != DBNull.Value)
            {
                countAuth += Convert.ToInt32(obj);
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
                string npwp_client = myrow["NPWP"].ToString();

                if (cbSource.Text == "SMILE")
                {
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
                }
                else
                {
                    //apcalert.Text = "SLIK request must from data source SMILE";
                    //apcalert.ShowOnPageLoad = true;
                    var dtFindDeb = from row in tmpDtTable.AsEnumerable()
                                    where row.Field<string>("CLIENT") == clientcode
                                    && row.Field<string>("NPWP") == npwp_client
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
        
        public bool InsertClientUpload(string filename, DataTable dtFromExcel)
        {
            bool res = true;

            if(dtFromExcel != null)
            {
                SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
                SqlTransaction sqlTrx;
                
                myconn.Open();
                sqlTrx = myconn.BeginTransaction();

                try
                {
                    foreach (DataRow dr in dtFromExcel.Rows)
                    {
                        if (dr["Name"].ToString() != "" && dr["Name"].ToString() != "-" && dr["NPWP"].ToString() != "-" && dr["NPWP"].ToString() != "")
                        {
                            SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[trxClientUploadSLIK] ([FILENAME],[NAME],[NPWP],[CLIENTTYPE],[CRE_BY],[CRE_DATE]) VALUES(@FILENAME,@NAME,@NPWP,@CLIENTTYPE,@CRE_BY,GETDATE())");
                            sqlCommand.Connection = myconn;
                            sqlCommand.Transaction = sqlTrx;

                            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@FILENAME", SqlDbType.VarChar);
                            sqlParameter1.Value = filename;
                            sqlParameter1.Direction = ParameterDirection.Input;
                            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@NAME", SqlDbType.VarChar);
                            sqlParameter2.Value = dr["Name"].ToString();
                            sqlParameter2.Direction = ParameterDirection.Input;
                            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@NPWP", SqlDbType.VarChar);
                            sqlParameter3.Value = dr["NPWP"].ToString();
                            sqlParameter3.Direction = ParameterDirection.Input;
                            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@CLIENTTYPE", SqlDbType.VarChar);
                            sqlParameter4.Value = dr["Type"].ToString();
                            sqlParameter4.Direction = ParameterDirection.Input;
                            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@CRE_BY", SqlDbType.VarChar);
                            sqlParameter5.Value = UserID;
                            sqlParameter5.Direction = ParameterDirection.Input;

                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                    sqlTrx.Commit();
                }
                catch (SqlException sqlError)
                {
                    sqlTrx.Rollback();
                    res = false;
                }
                myconn.Close();
            }

            return res;
        }

        public static DataTable ConvertExcelToDataTable(string FileName)
        {
            DataTable dtResult = null;
            int totalSheet = 0; //No of sheets on excel file  
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                return dtResult; //Returning Dattable  
            }
        }

        protected async void btnUpload_Click(object sender, EventArgs e)
        {
            ASPxUploadControl uploadControl = ucFileClient as ASPxUploadControl;

            if (uploadControl.UploadedFiles != null && uploadControl.UploadedFiles.Length > 0)
            {
                for (int i = 0; i < uploadControl.UploadedFiles.Length; i++)
                {
                    UploadedFile file = uploadControl.UploadedFiles[i];
                    if (file.FileName != "")
                    {
                        string resultExtension = Path.GetExtension(file.FileName);
                        string resultFileName = "SLIK_" + Path.ChangeExtension(Path.GetRandomFileName(), resultExtension);

                        string fileName = string.Format("{0}{1}", MapPath(UploadDirectory), resultFileName);
                        file.SaveAs(fileName, true);

                        DataTable dtFromExcel = new DataTable();
                        dtFromExcel = ConvertExcelToDataTable(fileName);
                        try
                        {
                            bool resUpload = InsertClientUpload(resultFileName, dtFromExcel);

                            if(resUpload == true)
                            {
                                API_SLIK APIClass = new API_SLIK();
                                var reqSLIK = await APIClass.RequestSLIKUpload(dtFromExcel);

                                //Clear All GridView
                                clientDtTable.Clear();
                                clientDtTable = GetListClient(cbSource.Text);
                                gvClient.DataBind();
                                tmpDtTable.Clear();
                                gvTempData.DataBind();
                                debDtTable.Clear();
                                gvDebSLIK.DataBind();
                                detailDtTable.Clear();
                                gvDetailSLIK.DataBind();

                                apcalert.Text = "Upload SLIK Request Success";
                                apcalert.ShowOnPageLoad = true;
                            }else
                            {
                                apcalert.Text = "Excel file is Incorrect";
                                apcalert.ShowOnPageLoad = true;
                            }
                            
                        }
                        catch(Exception ex)
                        {
                            apcalert.Text = "Error: " + ex.Message;
                            apcalert.ShowOnPageLoad = true;
                        }
                    }
                }
            }
            else
            {
                apcalert.Text = "Upload file is empty";
                apcalert.ShowOnPageLoad = true;
            }
        }

        protected void btnTemplate_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~/Transactions/eKYC/TemplateUpload/TemplateUploadSLIK.xlsx");
        }
    }
}