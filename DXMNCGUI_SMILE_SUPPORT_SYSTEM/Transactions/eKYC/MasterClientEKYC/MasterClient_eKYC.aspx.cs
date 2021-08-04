using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.EKYC;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.EKYC.Models;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.eKYC.MasterClientEKYC
{
    public partial class MasterClient_eKYC : BasePage
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
        protected DataTable logDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["logDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["logDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected Stream myFs
        {
            get { isValidLogin(false); return (Stream)HttpContext.Current.Session["myFs" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myFs" + this.ViewState["_PageID"]] = value; }
        }
        protected string resultFileName
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["resultFileName" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["resultFileName" + this.ViewState["_PageID"]] = value; }
        }
        protected string resultFilePath
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["resultFilePath" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["resultFilePath" + this.ViewState["_PageID"]] = value; }
        }
        protected string resultExtension
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["resultExtension" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["resultExtension" + this.ViewState["_PageID"]] = value; }
        }
        protected int FileDocID
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["FileDocID" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["FileDocID" + this.ViewState["_PageID"]] = value; }
        }
        protected int CountUploaded
        {
            get { isValidLogin(false); return (int)HttpContext.Current.Session["CountUploaded" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["CountUploaded" + this.ViewState["_PageID"]] = value; }
        }

        const string UploadDirectory = @"~/Transactions/eKYC/UploadEKYC/";
        string resultFileUrl = String.Empty;
        string name = String.Empty;
        string url = String.Empty;
        long sizeInKilobytes = 0;

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
                logDtTable = new DataTable();
                

                CountUploaded = 0;

                clientDtTable = GetListClient();
                gvClient.DataSource = clientDtTable;
                gvClient.DataBind();

                tmpDtTable = GetTempData("");
                gvTempData.DataSource = tmpDtTable;
                gvTempData.DataBind();

                logDtTable = GetLogData("");
                gvLogData.DataSource = logDtTable;
                gvLogData.DataBind();

                if (Convert.ToInt32(this.Request.QueryString["ID"]) != 0)
                {
                    SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM mstUploadEKYC WHERE ID=@ID", myconn);
                    sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(this.Request.QueryString["ID"]));
                    myconn.Open();
                    SqlDataReader dr = sqlCommand.ExecuteReader(); ;
                    if (dr.Read())
                    {
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.Buffer = true;
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + dr["clientid"].ToString() + dr["Ext"].ToString());
                        HttpContext.Current.Response.Charset = "";
                        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        HttpContext.Current.Response.BinaryWrite((byte[])dr["uploadfile"]);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                    myconn.Close();
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

            switch (callbackParam[0].ToUpper())
            {
                case "SAVE":
                    GridViewDataColumn dataColumn = gvTempData.Columns[10] as GridViewDataColumn;
                    for (int i = 0; i < gvTempData.VisibleRowCount; i++)
                    {
                        ASPxUploadControl uplControl = gvTempData.FindRowCellTemplateControl(i, dataColumn, "UploadCtrl") as ASPxUploadControl;

                    }
                    break;
                case "UPLOAD":
                    break;
                case "LOAD":
                    var ccode = callbackParam[1].ToString();
                    tmpDtTable = GetTempData(ccode);
                    logDtTable = GetLogData(ccode);
                    cplMain.JSProperties["cpEnableBtn"] = "disable";

                    var isCompleted = GetUploadedCompleted(ccode);
                    var isValidCheck = GetCountHitAPI(ccode);
                    var isAuth = GetUserRoleAuth();
                    var isComplteEKYC = GetCompletedEKYC(ccode);
                    //var isAuth = 1;
                    if (isCompleted == 0 && isAuth > 0 && isValidCheck < 2 && isComplteEKYC == 0)
                    {
                        cplMain.JSProperties["cpEnableBtn"] = "enable";
                    }
                    break;
            }
        }

        protected void gvClient_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = clientDtTable;
        }

        protected void gvTempData_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = tmpDtTable;
        }

        protected void gvLogData_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = logDtTable;
        }

        protected void gvTempData_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (e.DataColumn.FieldName.ToString().Length > 4)
            {
                if (e.DataColumn.FieldName.Substring(0, 4) == "EKYC")
                {
                    if (e.CellValue.ToString() == "Not Match" || e.CellValue.ToString() == "")
                    {
                        e.Cell.BackColor = System.Drawing.Color.LightYellow;
                    }
                    else
                    {
                        e.Cell.BackColor = System.Drawing.Color.LightCyan;
                    }
                }
            }

            if (e.DataColumn.FieldName == "UPLOAD_KTP")
            {
                ASPxUploadControl uplControl = grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "UploadCtrl") as ASPxUploadControl;
                if (Convert.ToInt32(e.CellValue) == 0)
                {
                    uplControl.Visible = true;
                    CountUploaded += 1;
                }
                else
                {
                    uplControl.Visible = false;
                }
            }
            
        }

        protected void gvTempData_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            var someFieldValue = (int)grid.GetRowValues(e.VisibleIndex, "UPLOAD_KTP");
            if (e.ButtonID == "GridbtnDownload")
            {
                if (Convert.ToInt32(someFieldValue) == 0)
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                }
                    
            }
        }

        protected void gvLogData_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (e.DataColumn.FieldName.ToString().Length > 4)
            {
                if (e.DataColumn.FieldName.Substring(0, 4) == "EKYC")
                {
                    if (e.CellValue.ToString() == "Not Match" || e.CellValue.ToString() == "")
                    {
                        e.Cell.BackColor = System.Drawing.Color.LightYellow;
                    }
                    else
                    {
                        e.Cell.BackColor = System.Drawing.Color.LightCyan;
                    }
                }
            }
        }

        

        DataTable GetListClient()
        {
            string ssql = "select " + 
                            "CLIENT[CIF], " +
                            "ISNULL(REAL_NAME,NAME) [Client Name], " + 
	                        "INKTP[No KTP], " +
	                        "INBORNDT[Tgl Lahir], " +
	                        "INBORNPLC[Tempat Lahir], " +
	                        "LTRIM(RTRIM(ISNULL(ADDRESS1, '')))[Alamat] " +
                        "from[dbo].[SYS_CLIENT] " +
                        "order by[Client Name]";

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

            if(UserID == "2009023")
            {
                countAuth = 1;
            }

            return countAuth;

        }

        Int32 GetCountHitAPI(string value)
        {
            int countEKYC = 0;
            string ssql = "select COUNT(1)[CountClient] from mstClientEKYC_log where CRE_BY = '" + UserID + "' and CLIENT = '" + value + "'";
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
                    countEKYC = Convert.ToInt32(row["CountClient"]);
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
            return countEKYC;
        }

        Int32 GetCompletedEKYC(string value)
        {
            int countEKYC = 0;
            string ssql = "select Count(1)[CountClient] from mstClientEKYC_log where client = '" + value + "' " +
                            "and ekyc_name = 'Match' and EKYC_INBORNDT = 'Match' and EKYC_INBORNPLC = 'Match' and ISNULL(EKYC_ADDRESS,'') <> ''";
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
                    countEKYC = Convert.ToInt32(row["CountClient"]);
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
            return countEKYC;
        }

        Int32 GetUploadedCompleted(string value)
        {
            int countCompleted = 0;
            string ssql = "select count(a.CLIENT) completed from sys_client a left join SID_PENGURUS c on a.CLIENT = c.LOCALCODE " +
                "left join mstUploadEKYC b on a.Client = b.clientid and ISNULL(c.SID_PENGURUSID, 0) = b.sid_pengurus " +
                "where a.client = '" + value + "' and b.id is null";

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
                    countCompleted = Convert.ToInt32(row["completed"]);
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

            return countCompleted;
        }



        DataTable GetTempData(string value)
        {
            //string ssql = "exec spMNCL_getClient_EKYC '" + value + "','" + UserID + "'";
            string ssql = "exec [dbo].[spMNCL_getEKYC] '" + value + "'";

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

        DataTable GetLogData(string value)
        {
            //string ssql = "select * from [mstClientEKYC_log] where CLIENT = '" + value + "'";

            string ssql = "select KTP, NAME, EKYC_NAME,FORMAT (a.INBORNDT, 'dd-MM-yyyy') [INBORNDT],EKYC_INBORNDT,INBORNPLC, " +
                "EKYC_INBORNPLC,ADDRESS,EKYC_ADDRESS,b.USER_NAME[Audit User],FORMAT(a.CRE_DATE, 'dd-MM-yyyy hh:mm:ss')[Audit Date] " +
                "from mstClientEKYC_log a left join MASTER_USER b on a.CRE_BY = b.USER_ID " +
                "where a.CLIENT = '" + value + "'";

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


        protected void UploadCtrl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            resultExtension = Path.GetExtension(e.UploadedFile.FileName);
            resultFileName = "EKYC_" + Path.ChangeExtension(Path.GetRandomFileName(), resultExtension);
            resultFileUrl = UploadDirectory + resultFileName;
            resultFilePath = MapPath(resultFileUrl);
            e.UploadedFile.SaveAs(resultFilePath);
            name = e.UploadedFile.FileName;
            myFs = e.UploadedFile.FileContent;

            string id_client = (string)HiddenField["CLIENTID"];
            int id_pengurus = Convert.ToInt32((string)HiddenField["SID_PENGURUSID"]);
            string id_ktp = (string)HiddenField["KTP"];

            uploadEKYC(id_client, id_pengurus, id_ktp);

            tmpDtTable = GetTempData(id_client);
        }
        

        protected void gvMain_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "GridbtnDownload")
            {
                try
                {
                    if (gvTempData.GetRowValues(e.VisibleIndex, "ID_UPLOAD").ToString() != "")
                    {
                        string id_upl = gvTempData.GetRowValues(e.VisibleIndex, "ID_UPLOAD").ToString();
                        ASPxWebControl.RedirectOnCallback(string.Format("MasterClient_eKYC.aspx?ID=" + id_upl));
                    }
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        protected async void btnSave_onClick(Object sender, EventArgs e)
        {
            if (tmpDtTable.Rows.Count > 0)
            {
                //apcalert.Text = "Can't run e-KYC checking because " + UserName + " already checking twice for this client";
                //apcalert.ShowOnPageLoad = true;

                var ccode = gvClient.Value.ToString();

                API_EKYC APIClass = new API_EKYC();
                ModelEKYC mdlEKYC = new ModelEKYC();

                var resultModel = await APIClass.BasicVerification(tmpDtTable, UserID);

                tmpDtTable = GetTempData(ccode);
                gvTempData.DataSource = tmpDtTable;
                gvTempData.DataBind();

                logDtTable = GetLogData(ccode);
                gvLogData.DataSource = logDtTable;
                gvLogData.DataBind();

                //VALIDASI BUTTON CHECK EKYC
                var isCompleted = GetUploadedCompleted(ccode);
                var isValidCheck = GetCountHitAPI(ccode);
                var isAuth = GetUserRoleAuth();
                var isComplteEKYC = GetCompletedEKYC(ccode);
                if (isCompleted == 0 && isAuth > 0 && isValidCheck < 2 && isComplteEKYC == 0)
                {
                    btnSave.ClientVisible = true;
                }
            }
        }
        

        public void uploadEKYC(string clientid, int pengurusid, string ktp)
        {
            //spMNCL_UploadEKYC
            BinaryReader br = new BinaryReader(myFs);
            Byte[] bytes = br.ReadBytes((Int32)myFs.Length);

            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                string ssql = "exec spMNCL_UploadEKYC @clientid,@pengurusid,@fileupload,@ext,@userid,@ktp";
                SqlCommand sqlCommand = new SqlCommand(ssql);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;
                sqlCommand.Transaction = trans;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@clientid", SqlDbType.VarChar);
                sqlParameter1.Value = clientid;

                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@pengurusid", SqlDbType.Int);
                sqlParameter2.Value = pengurusid;

                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@fileupload", SqlDbType.Binary);
                sqlParameter3.Value = bytes;

                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@ext", SqlDbType.VarChar);
                sqlParameter4.Value = resultExtension;

                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@userid", SqlDbType.VarChar);
                sqlParameter5.Value = UserID;

                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@ktp", SqlDbType.VarChar);
                sqlParameter6.Value = ktp;

                sqlCommand.ExecuteNonQuery();
                trans.Commit();
            }
            catch(Exception ex)
            {
                trans.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
        }

    }
}