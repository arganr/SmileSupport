<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="CreditProcessSummary.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.CreditProcessSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        function cplMain_EndCallback()
        {
            switch (cplMain.cpCallbackParam)
            {
                case "INQUIRY":
                    gvMain.PerformCallback();
                    gvMain2.PerformCallback();
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
            <dx:LayoutGroup ShowCaption="True" Caption="Credit Process Summary" GroupBoxDecoration="HeadingLine" ColCount="1">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="Transparent" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutGroup Caption="Filter Setting" ColCount="5">
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
                                                <dx:ListBoxColumn FieldName="StateDescription" Caption="Status"></dx:ListBoxColumn>
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
                                    ClientInstanceName="gvMain" KeyFieldName="Status" 
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
                                    <SettingsDetail ShowDetailRow="true"/>
                                    <SettingsContextMenu Enabled="true">
                                        <RowMenuItemVisibility CollapseDetailRow="true" ExpandDetailRow="true" Refresh="true"></RowMenuItemVisibility>
                                    </SettingsContextMenu>
                                    <Columns>
                                        <dx:GridViewBandColumn Caption="Application Summary">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Status" Name="colStatus" Caption="Status" Width="10%" VisibleIndex="0">
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AppsCount" Name="colAppsCount" Caption="Application Count" Width="15%" VisibleIndex="1">
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewBandColumn Caption="">
                                                    <Columns>
                                                        <dx:GridViewDataSpinEditColumn FieldName="TotalNTF" Name="colTotalNTF" Caption="Total NTF" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="2">
                                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="TotalDP" Name="colTotalDP" Caption="Total DP" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="3">
                                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="TotalOTR" Name="colTotalOTR" Caption="Total OTR" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="4">
                                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                </dx:GridViewBandColumn>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Names="Calibri"/>
                                        </dx:GridViewBandColumn>
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <dx:ASPxGridView 
                                                runat="server" 
                                                ID="gvDetail" 
                                                ClientInstanceName="gvDetail"
                                                Width="100%" KeyFieldName="Status" 
                                                AutoGenerateColumns="False"
                                                EnableCallBacks="true"
                                                EnablePagingCallbackAnimation="true"
                                                OnInit="gvDetail_Init"
                                                OnDataBinding="gvDetail_DataBinding"
                                                OnFocusedRowChanged="gvDetail_FocusedRowChanged"
                                                OnCustomCallback="gvDetail_CustomCallback" 
                                                OnBeforePerformDataSelect="gvDetail_BeforePerformDataSelect" 
                                                OnCustomUnboundColumnData="gvDetail_CustomUnboundColumnData"
                                                EnableTheming="True"
                                                Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                                <ClientSideEvents />
                                                <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                <Settings ShowFilterRow="false" ShowGroupPanel="False" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                <SettingsSearchPanel Visible="False" />
                                                <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                <SettingsLoadingPanel Mode="Disabled" />
                                                <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" FileName="Application Detail Summary"/>
                                                <SettingsPager>
                                                    <PageSizeItemSettings Visible="true" Items="10, 20, 50"></PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Branch" Name="colBranch" Caption="Branch" VisibleIndex="0" Width="115px"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="DocNo" Name="colDocNo" Caption="Document No." VisibleIndex="1" Width="173px"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataSpinEditColumn FieldName="NTF" Name="colNTF" Caption="NTF" VisibleIndex="2" PropertiesSpinEdit-DisplayFormatString="#,0.00"></dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataDateColumn FieldName="DocDate" Name="colDocDate" Caption="Document Date" VisibleIndex="3" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ClientName" Name="colClientName" Caption="Client Name" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Status" Name="colStatus" Caption="Status" VisibleIndex="5" Visible="false"></dx:GridViewDataTextColumn>
                                                </Columns>
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="NTF" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                                </TotalSummary>
                                            </dx:ASPxGridView>
                                        </DetailRow>
                                    </Templates>
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
                                        <dx:ASPxSummaryItem FieldName="AppsCount" SummaryType="Sum" DisplayFormat="#,##"/>
                                        <dx:ASPxSummaryItem FieldName="TotalOTR" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="TotalDP" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="TotalNTF" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                    </TotalSummary>
                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true" Footer-HorizontalAlign="Center" Footer-ForeColor="#255658" Footer-Font-Size="Larger">
                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                    </Styles>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    runat="server" 
                                    ID="gvMain2" 
                                    ClientInstanceName="gvMain2" KeyFieldName="Status" 
                                    Width="100%" 
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnInit="gvMain2_Init"
                                    OnDataBinding="gvMain2_DataBinding"
                                    OnFocusedRowChanged="gvMain2_FocusedRowChanged"
                                    OnCustomCallback="gvMain2_CustomCallback"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                    <ClientSideEvents />
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" AllowSort="false"/>
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="False" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="ShowAsPopup" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" FileName="Application Summary Cancelled & Rejected"/>
                                    <SettingsPager PageSize="30"></SettingsPager>
                                    <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                    <SettingsDetail ShowDetailRow="true" ExportMode="All"/>
                                    <SettingsContextMenu Enabled="true">
                                        <RowMenuItemVisibility CollapseDetailRow="true" ExpandDetailRow="true" Refresh="true"></RowMenuItemVisibility>
                                    </SettingsContextMenu>
                                    <Columns>
                                        <dx:GridViewBandColumn Caption="Application Summary ( Cancelled & Rejected )">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Status" Name="colStatus" Caption="Status" Width="10%" VisibleIndex="0">
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AppsCount" Name="colAppsCount" Caption="Application Count" Width="15%" VisibleIndex="1">
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewBandColumn Caption="">
                                                    <Columns>
                                                        <dx:GridViewDataSpinEditColumn FieldName="TotalNTF" Name="colTotalNTF" Caption="Total NTF" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="2">
                                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="TotalDP" Name="colTotalDP" Caption="Total DP" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="3">
                                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="TotalOTR" Name="colTotalOTR" Caption="Total OTR" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="4">
                                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                </dx:GridViewBandColumn>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Names="Calibri"/>
                                        </dx:GridViewBandColumn>
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <dx:ASPxGridView 
                                                runat="server" 
                                                ID="gvDetail2" 
                                                ClientInstanceName="gvDetail2"
                                                Width="100%" KeyFieldName="Status" 
                                                AutoGenerateColumns="False"
                                                EnableCallBacks="true"
                                                EnablePagingCallbackAnimation="true"
                                                OnInit="gvDetail_Init"
                                                OnDataBinding="gvDetail2_DataBinding"
                                                OnFocusedRowChanged="gvDetail2_FocusedRowChanged"
                                                OnCustomCallback="gvDetail2_CustomCallback" 
                                                OnBeforePerformDataSelect="gvDetail2_BeforePerformDataSelect" 
                                                OnCustomUnboundColumnData="gvDetail2_CustomUnboundColumnData"
                                                EnableTheming="True"
                                                Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                                <ClientSideEvents />
                                                <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                <Settings ShowFilterRow="false" ShowGroupPanel="False" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                <SettingsSearchPanel Visible="False" />
                                                <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                <SettingsLoadingPanel Mode="Disabled" />
                                                <SettingsExport EnableClientSideExportAPI="false" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4"/>
                                                <SettingsPager>
                                                    <PageSizeItemSettings Visible="true" Items="10, 20, 50"></PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Branch" Name="colBranch" Caption="Branch" VisibleIndex="0" Width="115px"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="DocNo" Name="colDocNo" Caption="Document No." VisibleIndex="1" Width="173px"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataSpinEditColumn FieldName="NTF" Name="colNTF" Caption="NTF" VisibleIndex="2" PropertiesSpinEdit-DisplayFormatString="#,0.00"></dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataDateColumn FieldName="DocDate" Name="colDocDate" Caption="Document Date" VisibleIndex="3" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ClientName" Name="colClientName" Caption="Client Name" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Status" Name="colStatus" Caption="Status" VisibleIndex="5" Visible="false"></dx:GridViewDataTextColumn>
                                                </Columns>
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="NTF" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                                </TotalSummary>
                                            </dx:ASPxGridView>
                                        </DetailRow>
                                    </Templates>
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
                                        <dx:ASPxSummaryItem FieldName="AppsCount" SummaryType="Sum" DisplayFormat="#,##"/>
                                        <dx:ASPxSummaryItem FieldName="TotalOTR" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="TotalDP" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                        <dx:ASPxSummaryItem FieldName="TotalNTF" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
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