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
    public partial class SurveyTask : BasePage
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

        public DataTable dtSuveyItem;

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                FillSurveyObj();
                cbSurveyObj.SelectedIndex = 0;


                if (this.Request.QueryString["AppNo"] != null)
                {
                    string appNo = this.Request.QueryString["AppNo"].ToString();
                    string objVal = this.Request.QueryString["Obj"].ToString();
                    LoadDetail(appNo, objVal);
                }
                else
                {
                    luAppNo.DataBind();
                    gvSurveyItem.DataBind();
                }

            }
            if (!IsCallback)
            {

            }


        }


        protected void Save(SaveAction saveAction)
        {
            GridViewDataColumn dataColumn = gvSurveyItem.Columns[2] as GridViewDataColumn;
            for (int i = 0; i < gvSurveyItem.VisibleRowCount; i++)
            {
                string agree = string.Empty;
                string itemdesc = gvSurveyItem.GetRowValues(i, new string[] { "Item" }).ToString();
                ASPxCheckBox box = gvSurveyItem.FindRowCellTemplateControl(i, dataColumn, "chkItem") as ASPxCheckBox;
                if (box.Checked == true)
                {
                    agree = "Setuju";
                }
                else
                {
                    agree = "Tidak Setuju";
                }

                InsertSurvey(luAppNo.Text, itemdesc, agree);


            }

            //Get Amount
            decimal income = 0, insSLIK = 0, insDeb = 0, insSpouse = 0, insChild = 0, insOthers = 0;
            decimal incomeOther = 0, incomePenjamin = 0, incomePasanganPenjamin = 0;

            if (txtIncome.Text != "") income = Convert.ToDecimal(txtIncome.Text);
            if (txtInsSLIK.Text != "") insSLIK = Convert.ToDecimal(txtInsSLIK.Text);
            if (txtInsDeb.Text != "") insDeb = Convert.ToDecimal(txtInsDeb.Text);
            if (txtInsChild.Text != "") insChild = Convert.ToDecimal(txtInsChild.Text);
            if (txtInsSpouse.Text != "") insSpouse = Convert.ToDecimal(txtInsSpouse.Text);
            if (txtInsOther.Text != "") insOthers = Convert.ToDecimal(txtInsOther.Text);

            if (txtIncomeOther.Text != "") incomeOther = Convert.ToDecimal(txtIncomeOther.Text);
            if (txtIncomePenjamin.Text != "") incomePenjamin = Convert.ToDecimal(txtIncomePenjamin.Text);
            if (txtIncomePasanganPenjamin.Text != "") incomePasanganPenjamin = Convert.ToDecimal(txtIncomePasanganPenjamin.Text);

            //recalculate
            decimal resDBR = 0;
            decimal totalIns = insSLIK + insDeb + insSpouse + insChild + insOthers;
            decimal totalIncome = income + incomeOther + incomePenjamin + incomePasanganPenjamin;

            if (totalIncome != 0)
            {
                resDBR = (totalIns / totalIncome) * 100;
            }else
            {
                resDBR = 100;
            }

            //Perhitungan Free Cash Flow
            decimal costiving = 0, resFCF = 0;
            if (txtBiayaHidup.Text != "") costiving = Convert.ToDecimal(txtBiayaHidup.Text);

            decimal totalMinIns = totalIncome - insSLIK - insDeb - insSpouse - insChild - insOthers;
            if (costiving != 0)
            {
                resFCF = (totalMinIns / costiving) * 100;
            }

            InsertDBR(luAppNo.Text, income.ToString(), insSLIK.ToString(), insDeb.ToString(), insSpouse.ToString(), insChild.ToString(), insOthers.ToString(),
                resDBR.ToString().Replace(",", "."), costiving.ToString(), resFCF.ToString().Replace(",", "."),
                incomeOther.ToString(), incomePenjamin.ToString(), incomePasanganPenjamin.ToString());

            cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
            cplMain.JSProperties["cplblActionButton"] = "SAVE";
            DevExpress.Web.ASPxWebControl.RedirectOnCallback("ListSurvey.aspx");
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
                    if (luAppNo.Value == null)
                    {
                        cplMain.JSProperties["cpAlertMessage"] = "App Number is empty...";
                        cplMain.JSProperties["cplblActionButton"] = "SAVE";
                        DevExpress.Web.ASPxWebControl.RedirectOnCallback("SurveyTask.aspx");
                    }
                    else
                    {
                        if (NoAppExist(luAppNo.Text))
                        {
                            cplMain.JSProperties["cpAlertMessage"] = "App Number is exist...";
                            cplMain.JSProperties["cplblActionButton"] = "SAVE";
                            //DevExpress.Web.ASPxWebControl.RedirectOnCallback("SurveyTask.aspx");
                        }
                        else
                        {
                            Save(SaveAction.Submit);
                            cplMain.JSProperties["cpAlertMessage"] = "Transaction has been save...";
                            cplMain.JSProperties["cplblActionButton"] = "SAVE";
                            DevExpress.Web.ASPxWebControl.RedirectOnCallback("ListSurvey.aspx");
                        }
                    }

                    break;

                case "DBR":
                    //Angsuran SLIK + Pengajuan cicilan haji)/Income Salary.
                    decimal income = 0, insSLIK = 0, insDeb = 0, insSpouse = 0, insChild = 0, insOthers = 0, costiving = 0;
                    decimal incomeOther = 0, incomePenjamin = 0, incomePasanganPenjamin = 0;
                    if (txtIncome.Text != "") income = Convert.ToDecimal(txtIncome.Text);
                    if (txtInsSLIK.Text != "") insSLIK = Convert.ToDecimal(txtInsSLIK.Text);
                    if (txtInsDeb.Text != "") insDeb = Convert.ToDecimal(txtInsDeb.Text);
                    if (txtInsChild.Text != "") insChild = Convert.ToDecimal(txtInsChild.Text);
                    if (txtInsSpouse.Text != "") insSpouse = Convert.ToDecimal(txtInsSpouse.Text);
                    if (txtInsOther.Text != "") insOthers = Convert.ToDecimal(txtInsOther.Text);
                    if (txtBiayaHidup.Text != "") costiving = Convert.ToDecimal(txtBiayaHidup.Text);

                    if (txtIncomeOther.Text != "") incomeOther = Convert.ToDecimal(txtIncomeOther.Text);
                    if (txtIncomePenjamin.Text != "") incomePenjamin = Convert.ToDecimal(txtIncomePenjamin.Text);
                    if (txtIncomePasanganPenjamin.Text != "") incomePasanganPenjamin = Convert.ToDecimal(txtIncomePasanganPenjamin.Text);

                    decimal resDBR = 0;
                    decimal totalIns = insSLIK + insDeb + insSpouse + insChild + insOthers;
                    decimal totalIncome = income + incomeOther + incomePenjamin + incomePasanganPenjamin;

                    if (totalIncome != 0)
                    {
                        resDBR = (totalIns / totalIncome) * 100;
                    }
                    else
                    {
                        resDBR = 100;
                    }

                    //Perhitungan Free Cash Flow
                    decimal resFCF = 0;
                    decimal totalMinIns = totalIncome - insSLIK - insDeb - insSpouse - insChild - insOthers;
                    if (costiving != 0)
                    {
                        resFCF = (totalMinIns / costiving) * 100;
                    }

                    cplMain.JSProperties["cpDBR"] = resDBR.ToString("0.##");
                    cplMain.JSProperties["cpFCF"] = resFCF.ToString("0.##");

                    break;

                case "SLIK":
                    if (luAppNo.Value != null)
                    {
                        var InsSLIK = GetAngsuranSLIK(luAppNo.Value.ToString());
                        cplMain.JSProperties["cpInsSLIK"] = InsSLIK;
                    }

                    break;
            }
        }


        protected void gvNoApp_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = GetListNoApp("");
        }


        protected void gvSurveyItem_Init(object sender, EventArgs e)
        {

        }

        protected void gvSurveyItem_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void gvSurveyItem_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = GetListSurveyItem();
        }

        protected void gvLoadSurvey_DataBinding(object sender, EventArgs e)
        {
            //(sender as ASPxGridView).DataSource = LoadListSurveyItem();
        }

        protected void cbSurveyObj_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            gvSurveyItem.DataBind();
        }

        protected void LoadDetail(string appNo, string objVal)
        {
            luAppNo.Visible = false;
            txtAppNo.Visible = true;
            txtAppNo.Text = appNo;

            var dtDetail = GetListNoApp(appNo);
            txtDebitur.Text = dtDetail.Rows[0].Field<string>("NAME");
            txtDebitur.Enabled = false;
            txtTglLahirDeb.Text = dtDetail.Rows[0].Field<string>("INBORNDT");
            txtTglLahirDeb.Enabled = false;
            txtMaritalStat.Text = dtDetail.Rows[0].Field<string>("INMARITAL");
            txtMaritalStat.Enabled = false;
            txtSpouName.Text = dtDetail.Rows[0].Field<string>("INSPOUNAME");
            txtSpouName.Enabled = false;
            mmAddress.Text = dtDetail.Rows[0].Field<string>("ADDRESS1");
            mmAddress.Enabled = false;
            txtPhoneDebitur.Text = dtDetail.Rows[0].Field<string>("PHONE");
            txtPhoneDebitur.Enabled = false;
            txtPenjamin.Text = dtDetail.Rows[0].Field<string>("INJAMIN");
            txtPenjamin.Enabled = false;
            txtPhonePenjamin.Text = dtDetail.Rows[0].Field<string>("INJAMTELP");
            txtPhonePenjamin.Enabled = false;
            mmAddressPenjamin.Text = dtDetail.Rows[0].Field<string>("INJAMADD1");
            mmAddressPenjamin.Enabled = false;
            txtRelation.Text = dtDetail.Rows[0].Field<string>("INJAMHUB");
            txtRelation.Enabled = false;

            //load DBR amount
            var dtDBR = GetlistDBR(appNo);
            if (dtDBR.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDBR.Rows)
                {
                    txtIncome.Text = dr["income"].ToString();
                    txtIncome.Enabled = false;
                    txtInsSLIK.Text = dr["ins_slik"].ToString();
                    txtInsSLIK.Enabled = false;
                    txtInsDeb.Text = dr["ins_deb"].ToString();
                    txtInsDeb.Enabled = false;
                    txtInsSpouse.Text = dr["ins_spouse"].ToString();
                    txtInsSpouse.Enabled = false;
                    txtInsChild.Text = dr["ins_child"].ToString();
                    txtInsChild.Enabled = false;
                    txtInsOther.Text = dr["ins_other"].ToString();
                    txtInsOther.Enabled = false;
                    txtDBR.Text = dr["dbr"].ToString();
                    txtDBR.Enabled = false;
                    txtBiayaHidup.Text = dr["biayahidup"].ToString();
                    txtBiayaHidup.Enabled = false;
                    txtFreeCashFlow.Text = dr["freecashflow"].ToString();
                    txtFreeCashFlow.Enabled = false;

                    txtIncomeOther.Text = dr["income_other"].ToString();
                    txtIncomeOther.Enabled = false;
                    txtIncomePenjamin.Text = dr["income_penjamin"].ToString();
                    txtIncomePenjamin.Enabled = false;
                    txtIncomePasanganPenjamin.Text = dr["income_penjamin_spouse"].ToString();
                    txtIncomePasanganPenjamin.Enabled = false;

                    if(dr["remarks"] != null)
                    {
                        mmRemarks.Text = dr["remarks"].ToString();
                    }
                    mmRemarks.Enabled = false;
                }
            }

            //load detail item survey
            var dtSurvey = LoadListSurveyItem(appNo, objVal);
            cbSurveyObj.Value = objVal;

            gvSurveyItem.Visible = false;
            gvLoadSurvey.Visible = true;
            gvLoadSurvey.DataSource = dtSurvey;
            gvLoadSurvey.DataBind();

            cbSurveyObj.Enabled = false;
            btnSave.Visible = false;

        }

        DataTable GetListNoApp(string appNo)
        {
            string ssql = "exec sp_MNCL_SurveyAppList '" + appNo + "'";

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


        DataTable GetListSurveyItem()
        {
            string ssql = "select ROW_NUMBER() OVER (ORDER BY survey_item) [No], survey_item [Item], '' [Jawaban] from mstSurveyItem " +
                "where survey_object ='" + cbSurveyObj.SelectedItem.Text + "'";
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

        DataTable LoadListSurveyItem(string appNo, string objVal)
        {
            string ssql = "select ROW_NUMBER() OVER (ORDER BY survey_item) [No], survey_item [Item], survey_object [Object], agreement [Jawaban] from trxSurveyTask " +
                            "where applicno = '" + appNo + "' AND survey_object = '" + objVal + "'";
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

        DataTable GetlistSurveyObj()
        {
            string ssql = "select distinct survey_object, survey_object from mstSurveyItem";
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

        DataTable GetlistDBR(string appNo)
        {
            string ssql = "select * from trxSurveyDBR where applicno = '" + appNo + "'";
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

        bool NoAppExist(string NoApp)
        {
            bool isExist = false;

            string ssql = "select applicno from trxSurveyTask where applicno = '" + NoApp + "'";
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
                    isExist = true;
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

            return isExist;
        }

        public decimal GetAngsuranSLIK(string appNo)
        {
            decimal retNum = 0;

            string ssql = "select a.RefNo, SUM(b.Angsuran) [Angsuran] from [dbo].[UpdateSLIK] a " +
                "left join [dbo].[UpdateSLIKDetail] b on a.DocKey = b.DocKey " +
                "where a.RefNo = '" + appNo + "' group by a.RefNo";
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(ssql);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //var test1 = reader.GetDecimal(1);
                        retNum = reader.GetDecimal(1);
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

            return retNum;
        }
        private void FillSurveyObj()
        {
            cbSurveyObj.DataSource = GetlistSurveyObj();
            cbSurveyObj.ValueField = "survey_object";
            cbSurveyObj.ValueType = typeof(string);
            cbSurveyObj.TextField = "survey_object";
            cbSurveyObj.DataBind();
        }

        private void InsertDBR(string applicno, string income, string insslik, string insdeb, string insspouse, string inschild, string insothers, string dbr, string costliving, string fcashflow, string income_otr, string income_penj, string income_penj_spouse)
        {
            string cmd = "insert trxSurveyDBR(applicno, income, ins_slik, ins_deb, ins_spouse, ins_child, ins_other, dbr, cr_user, cr_date, biayahidup, freecashflow, income_other, income_penjamin, income_penjamin_spouse, remarks) " +
                "values('" + applicno + "', " + income + "," + insslik + "," + insdeb + "," + insspouse + "," + inschild + "," + insothers + "," + dbr + ",'" + UserID.ToString() + 
                "',getdate(), " + costliving + ", " + fcashflow + ", " + income_otr + ", " + income_penj + ", " + income_penj_spouse + ",'" + mmRemarks.Text + "'" +   ")";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(cmd);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
        }

        private void InsertSurvey(string applicno, string survey, string agreement)
        {
            string cmd = "insert into trxSurveyTask(applicno,survey_object,survey_item,agreement,cr_user,cr_date) " +
                "values('" + applicno + "', '" + cbSurveyObj.SelectedItem.Value.ToString() + "', '" + survey + "', '" + agreement + "', '" + UserID + "', getdate())";
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(cmd);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
        }
    }
}