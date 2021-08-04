<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" Async="true" AutoEventWireup="true" CodeBehind="MasterClient_eKYC.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.eKYC.MasterClientEKYC.MasterClient_eKYC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function disableF5(e) { if ((e.which || e.keyCode) == 116) e.preventDefault(); };
        // To disable f5
        /* jQuery < 1.7 */
        $(document).bind("keydown", disableF5);
        /* OR jQuery >= 1.7 */
        $(document).on("keydown", disableF5);

        // To re-enable f5
        /* jQuery < 1.7 */
        $(document).unbind("keydown", disableF5);
        /* OR jQuery >= 1.7 */
        $(document).off("keydown", disableF5);

        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "ACTION":
                    break;
                case "LOAD":
                    gvTempData.Refresh();
                    gvLogData.Refresh();
                    if (cplMain.cpEnableBtn == 'enable') {
                        btnSave.SetVisible(true);
                    } else {
                        btnSave.SetVisible(false);
                    }
                    break;
                case "UPLOAD":
                    break;
                case "SAVE":
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
        function OnGridFocusedRowChanged() {
            gvTempData.GetRowValues(gvTempData.GetFocusedRowIndex(), 'CLIENT;SID_PENGURUSID;KTP;', OnGetRowValues);
        }
        function OnGetRowValues(values) {
            if (values[0] != null) {
                HiddenField.Set("CLIENTID", values[0].toString());
                HiddenField.Set("SID_PENGURUSID", values[1].toString());
                HiddenField.Set("KTP", values[2].toString());
            }
        }
        function onBeginCallback(s, e) {
            s.SetFocusedRowIndex(-1);
        }
        function onUploadControlFileUploadComplete(s, e) {
            gvTempData.Refresh();
            var hdf = HiddenField.Get("CLIENTID");
            cplMain.PerformCallback("LOAD;" + hdf);
        }
    </script>

    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout" ClientInstanceName="ASPxFormLayout" runat="server" Theme="Glass" Width="100%">
        <Items>
            <dx:LayoutGroup Name="LayoutGroupClient" ShowCaption="True" Caption="Master Client e-KYC" GroupBoxDecoration="Box" ColCount="1" >
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Find Client">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup runat="server" ID="gvClient" ClientInstanceName="gvClient" KeyFieldName="CIF" OnDataBinding="gvClient_DataBinding" DisplayFormatString="{1}" TextFormatString="{0}" MultiTextSeparator=";" Width="30%">
                                    <ClientSideEvents ValueChanged="function(s, e) { OnClientChanged(s); }"/>
                                    <GridViewProperties EnablePagingCallbackAnimation="true">
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                    </GridViewProperties>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
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
                                    OnHtmlDataCellPrepared="gvTempData_HtmlDataCellPrepared"
                                    OnCustomButtonCallback="gvMain_CustomButtonCallback"
                                    OnCustomButtonInitialize="gvTempData_CustomButtonInitialize"
                                    Width="100%">
                                    <SettingsBehavior AllowDragDrop="false" AllowSort="false" AllowFocusedRow="true"/>
                                    <ClientSideEvents BeginCallback="onBeginCallback" FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }"/>
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="KTP" Caption="No. KTP" >               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NAME" Caption="Nama KTP">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EKYC_NAME" Caption="Hasil" Visible="false">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="INBORNDT" Caption="Tgl Lahir">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EKYC_INBORNDT" Caption="Hasil" Visible="false">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="INBORNPLC" Caption="Tempat Lahir">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EKYC_INBORNPLC" Caption="Hasil" Visible="false">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ADDRESS" Caption="Alamat">                  
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EKYC_ADDRESS" Caption="Hasil" Visible="false">   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CLIENT" Visible="false">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SID_PENGURUSID" Visible="false">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ID_UPLOAD" Visible="false">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ISCOMPLETED" Visible="false">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UPLOAD_KTP" Caption=" "> 
                                              <DataItemTemplate>
                                                <dx:ASPxUploadControl runat="server" ID="UploadCtrl" ClientInstanceName="UploadCtrl" ShowProgressPanel="true" UploadMode="Auto" AutoStartUpload="true" FileUploadMode="OnPageLoad"
                                                    OnFileUploadComplete="UploadCtrl_FileUploadComplete" Width="100%">
                                                    <ValidationSettings MaxFileSize="10000000" ErrorStyle-BackColor="Red" ShowErrors="true" AllowedFileExtensions=".pdf,.jpg,.jpeg,.img,.tiff,.xls,.xlsx,.txt,.doc,.docx,.rar,.zip">
                                                        <ErrorStyle BackColor="Red"></ErrorStyle>
                                                    </ValidationSettings>
                                                     <ClientSideEvents FileUploadComplete="onUploadControlFileUploadComplete"/>
                                                </dx:ASPxUploadControl>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn ButtonType="Link"  Caption=" " Name="btnFile">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="GridbtnDownload" Text=" ">
                                                    <Image Height="20px" Width="20px" Url="../../../Content/Images/download.png" ToolTip="Click here to download file."></Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Height="30px" Text="eKYC Checking" AutoPostBack="true" OnClick="btnSave_onClick" Theme="MaterialCompact" ClientVisible="false">
                                    <%--<ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVE;' + 'CLICK'); }" />--%>
                                    <ClientSideEvents Click="function(s, e) { lp.Show(); } " />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem ></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Histori Hasil Pengecekan">
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvLogData"
                                    runat="server"
                                    ClientInstanceName="gvLogData"
                                    OnDataBinding="gvLogData_DataBinding"
                                    OnHtmlDataCellPrepared="gvLogData_HtmlDataCellPrepared"
                                    Width="100%">
                                    <SettingsBehavior AllowDragDrop="false" AllowSort="false" AllowFocusedRow="false"/>
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="KTP" Caption="No. KTP" >               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NAME" Caption="Nama KTP">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EKYC_NAME" Caption="Hasil" >               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="INBORNDT" Caption="Tgl Lahir">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EKYC_INBORNDT" Caption="Hasil">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="INBORNPLC" Caption="Tempat Lahir">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EKYC_INBORNPLC" Caption="Hasil">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ADDRESS" Caption="Alamat">                  
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EKYC_ADDRESS" Caption="Hasil">   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Audit User" Caption="Audit User">   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Audit Date" Caption="Audit Date">   
                                        </dx:GridViewDataTextColumn>
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
