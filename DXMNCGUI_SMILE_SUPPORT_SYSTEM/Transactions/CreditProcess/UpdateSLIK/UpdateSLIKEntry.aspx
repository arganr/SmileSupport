<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="UpdateSLIKEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.UpdateSLIK.UpdateSLIKEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../../Scripts/Application.js"></script>
    <script type="text/javascript">
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

        var CustomErrorText = "* Value can't be empty.";
        function cplMain_EndCallback(s, e)
        {
            switch (cplMain.cpCallbackParam)
            {
                case "ON_APPNO_VALIDATION":
                    if (cplMain.cpStrErrorMsg.length > 0) {
                        apcalert.SetContentHtml(cplMain.cpStrErrorMsg);
                        apcalert.Show();
                        {
                            luAppNo.SetText('');
                        }
                        break;
                    }
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
                case "LUAPPNO_CHANGED":
                    if (cplMain.cpStrErrorMsg.length > 0)
                        return;
                    vcDetail.PerformCallback();
                    gvUploadDoc.PerformCallback();
                    break;
            }
            cplMain.cpCallbackParam = null;
        }
        var command = "";
        function OnBeginCallback(s, e)
        {
            command = e.command;
        }
        function OnLuAppNoChanged(luAppNo)
        {
            var grid = luAppNo.GetGridView();
            if (luAppNo.GetText() != "")
            {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'NAME;', OnGetSelectedFieldValues);
                cplMain.PerformCallback("LUAPPNO_CHANGED;" + luAppNo.GetValue().toString());
            }
        }
        function OnAppNoValidation(s, e) {
            cplMain.PerformCallback('ON_APPNO_VALIDATION;ON_APPNO_VALIDATION');
        }
        function OnGetSelectedFieldValues(selectedValues) {
            txtDebitur.SetValue(selectedValues[0]);
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
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Info" AllowDragging="True" PopupAnimationType="None"
        EnableViewState="False" Width="400" Height="75">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" Theme="Glass">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup Name="LayoutGroupApplicationEntry" ShowCaption="True" Caption="SLIK Update Entry" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                    <Items>
                        <dx:LayoutItem ShowCaption="true" Caption="Document No." Width="25%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtDocNo" ClientInstanceName="txtDocNo" NullText="..." Width="100%" ReadOnly="true">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="Checking Date." Width="25%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxDateEdit runat="server" ID="deDocDate" ClientInstanceName="deDocDate" DisplayFormatString="dd/MM/yyyy" NullText="-- Select --" HelpText="you cant change document date,  this is required field default by system.">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="Application No." Width="25%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridLookup runat="server" ID="luAppNo" ClientInstanceName="luAppNo" KeyFieldName="APPLICNO" DataSourceID="sdsApplication" DisplayFormatString="{1}" TextFormatString="{1};{2};{3};{4}" MultiTextSeparator=";" Width="100%">
                                        <ClientSideEvents Init="function(s, e) { OnLuAppNoChanged(s); }" ValueChanged="function(s, e) { OnLuAppNoChanged(s); }" Validation="OnAppNoValidation"/>
                                        <GridViewProperties EnablePagingCallbackAnimation="true">
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                            <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                            <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                        </GridViewProperties>
                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                            <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxGridLookup>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Debitur" Width="25%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtDebitur" ClientInstanceName="txtDebitur" NullText="..." Width="100%" ReadOnly="true">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="SLIK Checking" Width="25%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox runat="server" ID="cbSlikCheck" ClientInstanceName="cbSlikCheck" NullText="-- Select --">
                                        <Items>
                                            <dx:ListEditItem Text="TERSEDIA" Value="1" />
                                            <dx:ListEditItem Text="TIDAK TERSEDIA" Value="0" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="CA Verification" Width="25%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxCheckBox runat="server" ID="chkSubmit" ClientInstanceName="chkSubmit" ValueType="System.String" ValueChecked="1" ValueUnchecked="0" Width="100%">
                                    </dx:ASPxCheckBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Remark" Width="40%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                       <dx:ASPxMemo runat="server" ID="mmRemark1" ClientInstanceName="mmRemark1" Width="100%" Height="100px"></dx:ASPxMemo>     
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                        <dx:TabbedLayoutGroup Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Height="100px" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                            <Items>
                                <dx:LayoutGroup Caption="SLIK Detail">
                                    <items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
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
                                                    OnRowInserting="gvDetail_RowInserting"
                                                    OnRowUpdating="gvDetail_RowUpdating"
                                                    OnRowDeleting="gvDetail_RowDeleting"
                                                    OnDataBinding="gvDetail_DataBinding" 
                                                    OnCustomColumnDisplayText="gvDetail_CustomColumnDisplayText" OnCustomButtonCallback="gvDetail_CustomButtonCallback">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" ShowFooter="true"/>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="10" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsEditing Mode="EditForm" UseFormLayout="true" NewItemRowPosition="Top"></SettingsEditing>
                                                    <SettingsCommandButton>
                                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px"></NewButton>
                                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                    </SettingsCommandButton>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" Width="5%" VisibleIndex="0">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewCommandColumn  Name="ClmnCommand2"  ShowNewButton="false" ShowEditButton="false" ButtonRenderMode="Image" Caption=" " VisibleIndex="2" Width="1%" Visible="false">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="ctmbtnView">
                                                                    <Image ToolTip="View detail" Url="../../../Content/Images/ViewIcon-16x16.png"/>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colNo" Caption="No" ReadOnly="True" UnboundType="String" Width="1%" VisibleIndex="1">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colDtlKey" Caption="DtlKey" FieldName="DtlKey" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="2" Visible="false">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="3" Visible="false">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colSeq" Caption="Seq" FieldName="Seq" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="4" Visible="false">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colJenisPembiayaan" Caption="Jenis Pembiayaan" FieldName="JenisPembiayaan" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="5">
                                                            <PropertiesComboBox
                                                                TextField="JenisPembiayaan" ValueField="JenisPembiayaan" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList"
                                                                TextFormatString="{0}">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Kredit Investasi / Konsumsi" Value="1"/>
                                                                    <dx:ListEditItem Text="Kartu Kredit" Value="2"/>
                                                                    <dx:ListEditItem Text="Kredit Modal Kerja" Value="3"/>
                                                                </Items>
                                                            </PropertiesComboBox>
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colPerusahaanPembiayaan" Caption="Bank/Perusahaan Pembiayaan" FieldName="PerusahaanPembiayaan" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="6">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colAtasNama" Caption="Atas Nama" FieldName="AtasNama" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="7">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colPlafon" Caption="Plafon" FieldName="Plafon" ReadOnly="false" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="8">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colBakiDebet" Caption="Baki Debet" FieldName="BakiDebet" ReadOnly="false" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="9">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colBunga" Caption="Bunga(%)" FieldName="Bunga" ReadOnly="false" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="false" VisibleIndex="10">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataDateColumn Name="colTglAkadAwal" Caption="Akad Awal" FieldName="TglAkadAwal" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="false" VisibleIndex="11">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn Name="colTglAwalSisaTenor" Caption="Awal Sisa Tenor" FieldName="TglAwalSisaTenor" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="false" VisibleIndex="12">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn Name="colJatuhTempo" Caption="Jatuh Tempo" FieldName="TglJatuhTempo" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="false" VisibleIndex="13">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colAngsuran" Caption="Angsuran" FieldName="Angsuran" ReadOnly="false" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="14">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colKolektibilitas" Caption="Kolektibilitas" FieldName="Kolektibilitas" ReadOnly="false" ShowInCustomizationForm="true" Visible="false" VisibleIndex="15">
                                                            <PropertiesComboBox
                                                                TextField="coll" ValueField="coll" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList"
                                                                TextFormatString="{0}">
                                                                <Items>
                                                                    <dx:ListEditItem Text="COLL 1 (0-10)" Value="1" />
                                                                    <dx:ListEditItem Text="COLL 2 (11-90)" Value="2" />
                                                                    <dx:ListEditItem Text="COLL 3 (91-120)" Value="3" />
                                                                    <dx:ListEditItem Text="COLL 4 (121-180)" Value="4" />
                                                                    <dx:ListEditItem Text="COLL 5 (> 180)" Value="5" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colHistoryKolek" Caption="History Kolek" FieldName="HistoryKolek" ReadOnly="false" ShowInCustomizationForm="true" Visible="false" VisibleIndex="16">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <PropertiesComboBox
                                                                TextField="collhist" ValueField="collhist" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList"
                                                                TextFormatString="{0}">
                                                                <Items>
                                                                    <dx:ListEditItem Text="COLL 1 (0-10)" Value="1" />
                                                                    <dx:ListEditItem Text="COLL 2 (11-90)" Value="2" />
                                                                    <dx:ListEditItem Text="COLL 3 (91-120)" Value="3" />
                                                                    <dx:ListEditItem Text="COLL 4 (121-180)" Value="4" />
                                                                    <dx:ListEditItem Text="COLL 5 (> 180)" Value="5" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colAktualOverDue" Caption="Aktual Overdue" FieldName="AktualOverDue" ReadOnly="false" ShowInCustomizationForm="true" Visible="false" VisibleIndex="17">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                        <EditFormLayoutProperties >
                                                            <Items>
                                                                <dx:GridViewLayoutGroup GroupBoxDecoration="None" Width="100%" ColCount="3">
                                                                    <Items>
                                                                        <dx:GridViewColumnLayoutItem Caption="Jenis Pembiayaan" ColumnName="colJenisPembiayaan" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Perusahaan Pembiayaan / Bank" ColumnName="colPerusahaanPembiayaan" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Atas Nama" ColumnName="colAtasNama" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="3"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Plafon" ColumnName="colPlafon" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Baki Debet" ColumnName="colBakiDebet" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Bunga(%)" ColumnName="colBunga" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="3"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Akad Awal" ColumnName="colTglAkadAwal" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Tgl Awal Sisa Tenor" ColumnName="colTglAwalSisaTenor" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Jatuh Tempo" ColumnName="colJatuhTempo" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="3"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Kolektibilitas" ColumnName="colKolektibilitas" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="History Kolek" ColumnName="colHistoryKolek" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Aktual Overdue" ColumnName="colAktualOverDue" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EditModeCommandLayoutItem HorizontalAlign="Right"></dx:EditModeCommandLayoutItem>
                                                                    </Items>
                                                                    <SettingsItemCaptions Location="Left"/>
                                                                </dx:GridViewLayoutGroup>
                                                            </Items>
                                                        </EditFormLayoutProperties>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="Plafon" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                                        <dx:ASPxSummaryItem FieldName="BakiDebet" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                                        <dx:ASPxSummaryItem FieldName="Angsuran" SummaryType="Sum" DisplayFormat="'Rp '#,0.00"/>
                                                    </TotalSummary>
                                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true" Footer-ForeColor="#255658" Footer-Font-Size="10" Footer-Font-Names="Calibri">
                                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Application Detail">
                                    <Items>
                                        <dx:LayoutItem ShowCaption="false">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxVerticalGrid 
                                                        runat="server" 
                                                        ID="vcDetail" 
                                                        ClientInstanceName="vcDetail" 
                                                        Font-Names="Calibri" Font-Size="Small"
                                                        Width="100%" OnDataBinding="vcDetail_DataBinding" OnCustomCallback="vcDetail_CustomCallback">
                                                        <Rows>
                                                            <dx:VerticalGridCategoryRow Caption="Data Client">
                                                                <CategoryStyle Font-Size="12" Font-Underline="true"></CategoryStyle>
                                                                <Rows>
                                                                    <dx:VerticalGridDataRow FieldName="CLIENT" Caption="CIF">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="NAMA_CUST" Caption="Nama">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="GENDER" Caption="Gender">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="NO_KTP" Caption="KTP">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="NPWP" Caption="NPWP">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="KOTA_LAHIR" Caption="Kota Kelahiran">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDateRow FieldName="TGLLAHIR" Caption="Tanggal Lahir" PropertiesDateEdit-DisplayFormatString="dd MMMM yyyy">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDateRow>
                                                                    <dx:VerticalGridDataRow FieldName="IBUKANDUNG" Caption="Nama Ibu Kandung">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="ALAMAT_KTP" Caption="Alamat">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="KOTA" Caption="Kota">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="MOBILEPHONE" Caption="HP">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="NAMA_PERUSAHAAN" Caption="Nama Perusahaan">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="PEKERJAAN" Caption="Pekerjaan">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridSpinEditRow FieldName="GAJI" Caption="Gaji" PropertiesSpinEdit-DisplayFormatString="#,0.00" RecordStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                        <RecordStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridSpinEditRow>
                                                                    <dx:VerticalGridTextRow FieldName="LAMA_TAHUN_KERJA" Caption="Lama Bekerja" PropertiesTextEdit-DisplayFormatString="{0} Tahun" RecordStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridTextRow>
                                                                </Rows>
                                                            </dx:VerticalGridCategoryRow>
                                                            <dx:VerticalGridCategoryRow Caption="Data Pasangan">
                                                                <CategoryStyle Font-Size="12" Font-Underline="true"></CategoryStyle>
                                                                <Rows>
                                                                    <dx:VerticalGridDataRow FieldName="NAMA_SPOUSE" Caption="Nama Pasangan">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="KOTA_LAHIR_SPOUSE" Caption="Kota Kelahiran Pasangan">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDateRow FieldName="TGL_LAHIR_ISTRI" Caption="Tanggal Kelahiran Pasangan" PropertiesDateEdit-DisplayFormatString="dd MMMM yyyy">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDateRow>
                                                                    <dx:VerticalGridDataRow FieldName="ALAMAT_KTP_ISTRI" Caption="Alamat Pasangan">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="KOTA_KTP_ISTRI" Caption="Kota">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="HP_SPOUSE" Caption="HP">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                </Rows>
                                                            </dx:VerticalGridCategoryRow>
                                                            <dx:VerticalGridCategoryRow Caption="Emergency Contact">
                                                                <CategoryStyle Font-Size="12" Font-Underline="true"></CategoryStyle>
                                                                <Rows>
                                                                    <dx:VerticalGridDataRow FieldName="Nama_Contact" Caption="Nama">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="Mobilephone_contact" Caption="HP">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="Hubungan" Caption="Hubungan">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                </Rows>
                                                            </dx:VerticalGridCategoryRow>
                                                            <dx:VerticalGridCategoryRow Caption="Data Penjamin">
                                                                <CategoryStyle Font-Size="12" Font-Underline="true"></CategoryStyle>
                                                                <Rows>
                                                                    <dx:VerticalGridDataRow FieldName="INJAMIN" Caption="Nama">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="INJAMADD1" Caption="Alamat">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="INJAMKTP" Caption="No. Ktp">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="INJAMTELP" Caption="No. Hp">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="INJAMSTAT" Caption="Status">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                    <dx:VerticalGridDataRow FieldName="INJAMHUB" Caption="Relation">
                                                                        <HeaderStyle Font-Bold="true"/>
                                                                    </dx:VerticalGridDataRow>
                                                                </Rows>
                                                            </dx:VerticalGridCategoryRow>
                                                        </Rows>
                                                    </dx:ASPxVerticalGrid>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
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
            <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>
            <dx:LayoutItem ShowCaption="false" Caption="" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Text="Save" AutoPostBack="false" Theme="Glass">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVE_CONFIRM;' + 'SAVE_CONFIRM'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem ShowCaption="false" Caption="" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnBack" ClientInstanceName="btnBack" Text="Back" AutoPostBack="true" Theme="Glass" PostBackUrl="~/Transactions/CreditProcess/UpdateSLIK/UpdateSLIKMaint.aspx"></dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
    <asp:SqlDataSource ID="sdsApplication" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="select c.C_NAME, a.APPLICNO, b.LSAGREE, a.NAME, 
            case a.MODULE
	            when '1' then 'Leasing'
	            when '2' then 'Consumer Finance'
	            when '3' then 'IMBT'
	            when '4' then 'Murabahah'
	            when '5' then 'Factoring'
	            when '6' then 'OPL'
	            when '7' then 'Hawalah'
	            else 'Other'
            end Module, b.CONTRACT_STATUS, a.APPLICDT, b.DISBURSEDT
            from LS_APPLICATION a with(NOLOCK) left join LS_AGREEMENT b with(NOLOCK) on a.APPLICNO = b.APPLICNO
            inner join sys_company c on a.C_CODE = c.C_CODE
            WHERE a.APPSTATUS NOT IN ('CANCEL', 'REJECT') ORDER BY a.APPLICDT DESC"></asp:SqlDataSource>
</asp:Content>
