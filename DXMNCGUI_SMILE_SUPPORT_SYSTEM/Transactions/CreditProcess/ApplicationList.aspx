<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ApplicationList.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.ApplicationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        var DocNo;
        function gvMain_EndCallback(s, e)
        {
            switch (gvMain.cpCallbackParam)
            {
                case "INDEX":
                    gvMain.SetFocusedRowIndex(gvMain.cpVisibleIndex);
                    break;
            }

            gvMain.cplblmessageError = "";
            gvMain.cpCallbackParam = "";
            scheduleGridUpdate(s);
        }
        function cplMain_EndCallback()
        {
            switch (cplMain.cpCallbackParam)
            {
                case "REFRESH":
                    gvMain.Refresh();
                    break;
            }
        }
        function GetDocNo(values) {
            DocNo = values;
        }
        window.onload = function () {
            if (gvMain.GetFocusedRowIndex() > -1) {
                gvMain.GetRowValues(gvMain.GetFocusedRowIndex(), 'DocNo', GetDocNo);
            }
        }
        function FocusedRowChanged(s) {
            if (gvMain.GetFocusedRowIndex() > -1) {
            }
        }
        function OnGetRowValues(Value) {
            alert(Value);
        }
        var timeout;
        function scheduleGridUpdate(grid)
        {
            //window.clearTimeout(timeout);
            //timeout = window.setTimeout(
            //    function ()
            //    { gvMain.PerformCallback('REFRESH; REFRESH'); },
            //    60000
            //);
        }
        function gvMain_Init(s, e) {
            scheduleGridUpdate(s);
        }
        function gvMain_BeginCallback(s, e) {
            window.clearTimeout(timeout);
        }
        var AppHeaderID;
        function GetHeaderID(values) {
            AppHeaderID = values;
        }
        window.onload = function () {
            if (gvMain.GetFocusedRowIndex() > -1) {
                gvMain.GetRowValues(gvMain.GetFocusedRowIndex(), 'DocNo', GetHeaderID);
            }
        }
        function FocusedRowChanged(s)
        {
            if (gvMain.GetFocusedRowIndex() > -1)
            {
                gvHistory.PerformCallback('LOAD_HISTORY;' + s.GetRowKey(s.GetFocusedRowIndex()));
                gvComment.PerformCallback('LOAD_COMMENT;' + s.GetRowKey(s.GetFocusedRowIndex()));
            }
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />
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
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Application List" ColCount="4">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxButton ID="btnNew" ClientInstanceName="btnNew" runat="server" Text="New" BackColor="LightGray" OnClick="btnNew_Click" ToolTip="Click here to create a new application" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxButton ID="btnView" ClientInstanceName="btnView" runat="server" Text="View" BackColor="LightGray" OnClick="btnView_Click" ToolTip="Click here to view application" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                <dx:ASPxButton ID="btnEdit" ClientInstanceName="btnEdit" runat="server" Text="Edit" BackColor="LightGray" OnClick="btnEdit_Click" ToolTip="Click here to edit application" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                <dx:ASPxButton ID="btnRefresh" ClientInstanceName="btnRefresh" runat="server" Text="Refresh" BackColor="LightGray" AutoPostBack="false" ToolTip="Click here to reload application list" Width="100%">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('REFRESH;' + 'REFRESH'); }"/>
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
                                    KeyFieldName="DocNo"
                                    Width="100%"
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnInit="gvMain_Init"
                                    OnDataBinding="gvMain_DataBinding"
                                    OnFocusedRowChanged="gvMain_FocusedRowChanged"
                                    OnCustomCallback="gvMain_CustomCallback"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri" EnableCallbackAnimation="true">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                    </SettingsAdaptivity>
                                    <ClientSideEvents
                                        EndCallback="gvMain_EndCallback"
                                        RowDblClick="function(s, e) {gvMain.PerformCallback('DOUBLECLICK;' +e.visibleIndex);}"
                                        FocusedRowChanged="FocusedRowChanged" Init="gvMain_Init" BeginCallback="gvMain_BeginCallback"/>
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
                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDocNo" Caption="Document No." FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" Width="10%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colDocDate" Caption="Document Date" FieldName="DocDate" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="true" VisibleIndex="2" Width="10%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colDocumentType" Caption="Document Type" FieldName="DocumentType" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="3">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataMemoColumn Name="colNote" Caption="Note" FieldName="Note" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="4">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colRemark1" Caption="Remark 1" FieldName="Remark1" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="5">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colRemark2" Caption="Remark 2" FieldName="Remark2" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="6">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colRemark3" Caption="Remark 3" FieldName="remark3" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="7">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colRemark4" Caption="Remark 4" FieldName="remark4" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="8">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataTextColumn Name="colBranch" Caption="Branch" FieldName="Branch" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="9">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colObjectPembiayaan" Caption="Lease Object" FieldName="ObjectPembiayaan" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="10">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colFacility" Caption="Facility" FieldName="Facility" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="11">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colJenisPengikitan" Caption="Binding Type" FieldName="JenisPengikatan" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="12">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colPackage" Caption="Package" FieldName="Package" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="13">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCIF" Caption="CIF" FieldName="CIF" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="14">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colClientName" Caption="Client Name" FieldName="ClientName" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="15">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colSupplierName" Caption="Supplier Name" FieldName="SupplierName" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="16">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colSupplierBranch" Caption="Supplier Branch" FieldName="SupplierBranch" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="17">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colOTR" Caption="OTR" FieldName="OTR" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="false" VisibleIndex="18">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colNTF" Caption="NTF" FieldName="NTF" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="true" VisibleIndex="19">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colDP" Caption="DP" FieldName="DP" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="false" VisibleIndex="20">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colTenor" Caption="Tenor" FieldName="Tenor" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="false" VisibleIndex="21">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colEffRate" Caption="Eff.rate" FieldName="EffRate" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="false" VisibleIndex="22">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn Name="colStatus" Caption="Status" FieldName="Status" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="23">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="Created By" FieldName="FULLNAME" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="24">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colCreatedDateTime" Caption="Created Date" FieldName="CreatedDateTime" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" Visible="false" VisibleIndex="25">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colLastModifiedBy" Caption="Last Modified User" FieldName="LastModifiedBy" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="26">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colLastModifiedTime" Caption="Last Modified Time" FieldName="LastModifiedTime" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" Visible="false" VisibleIndex="27">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataCheckColumn Name="colSubmit" Caption="Submit ?" FieldName="Submit" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="28">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                            <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" ValueGrayed="N" ClientInstanceName="colCancelled" ValueType="System.Char">
                                            </PropertiesCheckEdit>
                                        </dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataTextColumn Name="colSubmitBy" Caption="Submit By" FieldName="SubmitBy" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="29">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colSubmitDateTime" Caption="Submit Date" FieldName="SubmitDateTime" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" Visible="false" VisibleIndex="30">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataCheckColumn Name="colCancelled" Caption="Cancel ?" FieldName="Cancelled" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="31">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                            <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" ValueGrayed="N" ClientInstanceName="colCancelled" ValueType="System.Char">
                                            </PropertiesCheckEdit>
                                        </dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataDateColumn Name="colCancelledDateTime" Caption="Cancel Date" FieldName="CancelledDateTime" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" Visible="false" VisibleIndex="32">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colCancelledType" Caption="Cancel Type" FieldName="CancelledType" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="33">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataMemoColumn Name="colCancelledNote" Caption="Cancel Note" FieldName="CancelledNote" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="34">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataTextColumn Name="colOnHold" Caption="Hold?" FieldName="OnHold" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" >
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colUnit" Caption="Unit" FieldName="UNIT" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" >
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Styles>
                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                    </Styles>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="NTF" SummaryType="Sum" DisplayFormat="'Rp '#,0.00" />
                                    </TotalSummary>
                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                                    <SettingsDetail ShowDetailRow="false" />
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
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
                                <dx:ASPxGridView ID="gvHistory" runat="server" ClientInstanceName="gvHistory" KeyFieldName="DtlKey"
                                    EnableTheming="True"
                                    Theme="Glass"
                                    OnDataBinding="gvHistory_DataBinding"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    EnableCallbackAnimation="true"
                                    OnCustomCallback="gvHistory_CustomCallback"
                                    OnCustomCellMerge="gvHistory_CustomCellMerge"
                                    EndCallback="gvHistory_EndCallback" Font-Size="8" Font-Names="Calibri">
                                    <Settings ShowFooter="true" />
                                    <SettingsBehavior AllowCellMerge="true" AllowFocusedRow="true" AllowSelectByRowClick="True" EnableRowHotTrack="true" AllowSort="false" />
                                    <SettingsLoadingPanel Mode="ShowOnStatusBar"/>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Application No" FieldName="DocNo" Name="colDocNo" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0" Width="10%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Action By" FieldName="TransBy" Name="colTransBy" ShowInCustomizationForm="True" VisibleIndex="1" Width="20%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Caption="Action Date" FieldName="TransDate" Name="colTransDate" ShowInCustomizationForm="True" VisibleIndex="2" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" Width="55%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Caption="From" FieldName="FromStatus" Name="colFromStatus" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3" Visible="true" Width="10%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Move to" FieldName="Status" Name="colStatus" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Visible="true" Width="10%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Caption="Different time" FieldName="DiffTime" Name="colDiffTime" ShowInCustomizationForm="True" VisibleIndex="5" Width="5%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                    </Columns>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="DiffTime" SummaryType="Sum" DisplayFormat="#,# 'Minutes'" />
                                    </TotalSummary>
                                    <SettingsPager PageSize="30"></SettingsPager>
                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel ID="lblComment" runat="server" Text="Comment :" Font-Bold="true" Font-Names="Calibri" ForeColor="SlateGray"></dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView 
                                    runat="server" 
                                    ID="gvComment" 
                                    ClientInstanceName="gvComment" 
                                    Width="100%"
                                    AutoGenerateColumns="False" 
                                    EnableTheming="true" 
                                    Theme="Glass" OnDataBinding="gvComment_DataBinding"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true" 
                                    EnableCallbackAnimation="true" 
                                    OnCustomCallback="gvComment_CustomCallback" 
                                    EndCallback="gvComment_EndCallBack"
                                    Font-Size="8" Font-Names="Calibri">
                                    <Settings ShowFooter="true"/>
                                    <SettingsBehavior AllowFocusedRow="false" EnableRowHotTrack="true" AllowSort="false"/>
                                    <SettingsLoadingPanel Mode="ShowOnStatusBar"/>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Comment By" FieldName="CommentBy" Name="colCommentBy" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="20%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Caption="Comment Date" FieldName="CommentDate" Name="colCommentDate" ReadOnly="True" ShowInCustomizationForm="True" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" VisibleIndex="1" Width="15%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataMemoColumn Caption="Comment" FieldName="CommentNote" Name="colCommentNote" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="65%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataMemoColumn>
                                    </Columns>
                                    <SettingsPager PageSize="30"></SettingsPager>
                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="false"></Styles>
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
