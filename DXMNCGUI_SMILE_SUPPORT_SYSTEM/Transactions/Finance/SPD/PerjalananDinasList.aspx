<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PerjalananDinasList.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.SPD.PerjalananDinasList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <dx:ASPxHiddenField  runat="server" ID="HiddenField" ClientInstanceName="HiddenField"></dx:ASPxHiddenField>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert Error!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>

    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Font-Names="Calibri">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Perjalanan Dinas Listing" ColCount="4">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxButton ID="btnNew" ClientInstanceName="btnNew" runat="server" Text="New" BackColor="LightGray" OnClick="btnNew_Click" ToolTip="Click here to create a new document" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxButton ID="btnView" ClientInstanceName="btnView" runat="server" Text="View" BackColor="LightGray" OnClick="btnView_Click" ToolTip="Click here to view document" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                <dx:ASPxButton ID="btnEdit" ClientInstanceName="btnEdit" runat="server" Text="Edit" BackColor="LightGray" OnClick="btnEdit_Click" ToolTip="Click here to edit document" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>

            <dx:LayoutGroup ShowCaption="False" ColCount="1" GroupBoxDecoration="None">
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvMain"
                                    ClientInstanceName="gvMain"
                                    runat="server"
                                    KeyFieldName="DocKey"
                                    Width="100%"
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnDataBinding="gvMain_DataBinding"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri" EnableCallbackAnimation="true">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                    </SettingsAdaptivity>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="Disabled" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                    <SettingsPager PageSize="10"></SettingsPager>
                                    <SettingsResizing ColumnResizeMode="Control" Visualization="Live"/>
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="ShowCustomizationWindow" DisplayMode="ImageWithText" />
                                                <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to .xlsx" ToolTip="Click here to export grid data to excel" />
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDocNo" Caption="DocNo" FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colDocDate" Caption="Doc Date" FieldName="DocDate" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colStatus" Caption="Status" FieldName="Status" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colNIK" Caption="NIK" FieldName="NIK" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colName" Caption="Name" FieldName="Name" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="Created By" FieldName="CRE_BY" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colCreatedDateTime" Caption="Created Date" FieldName="CRE_DATE" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                    </Columns>
                                    <Styles>
                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                    </Styles>
                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                                    <SettingsDetail ShowDetailRow="false" />
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
