<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="UpdateNoSPPHEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.UpdateNoSPPH.UpdateNoSPPHEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../../Scripts/Application.js"></script>
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
                case "AGREEMENT_ONCHANGE":
                    OnluAgreementChanged();
                    break;
                case "IDPENGURUS_ONCHANGE":
                    OnluIDPengurusChanged();
                    break;
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
        function OnBeginCallback(s, e)
        {
            command = e.command;
        }
        function OnluAgreementChanged(s, e) {
            var grid = luAgreement.GetGridView();
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'NAME;TENOR;INSTALLMENT;C_NAME', OnGetSelectedAgreementFieldValues);
        }
        function OnGetSelectedAgreementFieldValues(selectedValues) {
            txtDebitur.SetValue(selectedValues[0]);
            txtTenor.SetValue(selectedValues[1]);
            txtInstallment.SetValue(selectedValues[2]);
            txtBranch.SetValue(selectedValues[3]);
        }
        function OnluIDPengurusChanged(s, e) {
            var grid = luPengurus.GetGridView();
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'ID;UserName', OnGetSelectedPengurusFieldValues);
        }
        function OnGetSelectedPengurusFieldValues(selectedValues) {
            txtNamaPengurus.SetValue(selectedValues[1]);
        }
        function OnrbtPengurusChanged(rbtPengurus) {
            txtNamaPengurus.SetText("");
            luPengurus.GetGridView().PerformCallback();
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
    <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" Theme="Glass">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup Name="LayoutGroupUpdateNoSPPHEntry" ShowCaption="True" Caption="Update No. SPPH Entry" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Agreement No." Width="25%">
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
                                            <ClientSideEvents ValueChanged="function(s, e) { cplMain.PerformCallback('AGREEMENT_ONCHANGE;' + 'CHANGE'); }"/>
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
                                                <dx:GridViewDataColumn Caption="Installment" FieldName="INSTALLMENT" ShowInCustomizationForm="True" VisibleIndex="3">
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
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Branch" Width="25%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtBranch" ClientInstanceName="txtBranch" NullText="..." Width="100%">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Status" Width="25%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtStatus" ClientInstanceName="txtStatus" NullText="..." Width="100%">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Nama Debitur" Width="25%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDebitur" ClientInstanceName="txtDebitur" NullText="..." Width="100%">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Tenor" Width="25%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtTenor" ClientInstanceName="txtTenor" NullText="..." Width="100%">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Installment" Width="25%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtInstallment" ClientInstanceName="txtInstallment" NullText="..." DisplayFormatString="{0:#,0.##}" Width="100%">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Jenis Pengurus" Width="25%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList runat="server" ID="rbtPengurus" ClientInstanceName="rbtPengurus" Border-BorderStyle="None" RepeatColumns="2" ValueType="System.String" Font-Size="8">
                                        <Border BorderStyle="None"/>
                                        <Items>
                                            <dx:ListEditItem Text="KARYAWAN" Value="KARYAWAN" Selected="true"/>
                                            <dx:ListEditItem Text="MITRA" Value="MITRA"/>
                                        </Items>
                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                            <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                        </ValidationSettings>
                                        <%--<ClientSideEvents Init="function(s, e) { OnrbtPengurusChanged(s); }" SelectedIndexChanged="function(s, e) { OnrbtPengurusChanged(s); }" />--%>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { OnrbtPengurusChanged(s); }" />
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="ID Pengurus" Width="25%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridLookup
                                            runat="server"
                                            ID="luPengurus"
                                            ClientInstanceName="luPengurus" 
                                            OnDataBinding="luPengurus_DataBinding" OnInit="luPengurus_Init"
                                            AutoGenerateColumns="False"
                                            DisplayFormatString="{0}"
                                            TextFormatString="{0}"
                                            KeyFieldName="ID"
                                            SelectionMode="Single"
                                            AnimationType="Fade" NullText="-- Select --" HelpText="Please select pic id.">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                            <ClientSideEvents ValueChanged="function(s, e) { cplMain.PerformCallback('IDPENGURUS_ONCHANGE;' + 'CHANGE'); }"/>
                                            <GridViewProperties EnablePagingCallbackAnimation="true">
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                                <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                                <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                            </GridViewProperties>
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="ID" FieldName="ID" ShowInCustomizationForm="True" VisibleIndex="0">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Nama" FieldName="UserName" ShowInCustomizationForm="True" VisibleIndex="1">
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
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Nama Pengurus" Width="25%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtNamaPengurus" ClientInstanceName="txtNamaPengurus" NullText="..." Width="100%">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="No. SPPH" Width="25%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtNoSPPH" ClientInstanceName="txtNoSPPH" NullText="..." Width="100%">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:TabbedLayoutGroup Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Height="100px" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee" Visible="False">
                        <Items>
                            <dx:LayoutGroup Caption="Upload Document">
                                    <Items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxGridView
                                                        ID="gvUploadDoc"
                                                        ClientInstanceName="gvUploadDoc"
                                                        runat="server"
                                                        KeyFieldName="ID"
                                                        Width="100%"
                                                        AutoGenerateColumns="False"
                                                        EnableCallBacks="true"
                                                        EnablePagingCallbackAnimation="true"
                                                        EnableTheming="True"
                                                        Theme="Glass" Font-Size="Small" Font-Names="Calibri" OnCustomButtonCallback="gvUploadDoc_CustomButtonCallback" OnDataBinding="gvUploadDoc_DataBinding" OnCustomCallback="gvUploadDoc_CustomCallback">
                                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                                        </SettingsAdaptivity>
                                                        <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                        <SettingsSearchPanel Visible="True" />
                                                        <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                        <SettingsLoadingPanel Mode="Disabled" />
                                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                                        <SettingsPager PageSize="5"></SettingsPager>
                                                        <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                                        <Toolbars>
                                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                                                <Items>
                                                                    <dx:GridViewToolbarItem Command="ShowCustomizationWindow" DisplayMode="ImageWithText" />
                                                                </Items>
                                                            </dx:GridViewToolbar>
                                                        </Toolbars>
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Name="colID" Caption="ID." FieldName="ID" ReadOnly="True" Visible="false">
                                                                <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Name="colAppNo" Caption="App No" FieldName="AppNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                                                <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Name="colAgreeNo" Caption="Agreement" FieldName="AgreeNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                                                <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Name="colDebitur" Caption="Debitur" FieldName="DebiturName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                                                <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Name="colModule" Caption="Module" FieldName="Module" ReadOnly="True" ShowInCustomizationForm="true" Visible="false">
                                                                <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataDateColumn Name="colName" Caption="Document Type" FieldName="Name" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss" Visible="true">
                                                                <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataTextColumn Name="colExt" Caption="Jenis File" FieldName="Ext" ReadOnly="True" UnboundType="String" Width="10%">
                                                                <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Name="colRemarks" Caption="Remark" FieldName="Remarks" ReadOnly="True" UnboundType="String">
                                                                <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataDateColumn Name="colCreatedDateTime" Caption="Upload Date" FieldName="CreatedDateTime" ReadOnly="True" UnboundType="DateTime" Width="10%" Visible="True">
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesDateEdit>
                                                                <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Link"  Caption="">
                                                                <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                                                <CustomButtons>
                                                                    <dx:GridViewCommandColumnCustomButton ID="GridbtnDownload" Text=" ">
                                                                        <Image Height="20px" Width="20px" Url="../../../Content/Images/download.png" ToolTip="Click here to download file."></Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                </CustomButtons>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="CreatedBy" FieldName="CreatedBy" ReadOnly="True" UnboundType="String" Width="10%" Visible="false">
                                                                <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <Styles>
                                                            <AlternatingRow Enabled="True"></AlternatingRow>
                                                        </Styles>
                                                        <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                        </Items>
                    </dx:TabbedLayoutGroup>
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
                        <dx:ASPxButton runat="server" ID="btnBack" ClientInstanceName="btnBack" Text="Back" AutoPostBack="true" Theme="Glass" PostBackUrl="~/Transactions/Syariah/UpdateNoSPPH/UpdateNoSPPHList.aspx"></dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem> 
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
