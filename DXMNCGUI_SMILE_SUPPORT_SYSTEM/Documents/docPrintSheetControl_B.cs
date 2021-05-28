using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for docPrintSheetControl_B
/// </summary>
public class docPrintSheetControl_B : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel9;
    private XRLabel xrLabel10;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRLine xrLine1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel8;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource;
    private GroupFooterBand GroupFooter1;
    private ReportFooterBand ReportFooter;
    private PageFooterBand PageFooter;
    private XRLabel lblHeaderCDM;
    private DetailReportBand DetailReport_1;
    private DetailBand Detail1;
    private DetailReportBand DetailReport_2;
    private DetailBand Detail2;
    private DevExpress.XtraReports.Parameters.Parameter AppNo;
    private DevExpress.XtraReports.Parameters.Parameter DocNo;
    private XRTable xrTable3;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell133;
    private XRTableCell xrTableCell134;
    private XRTableCell xrTableCell135;
    private XRTableCell xrTableCell136;
    private XRTableCell xrTableCell137;
    private XRTableCell xrTableCell138;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell139;
    private XRTableCell xrTableCell140;
    private XRTableCell xrTableCell141;
    private XRTableCell xrTableCell142;
    private XRTableCell xrTableCell143;
    private XRTableCell xrTableCell144;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel2;
    private XRTable xrTable4;
    private XRTableRow xrTableRow21;
    private XRTableCell xrTableCell145;
    private XRTableCell xrTableCell146;
    private XRTableCell xrTableCell147;
    private XRTableCell xrTableCell148;
    private XRTableCell xrTableCell149;
    private XRTableCell xrTableCell150;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell151;
    private XRTableCell xrTableCell152;
    private XRTableCell xrTableCell153;
    private XRTableCell xrTableCell154;
    private XRTableCell xrTableCell155;
    private XRTableCell xrTableCell156;
    private GroupHeaderBand GroupHeader2;
    private XRLabel xrLabel3;
    private DetailReportBand DetailReport_3;
    private DetailBand Detail3;
    private XRTable xrTable5;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell157;
    private XRTableCell xrTableCell158;
    private XRTableCell xrTableCell159;
    private XRTableCell xrTableCell161;
    private XRTableCell xrTableCell162;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell163;
    private XRTableCell xrTableCell164;
    private XRTableCell xrTableCell165;
    private XRTableCell xrTableCell166;
    private XRTableCell xrTableCell167;
    private XRTableCell xrTableCell168;
    private GroupHeaderBand GroupHeader3;
    private XRLabel xrLabel4;
    private GroupFooterBand GroupFooter2;
    private XRPageBreak xrPageBreak1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell107;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell95;
    private XRTableCell xrTableCell108;
    private XRTableCell xrTableCell121;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell96;
    private XRTableCell xrTableCell109;
    private XRTableCell xrTableCell122;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell97;
    private XRTableCell xrTableCell110;
    private XRTableCell xrTableCell123;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell98;
    private XRTableCell xrTableCell111;
    private XRTableCell xrTableCell124;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell51;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell100;
    private XRTableCell xrTableCell113;
    private XRTableCell xrTableCell126;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell101;
    private XRTableCell xrTableCell114;
    private XRTableCell xrTableCell127;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell67;
    private XRTableCell xrTableCell68;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell102;
    private XRTableCell xrTableCell115;
    private XRTableCell xrTableCell128;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell70;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell103;
    private XRTableCell xrTableCell116;
    private XRTableCell xrTableCell129;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell76;
    private XRTableCell xrTableCell77;
    private XRTableCell xrTableCell78;
    private XRTableCell xrTableCell79;
    private XRTableCell xrTableCell80;
    private XRTableCell xrTableCell81;
    private XRTableCell xrTableCell104;
    private XRTableCell xrTableCell117;
    private XRTableCell xrTableCell130;
    private XRTableRow xrTableRow17;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell83;
    private XRTableCell xrTableCell84;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell88;
    private XRTableCell xrTableCell89;
    private XRTableCell xrTableCell90;
    private DetailReportBand DetailReport_4;
    private DetailBand Detail4;
    private GroupHeaderBand GroupHeader4;
    private XRLabel lblDataPembiayaan;
    private XRTable tblDataPembiayaan;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell176;
    private XRTableCell xrTableCell177;
    private XRTableCell xrTableCell180;
    private XRTableCell xrTableCell182;
    private XRTableCell xrTableCell183;
    private XRTableCell xrTableCell184;
    private XRTableCell xrTableCell185;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell186;
    private XRTableCell xrTableCell187;
    private XRTableCell xrTableCellCRDate;
    private XRTableCell xrTableCell192;
    private XRTableCell xrTableCell193;
    private XRTableCell xrTableCell194;
    private XRTableCell xrTableCellCamDate;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell206;
    private XRTableCell xrTableCell207;
    private XRTableCell xrTableCell212;
    private XRTableCell xrTableCell213;
    private XRTableCell xrTableCell214;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell217;
    private XRTableCell xrTableCell222;
    private XRTableCell xrTableCell223;
    private XRTableCell xrTableCell224;
    private XRTableCell xrTableCell225;
    private DetailReportBand DetailReport_5;
    private DetailBand Detail5;
    private GroupHeaderBand GroupHeader5;
    private XRTableCell xrTableCell215;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell30;
    private XRTableRow xrTableRow29;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell36;
    private XRTableRow xrTableRow30;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell60;
    private XRLine xrLine2;
    private XRLine xrLine3;
    private XRLine xrLine4;
    private DetailReportBand DetailReport_6;
    private DetailBand Detail6;
    private GroupHeaderBand GroupHeader6;
    private XRTable xrTable8;
    private XRTableRow xrTableRow31;
    private XRTableCell xrTableCell92;
    private XRTableRow xrTableRow37;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell42;
    private XRTable xrTable9;
    private XRTableRow xrTableRow39;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell85;
    private XRTableCell xrTableCell86;
    private XRTable xrTable7;
    private XRTableRow xrTableRow34;
    private XRTableCell xrTableCell230;
    private XRTableRow xrTableRow35;
    private XRTableCell xrTableCell87;
    private XRTable xrTable6;
    private XRTableRow xrTableRow32;
    private XRTableCell xrTableCell226;
    private XRTableCell xrTableCell227;
    private XRTableRow xrTableRow33;
    private XRTableCell xrTableCell228;
    private XRTableCell xrTableCell229;
    private XRTable xrTable10;
    private XRTableRow xrTableRow36;
    private XRTableCell xrTableCell234;
    private XRTableRow xrTableRow38;
    private XRTableCell xrTableCell91;
    private XRTable xrTable11;
    private XRTableRow xrTableRow40;
    private XRTableCell xrTableCell231;
    private XRTableCell xrTableCell233;
    private XRTableRow xrTableRow41;
    private XRTableCell xrTableCell236;
    private XRTableCell xrTableCell237;
    private XRTableCell xrTableCell238;
    private XRTableCell xrTableCell239;
    private XRTableRow xrTableRow42;
    private XRTableCell xrTableCell232;
    private XRTableCell xrTableCell235;
    private XRTableCell xrTableCell240;
    private XRTableCell xrTableCell241;
    private XRPageInfo xrPageInfo2;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private XRLabel xrLabel5;
    private XRPageInfo xrPageInfo1;
    private GroupFooterBand GroupFooter6;
    private XRPageBreak xrPageBreak2;
    private XRPageBreak xrPageBreak3;
    private XRTableCell xrTableCell94;
    private XRTableCell xrTableCell93;
    private XRTableCell xrTableCell99;
    private XRTableCell xrTableCell105;
    private XRTableCell xrTableCell106;
    private XRTableCell xrTableCell112;
    private XRTableCell xrTableCell160;
    private XRTableCell xrTableCell216;
    private XRTableCell xrTableCell119;
    private XRTableCell xrTableCell118;
    private XRTableRow xrTableRow43;
    private XRTableCell xrTableCell120;
    private XRTableCell xrTableCell125;
    private XRTableCell xrTableCell131;
    private XRTableCell xrTableCell132;
    private XRTableRow xrTableRow44;
    private XRTableCell xrTableCell169;
    private XRTableCell xrTableCell170;
    private XRTableCell xrTableCell171;
    private XRTableCell xrTableCell172;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public docPrintSheetControl_B()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(docPrintSheetControl_B));
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery2 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery3 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery4 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery5 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery6 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.MasterDetailInfo masterDetailInfo1 = new DevExpress.DataAccess.Sql.MasterDetailInfo();
            DevExpress.DataAccess.Sql.RelationColumnInfo relationColumnInfo1 = new DevExpress.DataAccess.Sql.RelationColumnInfo();
            DevExpress.DataAccess.Sql.MasterDetailInfo masterDetailInfo2 = new DevExpress.DataAccess.Sql.MasterDetailInfo();
            DevExpress.DataAccess.Sql.RelationColumnInfo relationColumnInfo2 = new DevExpress.DataAccess.Sql.RelationColumnInfo();
            DevExpress.DataAccess.Sql.MasterDetailInfo masterDetailInfo3 = new DevExpress.DataAccess.Sql.MasterDetailInfo();
            DevExpress.DataAccess.Sql.RelationColumnInfo relationColumnInfo3 = new DevExpress.DataAccess.Sql.RelationColumnInfo();
            DevExpress.DataAccess.Sql.MasterDetailInfo masterDetailInfo4 = new DevExpress.DataAccess.Sql.MasterDetailInfo();
            DevExpress.DataAccess.Sql.RelationColumnInfo relationColumnInfo4 = new DevExpress.DataAccess.Sql.RelationColumnInfo();
            DevExpress.DataAccess.Sql.MasterDetailInfo masterDetailInfo5 = new DevExpress.DataAccess.Sql.MasterDetailInfo();
            DevExpress.DataAccess.Sql.RelationColumnInfo relationColumnInfo5 = new DevExpress.DataAccess.Sql.RelationColumnInfo();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.sqlDataSource = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow36 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell234 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow38 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow34 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell230 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow35 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell87 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow32 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell226 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell227 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow33 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell228 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell105 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell229 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPageBreak3 = new DevExpress.XtraReports.UI.XRPageBreak();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow40 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell231 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell233 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow41 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell236 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell237 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell238 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell239 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow42 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell232 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell235 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell240 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell241 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow43 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell120 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell125 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell131 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell132 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow44 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell169 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell170 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell171 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell172 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.lblHeaderCDM = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport_1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell107 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell108 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell121 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell109 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell122 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell110 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell123 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell111 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell124 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell100 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell113 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell126 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell101 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell114 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell127 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell102 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell115 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell128 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell103 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell116 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell129 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell104 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell117 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell130 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell84 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow29 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow30 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport_2 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell133 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell134 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell135 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell136 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell137 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell138 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell139 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell140 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell141 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell142 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell143 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell144 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
            this.AppNo = new DevExpress.XtraReports.Parameters.Parameter();
            this.DocNo = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell145 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell146 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell147 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell148 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell149 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell150 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell151 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell152 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell153 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell154 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell155 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell156 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport_3 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell157 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell158 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell159 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell160 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell161 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell162 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell163 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell164 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell165 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell166 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell167 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell168 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport_4 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
            this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.lblDataPembiayaan = new DevExpress.XtraReports.UI.XRLabel();
            this.tblDataPembiayaan = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell176 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell177 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell180 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell182 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell183 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell184 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell185 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell186 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell187 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCRDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell192 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell193 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell194 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellCamDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell206 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell207 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell212 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell213 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell214 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell215 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell216 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell217 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell222 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell223 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell224 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell225 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport_5 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.DetailReport_6 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow39 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell106 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell119 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell86 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader6 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow31 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow37 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell112 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell118 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter6 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrPageBreak2 = new DevExpress.XtraReports.UI.XRPageBreak();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblDataPembiayaan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 33F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrLabel6,
            this.xrLabel7,
            this.xrLabel5,
            this.xrPageInfo1});
            this.BottomMargin.HeightF = 33F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrPageInfo2.Font = new System.Drawing.Font("Arial", 8F);
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(364.233F, 0F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(36.87274F, 23F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DocNo]")});
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Italic);
            this.xrLabel6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(48.53469F, 0F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 13F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseForeColor = false;
            this.xrLabel6.Text = "xrLabel6";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Italic);
            this.xrLabel7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(23.99766F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(24.53702F, 13F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseForeColor = false;
            this.xrLabel7.Text = "Ref :";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Italic);
            this.xrLabel5.ForeColor = System.Drawing.Color.Black;
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(600.0861F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(49.42328F, 12.99998F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseForeColor = false;
            this.xrLabel5.Text = "Print Date :";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Italic);
            this.xrPageInfo1.ForeColor = System.Drawing.Color.Black;
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(649.5095F, 0F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(101.488F, 12.99998F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseForeColor = false;
            this.xrPageInfo1.TextFormatString = "{0:dd-MMM-yyyy hh:mm:ss tt}";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.xrLabel1,
            this.xrLabel8,
            this.xrTable1,
            this.xrPictureBox1,
            this.xrLabel9,
            this.xrLabel10});
            this.ReportHeader.HeightF = 160.3125F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLine1
            // 
            this.xrLine1.LineWidth = 2;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(522.8748F, 26.76787F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(228.1252F, 2.232143F);
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(522.8748F, 6.101211F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(228.1249F, 20.66664F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "LEMBAR KONTROL";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DocNo]")});
            this.xrLabel8.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(522.8751F, 28.99998F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(228.1249F, 20.66664F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "LEMBAR KONTROL";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTable1
            // 
            this.xrTable1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(522.8751F, 59.27083F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5});
            this.xrTable1.SizeF = new System.Drawing.SizeF(228.125F, 101.0417F);
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "LK DATE";
            this.xrTableCell1.Weight = 0.93750060939359647D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = ":";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.13020692755625124D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DocDate]")});
            this.xrTableCell3.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = " ";
            this.xrTableCell3.TextFormatString = "{0:dd-MMM-yyyy}";
            this.xrTableCell3.Weight = 1.2135430734017147D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "APP NO";
            this.xrTableCell4.Weight = 0.93750060939359647D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = ":";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.13020692755625124D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[APPNO]")});
            this.xrTableCell6.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Weight = 1.2135430734017147D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Text = "CIF NO";
            this.xrTableCell7.Weight = 0.93750060939359647D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = ":";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.13020692755625124D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Client]")});
            this.xrTableCell9.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.Weight = 1.2135430734017147D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Text = "TYPE";
            this.xrTableCell10.Weight = 0.93750060939359647D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = ":";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 0.13020692755625124D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TIPE]")});
            this.xrTableCell12.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "xrTableCell12";
            this.xrTableCell12.Weight = 1.2135430734017147D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Text = "RO";
            this.xrTableCell13.Weight = 0.93750060939359647D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = ":";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell14.Weight = 0.13020692755625124D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RO]")});
            this.xrTableCell15.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.Weight = 1.2135430734017147D;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(2.666664F, 8.333333F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(50.00002F, 50F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(60.25801F, 8.333333F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(292.8497F, 20.66664F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "PT. MNC GUNA USAHA INDONESIA";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new System.Drawing.Font("Calibri", 9F);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(60.25747F, 28.99998F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(129.1592F, 68.45539F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "MNC Tower 23rd Floor \r\nJl. Kebon Sirih No 17-19, \r\nJakarta Pusat \r\n10340 - Indone" +
    "sia";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionName = "SqlLocalConnectionString";
            this.sqlDataSource.Name = "sqlDataSource";
            customSqlQuery1.MetaSerializable = "<Meta X=\"20\" Y=\"20\" Width=\"100\" Height=\"802\" />";
            customSqlQuery1.Name = "SheetControl";
            queryParameter1.Name = "DocNo";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.DocNo]", typeof(string));
            customSqlQuery1.Parameters.Add(queryParameter1);
            customSqlQuery1.Sql = resources.GetString("customSqlQuery1.Sql");
            customSqlQuery2.MetaSerializable = "<Meta X=\"140\" Y=\"20\" Width=\"100\" Height=\"139\" />";
            customSqlQuery2.Name = "SheetControlAkteNotaris";
            queryParameter2.Name = "AppNo";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.AppNo]", typeof(string));
            customSqlQuery2.Parameters.Add(queryParameter2);
            customSqlQuery2.Sql = resources.GetString("customSqlQuery2.Sql");
            customSqlQuery3.MetaSerializable = "<Meta X=\"260\" Y=\"20\" Width=\"100\" Height=\"564\" />";
            customSqlQuery3.Name = "SheetControlBadanUsaha";
            queryParameter3.Name = "AppNo";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.AppNo]", typeof(string));
            customSqlQuery3.Parameters.Add(queryParameter3);
            customSqlQuery3.Sql = resources.GetString("customSqlQuery3.Sql");
            customSqlQuery4.MetaSerializable = "<Meta X=\"380\" Y=\"20\" Width=\"100\" Height=\"122\" />";
            customSqlQuery4.Name = "SheetControlDetailAsset";
            queryParameter4.Name = "AppNo";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("[Parameters.AppNo]", typeof(string));
            customSqlQuery4.Parameters.Add(queryParameter4);
            customSqlQuery4.Sql = resources.GetString("customSqlQuery4.Sql");
            customSqlQuery5.MetaSerializable = "<Meta X=\"500\" Y=\"20\" Width=\"100\" Height=\"156\" />";
            customSqlQuery5.Name = "SheetControlPemegangSaham";
            queryParameter5.Name = "AppNo";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("[Parameters.AppNo]", typeof(string));
            customSqlQuery5.Parameters.Add(queryParameter5);
            customSqlQuery5.Sql = resources.GetString("customSqlQuery5.Sql");
            customSqlQuery6.MetaSerializable = "<Meta X=\"620\" Y=\"20\" Width=\"100\" Height=\"156\" />";
            customSqlQuery6.Name = "SheetControlPengurus";
            queryParameter6.Name = "AppNo";
            queryParameter6.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter6.Value = new DevExpress.DataAccess.Expression("[Parameters.AppNo]", typeof(string));
            customSqlQuery6.Parameters.Add(queryParameter6);
            customSqlQuery6.Sql = resources.GetString("customSqlQuery6.Sql");
            this.sqlDataSource.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1,
            customSqlQuery2,
            customSqlQuery3,
            customSqlQuery4,
            customSqlQuery5,
            customSqlQuery6});
            masterDetailInfo1.DetailQueryName = "SheetControlAkteNotaris";
            relationColumnInfo1.NestedKeyColumn = "AppNo";
            relationColumnInfo1.ParentKeyColumn = "AppNo";
            masterDetailInfo1.KeyColumns.Add(relationColumnInfo1);
            masterDetailInfo1.MasterQueryName = "SheetControl";
            masterDetailInfo2.DetailQueryName = "SheetControlBadanUsaha";
            relationColumnInfo2.NestedKeyColumn = "AppNo";
            relationColumnInfo2.ParentKeyColumn = "AppNo";
            masterDetailInfo2.KeyColumns.Add(relationColumnInfo2);
            masterDetailInfo2.MasterQueryName = "SheetControl";
            masterDetailInfo3.DetailQueryName = "SheetControlDetailAsset";
            relationColumnInfo3.NestedKeyColumn = "AppNo";
            relationColumnInfo3.ParentKeyColumn = "AppNo";
            masterDetailInfo3.KeyColumns.Add(relationColumnInfo3);
            masterDetailInfo3.MasterQueryName = "SheetControl";
            masterDetailInfo4.DetailQueryName = "SheetControlPemegangSaham";
            relationColumnInfo4.NestedKeyColumn = "AppNo";
            relationColumnInfo4.ParentKeyColumn = "AppNo";
            masterDetailInfo4.KeyColumns.Add(relationColumnInfo4);
            masterDetailInfo4.MasterQueryName = "SheetControl";
            masterDetailInfo5.DetailQueryName = "SheetControlPengurus";
            relationColumnInfo5.NestedKeyColumn = "AppNo";
            relationColumnInfo5.ParentKeyColumn = "AppNo";
            masterDetailInfo5.KeyColumns.Add(relationColumnInfo5);
            masterDetailInfo5.MasterQueryName = "SheetControl";
            this.sqlDataSource.Relations.AddRange(new DevExpress.DataAccess.Sql.MasterDetailInfo[] {
            masterDetailInfo1,
            masterDetailInfo2,
            masterDetailInfo3,
            masterDetailInfo4,
            masterDetailInfo5});
            this.sqlDataSource.ResultSchemaSerializable = resources.GetString("sqlDataSource.ResultSchemaSerializable");
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10,
            this.xrTable7,
            this.xrTable6,
            this.xrPageBreak3});
            this.GroupFooter1.Expanded = false;
            this.GroupFooter1.HeightF = 220.2083F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrTable10
            // 
            this.xrTable10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(23.99876F, 171.0069F);
            this.xrTable10.Name = "xrTable10";
            this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow36,
            this.xrTableRow38});
            this.xrTable10.SizeF = new System.Drawing.SizeF(726.9999F, 49.20132F);
            this.xrTable10.StylePriority.UseBorders = false;
            // 
            // xrTableRow36
            // 
            this.xrTableRow36.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell234});
            this.xrTableRow36.Name = "xrTableRow36";
            this.xrTableRow36.Weight = 1D;
            // 
            // xrTableCell234
            // 
            this.xrTableCell234.BackColor = System.Drawing.SystemColors.ControlLight;
            this.xrTableCell234.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell234.BorderWidth = 2F;
            this.xrTableCell234.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell234.Name = "xrTableCell234";
            this.xrTableCell234.StylePriority.UseBackColor = false;
            this.xrTableCell234.StylePriority.UseBorders = false;
            this.xrTableCell234.StylePriority.UseBorderWidth = false;
            this.xrTableCell234.StylePriority.UseFont = false;
            this.xrTableCell234.StylePriority.UseTextAlignment = false;
            this.xrTableCell234.Text = "KEKURANGAN DOKUMEN";
            this.xrTableCell234.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell234.Weight = 3D;
            // 
            // xrTableRow38
            // 
            this.xrTableRow38.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell94,
            this.xrTableCell91});
            this.xrTableRow38.Name = "xrTableRow38";
            this.xrTableRow38.Weight = 1D;
            // 
            // xrTableCell94
            // 
            this.xrTableCell94.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell94.BorderWidth = 2F;
            this.xrTableCell94.Font = new System.Drawing.Font("Arial", 10F);
            this.xrTableCell94.Name = "xrTableCell94";
            this.xrTableCell94.StylePriority.UseBorders = false;
            this.xrTableCell94.StylePriority.UseBorderWidth = false;
            this.xrTableCell94.StylePriority.UseFont = false;
            this.xrTableCell94.StylePriority.UseTextAlignment = false;
            this.xrTableCell94.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell94.Weight = 0.055717844919632442D;
            // 
            // xrTableCell91
            // 
            this.xrTableCell91.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell91.BorderWidth = 2F;
            this.xrTableCell91.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UncompletedDoc]")});
            this.xrTableCell91.Font = new System.Drawing.Font("Arial", 10F);
            this.xrTableCell91.Multiline = true;
            this.xrTableCell91.Name = "xrTableCell91";
            this.xrTableCell91.StylePriority.UseBorders = false;
            this.xrTableCell91.StylePriority.UseBorderWidth = false;
            this.xrTableCell91.StylePriority.UseFont = false;
            this.xrTableCell91.StylePriority.UseTextAlignment = false;
            this.xrTableCell91.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell91.Weight = 2.9442821550803675D;
            // 
            // xrTable7
            // 
            this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable7.BorderWidth = 2F;
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(24.00111F, 108.7747F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow34,
            this.xrTableRow35});
            this.xrTable7.SizeF = new System.Drawing.SizeF(726.9999F, 49.99998F);
            this.xrTable7.StylePriority.UseBorders = false;
            this.xrTable7.StylePriority.UseBorderWidth = false;
            // 
            // xrTableRow34
            // 
            this.xrTableRow34.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell230});
            this.xrTableRow34.Name = "xrTableRow34";
            this.xrTableRow34.Weight = 1D;
            // 
            // xrTableCell230
            // 
            this.xrTableCell230.BackColor = System.Drawing.SystemColors.ControlLight;
            this.xrTableCell230.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell230.Name = "xrTableCell230";
            this.xrTableCell230.StylePriority.UseBackColor = false;
            this.xrTableCell230.StylePriority.UseFont = false;
            this.xrTableCell230.StylePriority.UseTextAlignment = false;
            this.xrTableCell230.Text = "KESIMPULAN DEPARTEMEN LEGAL";
            this.xrTableCell230.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell230.Weight = 3D;
            // 
            // xrTableRow35
            // 
            this.xrTableRow35.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell93,
            this.xrTableCell87});
            this.xrTableRow35.Name = "xrTableRow35";
            this.xrTableRow35.Weight = 1D;
            // 
            // xrTableCell93
            // 
            this.xrTableCell93.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell93.Font = new System.Drawing.Font("Arial", 10F);
            this.xrTableCell93.Name = "xrTableCell93";
            this.xrTableCell93.StylePriority.UseBorders = false;
            this.xrTableCell93.StylePriority.UseFont = false;
            this.xrTableCell93.StylePriority.UseTextAlignment = false;
            this.xrTableCell93.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell93.Weight = 0.055708148135959146D;
            // 
            // xrTableCell87
            // 
            this.xrTableCell87.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell87.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LegalConclution]")});
            this.xrTableCell87.Font = new System.Drawing.Font("Arial", 10F);
            this.xrTableCell87.Multiline = true;
            this.xrTableCell87.Name = "xrTableCell87";
            this.xrTableCell87.StylePriority.UseBorders = false;
            this.xrTableCell87.StylePriority.UseFont = false;
            this.xrTableCell87.StylePriority.UseTextAlignment = false;
            this.xrTableCell87.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell87.Weight = 2.9442918518640409D;
            // 
            // xrTable6
            // 
            this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable6.BorderWidth = 2F;
            this.xrTable6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(23.9988F, 9.99999F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow32,
            this.xrTableRow33});
            this.xrTable6.SizeF = new System.Drawing.SizeF(727F, 71.77077F);
            this.xrTable6.StylePriority.UseBorders = false;
            this.xrTable6.StylePriority.UseBorderWidth = false;
            this.xrTable6.StylePriority.UseFont = false;
            this.xrTable6.StylePriority.UseTextAlignment = false;
            this.xrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow32
            // 
            this.xrTableRow32.BackColor = System.Drawing.SystemColors.ControlLight;
            this.xrTableRow32.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell226,
            this.xrTableCell227});
            this.xrTableRow32.Name = "xrTableRow32";
            this.xrTableRow32.StylePriority.UseBackColor = false;
            this.xrTableRow32.Weight = 1D;
            // 
            // xrTableCell226
            // 
            this.xrTableCell226.Name = "xrTableCell226";
            this.xrTableCell226.Text = "DOKUMEN PERJANJIAN (MANDATORY)";
            this.xrTableCell226.Weight = 3.4023434448242189D;
            // 
            // xrTableCell227
            // 
            this.xrTableCell227.Name = "xrTableCell227";
            this.xrTableCell227.Text = "DOKUMEN TAMBAHAN";
            this.xrTableCell227.Weight = 3.8676589965820312D;
            // 
            // xrTableRow33
            // 
            this.xrTableRow33.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell99,
            this.xrTableCell228,
            this.xrTableCell105,
            this.xrTableCell229});
            this.xrTableRow33.Font = new System.Drawing.Font("Arial", 9F);
            this.xrTableRow33.Name = "xrTableRow33";
            this.xrTableRow33.StylePriority.UseFont = false;
            this.xrTableRow33.StylePriority.UseTextAlignment = false;
            this.xrTableRow33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableRow33.Weight = 1.9821492004394519D;
            // 
            // xrTableCell99
            // 
            this.xrTableCell99.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell99.Name = "xrTableCell99";
            this.xrTableCell99.StylePriority.UseBorders = false;
            this.xrTableCell99.Weight = 0.13502175188883414D;
            // 
            // xrTableCell228
            // 
            this.xrTableCell228.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell228.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DOCMAND]")});
            this.xrTableCell228.Multiline = true;
            this.xrTableCell228.Name = "xrTableCell228";
            this.xrTableCell228.StylePriority.UseBorders = false;
            this.xrTableCell228.Text = " ";
            this.xrTableCell228.Weight = 3.2673216929353845D;
            // 
            // xrTableCell105
            // 
            this.xrTableCell105.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell105.Name = "xrTableCell105";
            this.xrTableCell105.StylePriority.UseBorders = false;
            this.xrTableCell105.Weight = 0.10765716261521185D;
            // 
            // xrTableCell229
            // 
            this.xrTableCell229.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell229.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DOCADDI]")});
            this.xrTableCell229.Multiline = true;
            this.xrTableCell229.Name = "xrTableCell229";
            this.xrTableCell229.StylePriority.UseBorders = false;
            this.xrTableCell229.Text = " ";
            this.xrTableCell229.Weight = 3.7600018339668191D;
            // 
            // xrPageBreak3
            // 
            this.xrPageBreak3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 93.75386F);
            this.xrPageBreak3.Name = "xrPageBreak3";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable11});
            this.ReportFooter.Expanded = false;
            this.ReportFooter.HeightF = 211.9805F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable11
            // 
            this.xrTable11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable11.BorderWidth = 2F;
            this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(23.9988F, 9.99999F);
            this.xrTable11.Name = "xrTable11";
            this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow40,
            this.xrTableRow41,
            this.xrTableRow42,
            this.xrTableRow43,
            this.xrTableRow44});
            this.xrTable11.SizeF = new System.Drawing.SizeF(726.9999F, 201.9805F);
            this.xrTable11.StylePriority.UseBorders = false;
            this.xrTable11.StylePriority.UseBorderWidth = false;
            // 
            // xrTableRow40
            // 
            this.xrTableRow40.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell231,
            this.xrTableCell233});
            this.xrTableRow40.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableRow40.Name = "xrTableRow40";
            this.xrTableRow40.StylePriority.UseFont = false;
            this.xrTableRow40.StylePriority.UseTextAlignment = false;
            this.xrTableRow40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableRow40.Weight = 1D;
            // 
            // xrTableCell231
            // 
            this.xrTableCell231.Name = "xrTableCell231";
            this.xrTableCell231.Text = "DEPARTEMEN LEGAL";
            this.xrTableCell231.Weight = 2D;
            // 
            // xrTableCell233
            // 
            this.xrTableCell233.Name = "xrTableCell233";
            this.xrTableCell233.Text = "REPRESENTATIVE OFFICE";
            this.xrTableCell233.Weight = 2D;
            // 
            // xrTableRow41
            // 
            this.xrTableRow41.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell236,
            this.xrTableCell237,
            this.xrTableCell238,
            this.xrTableCell239});
            this.xrTableRow41.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableRow41.Name = "xrTableRow41";
            this.xrTableRow41.StylePriority.UseFont = false;
            this.xrTableRow41.StylePriority.UseTextAlignment = false;
            this.xrTableRow41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableRow41.Weight = 1D;
            // 
            // xrTableCell236
            // 
            this.xrTableCell236.Name = "xrTableCell236";
            this.xrTableCell236.Text = "Made By";
            this.xrTableCell236.Weight = 1D;
            // 
            // xrTableCell237
            // 
            this.xrTableCell237.Name = "xrTableCell237";
            this.xrTableCell237.Text = "Approved By";
            this.xrTableCell237.Weight = 1D;
            // 
            // xrTableCell238
            // 
            this.xrTableCell238.Name = "xrTableCell238";
            this.xrTableCell238.Text = "Marketing";
            this.xrTableCell238.Weight = 1D;
            // 
            // xrTableCell239
            // 
            this.xrTableCell239.Name = "xrTableCell239";
            this.xrTableCell239.Text = "Business Manager";
            this.xrTableCell239.Weight = 1D;
            // 
            // xrTableRow42
            // 
            this.xrTableRow42.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell232,
            this.xrTableCell235,
            this.xrTableCell240,
            this.xrTableCell241});
            this.xrTableRow42.Font = new System.Drawing.Font("Arial", 9F);
            this.xrTableRow42.Name = "xrTableRow42";
            this.xrTableRow42.StylePriority.UseFont = false;
            this.xrTableRow42.StylePriority.UseTextAlignment = false;
            this.xrTableRow42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            this.xrTableRow42.Weight = 4.630951594961088D;
            // 
            // xrTableCell232
            // 
            this.xrTableCell232.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell232.Name = "xrTableCell232";
            this.xrTableCell232.StylePriority.UseBorders = false;
            this.xrTableCell232.Weight = 1D;
            // 
            // xrTableCell235
            // 
            this.xrTableCell235.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell235.Name = "xrTableCell235";
            this.xrTableCell235.StylePriority.UseBorders = false;
            this.xrTableCell235.Weight = 1D;
            // 
            // xrTableCell240
            // 
            this.xrTableCell240.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell240.Name = "xrTableCell240";
            this.xrTableCell240.StylePriority.UseBorders = false;
            this.xrTableCell240.Weight = 1D;
            // 
            // xrTableCell241
            // 
            this.xrTableCell241.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell241.Name = "xrTableCell241";
            this.xrTableCell241.StylePriority.UseBorders = false;
            this.xrTableCell241.Weight = 1D;
            // 
            // xrTableRow43
            // 
            this.xrTableRow43.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell120,
            this.xrTableCell125,
            this.xrTableCell131,
            this.xrTableCell132});
            this.xrTableRow43.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableRow43.Name = "xrTableRow43";
            this.xrTableRow43.StylePriority.UseFont = false;
            this.xrTableRow43.StylePriority.UseTextAlignment = false;
            this.xrTableRow43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableRow43.Weight = 0.85002052275223816D;
            // 
            // xrTableCell120
            // 
            this.xrTableCell120.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell120.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FooterMadeBy]")});
            this.xrTableCell120.Name = "xrTableCell120";
            this.xrTableCell120.StylePriority.UseBorders = false;
            this.xrTableCell120.Weight = 1D;
            // 
            // xrTableCell125
            // 
            this.xrTableCell125.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell125.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FooterApprovedBy]")});
            this.xrTableCell125.Name = "xrTableCell125";
            this.xrTableCell125.StylePriority.UseBorders = false;
            this.xrTableCell125.Weight = 1D;
            // 
            // xrTableCell131
            // 
            this.xrTableCell131.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell131.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FooterMarketing]")});
            this.xrTableCell131.Name = "xrTableCell131";
            this.xrTableCell131.StylePriority.UseBorders = false;
            this.xrTableCell131.Weight = 1D;
            // 
            // xrTableCell132
            // 
            this.xrTableCell132.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell132.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FooterBusinessManager]")});
            this.xrTableCell132.Name = "xrTableCell132";
            this.xrTableCell132.StylePriority.UseBorders = false;
            this.xrTableCell132.Weight = 1D;
            // 
            // xrTableRow44
            // 
            this.xrTableRow44.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell169,
            this.xrTableCell170,
            this.xrTableCell171,
            this.xrTableCell172});
            this.xrTableRow44.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Italic);
            this.xrTableRow44.Name = "xrTableRow44";
            this.xrTableRow44.StylePriority.UseFont = false;
            this.xrTableRow44.StylePriority.UseTextAlignment = false;
            this.xrTableRow44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow44.Weight = 0.85002052275223816D;
            // 
            // xrTableCell169
            // 
            this.xrTableCell169.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FooterMadeByPos]")});
            this.xrTableCell169.Name = "xrTableCell169";
            this.xrTableCell169.Weight = 1D;
            // 
            // xrTableCell170
            // 
            this.xrTableCell170.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FooterApprovedByPos]")});
            this.xrTableCell170.Name = "xrTableCell170";
            this.xrTableCell170.Weight = 1D;
            // 
            // xrTableCell171
            // 
            this.xrTableCell171.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FooterMarketingPos]")});
            this.xrTableCell171.Name = "xrTableCell171";
            this.xrTableCell171.Weight = 1D;
            // 
            // xrTableCell172
            // 
            this.xrTableCell172.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FooterBusinessManagerPos]")});
            this.xrTableCell172.Name = "xrTableCell172";
            this.xrTableCell172.Weight = 1D;
            // 
            // PageFooter
            // 
            this.PageFooter.Expanded = false;
            this.PageFooter.HeightF = 100F;
            this.PageFooter.Name = "PageFooter";
            // 
            // lblHeaderCDM
            // 
            this.lblHeaderCDM.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblHeaderCDM.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblHeaderCDM.BorderWidth = 2F;
            this.lblHeaderCDM.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblHeaderCDM.LocationFloat = new DevExpress.Utils.PointFloat(24.00039F, 9.99999F);
            this.lblHeaderCDM.Name = "lblHeaderCDM";
            this.lblHeaderCDM.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeaderCDM.SizeF = new System.Drawing.SizeF(726.9999F, 27.8077F);
            this.lblHeaderCDM.StylePriority.UseBackColor = false;
            this.lblHeaderCDM.StylePriority.UseBorders = false;
            this.lblHeaderCDM.StylePriority.UseBorderWidth = false;
            this.lblHeaderCDM.StylePriority.UseFont = false;
            this.lblHeaderCDM.StylePriority.UseTextAlignment = false;
            this.lblHeaderCDM.Text = "CUSTOMER DATABASE CHECKING";
            this.lblHeaderCDM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailReport_1
            // 
            this.DetailReport_1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1});
            this.DetailReport_1.DataMember = "SheetControlBadanUsaha";
            this.DetailReport_1.DataSource = this.sqlDataSource;
            this.DetailReport_1.Expanded = false;
            this.DetailReport_1.Level = 0;
            this.DetailReport_1.Name = "DetailReport_1";
            // 
            // Detail1
            // 
            this.Detail1.HeightF = 0F;
            this.Detail1.Name = "Detail1";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2,
            this.lblHeaderCDM});
            this.GroupHeader1.HeightF = 378.1385F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable2
            // 
            this.xrTable2.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(24.00039F, 50.4143F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6,
            this.xrTableRow7,
            this.xrTableRow8,
            this.xrTableRow9,
            this.xrTableRow10,
            this.xrTableRow11,
            this.xrTableRow12,
            this.xrTableRow13,
            this.xrTableRow14,
            this.xrTableRow15,
            this.xrTableRow16,
            this.xrTableRow17,
            this.xrTableRow18,
            this.xrTableRow29,
            this.xrTableRow30});
            this.xrTable2.SizeF = new System.Drawing.SizeF(726.9999F, 319.9117F);
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell107});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.Text = "Nama";
            this.xrTableCell16.Weight = 1.0948902638989964D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.Text = ":";
            this.xrTableCell17.Weight = 0.082530908229122679D;
            // 
            // xrTableCell107
            // 
            this.xrTableCell107.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Name]")});
            this.xrTableCell107.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell107.Name = "xrTableCell107";
            this.xrTableCell107.StylePriority.UseFont = false;
            this.xrTableCell107.Weight = 4.8225748486717244D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell18,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell95,
            this.xrTableCell108,
            this.xrTableCell121});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.Text = "NPWP";
            this.xrTableCell22.Weight = 1.0948902638989964D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.Text = ":";
            this.xrTableCell23.Weight = 0.082530908229122679D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NPWP]")});
            this.xrTableCell24.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.Weight = 0.99426099769546838D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell18.Weight = 0.63475734033947806D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.Text = "Bentuk";
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell25.Weight = 0.41983293224990847D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseFont = false;
            this.xrTableCell26.Text = ":";
            this.xrTableCell26.Weight = 0.082530908229122679D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[JenisBadanUsaha]")});
            this.xrTableCell27.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.Weight = 1.1475624678779892D;
            // 
            // xrTableCell95
            // 
            this.xrTableCell95.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell95.Name = "xrTableCell95";
            this.xrTableCell95.StylePriority.UseFont = false;
            this.xrTableCell95.Text = "Status";
            this.xrTableCell95.Weight = 0.72023533566497333D;
            // 
            // xrTableCell108
            // 
            this.xrTableCell108.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell108.Name = "xrTableCell108";
            this.xrTableCell108.StylePriority.UseFont = false;
            this.xrTableCell108.Text = ":";
            this.xrTableCell108.Weight = 0.082530908229122679D;
            // 
            // xrTableCell121
            // 
            this.xrTableCell121.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[StatusBadanUsaha]")});
            this.xrTableCell121.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell121.Name = "xrTableCell121";
            this.xrTableCell121.StylePriority.UseFont = false;
            this.xrTableCell121.Weight = 0.74086395838566166D;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell33,
            this.xrTableCell96,
            this.xrTableCell109,
            this.xrTableCell122});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseFont = false;
            this.xrTableCell28.Text = "Alamat SKD";
            this.xrTableCell28.Weight = 1.0948902638989964D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseFont = false;
            this.xrTableCell29.Text = ":";
            this.xrTableCell29.Weight = 0.082530908229122679D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AddressSKD]")});
            this.xrTableCell33.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseFont = false;
            this.xrTableCell33.Weight = 3.2789446463919667D;
            // 
            // xrTableCell96
            // 
            this.xrTableCell96.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell96.Name = "xrTableCell96";
            this.xrTableCell96.StylePriority.UseFont = false;
            this.xrTableCell96.Text = "Kode Pos";
            this.xrTableCell96.Weight = 0.72023533566497333D;
            // 
            // xrTableCell109
            // 
            this.xrTableCell109.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell109.Name = "xrTableCell109";
            this.xrTableCell109.StylePriority.UseFont = false;
            this.xrTableCell109.Text = ":";
            this.xrTableCell109.Weight = 0.082530908229122679D;
            // 
            // xrTableCell122
            // 
            this.xrTableCell122.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[KodePOSSKD]")});
            this.xrTableCell122.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell122.Name = "xrTableCell122";
            this.xrTableCell122.StylePriority.UseFont = false;
            this.xrTableCell122.Weight = 0.74086395838566166D;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell39,
            this.xrTableCell97,
            this.xrTableCell110,
            this.xrTableCell123});
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseFont = false;
            this.xrTableCell34.Text = "Alamat NPWP";
            this.xrTableCell34.Weight = 1.0948902638989964D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.Text = ":";
            this.xrTableCell35.Weight = 0.082530908229122679D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AddressNPWP]")});
            this.xrTableCell39.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseFont = false;
            this.xrTableCell39.Weight = 3.2789446463919667D;
            // 
            // xrTableCell97
            // 
            this.xrTableCell97.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell97.Name = "xrTableCell97";
            this.xrTableCell97.StylePriority.UseFont = false;
            this.xrTableCell97.Text = "Kode Pos";
            this.xrTableCell97.Weight = 0.72023533566497333D;
            // 
            // xrTableCell110
            // 
            this.xrTableCell110.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell110.Name = "xrTableCell110";
            this.xrTableCell110.StylePriority.UseFont = false;
            this.xrTableCell110.Text = ":";
            this.xrTableCell110.Weight = 0.082530908229122679D;
            // 
            // xrTableCell123
            // 
            this.xrTableCell123.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[KodePOSNPWP]")});
            this.xrTableCell123.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell123.Name = "xrTableCell123";
            this.xrTableCell123.StylePriority.UseFont = false;
            this.xrTableCell123.Weight = 0.74086395838566166D;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell45,
            this.xrTableCell98,
            this.xrTableCell111,
            this.xrTableCell124});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseFont = false;
            this.xrTableCell40.Text = "Alamat Surat Menyurat";
            this.xrTableCell40.Weight = 1.0948902638989964D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseFont = false;
            this.xrTableCell41.Text = ":";
            this.xrTableCell41.Weight = 0.082530908229122679D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AddresssBILL]")});
            this.xrTableCell45.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.StylePriority.UseFont = false;
            this.xrTableCell45.Weight = 3.2789446463919667D;
            // 
            // xrTableCell98
            // 
            this.xrTableCell98.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell98.Name = "xrTableCell98";
            this.xrTableCell98.StylePriority.UseFont = false;
            this.xrTableCell98.Text = "Kode Pos";
            this.xrTableCell98.Weight = 0.72023533566497333D;
            // 
            // xrTableCell111
            // 
            this.xrTableCell111.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell111.Name = "xrTableCell111";
            this.xrTableCell111.StylePriority.UseFont = false;
            this.xrTableCell111.Text = ":";
            this.xrTableCell111.Weight = 0.082530908229122679D;
            // 
            // xrTableCell124
            // 
            this.xrTableCell124.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[KodePOSBILL]")});
            this.xrTableCell124.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell124.Name = "xrTableCell124";
            this.xrTableCell124.StylePriority.UseFont = false;
            this.xrTableCell124.Weight = 0.74086395838566166D;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell46,
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell19,
            this.xrTableCell49,
            this.xrTableCell50,
            this.xrTableCell51});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 1D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseFont = false;
            this.xrTableCell46.Text = "Group Nasabah";
            this.xrTableCell46.Weight = 1.0948902638989964D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.StylePriority.UseFont = false;
            this.xrTableCell47.Text = ":";
            this.xrTableCell47.Weight = 0.082530908229122679D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CorporateGroup]")});
            this.xrTableCell48.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseFont = false;
            this.xrTableCell48.Weight = 0.99426099769546838D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.Weight = 0.63475734033947817D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.StylePriority.UseFont = false;
            this.xrTableCell49.Text = "Email";
            this.xrTableCell49.Weight = 0.41983293224990842D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseFont = false;
            this.xrTableCell50.Text = ":";
            this.xrTableCell50.Weight = 0.082530908229122679D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CorporateEmail]")});
            this.xrTableCell51.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseFont = false;
            this.xrTableCell51.Weight = 2.6911926701577471D;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell52,
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCell20,
            this.xrTableCell55,
            this.xrTableCell56,
            this.xrTableCell57,
            this.xrTableCell100,
            this.xrTableCell113,
            this.xrTableCell126});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 1D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseFont = false;
            this.xrTableCell52.Text = "Nama Contact Person";
            this.xrTableCell52.Weight = 1.0948902638989964D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.StylePriority.UseFont = false;
            this.xrTableCell53.Text = ":";
            this.xrTableCell53.Weight = 0.082530908229122679D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CorporateContactPerson]")});
            this.xrTableCell54.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.StylePriority.UseFont = false;
            this.xrTableCell54.Weight = 0.99426099769546838D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.Weight = 0.63475734033947806D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.StylePriority.UseFont = false;
            this.xrTableCell55.Text = "Hp";
            this.xrTableCell55.Weight = 0.41983293224990847D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.StylePriority.UseFont = false;
            this.xrTableCell56.Text = ":";
            this.xrTableCell56.Weight = 0.082530908229122679D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CorporateContactPersonHp]")});
            this.xrTableCell57.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.StylePriority.UseFont = false;
            this.xrTableCell57.Weight = 1.1475624678779892D;
            // 
            // xrTableCell100
            // 
            this.xrTableCell100.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell100.Name = "xrTableCell100";
            this.xrTableCell100.StylePriority.UseFont = false;
            this.xrTableCell100.Text = "No. Telp";
            this.xrTableCell100.Weight = 0.72023533566497333D;
            // 
            // xrTableCell113
            // 
            this.xrTableCell113.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell113.Name = "xrTableCell113";
            this.xrTableCell113.StylePriority.UseFont = false;
            this.xrTableCell113.Text = ":";
            this.xrTableCell113.Weight = 0.082530908229122679D;
            // 
            // xrTableCell126
            // 
            this.xrTableCell126.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CorporateContactPersonTelp]")});
            this.xrTableCell126.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell126.Name = "xrTableCell126";
            this.xrTableCell126.StylePriority.UseFont = false;
            this.xrTableCell126.Weight = 0.74086395838566166D;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell58,
            this.xrTableCell59,
            this.xrTableCell61,
            this.xrTableCell62,
            this.xrTableCell63,
            this.xrTableCell101,
            this.xrTableCell114,
            this.xrTableCell127});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.StylePriority.UseFont = false;
            this.xrTableCell58.Text = "Nomor izin Usaha";
            this.xrTableCell58.Weight = 1.0948902638989964D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.StylePriority.UseFont = false;
            this.xrTableCell59.Text = ":";
            this.xrTableCell59.Weight = 0.082530908229122679D;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SIUP]")});
            this.xrTableCell61.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.StylePriority.UseFont = false;
            this.xrTableCell61.Weight = 2.0488512702848549D;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell62.Name = "xrTableCell62";
            this.xrTableCell62.StylePriority.UseFont = false;
            this.xrTableCell62.Weight = 0.082530908229122679D;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.Name = "xrTableCell63";
            this.xrTableCell63.Weight = 1.1475624678779892D;
            // 
            // xrTableCell101
            // 
            this.xrTableCell101.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell101.Name = "xrTableCell101";
            this.xrTableCell101.StylePriority.UseFont = false;
            this.xrTableCell101.Text = "Berlaku Hingga";
            this.xrTableCell101.Weight = 0.72023533566497333D;
            // 
            // xrTableCell114
            // 
            this.xrTableCell114.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell114.Name = "xrTableCell114";
            this.xrTableCell114.StylePriority.UseFont = false;
            this.xrTableCell114.Text = ":";
            this.xrTableCell114.Weight = 0.082530908229122679D;
            // 
            // xrTableCell127
            // 
            this.xrTableCell127.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SIUPExpTo]")});
            this.xrTableCell127.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell127.Name = "xrTableCell127";
            this.xrTableCell127.StylePriority.UseFont = false;
            this.xrTableCell127.TextFormatString = "{0:dd-MMM-yyyy}";
            this.xrTableCell127.Weight = 0.74086395838566166D;
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell64,
            this.xrTableCell65,
            this.xrTableCell67,
            this.xrTableCell68,
            this.xrTableCell69,
            this.xrTableCell102,
            this.xrTableCell115,
            this.xrTableCell128});
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.StylePriority.UseFont = false;
            this.xrTableCell64.Text = "Nomor TDP/NIB";
            this.xrTableCell64.Weight = 1.0948902638989964D;
            // 
            // xrTableCell65
            // 
            this.xrTableCell65.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell65.Name = "xrTableCell65";
            this.xrTableCell65.StylePriority.UseFont = false;
            this.xrTableCell65.Text = ":";
            this.xrTableCell65.Weight = 0.082530908229122679D;
            // 
            // xrTableCell67
            // 
            this.xrTableCell67.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TDP]")});
            this.xrTableCell67.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell67.Name = "xrTableCell67";
            this.xrTableCell67.StylePriority.UseFont = false;
            this.xrTableCell67.Weight = 2.0488512702848549D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.StylePriority.UseFont = false;
            this.xrTableCell68.Weight = 0.082530908229122679D;
            // 
            // xrTableCell69
            // 
            this.xrTableCell69.Name = "xrTableCell69";
            this.xrTableCell69.Weight = 1.1475624678779892D;
            // 
            // xrTableCell102
            // 
            this.xrTableCell102.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell102.Name = "xrTableCell102";
            this.xrTableCell102.StylePriority.UseFont = false;
            this.xrTableCell102.Text = "Berlaku Hingga";
            this.xrTableCell102.Weight = 0.72023533566497333D;
            // 
            // xrTableCell115
            // 
            this.xrTableCell115.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell115.Name = "xrTableCell115";
            this.xrTableCell115.StylePriority.UseFont = false;
            this.xrTableCell115.Text = ":";
            this.xrTableCell115.Weight = 0.082530908229122679D;
            // 
            // xrTableCell128
            // 
            this.xrTableCell128.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TDPExpTo]")});
            this.xrTableCell128.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell128.Name = "xrTableCell128";
            this.xrTableCell128.StylePriority.UseFont = false;
            this.xrTableCell128.TextFormatString = "{0:dd-MMM-yyyy}";
            this.xrTableCell128.Weight = 0.74086395838566166D;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell70,
            this.xrTableCell71,
            this.xrTableCell72,
            this.xrTableCell21,
            this.xrTableCell73,
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell103,
            this.xrTableCell116,
            this.xrTableCell129});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1D;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.StylePriority.UseFont = false;
            this.xrTableCell70.Text = "Tempat Pendirian";
            this.xrTableCell70.Weight = 1.0948902638989964D;
            // 
            // xrTableCell71
            // 
            this.xrTableCell71.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell71.Name = "xrTableCell71";
            this.xrTableCell71.StylePriority.UseFont = false;
            this.xrTableCell71.Text = ":";
            this.xrTableCell71.Weight = 0.082530908229122679D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TempatPendirian]")});
            this.xrTableCell72.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.StylePriority.UseFont = false;
            this.xrTableCell72.Weight = 0.99426099769546838D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.Weight = 0.63475734033947806D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.StylePriority.UseFont = false;
            this.xrTableCell73.Text = "No. Akta";
            this.xrTableCell73.Weight = 0.41983293224990847D;
            // 
            // xrTableCell74
            // 
            this.xrTableCell74.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell74.Name = "xrTableCell74";
            this.xrTableCell74.StylePriority.UseFont = false;
            this.xrTableCell74.Text = ":";
            this.xrTableCell74.Weight = 0.082530908229122679D;
            // 
            // xrTableCell75
            // 
            this.xrTableCell75.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NoAkta]")});
            this.xrTableCell75.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell75.Name = "xrTableCell75";
            this.xrTableCell75.StylePriority.UseFont = false;
            this.xrTableCell75.Weight = 1.1475624678779892D;
            // 
            // xrTableCell103
            // 
            this.xrTableCell103.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell103.Name = "xrTableCell103";
            this.xrTableCell103.StylePriority.UseFont = false;
            this.xrTableCell103.Text = "Tgl Akta";
            this.xrTableCell103.Weight = 0.72023533566497333D;
            // 
            // xrTableCell116
            // 
            this.xrTableCell116.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell116.Name = "xrTableCell116";
            this.xrTableCell116.StylePriority.UseFont = false;
            this.xrTableCell116.Text = ":";
            this.xrTableCell116.Weight = 0.082530908229122679D;
            // 
            // xrTableCell129
            // 
            this.xrTableCell129.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TglAkta]")});
            this.xrTableCell129.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell129.Name = "xrTableCell129";
            this.xrTableCell129.StylePriority.UseFont = false;
            this.xrTableCell129.TextFormatString = "{0:dd-MMM-yyyy}";
            this.xrTableCell129.Weight = 0.74086395838566166D;
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell76,
            this.xrTableCell77,
            this.xrTableCell78,
            this.xrTableCell30,
            this.xrTableCell79,
            this.xrTableCell80,
            this.xrTableCell81,
            this.xrTableCell104,
            this.xrTableCell117,
            this.xrTableCell130});
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.Weight = 1D;
            // 
            // xrTableCell76
            // 
            this.xrTableCell76.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell76.Name = "xrTableCell76";
            this.xrTableCell76.StylePriority.UseFont = false;
            this.xrTableCell76.Text = "Notaris";
            this.xrTableCell76.Weight = 1.0948902638989964D;
            // 
            // xrTableCell77
            // 
            this.xrTableCell77.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell77.Name = "xrTableCell77";
            this.xrTableCell77.StylePriority.UseFont = false;
            this.xrTableCell77.Text = ":";
            this.xrTableCell77.Weight = 0.082530908229122679D;
            // 
            // xrTableCell78
            // 
            this.xrTableCell78.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Notaris]")});
            this.xrTableCell78.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell78.Name = "xrTableCell78";
            this.xrTableCell78.StylePriority.UseFont = false;
            this.xrTableCell78.Weight = 0.99426106066155429D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseFont = false;
            this.xrTableCell30.Weight = 0.63475734033947817D;
            // 
            // xrTableCell79
            // 
            this.xrTableCell79.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell79.Name = "xrTableCell79";
            this.xrTableCell79.StylePriority.UseFont = false;
            this.xrTableCell79.Text = "SK";
            this.xrTableCell79.Weight = 0.41983286928382252D;
            // 
            // xrTableCell80
            // 
            this.xrTableCell80.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.StylePriority.UseFont = false;
            this.xrTableCell80.Text = ":";
            this.xrTableCell80.Weight = 0.082530908229122679D;
            // 
            // xrTableCell81
            // 
            this.xrTableCell81.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SKMenhukam]")});
            this.xrTableCell81.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell81.Name = "xrTableCell81";
            this.xrTableCell81.StylePriority.UseFont = false;
            this.xrTableCell81.Weight = 1.1475624678779892D;
            // 
            // xrTableCell104
            // 
            this.xrTableCell104.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell104.Name = "xrTableCell104";
            this.xrTableCell104.StylePriority.UseFont = false;
            this.xrTableCell104.Text = "Tgl SK";
            this.xrTableCell104.Weight = 0.72023533566497333D;
            // 
            // xrTableCell117
            // 
            this.xrTableCell117.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell117.Name = "xrTableCell117";
            this.xrTableCell117.StylePriority.UseFont = false;
            this.xrTableCell117.Text = ":";
            this.xrTableCell117.Weight = 0.082530908229122679D;
            // 
            // xrTableCell130
            // 
            this.xrTableCell130.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TglSK]")});
            this.xrTableCell130.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell130.Name = "xrTableCell130";
            this.xrTableCell130.StylePriority.UseFont = false;
            this.xrTableCell130.TextFormatString = "{0:dd-MMM-yyyy}";
            this.xrTableCell130.Weight = 0.74086395838566166D;
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell82,
            this.xrTableCell83,
            this.xrTableCell84});
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.Weight = 1D;
            // 
            // xrTableCell82
            // 
            this.xrTableCell82.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell82.Name = "xrTableCell82";
            this.xrTableCell82.StylePriority.UseFont = false;
            this.xrTableCell82.Text = "Golongan (SLIK)";
            this.xrTableCell82.Weight = 1.0948902638989964D;
            // 
            // xrTableCell83
            // 
            this.xrTableCell83.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell83.Name = "xrTableCell83";
            this.xrTableCell83.StylePriority.UseFont = false;
            this.xrTableCell83.Text = ":";
            this.xrTableCell83.Weight = 0.082530908229122679D;
            // 
            // xrTableCell84
            // 
            this.xrTableCell84.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SLIKGolongan]")});
            this.xrTableCell84.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell84.Name = "xrTableCell84";
            this.xrTableCell84.StylePriority.UseFont = false;
            this.xrTableCell84.Weight = 4.8225748486717244D;
            // 
            // xrTableRow18
            // 
            this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell88,
            this.xrTableCell89,
            this.xrTableCell90});
            this.xrTableRow18.Name = "xrTableRow18";
            this.xrTableRow18.Weight = 1D;
            // 
            // xrTableCell88
            // 
            this.xrTableCell88.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell88.Name = "xrTableCell88";
            this.xrTableCell88.StylePriority.UseFont = false;
            this.xrTableCell88.Text = "Golongan (SIPP)";
            this.xrTableCell88.Weight = 1.0948902638989964D;
            // 
            // xrTableCell89
            // 
            this.xrTableCell89.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell89.Name = "xrTableCell89";
            this.xrTableCell89.StylePriority.UseFont = false;
            this.xrTableCell89.Text = ":";
            this.xrTableCell89.Weight = 0.082530908229122679D;
            // 
            // xrTableCell90
            // 
            this.xrTableCell90.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SIPPGolongan]")});
            this.xrTableCell90.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell90.Name = "xrTableCell90";
            this.xrTableCell90.StylePriority.UseFont = false;
            this.xrTableCell90.Weight = 4.8225748486717244D;
            // 
            // xrTableRow29
            // 
            this.xrTableRow29.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell36});
            this.xrTableRow29.Name = "xrTableRow29";
            this.xrTableRow29.Weight = 1D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.Text = "Sektor Ekonomi (SLIK)";
            this.xrTableCell31.Weight = 1.0948902638989964D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseFont = false;
            this.xrTableCell32.Text = ":";
            this.xrTableCell32.Weight = 0.082530908229122679D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SLIKSektorEkonomi]")});
            this.xrTableCell36.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseFont = false;
            this.xrTableCell36.Weight = 4.8225748486717244D;
            // 
            // xrTableRow30
            // 
            this.xrTableRow30.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell43,
            this.xrTableCell44,
            this.xrTableCell60});
            this.xrTableRow30.Name = "xrTableRow30";
            this.xrTableRow30.Weight = 1D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseFont = false;
            this.xrTableCell43.Text = "Sektor Ekonomi (SIPP)";
            this.xrTableCell43.Weight = 1.0948902638989964D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseFont = false;
            this.xrTableCell44.Text = ":";
            this.xrTableCell44.Weight = 0.082530908229122679D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SIPPSektorEkonomi]")});
            this.xrTableCell60.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.StylePriority.UseFont = false;
            this.xrTableCell60.Weight = 4.8225748486717244D;
            // 
            // DetailReport_2
            // 
            this.DetailReport_2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader2,
            this.GroupFooter2});
            this.DetailReport_2.DataMember = "SheetControlAkteNotaris";
            this.DetailReport_2.DataSource = this.sqlDataSource;
            this.DetailReport_2.Expanded = false;
            this.DetailReport_2.Level = 1;
            this.DetailReport_2.Name = "DetailReport_2";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrLine2});
            this.Detail2.HeightF = 39.59614F;
            this.Detail2.Name = "Detail2";
            // 
            // xrTable3
            // 
            this.xrTable3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(24.00029F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19,
            this.xrTableRow20});
            this.xrTable3.SizeF = new System.Drawing.SizeF(726.9998F, 37.59614F);
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell133,
            this.xrTableCell134,
            this.xrTableCell135,
            this.xrTableCell136,
            this.xrTableCell137,
            this.xrTableCell138});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 1D;
            // 
            // xrTableCell133
            // 
            this.xrTableCell133.Name = "xrTableCell133";
            this.xrTableCell133.Text = "No. Akta Perubahan";
            this.xrTableCell133.Weight = 1.0948904895194189D;
            // 
            // xrTableCell134
            // 
            this.xrTableCell134.Name = "xrTableCell134";
            this.xrTableCell134.Text = ":";
            this.xrTableCell134.Weight = 0.082530970312333141D;
            // 
            // xrTableCell135
            // 
            this.xrTableCell135.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NoAktaPerubahan]")});
            this.xrTableCell135.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell135.Name = "xrTableCell135";
            this.xrTableCell135.StylePriority.UseFont = false;
            this.xrTableCell135.Weight = 2.0488506753690152D;
            // 
            // xrTableCell136
            // 
            this.xrTableCell136.Name = "xrTableCell136";
            this.xrTableCell136.Text = "Tgl Akta Perubahan";
            this.xrTableCell136.Weight = 1.209925679740639D;
            // 
            // xrTableCell137
            // 
            this.xrTableCell137.Name = "xrTableCell137";
            this.xrTableCell137.Text = ":";
            this.xrTableCell137.Weight = 0.082530970154892511D;
            // 
            // xrTableCell138
            // 
            this.xrTableCell138.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TglAktaPerubahan]")});
            this.xrTableCell138.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell138.Name = "xrTableCell138";
            this.xrTableCell138.StylePriority.UseFont = false;
            this.xrTableCell138.Text = " ";
            this.xrTableCell138.TextFormatString = "{0:dd-MMM-yyyy}";
            this.xrTableCell138.Weight = 1.4812712149037013D;
            // 
            // xrTableRow20
            // 
            this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell139,
            this.xrTableCell140,
            this.xrTableCell141,
            this.xrTableCell142,
            this.xrTableCell143,
            this.xrTableCell144});
            this.xrTableRow20.Name = "xrTableRow20";
            this.xrTableRow20.Weight = 1D;
            // 
            // xrTableCell139
            // 
            this.xrTableCell139.Name = "xrTableCell139";
            this.xrTableCell139.Text = "SK Menhukam No.";
            this.xrTableCell139.Weight = 1.0948904895194189D;
            // 
            // xrTableCell140
            // 
            this.xrTableCell140.Name = "xrTableCell140";
            this.xrTableCell140.Text = ":";
            this.xrTableCell140.Weight = 0.082530970312333141D;
            // 
            // xrTableCell141
            // 
            this.xrTableCell141.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SK]")});
            this.xrTableCell141.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell141.Name = "xrTableCell141";
            this.xrTableCell141.StylePriority.UseFont = false;
            this.xrTableCell141.Weight = 2.0488506753690152D;
            // 
            // xrTableCell142
            // 
            this.xrTableCell142.Name = "xrTableCell142";
            this.xrTableCell142.Text = "Tgl SK";
            this.xrTableCell142.Weight = 1.209925679740639D;
            // 
            // xrTableCell143
            // 
            this.xrTableCell143.Name = "xrTableCell143";
            this.xrTableCell143.Text = ":";
            this.xrTableCell143.Weight = 0.082530970154892511D;
            // 
            // xrTableCell144
            // 
            this.xrTableCell144.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TglSK]")});
            this.xrTableCell144.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell144.Name = "xrTableCell144";
            this.xrTableCell144.StylePriority.UseFont = false;
            this.xrTableCell144.TextFormatString = "{0:dd-MMM-yyyy}";
            this.xrTableCell144.Weight = 1.4812712149037013D;
            // 
            // xrLine2
            // 
            this.xrLine2.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(23.99893F, 37.59614F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(726.9999F, 2F);
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
            this.GroupHeader2.HeightF = 37.8077F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrLabel2
            // 
            this.xrLabel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel2.BorderWidth = 2F;
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(23.99967F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(726.9999F, 27.8077F);
            this.xrLabel2.StylePriority.UseBackColor = false;
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseBorderWidth = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "AKTA PERUBAHAN";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak1});
            this.GroupFooter2.Expanded = false;
            this.GroupFooter2.HeightF = 2F;
            this.GroupFooter2.Name = "GroupFooter2";
            // 
            // xrPageBreak1
            // 
            this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPageBreak1.Name = "xrPageBreak1";
            // 
            // AppNo
            // 
            this.AppNo.Description = "paramAppNo";
            this.AppNo.Name = "AppNo";
            this.AppNo.Visible = false;
            // 
            // DocNo
            // 
            this.DocNo.Description = "paramDocNo";
            this.DocNo.Name = "DocNo";
            this.DocNo.Visible = false;
            // 
            // xrLabel3
            // 
            this.xrLabel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel3.BorderWidth = 2F;
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(24.00029F, 9.99999F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(726.9999F, 27.8077F);
            this.xrLabel3.StylePriority.UseBackColor = false;
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseBorderWidth = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "DATA PENGURUS";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable4
            // 
            this.xrTable4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(24.00039F, 10.00002F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow21,
            this.xrTableRow22});
            this.xrTable4.SizeF = new System.Drawing.SizeF(726.9998F, 37.59614F);
            this.xrTable4.StylePriority.UseFont = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow21
            // 
            this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell145,
            this.xrTableCell146,
            this.xrTableCell147,
            this.xrTableCell148,
            this.xrTableCell149,
            this.xrTableCell150});
            this.xrTableRow21.Name = "xrTableRow21";
            this.xrTableRow21.Weight = 1D;
            // 
            // xrTableCell145
            // 
            this.xrTableCell145.Name = "xrTableCell145";
            this.xrTableCell145.Text = "Nama Pengurus";
            this.xrTableCell145.Weight = 1.0948904895194189D;
            // 
            // xrTableCell146
            // 
            this.xrTableCell146.Name = "xrTableCell146";
            this.xrTableCell146.Text = ":";
            this.xrTableCell146.Weight = 0.082530970312333141D;
            // 
            // xrTableCell147
            // 
            this.xrTableCell147.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NamaPengurus]")});
            this.xrTableCell147.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell147.Name = "xrTableCell147";
            this.xrTableCell147.StylePriority.UseFont = false;
            this.xrTableCell147.Weight = 2.0488506753690152D;
            // 
            // xrTableCell148
            // 
            this.xrTableCell148.Name = "xrTableCell148";
            this.xrTableCell148.Text = "NIK/NPWP";
            this.xrTableCell148.Weight = 1.209925679740639D;
            // 
            // xrTableCell149
            // 
            this.xrTableCell149.Name = "xrTableCell149";
            this.xrTableCell149.Text = ":";
            this.xrTableCell149.Weight = 0.082530970154892511D;
            // 
            // xrTableCell150
            // 
            this.xrTableCell150.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NIK] + \' \' + [NPWP]")});
            this.xrTableCell150.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell150.Name = "xrTableCell150";
            this.xrTableCell150.StylePriority.UseFont = false;
            this.xrTableCell150.Text = " ";
            this.xrTableCell150.Weight = 1.4812712149037013D;
            // 
            // xrTableRow22
            // 
            this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell151,
            this.xrTableCell152,
            this.xrTableCell153,
            this.xrTableCell154,
            this.xrTableCell155,
            this.xrTableCell156});
            this.xrTableRow22.Name = "xrTableRow22";
            this.xrTableRow22.Weight = 1D;
            // 
            // xrTableCell151
            // 
            this.xrTableCell151.Name = "xrTableCell151";
            this.xrTableCell151.Text = "Alamat";
            this.xrTableCell151.Weight = 1.0948904895194189D;
            // 
            // xrTableCell152
            // 
            this.xrTableCell152.Name = "xrTableCell152";
            this.xrTableCell152.Text = ":";
            this.xrTableCell152.Weight = 0.082530970312333141D;
            // 
            // xrTableCell153
            // 
            this.xrTableCell153.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AlamatPengurus]")});
            this.xrTableCell153.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell153.Multiline = true;
            this.xrTableCell153.Name = "xrTableCell153";
            this.xrTableCell153.StylePriority.UseFont = false;
            this.xrTableCell153.Weight = 2.0488506753690152D;
            // 
            // xrTableCell154
            // 
            this.xrTableCell154.Name = "xrTableCell154";
            this.xrTableCell154.Text = "Jabatan";
            this.xrTableCell154.Weight = 1.209925679740639D;
            // 
            // xrTableCell155
            // 
            this.xrTableCell155.Name = "xrTableCell155";
            this.xrTableCell155.Text = ":";
            this.xrTableCell155.Weight = 0.082530970154892511D;
            // 
            // xrTableCell156
            // 
            this.xrTableCell156.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Jabatan]")});
            this.xrTableCell156.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell156.Multiline = true;
            this.xrTableCell156.Name = "xrTableCell156";
            this.xrTableCell156.StylePriority.UseFont = false;
            this.xrTableCell156.Weight = 1.4812712149037013D;
            // 
            // DetailReport_3
            // 
            this.DetailReport_3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader3});
            this.DetailReport_3.DataMember = "SheetControlPengurus";
            this.DetailReport_3.DataSource = this.sqlDataSource;
            this.DetailReport_3.Expanded = false;
            this.DetailReport_3.Level = 2;
            this.DetailReport_3.Name = "DetailReport_3";
            // 
            // Detail3
            // 
            this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine3,
            this.xrTable4});
            this.Detail3.HeightF = 49.59615F;
            this.Detail3.Name = "Detail3";
            // 
            // xrLine3
            // 
            this.xrLine3.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(23.99893F, 47.59615F);
            this.xrLine3.Name = "xrLine3";
            this.xrLine3.SizeF = new System.Drawing.SizeF(726.9999F, 2F);
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
            this.GroupHeader3.HeightF = 37.8077F;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // xrLabel4
            // 
            this.xrLabel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel4.BorderWidth = 2F;
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(23.99764F, 10.00001F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(726.9999F, 27.8077F);
            this.xrLabel4.StylePriority.UseBackColor = false;
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseBorderWidth = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "DATA PEMEGANG SAHAM";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable5
            // 
            this.xrTable5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(24.00029F, 9.99999F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23,
            this.xrTableRow24});
            this.xrTable5.SizeF = new System.Drawing.SizeF(726.9998F, 37.59614F);
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow23
            // 
            this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell157,
            this.xrTableCell158,
            this.xrTableCell159,
            this.xrTableCell160,
            this.xrTableCell161,
            this.xrTableCell162});
            this.xrTableRow23.Name = "xrTableRow23";
            this.xrTableRow23.Weight = 1D;
            // 
            // xrTableCell157
            // 
            this.xrTableCell157.Name = "xrTableCell157";
            this.xrTableCell157.Text = "Nama Pemegang Saham";
            this.xrTableCell157.Weight = 1.0948904895194189D;
            // 
            // xrTableCell158
            // 
            this.xrTableCell158.Name = "xrTableCell158";
            this.xrTableCell158.Text = ":";
            this.xrTableCell158.Weight = 0.082530970312333141D;
            // 
            // xrTableCell159
            // 
            this.xrTableCell159.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NamaPemegangSaham]")});
            this.xrTableCell159.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell159.Name = "xrTableCell159";
            this.xrTableCell159.StylePriority.UseFont = false;
            this.xrTableCell159.Weight = 2.0488506753690152D;
            // 
            // xrTableCell160
            // 
            this.xrTableCell160.Name = "xrTableCell160";
            this.xrTableCell160.Weight = 1.209926115922662D;
            // 
            // xrTableCell161
            // 
            this.xrTableCell161.Name = "xrTableCell161";
            this.xrTableCell161.Weight = 0.082530970800417491D;
            // 
            // xrTableCell162
            // 
            this.xrTableCell162.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell162.Name = "xrTableCell162";
            this.xrTableCell162.StylePriority.UseFont = false;
            this.xrTableCell162.Weight = 1.4812707780761532D;
            // 
            // xrTableRow24
            // 
            this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell163,
            this.xrTableCell164,
            this.xrTableCell165,
            this.xrTableCell166,
            this.xrTableCell167,
            this.xrTableCell168});
            this.xrTableRow24.Name = "xrTableRow24";
            this.xrTableRow24.Weight = 1D;
            // 
            // xrTableCell163
            // 
            this.xrTableCell163.Name = "xrTableCell163";
            this.xrTableCell163.Text = "Porsi Kepemilikan";
            this.xrTableCell163.Weight = 1.0948904895194189D;
            // 
            // xrTableCell164
            // 
            this.xrTableCell164.Name = "xrTableCell164";
            this.xrTableCell164.Text = ":";
            this.xrTableCell164.Weight = 0.082530970312333141D;
            // 
            // xrTableCell165
            // 
            this.xrTableCell165.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PorsiKepemilikan]")});
            this.xrTableCell165.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell165.Name = "xrTableCell165";
            this.xrTableCell165.StylePriority.UseFont = false;
            this.xrTableCell165.Weight = 2.0488506753690152D;
            // 
            // xrTableCell166
            // 
            this.xrTableCell166.Name = "xrTableCell166";
            this.xrTableCell166.Text = "Modal Disetor";
            this.xrTableCell166.Weight = 1.209925679740639D;
            // 
            // xrTableCell167
            // 
            this.xrTableCell167.Name = "xrTableCell167";
            this.xrTableCell167.Text = ":";
            this.xrTableCell167.Weight = 0.082530970154892511D;
            // 
            // xrTableCell168
            // 
            this.xrTableCell168.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ModalDisetor]")});
            this.xrTableCell168.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell168.Name = "xrTableCell168";
            this.xrTableCell168.StylePriority.UseFont = false;
            this.xrTableCell168.TextFormatString = "{0:#,#}";
            this.xrTableCell168.Weight = 1.4812712149037013D;
            // 
            // DetailReport_4
            // 
            this.DetailReport_4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.GroupHeader4});
            this.DetailReport_4.DataMember = "SheetControlPemegangSaham";
            this.DetailReport_4.DataSource = this.sqlDataSource;
            this.DetailReport_4.Expanded = false;
            this.DetailReport_4.Level = 3;
            this.DetailReport_4.Name = "DetailReport_4";
            // 
            // Detail4
            // 
            this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine4,
            this.xrTable5});
            this.Detail4.HeightF = 49.59613F;
            this.Detail4.Name = "Detail4";
            // 
            // xrLine4
            // 
            this.xrLine4.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(24.00111F, 47.59613F);
            this.xrLine4.Name = "xrLine4";
            this.xrLine4.SizeF = new System.Drawing.SizeF(726.9999F, 2F);
            // 
            // GroupHeader4
            // 
            this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
            this.GroupHeader4.HeightF = 37.8077F;
            this.GroupHeader4.Name = "GroupHeader4";
            // 
            // lblDataPembiayaan
            // 
            this.lblDataPembiayaan.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblDataPembiayaan.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblDataPembiayaan.BorderWidth = 2F;
            this.lblDataPembiayaan.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblDataPembiayaan.LocationFloat = new DevExpress.Utils.PointFloat(23.99893F, 9.99999F);
            this.lblDataPembiayaan.Name = "lblDataPembiayaan";
            this.lblDataPembiayaan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDataPembiayaan.SizeF = new System.Drawing.SizeF(726.9999F, 27.80771F);
            this.lblDataPembiayaan.StylePriority.UseBackColor = false;
            this.lblDataPembiayaan.StylePriority.UseBorders = false;
            this.lblDataPembiayaan.StylePriority.UseBorderWidth = false;
            this.lblDataPembiayaan.StylePriority.UseFont = false;
            this.lblDataPembiayaan.StylePriority.UseTextAlignment = false;
            this.lblDataPembiayaan.Text = "DATA PEMBIAYAAN";
            this.lblDataPembiayaan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // tblDataPembiayaan
            // 
            this.tblDataPembiayaan.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblDataPembiayaan.Font = new System.Drawing.Font("Arial", 9.75F);
            this.tblDataPembiayaan.LocationFloat = new DevExpress.Utils.PointFloat(24.00111F, 37.8077F);
            this.tblDataPembiayaan.Name = "tblDataPembiayaan";
            this.tblDataPembiayaan.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow25,
            this.xrTableRow26,
            this.xrTableRow27,
            this.xrTableRow28});
            this.tblDataPembiayaan.SizeF = new System.Drawing.SizeF(726.9991F, 68.74995F);
            this.tblDataPembiayaan.StylePriority.UseBorders = false;
            this.tblDataPembiayaan.StylePriority.UseFont = false;
            this.tblDataPembiayaan.StylePriority.UseTextAlignment = false;
            this.tblDataPembiayaan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow25
            // 
            this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell176,
            this.xrTableCell177,
            this.xrTableCell180,
            this.xrTableCell182,
            this.xrTableCell183,
            this.xrTableCell184,
            this.xrTableCell185});
            this.xrTableRow25.Name = "xrTableRow25";
            this.xrTableRow25.Weight = 1D;
            // 
            // xrTableCell176
            // 
            this.xrTableCell176.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell176.Name = "xrTableCell176";
            this.xrTableCell176.StylePriority.UseFont = false;
            this.xrTableCell176.Text = "CR No.";
            this.xrTableCell176.Weight = 0.777804834456715D;
            // 
            // xrTableCell177
            // 
            this.xrTableCell177.Name = "xrTableCell177";
            this.xrTableCell177.StylePriority.UseTextAlignment = false;
            this.xrTableCell177.Text = ":";
            this.xrTableCell177.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell177.Weight = 0.071751725117422768D;
            // 
            // xrTableCell180
            // 
            this.xrTableCell180.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CRNo]")});
            this.xrTableCell180.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell180.Name = "xrTableCell180";
            this.xrTableCell180.StylePriority.UseFont = false;
            this.xrTableCell180.Text = "__________________________";
            this.xrTableCell180.Weight = 1.8835796541663841D;
            // 
            // xrTableCell182
            // 
            this.xrTableCell182.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell182.Name = "xrTableCell182";
            this.xrTableCell182.StylePriority.UseFont = false;
            this.xrTableCell182.Weight = 0.071751730368429245D;
            // 
            // xrTableCell183
            // 
            this.xrTableCell183.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell183.Name = "xrTableCell183";
            this.xrTableCell183.StylePriority.UseFont = false;
            this.xrTableCell183.Text = "CAM No.";
            this.xrTableCell183.Weight = 1.4472951618533534D;
            // 
            // xrTableCell184
            // 
            this.xrTableCell184.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrTableCell184.Name = "xrTableCell184";
            this.xrTableCell184.StylePriority.UseFont = false;
            this.xrTableCell184.StylePriority.UseTextAlignment = false;
            this.xrTableCell184.Text = ":";
            this.xrTableCell184.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell184.Weight = 0.093425633686121362D;
            // 
            // xrTableCell185
            // 
            this.xrTableCell185.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CAMNo]")});
            this.xrTableCell185.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell185.Name = "xrTableCell185";
            this.xrTableCell185.StylePriority.UseFont = false;
            this.xrTableCell185.Text = "__________________";
            this.xrTableCell185.Weight = 0.87073685666505185D;
            // 
            // xrTableRow26
            // 
            this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell186,
            this.xrTableCell187,
            this.xrTableCellCRDate,
            this.xrTableCell192,
            this.xrTableCell193,
            this.xrTableCell194,
            this.xrTableCellCamDate});
            this.xrTableRow26.Name = "xrTableRow26";
            this.xrTableRow26.Weight = 1D;
            // 
            // xrTableCell186
            // 
            this.xrTableCell186.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell186.Name = "xrTableCell186";
            this.xrTableCell186.StylePriority.UseFont = false;
            this.xrTableCell186.Text = "CR Date";
            this.xrTableCell186.Weight = 0.777804834456715D;
            // 
            // xrTableCell187
            // 
            this.xrTableCell187.Name = "xrTableCell187";
            this.xrTableCell187.StylePriority.UseTextAlignment = false;
            this.xrTableCell187.Text = ":";
            this.xrTableCell187.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell187.Weight = 0.071751725117422768D;
            // 
            // xrTableCellCRDate
            // 
            this.xrTableCellCRDate.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CRDate]")});
            this.xrTableCellCRDate.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCellCRDate.Name = "xrTableCellCRDate";
            this.xrTableCellCRDate.Scripts.OnBeforePrint = "xrTableCellCRDate_BeforePrint";
            this.xrTableCellCRDate.StylePriority.UseFont = false;
            this.xrTableCellCRDate.Text = "__________________________";
            this.xrTableCellCRDate.TextFormatString = "{0:dd-MMM-yyyy}";
            this.xrTableCellCRDate.Weight = 1.8835835956073534D;
            // 
            // xrTableCell192
            // 
            this.xrTableCell192.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell192.Name = "xrTableCell192";
            this.xrTableCell192.StylePriority.UseFont = false;
            this.xrTableCell192.Weight = 0.071751511399486517D;
            // 
            // xrTableCell193
            // 
            this.xrTableCell193.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell193.Name = "xrTableCell193";
            this.xrTableCell193.StylePriority.UseFont = false;
            this.xrTableCell193.Text = "CAM Date";
            this.xrTableCell193.Weight = 1.447291439381327D;
            // 
            // xrTableCell194
            // 
            this.xrTableCell194.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrTableCell194.Name = "xrTableCell194";
            this.xrTableCell194.StylePriority.UseFont = false;
            this.xrTableCell194.StylePriority.UseTextAlignment = false;
            this.xrTableCell194.Text = ":";
            this.xrTableCell194.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell194.Weight = 0.093425633686121362D;
            // 
            // xrTableCellCamDate
            // 
            this.xrTableCellCamDate.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CAMDate]")});
            this.xrTableCellCamDate.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCellCamDate.Name = "xrTableCellCamDate";
            this.xrTableCellCamDate.Scripts.OnBeforePrint = "xrTableCellCamDate_BeforePrint";
            this.xrTableCellCamDate.StylePriority.UseFont = false;
            this.xrTableCellCamDate.Text = "__________________";
            this.xrTableCellCamDate.TextFormatString = "{0:dd-MMM-yyyy}";
            this.xrTableCellCamDate.Weight = 0.87073685666505185D;
            // 
            // xrTableRow27
            // 
            this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell206,
            this.xrTableCell207,
            this.xrTableCell212,
            this.xrTableCell213,
            this.xrTableCell214,
            this.xrTableCell215});
            this.xrTableRow27.Name = "xrTableRow27";
            this.xrTableRow27.Weight = 1D;
            // 
            // xrTableCell206
            // 
            this.xrTableCell206.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell206.Name = "xrTableCell206";
            this.xrTableCell206.StylePriority.UseFont = false;
            this.xrTableCell206.Text = "Jenis Fasilitas";
            this.xrTableCell206.Weight = 0.777804834456715D;
            // 
            // xrTableCell207
            // 
            this.xrTableCell207.Name = "xrTableCell207";
            this.xrTableCell207.StylePriority.UseTextAlignment = false;
            this.xrTableCell207.Text = ":";
            this.xrTableCell207.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell207.Weight = 0.071751725117422768D;
            // 
            // xrTableCell212
            // 
            this.xrTableCell212.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[JenisPengikatan]")});
            this.xrTableCell212.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell212.Name = "xrTableCell212";
            this.xrTableCell212.StylePriority.UseFont = false;
            this.xrTableCell212.Text = " ";
            this.xrTableCell212.Weight = 2.8331353959922696D;
            // 
            // xrTableCell213
            // 
            this.xrTableCell213.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell213.Name = "xrTableCell213";
            this.xrTableCell213.StylePriority.UseFont = false;
            this.xrTableCell213.Weight = 0.56949115039589759D;
            // 
            // xrTableCell214
            // 
            this.xrTableCell214.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrTableCell214.Name = "xrTableCell214";
            this.xrTableCell214.StylePriority.UseFont = false;
            this.xrTableCell214.StylePriority.UseTextAlignment = false;
            this.xrTableCell214.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell214.Weight = 0.093425633686121362D;
            // 
            // xrTableCell215
            // 
            this.xrTableCell215.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell215.Name = "xrTableCell215";
            this.xrTableCell215.StylePriority.UseFont = false;
            this.xrTableCell215.Weight = 0.87073685666505185D;
            // 
            // xrTableRow28
            // 
            this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell216,
            this.xrTableCell217,
            this.xrTableCell222,
            this.xrTableCell223,
            this.xrTableCell224,
            this.xrTableCell225});
            this.xrTableRow28.Name = "xrTableRow28";
            this.xrTableRow28.Weight = 1D;
            // 
            // xrTableCell216
            // 
            this.xrTableCell216.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell216.Name = "xrTableCell216";
            this.xrTableCell216.StylePriority.UseFont = false;
            this.xrTableCell216.Weight = 0.777804834456715D;
            // 
            // xrTableCell217
            // 
            this.xrTableCell217.Name = "xrTableCell217";
            this.xrTableCell217.StylePriority.UseTextAlignment = false;
            this.xrTableCell217.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell217.Weight = 0.071751725117422768D;
            // 
            // xrTableCell222
            // 
            this.xrTableCell222.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableCell222.Name = "xrTableCell222";
            this.xrTableCell222.StylePriority.UseFont = false;
            this.xrTableCell222.Text = " ";
            this.xrTableCell222.Weight = 2.8331353959922696D;
            // 
            // xrTableCell223
            // 
            this.xrTableCell223.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell223.Name = "xrTableCell223";
            this.xrTableCell223.StylePriority.UseFont = false;
            this.xrTableCell223.Weight = 0.56949115039589759D;
            // 
            // xrTableCell224
            // 
            this.xrTableCell224.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xrTableCell224.Name = "xrTableCell224";
            this.xrTableCell224.StylePriority.UseFont = false;
            this.xrTableCell224.StylePriority.UseTextAlignment = false;
            this.xrTableCell224.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell224.Weight = 0.093425633686121362D;
            // 
            // xrTableCell225
            // 
            this.xrTableCell225.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell225.Name = "xrTableCell225";
            this.xrTableCell225.StylePriority.UseFont = false;
            this.xrTableCell225.Text = " ";
            this.xrTableCell225.Weight = 0.87073685666505185D;
            // 
            // DetailReport_5
            // 
            this.DetailReport_5.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.GroupHeader5});
            this.DetailReport_5.DataMember = "SheetControl";
            this.DetailReport_5.DataSource = this.sqlDataSource;
            this.DetailReport_5.Expanded = false;
            this.DetailReport_5.Level = 4;
            this.DetailReport_5.Name = "DetailReport_5";
            // 
            // Detail5
            // 
            this.Detail5.HeightF = 0F;
            this.Detail5.Name = "Detail5";
            // 
            // GroupHeader5
            // 
            this.GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblDataPembiayaan,
            this.lblDataPembiayaan});
            this.GroupHeader5.HeightF = 106.5577F;
            this.GroupHeader5.Name = "GroupHeader5";
            // 
            // DetailReport_6
            // 
            this.DetailReport_6.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6,
            this.GroupHeader6,
            this.GroupFooter6});
            this.DetailReport_6.DataMember = "SheetControlDetailAsset";
            this.DetailReport_6.DataSource = this.sqlDataSource;
            this.DetailReport_6.Expanded = false;
            this.DetailReport_6.Level = 5;
            this.DetailReport_6.Name = "DetailReport_6";
            // 
            // Detail6
            // 
            this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9});
            this.Detail6.HeightF = 24.99999F;
            this.Detail6.Name = "Detail6";
            // 
            // xrTable9
            // 
            this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTable9.BorderWidth = 2F;
            this.xrTable9.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(23.99763F, 0F);
            this.xrTable9.Name = "xrTable9";
            this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow39});
            this.xrTable9.SizeF = new System.Drawing.SizeF(727.0034F, 24.99999F);
            this.xrTable9.StylePriority.UseBorders = false;
            this.xrTable9.StylePriority.UseBorderWidth = false;
            this.xrTable9.StylePriority.UseFont = false;
            this.xrTable9.StylePriority.UseTextAlignment = false;
            this.xrTable9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow39
            // 
            this.xrTableRow39.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow39.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell106,
            this.xrTableCell66,
            this.xrTableCell119,
            this.xrTableCell85,
            this.xrTableCell86});
            this.xrTableRow39.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTableRow39.Name = "xrTableRow39";
            this.xrTableRow39.StylePriority.UseBorders = false;
            this.xrTableRow39.StylePriority.UseFont = false;
            this.xrTableRow39.StylePriority.UseTextAlignment = false;
            this.xrTableRow39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableRow39.Weight = 1D;
            // 
            // xrTableCell106
            // 
            this.xrTableCell106.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell106.Name = "xrTableCell106";
            this.xrTableCell106.StylePriority.UseBorders = false;
            this.xrTableCell106.Weight = 0.074289786274528272D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell66.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ItemDesc]")});
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.StylePriority.UseBorders = false;
            this.xrTableCell66.Text = "Unit Description";
            this.xrTableCell66.Weight = 1.7551581738247672D;
            // 
            // xrTableCell119
            // 
            this.xrTableCell119.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AssetTypeDetail]")});
            this.xrTableCell119.Name = "xrTableCell119";
            this.xrTableCell119.StylePriority.UseTextAlignment = false;
            this.xrTableCell119.Text = "xrTableCell119";
            this.xrTableCell119.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell119.Weight = 0.71526822558459424D;
            // 
            // xrTableCell85
            // 
            this.xrTableCell85.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Year]")});
            this.xrTableCell85.Name = "xrTableCell85";
            this.xrTableCell85.StylePriority.UseTextAlignment = false;
            this.xrTableCell85.Text = "Year";
            this.xrTableCell85.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell85.Weight = 0.71526822558459413D;
            // 
            // xrTableCell86
            // 
            this.xrTableCell86.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Condition]")});
            this.xrTableCell86.Name = "xrTableCell86";
            this.xrTableCell86.StylePriority.UseTextAlignment = false;
            this.xrTableCell86.Text = "Condition";
            this.xrTableCell86.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell86.Weight = 0.87207048040964574D;
            // 
            // GroupHeader6
            // 
            this.GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
            this.GroupHeader6.HeightF = 59.99999F;
            this.GroupHeader6.Name = "GroupHeader6";
            // 
            // xrTable8
            // 
            this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable8.BorderWidth = 2F;
            this.xrTable8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(23.99763F, 9.999974F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow31,
            this.xrTableRow37});
            this.xrTable8.SizeF = new System.Drawing.SizeF(727.0034F, 50F);
            this.xrTable8.StylePriority.UseBorders = false;
            this.xrTable8.StylePriority.UseBorderWidth = false;
            this.xrTable8.StylePriority.UseFont = false;
            this.xrTable8.StylePriority.UseTextAlignment = false;
            this.xrTable8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow31
            // 
            this.xrTableRow31.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell92});
            this.xrTableRow31.Name = "xrTableRow31";
            this.xrTableRow31.Weight = 1D;
            // 
            // xrTableCell92
            // 
            this.xrTableCell92.BackColor = System.Drawing.SystemColors.ControlLight;
            this.xrTableCell92.Name = "xrTableCell92";
            this.xrTableCell92.StylePriority.UseBackColor = false;
            this.xrTableCell92.Text = " RINCIAN UNIT";
            this.xrTableCell92.Weight = 3.5032334955633035D;
            // 
            // xrTableRow37
            // 
            this.xrTableRow37.BackColor = System.Drawing.SystemColors.Control;
            this.xrTableRow37.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableRow37.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell112,
            this.xrTableCell37,
            this.xrTableCell118,
            this.xrTableCell38,
            this.xrTableCell42});
            this.xrTableRow37.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableRow37.Name = "xrTableRow37";
            this.xrTableRow37.StylePriority.UseBackColor = false;
            this.xrTableRow37.StylePriority.UseBorders = false;
            this.xrTableRow37.StylePriority.UseFont = false;
            this.xrTableRow37.StylePriority.UseTextAlignment = false;
            this.xrTableRow37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableRow37.Weight = 1D;
            // 
            // xrTableCell112
            // 
            this.xrTableCell112.BackColor = System.Drawing.SystemColors.Control;
            this.xrTableCell112.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell112.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell112.Name = "xrTableCell112";
            this.xrTableCell112.StylePriority.UseBackColor = false;
            this.xrTableCell112.StylePriority.UseBorders = false;
            this.xrTableCell112.StylePriority.UseFont = false;
            this.xrTableCell112.Weight = 0.074289859734998354D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.BackColor = System.Drawing.SystemColors.Control;
            this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell37.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseBackColor = false;
            this.xrTableCell37.StylePriority.UseBorders = false;
            this.xrTableCell37.StylePriority.UseFont = false;
            this.xrTableCell37.Text = "Unit Description";
            this.xrTableCell37.Weight = 1.7551581003642969D;
            // 
            // xrTableCell118
            // 
            this.xrTableCell118.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell118.Name = "xrTableCell118";
            this.xrTableCell118.StylePriority.UseFont = false;
            this.xrTableCell118.Text = "Asset Type";
            this.xrTableCell118.Weight = 0.71526822558459391D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.Text = "Year of Manufacture";
            this.xrTableCell38.Weight = 0.71526822558459424D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.Text = "Condition";
            this.xrTableCell42.Weight = 0.87207048040964574D;
            // 
            // GroupFooter6
            // 
            this.GroupFooter6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak2});
            this.GroupFooter6.HeightF = 2F;
            this.GroupFooter6.Name = "GroupFooter6";
            // 
            // xrPageBreak2
            // 
            this.xrPageBreak2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPageBreak2.Name = "xrPageBreak2";
            // 
            // docPrintSheetControl_B
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupFooter1,
            this.ReportFooter,
            this.PageFooter,
            this.DetailReport_1,
            this.DetailReport_2,
            this.DetailReport_3,
            this.DetailReport_4,
            this.DetailReport_5,
            this.DetailReport_6});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource});
            this.DataMember = "SheetControl";
            this.DataSource = this.sqlDataSource;
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margins = new System.Drawing.Printing.Margins(39, 39, 33, 33);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.AppNo,
            this.DocNo});
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.Version = "17.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblDataPembiayaan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
