using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.UpdateSLIK
{
    public partial class UpdateSLIKMaint : BasePage
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
        protected DataTable mytableSearch
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mytableSearch" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mytableSearch" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable mytableDetailSearch
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["mytableDetailSearch" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mytableDetailSearch" + this.ViewState["_PageID"]] = value; }
        }
        protected UpdateSLIKDB myUpdateSLIKDB
        {
            get { isValidLogin(false); return (UpdateSLIKDB)HttpContext.Current.Session["myUpdateSLIKDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUpdateSLIKDB" + this.ViewState["_PageID"]] = value; }
        }
        protected UpdateSLIKEntity myUpdateSLIKEntity
        {
            get { isValidLogin(false); return (UpdateSLIKEntity)HttpContext.Current.Session["myUpdateSLIKEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myUpdateSLIKEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected IContainer components
        {
            get { isValidLogin(false); return (IContainer)HttpContext.Current.Session["components" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["components" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;
                mytableSearch = new DataTable();
                mytableDetailSearch = new DataTable();
                this.myUpdateSLIKDB = UpdateSLIKDB.Create(myDBSetting, myLocalDBSetting, dbsession);

                if (!accessright.IsAccessibleByUserID(UserID, "VERIFICATION_TASK") && !accessright.IsAccessibleByUserID(UserID, "SLIK_MNTC_SHOW"))
                {
                    FormLayout1.FindItemOrGroupByName("New").Visible = false;
                }

                if (!accessright.IsAccessibleByUserID(UserID, "WLC_VIEW_ALL"))
                {
                    mytableSearch = this.myUpdateSLIKDB.LoadBrowseTable(false, myDBSession.LoginUserID);
                }
                else
                {
                    mytableSearch = this.myUpdateSLIKDB.LoadBrowseTable(true, myDBSession.LoginUserID);
                }
                gvMain.DataBind();
                accessable();

                if (this.Request.QueryString["DocNo"] != null)
                {
                    int indexVal = gvMain.FindVisibleIndexByKeyValue(this.Request.QueryString["DocNo"]);
                    gvMain.FocusedRowIndex = indexVal;
                }
                else
                {
                    gvMain.FocusedRowIndex = -1;
                }
            }
        }
        protected void OpenData(DXSSAction Action)
        {
            string updatedQueryString = "";
            try
            {
                DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                if (myrow != null)
                {
                    var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                    updatedQueryString = "?" + nameValues.ToString();
                    myUpdateSLIKEntity = myUpdateSLIKDB.Edit(Convert.ToInt32(myrow["DocKey"]), Action);
                    Response.Redirect("~/Transactions/CreditProcess/UpdateSLIK/UpdateSLIKEntry.aspx" + updatedQueryString);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "please select row first.." + "');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
        }
        protected void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose();
        }
        protected void accessable()
        {
            if (!accessright.IsAccessibleByUserID(UserID, "TICKET_CAN_GRAB"))
            {

            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            string updatedQueryString = "";
            try
            {
                myUpdateSLIKEntity = myUpdateSLIKDB.Entity(DXSSType.SLK);
                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                updatedQueryString = "?" + nameValues.ToString();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
            Response.Redirect("~/Transactions/CreditProcess/UpdateSLIK/UpdateSLIKEntry.aspx" + updatedQueryString);
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            OpenData(DXSSAction.View);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            OpenData(DXSSAction.View);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                isValidLogin();
                if (!accessright.IsAccessibleByUserID(UserID, "WLC_VIEW_ALL"))
                {
                    mytableSearch = this.myUpdateSLIKDB.LoadBrowseTable(false, myDBSession.LoginUserID);
                }
                else
                {
                    mytableSearch = this.myUpdateSLIKDB.LoadBrowseTable(true, myDBSession.LoginUserID);
                }
                gvMain.DataSource = mytableSearch;
                gvMain.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                return;
            }
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = mytableSearch;
        }

        protected void gvMain_FocusedRowChanged(object sender, EventArgs e)
        {

        }

        protected void gvMain_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string updatedQueryString = "";
            ASPxGridView gridView = (ASPxGridView)sender;
            string[] callbackParam = e.Parameters.ToString().Split(';');
            (sender as ASPxGridView).JSProperties["cpNewWindowUrl"] = null;
            gvMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            if (callbackParam.Length > 1)
            {
                object paramName = callbackParam[0].ToUpper();
                object paramValue = callbackParam[1].ToUpper();
                gridView.JSProperties["cplblmessageError"] = "";
                gridView.JSProperties["cplblActionButton"] = "";

                switch (callbackParam[0].ToUpper())
                {
                    case "INDEX":
                        int ivisibleindex = 0;
                        ivisibleindex = System.Convert.ToInt16(paramValue);
                        gvMain.JSProperties["cpVisibleIndex"] = ivisibleindex;
                        gridView.KeyFieldName = "DocNo";
                        break;
                    case "OPEN":
                        break;
                    case "REFRESH":
                        try
                        {
                            isValidLogin();
                            if (!accessright.IsAccessibleByUserID(UserID, "WLC_VIEW_ALL"))
                            {
                                mytableSearch = this.myUpdateSLIKDB.LoadBrowseTable(false, myDBSession.LoginUserID);
                            }
                            else
                            {
                                mytableSearch = this.myUpdateSLIKDB.LoadBrowseTable(true, myDBSession.LoginUserID);
                            }
                            gvMain.DataSource = mytableSearch;
                            gvMain.DataBind();
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                            return;
                        }
                        break;
                    case "DOUBLECLICK":
                        try
                        {
                            DataRow myrow = gvMain.GetDataRow(gvMain.FocusedRowIndex);
                            if (myrow != null)
                            {
                                var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                                nameValues.Set("Key", this.ViewState["_PageID"].ToString());
                                updatedQueryString = "?" + nameValues.ToString();
                                myUpdateSLIKEntity = myUpdateSLIKDB.Edit(Convert.ToInt32(myrow["DocNo"]), DXSSAction.Edit);
                                ASPxWebControl.RedirectOnCallback("~/Transactions/CreditProcess/UpdateSLIK/UpdateSLIKEntry.aspx" + updatedQueryString);
                            }
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                            return;
                        }
                        break;
                }
            }
        }

        protected void cplMain_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {

        }
    }
}