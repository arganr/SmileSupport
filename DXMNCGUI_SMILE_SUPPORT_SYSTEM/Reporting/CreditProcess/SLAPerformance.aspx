<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="SLAPerformance.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.SLAPerformance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        function cplMain_EndCallback()
        {
            switch (cplMain.cpCallbackParam)
            {
                case "INQUIRY":
                    gvMain.PerformCallback();
                    break;
            }
        }
        function OnBtnClearClick(s)
        {
            s.SetValue(null);
        }
    </script>
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Alert Confirmation!" AllowDragging="True" PopupAnimationType="None" EnableViewState="False">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2">
                    <Items>
                        <dx:LayoutItem Caption="" ColSpan="2" ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                    <dx:ASPxLabel ID="lblmessage" ClientInstanceName="lblmessage" runat="server" Text="">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                    <dx:ASPxButton ID="btnSaveConfirm" runat="server" Text="OK" AutoPostBack="False" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s, e) { apcconfirm.Hide();gvIncidentList.PerformCallback(gvIncidentList.cplblActionButton + ';'+gvIncidentList.cplblActionButton); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                    <dx:ASPxButton ID="btnCancelConfirm" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s, e) { apcconfirm.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert Warning!" AllowDragging="True" PopupAnimationType="None"
        EnableViewState="False" Width="400" Height="200">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup ShowCaption="True" Caption="Service Level Agreement Performance" GroupBoxDecoration="HeadingLine" ColCount="1">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="Transparent" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutGroup Caption="Filter Setting" ColCount="1">
                        <GroupBoxStyle Caption-BackColor="#f8fafd"></GroupBoxStyle>
                            <Items>
                                <dx:LayoutItem ShowCaption="False" Caption="" Width="20%">
                                    <CaptionSettings Location="Left" />
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxDateEdit runat="server" ID="deFrom" ClientInstanceName="deFrom" Width="100%" EditFormat="Date" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" NullText="From..." HelpText="this filter will applicable for all transaction include reject and cancel base on application date, clear date periode to get all transaction.">
                                                <HelpTextStyle Font-Italic="false" Font-Size="Smaller" ForeColor="#255658" Wrap="True"></HelpTextStyle>
                                                <HelpTextSettings DisplayMode="Popup" ></HelpTextSettings>
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>
                                <dx:LayoutItem ShowCaption="False" Caption="" Width="20%">
                                    <CaptionSettings Location="Left" />
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxDateEdit runat="server" ID="deTo" ClientInstanceName="deTo" Width="100%" EditFormat="Date" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" NullText="To..." HelpText="this filter will applicable for all transaction include reject and cancel base on application date, clear date periode to get all transaction.">
                                                <HelpTextStyle Font-Italic="false" Font-Size="Smaller" ForeColor="#255658" Wrap="True"></HelpTextStyle>
                                                <HelpTextSettings DisplayMode="Popup" ></HelpTextSettings>
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>
                                <dx:LayoutItem ShowCaption="False" Caption="" Width="20%" HorizontalAlign="Right">
                                    <CaptionSettings Location="Left" />
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton runat="server" ID="btnInquiry" ClientInstanceName="btnInquiry" Text="Inquiry" Width="100px" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('INQUIRY;INQUIRY'); }" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    runat="server" 
                                    ID="gvMain" 
                                    ClientInstanceName="gvMain" KeyFieldName="Status" 
                                    Width="100%" 
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnInit="gvMain_Init"
                                    OnDataBinding="gvMain_DataBinding"
                                    OnFocusedRowChanged="gvMain_FocusedRowChanged"
                                    OnCustomCallback="gvMain_CustomCallback" 
                                    OnCustomUnboundColumnData="gvMain_CustomUnboundColumnData"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                    <ClientSideEvents />
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" HorizontalScrollBarMode="Visible"/>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" AllowSort="true"/>
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="False" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="ShowAsPopup" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" FileName="SLA Performance"/>
                                    <SettingsPager PageSize="30"></SettingsPager>
                                    <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                    <SettingsDetail ShowDetailRow="false"/>
                                    <SettingsContextMenu Enabled="true">
                                        <RowMenuItemVisibility CollapseDetailRow="true" ExpandDetailRow="true" Refresh="true"></RowMenuItemVisibility>
                                    </SettingsContextMenu>
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="DocNo" Name="colDocNo" Caption="Application No." Width="120" VisibleIndex="0" FixedStyle="Left">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn FieldName="DocDate" Name="colDocDate" Caption="Application Date" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Width="120" VisibleIndex="1">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn FieldName="Seq" Name="colSeq" Caption="Seq" Width="50" VisibleIndex="2">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Status" Name="colStatus" Caption="Current Status" Width="120" VisibleIndex="3">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="LamaProses" Name="colLamaProses" Caption="Lama Proses (Hari)" VisibleIndex="4" PropertiesSpinEdit-DisplayFormatString="#,0.00">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn FieldName="IsReturn" Name="colIsReturn" Caption="Retun/CAM?" Width="120" VisibleIndex="5">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="OnHold" Name="colOnHold" Caption="Hold?" Width="120" VisibleIndex="6">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Branch" Name="colBranch" Caption="Branch" Width="120" VisibleIndex="7">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ClientName" Name="colClientName" Width="120" Caption="Client Name" FixedStyle="Left" VisibleIndex="8">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ObjectPembiayaan" Name="colObjectPembiayaan" Width="120" Caption="Lease Object" VisibleIndex="9">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="JenisPengikatan" Name="colJenisPengikatan" Width="120" Caption="Binding Type" VisibleIndex="10">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="NTF" Name="colNTF" Caption="NTF" PropertiesSpinEdit-DisplayFormatString="#,0.00" Width="120" VisibleIndex="11">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle Font-Bold="true"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="BRH_MGR" Name="colBRH_MGR" Caption="BRH MGR" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="12">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="APP_IN" Name="colAPP_IN" Caption="APP IN" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="13">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CA" Name="colCA" Caption="CA" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="14">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="HOLDING" Name="colHOLDING" Caption="HOLDING" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="15">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="AM" Name="colAM" Caption="AM" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="16">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CA_HEAD" Name="colCA_HEAD" Caption="CA HEAD" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="17">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="SIRKULASI_KK" Name="colSirkulasiKK" Caption="SIRKULASI KK" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="18">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CMO" Name="colCMO" Caption="CMO" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="19">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="COO" Name="colCOO" Caption="COO" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="20">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CFO" Name="colCFO" Caption="CFO" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="21">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CEO" Name="colCEO" Caption="CEO" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="22">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CA_SKK" Name="colCASKK" Caption="CA SKK" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="23">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CEK_SKK" Name="colCEKSKK" Caption="CEK SKK" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="24">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="LEMBAR_KONTROL" Name="colLembarKontrol" Caption="LEMBAR KONTROL" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="25">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="PREPARE_CETAK_KONTRAK" Name="colPrepareCetakKontrak" Caption="PREPARE CETAK KONTRAK" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="26">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="REVIEW_KONTRAK" Name="colREVIEWKONTRAK" Caption="REVIEW KONTRAK" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="27">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CETAK_KONTRAK" Name="colCETAKKONTRAK" Caption="CETAK KONTRAK" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="28">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="SIGNING" Name="colSIGNING" Caption="SIGNING" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="29">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CEK_KONTRAK" Name="colCEKKONTRAK" Caption="CEK KONTRAK" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="30">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CHECKLIST" Name="colCHECKLIST" Caption="CHECKLIST" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="31">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="PREPARE_PO" Name="colPREPARE_PO" Caption="PREPARE PO" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="32">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="PO" Name="colPO" Caption="PO" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="33">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="TAGIHAN" Name="colTAGIHAN" Caption="TAGIHAN" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="34">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="PREPARE_DISBURSE" Name="colPREPARE_DISBURSE" Caption="PREPARE DISBURSE" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="35">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="CREDAM" Name="colCREDAM" Caption="CREDAM" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="36">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="FINANCE" Name="colFINANCE" Caption="FINANCE" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="37">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="DONE" Name="colDONE" Caption="DONE" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="38">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn  FieldName="TIME_TO_YES" Name="colTIME_TO_YES" Caption="TIME TO YES" PropertiesSpinEdit-DisplayFormatString="#,0.00" UnboundType="Decimal" Width="150" VisibleIndex="39">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="TIME_TO_DISBURSE" Name="colTIME_TO_DISBURSE" Caption="TIME TO DISBURSE" PropertiesSpinEdit-DisplayFormatString="#,0.00" UnboundType="Decimal" Width="150" VisibleIndex="40">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                    </Columns>
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                            <Items>
                                                <dx:GridViewToolbarItem Text="Export Menu" DropDownMode="true">
                                                    <Items>
                                                        <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to .xlsx" ToolTip="Click here to export grid data to excel, exclude detail rows." ItemStyle-Font-Names="Calibri"/>
                                                        <dx:GridViewToolbarItem Command="ExportToXls" Text="Export to .xls" ToolTip="Click here to export grid data to excel, exclude detail rows." ItemStyle-Font-Names="Calibri"/>
                                                        <dx:GridViewToolbarItem Command="ExportToPdf" Text="Export to .pdf" ToolTip="Click here to export grid data to pdf, exclude detail rows." ItemStyle-Font-Names="Calibri"/>
                                                        <dx:GridViewToolbarItem Command="ExportToDocx" Text="Export to .docx" ToolTip="Click here to export grid data to docx, exclude detail rows." ItemStyle-Font-Names="Calibri"/>
                                                    </Items>
                                                </dx:GridViewToolbarItem>
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="NTF" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="BRH_MGR" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="APP_IN" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CA" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="HOLDING" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="AM" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CA_HEAD" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="SIRKULASI_KK" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CMO" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="COO" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CFO" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CEO" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CA_SKK" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CEK_SKK" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="LEMBAR_KONTROL" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="PREPARE_CETAK_KONTRAK" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="REVIEW_KONTRAK" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CETAK_KONTRAK" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="SIGNING" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CEK_KONTRAK" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CHECKLIST" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="PREPARE_PO" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="PO" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="TAGIHAN" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="PREPARE_DISBURSE" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="CREDAM" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="FINANCE" SummaryType="Average" DisplayFormat="#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="DONE" SummaryType="Average" DisplayFormat="#,0.00"/>
                                    </TotalSummary>
                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true" Footer-HorizontalAlign="Center" Footer-ForeColor="#255658" Footer-Font-Size="Larger">
                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                        <FixedColumn BackColor="LightYellow"></FixedColumn>
                                    </Styles>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
