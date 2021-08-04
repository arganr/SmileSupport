<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="UploadPaymentTextFile.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.UploadPaymentTextFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        function ShowAlert() {
            alert('Display Message Alert');
        }
        function ShowAlertSaveWindow() {
            lblmessage.SetValue("Apakah anda yakin ingin Simpan ?");
            cplMain.PerformCallback("SAVE");
            pcsave.Show();
        }

        var CustomErrorText = "* Value can't be empty.";
        function cplMain_EndCallback() {
            switch (cplMain.cpCallbackParam) {
                case "ACTION":
                    var varMainLayout = FormLayout1.GetItemByName("liPromiseToPay");
                    if (cplMain.cpVisible != null)
                        varMainLayout.SetVisible(cplMain.cpVisible);
                    break;
                case "SAVECONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;
                case "SAVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "UPLOAD_FINISH":
                    apcalert.SetContentHtml('Upload Success.');
                    apcalert.Show();
                    break;
            }
            cplMain.cpCallbackParam = null;
        }

        var command = "";
        function OnBeginCallback(s, e) {
            command = e.command;
        }
        function onFilesUploadComplete() {
            cplMain.PerformCallback("UPLOAD_FINISH; UPLOAD_FINISH");
        }
        function OnBtnUploadClick() {
            CtlUpload.Upload();
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
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert !" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Theme="PlasticBlue" Font-Names="Calibri" ColCount="3">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup GroupBoxDecoration="Box" Caption="Upload Payment Text File">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="#f8fafd"></Caption>
                </GroupBoxStyle>
                <SettingsItemCaptions Location="Top" />
                <Items>
                    <dx:LayoutItem Caption="Jasa Pembayaran" ShowCaption="True">
                        <CaptionStyle Font-Size="11"></CaptionStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" ID="cbPenyediaJasa" ClientInstanceName="cbPenyediaJasa" DropDownStyle="DropDownList" Height="23px">
                                    <Items>
                                        <dx:ListEditItem Text="AyoConnect" Value="AyoConnect" Selected="true" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxUploadControl ID="CtlUpload" runat="server" ClientInstanceName="CtlUpload" FileUploadMode="OnPageLoad" UploadMode="Advanced" OnFilesUploadComplete="CtlUpload_FilesUploadComplete" ShowProgressPanel="true" BrowseButton-Text="Browse" ShowTextBox="true" Height="23px">
                                    <AdvancedModeSettings EnableMultiSelect="true" EnableFileList="True" EnableDragAndDrop="True"></AdvancedModeSettings>
                                    <ValidationSettings AllowedFileExtensions=".txt" MaxFileSize="4000000" ErrorStyle-BackColor="Red" ShowErrors="true">
                                        <ErrorStyle BackColor="Red"></ErrorStyle>
                                    </ValidationSettings>
                                        <ClientSideEvents FilesUploadComplete="onFilesUploadComplete"/>
                                    <AdvancedModeSettings EnableDragAndDrop="True">
                                    </AdvancedModeSettings>
                                </dx:ASPxUploadControl>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxButton runat="server" ID="btnUpload" ClientInstanceName="btnUpload" Text="Upload" ClientEnabled="true" AutoPostBack="false">
                                        <ClientSideEvents Click="OnBtnUploadClick"/>
                                    </dx:ASPxButton>
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
