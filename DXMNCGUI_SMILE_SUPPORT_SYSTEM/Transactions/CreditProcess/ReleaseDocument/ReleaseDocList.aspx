<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ReleaseDocList.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.ReleaseDocument.ReleaseDocList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "dafault":
                    break;
                
            }
            cplMain.cpCallbackParam = null;
        }

        function gvApprovalList_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case "btnShow":
                    cplMain.PerformCallback("SHOW;SHOW");
                    break;
            }
        }

        function gvWaiveApprovalList_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case "btnShowWaive":
                    cplMain.PerformCallback("SHOW_WAIVE;SHOW_WAIVE");
                    break;
            }
        }

        function gvReleaseList_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case "btnShowRelease":
                    cplMain.PerformCallback("SHOW_RELEASE;SHOW_RELEASE");
                    break;
            }
        }

        
    </script>
    <dx:ASPxHiddenField  runat="server" ID="HiddenField" ClientInstanceName="HiddenField"></dx:ASPxHiddenField>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert Error!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcApproval" ClientInstanceName="apcApproval" runat="server" Modal="True" Theme="Aqua" Width="850px" CloseAction="CloseButton"
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
                                        EnableTheming="True" 
                                        OnDataBinding="gvApprovalList_DataBinding" OnCustomCallback="gvApprovalList_CustomCallback"
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
                                            <dx:GridViewDataTextColumn Name="colAgreementNo" Caption="Agreement No." FieldName="AgreementNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colClientName" Caption="Name" FieldName="ClientName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="2" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="Created By" FieldName="CreatedUser" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colCreatedDate" Caption="Created Date" FieldName="CreatedDate" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            
                                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="4" Caption=" " >
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

    <dx:ASPxPopupControl ID="apcWaive" ClientInstanceName="apcWaive" runat="server" Modal="True" Theme="Aqua" Width="750px" CloseAction="CloseButton"
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
                                        ID="gvWaiveApprovalList"
                                        ClientInstanceName="gvWaiveApprovalList"
                                        runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="False"
                                        EnableCallBacks="true"
                                        EnablePagingCallbackAnimation="true"
                                        EnableTheming="True" 
                                        OnDataBinding="gvWaiveApprovalList_DataBinding" OnCustomCallback="gvWaiveApprovalList_CustomCallback"
                                        Theme="Aqua" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DocKey">
                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                        <ClientSideEvents CustomButtonClick="function(s, e) { gvWaiveApprovalList_CustomButtonClick(s, e); }"/>
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
                                            <dx:GridViewDataTextColumn Name="colAgreementNo" Caption="Agreement No." FieldName="AgreementNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="Created By" FieldName="CreatedUser" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="2">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colCreatedDate" Caption="Created Date" FieldName="CreatedDate" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            
                                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="4" Caption=" " >
                                                <HeaderStyle Font-Bold="true" />
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnShowWaive" Text="Show"></dx:GridViewCommandColumnCustomButton>
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

    <dx:ASPxPopupControl ID="apcRelease" ClientInstanceName="apcRelease" runat="server" Modal="True" Theme="Aqua" Width="750px" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Your doc release list.." AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
        <SettingsAdaptivity Mode="OnWindowInnerWidth" MaxHeight="100%" MaxWidth="100%"/>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxFormLayout runat="server">
                    <Items>
                        <dx:LayoutItem ShowCaption="False" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridView 
                                        ID="gvReleaseList"
                                        ClientInstanceName="gvReleaseList"
                                        runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="False"
                                        EnableCallBacks="true"
                                        EnablePagingCallbackAnimation="true"
                                        EnableTheming="True" 
                                        OnDataBinding="gvReleaseList_DataBinding" OnCustomCallback="gvReleaseList_CustomCallback"
                                        Theme="Aqua" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DocKey">
                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                        <ClientSideEvents CustomButtonClick="function(s, e) { gvReleaseList_CustomButtonClick(s, e); }"/>
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
                                            <dx:GridViewDataTextColumn Name="colAgreementNo" Caption="Agreement No." FieldName="AgreementNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="Created By" FieldName="CreatedUser" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="2">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colCreatedDate" Caption="Created Date" FieldName="CreatedDate" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            
                                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="4" Caption=" " >
                                                <HeaderStyle Font-Bold="true" />
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnShowRelease" Text="Show"></dx:GridViewCommandColumnCustomButton>
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
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Release Document Listing" ColCount="7">
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
                    <dx:EmptyLayoutItem Width="35%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                <dx:ASPxButton ID="btnApprovalList" ClientInstanceName="btnApprovalList" runat="server" Text="Approval List" BackColor="LightGray" AutoPostBack="false" OnClick="btnApprovalList_Click" ToolTip="Click here to view approval list" Width="100%">
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                <dx:ASPxButton ID="btnWaiveList" ClientInstanceName="btnWaiveList" runat="server" Text="Waive List" BackColor="LightGray" AutoPostBack="false" OnClick="btnWaiveList_Click" ToolTip="Click here to view waive list" Width="100%">
                                    
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                <dx:ASPxButton ID="btnRelease" ClientInstanceName="btnRelease" runat="server" Text="Ready to Release" BackColor="LightGray" AutoPostBack="false" OnClick="btnRelease_Click" ToolTip="Click here to view release list">
                                    
                                </dx:ASPxButton>
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
                                    KeyFieldName="AgreementNo"
                                    Width="100%"
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnInit="gvMain_Init"
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
                                        <dx:GridViewDataTextColumn Name="colAgreementNo" Caption="Agreement No." FieldName="AgreementNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colName" Caption="Name" FieldName="ClientName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colReleaseDate" Caption="Release Date" FieldName="LastInsPaidDate" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataDateColumn Name="colLastPaidDate" Caption="Last Paid Date" FieldName="ReleaseDate" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colStatus" Caption="Status" FieldName="ReleaseStat" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colBiaya" Caption="Fee" FieldName="FeePenitipanDoc" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" PropertiesSpinEdit-DisplayFormatString="#,0.00">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataSpinEditColumn>
                                        <%--<dx:GridViewDataTextColumn Name="colWaiveReason" Caption="Waive Reason" FieldName="WaiveReason" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>--%>
                                        <dx:GridViewDataSpinEditColumn Name="colWaiveAmtReq" Caption="Waive Fee Request" FieldName="WaiveFeePenitipanDoc" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" PropertiesSpinEdit-DisplayFormatString="#,0.00">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colWaiveAmtApprove" Caption="Waive Approved Amount" FieldName="WaiveApproveFeeAmt" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" PropertiesSpinEdit-DisplayFormatString="#,0.00">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="Created By" FieldName="CreatedBy" ReadOnly="True" ShowInCustomizationForm="true" Visible="false">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="Created By" FieldName="CreatedByName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colCreatedDateTime" Caption="Created Date" FieldName="CreatedDate" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt">
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

    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
