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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.PeminjamanDocument.MasterDocLoacation
{
    public partial class DocLocationEntry : BasePage
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
        protected string strKey
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable editDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["editDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["editDtTable" + this.ViewState["_PageID"]] = value; }
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
                editDtTable = new DataTable();
                strKey = "";

                if (this.Request.QueryString["Key"] != null)
                {
                    strKey = this.Request.QueryString["Key"].ToString();
                    editDtTable = loadData(strKey);

                    bindData();
                    //btnDelete.ClientVisible = true;
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
            //string ccode = "";
            //int ctype = 0;

            switch (callbackParam[0].ToUpper())
            {
                case "LOAD":
                    break;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(strKey != "")
            {
                //int locID = Convert.ToInt32(luLocation.Value);
                updateData(strKey, cbCategory.Value.ToString(), mmDesc.Text, cbLocation.Text, txtRefNum.Text);

                //log
                insertLog(txtEditKontrak.Text, cbCategory.Value.ToString(), mmDesc.Text, cbLocation.Text, txtRefNum.Text);

                apcalert.Text = "Update data success";
                apcalert.ShowOnPageLoad = true;
            }
            else
            {
                //int locID = Convert.ToInt32(luLocation.Value);
                saveData(luKontrak.Text, cbCategory.Value.ToString(), mmDesc.Text, cbLocation.Text, txtRefNum.Text);

                //log
                insertLog(luKontrak.Text, cbCategory.Value.ToString(), mmDesc.Text, cbLocation.Text, txtRefNum.Text);

                apcalert.Text = "Save data success";
                apcalert.ShowOnPageLoad = true;
            }
            

            Response.Redirect("DocLocationList.aspx");
        }

        protected void bindData()
        {
            if(editDtTable.Rows.Count > 0)
            {
                foreach (DataRow dr in editDtTable.Rows)
                {
                    cbCategory.SelectedItem.Value = dr["DocCategory"].ToString();
                    txtEditKontrak.Text = dr["DocID"].ToString();
                    mmDesc.Text = dr["Description"].ToString();
                    //luLocation.Value = dr["LocID"].ToString();

                    string LocDesc = dr["Location"].ToString();

                    var findUserLoc = cbLocation.Items.FindByText(LocDesc);
                    if(findUserLoc == null)
                    {
                        var userLoc = new ListEditItem(LocDesc, LocDesc);
                        cbLocation.Items.Add(userLoc);
                    }
                    cbLocation.Text = dr["Location"].ToString();

                    if (dr["ReffNum"] != null)
                    {
                        txtRefNum.Text = dr["ReffNum"].ToString();
                    }
                }
            }
            
            luKontrak.ClientVisible = false;
            txtEditKontrak.ClientVisible = true;
            //cbCategory.ClientEnabled = false;
            //mmDesc.ClientEnabled = false;
            //luLocation.ClientEnabled = false;
            //txtRefNum.ClientEnabled = false;
        }

        protected void saveData(string DocID, string DocCategory, string DocDesc, string LocDesc, string RefID)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [SSS].[dbo].[mstDocLocation] ([DocID],[DocCategory],[Location],[ReffNum],[Description],[CRE_BY],[CRE_DATE],[Status]) VALUES(@DocID,@DocCategory,@LocDesc,@ReffNum,@Description,@CRE_BY,GETDATE(),'New')");
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocID", SqlDbType.VarChar);
            sqlParameter1.Value = DocID;
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocCategory", SqlDbType.VarChar);
            sqlParameter2.Value = DocCategory;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@LocDesc", SqlDbType.VarChar);
            sqlParameter3.Value = LocDesc;
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@ReffNum", SqlDbType.VarChar);
            sqlParameter4.Value = RefID;
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar);
            sqlParameter5.Value = DocDesc;
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@CRE_BY", SqlDbType.VarChar);
            sqlParameter6.Value = UserID;
            sqlParameter6.Direction = ParameterDirection.Input;

            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        protected void updateData(string DocKey, string DocCategory, string DocDesc, string LocDesc, string RefID)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(@"UPDATE [SSS].[dbo].[mstDocLocation] SET DocCategory = @DocCategory, Location = @LocDesc, Description = @Description, ReffNum = @ReffNum, MOD_BY=@MOD_BY, MOD_DATE = GETDATE() WHERE DocKey = @DocKey");
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.VarChar);
            sqlParameter1.Value = DocKey;
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocCategory", SqlDbType.VarChar);
            sqlParameter2.Value = DocCategory;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@LocDesc", SqlDbType.VarChar);
            sqlParameter3.Value = LocDesc;
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@ReffNum", SqlDbType.VarChar);
            sqlParameter4.Value = RefID;
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar);
            sqlParameter5.Value = DocDesc;
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@MOD_BY", SqlDbType.VarChar);
            sqlParameter6.Value = UserID;
            sqlParameter6.Direction = ParameterDirection.Input;

            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        protected void deleteData(string DocKey)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(@"DELETE [SSS].[dbo].[mstDocLocation] WHERE DocKey = @DocKey");
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.VarChar);
            sqlParameter1.Value = DocKey;
            sqlParameter1.Direction = ParameterDirection.Input;

            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        protected void insertLog(string DocID, string DocCategory, string DocDesc, string LocDesc, string RefID)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [SSS].[dbo].[mstDocLocation_log]([DocID],[DocCategory],[Location],[ReffNum],[Description],[CRE_BY],[CRE_DATE],[Status]) VALUES(@DocID,@DocCategory,@LocDesc,@ReffNum,@Description,@CRE_BY,GETDATE(),'New')");
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocID", SqlDbType.VarChar);
            sqlParameter1.Value = DocID;
            sqlParameter1.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocCategory", SqlDbType.VarChar);
            sqlParameter2.Value = DocCategory;
            sqlParameter2.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@LocDesc", SqlDbType.VarChar);
            sqlParameter3.Value = LocDesc;
            sqlParameter3.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@ReffNum", SqlDbType.VarChar);
            sqlParameter4.Value = RefID;
            sqlParameter4.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar);
            sqlParameter5.Value = DocDesc;
            sqlParameter5.Direction = ParameterDirection.Input;
            SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@CRE_BY", SqlDbType.VarChar);
            sqlParameter6.Value = UserID;
            sqlParameter6.Direction = ParameterDirection.Input;

            sqlCommand.ExecuteNonQuery();

            myconn.Close();
        }

        protected DataTable loadData(string value)
        {
            DataTable resDT = new DataTable();
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(@"select * from [SSS].[dbo].[mstDocLocation] where DocKey = @DocKey");
            sqlCommand.Connection = myconn;
            myconn.Open();

            SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.VarChar);
            sqlParameter1.Value = value;
            sqlParameter1.Direction = ParameterDirection.Input;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            resDT.Load(reader);

            myconn.Close();

            return resDT;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DocLocationList.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (strKey != "")
            {
                deleteData(strKey);

                apcalert.Text = "Delete data success";
                apcalert.ShowOnPageLoad = true;
            }
            
            Response.Redirect("DocLocationList.aspx");
        }
    }
}