<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="MasterClientSLIKCheck.aspx.cs" Async="true" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.eKYC.SLIKChecking.MasterClientSLIKCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "LOAD":
                    gvTempData.Refresh();
                    gvDebSLIK.Refresh();
                    gvDetailSLIK.Refresh();
                    if (cplMain.cpEnableBtn == 'enable') {
                        btnProgress.SetVisible(true);
                    } else {
                        btnProgress.SetVisible(false);
                    }
                    break;
                case "DEB":
                    gvDebSLIK.Refresh();
                    gvDetailSLIK.Refresh();
                    break;
            }
            cplMain.cpCallbackParam = null;
        }
        function OnClientChanged(ccode) {
            var grid = gvClient.GetGridView();
            if (gvClient.GetText() != "") {
                cplMain.PerformCallback("LOAD;" + gvClient.GetValue().toString());
            }
        }
        function OnDebGridFocusedRowChanged() {
            
        }
        function OnTempGridFocusedRowChanged() {
            gvTempData.GetRowValues(gvTempData.GetFocusedRowIndex(), 'CLIENT;SID_PENGURUSID', OnTempGetRowValues);
        }
        function OnTempGetRowValues(values) {
            cplMain.PerformCallback("DEB;" + values[0].toString() + ";" + values[1].toString());
            
        }
    </script>
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
                                            <div style="text-align:center">
                                                <dx:ASPxLabel ID="lblmessage" ClientInstanceName="lblmessage" runat="server" Text="Are you sure ?" Width="100%">
                                                </dx:ASPxLabel>
                                            </div>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                            <dx:ASPxButton ID="btnRequestConfirm" ClientInstanceName="btnRequestConfirm" runat="server" Text="OK" AutoPostBack="true" OnClick="btnRequestConfirm_Click" Width="100" Theme="Office2010Black">
                                                <ClientSideEvents Click="function(s, e) { apcconfirm.Hide(); lp.Show(); } " />
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
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout" ClientInstanceName="ASPxFormLayout" runat="server" Theme="Glass" Width="100%">
        <Items>
            <dx:LayoutGroup Name="LayoutGroupClient" ShowCaption="True" Caption="Client SLIK Checking" GroupBoxDecoration="Box" ColCount="1" >
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Find Client">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup runat="server" ID="gvClient" ClientInstanceName="gvClient" KeyFieldName="CIF" OnDataBinding="gvClient_DataBinding" DisplayFormatString="{1}" TextFormatString="{1}" MultiTextSeparator=";" Width="30%">
                                    <ClientSideEvents ValueChanged="function(s, e) { OnClientChanged(s); }"/>
                                    <GridViewProperties EnablePagingCallbackAnimation="true">
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                    </GridViewProperties>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvTempData"
                                    runat="server"
                                    ClientInstanceName="gvTempData"
                                    OnDataBinding="gvTempData_DataBinding"
                                    Width="100%">
                                    <SettingsBehavior AllowDragDrop="false" AllowSort="false" AllowFocusedRow="true"/>
                                    <ClientSideEvents FocusedRowChanged="function(s, e) { OnTempGridFocusedRowChanged(); }"/>
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="CLIENT" Caption="CLIENT" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SID_PENGURUSID" Caption="SID_PENGURUSID" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="KTP" Caption="No. KTP" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NAME" Caption="Nama KTP" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="INBORNDT" Caption="Tgl Lahir" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="INBORNPLC" Caption="Tempat Lahir" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ADDRESS" Caption="Alamat" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NPWP" Caption="No. NPWP" Visible="false" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="INGENDER" Caption="Gender" Visible="false" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="IBUKANDUNG" Caption="Ibu Kandung" Visible="false" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CUSTTYPE" Caption="Cust Type" Visible="false" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TYPEDESC" Caption="Type" ></dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnProgress" ClientInstanceName="btnProgress" Height="30px" Text="Request SLIK Checking" AutoPostBack="false"  Theme="MaterialCompact" ClientVisible="false">
                                    <%--<ClientSideEvents Click="function(s, e) { lp.Show(); } " />--%>
                                    <ClientSideEvents Click="function(s, e) { apcconfirm.Show(); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel ID="lblListSLIK" runat="server" Text="List SLIK Request :" Font-Bold="true" Font-Names="Calibri" ForeColor="SlateGray"></dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvDebSLIK"
                                    runat="server"
                                    ClientInstanceName="gvDebSLIK"
                                    OnDataBinding="gvDebSLIK_DataBinding"
                                    Width="100%">
                                    <SettingsBehavior AllowDragDrop="false" AllowSort="false" AllowFocusedRow="true"/>
                                    <ClientSideEvents FocusedRowChanged="function(s, e) { OnDebGridFocusedRowChanged(); }"/>
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="REFID" Caption="No. Reference" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NAME" Caption="Nama KTP" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="KTP" Caption="No. KTP" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NPWP" Caption="No. NPWP" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DOB" Caption="Tgl Lahir" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="REQSTATUS" Caption="Last Req Status" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CRE_BY" Caption="Created By" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CRE_DATE" Caption="Created Date" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CUSTTYPE" Caption="CUSTTYPE" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CLIENT" Caption="CLIENT" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SID_PENGURUSID" Caption="SID_PENGURUSID" Visible="false"></dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnView" ClientInstanceName="btnView" Height="30px" Text="Check Request & View SLIK" AutoPostBack="true" OnClick="btnView_Click" Theme="MaterialCompact">
                                    <ClientSideEvents Click="function(s, e) { lp.Show(); } " />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel ID="lblHasilCheck" runat="server" Text="Hasil Pengecekan :" Font-Bold="true" Font-Names="Calibri" ForeColor="SlateGray"></dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvDetailSLIK"
                                    runat="server"
                                    ClientInstanceName="gvDetailSLIK"
                                    OnDataBinding="gvDetailSLIK_DataBinding"
                                    Width="100%">
                                    <Settings HorizontalScrollBarMode="Visible" />

                                    <SettingsBehavior AllowDragDrop="false" AllowSort="false" AllowFocusedRow="false"/>
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to .xlsx" ToolTip="Click here to export grid data to excel" />
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="LJK" Caption="LJK" Width="250px"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="JENIS" Caption="Jenis Kredit Pembiayaan" Width="250px" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="JENIS_PENGGUNAAN" Caption="Jenis Penggunaan" Width="250px" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PLAFON" Caption="Plafon Awal" >
                                            <PropertiesTextEdit DisplayFormatString="{0:#,0}" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="BAKIDEBET" Caption="Baki Debet" >
                                            <PropertiesTextEdit DisplayFormatString="{0:#,0}" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="BUNGA" Caption="Bunga" >
                                            <PropertiesTextEdit DisplayFormatString="{0}%" /> 
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="AKADAWAL" Caption="Akad Awal" >
                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TGLAWAL_SISATENOR" Caption="Awal Sisa Tenor" >
                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="JATUHTEMPO" Caption="Jatuh Tempo" >
                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RoundJangka" Caption="Jangka" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RoundSisaTenor" Caption="Sisa Tenor" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ANGSURAN" Caption="Angsuran" >
                                            <PropertiesTextEdit DisplayFormatString="{0:#,0}" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="KOLEKTIBILITAS" Caption="Kolektibilitas" Width="200px">
                                            <%--<CellStyle HorizontalAlign="Center"></CellStyle>--%>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="HISTORY_KOLEKTIBILITAS" Caption="History" Width="200px">
                                            <%--<CellStyle HorizontalAlign="Center"></CellStyle>--%>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FREKUENSI_TUNGGAKAN" Caption="Frek Tunggakan" ></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TUNGGAKAN_POKOK" Caption="T. Pokok" >
                                            <PropertiesTextEdit DisplayFormatString="{0:#,0}" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TUNGGAKAN_BUNGA" Caption="T. Bunga" >
                                            <PropertiesTextEdit DisplayFormatString="{0:#,0}" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DENDA" Caption="Denda" >
                                            <PropertiesTextEdit DisplayFormatString="{0:#,0}" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="KONDISI" Caption="Kondisi"  Width="250px"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="AGUNAN_LIST" Caption="List Agunan"  Width="250px"></dx:GridViewDataTextColumn>
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
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Modal="True" BackgroundImage-HorizontalPosition="center" Theme="Glass" Text="Loading Please Wait...">
        <BackgroundImage VerticalPosition="top" />
    </dx:ASPxLoadingPanel>
</asp:Content>
