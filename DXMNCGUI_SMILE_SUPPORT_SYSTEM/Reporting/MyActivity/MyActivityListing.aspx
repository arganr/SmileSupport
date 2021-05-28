<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="MyActivityListing.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.MyActivity.MyActivityListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <dx:ASPxFormLayout  ID="ASPxFormLayout1" runat="server" Width="100%">
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
            <Items>
                <dx:LayoutGroup ShowCaption="True" Caption="My Activity" GroupBoxDecoration="HeadingLine" ColCount="1">
                    <GroupBoxStyle BackColor="Transparent" Caption-BackColor="Transparent" Caption-Font-Bold="true"></GroupBoxStyle>
                    <Items>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridView
                                    runat="server" 
                                    ID="gvMain" 
                                    ClientInstanceName="gvMain" KeyFieldName="Status" 
                                    Width="100%" 
                                    AutoGenerateColumns="True"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnDataBinding="gvMain_DataBinding"
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
</asp:Content>
