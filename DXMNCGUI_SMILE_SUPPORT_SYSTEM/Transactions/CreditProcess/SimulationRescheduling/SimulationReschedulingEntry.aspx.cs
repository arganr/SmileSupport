using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Reschedule;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.Data.Filtering;
using System.Drawing;
using DevExpress.Printing.ExportHelpers;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.SimulationRescheduling
{
    public partial class SimulationReschedulingEntry : BasePage
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
        protected SimulationReschedulingDB mySimulationReschedulingDB
        {
            get { isValidLogin(false); return (SimulationReschedulingDB)HttpContext.Current.Session["mySimulationReschedulingDB" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["mySimulationReschedulingDB" + this.ViewState["_PageID"]] = value; }
        }
        protected DataTable myFinalReturnTable
        {
            get { isValidLogin(false); return (DataTable)HttpContext.Current.Session["myFinalReturnTable" + this.ViewState["_PageID"]]; }
            set { HttpContext.Current.Session["myFinalReturnTable" + this.ViewState["_PageID"]] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            isValidLogin(false);
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Session["Log"] == null)
                    HttpContext.Current.Session["Log"] = "";

                this.ViewState["_PageID"] = Guid.NewGuid();
                myDBSetting = dbsetting;
                myLocalDBSetting = localdbsetting;
                myDBSession = dbsession;

                if (this.Request.QueryString["SourceKey"] != null && this.Request.QueryString["Type"] != null)
                {
                    this.mySimulationReschedulingDB = SimulationReschedulingDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                }
                this.mySimulationReschedulingDB = SimulationReschedulingDB.Create(myDBSetting, myLocalDBSetting, myDBSession);
                myFinalReturnTable = new DataTable();

            }
            if (!IsCallback)
            {

            }
        }
        protected void cplMain_Callback(object source, CallbackEventArgs e)
        {
            string[] callbackParam = e.Parameter.ToString().Split(';');
            cplMain.JSProperties["cpCallbackParam"] = callbackParam[0].ToUpper();
            SqlDBSetting dbSetting = this.myDBSetting;
            SqlConnection SQLConn = new SqlConnection(dbsetting.ConnectionString);

            double[] StepDown = { Convert.ToDouble(seStepDown.Value), Convert.ToDouble(seStepDown2.Value) };
            double[] StepDownAmount = { Convert.ToDouble(seStepDownAmount.Value), Convert.ToDouble(seStepDownAmount2.Value) };
            switch (callbackParam[0].ToUpper())
            {

                case "CALCULATE":

                    myFinalReturnTable = mySimulationReschedulingDB.LoadReturnTable(
                    Convert.ToString(txtAgreeNo.Value),
                    Convert.ToDateTime(deEffDate.Value),
                    Convert.ToDouble(seEffRate.Value),
                    Convert.ToDouble(seNewTenor.Value),
                    StepDown,
                    StepDownAmount);

                    if (myFinalReturnTable.Rows.Count == 0)
                        return;
                    gvMain.DataSource = myFinalReturnTable;
                    gvMain.DataBind();

                    DataRow dataRow = myFinalReturnTable.Rows[0];
                    cplMain.JSProperties["cplblEffRate"] = string.Format("{0:N2}%", mySimulationReschedulingDB.GetEffRateRounding());
                    cplMain.JSProperties["cplblEffDate"] = string.Format("{0:dd/M/yyyy}", deEffDate.Value);
                    cplMain.JSProperties["cplblOutstandingDenda"] = string.Format("{0:N2}", mySimulationReschedulingDB.GetDenda());
                    cplMain.JSProperties["cplblAccruedInterest"] = string.Format("{0:N2}", mySimulationReschedulingDB.GetAccruedIntAmt());
                    cplMain.JSProperties["cpllblClientName"] = mySimulationReschedulingDB.GetClientName();
                    cplMain.JSProperties["cpllblInsuranceDueAmt"] = string.Format("{0:N2}", mySimulationReschedulingDB.GetInsDueAmt());
                    break;
            }
        }

        protected void gvMain_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = myFinalReturnTable;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvMain.GroupCount == 0)
            {
                var exportOptions = new XlsxExportOptionsEx();
                exportOptions.SheetName = "SIMULASI RESTRU - " + mySimulationReschedulingDB.GetClientName() + " - " + txtAgreeNo.Text;
                exportOptions.CustomizeCell += exportOptions_CustomizeCell;
                exportOptions.CustomizeSheetHeader += options_CustomizeSheetHeader;

                ASPxGridViewExporter1.FileName = "SIMULASI RESTRU - " + mySimulationReschedulingDB.GetClientName();
                ASPxGridViewExporter1.WriteXlsxToResponse(exportOptions);
            }
        }

        void exportOptions_CustomizeCell(DevExpress.Export.CustomizeCellEventArgs ea)
        {
            if (ea.ColumnFieldName != "UnitsOnOrder") return;
            if (Convert.ToInt32(ea.Value) == 0)
                ea.Formatting.BackColor = Color.Salmon;
            else
                ea.Formatting.BackColor = Color.LightGray;
            ea.Handled = true;
        }

        void options_CustomizeSheetHeader(DevExpress.Export.ContextEventArgs e)
        {
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "SIMULASI RESTRUKTUR MNC LEASING"}, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "" }, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "" }, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Informasi Klien" }, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "" }, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "No Kontrak" }, new CellObject() { Value = ":" }, new CellObject() { Value = txtAgreeNo.Text } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Nama Klien" }, new CellObject() { Value = ":" }, new CellObject() { Value = mySimulationReschedulingDB.GetClientName() } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Bunga Efektif" }, new CellObject() { Value = ":" }, new CellObject() { Value = string.Format("{0:N2}%", mySimulationReschedulingDB.GetEffRateRounding()) } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Tanggal Efektif" }, new CellObject() { Value = ":" }, new CellObject() { Value = string.Format("{0:dd/M/yyyy}", deEffDate.Value) } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "New Tenor" }, new CellObject() { Value = ":" }, new CellObject() { Value = seNewTenor.Text } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "" }, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "" }, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Informasi Biaya" }, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "" }, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Outstanding Installment" }, new CellObject() { Value = ":" }, new CellObject() { Value = string.Format("{0:N2}", mySimulationReschedulingDB.GetDenda()) } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Bunga Berjalan" }, new CellObject() { Value = ":" }, new CellObject() { Value = string.Format("{0:N2}", mySimulationReschedulingDB.GetAccruedIntAmt()) } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Biaya Asuransi Jatuh Tempo" }, new CellObject() { Value = ":" }, new CellObject() { Value = string.Format("{0:N2}", mySimulationReschedulingDB.GetInsDueAmt()) } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Biaya Restruktur" }, new CellObject() { Value = ":" }, new CellObject() { Value = string.Format("{0:N2}", seRestructureFee.Value) } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Biaya Perpanjangan Asuransi" }, new CellObject() { Value = ":" }, new CellObject() { Value = string.Format("{0:N2}", seInsuranceRenewalFee.Value) } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "Total Tagiha ke Klien" }, new CellObject() { Value = ":" }, new CellObject() { Value = string.Format("{0:N2}", seTotalChargetoCustomer.Value) } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "" }, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
            e.ExportContext.AddRow(new CellObject[] { new CellObject() { Value = "" }, new CellObject() { Value = "" }, new CellObject() { Value = "" } });
        }
    }
}