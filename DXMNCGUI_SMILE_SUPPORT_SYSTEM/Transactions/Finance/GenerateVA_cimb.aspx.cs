using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance
{
    public partial class GenerateVA_cimb : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myDBSession = dbsession;
            }
            if (!IsCallback)
            {

            }
        }

        protected void btnSave_Click(Object sender, EventArgs e)
        {
            isValidLogin(false);
            DateTime datefrom = txtStartDate.Date;
            DateTime dateto = txtEndDate.Date;
            string filename;
            string pathfile;
            //filename = @"E:\Test.CSV";
            filename = "VA_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DataTable dtVA = GetTableVA(datefrom, dateto);
            pathfile = HttpContext.Current.Server.MapPath(".") + @"\Download\" + filename + ".csv";

            if (dtVA.Rows.Count > 0)
            {
                ToCSV(dtVA, pathfile);
                lblError.Text = "";
                //DevExpress.Web.ASPxWebControl.RedirectOnCallback("DownloadFile.ashx?filename=" + filename);
                Response.Redirect("DownloadFile.ashx?filename=" + filename);
            }
            else
            {
                //cplMain.JSProperties["cplblmessageError"] = "Data Not Found";
                lblError.Text = "Data Not Found";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            
        }

        DataTable GetTableVA(DateTime datefrom, DateTime dateto)
        {
            string ssql = "exec spGenerateVA_Cimb '" + datefrom.ToString("yyyy/MM/dd") + "', '" + dateto.ToString("yyyy/MM/dd") + "'";
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

        protected void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}