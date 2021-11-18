<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="MitraEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.Mitra.MitraEntry" %>

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
                case "REFRESH":
                    gvUploadDoc.Refresh();
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
                case "SUBMITRA":
                    var myLayout = ASPxFormLayout.GetItemByName("liMitra");
                    if (cplMain.cpVisible != null)
                        myLayout.SetVisible(cplMain.cpVisible);
                    break;
                case "DOWNLOAD":
                    break;
                case "PIC":
                    if (cplMain.cplblBranch.length > 0)
                    {
                        txtMitraCode.SetValue(cplMain.cplblBranch);
                    }
                    break;
            }
            cplMain.cpCallbackParam = null;
        }

        var command = "";
        function OnBeginCallback(s, e)
        {
            command = e.command;
        }
        function OnluAgreementChanged(s, e) {
            var grid = luAgreement.GetGridView();
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'DEBITUR;DEBITUR', OnGetSelectedFieldValues);
        }
        function OnGetSelectedFieldValues(selectedValues) {
            txtDebitur.SetValue(selectedValues[1]);
        }
        function OnrbtSubMitraChanged(rbtSubMitra)
        {
            if (rbtSubMitra.GetValue() != "") {
                cplMain.PerformCallback("SUBMITRA;" + rbtSubMitra.GetValue().toString());
                }
        }
        function OnBtnUploadClick() {
            CtlUpload.Upload();
        }
        function onFilesUploadComplete()
        {
            cplMain.PerformCallback("REFRESH; REFRESH");
        }
        var FocusedCell;
        function onCellClick(columnName, cellValue, htmlId)
        {
            if (FocusedCell != null) {
                FocusedCell.style.color = '';
                FocusedCell.style.border = '';
            }
            if (columnName == 'colDocPath') {
                FocusedCell = document.getElementById(htmlId);
                FocusedCell.style.color = 'Red';
                FocusedCell.style.border = '1px solid Red';
                cplMain.PerformCallback("DOWNLOAD;" + columnName + ";" + cellValue);
             }
        }
        var lastProvinsi = null;
        function OncbProvinsiChanged(cbProvinsi) {
            if (cbProvinsi.GetText() != "") {
                if (cbKotaKabupaten.InCallback())
                    lastProvinsi = cbProvinsi.GetValue().toString();
                else
                    cbKotaKabupaten.PerformCallback(cbProvinsi.GetValue().toString());
            }
        }
        function OncbKotaKabupaten_EndCallback(s, e) {
            if (lastProvinsi) {
                cbKotaKabupaten.PerformCallback(lastProvinsi);
                lastProvinsi = null;
            }
        }
        function OnSourceChanged() {
            cplMain.PerformCallback("PIC;");
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True" Theme="MetropolisBlue" EnableCallbackAnimation="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="we need your confirmation.." AllowDragging="False" PopupAnimationType="Fade" EnableViewState="False" CloseAction="CloseButton">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2" Width="100%">
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
    <dx:ASPxFormLayout ID="ASPxFormLayout" ClientInstanceName="ASPxFormLayout" runat="server" Theme="Glass">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup Name="LayoutGroupMitraEntry" ShowCaption="True" Caption="Mitra Maintenance Entry" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                    <Items>
                        <dx:LayoutItem ShowCaption="true" Caption="PIC MNC Leasing" Width="40%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox runat="server" ID="cbPIC" ClientInstanceName="cbPIC" DataSourceID="sdsAO" ValueField="NAME" ValueType="System.String" DisplayFormatString="{1}" TextFormatString="{1}" NullText="-- Select --">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="NIK" FieldName="NIK" Name="colNIK" Width="100px" />
                                            <dx:ListBoxColumn Caption="Nama" FieldName="NAME" Name="colNAME" Width="100px" />
                                        </Columns>
                                        <ClientSideEvents SelectedIndexChanged="OnSourceChanged"/>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Mitra Code" Width="25%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtMitraCode" ClientInstanceName="txtMitraCode" NullText="..." Width="100%">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Cabang" Width="25%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtBranch" ClientInstanceName="txtMitraCode" NullText="..." Width="100%">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Active?" Width="25%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxCheckBox runat="server" ID="chkActive" ClientInstanceName="chkActive" Width="100%" ValueChecked="T" ValueUnchecked="F" ValueType="System.String">
                                    </dx:ASPxCheckBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="25%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Jenis Mitra" Width="50%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList runat="server" ID="rbtjenisMitra" ClientInstanceName="rbtjenisMitra" Border-BorderStyle="None" RepeatColumns="3" ValueType="System.String" Font-Size="8">
                                        <Border BorderStyle="Solid"/>
                                        <Items>
                                            <dx:ListEditItem Text="KBIH" Value="KBIH" />
                                            <dx:ListEditItem Text="BPS" Value="BPS" />
                                            <dx:ListEditItem Text="BADAN USAHA" Value="BADAN USAHA" />
                                            <dx:ListEditItem Text="KARYAWAN INTERNAL" Value="KARYAWAN INTERNAL" />
                                            <dx:ListEditItem Text="PERSEORANGAN - KARYAWAN" Value="PERSEORANGAN - KARYAWAN" />
                                            <dx:ListEditItem Text="PERSEORANGAN - NON KARYAWAN" Value="PERSEORANGAN - NON KARYAWAN" />
                                            <dx:ListEditItem Text="MEMBER GET MEMBER" Value="MEMBER GET MEMBER" />
                                            <dx:ListEditItem Text="MAGANG" Value="MAGANG" />
                                            <dx:ListEditItem Text="AO" Value="AO" />
                                            <dx:ListEditItem Text="LAIN-NYA" Value="LAIN-NYA" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Tipe Mitra" Width="25%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox runat="server" ID="cbTipeMitra" ClientInstanceName="cbTipeMitra" NullText="-- Select --">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                            <Items>
                                                <dx:ListEditItem Text="UTAMA" Value="UTAMA"/>
                                                <dx:ListEditItem Text="MADYA" Value="MADYA"/>
                                                <dx:ListEditItem Text="PRATAMA" Value="PRATAMA"/>
                                            </Items>
                                        </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Nama Lengkap" Width="40%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtNamaLengkap" ClientInstanceName="txtNamaLengkap" Width="100%">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Tempat Lahir" Width="40%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtTempatLahir" ClientInstanceName="txtTempatLahir" Width="100%">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="Tanggal Lahir" Width="25%">
                            <CaptionSettings Location="Left"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="deTglLahir" ClientInstanceName="deTglLahir" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" NullText="-- Select --" HelpText="please fill your birthday.">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="Alamat Lengkap" Width="50%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxMemo runat="server" ID="mmAddress" ClientInstanceName="mmAddress" Height="100px">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxMemo>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Provinsi" Width="25%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox runat="server" ID="cbProvinsi" ClientInstanceName="cbProvinsi" NullText="-- Select --" DropDownStyle="DropDownList" OnDataBinding="cbProvinsi_DataBinding" TextField="nama" ValueField="id" ValueType="System.String">
                                        <ClientSideEvents Init="function(s, e) { OncbProvinsiChanged(s); }" SelectedIndexChanged="function(s, e) { OncbProvinsiChanged(s); }" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Provinsi" FieldName="nama" Name="colProvinsi" Width="100px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Kota/Kabupaten" Width="25%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox runat="server" ID="cbKotaKabupaten" ClientInstanceName="cbKotaKabupaten" NullText="-- Select --" OnDataBinding="cbKotaKabupaten_DataBinding" OnCallback="cbKotaKabupaten_Callback" TextField="nama" ValueField="id" ValueType="System.String">
                                        <ClientSideEvents EndCallback="OncbKotaKabupaten_EndCallback"/>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Kota/Kabupaten" FieldName="nama" Name="colKotaKabupaten" Width="100px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Email" Width="40%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtEmail" ClientInstanceName="txtEmail" Width="100%">
                                        <ValidationSettings SetFocusOnError="true" ValidationGroup="EditForm" Display="Dynamic" ErrorTextPosition="Right" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle Font-Size="8"></ErrorFrameStyle>
                                            <RegularExpression ErrorText="Invalid Email Format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*.\w+([-.]\w+)*" />
                                        </ValidationSettings>
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Contact Person" Width="40%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtContactPerson" ClientInstanceName="txtContactPerson" Width="100%">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="No Telepon" Width="40%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtNoTlp" ClientInstanceName="txtNoTlp" Width="100%">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="No HP" Width="40%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtHp" ClientInstanceName="txtHp" Width="100%">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="No Whatsapp" Width="40%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtNoWhatsApp" ClientInstanceName="txtNoWhatsApp" Width="100%">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="NPWP (Individu/Badan Usaha)" Width="40%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtNPWP" ClientInstanceName="txtNPWP" Width="100%">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="No Akta (Badan Usaha)" Width="40%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtAktePendirian" ClientInstanceName="txtAktePendirian" Width="100%">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="true" Caption="Memiliki Induk Mitra?" Width="50%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList runat="server" ID="rbtSubMitra" ClientInstanceName="rbtSubMitra" Border-BorderStyle="None" RepeatColumns="0" ValueType="System.String" Font-Size="8">
                                        <ClientSideEvents Init="function(s, e) { OnrbtSubMitraChanged(s); }" SelectedIndexChanged="function(s, e) { OnrbtSubMitraChanged(s); }" />
                                        <Border BorderStyle="None"/>
                                        <Items>
                                            <dx:ListEditItem Text="YA" Value="YA" />
                                            <dx:ListEditItem Text="TIDAK" Value="TIDAK" Selected="true"/>
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem Name="liMitra" ClientVisible="false" ShowCaption="true" Caption="Mitra" Width="40%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridLookup runat="server" ID="luMitra" ClientInstanceName="luMitra" KeyFieldName="Code" DataSourceID="sdsMitra" DisplayFormatString="{1}" TextFormatString="{0};{1}" MultiTextSeparator=";" Width="100%">
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
                        <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="Latar Belakang/Profile" Width="50%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxMemo runat="server" ID="mmProfile" ClientInstanceName="mmProfile" Height="100px">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    </dx:ASPxMemo>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                        <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                        <dx:TabbedLayoutGroup Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Height="100px" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                            <Items>
                                <dx:LayoutGroup Caption="Detail Bank Account">
                                    <items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxGridView
                                                    runat="server"
                                                    ID="gvBankDetail"
                                                    ClientInstanceName="gvBankDetail"
                                                    Width="100%"
                                                    KeyFieldName="MBankKey"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Glass"
                                                    Font-Names="Calibri"
                                                    OnInit="gvBankDetail_Init"
                                                    OnInitNewRow="gvBankDetail_InitNewRow"
                                                    OnRowInserting="gvBankDetail_RowInserting"
                                                    OnRowUpdating="gvBankDetail_RowUpdating"
                                                    OnRowDeleting="gvBankDetail_RowDeleting"
                                                    OnDataBinding="gvBankDetail_DataBinding" 
                                                    OnCustomColumnDisplayText="gvBankDetail_CustomColumnDisplayText">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" ShowFooter="true"/>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom"></SettingsEditing>
                                                    <SettingsCommandButton>
                                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px"></NewButton>
                                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                    </SettingsCommandButton>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" Width="5%" VisibleIndex="1">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colNo" Caption="No" ReadOnly="True" UnboundType="String" Width="5%" VisibleIndex="1">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colBankName" Caption="Nama Bank" FieldName="BankName" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="2">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colBankBranch" Caption="Cabang" FieldName="BankBranch" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="3">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colBankAccNo" Caption="Account No" FieldName="BankAccNo" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="4">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colBankAccName" Caption="A/N" FieldName="BankAccName" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="5">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colSeq" Caption="Seq" FieldName="Seq" ReadOnly="True" ShowInCustomizationForm="true" Visible="false">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true" Footer-ForeColor="#255658" Footer-Font-Size="Larger">
                                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Name="lgUploadTab" Caption="Upload Document" ClientVisible="false">
                                    <Items>
                                        <dx:LayoutGroup ShowCaption="True" Caption="Upload Control" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                                            <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#edf3f4" Caption-Font-Bold="true"></GroupBoxStyle>
                                            <Items>
                                                <dx:LayoutItem ShowCaption="False" Width="10%">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                             <dx:ASPxUploadControl ID="CtlUpload" runat="server" ClientInstanceName="CtlUpload" FileUploadMode="OnPageLoad" UploadMode="Advanced" Width="365px" OnFilesUploadComplete="CtlUpload_FilesUploadComplete" ShowProgressPanel="true" BrowseButton-Text="Browse" ShowTextBox="true">
                                                                <AdvancedModeSettings EnableMultiSelect="true" EnableFileList="True" EnableDragAndDrop="True"></AdvancedModeSettings>
                                                                <ValidationSettings AllowedFileExtensions=".txt,.jpg,.jpe,.jpeg,.doc,.docx,.pdf,.xls,.xlsx" MaxFileSize="4000000" ErrorStyle-BackColor="Red" ShowErrors="true">
                                                                    <ErrorStyle BackColor="Red"></ErrorStyle>
                                                                </ValidationSettings>
                                                                 <ClientSideEvents FilesUploadComplete="onFilesUploadComplete"/>
                                                                <AdvancedModeSettings EnableDragAndDrop="True">
                                                                </AdvancedModeSettings>
                                                            </dx:ASPxUploadControl>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:EmptyLayoutItem Width="90%"></dx:EmptyLayoutItem>
                                                <dx:LayoutItem ShowCaption="False" Width="10%">
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
                                        <dx:LayoutItem ShowCaption="False" Width="100%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxGridView
                                                    runat="server"
                                                    ID="gvUploadDoc"
                                                    ClientInstanceName="gvUploadDoc"
                                                    Width="100%"
                                                    KeyFieldName="DtlUploadKey"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Glass"
                                                    Font-Names="Calibri"
                                                    OnDataBinding="gvUploadDoc_DataBinding" OnCustomColumnDisplayText="gvUploadDoc_CustomColumnDisplayText" OnCustomCallback="gvUploadDoc_CustomCallback" OnHtmlDataCellPrepared="gvUploadDoc_HtmlDataCellPrepared" OnCustomButtonCallback="gvUploadDoc_CustomButtonCallback">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" ShowFooter="true"/>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" Visible="true"></SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Name="colNo" Caption="No" ReadOnly="True" UnboundType="String" Width="5%" VisibleIndex="0">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colRemark" FieldName="Remark" Caption="Original file name" ReadOnly="True" UnboundType="String" Width="40%" VisibleIndex="1">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colDocPath" Caption="File" FieldName="DocPath" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="2">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Link"  Caption="">
                                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="GridbtnDownload" Text=" ">
                                                                    <Image Height="20px" Width="20px" Url="../../../Content/Images/download.png" ToolTip="Click here to download file."></Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <Styles AdaptiveDetailButtonWidth="22">
                                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:TabbedLayoutGroup>
                        <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
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
                        <dx:ASPxButton runat="server" ID="btnBack" ClientInstanceName="btnBack" Text="Back" AutoPostBack="true" Theme="Glass" PostBackUrl="~/Transactions/Syariah/Mitra/MitraMaint.aspx"></dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem> 
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
    <asp:SqlDataSource ID="sdsMitra" runat="server" ConnectionString="<%$ ConnectionStrings:SqlLocalConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="SELECT MCode as Code, Nama FROM [dbo].[Mitra] WHERE IsActive='T'">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAO" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="select distinct a.CODE NIK, a.DESCS NAME
            from SYS_TBLEMPLOYEE a
            inner join sys_tblemployee_position ep with(NOLOCK) on a.CODE = ep.employee_code and a.ISACTIVE = '1'
            inner join sys_tblposition p with(NOLOCK) on ep.POSITION = p.CODE
            where   p.DESCR like '%account officer%' or p.DESCR like '%marketing%' or p.DESCR like '%account manager%'
            ">
    </asp:SqlDataSource>
</asp:Content>
