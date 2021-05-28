<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ListJaminanEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.SupplyChainFinancing.ListJaminan.ListJaminanEntry" %>
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
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "SAVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
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
            }
        }
        function OnLuAppNoChanged(luAppNo) {
            var grid = luAppNo.GetGridView();
            if (luAppNo.GetText() != "") {
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
        function gvDetail_EndCallback(s, e) {
            if (s.cpCmd == "INSERT" || s.cpCmd == "UPDATE" || s.cpCmd == "DELETE") {
                seTotalJaminan.SetText("0");

                seTotalJaminan.SetText(s.cpTotal);
                seTotalJaminan.GetInputElement().readOnly = true;
                calculationTotalJaminan();
                s.cpCmd = "";
            }
        }
        function calculationGrid() {
            var vQty = parseFloat(0.0);
            if (gvDetail.GetEditor("Qty").GetValue() != null &&
                gvDetail.GetEditor("Qty").GetValue().toString() != "" &&
                gvDetail.GetEditor("Qty").GetValue().toString().length != 0) {
                vQty = parseFloat(gvDetail.GetEditor("Qty").GetValue().toString());
            }
            var vDBP = parseFloat(0.0);
            if (gvDetail.GetEditor("DBP").GetValue() != null &&
                gvDetail.GetEditor("DBP").GetValue().toString() != "" &&
                gvDetail.GetEditor("DBP").GetValue().toString().length != 0) {
                vDBP = parseFloat(gvDetail.GetEditor("DBP").GetValue().toString());
            }
            gvDetail.GetEditor("DBPSubTotal").SetValue(vQty * vDBP);
        }
        function calculationTotalJaminan() {
        }
        function OnItemDescriptionValueChanged(s, e) {
            gvDetail.GetEditor("ItemCategory").SetValue(null);
            gvDetail.GetEditor("ItemBrand").SetValue(null);
            gvDetail.GetEditor("UOM").SetValue(null);
            gvDetail.GetEditor("Qty").SetValue(0);
            gvDetail.GetEditor("DBP").SetValue(0);
            gvDetail.GetEditor("RBP").SetValue(0);
            gvDetail.GetEditor("DBPSubTotal").SetValue(0);
        }
        function OnFileUploadComplete(s, e) {
            gvDetail.PerformCallback();
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
            <dx:LayoutGroup Name="LayoutGroupListJaminanEntry" ShowCaption="True" Caption="List Jaminan Entry" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:EmptyLayoutItem Width="30%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="40%" HorizontalAlign="Center">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="lblHeader" ClientInstanceName="lblHeader" Text="List Jaminan Entry" Font-Size="24" Font-Bold="true" ForeColor="#009933"></dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="30%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Post to SMILE" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxCheckBox runat="server" ID="chkSubmit" ClientInstanceName="chkSubmit"></dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Leased Asset No." Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDocNo" ClientInstanceName="txtDocNo" ClientEnabled="false"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Created Date" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deDocDate" ClientInstanceName="deDocDate" ClientEnabled="false" DisplayFormatString="dd/MM/yyyy"></dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="SMILE Application No." Width="30%">
                            <CaptionSettings Location="Left"/>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridLookup runat="server" ID="luAppNo" ClientInstanceName="luAppNo" KeyFieldName="APPLICNO" DataSourceID="sdsApplication" DisplayFormatString="{1}" TextFormatString="{1}" MultiTextSeparator=";" Width="100%">
                                        <ClientSideEvents Init="function(s, e) { OnLuAppNoChanged(s); }" ValueChanged="function(s, e) { OnLuAppNoChanged(s); }" Validation="OnAppNoValidation"/>
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
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Client" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDebitur" ClientInstanceName="txtDebitur" ClientEnabled="false"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Leased Asset Description" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmAssetDesc" ClientInstanceName="mmAssetDesc" Height="75px"></dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%" Visible="false"></dx:EmptyLayoutItem>
                    <dx:LayoutItem Caption="Import detail" Width="30%" Visible="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxUploadControl ID="Upload" runat="server" ShowUploadButton="True" OnFileUploadComplete="Upload_FileUploadComplete">
                                    <ValidationSettings AllowedFileExtensions=".xls,.xlsx">
                                    </ValidationSettings>
                                    <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />
                                </dx:ASPxUploadControl>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:TabbedLayoutGroup Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Height="100px" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                        <Items>
                            <dx:LayoutGroup Caption="Detail">
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
                                                OnCustomColumnDisplayText="gvDetail_CustomColumnDisplayText" OnAutoFilterCellEditorInitialize="gvDetail_AutoFilterCellEditorInitialize">
                                                <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                <ClientSideEvents EndCallback="gvDetail_EndCallback" />
                                                <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" ShowFooter="true"/>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                <SettingsSearchPanel Visible="false" />
                                                <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom"></SettingsEditing>
                                                <SettingsCommandButton>
                                                    <NewButton ButtonType="Button" Text="Add" Styles-Style-Width="75px"></NewButton>
                                                    <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                    <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                    <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                    <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                </SettingsCommandButton>
                                                <Columns>
                                                    <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" Width="3%">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Name="colNo" Caption="No." ReadOnly="True" UnboundType="String" Width="3%">
                                                        <HeaderStyle Font-Bold="true"/>
                                                        <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                            AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                            AllowSort="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colItemDesc" Caption="Item Descirption" FieldName="ItemDesc" ShowInCustomizationForm="true">
                                                        <HeaderStyle Font-Bold="true" />
                                                        <PropertiesTextEdit>
                                                            <ClientSideEvents ValueChanged="OnItemDescriptionValueChanged" />
                                                        </PropertiesTextEdit>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colItemCategory" Caption="Category" FieldName="ItemCategory" ShowInCustomizationForm="true">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colItemBrand" Caption="Brand" FieldName="ItemBrand" ShowInCustomizationForm="true">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colUOM" Caption="UoM (Satuan)" FieldName="UOM" ShowInCustomizationForm="true">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataSpinEditColumn Name="colQty" Caption="Qty" FieldName="Qty" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}">
                                                        <HeaderStyle Font-Bold="true" />
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false">
                                                            <ClientSideEvents ValueChanged="calculationGrid" />
                                                        </PropertiesSpinEdit>
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataSpinEditColumn Name="colDBP" Caption="DBP (IDR)" FieldName="DBP" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}">
                                                        <HeaderStyle Font-Bold="true" />
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false">
                                                            <ClientSideEvents ValueChanged="calculationGrid" />
                                                        </PropertiesSpinEdit>
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataSpinEditColumn Name="colRBP" Caption="RBP (IDR)" FieldName="RBP" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}">
                                                        <HeaderStyle Font-Bold="true" />
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false">
                                                        </PropertiesSpinEdit>
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataSpinEditColumn Name="colSubTotal" Caption="Sub Total" FieldName="DBPSubTotal" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}">
                                                        <HeaderStyle Font-Bold="true" />
                                                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" AllowMouseWheel="false">
                                                        </PropertiesSpinEdit>
                                                    </dx:GridViewDataSpinEditColumn>
                                                </Columns>
                                                <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true" Footer-ForeColor="#255658" Footer-Font-Size="Larger">
                                                    <AlternatingRow Enabled="True"></AlternatingRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                                    <dx:LayoutItem Caption="Total Jaminan (IDR)" Width="30%">
                                        <CaptionStyle Font-Bold="true"></CaptionStyle>
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxSpinEdit runat="server" ID="seTotalJaminan" ClientInstanceName="seTotalJaminan" MinValue="0" MaxValue="999999999999999" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" Font-Bold="true" ForeColor="#666666">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxSpinEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                                    <dx:LayoutItem Caption="Total Pembiayaan (IDR)" Width="30%">
                                        <CaptionStyle Font-Bold="true"></CaptionStyle>
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxSpinEdit runat="server" ID="seTotalPembiayaan" ClientInstanceName="seTotalPembiayaan" MinValue="0" MaxValue="999999999999999" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" Font-Bold="true" ForeColor="#666666">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True"/>
                                                    </ValidationSettings>
                                                </dx:ASPxSpinEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </items>
                            </dx:LayoutGroup>
                        </Items>
                    </dx:TabbedLayoutGroup>
                </Items>
            </dx:LayoutGroup>
            <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>
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
                        <dx:ASPxButton runat="server" ID="btnBack" ClientInstanceName="btnBack" Text="Back" AutoPostBack="false" UseSubmitBehavior="false" Width="100%" OnClick="btnBack_Click"></dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
    <%--<asp:SqlDataSource ID="sdsApplication" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
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
            WHERE a.APPSTATUS NOT IN ('CANCEL', 'REJECT') ORDER BY a.APPLICDT DESC"></asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="sdsApplication" runat="server" ConnectionString="Data Source=192.168.1.10\MGUISVR;Initial Catalog=IFINANCING_GOLIVE; Persist Security Info=True;User ID=mncl;Password=Mncleasing123"
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
