using DevExpress.Pdf;
using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.PrintSheetControl
{
    public partial class PrintSheetControl : BasePage
    {
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(false); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlLocalDBSetting myDBLocalSetting
        {
            get { isValidLogin(false); return (SqlLocalDBSetting)HttpContext.Current.Session["myDBLocalSetting" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBLocalSetting" + this.ViewState["_PageID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDBSession" + this.ViewState["_PageID"]] = value; }
        }
        protected PrintSheetControlDB myPrintSheetControl
        {
            get { isValidLogin(false); return (PrintSheetControlDB)HttpContext.Current.Session["myPrintSheetControl" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myPrintSheetControl" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable mySheetControlTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mySheetControlTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mySheetControlTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myAccNoTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myAccNoTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAccNoTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDocMandTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDocMandTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDocMandTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDocAddTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDocAddTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDocAddTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDataTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDataTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDataTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myClientTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myClientTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myClientTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myAkteNotarisTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myAkteNotarisTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAkteNotarisTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myPengurusTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myPengurusTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myPengurusTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myPemegangSahamTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myPemegangSahamTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myPemegangSahamTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myDetailAssetTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myDetailAssetTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDetailAssetTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myExistTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myExistTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myExistTable" + this.ViewState["_PageID"]] = value; }
        }
        protected DataSet myDataSet
        {
            get { isValidLogin(false); return (DataSet)HttpContext.Current.Session["myDataSet" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myDataSet" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myDBLocalSetting = localdbsetting;
                myDBSession = dbsession;
                this.myPrintSheetControl = PrintSheetControlDB.Create(myDBSetting, myDBLocalSetting, dbsession);
                mySheetControlTable = new DataTable();
                myDataTable = new DataTable();
                myAccNoTable = new DataTable();
                myDocMandTable = new DataTable();
                myDocAddTable = new DataTable();

                myClientTable = new DataTable();
                myAkteNotarisTable = new DataTable();
                myPengurusTable = new DataTable();
                myPemegangSahamTable = new DataTable();
                myDetailAssetTable = new DataTable();
                myExistTable = new DataTable();
                myDataSet = new DataSet();

                myAccNoTable = myPrintSheetControl.LoadDataKontrak();
                //myAccNoTable = myDBSetting.GetDataTable(@"SELECT a.APPLICNO AS APPLICNO, a.LSAGREE[NO KONTRAK], a.LESSEE[CLIENT], UPPER(a.NAME)[DEBITUR]
                //                                            FROM LS_AGREEMENT a
                //                                            INNER JOIN SYS_COMPANY b ON b.C_CODE = a.C_CODE
                //                                            WHERE a.CONTRACT_STATUS NOT IN ('GOLIVE', 'TERMINATE', 'REMOVED', 'NEW', 'OVERWRITE')", false);
                luAccNo.DataSource = myAccNoTable;
                luAccNo.DataBind();

                mySheetControlTable = myPrintSheetControl.LoadDataSheetControl();
                gvMain.DataSource = mySheetControlTable;
                gvMain.DataBind();

                myDocMandTable = myPrintSheetControl.LoadDataDocMand();
                ASPxListBox listMandDoc = ((ASPxListBox)ddeMandDoc.FindControl("lbMandDoc"));
                listMandDoc.DataSource = myDocMandTable;
                listMandDoc.DataBind();

                myDocAddTable = myPrintSheetControl.LoadDataDocAdd();
                ASPxListBox listAddDoc = ((ASPxListBox)ddeAddDoc.FindControl("lbAddDoc"));
                listAddDoc.DataSource = myDocAddTable;
                listAddDoc.DataBind();

                Accessable();
            }
            if (!IsCallback)
            {

            }
        }
        private bool Save(SaveAction saveAction)
        {
            bool bSave = true;
            GetEntity();
            if (!CheckDuplicate())
            {
                myPrintSheetControl.Save(myDataTable, myClientTable, myAkteNotarisTable, myPengurusTable, myPemegangSahamTable, myDetailAssetTable, SaveAction.Save, UserName);
            }
            else
            {
                myPrintSheetControl.Update(myDataTable, myClientTable, myAkteNotarisTable, myPengurusTable, myPemegangSahamTable, myDetailAssetTable, SaveAction.Update, UserName);
            }
            return bSave;
        }
        private bool SendComment(SaveAction saveAction)
        {
            bool bSendComment = true;
            myPrintSheetControl.SendComment(myDataTable, SaveAction.SaveComment, UserName);
            return bSendComment;
        }
        public void GetEntity()
        {
            DataRow dataRow;
            myDataSet = myPrintSheetControl.LoadDataClient(Convert.ToString(luAccNo.GridView.GetRowValues(luAccNo.GridView.FocusedRowIndex, "CLIENT")));

            myClientTable = myDataSet.Tables[0];
            myAkteNotarisTable = myDataSet.Tables[1];
            myPengurusTable = myDataSet.Tables[2];
            myPemegangSahamTable = myDataSet.Tables[3];
            myDetailAssetTable = myDataSet.Tables[4];

            myDataTable.Clear();
            myDataTable = myDBLocalSetting.GetDataTable("SELECT TOP 0 * FROM [dbo].[SheetControl]", false);
            dataRow = myDataTable.NewRow();
            dataRow["AppNo"] = luAccNo.Value;
            dataRow["JenisPengikatan"] = cbBindingType.Value;
            dataRow["RO"] = myDBSetting.ExecuteScalar("SELECT B.C_NAME FROM [dbo].[LS_APPLICATION] A LEFT JOIN [dbo].[SYS_COMPANY] B ON A.C_CODE = B.C_CODE WHERE A.APPLICNO=?", luAccNo.Value);
            dataRow["CRNo"] = txtCRNo.Value;
            dataRow["CRDate"] = Convert.ToString(deCR.Value).Length > 1 ? deCR.Value : "1970-01-01 07:00:00.000";
            dataRow["CAMNo"] = txtCAMNo.Value;
            dataRow["CAMDate"] = Convert.ToString(deCAM.Value).Length > 1 ? deCAM.Value : "1970-01-01 07:00:00.000";
            dataRow["DOCMAND"] = mmMandDoc.Value;
            dataRow["DOCADDI"] = mmAddDoc.Value;
            dataRow["LegalConclution"] = mmLegalCon.Value;
            dataRow["UncompletedDoc"] = mmDefDoc.Value;
            dataRow["FooterMadeBy"] = txtMadeBy.Value;
            dataRow["FooterMadeByPos"] = txtMadeByPos.Value;
            dataRow["FooterApprovedBy"] = txtApprovedBy.Value;
            dataRow["FooterApprovedByPos"] = txtApprovedByPos.Value;
            dataRow["FooterMarketing"] = txtMarketing.Value;
            dataRow["FooterMarketingPos"] = txtMarketingPos.Value;
            dataRow["FooterBusinessManager"] = txtBusinessManager.Value;
            dataRow["FooterBusinessManagerPos"] = txtBusinessManagerPos.Value;
            myDataTable.Rows.Add(dataRow);
        }
        private bool CheckExist()
        {
            bool bExist = false;

            cplMain.JSProperties["cpJenisPengikatan"] = "";
            cplMain.JSProperties["cpAssetType"] = "";
            cplMain.JSProperties["cpCRNo"] = "";
            cplMain.JSProperties["cpCRDate"] = null;
            cplMain.JSProperties["cpCAMNo"] = "";
            cplMain.JSProperties["cpCAMDate"] = null;
            cplMain.JSProperties["cpDocMand"] = "";
            cplMain.JSProperties["cpDocAddi"] = "";
            cplMain.JSProperties["cpLegalConclution"] = "";
            cplMain.JSProperties["cpUncompletedDoc"] = "";
            cplMain.JSProperties["cpFooterMadeBy"] = "";
            cplMain.JSProperties["cpFooterMadeByPos"] = "";
            cplMain.JSProperties["cpFooterApprovedBy"] = "";
            cplMain.JSProperties["cpFooterApprovedByPos"] = "";
            cplMain.JSProperties["cpFooterMarketing"] = "";
            cplMain.JSProperties["cpFooterMarketingPos"] = "";
            cplMain.JSProperties["cpFooterBusinessManager"] = "";
            cplMain.JSProperties["cpFooterBusinessManagerPos"] = "";

            myExistTable = localdbsetting.GetDataTable("SELECT * FROM [dbo].[SheetControl] WHERE AppNo=?", false, luAccNo.Value);
            if (myExistTable.Rows.Count > 0)
            {
                DataRow myrow = myExistTable.Rows[0];
                cplMain.JSProperties["cpJenisPengikatan"] = Convert.ToString(myrow["JenisPengikatan"]);
                cplMain.JSProperties["cpCRNo"] = Convert.ToString(myrow["CRNo"]);
                cplMain.JSProperties["cpCRDate"] = Convert.ToDateTime(myrow["CRDate"]);
                cplMain.JSProperties["cpCAMNo"] = Convert.ToString(myrow["CAMNo"]);
                cplMain.JSProperties["cpCAMDate"] = Convert.ToDateTime(myrow["CAMDate"]);
                cplMain.JSProperties["cpDocMand"] = Convert.ToString(myrow["DocMand"]);
                cplMain.JSProperties["cpDocAddi"] = Convert.ToString(myrow["DocAddi"]);
                cplMain.JSProperties["cpLegalConclution"] = Convert.ToString(myrow["LegalConclution"]);
                cplMain.JSProperties["cpUncompletedDoc"] = Convert.ToString(myrow["UncompletedDoc"]);
                cplMain.JSProperties["cpFooterMadeBy"] = Convert.ToString(myrow["FooterMadeBy"]);
                cplMain.JSProperties["cpFooterMadeByPos"] = Convert.ToString(myrow["FooterMadeByPos"]);
                cplMain.JSProperties["cpFooterApprovedBy"] = Convert.ToString(myrow["FooterApprovedBy"]);
                cplMain.JSProperties["cpFooterApprovedByPos"] = Convert.ToString(myrow["FooterApprovedByPos"]);
                cplMain.JSProperties["cpFooterMarketing"] = Convert.ToString(myrow["FooterMarketing"]);
                cplMain.JSProperties["cpFooterMarketingPos"] = Convert.ToString(myrow["FooterMarketingPos"]);
                cplMain.JSProperties["cpFooterBusinessManager"] = Convert.ToString(myrow["FooterBusinessManager"]);
                cplMain.JSProperties["cpFooterBusinessManagerPos"] = Convert.ToString(myrow["FooterBusinessManagerPos"]);
                bExist = true;
            }
            return bExist;
        }
        public bool CheckDuplicate()
        {
            bool bDuplicate = false;
            object obj = myDBLocalSetting.ExecuteScalar("SELECT AppNo FROM [dbo].[SheetControl] WHERE AppNo=?", luAccNo.Value);
            if (obj != null && obj != DBNull.Value)
            {
                bDuplicate = true;
            }
            return bDuplicate;
        }
        private void Accessable()
        {
            #region Control Color
            mmMandDoc.BackColor = mmMandDoc.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            mmAddDoc.BackColor = mmAddDoc.ReadOnly == true ? System.Drawing.Color.Transparent : System.Drawing.Color.White;
            #endregion
        }
        protected bool ErrorInField(out string strmessageError, SaveAction saveaction)
        {
            bool errorF = false;
            bool focusF = false;
            strmessageError = "";
            cplMain.JSProperties["cplActiveTabIndex"] = 0;
            return errorF;
        }
        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string urlsave = "";
            urlsave = "~/Reporting/CreditProcess/PrintSheetControl/PrintSheetControl.aspx";
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlConnection SQLConn = new SqlConnection(dbsetting.ConnectionString);
            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
            string strmessageError = "";

            switch (callbackParam[0].ToUpper())
            {
                case "PRINT":
                    cplMain.JSProperties["cpDocNo"] = myrow["DocNo"].ToString();
                    cplMain.JSProperties["cpAppNo"] = myrow["AppNo"].ToString();
                    cplMain.JSProperties["cpTipe"] = myrow["Tipe"].ToString();
                    break;
                case "GENERATE":
                    Save(SaveAction.Save);
                    if (chkSendComment.CheckState == CheckState.Checked)
                    {
                        SendComment(SaveAction.SaveComment);
                    }
                    cplMain.JSProperties["cpAlertMessage"] = "Application has been generate to sheet control.";
                    cplMain.JSProperties["cplblActionButton"] = "GENERATE";
                    ASPxWebControl.RedirectOnCallback(urlsave);
                    break;
                case "GENERATE_CONFIRM":
                    cplMain.JSProperties["cpAppNo"] = luAccNo.Value;
                    cplMain.JSProperties["cpJenisPengikatan"] = cbBindingType.Value;
                    cplMain.JSProperties["cpAssetType"] = "";//cbAssetType.Value;
                    cplMain.JSProperties["cplblmessageError"] = "";
                    cplMain.JSProperties["cplblmessage"] = "are you sure want to generate this application to Sheet Control?, Please becareful if application number are exist, it will be overwrited!";
                    cplMain.JSProperties["cplblActionButton"] = "GENERATE";
                    if (ErrorInField(out strmessageError, SaveAction.Save))
                    {
                        cplMain.JSProperties["cplblmessageError"] = strmessageError;
                    }
                    break;
                case "APP_NO_ONCHANGE":
                    CheckExist();
                    GetEntity();
                    gvDetailAsset.DataBind();
                    break;
            }
        }
        protected void gvMain_Init(object sender, EventArgs e)
        {

        }
        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = mySheetControlTable;
        }
        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }
        protected void gvMain_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        protected void luAccNo_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridLookup).DataSource = myAccNoTable;
        }
        protected void lbMandDoc_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxListBox).DataSource = myDocMandTable;
        }
        protected void lbAddDoc_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxListBox).DataSource = myDocAddTable;
        }
        protected void gvDetailAsset_DataBinding(object sender, EventArgs e)
        {
            DataTable mytable = new DataTable();
            mytable = myDBLocalSetting.GetDataTable(@"select B.DocNo, A.ItemDescription, A.Year, A.Condition, A.AssetTypeDetail 
                                                        from [dbo].[ApplicationDetail] A 
                                                            inner join[dbo].[Application] B ON A.DocKey = B.DocKey 
                                                                WHERE B.DocNo=?", false, (object)luAccNo.Value);

            (sender as ASPxGridView).DataSource = mytable;
        }
        protected void gvDetailAsset_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "No")
            {
                e.DisplayText = (e.VisibleIndex + 1).ToString();
            }
        }
    }
}