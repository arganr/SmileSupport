<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PolisAsuransiFAEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Insurance.PolisAsuransiFAEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        var vErrorMsg = "";
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "ON_ASSETNO_VALIDATION":
                    if (cplMain.cpStrErrorMsg.length > 0)
                    {
                        apcalert.SetContentHtml(cplMain.cpStrErrorMsg);
                        apcalert.Show();
                        {
                            luAssetNo.SetText('');
                            txtPlat.SetText('');
                            txtAssetDesc.SetText('');
                            txtNoRangka.SetText('');
                            txtNoMesin.SetText('');
                        }
                        break;
                    }
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
            }
            cplMain.cpCallbackParam = null;
        }
        function OnAssetNoChanged(s, e) {
            var grid = luAssetNo.GetGridView();
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'PLAT_NO;AST_NAME;SERI_NO;ENGINE_NO', OnGetSelectedFieldValues);
        }
        function OnGetSelectedFieldValues(selectedValues) {
            txtPlat.SetValue(selectedValues[0]);
            txtAssetDesc.SetValue(selectedValues[1]);
            txtNoRangka.SetValue(selectedValues[2]);
            txtNoMesin.SetValue(selectedValues[3]);
        }
        function OnAssetNoValidation(s, e) {
            cplMain.PerformCallback('ON_ASSETNO_VALIDATION;ON_ASSETNO_VALIDATION');
        }
        function dtStartEndDateValidation(s, e) {
            if (!e.isValid)
                return;
            var grd = ASPxClientGridView.Cast(gvDetail);
            var startDate = grd.GetEditValue('colStartDate');
            var endDate = grd.GetEditValue('colEndDate');
            console.log('StartDate: ' + startDate);
            console.log('EndDate: ' + endDate);
            e.isValid = startDate == null || endDate == null || startDate < endDate;
            e.errorText = "The Start Date must be greater than End Date.";
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
            <dx:LayoutGroup Name="LayoutGroupPAFAEntry" ShowCaption="True" Caption="Polis Asuransi Fixed Asset Entry" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                    <Items>
                        <dx:LayoutItem ShowCaption="True" Caption="Asset No.">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridLookup
                                        runat="server"
                                        ID="luAssetNo"
                                        ClientInstanceName="luAssetNo" 
                                        DataSourceID="sdsAsset"
                                        AutoGenerateColumns="False"
                                        DisplayFormatString="{0}"
                                        TextFormatString="{0}"
                                        KeyFieldName="AST_CODE"
                                        SelectionMode="Single"
                                        Width="20%" GridViewProperties-EnablePagingCallbackAnimation="true" AnimationType="Slide">
                                        <ClientSideEvents ValueChanged="OnAssetNoChanged" Validation="OnAssetNoValidation"/>
                                        <GridViewProperties>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                            <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                            <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True" />
                                        </GridViewProperties>
                                        <Columns>
                                            <dx:GridViewDataColumn Caption="Asset No." FieldName="AST_CODE" ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Asset Desc" FieldName="AST_NAME" ShowInCustomizationForm="True" VisibleIndex="2">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="License Plat" FieldName="PLAT_NO" ShowInCustomizationForm="True" VisibleIndex="3">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Serial" FieldName="SERI_NO" ShowInCustomizationForm="True" VisibleIndex="4">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Engine No." FieldName="ENGINE_NO" ShowInCustomizationForm="True" VisibleIndex="5">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Insurance" FieldName="INSURANCE" ShowInCustomizationForm="true" VisibleIndex="6">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Insurance No." FieldName="INS_NO" ShowInCustomizationForm="true" VisibleIndex="7">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataDateColumn Caption="Due Police" FieldName="DUE_POLICE" ShowInCustomizationForm="true" VisibleIndex="8" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                                            </dx:GridViewDataDateColumn>
                                        </Columns>
                                        <GridViewStyles AdaptiveDetailButtonWidth="22">
                                        </GridViewStyles>
                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorTextPosition="Bottom" ErrorFrameStyle-Font-Size="8">
                                            <RequiredField ErrorText="* Value can't be empty." IsRequired="True"/>
                                        </ValidationSettings>
                                    </dx:ASPxGridLookup>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="Entry Date">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxDateEdit runat="server" ID="deDocDate" ClientInstanceName="deDocDate" DisplayFormatString="dd/MM/yyyy" ReadOnly="true" Width="20%" BackColor="Transparent"></dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="Plat License">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtPlat" ClientInstanceName="txtPlat" ReadOnly="true" Width="20%" BackColor="Transparent"></dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="Asset Description">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtAssetDesc" ClientInstanceName="txtAssetDesc" ReadOnly="true" Width="20%" BackColor="Transparent"></dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="No. Rangka">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtNoRangka" ClientInstanceName="txtNoRangka" ReadOnly="true" Width="20%" BackColor="Transparent"></dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="No. Mesin">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtNoMesin" ClientInstanceName="txtNoMesin" ReadOnly="true" Width="20%" BackColor="Transparent"></dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                        <dx:TabbedLayoutGroup Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Height="100px" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                            <Items>
                                <dx:LayoutGroup Caption="Insurance Detail">
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
                                                    OnCustomColumnDisplayText="gvDetail_CustomColumnDisplayText" OnCustomButtonCallback="gvDetail_CustomButtonCallback" OnCellEditorInitialize="gvDetail_CellEditorInitialize" OnCustomButtonInitialize="gvDetail_CustomButtonInitialize" OnCommandButtonInitialize="gvDetail_CommandButtonInitialize">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" ShowFooter="true"/>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="100" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsEditing Mode="EditForm" UseFormLayout="true" NewItemRowPosition="Bottom"></SettingsEditing>
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
                                                        <dx:GridViewDataComboBoxColumn Name="colMaskapai" FieldName="Maskapai" ShowInCustomizationForm="True" Caption="Maskapai">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <PropertiesComboBox EnableCallbackMode="true" ClientInstanceName="colMaskapai" DropDownRows="10" IncrementalFilteringDelay="500" ItemStyle-Wrap="True" IncrementalFilteringMode="Contains" DisplayFormatString="{0}" TextFormatString="{0}" DropDownStyle="DropDownList" ValueField="NamaMaskapai" TextField="NamaMaskapai">
                                                                <ItemStyle Wrap="True"></ItemStyle>
                                                                <Columns>
                                                                    <dx:ListBoxColumn FieldName="NamaMaskapai" Caption="Maskapai" />
                                                                </Columns>
                                                                <ValidationSettings ValidateOnLeave="true" ValidationGroup="ValidationSave" Display="Dynamic" ErrorDisplayMode="ImageWithText">
                                                                    <RequiredField ErrorText="* Value can't be empty." IsRequired="true" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colPolisNo" Caption="Polis No." FieldName="NoPolis" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="6">
                                                            <HeaderStyle Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataDateColumn Name="colStartDate" Caption="Start Date" FieldName="StartDate" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="7">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <PropertiesDateEdit>  
                                                              <ClientSideEvents Validation="dtStartEndDateValidation" />  
                                                              <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="true" ValidateOnLeave="true" />  
                                                           </PropertiesDateEdit>  
                                                           <Settings SortMode="Value" /> 
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn Name="colEndDate" Caption="End Date" FieldName="EndDate" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="8">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <PropertiesDateEdit>  
                                                              <ClientSideEvents Validation="dtStartEndDateValidation" />  
                                                              <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="true" ValidateOnLeave="true" />  
                                                           </PropertiesDateEdit>  
                                                           <Settings SortMode="Value" /> 
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colCoverage" FieldName="Coverage" ShowInCustomizationForm="True" Caption="Coverage">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <PropertiesComboBox EnableCallbackMode="true" ClientInstanceName="colMaskapai" DropDownRows="10" IncrementalFilteringDelay="500" ItemStyle-Wrap="True" IncrementalFilteringMode="Contains" DisplayFormatString="{0}" TextFormatString="{0}" DropDownStyle="DropDownList" ValueField="CoverageDesc" TextField="CoverageDesc">
                                                                <ItemStyle Wrap="True"></ItemStyle>
                                                                <Columns>
                                                                    <dx:ListBoxColumn FieldName="CoverageDesc" Caption="Coverage" />
                                                                </Columns>
                                                                <ValidationSettings ValidateOnLeave="true" ValidationGroup="ValidationSave" Display="Dynamic" ErrorDisplayMode="ImageWithText">
                                                                    <RequiredField ErrorText="* Value can't be empty." IsRequired="true" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                    </Columns>
                                                        <EditFormLayoutProperties >
                                                            <Items>
                                                                <dx:GridViewLayoutGroup GroupBoxDecoration="None" Width="100%" ColCount="3">
                                                                    <Items>
                                                                        <dx:GridViewColumnLayoutItem Caption="Maskapai" ColumnName="colMaskapai" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Polis No." ColumnName="colPolisNo" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Start Date" ColumnName="colStartDate" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="End Date" ColumnName="colEndDate" RequiredMarkDisplayMode="Required" Width="350px"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:GridViewColumnLayoutItem Caption="Coverage" ColumnName="colCoverage" RequiredMarkDisplayMode="Required" Width="350px" RowSpan="4"></dx:GridViewColumnLayoutItem>
                                                                        <dx:EmptyLayoutItem  ColSpan="2"></dx:EmptyLayoutItem>
                                                                        <dx:EditModeCommandLayoutItem HorizontalAlign="Right"></dx:EditModeCommandLayoutItem>
                                                                    </Items>
                                                                    <SettingsItemCaptions Location="Left"/>
                                                                </dx:GridViewLayoutGroup>
                                                            </Items>
                                                        </EditFormLayoutProperties>
                                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true" Footer-ForeColor="#255658" Footer-Font-Size="10" Footer-Font-Names="Calibri">
                                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
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
                        <dx:ASPxButton runat="server" ID="btnBack" ClientInstanceName="btnBack" Text="Back" AutoPostBack="true" Theme="Glass" PostBackUrl="~/Transactions/Insurance/PolisAsuransiFAMaint.aspx"></dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem> 
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
    <asp:SqlDataSource ID="sdsAsset" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="SELECT 
                        a.AST_CODE, a.AST_NAME, a.PLAT_NO, 
                        a.SERI_NO, a.ENGINE_NO, a.INSURANCE, 
                        a.INS_NO, a.DUE_POLICE 
                        FROM FA_ASSETREGISTER a 
                        WHERE a.FISCAL_STATUS NOT IN ('disposal', 'sold')
                        ORDER BY a.AST_CODE">
    </asp:SqlDataSource>
</asp:Content>
