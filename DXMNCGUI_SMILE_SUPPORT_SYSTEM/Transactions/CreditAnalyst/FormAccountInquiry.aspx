<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FormAccountInquiry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditAnalyst.FormAccountInquiry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        var SID;
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
        }      
        function GetSID(values)
        {
            SID = values;
        }
        window.onload = function ()
        {
            if (gvMain.GetFocusedRowIndex() > -1)
            {
                gvMain.GetRowValues(gvMain.GetFocusedRowIndex(), 'SID', GetSID);
            }
        }
        function FocusedRowChanged(s)
        {
            if (gvMain.GetFocusedRowIndex() > -1)
            {
                //gvIncidentListDetail.PerformCallback('DetailLoad;' + s.GetRowKey(s.GetFocusedRowIndex()));
            }
        }
        function OnGetRowValues(Value)
        {
            alert(Value);
        }
        var timeout;
        function scheduleGridUpdate(grid) {
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
                <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Account Inquiry">
                    <GroupBoxStyle>
                        <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                    </GroupBoxStyle>
                    <Items>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxButton runat="server" ID="btnInquiry" ClientInstanceName="btnInquiry" Text="Inquiry" AutoPostBack="false" Width="130px" OnClick="btnInquiry_Click"></dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridView  
                                        ID="gvMain" 
                                        ClientInstanceName="gvMain" 
                                        runat="server" 
                                        KeyFieldName="CLIENT" 
                                        Width="100%" 
                                        AutoGenerateColumns="False"
                                        EnableCallBacks="true"
                                        EnablePagingCallbackAnimation="true"
                                        OnDataBinding="gvMain_DataBinding" 
                                        OnFocusedRowChanged="gvMain_FocusedRowChanged"  
                                        OnCustomCallback="gvMain_CustomCallback" 
                                        OnCustomUnboundColumnData="gvMain_CustomUnboundColumnData" 
                                        EnableTheming="True" 
                                        Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                        </SettingsAdaptivity>
                                        <ClientSideEvents 
                                            EndCallback="gvMain_EndCallback" 
                                            RowDblClick="function(s, e) {gvMain.PerformCallback('DOUBLECLICK;' +e.visibleIndex);}"
                                            FocusedRowChanged="FocusedRowChanged" Init="gvMain_Init" BeginCallback="gvMain_BeginCallback"
                                            />
                                        <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true"/>
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true"/>
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False"/>
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                        <SettingsLoadingPanel Mode="Disabled"/>
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4"/>
                                        <SettingsPager PageSize="18"></SettingsPager>
                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Expor Master to Xlsx" />
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>
                                        <Columns>
                                                <dx:GridViewDataTextColumn Name="colSID" Caption="SID" FieldName="CLIENT" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="0" Width="15%">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Name" FieldName="NAME" ReadOnly="True" ShowInCustomizationForm="true" VisibleIndex="1" Width="25%">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Address" FieldName="ADDRESS" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="2" Width="45%">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn Caption="Tgl Lahir / Pendirian" FieldName="INBORNDT" ReadOnly="True" ShowInCustomizationForm="true" VisibleIndex="3" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Width="15%">
                                                </dx:GridViewDataDateColumn>
                                        </Columns>
                                            <Templates>
                                                <DetailRow>
                                                    <dx:ASPxGridView ID="detailGrid" runat="server" KeyFieldName="CLIENT" DataSourceID="sdsDetail" Theme="MetropolisBlue" Font-Size="9" Font-Names="Calibri"
                                                        Width="100%" EnablePagingGestures="False" OnBeforePerformDataSelect="detailGrid_BeforePerformDataSelect" OnCustomUnboundColumnData="detailGrid_CustomUnboundColumnData" AutoGenerateColumns="False">
                                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Name="colBranch" Caption="Branch" FieldName="C_NAME" VisibleIndex="0"/>
                                                            <dx:GridViewDataDateColumn Name="colAPPLICDT" Caption="Application Date" FieldName="APPLICDT" VisibleIndex="1" />
                                                            <dx:GridViewDataTextColumn Name="colStatus" Caption="Status" FieldName="STATUS" VisibleIndex="2" />
                                                            <dx:GridViewDataTextColumn Name="colAPPLICNO" Caption="Application No" FieldName="APPLICNO" VisibleIndex="3" />
                                                            <dx:GridViewDataTextColumn Name="colLSAGREE" Caption="Contract No" FieldName="LSAGREE" VisibleIndex="4"/>
                                                            <dx:GridViewDataTextColumn Name="colDPLEASE" Caption="DP" FieldName="DPLEASE" VisibleIndex="5" UnboundType="Decimal" PropertiesTextEdit-DisplayFormatString="#,0.00"/>
                                                            <dx:GridViewDataTextColumn Name="colAMTLEASE" Caption="NTF" FieldName="AMTLEASE" VisibleIndex="6" UnboundType="Decimal" PropertiesTextEdit-DisplayFormatString="#,0.00"/>
                                                            <dx:GridViewDataTextColumn Name="colLSPERIOD" Caption="Tenor" FieldName="LSPERIOD" VisibleIndex="7" UnboundType="Decimal" PropertiesTextEdit-DisplayFormatString="#,0.00"/>
                                                            <dx:GridViewDataTextColumn Name="colOutPrincipal" Caption="OS Principal" FieldName="OUTSTANDING_PRINCIPAL" VisibleIndex="8" UnboundType="Decimal" PropertiesTextEdit-DisplayFormatString="#,0.00"/>
                                                            <dx:GridViewDataTextColumn Name="colRental" Caption="Installment" FieldName="RENTAL" VisibleIndex="9" UnboundType="Decimal" PropertiesTextEdit-DisplayFormatString="#,0.00"/>
                                                            <dx:GridViewDataTextColumn Name="colEffRate" Caption="Eff Rate %" FieldName="EFFRATE" VisibleIndex="10" UnboundType="Decimal" PropertiesTextEdit-DisplayFormatString="#,0.00"/>
                                                            <dx:GridViewDataTextColumn Name="colCampaignDesc" Caption="Campaign Description" FieldName="CAMPAIGN_DESC" VisibleIndex="11"/>
                                                        </Columns>
                                                        <TotalSummary>
                                                            <dx:ASPxSummaryItem FieldName="AMTLEASE" SummaryType="Sum" DisplayFormat="#,0.00"/>
                                                        </TotalSummary>
                                                        <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true"/>
                                                        <SettingsPager EnableAdaptivity="true" />
                                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4"/>
                                                        <SettingsBehavior FilterRowMode="OnClick" EnableRowHotTrack="true"/>
                                                        <Styles Header-Wrap="True" Cell-Wrap="True"/>
                                                        <Toolbars>
                                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                                <Items>
                                                                    <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export Detail to Xlsx" />
                                                                </Items>
                                                            </dx:GridViewToolbar>
                                                        </Toolbars>                                                        
                                                    </dx:ASPxGridView>
                                                </DetailRow>
                                            </Templates>
                                        <Styles AdaptiveDetailButtonWidth="22"></Styles>
                                        <SettingsDetail ShowDetailRow="true" />
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
    <asp:SqlDataSource ID="sdsDetail" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>"
        SelectCommand="SELECT *
FROM (

select *
from (
select C_NAME, APPLICDT, APPLICNO, '' LSAGREE, NAME, CAMPAIGN_DESC, DPLEASE, AMTLEASE, LSPERIOD, CAST(EFFRATE as NUMERIC(8,2)) EFFRATE, RENTAL, APPSTATUS STATUS, 0 OUTSTANDING_PRINCIPAL, LESSEE AS CLIENT
from LS_APPLICATION a
inner join SYS_COMPANY b on a.C_CODE = b.C_CODE
where LESSEE = @LESSEE
) as x
where x.APPLICNO not in ( select applicno from LS_AGREEMENT )

UNION

select C_NAME, APPLICDT, APPLICNO, LSAGREE, NAME, CAMPAIGN_DESC, DPLEASE, AMTLEASE, LSPERIOD, EFFRATE, RENTAL, CONTRACT_STATUS STATUS, OUTSTANDING_PRINCIPAL, LESSEE AS CLIENT
from LS_AGREEMENT a
inner join SYS_COMPANY b on a.C_CODE = b.C_CODE
where LESSEE = @LESSEE

) as y
ORDER BY y.STATUS" SelectCommandType="Text">
        <SelectParameters>
            <asp:SessionParameter SessionField="CLIENT" DefaultValue="" Name="LESSEE" Type="String"></asp:SessionParameter>
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
