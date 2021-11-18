<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="SLAPerformancePerAction.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.SLAPerformancePerAction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        function cplMain_EndCallback()
        {
            switch (cplMain.cpCallbackParam)
            {
                case "INQUIRY":
                    gvMain.PerformCallback();
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
            <dx:LayoutGroup ShowCaption="True" Caption="Service Level Agreement Performance Per Action" GroupBoxDecoration="HeadingLine" ColCount="1">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="Transparent" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutGroup Caption="Filter Setting" ColCount="1">
                        <GroupBoxStyle Caption-BackColor="#f8fafd"></GroupBoxStyle>
                            <Items>
                                <dx:LayoutItem ShowCaption="False" Caption="" Width="20%">
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
                                <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>
                                <dx:LayoutItem ShowCaption="False" Caption="" Width="20%">
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
                                <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>
                                <dx:LayoutItem ShowCaption="False" Caption="" Width="20%" HorizontalAlign="Right">
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
                                    AutoGenerateColumns="True"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnInit="gvMain_Init"
                                    OnDataBinding="gvMain_DataBinding"
                                    OnFocusedRowChanged="gvMain_FocusedRowChanged"
                                    OnCustomCallback="gvMain_CustomCallback" 
                                    OnCustomUnboundColumnData="gvMain_CustomUnboundColumnData"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                    <ClientSideEvents />
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" HorizontalScrollBarMode="Visible"/>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" AllowSort="false"/>
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
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="DocNo" Name="colDocNo" Caption="Application No." Width="20%" VisibleIndex="0">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ClientName" Name="colClientName" Caption="Client Name" Width="20%" VisibleIndex="1">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TipeDebitur" Name="colTipeDebitur" Caption="Client Type" Width="20%" VisibleIndex="2">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Branch" Name="colBranch" Caption="Branch" Width="20%" VisibleIndex="3">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ObjectPembiayaan" Name="colObjectPembiayaan" Caption="Lease Object" Width="20%" VisibleIndex="4">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="JenisPengikatan" Name="colJenisPengikatan" Caption="Binding Type" Width="20%" VisibleIndex="5">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="NTF" Name="colNTF" Caption="NTF" PropertiesSpinEdit-DisplayFormatString="#,0.00" Width="120" VisibleIndex="6">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn FieldName="SupplierName" Name="colSupplierName" Caption="Supplier" Width="20%" VisibleIndex="7">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="StatusFrom" Name="colFromStatus" Caption="From" Width="20%" VisibleIndex="8">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <SettingsHeaderFilter Mode="CheckedList"></SettingsHeaderFilter>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="StatusTo" Name="colToStatus" Caption="To" Width="20%" VisibleIndex="9">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <SettingsHeaderFilter Mode="CheckedList"></SettingsHeaderFilter>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TransBy" Name="colTransBy" Caption="Action By" Width="20%" VisibleIndex="10">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn FieldName="TransDate" Name="colTransDate" Caption="Date Time" Width="20%" VisibleIndex="11" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="DiffTime" Name="colDiffTime" Caption="Working Time In Minutes" PropertiesSpinEdit-DisplayFormatString="#,0" Width="10%" VisibleIndex="12">
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Names="Calibri"/>
                                            <CellStyle Font-Bold="true"></CellStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                    </Columns>
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
