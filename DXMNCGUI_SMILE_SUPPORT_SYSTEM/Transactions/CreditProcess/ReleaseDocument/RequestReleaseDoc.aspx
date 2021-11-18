<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="RequestReleaseDoc.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.ReleaseDocument.RequestReleaseDoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "dafault":
                    break;
                case "SAVE_CONFIRM":
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
        
        function OnluKontrakChanged(luAgreement) {
            var grid = luAgreement.GetGridView();
            if (luAgreement.GetText() != "") {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'LASTPAID;FEE;NAME;DaysDiff;', OnGetSelectedFieldValues);
            }
        }

        function OnGetSelectedFieldValues(selectedValues) {
            deLastPaidDate.SetValue(selectedValues[0]);
            txtFeePenitipan.SetValue(selectedValues[1]);
            txtName.SetValue(selectedValues[2]);
            txtDaysDiff.SetValue(selectedValues[3]);
        }

        function OnWaiveChanged(cbWaiveReason) {
            var waiveReason = cbWaiveReason.GetValue().toString();
            if (waiveReason == "Not Waive") {
                txtWaiveFeePenitipan.SetValue(0);
                txtWaiveFeePenitipan.SetEnabled(false);
            } else {
                txtWaiveFeePenitipan.SetEnabled(true);
            }
        }

        function onUploadControlFileUploadComplete(s, e) {
            UploadCtrl.SetVisible(false);
            btnDownload.SetVisible(true);
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
                                        <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback(cplMain.cplblActionButton + ';'+cplMain.cplblActionButton); apcconfirm.Hide(); }" />
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
    <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" ClientInstanceName="ASPxFormLayout" Theme="Glass">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup Name="LayoutGroupUpdateNoSPPHEntry" ShowCaption="True" Caption="Request Release Document" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Agreement No." Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup
                                        runat="server"
                                        ID="luAgreement"
                                        ClientInstanceName="luAgreement" 
                                        OnDataBinding="luAgreement_DataBinding"
                                        AutoGenerateColumns="False"
                                        DisplayFormatString="{1}"
                                        TextFormatString="{1}"
                                        KeyFieldName="LSAGREE"
                                        SelectionMode="Single"
                                        AnimationType="Fade" NullText="-- Select --" HelpText="Please select agreement number.">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        <ClientSideEvents Init="function(s, e) { OnluKontrakChanged(s); }" ValueChanged="function(s, e) { OnluKontrakChanged(s); }"/>
                                        <GridViewProperties EnablePagingCallbackAnimation="true">
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                            <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                            <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                        </GridViewProperties>
                                        <Columns>
                                            <dx:GridViewDataColumn Caption="Branch" FieldName="C_NAME" ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="No. Kontrak" FieldName="LSAGREE" ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Debitur" FieldName="NAME" ShowInCustomizationForm="True" VisibleIndex="2">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Tenor" FieldName="TENOR" ShowInCustomizationForm="True" VisibleIndex="3">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Installment" FieldName="INSTALLMENT" ShowInCustomizationForm="True" VisibleIndex="4">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Last Paid Date" FieldName="LASTPAID" ShowInCustomizationForm="True" VisibleIndex="5">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Fee" FieldName="FEE" ShowInCustomizationForm="True" VisibleIndex="6" Visible="false">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Days" FieldName="DaysDiff" ShowInCustomizationForm="True" VisibleIndex="7" Visible="false">
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <GridViewStyles AdaptiveDetailButtonWidth="22">
                                        </GridViewStyles>
                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                            <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Name" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtName" ClientInstanceName="txtName" ClientEnabled ="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Status Release" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtStatus" ClientInstanceName="txtStatus" ClientEnabled ="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Release Date" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deReleaseDate" ClientInstanceName="deReleaseDate" Width="100%" ClientEnabled="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Last Installment Paid Date" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deLastPaidDate" ClientInstanceName="deLastPaidDate" Width="100%" ClientEnabled="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Lama Penitipan (Days)" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDaysDiff" ClientInstanceName="txtDaysDiff" ClientEnabled ="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Biaya Penitipan Dokumen" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtFeePenitipan" runat="server" ClientInstanceName="txtFeePenitipan" Number="0" DisplayFormatString="{0:#,0}" Width="100%" ClientEnabled="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Waive Reason" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cbWaiveReason" runat="server" ClientInstanceName="cbWaiveReason" Width="100%">
                                    <Items>
                                        <dx:ListEditItem Text="Not Waive" Value="Not Waive" Selected="true"/>
                                        <dx:ListEditItem Text="Request Customer" Value="Request Customer" />
                                    </Items>
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) { OnWaiveChanged(s); }" />
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Waive Biaya Penitipan Dokumen" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtWaiveFeePenitipan" runat="server" ClientInstanceName="txtWaiveFeePenitipan" Number="0" DisplayFormatString="{0:#,0}" Width="100%" ClientEnabled="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <%--<dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <label style="font-size:10px"><i>* Fee penitipan dokumen adalah 15.000 per hari dari last paid date.<br />* Berlaku untuk penitipan lebih dari 30 hari.</i></label>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>--%>
                </Items>
            </dx:LayoutGroup>

            <dx:LayoutGroup Name="LayoutGroupApproval" ShowCaption="True" Caption="Approval Release Document" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Note" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="txtApprovalNote" ClientInstanceName="txtApprovalNote" NullText="">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutGroup Name="LayoutGroupApprovalDetail" ShowCaption="False" GroupBoxDecoration="None" ColCount="1" Width="100%">
                        <Items>
                            <dx:LayoutItem ShowCaption="true" Caption="Approve By" Width="30%">
                                <CaptionSettings Location="Left"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtApproveBy" ClientInstanceName="txtApproveBy" ClientEnabled ="false">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="true" Caption="Approve Date" Width="30%">
                                <CaptionSettings Location="Left"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="deApproveDate" ClientInstanceName="deApproveDate" Width="100%" ClientEnabled="false">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="true" Caption="Decision" Width="30%">
                                <CaptionSettings Location="Left"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtApproveDecision" ClientInstanceName="txtApproveDecision" ClientEnabled ="false">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:LayoutGroup>

            <dx:LayoutGroup Name="LayoutGroupWaiveApproval" ShowCaption="True" Caption="Waive Approval" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Approve Waive Amount" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtApproveWaiveAmt" runat="server" ClientInstanceName="txtApproveWaiveAmt" Number="0" DisplayFormatString="{0:#,0}" Width="100%" NullText="0">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Note" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="txtWaiveApprovalNote" ClientInstanceName="txtWaiveApprovalNote" NullText="">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutGroup Name="LayoutGroupWaiveApprovalDetail" ShowCaption="False" GroupBoxDecoration="None" ColCount="1" Width="100%">
                        <Items>
                            <dx:LayoutItem ShowCaption="true" Caption="Approve By" Width="30%">
                                <CaptionSettings Location="Left"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtWaiveApproveBy" ClientInstanceName="txtWaiveApproveBy" ClientEnabled ="false">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="true" Caption="Approve Date" Width="30%">
                                <CaptionSettings Location="Left"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="deWaiveApproveDate" ClientInstanceName="deWaiveApproveDate" Width="100%" ClientEnabled="false">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="true" Caption="Decision" Width="30%">
                                <CaptionSettings Location="Left"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtWaiveApprovalDecision" ClientInstanceName="txtWaiveApprovalDecision" ClientEnabled ="false">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    
                </Items>
            </dx:LayoutGroup>

            <dx:LayoutGroup Name="LayoutGroupReleaseDoc" ShowCaption="True" Caption="Release Document Execution" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Cashier No" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <%--<dx:ASPxTextBox runat="server" ID="txtCashierNo" ClientInstanceName="txtCashierNo" NullText="">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>--%>
                                <dx:ASPxGridLookup
                                        runat="server"
                                        ID="luCashierNo"
                                        ClientInstanceName="luCashierNo" 
                                        OnDataBinding="luCashierNo_DataBinding"
                                        AutoGenerateColumns="False"
                                        DisplayFormatString="{0}"
                                        TextFormatString="{0}"
                                        KeyFieldName="CASHIERNUMB"
                                        SelectionMode="Single"
                                        AnimationType="Fade" NullText="-- Select --" HelpText="Please select cashier number.">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        <GridViewProperties EnablePagingCallbackAnimation="true">
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                            <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                            <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                        </GridViewProperties>
                                        <Columns>
                                            <dx:GridViewDataColumn Caption="Cashier No." FieldName="CASHIERNUMB" ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="From" FieldName="RFROM" ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <GridViewStyles AdaptiveDetailButtonWidth="22">
                                        </GridViewStyles>
                                    </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Note" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="txtReleaseNote" ClientInstanceName="txtReleaseNote" NullText="">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Waive Document" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxUploadControl runat="server" ID="UploadCtrl" ClientInstanceName="UploadCtrl" ShowProgressPanel="true" UploadMode="Auto" AutoStartUpload="true" FileUploadMode="OnPageLoad"
                                    OnFileUploadComplete="UploadCtrl_FileUploadComplete" Width="100%">
                                    <ValidationSettings MaxFileSize="10000000" ErrorStyle-BackColor="Red" ShowErrors="true" AllowedFileExtensions=".pdf,.jpg,.jpeg,.img,.tiff,.xls,.xlsx,.txt,.doc,.docx,.rar,.zip">
                                        <ErrorStyle BackColor="Red"></ErrorStyle>
                                    </ValidationSettings>
                                     <ClientSideEvents FileUploadComplete="onUploadControlFileUploadComplete"/>
                                </dx:ASPxUploadControl>
                                <dx:ASPxButton ID="btnDownload" runat="server" ClientInstanceName="btnDownload" OnClick="btnDownload_Click" Text="View Document" ClientVisible="false" Width="35%" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Left" VerticalAlign="Middle">
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="btnView" runat="server" ClientInstanceName="btnView" OnClick="btnView_Click" Text="View Document" ClientVisible="false" Width="35%" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Left" VerticalAlign="Middle">
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutGroup Name="LayoutGroupReleaseDetail" ShowCaption="False" GroupBoxDecoration="None" ColCount="1" Width="100%">
                        <Items>
                            <dx:LayoutItem ShowCaption="true" Caption="Release By" Width="30%">
                                <CaptionSettings Location="Left"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtReleaseExecBy" ClientInstanceName="txtReleaseExecBy" ClientEnabled ="false">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="true" Caption="Release Document Date" Width="30%">
                                <CaptionSettings Location="Left"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="deReleaseExecDate" ClientInstanceName="deReleaseExecDate" Width="100%" ClientEnabled="false">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:LayoutGroup>

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
                        <dx:ASPxButton runat="server" ID="btnBack" ClientInstanceName="btnBack" Text="Back" AutoPostBack="true" Theme="Glass" PostBackUrl="~/Transactions/CreditProcess/ReleaseDocument/ReleaseDocList.aspx"></dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem> 
        </Items>
    </dx:ASPxFormLayout>

    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Modal="True" BackgroundImage-HorizontalPosition="center" Theme="Glass" Text="Loading Please Wait...">
        <BackgroundImage VerticalPosition="top" />
    </dx:ASPxLoadingPanel>
</asp:Content>
