using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.ProfitCalculator.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.ProfitCalculator.Models.Constanta;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.ProfitCalculator
{
    public partial class ProfitCalculator : BasePage
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
        protected DataTable branchDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["branchDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["branchDtTable" + this.ViewState["_PageID"]] = value; }
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

                branchDtTable = new DataTable();
                branchDtTable = GetListBranch();
                gvCabang.DataSource = branchDtTable;
                gvCabang.DataBind();
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
                    cplMain.JSProperties["cplblmessageError"] = "";
                    if(gvCabang.Text == "")
                    {
                        cplMain.JSProperties["cplblmessageError"] = "error";
                        cplMain.JSProperties["cpAlertMessage"] = "Cabang is Empty...";
                        cplMain.JSProperties["cplblActionButton"] = "OK";
                    }else if(cbTenor.Text == "")
                    {
                        cplMain.JSProperties["cplblmessageError"] = "error";
                        cplMain.JSProperties["cpAlertMessage"] = "Tenor is Empty...";
                        cplMain.JSProperties["cplblActionButton"] = "OK";
                    }
                    else
                    {
                        CalculateParam param = new CalculateParam();
                        double bankrate = Convert.ToDouble(ConfigurationManager.AppSettings["profit_calc_bankrate"].ToString());
                        param.pembiayaan = Convert.ToDouble(txtPembiayaan.Text == "" ? "0" : txtPembiayaan.Text);
                        param.tenor = Convert.ToDouble(cbTenor.Text == "" ? "0" : cbTenor.Text);
                        param.firstPaymentType = rdIns.ValueField == "Advance" ? Constanta.FIRST_PAYMENT_TYPE.ADVANCE : Constanta.FIRST_PAYMENT_TYPE.ARREAR;
                        param.effectiveRate = Convert.ToDouble(txtEffRate.Text == "" ? "0" : txtEffRate.Text);
                        //param.effectiveRateBank = Convert.ToDouble(txtBankRate.Text == "" ? "0" : txtBankRate.Text);
                        param.effectiveRateBank = bankrate;
                        param.officeCode = gvCabang.Text;
                        CalculateResult res = CalculateProfit(param);

                        var strOutput = JsonConvert.SerializeObject(res, Formatting.Indented);

                        if (res.profitLossBeforeTax < 0)
                        {
                            cplMain.JSProperties["cpProfitType"] = "Loss";
                            cplMain.JSProperties["cpOutput"] = String.Format("Loss : {0:n0} (Estimation)", res.profitLossBeforeTax);
                            //cplMain.JSProperties["cpOutput"] = strOutput;
                        }
                        else
                        {
                            cplMain.JSProperties["cpProfitType"] = "Profit";
                            cplMain.JSProperties["cpOutput"] = String.Format("Profit : {0:n0} (Estimation)", res.profitLossBeforeTax);
                            //cplMain.JSProperties["cpOutput"] = strOutput;
                        }
                    }
                    break;
            }
        }

        protected void gvCabang_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = branchDtTable;
        }

        DataTable GetListBranch()
        {
            //string ssql = "select C_CODE, C_NAME from SYS_COMPANY where C_NAME not like '%SYARIAH%' AND C_CODE NOT IN('0000','0001','0002')";
            string ssql = "select LTRIM(RTRIM(a.OfficeCode)) [OfficeCode], a.OfficeName from MasterBebanCabang a inner join MsNpl b on LTRIM(RTRIM(a.OfficeCode)) = LTRIM(RTRIM(b.OfficeCode))";
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

        protected decimal GetBebanCabang(string value)
        {
            string ssql = "select ISNULL(BebanAmt,0) [BebanAmt] from dbo.MasterBebanCabang where LTRIM(RTRIM(officeCode)) = '" + value + "'";
            decimal resDec = 0;
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
                if(resDT.Rows.Count > 0)
                {
                    foreach(DataRow row in resDT.Rows)
                    {
                        resDec = Convert.ToDecimal(row["BebanAmt"].ToString());
                    }
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

            return resDec;
        }

        protected decimal GetNplPercent(string value)
        {
            string ssql = "select ISNULL(NplPercentage,0) [NplPercentage] from dbo.MsNpl where LTRIM(RTRIM(officecode)) = '" + value + "'";
            decimal resDec = 0;
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
                if (resDT.Rows.Count > 0)
                {
                    foreach (DataRow row in resDT.Rows)
                    {
                        resDec = Convert.ToDecimal(row["NplPercentage"].ToString());
                    }
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

            return resDec;
        }

        protected CalculateResult CalculateProfit(CalculateParam param)
        {
            CalculateResult res = new CalculateResult();
            res.calculatorParam = param;
            //decimal? beban = ent.MasterBebanCabangs.Where(x => x.OfficeCode == param.officeCode).SingleOrDefault().BebanAmt;
            //decimal? npl = ent.MsNpls.Where(y => y.OfficeCode == param.officeCode).SingleOrDefault().NplPercentage;

            decimal? beban = GetBebanCabang(param.officeCode);
            decimal? npl = GetNplPercent(param.officeCode);
            beban = beban == null ? 0 : beban;
            npl = npl == null ? 0 : npl;

            param.generalAdmFee = Convert.ToDouble(beban);
            param.bebanCadanganRate = Convert.ToDouble(npl);


            if (param.firstPaymentType == FIRST_PAYMENT_TYPE.ADVANCE)
            {
                res.flatRateFullTerm = (Financial.Pmt(param.effectiveRate / 12.0, param.tenor, -1, 0, DueDate.BegOfPeriod) * param.tenor - 1) * 100;
                res.flatRateFullTermBank = (Financial.Pmt(param.effectiveRateBank / 12.0, param.tenor, -1, 0, DueDate.BegOfPeriod) * param.tenor - 1) * 100;
            }
            else
            {
                res.flatRateFullTerm = (Financial.Pmt((param.effectiveRate / 100) / 12.0, param.tenor, -1, 0, DueDate.EndOfPeriod) * param.tenor - 1) * 100;
                res.flatRateFullTermBank = (Financial.Pmt((param.effectiveRateBank / 100) / 12.0, param.tenor, -1, 0, DueDate.EndOfPeriod) * param.tenor - 1) * 100;
            }
            res.flatRateAnnual = res.flatRateFullTerm / param.tenor * 12;
            res.flatRateAnnualBank = res.flatRateFullTermBank / param.tenor * 12;
            res.interestIncome = param.pembiayaan * res.flatRateFullTerm / 100;
            res.provisionIncome = param.provisionIncomeRate / 100 * param.pembiayaan;
            res.hutangPembiayaan = param.pembiayaan + res.interestIncome;
            res.installment = res.hutangPembiayaan / param.tenor;
            res.incomeAdm = param.admIncome;
            res.incomeInsurance = (param.insRate / 100 * param.pembiayaan) * param.insIncomeRate / 100;
            res.totalIncome = res.interestIncome + res.provisionIncome + res.incomeInsurance + res.incomeAdm;

            res.bebanBungaBank = res.flatRateFullTermBank / 100 * param.pembiayaan;
            res.installmentBank = res.bebanBungaBank + param.pembiayaan / param.tenor;
            res.bebanProvisiBank = param.provisionBankRate / 100 * param.pembiayaan;
            res.bebanGeneralAdmExpenseCabang = param.generalAdmFee;
            res.bebanAdmBank = param.bankAdmFeeRate / 100 * param.pembiayaan;
            res.bebanCadangan = param.bebanCadanganRate / 100 * param.pembiayaan;
            res.totalBeban = res.bebanBungaBank + res.bebanProvisiBank + res.bebanAdmBank + res.bebanGeneralAdmExpenseCabang + res.bebanCadangan;

            res.profitLossBeforeTax = res.totalIncome - res.totalBeban;
            return res;
        }
    }
}