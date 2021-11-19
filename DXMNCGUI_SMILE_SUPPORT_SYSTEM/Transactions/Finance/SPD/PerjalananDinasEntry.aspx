<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PerjalananDinasEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.SPD.PerjalananDinasEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var command = "";
        var CustomErrorText = "* Value can't be empty.";
        function ShowAlert()
        {
            alert('Display Message Alert');
        }
        function ShowAlertSaveWindow()
        {
            lblmessage.SetValue("Apakah anda yakin ingin Simpan ?");
            cplMain.PerformCallback("SAVE");
            pcsave.Show();
        }
        function cplMain_EndCallback(s, e)
        {
            switch (cplMain.cpCallbackParam)
            {
                case "SAVE_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0)
                    {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
                    if (ASPxClientEdit.ValidateGroup("ValidationSave"))
                    {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);                      
                    }
                    break;
                case "SAVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "APPROVE_CONFIRM":
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
                case "APPROVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "REJECT_CONFIRM":
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
                case "REJECT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "CANCEL":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
            }
            cplMain.cpCallbackParam = null;
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True" Theme="MetropolisBlue" EnableCallbackAnimation="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="we need your confirmation.." AllowDragging="False" PopupAnimationType="Fade" EnableViewState="False" CloseAction="CloseButton">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2">
                    <Items>
                        <dx:LayoutItem Caption="" ColSpan="2" ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                    <dx:ASPxLabel ID="lblmessage" ClientInstanceName="lblmessage" runat="server" Text="">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                        <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                    <dx:ASPxButton ID="btnSaveConfirm" runat="server" Text="OK" AutoPostBack="False" UseSubmitBehavior="false" Width="100%">
                                        <%--<ClientSideEvents Click="function(s, e) { cplMain.PerformCallback(cplMain.cplblActionButton + ';'+cplMain.cplblActionButton); apcconfirm.Hide(); }" />--%>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                    <dx:ASPxButton ID="btnCancelConfirm" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="100%">
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
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" Theme="MetropolisBlue" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="None"
        EnableViewState="False" Width="400" Height="75">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" Theme="Glass">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup Name="LayoutGroupUpdateNoSPPHEntry" ShowCaption="True" Caption="Update No. SPPH Entry" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="NIK" Width="25%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtNIK" ClientInstanceName="txtNIK" NullText="..." Width="100%">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Name" Width="25%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtName" ClientInstanceName="txtName" NullText="..." Width="100%">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Dept" Width="25%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtDept" ClientInstanceName="txtDept" NullText="..." Width="100%">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>


                    <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnApprove" ClientInstanceName="btnApprove" Text="Approve" ForeColor="Green" AutoPostBack="false" Theme="Glass">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('APPROVE_CONFIRM;' + 'APPROVE_CONFIRM;' + + luAgreement.GetText()); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnReject" ClientInstanceName="btnReject" Text="Reject" ForeColor="Red" AutoPostBack="false" Theme="Glass">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('REJECT_CONFIRM;' + 'REJECT_CONFIRM;' + + luAgreement.GetText()); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Text="Save" AutoPostBack="false" Theme="Glass">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVE_CONFIRM;' + 'SAVE_CONFIRM;' + luAgreement.GetText()); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnBack" ClientInstanceName="btnBack" Text="Back" AutoPostBack="true" Theme="Glass" PostBackUrl="~/Transactions/Syariah/UpdateNoSPPH/UpdateNoSPPHList.aspx"></dx:ASPxButton>
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
