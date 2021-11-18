<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ApplicationEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.ApplicationEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        var prevOnLoad = window.onload;
        window.onload = myOnLoad;
        function myOnLoad()
        {
            if (prevOnLoad != null)
                prevOnLoad();
            document.onkeydown = myOnKeyDown;
        }
        function myOnKeyDown()
        {
            if (event.keyCode == 27)
                gvDetail.UpdateEdit();
            if (event.keyCode == 13)
                gvDetail.UpdateEdit();
        }
        function gvDetail_EndCallback(s, e)
        {
            if (s.cpCmd == "INSERT" || s.cpCmd == "UPDATE" || s.cpCmd == "DELETE")
            {
                seOTR.SetText("0");
                seNTF.SetText("0");

                seOTR.SetText(s.cpTotal);
                seOTR.GetInputElement().readOnly = true;
                seNTF.SetText(s.cpTotal);
                seNTF.GetInputElement().readOnly = true;
                calculationNTF();
                s.cpCmd = "";
            }
        }
        function cplMain_EndCallback(s, e)
        {
            switch (cplMain.cpCallbackParam)
            {
                case "DEBITUR_ONCHANGE":
                        OnDebiturChanged();
                    break;
                case "SAVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "SAVECONFIRM":
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
                case "APPROVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "APPROVECONFIRM":;
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
                case "HOLD_RELEASE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "HOLD_RELEASE_CONFIRM":;
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
                case "REJECTCONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
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
                case "CAM":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "CAM_CONFIRM":
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
                case "RETURN":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "RETURN_CONFIRM":
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
                case "CANCEL":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "CANCEL_CONFIRM":
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
            }
        }
        function calculationGrid()
        {
            var vQty = parseFloat(0.0);
            if (gvDetail.GetEditor("Qty").GetValue() != null &&
                gvDetail.GetEditor("Qty").GetValue().toString() != "" &&
                gvDetail.GetEditor("Qty").GetValue().toString().length != 0) {
                vQty = parseFloat(gvDetail.GetEditor("Qty").GetValue().toString());
            }
            var vUnitPrice = parseFloat(0.0);
            if (gvDetail.GetEditor("UnitPrice").GetValue() != null &&
                gvDetail.GetEditor("UnitPrice").GetValue().toString() != "" &&
                gvDetail.GetEditor("UnitPrice").GetValue().toString().length != 0) {
                vUnitPrice = parseFloat(gvDetail.GetEditor("UnitPrice").GetValue().toString());
            }
            gvDetail.GetEditor("SubTotal").SetValue(vQty * vUnitPrice);
        }
        function calculationNTF()
        {
            var vOTR = parseFloat(0.0);
            if (seOTR.GetValue() != null && seOTR.GetValue().toString() != "" && seOTR.GetValue().toString().length != 0)
            {
                vOTR = parseFloat(seOTR.GetValue().toString());
            }
            var vDP = parseFloat(0.0);
            if (seDP.GetValue() != null && seDP.GetValue().toString() != "" && seDP.GetValue().toString().length != 0)
            {
                vDP = parseFloat(seDP.GetValue().toString());
            }

            if (vOTR != 0)
            {
                seNTF.SetValue(vOTR - vDP);
            }
        }
        function OnItemDescriptionValueChanged(s, e)
        {
            var vYear = new Date();
            gvDetail.GetEditor("Year").SetValue(vYear.getFullYear());
            gvDetail.GetEditor("Qty").SetValue("0");
            gvDetail.GetEditor("UnitPrice").SetValue("0");
            gvDetail.GetEditor("SubTotal").SetValue("0");
        }
        function btnAddCommentOnCLick()
        {
            apcFormComment.Show();
        }
        function OnDebiturChanged(s, e) {
            var grid = luDebitur.GetGridView();
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'DEBITUR_CODE;DEBITUR_CODE', OnGetSelectedFieldValues);
        }
        function OnGetSelectedFieldValues(selectedValues) {
            txtCIF.SetValue(selectedValues[1]);
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />
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
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True" Theme="Glass" EnableCallbackAnimation="true" Width="400px" Height="100px"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcFormComment" ClientInstanceName="apcFormComment" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Add Comment" AllowDragging="True" PopupAnimationType="Fade"
        EnableCallbackAnimation="true" CloseAction="CloseButton"
        EnableViewState="False"
        Width="400px"
        Height="200px"
        FooterStyle-Wrap="False" ShowFooter="false" FooterText="" AllowResize="false">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" ColCount="2" Width="100%">
                    <Items>      
                        <dx:LayoutItem ShowCaption="True" Caption="Req. Disb. Date" Width="100%" ColSpan="2" Name="lytDisbDate">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxDateEdit runat="server" ID="deDistDate" ClientInstanceName="deDistDate" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" Width="100%" ClientEnabled="false"></dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>                 
                        <dx:LayoutItem ShowCaption="True" Caption="Comment" Width="100%" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxMemo runat="server" ID="mmComment" ClientInstanceName="mmComment" Width="100%" Height="100px"></dx:ASPxMemo>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="" Width="100%" ColSpan="2" HorizontalAlign="Right">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                    <dx:ASPxButton ID="btnSaveComment" runat="server" Text="Save" AutoPostBack="False" UseSubmitBehavior="false" Width="50%">
                                        <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('SAVE_COMMENT'); apcFormComment.Hide(); }" />
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="btnCancelComment" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="50%">
                                        <ClientSideEvents Click="function(s, e) { apcFormComment.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Theme="Glass">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Name="LayoutGroupApplicationEntry" GroupBoxDecoration="Box" Caption="Application Entry" Width="100%" ColCount="1">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="#f8fafd">
                    </Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutGroup GroupBoxDecoration="Box" Caption="" ColCount="3" GroupBoxStyle-Border-BorderColor="#d1ecee" Width="100%">
                        <Items>
                            <dx:LayoutItem ShowCaption="True" Caption="CIF">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtCIF" ClientInstanceName="txtCIF" AutoPostBack="false" ClientEnabled="false" NullText="...">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="False" Caption="" HorizontalAlign="Center">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel runat="server" ID="lblStatus" ClientInstanceName="lblStatus" Text="Application Status" Font-Names="Calibri" Font-Bold="true" ForeColor="#339933" Font-Size="Large"></dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Submit" HorizontalAlign="Left">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxCheckBox runat="server" ID="chkSubmit" ClientInstanceName="chkSubmit" Text="" TextAlign="Left" AutoPostBack="false"></dx:ASPxCheckBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Debitur">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxGridLookup
                                            runat="server"
                                            ID="luDebitur"
                                            ClientInstanceName="luDebitur"
                                            AutoPostBack="false"
                                            KeyFieldName="DEBITUR_NAME"
                                            DisplayFormatString="{1}"
                                            TextFormatString="{1}"
                                            SelectionMode="Single"
                                            OnDataBinding="luDebitur_DataBinding" NullText="-- Select --">
                                            <ClientSideEvents ValueChanged="function(s,e) { cplMain.PerformCallback('DEBITUR_ONCHANGE;' + 'CHANGE'); }"/>
                                            <GridViewProperties>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                                <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                                <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True" />
                                            </GridViewProperties>
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Debitur Code" Name="colDEBITUR_CODE" FieldName="DEBITUR_CODE" ShowInCustomizationForm="True" VisibleIndex="0">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Debitur Name" Name="colDEBITUR_NAME" FieldName="DEBITUR_NAME" ShowInCustomizationForm="True" VisibleIndex="1">
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
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Application No">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtDocNo" ClientInstanceName="txtDocNo" AutoPostBack="false">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtDebitur2" ClientInstanceName="txtDebitur2" AutoPostBack="false">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Date">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="deDocDate" ClientInstanceName="deDocDate" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" AutoPostBack="false">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Supplier">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxGridLookup runat="server"
                                            ID="luSupplier"
                                            ClientInstanceName="luSupplier"
                                            AutoPostBack="false"
                                            KeyFieldName="SUPP_NAME"
                                            DisplayFormatString="{1}"
                                            TextFormatString="{1}"
                                            SelectionMode="Single"
                                            OnDataBinding="cbSupplier_DataBinding" NullText="-- Select --">
                                            <GridViewProperties>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                                <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                                <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True" />
                                            </GridViewProperties>
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Supplier Code" FieldName="SUPP_CODE" ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Supplier Name" FieldName="SUPP_NAME" ShowInCustomizationForm="True" VisibleIndex="2">
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
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Contract Type">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox runat="server" ID="cbAppType" ClientInstanceName="cbAppType" AutoPostBack="false" NullText="-- Select --">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Branch Supplier">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtSupplierBranch" ClientInstanceName="txtSupplierBranch" AutoPostBack="false" NullText="..."></dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Branch">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox runat="server"
                                            ID="cbBranch"
                                            ClientInstanceName="cbBranch"
                                            AutoPostBack="false"
                                            KeyFieldName="C_NAME" ValueField="C_NAME" TextField="C_NAME"
                                            DisplayFormatString="{1}"
                                            TextFormatString="{1}"
                                            SelectionMode="Single"
                                            OnDataBinding="cbBranch_DataBinding" NullText="-- Select --">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Code" FieldName="C_CODE" />
                                                <dx:ListBoxColumn Caption="Branch" FieldName="C_NAME" />
                                            </Columns>
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Marketing Supplier">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtMarketingSupplier" ClientInstanceName="txtMarketingSupplier" AutoPostBack="false" NullText="...">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Lease Object">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox runat="server" ID="cbObjectPembiayaan" ClientInstanceName="cbObjectPembiayaan" AutoPostBack="false" NullText="-- Select --">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Lease Facility">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox runat="server" ID="cbFacility" ClientInstanceName="cbFacility" AutoPostBack="false" NullText="-- Select --">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Binding Type">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox runat="server" ID="cbJenisPengikatan" ClientInstanceName="cbJenisPengikatan" AutoPostBack="false" NullText="-- Select --">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Package">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox runat="server" ID="cbPackage" ClientInstanceName="cbPackage" AutoPostBack="false" NullText="-- Select --">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:TabbedLayoutGroup Height="200px" Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                        <Items>
                            <dx:LayoutGroup Caption="Detail Asset">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                                <dx:ASPxGridView
                                                    runat="server"
                                                    ID="gvDetail"
                                                    ClientInstanceName="gvDetail"
                                                    Width="100%"
                                                    KeyFieldName="DtlKey"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Glass"
                                                    Font-Names="Calibri"
                                                    OnInit="gvDetail_Init"
                                                    OnInitNewRow="gvDetail_InitNewRow"
                                                    OnDataBinding="gvDetail_DataBinding"
                                                    OnRowInserting="gvDetail_RowInserting"
                                                    OnRowUpdating="gvDetail_RowUpdating"
                                                    OnRowDeleting="gvDetail_RowDeleting"
                                                    OnCustomCallback="gvDetail_CustomCallback"
                                                    OnAutoFilterCellEditorInitialize="gvDetail_AutoFilterCellEditorInitialize"
                                                    OnCustomColumnDisplayText="gvDetail_CustomColumnDisplayText">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents EndCallback="gvDetail_EndCallback" />
                                                    <ClientSideEvents />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" />
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsText ConfirmDelete="Are you really want to Delete?" />
                                                    <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom"></SettingsEditing>
                                                    <SettingsCommandButton>
                                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px">
                                                        </NewButton>
                                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                    </SettingsCommandButton>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colNo" Caption="No" ReadOnly="True" UnboundType="String" VisibleIndex="0" Width="2%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colDtlKey" Caption="DtlKey" FieldName="DtlKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="2">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="3">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colSeq" Caption="Seq" FieldName="Seq" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="4">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colCondition" Caption="Condition" FieldName="Condition" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="5" Width="8%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesComboBox ClientInstanceName="colCondition"
                                                                TextField="Condition" ValueField="Condition" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList"
                                                                TextFormatString="{0}">
                                                                <Items>
                                                                    <dx:ListEditItem Text="New" Value="New" />
                                                                    <dx:ListEditItem Text="Used" Value="Used" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colAssetTypeDetail" Caption="Asset Type" FieldName="AssetTypeDetail" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="6" Width="8%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesComboBox ClientInstanceName="colCondition"
                                                                TextField="Condition" ValueField="Condition" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList"
                                                                TextFormatString="{0}">
                                                                <Items>
                                                                    <dx:ListEditItem Text="BPKB" Value="BPKB" />
                                                                    <dx:ListEditItem Text="NONBPKB" Value="NONBPKB" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colItemDescription" Caption="Item Descirption" FieldName="ItemDescription" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="7" Width="35%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesTextEdit>
                                                                <ClientSideEvents ValueChanged="OnItemDescriptionValueChanged" />
                                                            </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colYear" Caption="Year" FieldName="Year" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="8" Width="5%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false">
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn
                                                            Name="colUnitPrice"
                                                            Caption="Unit Price"
                                                            FieldName="UnitPrice"
                                                            ReadOnly="false"
                                                            ShowInCustomizationForm="True"
                                                            PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}"
                                                            PropertiesSpinEdit-DisplayFormatInEditMode="true"
                                                            PropertiesSpinEdit-AllowMouseWheel="false"
                                                            PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false"
                                                            PropertiesSpinEdit-MinValue="0"
                                                            PropertiesSpinEdit-MaxValue="999999999999999"
                                                            PropertiesSpinEdit-ClientSideEvents-ValueChanged="calculationGrid"
                                                            VisibleIndex="9"
                                                            Width="15%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colQty" Caption="Qty" FieldName="Qty" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="10" Width="10%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesSpinEdit AllowMouseWheel="false" MinValue="0" MaxValue="999">
                                                                <ClientSideEvents ValueChanged="calculationGrid" />
                                                            </PropertiesSpinEdit>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn
                                                            Name="colSubTotal"
                                                            Caption="Sub Total"
                                                            FieldName="SubTotal"
                                                            ReadOnly="true"
                                                            ShowInCustomizationForm="True"
                                                            PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}"
                                                            PropertiesSpinEdit-DisplayFormatInEditMode="true"
                                                            PropertiesSpinEdit-AllowMouseWheel="false"
                                                            PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false"
                                                            PropertiesSpinEdit-MinValue="0"
                                                            PropertiesSpinEdit-MaxValue="999999999999999"
                                                            VisibleIndex="11"
                                                            Width="15%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            <dx:LayoutGroup Caption="Remark" ClientVisible="false">
                                <Items>
                                    <dx:LayoutItem Caption="Remark 1" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxMemo runat="server" ID="txtRemark1" ClientInstanceName="txtRemark1" Width="40%" Height="50" Theme="MetropolisBlue"></dx:ASPxMemo>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Remark 2" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxMemo runat="server" ID="txtRemark2" ClientInstanceName="txtRemark2" Width="40%" Height="50" Theme="MetropolisBlue"></dx:ASPxMemo>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Remark 3" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxMemo runat="server" ID="txtRemark3" ClientInstanceName="txtRemark3" Width="40%" Height="50" Theme="MetropolisBlue"></dx:ASPxMemo>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Remark 4" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxMemo runat="server" ID="txtRemark4" ClientInstanceName="txtRemark4" Width="40%" Height="50" Theme="MetropolisBlue"></dx:ASPxMemo>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            <dx:LayoutGroup Caption="Note" Width="100%">
                                <Items>
                                    <dx:LayoutItem Caption="Note">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxMemo runat="server" ID="mmNote" ClientInstanceName="mmNote" Width="100%" Height="150" Theme="MetropolisBlue"></dx:ASPxMemo>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
                    </dx:TabbedLayoutGroup>
                    <dx:LayoutGroup GroupBoxDecoration="Box" Caption="" ColCount="3" GroupBoxStyle-Border-BorderColor="#d1ecee" Width="100%">
                        <Items>
                            <dx:LayoutItem Caption="On The Road (OTR)">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit runat="server" ID="seOTR" ClientInstanceName="seOTR" AutoPostBack="false" MinValue="0" MaxValue="999999999999999" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" ReadOnly="true" Font-Bold="true" ForeColor="#666666">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem Caption="Down Payment (DP)">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit runat="server" ID="seDP" ClientInstanceName="seDP" AutoPostBack="false" MinValue="0" MaxValue="999999999999999" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" Font-Bold="true" ForeColor="#666666">
                                            <ClientSideEvents ValueChanged="calculationNTF" />
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem Caption="Nett To Finance (NTF)">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit runat="server" ID="seNTF" ClientInstanceName="seNTF" AutoPostBack="false" MinValue="0" MaxValue="999999999999999" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" Font-Bold="true" ForeColor="#666666">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem Caption="Tenor">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit runat="server" ID="seTenor" ClientInstanceName="seTenor" AutoPostBack="false" MinValue="0" MaxValue="60" DisplayFormatString="#,0" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" Font-Bold="true" ForeColor="#666666">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem Caption="Effective Rate %">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit runat="server" ID="seEffRate" ClientInstanceName="seEffRate" AutoPostBack="false" MinValue="0" MaxValue="100" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" Font-Bold="true" ForeColor="#666666">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutItem ShowCaption="False" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnAddComment" ClientInstanceName="btnAddComment" Text="Add Comment" AutoPostBack="false" UseSubmitBehavior="false" Width="150px">
                                    <ClientSideEvents Click="btnAddCommentOnCLick" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Name="eliButton" Width="10%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Text="Save" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVECONFIRM;' + 'SAVECONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnApprove" ClientInstanceName="btnApprove" Text="Proceed" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('APPROVECONFIRM;' + 'APPROVECONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnHoldRelease" ClientInstanceName="btnHoldRelease" Text="Hold" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('HOLD_RELEASE_CONFIRM;' + 'HOLD_RELEASE_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnReturn" ClientInstanceName="btnReturn" Text="Return" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('RETURN_CONFIRM;' + 'RETURN_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnReject" ClientInstanceName="btnReject" Text="Reject" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('REJECTCONFIRM;' + 'REJECTCONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnCancel" ClientInstanceName="btnCancel" Text="Cancel" AutoPostBack="false" UseSubmitBehavior="false" Width="100%" ForeColor="Red">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('CANCEL_CONFIRM;' + 'CANCEL_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnBack" ClientInstanceName="btnBack" Text="Back" AutoPostBack="false" UseSubmitBehavior="false" Width="100%" OnClick="btnBack_Click"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView 
                                    runat="server" 
                                    ID="dvComment" 
                                    ClientInstanceName="dvComment" 
                                    Width="100%"
                                    AutoGenerateColumns="False" EnableTheming="true" 
                                    Theme="MetropolisBlue" 
                                    EnablePagingCallbackAnimation="true" 
                                    EnableCallbackAnimation="true" OnDataBinding="dvComment_DataBinding" 
                                    Font-Size="8" Font-Names="Calibri">
                                    <Columns>
                                        <dx:GridViewDataDateColumn Caption="Comment Date" FieldName="CommentDate" Name="colCommentDate" ReadOnly="True" ShowInCustomizationForm="True" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" VisibleIndex="0" Width="15%">
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Caption="Comment By" FieldName="CommentBy" Name="colCommentBy" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Width="20%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataMemoColumn Caption="Comment" FieldName="CommentNote" Name="colCommentNote" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="65%">
                                        </dx:GridViewDataMemoColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="false" EnableRowHotTrack="true" AllowSort="false"/>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnCam" ClientInstanceName="btnCam" Text="Credit Approval Memo" AutoPostBack="false" UseSubmitBehavior="false" ClientEnabled="false" Width="150px">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('CAM_CONFIRM;' + 'CAM_CONFIRM'); }"/>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" GroupBoxDecoration="None" ColCount="2" Width="100%" HorizontalAlign="Center">
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                <dx:ASPxLabel runat="server" ID="lblerror" ForeColor="Red"></dx:ASPxLabel>
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
    <asp:SqlDataSource ID="sdsComment" runat="server"></asp:SqlDataSource>
</asp:Content>
