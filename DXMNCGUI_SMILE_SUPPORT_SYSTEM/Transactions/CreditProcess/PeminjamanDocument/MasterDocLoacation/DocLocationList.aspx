<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="DocLocationList.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.PeminjamanDocument.MasterDocLoacation.DocLocationList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "HIST":
                    gvHistory.Refresh();
                    break;
            }
            cplMain.cpCallbackParam = null;
        }
        function OnMainGridFocusedRowChanged() {
            gvMain.GetRowValues(gvMain.GetFocusedRowIndex(), 'DocID;', OnMainGetRowValues);
        }
        function OnMainGetRowValues(values) {
            cplMain.PerformCallback("HIST;" + values[0].toString());
            
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert Error!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Font-Names="Calibri">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Master Document Location" ColCount="4">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxButton ID="btnNew" ClientInstanceName="btnNew" runat="server" Text="New" BackColor="LightGray" OnClick="btnNew_Click" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxButton ID="btnEdit" ClientInstanceName="btnEdit" runat="server" Text="Edit" BackColor="LightGray" OnClick="btnEdit_Click" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                <dx:ASPxButton ID="btnApprovalList" ClientInstanceName="btnApprovalList" runat="server" Text="List Approval" Width="100%" AutoPostBack="false" OnClick="btnApprovalList_Click"></dx:ASPxButton>
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
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnDataBinding="gvMain_DataBinding"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri" EnableCallbackAnimation="true">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) { OnMainGridFocusedRowChanged(); }"/>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="Disabled" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                    <SettingsPager PageSize="10"></SettingsPager>
                                    <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="ShowCustomizationWindow" DisplayMode="ImageWithText" />
                                                <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to .xlsx" ToolTip="Click here to export grid data to excel" />
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="DocKey" Caption="Doc. Key" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DocID" Caption="Doc. ID" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DocCategory" Caption="Category" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Location" Caption="Location" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ReffNum" Caption="Ref. Number" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="USER_NAME" Caption="Created By" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CRE_DATE" Caption="Created Date" ></dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel ID="lblHistory" runat="server" Text="History :" Font-Bold="true" Font-Names="Calibri" ForeColor="SlateGray"></dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView 
                                    runat="server" 
                                    ID="gvHistory" 
                                    ClientInstanceName="gvHistory" 
                                    Width="100%" 
                                    EnableTheming="true" 
                                    Theme="Glass" 
                                    OnDataBinding="gvHistory_DataBinding"
                                    Font-Size="8" Font-Names="Calibri">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Name="colDocID" Caption="Doc ID" FieldName="DocID" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="0">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDocCategory" Caption="Doc Category" FieldName="DocCategory" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="1">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colReffNum" Caption="Ref Numb." FieldName="ReffNum" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="2">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDescription" Caption="Description" FieldName="Description" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="3">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colLocation" Caption="Location" FieldName="Location" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="4">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colUser" Caption="Audit User" FieldName="USER_NAME" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="5">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colCREDATE" Caption="Audit Date" FieldName="CRE_DATE" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="6">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                    </Columns>
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
