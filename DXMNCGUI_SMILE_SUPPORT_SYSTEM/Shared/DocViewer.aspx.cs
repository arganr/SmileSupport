using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DevExpress.XtraReports.Security;

using System.Data;
using System.Data.SqlClient;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Documents;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Shared
{
    public partial class DocViewer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptPermissionManager.GlobalInstance = new ScriptPermissionManager(ExecutionMode.Unrestricted);
            if (this.Request.QueryString["Title"] != null)
                pnlMain.HeaderText = this.Request.QueryString["Title"].ToString();

            if (this.Request.QueryString["ReportType"] != null)
            {
                switch (this.Request.QueryString["ReportType"].ToString().ToUpper())
                {
                    case "PSC":
                            if (this.Request.QueryString["DocNo"] == null && this.Request.QueryString["AppNo"] == null) return;
                            docPrintSheetControl docPrintSheetControl = new docPrintSheetControl();
                            docPrintSheetControl.Parameters["DocNo"].Value = this.Request["DocNo"];
                            docPrintSheetControl.Parameters["AppNo"].Value = this.Request["AppNo"];

                            DevExpress.DataAccess.Sql.SqlDataSource dsLK = docPrintSheetControl.DataSource as DevExpress.DataAccess.Sql.SqlDataSource;
                            dsLK.ConnectionName = "SqlLocalConnectionString";
                            //dsLK.ConnectionParameters = new DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters(".\\MNCGUI", "SSS", "sa", "invictus14", DevExpress.DataAccess.ConnectionParameters.MsSqlAuthorizationType.SqlServer);
                            dsLK.ConnectionParameters = new DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters("MNCGUI-NONCORE", "SSS", "applogin", "Gr3atMNC", DevExpress.DataAccess.ConnectionParameters.MsSqlAuthorizationType.SqlServer);

                            dvrMain.Report = docPrintSheetControl;
                        break;

                    case "PSC-B":
                            if (this.Request.QueryString["DocNo"] == null && this.Request.QueryString["AppNo"] == null) return;
                            docPrintSheetControl_B docPrintSheetControl_B = new docPrintSheetControl_B();
                            docPrintSheetControl_B.Parameters["DocNo"].Value = this.Request["DocNo"];
                            docPrintSheetControl_B.Parameters["AppNo"].Value = this.Request["AppNo"];

                            DevExpress.DataAccess.Sql.SqlDataSource dsLK_B = docPrintSheetControl_B.DataSource as DevExpress.DataAccess.Sql.SqlDataSource;
                            dsLK_B.ConnectionName = "SqlLocalConnectionString";
                            //dsLK_B.ConnectionParameters = new DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters(".\\MNCGUI", "SSS", "sa", "invictus14", DevExpress.DataAccess.ConnectionParameters.MsSqlAuthorizationType.SqlServer);
                            dsLK_B.ConnectionParameters = new DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters("MNCGUI-NONCORE", "SSS", "applogin", "Gr3atMNC", DevExpress.DataAccess.ConnectionParameters.MsSqlAuthorizationType.SqlServer);

                            dvrMain.Report = docPrintSheetControl_B;
                        break;
                    case "RESCHDULING":
                        docReschdulingSimulation docReschdulingSimulation = new docReschdulingSimulation();
                        docReschdulingSimulation.Parameters["parameter1"].Value = this.Request.QueryString["DocNo"];
                        docReschdulingSimulation.Parameters["parameter1"].Value = Session["DocNo"];
                        dvrMain.Report = docReschdulingSimulation;
                        break;
                }
            }
        }
    }
}