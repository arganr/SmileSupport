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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.PeminjamanDocument
{
    public partial class PeminjamanDocEntry : BasePage
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
        protected DataTable hdrDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["hdrDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["hdrDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable dtlDtTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["dtlDtTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["dtlDtTable" + this.ViewState["_PageID"]] = value; }
        }
        protected string userDoc
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["userDoc" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["userDoc" + this.ViewState["_PageID"]] = value; }
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
                hdrDtTable = new DataTable();
                dtlDtTable = new DataTable();
                strKey = "";

                if (this.Request.QueryString["Key"] != null)
                {
                    strKey = this.Request.QueryString["Key"].ToString();
                    hdrDtTable = getDataHeader(strKey);
                    dtlDtTable = getDataDetail(strKey);
                    gvDetail.DataSource = dtlDtTable;
                    gvDetail.DataBind();

                    bindData();
                }
                else
                {
                    Response.Redirect("PeminjamanDocList.aspx");
                }
            }
            if (!IsCallback)
            {

            }
        }

        protected void bindData()
        {
            //HEADER DATA BIND
            if (hdrDtTable.Rows.Count > 0)
            {
                foreach (DataRow dr in hdrDtTable.Rows)
                {
                    txtDocKey.Text = dr["DocKey"].ToString();
                    txtDocNo.Text = dr["DocNo"].ToString();
                    deDocDate.Value = dr["DocDate"].ToString();
                    txtCategory.Text = dr["DocCategory"].ToString();
                    txtDept.Text = dr["Department"].ToString();
                    //txtStatus.Text = dr["Status"].ToString();

                    string statsHDR = dr["Status"].ToString();
                    var findStatusHDR = cbStatusHDR.Items.FindByText(statsHDR);
                    if (findStatusHDR == null)
                    {
                        var statusValue = new ListEditItem(statsHDR, statsHDR);
                        cbStatusHDR.Items.Add(statusValue);

                        cbStatusHDR.ClientEnabled = false;
                        btnSave.Visible = false;
                    }
                    cbStatusHDR.Text = statsHDR;

                    txtKeperluan.Text = dr["Keperluan"].ToString();
                    txtRemarks.Text = dr["Remark"].ToString();
                    deTglPinjam.Value = dr["TglPeminjaman"].ToString();
                    deTglKembali.Value = dr["TglPengembalian"].ToString();

                    userDoc = dr["CreatedBy"].ToString();

                    if (dr["DecicionNote"] != DBNull.Value)
                    {
                        mmNoteApproval.Text = dr["DecicionNote"].ToString();
                    }
                    if (dr["DecicionDate"] != DBNull.Value)
                    {
                        deDateApproval.Value = dr["DecicionDate"].ToString();
                    }
                }
            }

            //DETAIL DATA BIND
            //if (dtlDtTable.Rows.Count > 0)
            //{
            //    if (cbStatusHDR.SelectedItem.Text == "CLOSE")
            //    {
            //        if (dtlDtTable.Rows.Count > 0)
            //        {
            //            GridViewDataColumn dataColumn = gvDetail.Columns[6] as GridViewDataColumn;
            //            for (int i = 0; i < gvDetail.VisibleRowCount; i++)
            //            {
            //                ASPxComboBox box = gvDetail.FindRowCellTemplateControl(i, dataColumn, "cbStatus") as ASPxComboBox;
            //                box.SelectedIndex = box.Items.IndexOfText("Close");
            //                box.ClientEnabled = false;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        GridViewDataColumn dataColumn = gvDetail.Columns[6] as GridViewDataColumn;
            //        for (int i = 0; i < gvDetail.VisibleRowCount; i++)
            //        {
            //            string agree = string.Empty;
            //            string statusDoc = gvDetail.GetRowValues(i, new string[] { "Status" }).ToString();
            //            ASPxComboBox box = gvDetail.FindRowCellTemplateControl(i, dataColumn, "cbStatus") as ASPxComboBox;
            //            box.SelectedIndex = box.Items.IndexOfText(statusDoc);
            //            box.ClientEnabled = true;
            //        }
            //    }
            //}

            //Check Approval State
            bool IsApprovalState = getApprovalState(txtDocKey.Text);
            if (IsApprovalState)
            {
                cbStatusHDR.ReadOnly = true;
                btnSave.Visible = false;
                btnApprove.Visible = true;
                btnReject.Visible = true;
                mmNoteApproval.ReadOnly = false;
            }
            //else
            //{
            //    ASPxFormLayout.FindItemOrGroupByName("NoteApproval").Visible = false;
            //}
        }

        protected DataTable getDataHeader(string value)
        {
            DataTable resDT = new DataTable();
            string ssql = "SELECT * FROM [INFORMA].[dbo].[PeminjamanDokumen] WHERE DocKey = ?";
            resDT = myLocalDBSetting.GetDataTable(ssql, false, value);
            return resDT;
        }

        protected DataTable getDataDetail(string value)
        {
            DataTable resDT = new DataTable();
            string ssql = "SELECT * FROM [INFORMA].[dbo].[PeminjamanDokumenDetail] WHERE DocKey = ?";
            resDT = myLocalDBSetting.GetDataTable(ssql, false, value);
            return resDT;
        }

        protected void gvDetail_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = dtlDtTable;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PeminjamanDocList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            string statusDoc = cbStatusHDR.SelectedItem.Text;

            updateStatusHDR(strKey, statusDoc);
            UpdateAllDetail(statusDoc);
            insertLog();

            apcalert.Text = "Save Data Success";
            apcalert.ShowOnPageLoad = true;

            Response.Redirect("PeminjamanDocList.aspx");
        }

        protected void UpdateAllDetail(string value)
        {
            if (dtlDtTable.Rows.Count > 0)
            {
                for (int i = 0; i < gvDetail.VisibleRowCount; i++)
                {
                    string idDtl = gvDetail.GetRowValues(i, new string[] { "DtlKey" }).ToString();
                    string idDoc = gvDetail.GetRowValues(i, new string[] { "DocID" }).ToString();

                    updateStatusDTL(idDtl, value);
                    if (value.ToUpper() == "ON USER")
                    {
                        updateMasterDoc(idDoc, getUsernameByID(userDoc));
                    }
                    else
                    {
                        updateMasterDoc(idDoc, "CUSTODIAN");
                    }
                    insertLogMasterDoc(idDoc);
                }
            }
        }

        protected bool checkAllStatus()
        {
            bool retBool = true;
            if (dtlDtTable.Rows.Count > 0)
            {
                GridViewDataColumn dataColumn = gvDetail.Columns[6] as GridViewDataColumn;
                for (int i = 0; i < gvDetail.VisibleRowCount; i++)
                {
                    ASPxComboBox box = gvDetail.FindRowCellTemplateControl(i, dataColumn, "cbStatus") as ASPxComboBox;
                    string boxValue = box.SelectedItem.Text;
                    if(boxValue != "Close")
                    {
                        retBool = false;
                    }
                }
            }

            return retBool;
        }

        protected void updateStatusHDR(string id, string value)
        {
            string ssql = "UPDATE [INFORMA].[dbo].[PeminjamanDokumen] SET Status = ?, LastModifiedBy = ?, LastModifiedDateTime = GETDATE() where DocKey = ?";
            myLocalDBSetting.ExecuteNonQuery(ssql, value, UserID, id);
        }

        protected void updateStatusDTL(string id, string value)
        {
            string ssql = "UPDATE [INFORMA].[dbo].[PeminjamanDokumenDetail] SET Status = ? where DtlKey = ?";
            myLocalDBSetting.ExecuteNonQuery(ssql, value, id);
        }

        protected void updateMasterDoc(string id, string value)
        {
            //string userDoc = getUsernameByID(userid);
            string ssql = "UPDATE [SSS].[dbo].[mstDocLocation] SET Location = ?, MOD_BY = ?, MOD_DATE = GETDATE() where DocID = ?";
            myLocalDBSetting.ExecuteNonQuery(ssql, value, UserID, id);
        }

        protected void insertLogMasterDoc(string id)
        {
            string ssql = @"insert into [SSS].[dbo].[mstDocLocation_log] 
                select a.DocID, a.DocCategory, a.Location, a.ReffNum, a.Description, ? CRE_BY, GETDATE() CRE_DATE, null, null, null 
                from [SSS].[dbo].[mstDocLocation] a where a.DocID = ?";
            myLocalDBSetting.ExecuteNonQuery(ssql, UserID, id);
        }
        
        protected void insertLog()
        {
            if (dtlDtTable.Rows.Count > 0)
            {
                int rowCount = 0;
                foreach (DataRow dr in dtlDtTable.Rows)
                {
                    string ssql = @"INSERT INTO [INFORMA].[dbo].[PeminjamanDokumenHistory] ([DocKey],[DocNo],[DocDate],[DocCategory],[Department],[PengajuanStatus],[Keperluan],[Remarks], 
                        [TglPeminjaman],[TglPengembalian],[DocID],[DocDesc],[DocStatus],[CreatedBy],[CreatedDate])
                        VALUES(@DocKey,@DocNo,@DocDate,@DocCategory,@Department,@PengajuanStatus,@Keperluan,@Remarks,@TglPeminjaman,@TglPengembalian,@DocID,@DocDesc,@DocStatus,@CreatedBy,GETDATE())";

                    SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
                    SqlCommand sqlCommand = new SqlCommand(ssql);
                    sqlCommand.Connection = myconn;
                    myconn.Open();

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                    sqlParameter1.Value = txtDocKey.Text;
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocNo", SqlDbType.VarChar);
                    sqlParameter2.Value = txtDocNo.Text;
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@DocDate", SqlDbType.DateTime);
                    sqlParameter3.Value = deDocDate.Date;
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@DocCategory", SqlDbType.VarChar);
                    sqlParameter4.Value = txtCategory.Text;
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Department", SqlDbType.VarChar);
                    sqlParameter5.Value = txtDept.Text;
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@PengajuanStatus", SqlDbType.VarChar);
                    sqlParameter6.Value = cbStatusHDR.SelectedItem.Text;
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@Keperluan", SqlDbType.VarChar);
                    sqlParameter7.Value = txtKeperluan.Text;
                    sqlParameter7.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@Remarks", SqlDbType.VarChar);
                    sqlParameter8.Value = txtRemarks.Text;
                    sqlParameter8.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@TglPeminjaman", SqlDbType.DateTime);
                    sqlParameter9.Value = deTglPinjam.Date;
                    sqlParameter9.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@TglPengembalian", SqlDbType.DateTime);
                    sqlParameter10.Value = deTglKembali.Date;
                    sqlParameter10.Direction = ParameterDirection.Input;

                    //Detail
                    //GridViewDataColumn dataColumn = gvDetail.Columns[6] as GridViewDataColumn;
                    //ASPxComboBox box = gvDetail.FindRowCellTemplateControl(rowCount, dataColumn, "cbStatus") as ASPxComboBox;
                    SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@DocID", SqlDbType.VarChar);
                    sqlParameter11.Value = gvDetail.GetRowValues(rowCount, new string[] { "DocID" }).ToString();
                    sqlParameter11.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@DocDesc", SqlDbType.VarChar);
                    sqlParameter12.Value = gvDetail.GetRowValues(rowCount, new string[] { "Description" }).ToString();
                    sqlParameter12.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@DocStatus", SqlDbType.VarChar);
                    sqlParameter13.Value = cbStatusHDR.SelectedItem.Text;
                    sqlParameter13.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.VarChar);
                    sqlParameter14.Value = UserID;
                    sqlParameter14.Direction = ParameterDirection.Input;

                    sqlCommand.ExecuteNonQuery();

                    myconn.Close();
                    rowCount += 1;
                }
            }
        }

        protected string getUsernameByID(string value)
        {
            string retUser = string.Empty;
            string ssql = "select USER_NAME from [SSS].[dbo].[MASTER_USER] WHERE USER_ID = ?";
            var dtUser = myLocalDBSetting.GetDataTable(ssql, false, value);

            if (dtUser.Rows.Count > 0)
            {
                DataRow row = dtUser.Rows[0];
                retUser = row["USER_NAME"].ToString();
            }

            return retUser;
        }

        protected bool getApprovalState(string dockey)
        {
            bool retState = false;
            string ssql = @"select b.UserID UserApproval from [INFORMA].[dbo].[PeminjamanDokumen] a left join [INFORMA].[dbo].[UserDokumenCategory] b on a.DocCategory = b.Category where a.DocKey = ? and b.UserID = ? and b.IsApprover = 'T' and a.Status = 'WAITING CUSTODIAN'";
            var dtApprover = myLocalDBSetting.GetDataTable(ssql, false, dockey, UserID);

            if (dtApprover.Rows.Count > 0)
            {
                retState = true;
            }
            
            return retState;
        }

        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            isValidLogin(false);
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            cplMain.JSProperties["cpVisible"] = null;

            switch (callbackParam[0].ToUpper())
            {
                case "LOAD":
                    break;
            }
        }

        protected void cbStatusHDR_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(cbStatusHDR.SelectedItem.Text == "CLOSE")
            //{
            //    if (dtlDtTable.Rows.Count > 0)
            //    {
            //        GridViewDataColumn dataColumn = gvDetail.Columns[6] as GridViewDataColumn;
            //        for (int i = 0; i < gvDetail.VisibleRowCount; i++)
            //        {
            //            ASPxComboBox box = gvDetail.FindRowCellTemplateControl(i, dataColumn, "cbStatus") as ASPxComboBox;
            //            box.SelectedIndex = box.Items.IndexOfText("Close");
            //            box.ClientEnabled = false;
            //        }
            //    }
            //}else
            //{
            //    bindData();
            //}
        }

        protected void UpdateApprover(string DocStatus, string DocKey, string DecicionApprover)
        {
            string ssql = "Update [INFORMA].[dbo].[PeminjamanDokumen] set Status = ?, CustodianDecision = ?, DecicionNote = ?, DecicionDate = GETDATE() WHERE DocKey = ?";
            myLocalDBSetting.ExecuteNonQuery(ssql, DocStatus, DecicionApprover, mmNoteApproval.Text, DocKey);
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            UpdateApprover("OPEN", txtDocKey.Text, "T");
            //UpdateAllDetail("Open");

            apcalert.Text = "Approve Data Success";
            apcalert.ShowOnPageLoad = true;

            Response.Redirect("PeminjamanDocList.aspx");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            UpdateApprover("REJECT BY CUSTODIAN", txtDocKey.Text, "F");
            //UpdateAllDetail("New");

            apcalert.Text = "Reject Data Success";
            apcalert.ShowOnPageLoad = true;

            Response.Redirect("PeminjamanDocList.aspx");
        }
    }
}