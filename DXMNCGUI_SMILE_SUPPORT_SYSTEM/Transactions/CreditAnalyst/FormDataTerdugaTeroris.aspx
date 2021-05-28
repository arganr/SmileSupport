<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FormDataTerdugaTeroris.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditAnalyst.FormDataTerdugaTeroris" %>
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
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
            <Items>
                <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Daftar Terduga Teroris" ColCount="4">
                    <GroupBoxStyle>
                        <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                    </GroupBoxStyle>
                    <Items>
                        <dx:LayoutItem ShowCaption="False" ColSpan="4" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxButton runat="server" ID="btnInquiry" ClientInstanceName="btnInquiry" Text="Inquiry" AutoPostBack="false" Width="130px" OnClick="btnInquiry_Click"></dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False" ColSpan="4" Width="100%" Height="500px">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridView  
                                        ID="gvMain" DataSourceID="sdsMain"
                                        ClientInstanceName="gvMain" 
                                        runat="server" 
                                        KeyFieldName="ID" 
                                        Width="100%"
                                        AutoGenerateColumns="False"
                                        EnableCallBacks="true"
                                        EnablePagingCallbackAnimation="true"
                                        EnableTheming="True" 
                                        Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                        </SettingsAdaptivity>
                                        <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" HorizontalScrollBarMode="Visible"/>
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" AllowEllipsisInText="true"/>
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False"/>
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                        <SettingsLoadingPanel Mode="Disabled"/>
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4"/>
                                        <SettingsPager Mode="ShowPager" PageSize="10"> </SettingsPager>
                                        <SettingsResizing ColumnResizeMode="Control" Visualization="Live"/>
                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to Xlsx" />
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Name="colID" Caption="ID" FieldName="ID" ReadOnly="true" Visible="false" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colTahun" Caption="Tahun" FieldName="TAHUN" ReadOnly="true" Visible="false" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colNama" Caption="Nama" FieldName="NAMA" ReadOnly="true" Visible="true" VisibleIndex="2" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colTempatLahir" Caption="Tempat Lahir" FieldName="TEMPAT_LAHIR" ReadOnly="true" Visible="true" VisibleIndex="3" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Name="colTanggalLahir" Caption="Tanggal Lahir" FieldName="TANGGAL_LAHIR" ReadOnly="true" VisibleIndex="4" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Width="300">
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn Name="colKewarganegaraan" Caption="Kewarganegaraan" FieldName="KEWARGANEGARAAN" ReadOnly="true" Visible="true" VisibleIndex="5" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlamat" Caption="Alamat" FieldName="ALAMAT" ReadOnly="true" Visible="true" VisibleIndex="6" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colFlag" Caption="Flag" FieldName="FLAG" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="7" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colKategori" Caption="Kategori" FieldName="KATEGORI" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="8" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colReferensi" Caption="Referensi" FieldName="REFERENSI" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="9" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataMemoColumn Name="colPekerjaan" Caption="Pekerjaan" FieldName="PEKERJAAN" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="10" Width="300">
                                            </dx:GridViewDataMemoColumn>
                                            <dx:GridViewDataTextColumn Name="colTelepon" Caption="Telepon" FieldName="TELEPON" ReadOnly="true" Visible="true" VisibleIndex="11" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colEmail" Caption="Email" FieldName="EMAIL" ReadOnly="true" Visible="true" VisibleIndex="12" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataMemoColumn Name="colKeterangan" Caption="Keterangan" FieldName="KETERANGAN" ReadOnly="true" Visible="true" VisibleIndex="13" Width="300">
                                            </dx:GridViewDataMemoColumn>
                                            <dx:GridViewDataTextColumn Name="colPassport" Caption="Passport" FieldName="NO_PASSPORT" ReadOnly="true" Visible="true" VisibleIndex="14" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colNoIdentitas" Caption="No Identitas" FieldName="NO_IDENTITAS" ReadOnly="true" Visible="true" VisibleIndex="15" Width="300">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias1" Caption="Alias 1" FieldName="ALIAS1" ReadOnly="true" ShowInCustomizationForm="false" Visible="true" VisibleIndex="16" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias2" Caption="Alias 2" FieldName="ALIAS2" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="17" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias3" Caption="Alias 3" FieldName="ALIAS3" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="18" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias4" Caption="Alias 4" FieldName="ALIAS4" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="19" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias5" Caption="Alias 5" FieldName="ALIAS5" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="20" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias6" Caption="Alias 6" FieldName="ALIAS6" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="21" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias7" Caption="Alias 7" FieldName="ALIAS7" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="22" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias8" Caption="Alias 8" FieldName="ALIAS8" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="23" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias9" Caption="Alias 9" FieldName="ALIAS9" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="24" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias10" Caption="Alias 10" FieldName="ALIAS10" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="25" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias11" Caption="Alias 11" FieldName="ALIAS11" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="26" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias12" Caption="Alias 12" FieldName="ALIAS12" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="27" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias13" Caption="Alias 13" FieldName="ALIAS13" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="28" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias14" Caption="Alias 14" FieldName="ALIAS14" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="29" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias15" Caption="Alias 15" FieldName="ALIAS15" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="30" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias16" Caption="Alias 16" FieldName="ALIAS16" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="31" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias17" Caption="Alias 17" FieldName="ALIAS17" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="32" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias18" Caption="Alias 18" FieldName="ALIAS18" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="33" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias19" Caption="Alias 19" FieldName="ALIAS19" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="34" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias20" Caption="Alias 20" FieldName="ALIAS20" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="35" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias21" Caption="Alias 21" FieldName="ALIAS21" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="36" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias22" Caption="Alias 22" FieldName="ALIAS22" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="37" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias23" Caption="Alias 23" FieldName="ALIAS23" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="38" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias24" Caption="Alias 24" FieldName="ALIAS24" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="39" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias25" Caption="Alias 25" FieldName="ALIAS25" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="40" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias26" Caption="Alias 26" FieldName="ALIAS26" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="41" Width="100">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAlias27" Caption="Alias 27" FieldName="ALIAS27" ReadOnly="true" ShowInCustomizationForm="true" Visible="true" VisibleIndex="42" Width="100">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Styles AdaptiveDetailButtonWidth="22">
                                        </Styles>
                                    </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
    </dx:ASPxFormLayout>
        <asp:SqlDataSource ID="sdsMain" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>"
        SelectCommand="SELECT * FROM MNCL_MS_DTTOT ORDER BY NAMA" SelectCommandType="Text">
    </asp:SqlDataSource>
</asp:Content>
