<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PrintSheetControl.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.PrintSheetControl.PrintSheetControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../../Scripts/Application.js"></script>
    <script type="text/javascript">
        var timeout;
        var textSeparator = "\r\n";
        function gvMain_EndCallback(s, e) {
            switch (gvMain.cpCallbackParam) {
                case "INDEX":
                    gvMain.SetFocusedRowIndex(gvMain.cpVisibleIndex);
                    break;
            }

            gvMain.cplblmessageError = "";
            gvMain.cpCallbackParam = "";
            scheduleGridUpdate(s);
        }
        function cplMain_EndCallback(s, e)
        {
            switch (cplMain.cpCallbackParam)
            {
                case "PRINT":
                    var DocNo = cplMain.cpDocNo;
                    var AppNo = cplMain.cpAppNo;
                    var Tipe = cplMain.cpTipe;
                    if (Tipe == "Perorangan")
                    {
                        window.open("../../../Shared/DocViewer.aspx?ReportType=PSC&Title=Lembar Kontrol - " + DocNo + "&DocNo=" + DocNo + "&AppNo=" + AppNo);
                    }
                    else
                    {
                        window.open("../../../Shared/DocViewer.aspx?ReportType=PSC-B&Title=Lembar Kontrol - " + DocNo + "&DocNo=" + DocNo + "&AppNo=" + AppNo);
                    }
                    break;
                case "GENERATE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "GENERATE_CONFIRM":
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
                case "APP_NO_ONCHANGE":
                        OnluAccNoChanged();
                        
                        cbBindingType.SetValue(cplMain.cpJenisPengikatan);
                        txtCRNo.SetValue(cplMain.cpCRNo);
                        txtCAMNo.SetValue(cplMain.cpCAMNo);

                        var CRdate = new Date(cplMain.cpCRDate);
                        deCR.SetDate(CRdate);
                        var CAMdate = new Date(cplMain.cpCAMDate);
                        deCAM.SetDate(CAMdate);

                        if (CRdate.toString() == "01/01/1970")
                            deCR.SetText = "";
                        if (CAMdate.toString() == "01/01/1970")
                            deCAM.SetText = "";

                        mmMandDoc.SetValue(cplMain.cpDocMand);
                        mmAddDoc.SetValue(cplMain.cpDocAddi);
                        mmLegalCon.SetValue(cplMain.cpLegalConclution);
                        mmDefDoc.SetValue(cplMain.cpUncompletedDoc);

                        txtMadeBy.SetValue(cplMain.cpFooterMadeBy);
                        txtMadeByPos.SetValue(cplMain.cpFooterMadeByPos);
                        txtApprovedBy.SetValue(cplMain.cpFooterApprovedBy);
                        txtApprovedByPos.SetValue(cplMain.cpFooterApprovedByPos);
                        txtMarketing.SetValue(cplMain.cpFooterMarketing);
                        txtMarketingPos.SetValue(cplMain.cpFooterMarketingPos);
                        txtBusinessManager.SetValue(cplMain.cpFooterBusinessManager);
                        txtBusinessManagerPos.SetValue(cplMain.cpFooterBusinessManagerPos);
                        gvDetailAsset.Refresh();
                    break;
            }
        }
        function scheduleGridUpdate(grid)
        {
            //window.clearTimeout(timeout);
            //timeout = window.setTimeout(
            //    function ()
            //    { gvMain.PerformCallback('REFRESH; REFRESH'); },
            //    60000
            //);
        }
        function gvMain_Init(s, e)
        {
            scheduleGridUpdate(s);
        }
        function gvMain_BeginCallback(s, e) {
            window.clearTimeout(timeout);
        }
        function FocusedRowChanged(s)
        {
            if (gvMain.GetFocusedRowIndex() > -1)
            {
            }
        }
        function updateTextMandDoc() {
            var selectedItems = lbMandDoc.GetSelectedItems();
            ddeMandDoc.SetText(getSelectedItemsTextMandDoc(selectedItems));
            mmMandDoc.SetText(getSelectedItemsTextMandDoc(selectedItems));
        }
        function updateTextAddDoc() {
            var selectedItems = lbAddDoc.GetSelectedItems();
            ddeAddDoc.SetText(getSelectedItemsTextAddDoc(selectedItems));
            mmAddDoc.SetText(getSelectedItemsTextAddDoc(selectedItems));
        }
        function synchronizeListBoxValuesMandDoc(dropDown, args) {
            lbMandDoc.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = getValuesByTextsMandDoc(texts);
            lbMandDoc.SelectValues(values);
            updateTextMandDoc();
        }
        function synchronizeListBoxValuesAddDoc(dropDown, args) {
            lbAddDoc.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = getValuesByTextsAddDoc(texts);
            lbAddDoc.SelectValues(values);
            updateTextAddDoc();
        }
        function getSelectedItemsTextMandDoc(items)
        {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getSelectedItemsTextAddDoc(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getValuesByTextsMandDoc(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = lbMandDoc.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
        function getValuesByTextsAddDoc(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = lbAddDoc.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
        function SetPersonalInfo()
        {

        }
        function gvMain_CustomButtonClick(s, e)
        {
            switch (e.buttonID)
            {
                case "btnPrint":
                    cplMain.PerformCallback("PRINT;PRINT;");
                    break;
            }
        }
        function OnluAccNoChanged(s, e) {
            var grid = luAccNo.GetGridView();
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'DEBITUR;DEBITUR', OnGetSelectedFieldValues);
        }
        function OnGetSelectedFieldValues(selectedValues) {
            txtDebitur.SetValue(selectedValues[1]);
        }
    </script>
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
                        <dx:LayoutItem Caption="" ColSpan="2" ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                    <dx:ASPxCheckBox ID="chkSendComment" ClientInstanceName="chkSendComment" runat="server" Text="Send Comment to Credit Process ?" ForeColor="DarkRed"></dx:ASPxCheckBox>
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
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup ShowCaption="True" Caption="Print Sheet Control" GroupBoxDecoration="HeadingLine" ColCount="1">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="Transparent" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutGroup ShowCaption="True" Caption="Data Entry" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                        <GroupBoxStyle BackColor="#f8fafd" Caption-BackColor="#f8fafd" Caption-Font-Bold="false"></GroupBoxStyle>
                        <Items>
                            <dx:LayoutItem ShowCaption="True" Caption="Application No." Width="25%">
                                <CaptionSettings Location="Left" />
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxGridLookup
                                            runat="server"
                                            ID="luAccNo"
                                            ClientInstanceName="luAccNo" 
                                            OnDataBinding="luAccNo_DataBinding"
                                            AutoGenerateColumns="False"
                                            DisplayFormatString="{0}"
                                            TextFormatString="{0}"
                                            KeyFieldName="NO APLIKASI"
                                            SelectionMode="Single"
                                            AnimationType="Slide" NullText="-- Select --" HelpText="Please select application number.">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                            <ClientSideEvents ValueChanged="function(s,e) { cplMain.PerformCallback('APP_NO_ONCHANGE;' + 'CHANGE'); }"/>
                                            <GridViewProperties EnablePagingCallbackAnimation="true">
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                                <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                                <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                            </GridViewProperties>
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Application No." FieldName="NO APLIKASI" ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="CIF" FieldName="CLIENT" ShowInCustomizationForm="True" VisibleIndex="2">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Client Name" FieldName="DEBITUR" ShowInCustomizationForm="True" VisibleIndex="3">
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
                            <dx:LayoutItem ShowCaption="true" Caption="Debitur Name" Width="25%">
                                <CaptionSettings Location="Left" HorizontalAlign="Right"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtDebitur" ClientInstanceName="txtDebitur" NullText="..." Width="100%">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Binding Type" Width="25%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox runat="server" ID="cbBindingType" ClientInstanceName="cbBindingType" NullText="-- Select --" HelpText="What binding type you choose ?">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                            <Items>
                                                <dx:ListEditItem Text="IF" Value="IF"/>
                                                <dx:ListEditItem Text="SLB" Value="SLB"/>
                                                <dx:ListEditItem Text="FL" Value="FL"/>
                                                <dx:ListEditItem Text="FMU" Value="FMU"/>
                                            </Items>
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="75%"></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="CR No." Width="25%">
                                <CaptionSettings Location="Left" />
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtCRNo" ClientInstanceName="txtCRNo" NullText="..." HelpText="you can added CR number here, this is required field.">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="CR Date" Width="25%">
                                <CaptionSettings Location="Left" HorizontalAlign="Right"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="deCR" ClientInstanceName="deCR" DisplayFormatString="dd/MM/yyyy" NullText="-- Select --" HelpText="you can select CR date,  this is required field.">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="CAM No." Width="25%">
                                <CaptionSettings Location="Left" />
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox runat="server" ID="txtCAMNo" ClientInstanceName="txtCAMNo" NullText="..." HelpText="you can added CAM number here, this is required field.">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="CAM Date" Width="25%">
                                <CaptionSettings Location="Left" HorizontalAlign="Right"/>
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit runat="server" ID="deCAM" ClientInstanceName="deCAM" DisplayFormatString="dd/MM/yyyy" NullText="-- Select --" HelpText="you can select CAM date, this is required field.">
                                            <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                            <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                            <dx:TabbedLayoutGroup Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Height="100px" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                            <Items>
                                <dx:LayoutGroup Caption="Detail Asset">
                                    <items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxGridView
                                                    runat="server"
                                                    ID="gvDetailAsset"
                                                    ClientInstanceName="gvDetailAsset" 
                                                    KeyFieldName="No"
                                                    Width="100%"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Glass"
                                                    Font-Names="Calibri" OnDataBinding="gvDetailAsset_DataBinding" OnCustomColumnDisplayText="gvDetailAsset_CustomColumnDisplayText">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" ShowFooter="True"/>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Name="colNo" Caption="No" ReadOnly="True" UnboundType="String" VisibleIndex="1" Width="5%">
                                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colItemDesc" Caption="Item Description" FieldName="ItemDescription" ReadOnly="True" ShowInCustomizationForm="true" Width="65%" VisibleIndex="2">
                                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colYear" Caption="Year" FieldName="Year" ReadOnly="True" ShowInCustomizationForm="true" Width="10%" VisibleIndex="3">
                                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colCondition" Caption="Condition" FieldName="Condition" ReadOnly="True" ShowInCustomizationForm="true" Width="10%" VisibleIndex="4" Visible="true">
                                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colAssetTypeDetail" Caption="Asset Type" FieldName="AssetTypeDetail" ReadOnly="True" ShowInCustomizationForm="true" Width="10%" VisibleIndex="5" Visible="true">
                                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles AdaptiveDetailButtonWidth="22">
                                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Footer">
                                    <Items>
                                        <dx:LayoutGroup ShowCaption="True" Caption="Footer Settings" GroupBoxDecoration="Box" ColCount="1">
                                            <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#edf3f4" Caption-Font-Bold="false"></GroupBoxStyle>
                                            <Items>
                                                <dx:LayoutItem ShowCaption="True" Caption="Made By" Width="35%">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="txtMadeBy" ClientInstanceName="txtMadeBy"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ShowCaption="True" Caption="Position" Width="35%">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="txtMadeByPos" ClientInstanceName="txtMadeByPos"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:EmptyLayoutItem Width="30%"></dx:EmptyLayoutItem>
                                                <dx:LayoutItem ShowCaption="True" Caption="Approved By" Width="35%">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="txtApprovedBy" ClientInstanceName="txtApprovedBy"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ShowCaption="True" Caption="Position" Width="35%">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="txtApprovedByPos" ClientInstanceName="txtApprovedByPos"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:EmptyLayoutItem Width="30%"></dx:EmptyLayoutItem>
                                                <dx:LayoutItem ShowCaption="True" Caption="Marketing" Width="35%">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="txtMarketing" ClientInstanceName="txtMarketing"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ShowCaption="True" Caption="Position" Width="35%">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="txtMarketingPos" ClientInstanceName="txtMarketingPos"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:EmptyLayoutItem Width="30%"></dx:EmptyLayoutItem>
                                                <dx:LayoutItem ShowCaption="True" Caption="Business Manager" Width="35%">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="txtBusinessManager" ClientInstanceName="txtBusinessManager"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ShowCaption="True" Caption="Position" Width="35%">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="txtBusinessManagerPos" ClientInstanceName="txtBusinessManagerPos"></dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                            </dx:TabbedLayoutGroup>
                            <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Mandatory Documents" Width="50%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDropDownEdit runat="server" ID="ddeMandDoc" ClientInstanceName="ddeMandDoc" DisplayFormatString="{1}" NullText="<-- Click here to add mandatory document" AnimationType="Fade" DropDownWindowWidth="600px" BackColor="Transparent" DropDownButton-Position="Left">
                                            <Border BorderStyle="None"/>
                                            <DropDownWindowTemplate>
                                                <dx:ASPxListBox runat="server"
                                                    ID="lbMandDoc" 
                                                    ClientInstanceName="lbMandDoc" 
                                                    OnDataBinding="lbMandDoc_DataBinding"  ValueField="Description"
                                                    SelectionMode="CheckColumn" EnableSelectAll="true" Width="100%" Height="200px">
                                                    <FilteringSettings ShowSearchUI="true"/>
                                                    <Border BorderStyle="None" />
                                                    <BorderBottom BorderStyle="Solid" />
                                                    <ClientSideEvents SelectedIndexChanged="updateTextMandDoc" Init="updateTextMandDoc" />
                                                    <Columns>
                                                        <dx:ListBoxColumn Width="99%" FieldName="Description" Caption="Description"></dx:ListBoxColumn>
                                                    </Columns>
                                                </dx:ASPxListBox>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="padding: 4px">
                                                            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" style="float: right">
                                                                <ClientSideEvents Click="function(s, e){ ddeMandDoc.HideDropDown(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </DropDownWindowTemplate>
                                            <ClientSideEvents TextChanged="synchronizeListBoxValuesMandDoc" DropDown="synchronizeListBoxValuesMandDoc" />
                                        </dx:ASPxDropDownEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Additional Documents" Width="50%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDropDownEdit runat="server" ID="ddeAddDoc" ClientInstanceName="ddeAddDoc" DisplayFormatString="{1}" NullText="<-- Click here to add additional document" AnimationType="Fade" DropDownWindowWidth="600px" BackColor="Transparent" DropDownButton-Position="Left">
                                            <Border BorderStyle="None"/>
                                            <DropDownWindowTemplate>
                                                <dx:ASPxListBox runat="server" 
                                                    ID="lbAddDoc" 
                                                    ClientInstanceName="lbAddDoc" 
                                                    OnDataBinding="lbAddDoc_DataBinding"  ValueField="Description"
                                                    SelectionMode="CheckColumn" EnableSelectAll="true" Width="100%" Height="200px">
                                                    <FilteringSettings ShowSearchUI="true"/>
                                                    <Border BorderStyle="None" />
                                                    <BorderBottom BorderStyle="Solid" />
                                                    <ClientSideEvents SelectedIndexChanged="updateTextAddDoc" Init="updateTextAddDoc" />
                                                    <Columns>
                                                        <dx:ListBoxColumn Width="99%" FieldName="Description" Caption="Description"></dx:ListBoxColumn>
                                                    </Columns>
                                                </dx:ASPxListBox>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="padding: 4px">
                                                            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" style="float: right">
                                                                <ClientSideEvents Click="function(s, e){ ddeAddDoc.HideDropDown(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </DropDownWindowTemplate>
                                            <ClientSideEvents TextChanged="synchronizeListBoxValuesAddDoc" DropDown="synchronizeListBoxValuesAddDoc" />
                                        </dx:ASPxDropDownEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="" Width="50%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxMemo runat="server" ID="mmMandDoc" ClientInstanceName="mmMandDoc" Font-Size="8" Height="70px" ReadOnly="true">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="" Width="50%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxMemo runat="server" ID="mmAddDoc" ClientInstanceName="mmAddDoc" Font-Size="8" Height="70px" ReadOnly="true">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Legal Dept. Conclusion" Width="50%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxMemo runat="server" ID="mmLegalCon" ClientInstanceName="mmLegalCon" Height="70px">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="Uncompleted Documents" Width="50%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxMemo runat="server" ID="mmDefDoc" ClientInstanceName="mmDefDoc" Height="70px">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="True" Caption="" Width="30%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButton runat="server" ID="btnGenerate" ClientInstanceName="btnGenerate" Height="30px" Text="Generate Sheet Control" AutoPostBack="false" Theme="MaterialCompact">
                                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('GENERATE_CONFIRM;' + 'CLICK'); }" />
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvMain"
                                    ClientInstanceName="gvMain"
                                    runat="server"
                                    KeyFieldName="DocNo"
                                    Width="100%"
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnInit="gvMain_Init"
                                    OnDataBinding="gvMain_DataBinding"
                                    OnFocusedRowChanged="gvMain_FocusedRowChanged"
                                    OnCustomCallback="gvMain_CustomCallback"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                    </SettingsAdaptivity>
                                    <ClientSideEvents
                                        EndCallback="gvMain_EndCallback"
                                        RowDblClick="function(s, e) {gvMain.PerformCallback('DOUBLECLICK;' +e.visibleIndex);}"
                                        FocusedRowChanged="FocusedRowChanged" Init="gvMain_Init" BeginCallback="gvMain_BeginCallback" 
                                        CustomButtonClick="function(s,e) { gvMain_CustomButtonClick(s,e); }"/>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="Disabled" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                    <SettingsPager PageSize="10"></SettingsPager>
                                    <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="ShowCustomizationWindow" DisplayMode="ImageWithText" />
                                                <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to .xlsx" ToolTip="Click here to export grid data to excel" />
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Columns>
                                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0" Caption=" ">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnPrint" Text="Preview" Image-Url="../../../Content/Images/ViewIcon-16x16.png">
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Name="colDocNo" Caption="LK No." FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />                                            
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colDocDate" Caption="LK Date" FieldName="DocDate" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="true" VisibleIndex="2" Width="10%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colAPPNO" Caption="Application No." FieldName="AppNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colClient" Caption="Client Code" FieldName="Client" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colClientName" Caption="Client Name" FieldName="Name" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="5">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colClientType" Caption="Client Type" FieldName="Tipe" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="6">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colInSalary" Caption="Salary" FieldName="Insalary" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" Visible="true" VisibleIndex="7">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataSpinEditColumn>
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
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
