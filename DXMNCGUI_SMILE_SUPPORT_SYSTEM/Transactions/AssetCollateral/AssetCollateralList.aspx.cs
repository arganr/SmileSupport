using DevExpress.Web;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.AssetCollateral
{
    public partial class AssetCollateralList : BasePage
    {
        DateTime datenow = DateTime.Now;
        public List<int> MergedIndexList = new List<int>();
        protected SqlDBSetting myDBSetting
        {
            get { isValidLogin(); return (SqlDBSetting)HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSetting" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected SqlDBSession myDBSession
        {
            get { isValidLogin(false); return (SqlDBSession)HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]]; }
            set { HttpContext.Current.Session["myDBSession" + HttpContext.Current.Session["UserID"]] = value; }
        }
        protected AssetCollateralDB myAssetCollateralDB
        {
            get { isValidLogin(false); return (AssetCollateralDB)HttpContext.Current.Session["myAssetCollateralDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAssetCollateralDB" + this.ViewState["_PageID"]] = value; }
        }
        protected AssetCollateralEntity myAssetCollateralEntity
        {
            get { isValidLogin(false); return (AssetCollateralEntity)HttpContext.Current.Session["myAssetCollateralEntity" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myAssetCollateralEntity" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myMainTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myMainTable" + this.ViewState["_PageID"]] = value; }
        }

        protected void gvMain_Init(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();

                myDBSetting = dbsetting;
                myDBSession = dbsession;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["_PageID"] = Guid.NewGuid();
                isValidLogin();
                myDBSetting = dbsetting;
                myDBSession = dbsession;
                LoadData();
            }
        }
        protected void LoadData()
        {
            string sQuery = "";
            sQuery = @"SELECT
                            C.AST_CODE,
                            C.AST_NAME,
                            A.LOCATION,
                            A.DETAIL_LOCATION,
                            A.REMARKS,
                            B.CRE_BY,
                            B.CRE_DATE,
                            B.DATE,
                            B.FROM_LOC,
                            B.TO_LOC,
                            B.PROMISE_DATE
                            FROM [dbo].[ASSET_COLLATERAL_LOCATION] A
                            INNER JOIN [dbo].[ASSET_COLLATERAL_LOCATION_HIST] B ON A.AST_CODE = B.AST_CODE
                            INNER JOIN [dbo].[FA_ASSETREGISTER] C ON A.AST_CODE = C.AST_CODE
                            ORDER BY B.AST_CODE, B.ID DESC";
            myMainTable = new DataTable();
            myMainTable = myDBSetting.GetDataTable(sQuery, false);
            gvMain.DataSource = myMainTable;
            gvMain.DataBind();
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void gvMain_CustomCellMerge(object sender, DevExpress.Web.ASPxGridViewCustomCellMergeEventArgs e)
        {
            if (e.Column.FieldName == "AST_CODE")
            {
                e.Handled = true;
                if ((string)e.Value1 == (string)e.Value2)
                {
                    MergedIndexList.Add(e.RowVisibleIndex1);
                    e.Merge = true;
                }
            }
            else if (e.Column.FieldName == "AST_NAME")
            {
                e.Handled = true;
                if ((string)e.Value1 == (string)e.Value2)
                {
                    MergedIndexList.Add(e.RowVisibleIndex1);
                    e.Merge = true;
                }
            }
            else if (e.Column.FieldName == "LOCATION")
            {
                e.Handled = true;
                if ((string)e.Value1 == (string)e.Value2)
                {
                    MergedIndexList.Add(e.RowVisibleIndex1);
                    e.Merge = true;
                }
            }
            else if (e.Column.FieldName == "DETAIL_LOCATION")
            {
                e.Handled = true;
                if ((string)e.Value1 == (string)e.Value2)
                {
                    MergedIndexList.Add(e.RowVisibleIndex1);
                    e.Merge = true;
                }
            }
            else if (e.Column.FieldName == "REMARKS")
            {
                e.Handled = true;
                if ((string)e.Value1 == (string)e.Value2)
                {
                    MergedIndexList.Add(e.RowVisibleIndex1);
                    e.Merge = true;
                }
            }
            else
            { e.Handled = true; e.Merge = false; }
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myMainTable;
        }
    }
}