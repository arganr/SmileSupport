<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PeminjamanDocList.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.PeminjamanDocument.PeminjamanDocList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case 'NEW':
                    break;
            }
        }
        function gvApprovalList_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case "btnShow":
                    cplMain.PerformCallback("SHOW;SHOW");
                    break;
            }
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert Error!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcApproval" ClientInstanceName="apcApproval" runat="server" Modal="True" Theme="Aqua" Width="750px" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Your approval list.." AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
        <SettingsAdaptivity Mode="OnWindowInnerWidth" MaxHeight="100%" MaxWidth="100%"/>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxFormLayout runat="server">
                    <Items>
                        <dx:LayoutItem ShowCaption="False" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridView 
                                        ID="gvApprovalList"
                                        ClientInstanceName="gvApprovalList"
                                        runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="False"
                                        EnableCallBacks="true"
                                        EnablePagingCallbackAnimation="true"
                                        EnableTheming="True" OnDataBinding="gvApprovalList_DataBinding" 
                                        Theme="Aqua" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DocKey">
                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                        <ClientSideEvents CustomButtonClick="function(s, e) { gvApprovalList_CustomButtonClick(s, e); }"/>
                                        <Settings ShowFilterRow="false" ShowGroupPanel="False" ShowFilterRowMenu="false" ShowFilterBar="Hidden" ShowHeaderFilterButton="true" ShowFooter="false" />
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                        <SettingsLoadingPanel Mode="Disabled" />
                                        <SettingsPager PageSize="5"></SettingsPager>
                                        <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colDocNo" Caption="Doc No." FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" Width="20%">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colDocCategory" Caption="Category" FieldName="DocCategory" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="2">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colDepartment" Caption="Department" FieldName="Department" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3" Width="20%">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="4" Caption=" " Width="6%">
                                                <HeaderStyle Font-Bold="true" />
                                                <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnShow" Text="Show"></dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                        </Columns>
                                        <Styles>
                                            <AlternatingRow Enabled="True"></AlternatingRow>
                                        </Styles>
                                        <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                                    </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Font-Names="Calibri">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Peminjaman Document" ColCount="4">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxButton ID="btnEdit" ClientInstanceName="btnEdit" runat="server" Text="Edit" BackColor="LightGray" OnClick="btnEdit_Click" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxButton ID="btnMasterDocLoc" ClientInstanceName="btnMasterDocLoc" runat="server" Text="Master Doc Location" OnClick="btnMasterDocLoc_Click" BackColor="LightGray" Width="100%" ClientVisible ="false"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="15%">
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
                                    KeyFieldName="DocKey"
                                    runat="server"
                                    Width="100%"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnDataBinding="gvMain_DataBinding"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri" EnableCallbackAnimation="true">
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="Disabled" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                    <SettingsPager PageSize="10"></SettingsPager>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="DocKey" FieldName="DocKey" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="0" Visible="false">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="1">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Caption="DocDate" FieldName="DocDate" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="2">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Caption="DocID" FieldName="DocID" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="3">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Description" FieldName="Description" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="4">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Doc Status" FieldName="Status" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="5">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Category" FieldName="DocCategory" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="6">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Caption="Tgl Peminjaman" FieldName="TglPeminjaman" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="7">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataDateColumn Caption="Tgl Pengembalian" FieldName="TglPengembalian" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="8">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Caption="Created By" FieldName="CreatedBy" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="9">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Caption="Created Date" FieldName="CreatedDateTime" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="10">
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
