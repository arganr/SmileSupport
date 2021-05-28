using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance
{
    public partial class CalculateProduct : BasePage
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
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                gvProduct.DataBind();
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
                    if (gvProduct.Text == "")
                    {
                        cplMain.JSProperties["cpAlertMessage"] = "Error: Product is empty";
                        cplMain.JSProperties["cplblActionButton"] = "SAVE";
                        cplMain.JSProperties["cpTotal"] = "0";
                        cplMain.JSProperties["cpTotalOther"] = "0";
                    }
                    else
                    {

                        double total, firstmonth, amt, adminfee, provision, ratejual, ratebank;
                        if (txtAmt.Text == "") { amt = 0; } else { amt = Convert.ToDouble(txtAmt.Text); }
                        if (txtRateJual.Text == "") { ratejual = 0; } else { ratejual = Convert.ToDouble(txtRateJual.Text); }
                        adminfee = (double)HiddenField["fee"];
                        provision = (double)HiddenField["provision"];
                        int idproduct = Convert.ToInt32(HiddenField["id"]);
                        ratebank = Convert.ToDouble(ConfigurationManager.AppSettings["profit_calc_bankrate"].ToString());

                        ratejual = (ratejual - ratebank) /100;
                        firstmonth = ratejual / 12 * amt;

                        if(idproduct == 1)
                        {
                            provision = 0.01 * amt;
                        }
                        else if(idproduct == 2)
                        {
                            provision = 0.001 * 3 * amt;
                        }
                        else
                        {
                            provision = 0;
                        }

                        total = adminfee + provision + firstmonth;

                        double total_other, other_amt, survey_amt;
                        if (txtSurveyAmt.Text == "") { survey_amt = 0; } else { survey_amt = Convert.ToDouble(txtSurveyAmt.Text); }
                        if (txtOtherAmt.Text == "") { other_amt = 0; } else { other_amt = Convert.ToDouble(txtOtherAmt.Text); }
                        total_other = survey_amt + other_amt;

                        cplMain.JSProperties["cpAlertMessage"] = "Calculate Success";
                        cplMain.JSProperties["cplblActionButton"] = "SAVE";
                        cplMain.JSProperties["cpTotal"] = total.ToString("#,0");
                        cplMain.JSProperties["cpTotalOther"] = total_other.ToString("#,0");
                    }
                    break;
            }
        }

        protected void gvProduct_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = GetListProduct();
        }

        DataTable GetListProduct()
        {
            string ssql = "select product_name Product, adminfee, provision, id from mstCalculateProductList";

            DataTable resDT = new DataTable();
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
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
    }
}