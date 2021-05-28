<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="MyWorkingListMaint.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.WorkingList.MyWorkingListMaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        var TicketHeaderID;
        function gvWorkingList_EndCallback(s, e) {
            switch (gvWorkingList.cpCallbackParam) {
                case "INDEX":
                    gvWorkingList.SetFocusedRowIndex(gvWorkingList.cpVisibleIndex);
                    break;
            }

            gvWorkingList.cplblmessageError = "";
            gvWorkingList.cpCallbackParam = "";
            scheduleGridUpdate(s);
        }
        function cplMain_EndCallback() {
            switch (cplMain.cpCallbackParam) {
            }

            cplMain.cpCallbackParam = "";
        }
        function btnPrint_Click() {
            if (gvWorkingList.GetFocusedRowIndex() < 0) {
                alert("Ticket belum dipilih !");
                return;
            }
            if (gvWorkingList.GetFocusedRowIndex() > -1) {
                var userName = "<%= Session["UserName"] %>";
                OpenWindow("../../Shared/DocViewer.aspx?ReportType=PO&Title=Purchase Order&DocKey=" + POHeaderID + "&ReportParam=" + userName, "DocViewer");
            }
        }
        function GetTicketHeaderID(values) {
            TicketHeaderID = values;
        }
        window.onload = function () {
            if (gvWorkingList.GetFocusedRowIndex() > -1) {
                gvWorkingList.GetRowValues(gvWorkingList.GetFocusedRowIndex(), 'Source', GetTicketHeaderID);
            }
        }
        function OnGetRowValues(Value) {
            alert(Value);
        }
        function gvWorkingList_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case "btnShow":
                    gvWorkingList.GetRowValues(e.visibleIndex, "DocKey;", ApprovalProcess);
                    break;
                case "btnAssign":
                    gvWorkingList.GetRowValues(e.visibleIndex, "DocKey;", btnAssignOnCLick);
                    break;
            }
        }
        function ApprovalProcess(values) {
            cplMain.PerformCallback("APPROVAL;" + values[0] + ";");
        }
        var timeout;
        function scheduleGridUpdate(grid)
        {
            //window.clearTimeout(timeout);
            //timeout = window.setTimeout(
            //    function ()
            //    {
            //        gvWorkingList.PerformCallback('REFRESH; REFRESH');
            //    },
            //    60000
            //);
        }
        function gvWorkingList_Init(s, e) {
            scheduleGridUpdate(s);
        }
        function gvWorkingList_BeginCallback(s, e) {
            window.clearTimeout(timeout);
        }
        function btnAssignOnCLick() {
            apcFormAssign.Show();
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
    <dx:ASPxPopupControl ID="apcFormAssign" ClientInstanceName="apcFormAssign" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="please select one PIC to assign this credit process" AllowDragging="True" PopupAnimationType="Fade"
        EnableCallbackAnimation="true" CloseAction="CloseButton"
        EnableViewState="False"
        Width="350px"
        Height="100px"
        FooterStyle-Wrap="False" ShowFooter="false" FooterText="" AllowResize="false">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" ColCount="2" Width="100%">
                    <Items>                       
                        <dx:LayoutItem ShowCaption="false" Caption="" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridLookup 
                                        runat="server" 
                                        ID="luPIC" 
                                        ClientInstanceName="luPIC"
                                        DisplayFormatString="{0} - {1}"
                                        TextFormatString="{0} - {1}"
                                        KeyFieldName="USER_NAME"
                                        SelectionMode="Single"
                                        OnDataBinding="luPIC_DataBinding"
                                        Width="100%" 
                                        HelpText="">
                                        <HelpTextSettings DisplayMode="Inline"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                            <GridViewProperties EnablePagingCallbackAnimation="true">
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                                <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                                <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                            </GridViewProperties>
                                        <Columns>
                                            <dx:GridViewDataColumn Caption="ID" FieldName="USER_ID" ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Name" FieldName="USER_NAME" ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <GridViewStyles AdaptiveDetailButtonWidth="22">
                                            </GridViewStyles>
                                    </dx:ASPxGridLookup>                                  
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="false" Caption="" Width="100%" ColSpan="2" HorizontalAlign="Right">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                    <dx:ASPxButton ID="btnSaveAssign" runat="server" Text="Save" AutoPostBack="False" UseSubmitBehavior="false" Width="50%">
                                        <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('ASSIGN'); apcFormAssign.Hide(); }" />
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="btnCancelAssign" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="50%">
                                        <ClientSideEvents Click="function(s, e) { apcFormAssign.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup GroupBoxDecoration="HeadingLine" Caption="My Working List" GroupBoxStyle-Caption-BackColor="Transparent">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvWorkingList"
                                    ClientInstanceName="gvWorkingList"
                                    runat="server"
                                    KeyFieldName="Source"
                                    Width="100%"
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnDataBinding="gvWorkingList_DataBinding"
                                    OnFocusedRowChanged="gvWorkingList_FocusedRowChanged"
                                    OnCustomCallback="gvWorkingList_CustomCallback"
                                    OnCustomUnboundColumnData="gvWorkingList_CustomUnboundColumnData"
                                    OnCustomButtonCallback="gvWorkingList_CustomButtonCallback"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                    </SettingsAdaptivity>
                                    <ClientSideEvents
                                        EndCallback="gvWorkingList_EndCallback"
                                        RowDblClick="function(s, e) {gvWorkingList.PerformCallback('DOUBLECLICK;' + e.visibleIndex);}"
                                        CustomButtonClick="function(s,e) { gvWorkingList_CustomButtonClick(s,e); }"
                                        BeginCallback="gvWorkingList_BeginCallback" />
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true"/>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true"/>
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
                                                <dx:GridViewToolbarItem Command="ShowCustomizationWindow" DisplayMode="ImageWithText"/>
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDocNo" Caption="Document No." FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" Width="10%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colDocDate" Caption="Document Date" FieldName="DocDate" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="true" VisibleIndex="2" Width="10%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colDocumentType" Caption="Document Type" FieldName="DocumentType" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="3">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataMemoColumn Name="colNote" Caption="Note" FieldName="Note" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="4">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colRemark1" Caption="Remark 1" FieldName="Remark1" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="5">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colRemark2" Caption="Remark 2" FieldName="Remark2" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="6">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colRemark3" Caption="Remark 3" FieldName="remark3" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="7">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colRemark4" Caption="Remark 4" FieldName="remark4" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="8">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataTextColumn Name="colBranch" Caption="Branch" FieldName="Branch" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="9">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colObjectPembiayaan" Caption="Binding Type" FieldName="ObjectPembiayaan" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="10">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colFacility" Caption="Facility" FieldName="Facility" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="11">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colJenisPengikitan" Caption="Jenis Pengikatan" FieldName="JenisPengikitan" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="12">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colPackage" Caption="Package" FieldName="Package" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="13">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCIF" Caption="CIF" FieldName="CIF" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="14">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colClientName" Caption="Client Name" FieldName="ClientName" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="15">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colSupplierName" Caption="Supplier Name" FieldName="SupplierName" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="16">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colSupplierBranch" Caption="Supplier Branch" FieldName="SupplierBranch" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="17">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colOTR" Caption="OTR" FieldName="OTR" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="false" VisibleIndex="18">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colNTF" Caption="NTF" FieldName="NTF" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="true" VisibleIndex="19">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colDP" Caption="DP" FieldName="DP" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="false" VisibleIndex="20">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colTenor" Caption="Tenor" FieldName="Tenor" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="false" VisibleIndex="21">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colEffRate" Caption="Eff.rate" FieldName="EffRate" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="false" VisibleIndex="22">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn Name="colStatus" Caption="Status" FieldName="Status" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="23">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="Created By" FieldName="FULLNAME" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="24">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colCreatedDateTime" Caption="Created Date" FieldName="CreatedDateTime" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="false" VisibleIndex="25">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colLastModifiedBy" Caption="Last Modified User" FieldName="LastModifiedBy" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="26">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colLastModifiedTime" Caption="Last Modified Time" FieldName="LastModifiedTime" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="false" VisibleIndex="27">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataCheckColumn Name="colSubmit" Caption="Submit ?" FieldName="Submit" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="28">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                            <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" ValueGrayed="N" ClientInstanceName="colCancelled" ValueType="System.Char">
                                            </PropertiesCheckEdit>
                                        </dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataTextColumn Name="colSubmitBy" Caption="Submit By" FieldName="SubmitBy" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="29">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colSubmitDateTime" Caption="Submit Date" FieldName="SubmitDateTime" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="false" VisibleIndex="30">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataCheckColumn Name="colCancelled" Caption="Cancel ?" FieldName="Cancelled" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="31">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                            <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" ValueGrayed="N" ClientInstanceName="colCancelled" ValueType="System.Char">
                                            </PropertiesCheckEdit>
                                        </dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataDateColumn Name="colCancelledDateTime" Caption="Cancel Date" FieldName="CancelledDateTime" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="false" VisibleIndex="32">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colCancelledType" Caption="Cancel Type" FieldName="CancelledType" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="33">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataMemoColumn Name="colCancelledNote" Caption="Cancel Note" FieldName="CancelledNote" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="34">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="35"  Caption="Show">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnShow" Text="Show">
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="36" Caption="Assign">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnAssign" Text="Assign to PIC">
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                    <Styles>
                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                    </Styles>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="NTF" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                    </TotalSummary>
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
