<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ApplicationSyariahSummary.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcessSyariah.ApplicationSyariahSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        function cplMain_EndCallback()
        {
            switch (cplMain.cpCallbackParam)
            {
                case "INQUIRY":
                    //gvMain.PerformCallback();
                    //gvMain2.PerformCallback();
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
            <dx:LayoutGroup ShowCaption="True" Caption="Credit Process Syariah Summary" GroupBoxDecoration="HeadingLine" ColCount="1">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="Transparent" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutGroup Caption="Filter Setting" ColCount="5" Visible="false">
                        <GroupBoxStyle Caption-BackColor="#f8fafd"></GroupBoxStyle>
                        <Items>
                            <dx:LayoutItem ShowCaption="False" Caption="">
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
                            <dx:LayoutItem ShowCaption="False" Caption="">
                                <CaptionSettings Location="Left" />
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox 
                                            runat="server" 
                                            ID="cbStatus" 
                                            ClientInstanceName="cbStatus" 
                                            Width="100%" 
                                            NullText="Status..." 
                                            HelpText="this filter will applicable for all transaction include reject and cancel base on application date, clear date periode to get all transaction."
                                            ValueType="System.String" 
                                            ValueField="StateDescription"
                                            OnDataBinding="cbStatus_DataBinding"
                                            DropDownStyle="DropDownList">
                                            <Columns>
                                                <dx:ListBoxColumn FieldName="STEP" Caption="Status"></dx:ListBoxColumn>
                                            </Columns>
                                            <HelpTextStyle Font-Italic="false" Font-Size="Smaller" ForeColor="#255658" Wrap="True"></HelpTextStyle>
                                            <HelpTextSettings DisplayMode="Popup" ></HelpTextSettings>
                                            <ClientSideEvents ButtonClick="OnBtnClearClick"/>
                                            <Buttons>  
                                                <dx:EditButton Width="15px" ToolTip="click here to clear text" Text="X" Position="Left"></dx:EditButton> 
                                            </Buttons>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="False" Caption="">
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
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="False" Caption="">
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
                                    ClientInstanceName="gvMain" KeyFieldName="CABANG"
                                    Width="100%" 
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnInit="gvMain_Init"
                                    OnDataBinding="gvMain_DataBinding"
                                    OnFocusedRowChanged="gvMain_FocusedRowChanged"
                                    OnCustomCallback="gvMain_CustomCallback"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                    <ClientSideEvents />
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="False" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" AllowSort="false"/>
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="False" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="ShowAsPopup" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" FileName="Application Summary"/>
                                    <SettingsPager PageSize="30"></SettingsPager>
                                    <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                    <SettingsDetail ShowDetailRow="false"/>
                                    <SettingsContextMenu Enabled="true">
                                        <RowMenuItemVisibility CollapseDetailRow="true" ExpandDetailRow="true" Refresh="true"></RowMenuItemVisibility>
                                    </SettingsContextMenu>
                                    <Columns>
                                        <dx:GridViewBandColumn Caption="">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="CABANG" Name="colBranch" Caption="Branch" Width="10%">
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="JUMLAH-APLIKASI" Name="colAppsCount" Caption="Application Count" Width="12%">
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewBandColumn Caption="Current Status">
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                    <Columns>
                                                        <dx:GridViewBandColumn Caption="Internal Process">
                                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <Columns>
                                                        <dx:GridViewDataSpinEditColumn FieldName="APPIN" Name="colAPPIN" Caption="App In">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="SLIK-CHECKING" Name="colCHECKSLIK" Caption="Check Slik">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="CREDIT-COMMITTEE" Name="colCREDITCOMMITE" Caption="Credit Commite">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="LEGAL-SIGNING CONTRACT" Name="colLEGALSIGNINGCONTRACT" Caption="Signing Contract">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="LEGAL-CHECKLIST" Name="colLEGALCHECKLIST" Caption="Checklist">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="LEGAL-FIRST-PAYMENT" Name="colLEGALFIRSTPAYMENT" Caption="First Payment">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="PEMBUKAAN BTH" Name="colPEMBUKAANBTH" Caption="Pembukaan BTH">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="READY-TO-GOLIVE" Name="colREADYTOGOLIVE" Caption="Ready To Golive">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="GOLIVE" Name="colGOLIVE" Caption="Golive">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="READY-DISBURSE" Name="colDISBURSEREQUEST" Caption="Disburse Request">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewBandColumn Caption="Disburse">
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                    <Columns>
                                                        <dx:GridViewDataSpinEditColumn FieldName="DISBURSE" Name="colDISBURSE" Caption="Disburse">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="BPIH" Name="colBPIH" Caption="BPIH">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="SPPH" Name="colSPPH" Caption="SPPH">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="DONE" Name="colDONE" Caption="DONE">
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                </dx:GridViewBandColumn>
                                                    </Columns>
                                                </dx:GridViewBandColumn>
                                            </Columns>
                                        </dx:GridViewBandColumn>
                                    </Columns>
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                            <Items>
                                                <dx:GridViewToolbarItem Text="Export Menu" DropDownMode="true">
                                                    <Items>
                                                        <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to .xlsx" ToolTip="Click here to export grid data to excel, exclude detail rows." ItemStyle-Font-Names="Calibri" />
                                                        <dx:GridViewToolbarItem Command="ExportToXls" Text="Export to .xls" ToolTip="Click here to export grid data to excel, exclude detail rows." ItemStyle-Font-Names="Calibri" />
                                                        <dx:GridViewToolbarItem Command="ExportToPdf" Text="Export to .pdf" ToolTip="Click here to export grid data to pdf, exclude detail rows." ItemStyle-Font-Names="Calibri" />
                                                        <dx:GridViewToolbarItem Command="ExportToDocx" Text="Export to .docx" ToolTip="Click here to export grid data to docx, exclude detail rows." ItemStyle-Font-Names="Calibri" />
                                                    </Items>
                                                </dx:GridViewToolbarItem>
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <TotalSummary>
                                        <%--<dx:ASPxSummaryItem FieldName="AppsCount" SummaryType="Sum" DisplayFormat="#,##"/>--%>
                                        <%--<dx:ASPxSummaryItem FieldName="TotalOTR" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>--%>
                                        <%--<dx:ASPxSummaryItem FieldName="TotalDP" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>--%>
                                        <%--<dx:ASPxSummaryItem FieldName="TotalNTF" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>--%>
                                    </TotalSummary>
                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true" Footer-HorizontalAlign="Center" Footer-ForeColor="#255658" Footer-Font-Size="Larger">
                                        <AlternatingRow Enabled="True"></AlternatingRow>
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
