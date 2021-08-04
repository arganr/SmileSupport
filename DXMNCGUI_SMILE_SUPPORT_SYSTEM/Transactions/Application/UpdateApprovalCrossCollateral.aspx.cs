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

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application
{
    public partial class UpdateApprovalCrossCollateral : BasePage
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
        protected string roleCrossColUser
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["roleCrossColUser" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["roleCrossColUser" + this.ViewState["_PageID"]] = value; }
        }
        protected string strKey
        {
            get { isValidLogin(false); return (string)HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["strKey" + this.ViewState["_PageID"]] = value; }
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
                tmpDtTable = new DataTable();
                logDtTable = new DataTable();
                roleCrossColUser = GetUserRole();

                if (this.Request.QueryString["Key"] != null)
                {
                    strKey = this.Request.QueryString["Key"].ToString();
                    tmpDtTable = GetListData();
                    gvTempData.DataSource = tmpDtTable;
                    gvTempData.DataBind();

                    logDtTable = GetLogData();
                    gvLogData.DataSource = logDtTable;
                    gvLogData.DataBind();

                    bindData();
                }
                else
                {
                    Response.Redirect("~/Transactions/Application/ListApprovalCrossCollateral.aspx");
                }
                
                //loadDataExist("");
            }
            if (!IsCallback)
            {

            }
        }

        protected void gvTempData_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = tmpDtTable;
        }

        protected void gvLogData_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = logDtTable;
        }

        protected void bindData()
        {
            foreach (DataRow row in tmpDtTable.Rows)
            {
                txtCrossColName.Text = row["CROSSCOL"].ToString();
            }
        }

        String GetUserRole()
        {
            string resRole = "";
            string ssql = "select CMDid from AccessRight where CMDid = 'CROSSCOL_CAN_REMOVE' AND NIK = '" + UserID + "'";
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
                        resRole = row["CMDid"].ToString();
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

            return resRole;
        }

        DataTable GetListData()
        {
            //string ssql = "select CODE, DESCRIPTION from LS_CROSS_COLLATERAL_H";
            string ssql = "select a.LSAGREE,c.DESCRIPTION [CROSSCOL],a.NAME,a.ASSET_DESCS, " +
                "case when isnull(b.update_type, '') <> 'DELETE' then '' else isnull(b.remarks, '') end[REMARKS], " +
                "case when isnull(b.update_type, '') <> 'DELETE' then '' else b.update_type end[updateType], a.CODE " +
                "from LS_CROSS_COLLATERAL_D a left join LS_CROSS_COLLATERAL_APPROVAL b on a.CODE = b.id_crosscol and a.LSAGREE = b.no_agreement " +
                "and status_approval = 'PENDING APPROVAL' left join LS_CROSS_COLLATERAL_H c on a.CODE = c.CODE " +
                "where a.CODE = '" + strKey + "'";


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

        DataTable GetLogData()
        {
            //string ssql = "select CODE, DESCRIPTION from LS_CROSS_COLLATERAL_H";
            string ssql = "select a.no_agreement, b.NAME, a.remarks, a.status_approval, a.update_type, d.USER_NAME from [LS_CROSS_COLLATERAL_APPROVAL_LOG] a " +
                            "LEFT JOIN LS_AGREEMENT b on a.no_agreement = b.LSAGREE LEFT JOIn MASTER_USER d on a.cre_by = d.USER_ID " +
                            "where a.id_crosscol = '" + strKey + "' ORDER BY a.id desc";

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

        protected void UpdateApproval(string ccode, string approval, string agreement)
        {
            string ssql;
            ssql = "update LS_CROSS_COLLATERAL_APPROVAL set status_approval = '" + approval + "' where id_crosscol = '" + ccode + "' and no_agreement = '" + agreement + "'";
            using (SqlConnection conn = new SqlConnection(myDBSetting.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(ssql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        protected void RemoveCrossCol(string ccode, string agreement)
        {
            string ssql;
            ssql = "delete LS_CROSS_COLLATERAL_D where CODE = '" + ccode + "' and LSAGREE = '" + agreement + "'";
            using (SqlConnection conn = new SqlConnection(myDBSetting.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(ssql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        protected void LogCrossCol(string ccode, string agreement)
        {
            string ssql;
            ssql = "insert into LS_CROSS_COLLATERAL_APPROVAL_LOG " +
                    "select id_crosscol [ID_CROSSCOL], no_agreement, remarks [REMARKS], status_approval [APPROVAL], update_type [TYPE_UPDATE], " +
                    "'" + UserID + "', GETDATE(), '" + UserID + "', GETDATE() from LS_CROSS_COLLATERAL_APPROVAL where id_crosscol = '" + ccode + "' and no_agreement = '" + agreement + "'";
            using (SqlConnection conn = new SqlConnection(myDBSetting.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(ssql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        protected void gvTempData_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {

        }

        protected void gvTempData_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            if (e.DataColumn.FieldName == "updateType")
            {
                GridViewDataColumn dataColumn = grid.Columns[6] as GridViewDataColumn;
                //ASPxCheckBox cbApprove = grid.FindRowTemplateControl(e.VisibleIndex, "chkItem") as ASPxCheckBox;
                ASPxCheckBox cbApprove = grid.FindRowCellTemplateControl(e.VisibleIndex, dataColumn, "chkItem") as ASPxCheckBox;
                if (e.CellValue.ToString() == "DELETE")
                {
                    cbApprove.Visible = true;
                }
                else
                {
                    cbApprove.Visible = false;
                }
            }

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            GridViewDataColumn dataColumn = gvTempData.Columns[6] as GridViewDataColumn;
            for (int i = 0; i < gvTempData.VisibleRowCount; i++)
            {
                string approval = string.Empty;
                var ccode = gvTempData.GetRowValues(i, "CODE");
                var lsagree = gvTempData.GetRowValues(i, "LSAGREE");
                ASPxCheckBox cbApprove = gvTempData.FindRowCellTemplateControl(i, dataColumn, "chkItem") as ASPxCheckBox;
                if (cbApprove.Checked == true)
                {
                    UpdateApproval(ccode.ToString(), "APPROVE",lsagree.ToString());
                    RemoveCrossCol(ccode.ToString(), lsagree.ToString());
                    LogCrossCol(ccode.ToString(), lsagree.ToString());
                }
            }

            //tmpDtTable = GetListData();
            //gvTempData.DataSource = tmpDtTable;
            //gvTempData.DataBind();

            Response.Redirect("~/Transactions/Application/ListApprovalCrossCollateral.aspx");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            GridViewDataColumn dataColumn = gvTempData.Columns[6] as GridViewDataColumn;
            for (int i = 0; i < gvTempData.VisibleRowCount; i++)
            {
                string approval = string.Empty;
                var ccode = gvTempData.GetRowValues(i, "CODE");
                var lsagree = gvTempData.GetRowValues(i, "LSAGREE");
                ASPxCheckBox cbApprove = gvTempData.FindRowCellTemplateControl(i, dataColumn, "chkItem") as ASPxCheckBox;
                if (cbApprove.Checked == true)
                {
                    UpdateApproval(ccode.ToString(), "REJECT", lsagree.ToString());
                    LogCrossCol(ccode.ToString(), lsagree.ToString());
                }
            }

            //tmpDtTable = GetListData();
            //gvTempData.DataSource = tmpDtTable;
            //gvTempData.DataBind();

            Response.Redirect("~/Transactions/Application/ListApprovalCrossCollateral.aspx");
        }

        
    }
}