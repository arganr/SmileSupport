<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FormCollectionRemarkEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections.FormCollectionRemarkEntry" %>

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
            }
            cplMain.cpCallbackParam = null;
        }

        var command = "";
        function OnBeginCallback(s, e) {
            command = e.command;
        }
        function OnTextChanged(s, e) {
            s.Upload();
        }
        function OncbActionChanged(cbAction) {
            if (cbAction.GetText() != "") {
                cplMain.PerformCallback("ACTION;" + cbAction.GetValue().toString());
            }
        }
        function OnKontrakNoChanged(s, e) {
            var grid = luAccNo.GetGridView();
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'CABANG;DEBITUR;JENIS PEMBIAYAAN;PRODUCT FACILITY;TENOR;NTF;OUTSTANDING AR;OVERDUE;LAST PAYMENT;SISA TENOR;NEXT DUEDATE', OnGetSelectedFieldValues);
        }
        function OnGetSelectedFieldValues(selectedValues) {
            mmRemark.SetValue(null);
            txtCabang.SetValue(selectedValues[0]);
            txtDebitur.SetValue(selectedValues[1]);
            txtJenisPembiayaan.SetValue(selectedValues[2]);
            txtProductFacility.SetValue(selectedValues[3]);
            txtTenor.SetValue(selectedValues[4]);
            txtNTF.SetValue(selectedValues[5]);
            txtOutstandingAR.SetValue(selectedValues[6]);
            txtOverdue.SetValue(selectedValues[7]);
            deLastPayment.SetDate(selectedValues[8]);
            txtSisaTenor.SetValue(selectedValues[9]);
            deNextDueDate.SetDate(selectedValues[10]);
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
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Theme="PlasticBlue" Font-Names="Calibri">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Name="FormCaption" GroupBoxDecoration="HeadingLine" Caption="Collection Remark Entry" ColCount="2">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutGroup GroupBoxDecoration="Box" Width="100%" Caption="Detail Entry" ShowCaption="True" GroupBoxStyle-Caption-BackColor="#f8fafd">
                        <Items>
                            <dx:LayoutItem ShowCaption="True" Caption="Contract No">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxGridLookup
                                            runat="server"
                                            ID="luAccNo"
                                            ClientInstanceName="luAccNo" OnDataBinding="luAccNo_DataBinding"
                                            AutoGenerateColumns="False"
                                            DisplayFormatString="{1}"
                                            TextFormatString="{1}"
                                            KeyFieldName="NO KONTRAK"
                                            SelectionMode="Single"
                                            Width="20%" GridViewProperties-EnablePagingCallbackAnimation="true" AnimationType="Slide">
                                            <ClientSideEvents ValueChanged="OnKontrakNoChanged" />
                                            <GridViewProperties>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                                <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                                <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True" />
                                            </GridViewProperties>
                                            <Columns>

                                                <dx:GridViewDataColumn Caption="Branch" FieldName="CABANG" ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Application No" FieldName="NO KONTRAK" ShowInCustomizationForm="True" VisibleIndex="2">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Debtor" FieldName="DEBITUR" ShowInCustomizationForm="True" VisibleIndex="3">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Jenis Pembiayaan" FieldName="JENIS PEMBIAYAAN" ShowInCustomizationForm="True" VisibleIndex="4">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Product Facility" FieldName="PRODUCT FACILITY" ShowInCustomizationForm="True" VisibleIndex="5">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Tenor" FieldName="TENOR" ShowInCustomizationForm="true" VisibleIndex="6">
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="NTF" FieldName="NTF" ShowInCustomizationForm="true" VisibleIndex="7" Visible="false">
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Outstanding AR" FieldName="OUTSTANDING AR" ShowInCustomizationForm="true" VisibleIndex="8" Visible="false">
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Overdue" FieldName="OVERDUE" ShowInCustomizationForm="true" VisibleIndex="9" Visible="false">
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataDateColumn Caption="Last payment" FieldName="LAST PAYMENT" ShowInCustomizationForm="true" VisibleIndex="10" Visible="false">
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Sisa Tenor" FieldName="SISA TENOR" ShowInCustomizationForm="true" VisibleIndex="11" Visible="false">
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataDateColumn Caption="Next Duedate" FieldName="NEXT DUEDATE" ShowInCustomizationForm="true" VisibleIndex="12" Visible="false">
                                                </dx:GridViewDataDateColumn>

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
                            <dx:LayoutItem ShowCaption="True" Caption="Collection Date">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="deCollDate" ClientInstanceName="deCollDate" Width="20%" ClientEnabled="true" EditFormat="DateTime" EditFormatString="dd/MM/yyyy">
                                            <ClientSideEvents ValueChanged="function(s,e) { cplMain.PerformCallback('COLL_DATE;' + deCollDate.GetValue()); }" />
                                            <TimeSectionProperties Visible="True">
                                                <TimeEditProperties EditFormatString="HH:mm:ss">
                                                </TimeEditProperties>
                                            </TimeSectionProperties>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Action">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox runat="server" ID="cbAction" ClientInstanceName="cbAction" Width="40%">
                                            <ClientSideEvents Init="function(s, e) { OncbActionChanged(s); }" SelectedIndexChanged="function(s, e) { OncbActionChanged(s); }" />
                                            <Items>
                                                <dx:ListEditItem Text="Bad Phone / No tidak valid" Value="123" />
                                                <dx:ListEditItem Text="Message / No tersambung dan sudah menitip pesan" Value="125" />
                                                <dx:ListEditItem Text="Customer sudah melakukan pembayaran" Value="126" />
                                                <dx:ListEditItem Text="Promise to directly / Berjanji membayar hari ini" Value="122" />
                                                <dx:ListEditItem Text="Promise to pay / Berjanji membayar pada tanggal tertentu" Value="121" />
                                                <dx:ListEditItem Text="Unable to contact / No tidak bisa dihubungi" Value="124" />
                                            </Items>
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="true" Name="liPromiseToPay" Caption="Promise pay date" ClientVisible="false">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="dePromisePayDate" ClientInstanceName="dePromisePayDate" Width="20%" EditFormat="DateTime" EditFormatString="dd/MM/yyyy">
                                            <TimeSectionProperties Visible="True">
                                                <TimeEditProperties EditFormatString="HH:mm:ss">
                                                </TimeEditProperties>
                                            </TimeSectionProperties>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Remark">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxMemo runat="server" ID="mmRemark" ClientInstanceName="mmRemark" Width="40%" Height="100px">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="" HorizontalAlign="Left">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButton runat="server" ID="btnSubmit" ClientInstanceName="btnSubmit" Text="Submit" ValidationGroup="Submit" AutoPostBack="false" Width="100px" Theme="Glass">
                                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVECONFIRM;' + 'SAVECONFIRM'); }" />
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup GroupBoxDecoration="Box" Width="100%" Caption="Contract Detail" ShowCaption="True" GroupBoxStyle-Caption-BackColor="#f8fafd">
                        <Items>
                            <dx:LayoutItem ShowCaption="True" Caption="Cabang">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtCabang" ClientInstanceName="txtCabang" ClientEnabled="false" Width="20%"></dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Debitur">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtDebitur" ClientInstanceName="txtDebitur" ClientEnabled="false" Width="20%"></dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Jenis Pembiayaan">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtJenisPembiayaan" ClientInstanceName="txtJenisPembiayaan" ClientEnabled="false" Width="20%"></dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Product Facility">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtProductFacility" ClientInstanceName="txtProductFacility" ClientEnabled="false" Width="20%"></dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Tenor">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtTenor" ClientInstanceName="txtTenor" ClientEnabled="false" Width="20%"></dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="NTF">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit runat="server" ID="txtNTF" ClientInstanceName="txtNTF" ClientEnabled="false" DisplayFormatString="#,0.00" Width="20%">
                                            <SpinButtons Enabled="False" ShowIncrementButtons="False"></SpinButtons>
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Outstanding AR">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit runat="server" ID="txtOutstandingAR" ClientInstanceName="txtOutstandingAR" ClientEnabled="false" DisplayFormatString="#,0.00" Width="20%">
                                            <SpinButtons Enabled="False" ShowIncrementButtons="False"></SpinButtons>
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Overdue">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtOverdue" ClientInstanceName="txtOverdue" ClientEnabled="false" Width="20%"></dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Last Payment">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="deLastPayment" ClientInstanceName="deLastPayment" ClientEnabled="false" Width="20%" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Sisa Tenor">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtSisaTenor" ClientInstanceName="txtSisaTenor" ClientEnabled="false" Width="20%"></dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Next DueDate">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="deNextDueDate" ClientInstanceName="deNextDueDate" ClientEnabled="false" Width="20%" EditFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:LayoutGroup>
        </Items>
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
