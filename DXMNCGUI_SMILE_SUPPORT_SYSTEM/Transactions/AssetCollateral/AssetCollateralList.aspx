<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AssetCollateralList.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.AssetCollateral.AssetCollateralList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Alert Confirmation !" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout6" runat="server">
                    <Items>
                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None" ColCount="2">
                            <Items>
                                <dx:LayoutItem Caption="" ShowCaption="False" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                            <dx:ASPxLabel ID="lblmessage" ClientInstanceName="lblmessage" runat="server" Text="" Width="100%">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                            <dx:ASPxButton ID="btnSaveConfirm" runat="server" Text="OK" AutoPostBack="False" UseSubmitBehavior="false" Width="100" Theme="Office2010Black">
                                                <ClientSideEvents Click="function(s, e) { apcconfirm.Hide();cplMain.PerformCallback(cplMain.cplblActionButton + ';'+cplMain.cplblActionButton); }" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                            <dx:ASPxButton ID="btnCancelConfirm" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="100" Theme="Office2010Black">
                                                <ClientSideEvents Click="function(s, e) { apcconfirm.Hide(); }" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert Error!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Font-Names="Calibri">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Asset Collateral History" ColCount="4">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnRefresh" ClientInstanceName="btnRefresh" Text="Refresh" Image-Url="~/Content/Images/RefreshIcon-16x16.png" AutoPostBack="false" OnClick="btnRefresh_Click"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView 
                                    runat="server"
                                    ID="gvMain" 
                                    ClientInstanceName="gvMain" KeyFieldName="AST_CODE"
                                    EnableCallBacks="true" 
                                    OnInit="gvMain_Init" 
                                    OnCustomCellMerge="gvMain_CustomCellMerge" 
                                    OnDataBinding="gvMain_DataBinding" 
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                    <ClientSideEvents/>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true"/>
                                    <SettingsBehavior AllowCellMerge="true" AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="Disabled" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                    <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                    <SettingsPager Mode="ShowPager" PageSize="10" SEOFriendly="Enabled">
                                        <PageSizeItemSettings Visible="true"></PageSizeItemSettings>
                                    </SettingsPager>
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="ShowCustomizationWindow" DisplayMode="ImageWithText"/>
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
                                    <Columns>
                                        <dx:GridViewDataTextColumn Name="colAssetCode" Caption="Asset Code" FieldName="AST_CODE" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="0">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colAssetName" Caption="Asset Name" FieldName="AST_NAME" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colLocation" Caption="Current Location" FieldName="LOCATION" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="2">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDetailLocation" Caption="Current Loc. Detail" FieldName="DETAIL_LOCATION" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="3">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colRemarks" Caption="Remark" FieldName="REMARKS" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewBandColumn Caption="History Listing">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="Transaction By" FieldName="CRE_BY" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="5">
                                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn Name="colCreatedDate" Caption="Created Date" FieldName="CRE_DATE" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="false" VisibleIndex="6" Width="10%">
                                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataDateColumn Name="colDate" Caption="Transaction Date" FieldName="DATE" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="true" VisibleIndex="7" Width="10%">
                                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn Name="colFromLoc" Caption="From Location" FieldName="FROM_LOC" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="8">
                                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="colFromLoc" Caption="To Location" FieldName="TO_LOC" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="9">
                                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn Name="colPromiseDate" Caption="Promise Date" FieldName="PROMISE_DATE" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="true" VisibleIndex="10" Width="10%">
                                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                                </dx:GridViewDataDateColumn>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Names="Calibri"/>
                                        </dx:GridViewBandColumn>
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
