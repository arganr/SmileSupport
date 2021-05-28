<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="MultipleUploadDocument.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.UploadDocument.MultipleUploadDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var IsDocumentCodeChanged = true;
        var vDocumentCode = "";

        function cplMain_EndCallback(s, e)
        {
            switch (cplMain.cpCallbackParam)
            {
                case "UPLOADCONFIRM":
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
                case "UPLOAD":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
            }
            cplMain.cpCallbackParam = null;
        }

        function OnViewClick(s, e) {
            switch (e.buttonID) {
                case "btnImage":                    
                    gvImage.PerformCallback('IMAGE;' + e.visibleIndex);
                    break;
            }
        }

        function gvImage_EndCallback() {
            switch (gvImage.cpCallbackParam) {
                case "IMAGE":
                    var imagedata = gvImage.cpImage;
                    izImageZoom.GetMainElement().src = imagedata;
                    apcImage.Show();

                    break;
            }
        }

        function upload_FileUploadComplete(s, e) {
            UploadControl.SetVisible(false);
            btnDownload.SetVisible(true);
        }

        function OnTextChanged(s, e) {
            s.Upload();
        }

        function gvMain_EndCallback(s, e)
        {
            switch (gvMain.cpCallbackParam) {
                case "UPLOAD":
                    //colAttachment1.SetValue(gvMain.cpFileName);
                    break;
            }
        }

        function OnDocumentChanged(s, e) {
            gvMain.GetEditor("SubType").PerformCallback(s.GetValue());
        }

    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Alert Confirmation !" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
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
                                            <dx:ASPxButton ID="btnSaveConfirm" runat="server" Text="OK" AutoPostBack="False" UseSubmitBehavior="false" Width="100">
                                                <ClientSideEvents Click="function(s, e) { apcconfirm.Hide();cplMain.PerformCallback(cplMain.cplblActionButton + ';'+cplMain.cplblActionButton); }" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                            <dx:ASPxButton ID="btnCancelConfirm" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="100">
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
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:TabbedLayoutGroup Height="100px" Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                <Items>
                    <dx:LayoutGroup Caption="Multiple Upload">
                        <Items>
                            <%--<dx:EmptyLayoutItem></dx:EmptyLayoutItem>--%>
                            <dx:LayoutItem ShowCaption="true" Caption="Application No." Width="25%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxGridLookup runat="server" ID="luAppNo" ClientInstanceName="luAppNo" KeyFieldName="APPLICNO" DataSourceID="sdsApplication" DisplayFormatString="{1}" TextFormatString="{0};{1};{2};{3};{4}" MultiTextSeparator=";" Width="100%">
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
                            <dx:LayoutItem ShowCaption="False" Width="100%">
                                 <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                        <dx:ASPxGridView
                                                    runat="server"
                                                    ID="gvMain"
                                                    ClientInstanceName="gvMain"
                                                    Width="100%"
                                                    KeyFieldName="ID"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Glass"
                                                    Font-Names="Calibri"
                                                    OnInit="gvMain_Init"
                                                    OnInitNewRow="gvMain_InitNewRow"
                                                    OnDataBinding="gvMain_DataBinding"
                                                    OnRowInserting="gvMain_RowInserting"
                                                    OnRowUpdating="gvMain_RowUpdating"
                                                    OnRowDeleting="gvMain_RowDeleting"
                                                    OnCustomCallback="gvMain_CustomCallback"
                                                    OnAutoFilterCellEditorInitialize="gvMain_AutoFilterCellEditorInitialize" 
                                                    OnCellEditorInitialize="gvMain_CellEditorInitialize"
                                                    OnCustomColumnDisplayText="gvMain_CustomColumnDisplayText">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents EndCallback="gvMain_EndCallback" /><ClientSideEvents />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" ShowFooter="true"/>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="100" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsText ConfirmDelete="Are you really want to Delete?" />
                                                    <SettingsEditing Mode="EditForm" NewItemRowPosition="Bottom"></SettingsEditing>
                                                    <SettingsCommandButton>
                                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px"></NewButton>
                                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                    </SettingsCommandButton>
                                                    <EditFormLayoutProperties>
                                                        <Items>
                                                            <dx:GridViewLayoutGroup Caption="Main" GroupBoxDecoration="None" ShowCaption="False" Width="50%">
                                                                <Items>
                                                                    <dx:GridViewColumnLayoutItem ColumnName="colDocument" Width="100%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem ColumnName="colSubDocument" Width="100%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem ColumnName="colRemark" Width="100%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem ColumnName="colAttachment" Caption="Attachment" Width="100%">
                                                                        <Template>
                                                                            <div class="uploadContainer">
                                                                                <dx:ASPxUploadControl ID="UploadControl" runat="server" ClientInstanceName="UploadControl" NullText="Select Single files..." OnFileUploadComplete="UploadControl_FileUploadComplete" ShowProgressPanel="True" ShowUploadButton="False" UploadMode="Advanced" Width="320">
                                                                                    <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="false" />
                                                                                    <ValidationSettings AllowedFileExtensions=".pdf,.jpg,.jpeg,.img,.tiff,.xls,.xlsx,.txt,.doc,.docx,.rar,.zip" MaxFileSize="10000000">
                                                                                    </ValidationSettings>
                                                                                    <ClientSideEvents FileUploadComplete="function(s,e){upload_FileUploadComplete(s,e);}" TextChanged="OnTextChanged"/>
                                                                                </dx:ASPxUploadControl>
                                                                                <dx:ASPxButton ID="btnDownload" runat="server" ClientInstanceName="btnDownload" OnClick="btnDownload_Click" Text="View Document" ClientVisible="false" Width="35%" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Left" VerticalAlign="Middle">
                                                                                </dx:ASPxButton>
                                                                            </div>
                                                                        </Template>
                                                                    </dx:GridViewColumnLayoutItem>
                                                                </Items>
                                                            </dx:GridViewLayoutGroup>
                                                            <dx:EditModeCommandLayoutItem HorizontalAlign="Right" Width="100%"></dx:EditModeCommandLayoutItem>
                                                        </Items>
                                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" />
                                                    </EditFormLayoutProperties>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" Width="5%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colDocument" Caption="Document Type" FieldName="Type" ReadOnly="false" ShowInCustomizationForm="true" Width="15%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesComboBox ClientInstanceName="colDocument" DataSourceID="sdsMasterDocument"
                                                                TextField="DocumentDesc" ValueField="DocumentCode" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList"
                                                                TextFormatString="{1}">
                                                                <ClientSideEvents SelectedIndexChanged="OnDocumentChanged" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="Code" FieldName="DocumentCode"></dx:ListBoxColumn>
                                                                    <dx:ListBoxColumn Caption="Description" FieldName="DocumentDesc" Width="200"></dx:ListBoxColumn>
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                            <Settings AllowHeaderFilter="True"></Settings>
                                                            <SettingsHeaderFilter Mode="CheckedList"></SettingsHeaderFilter>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colSubDocument" Caption="Sub Document" FieldName="SubType" ReadOnly="false" ShowInCustomizationForm="true" Width="15%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesComboBox ClientInstanceName="colSubDocument"
                                                                TextField="SubDocumentDesc" ValueField="SubDocumentDesc" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList"
                                                                TextFormatString="{1}">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="Code" FieldName="SubDocumentCode"></dx:ListBoxColumn>
                                                                    <dx:ListBoxColumn Caption="Description" FieldName="SubDocumentDesc" Width="200"></dx:ListBoxColumn>
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                            <Settings AllowHeaderFilter="True"></Settings>
                                                            <SettingsHeaderFilter Mode="CheckedList"></SettingsHeaderFilter>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataMemoColumn Visible="true" Caption="Remark" Name="colRemark" FieldName="Remarks" ShowInCustomizationForm="True" PropertiesMemoEdit-Height="50px">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesMemoEdit ClientInstanceName="colRemark"></PropertiesMemoEdit>
                                                        </dx:GridViewDataMemoColumn>
                                                        <dx:GridViewCommandColumn Name="colAttachment" ButtonType="Button" Caption="Attachment" ShowInCustomizationForm="True" Width="10%" Visible="false">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnAttachment" Text="View">
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataSpinEditColumn FieldName="FileSize" PropertiesSpinEdit-ClientInstanceName="FileSize" Caption="Size(Kb)"
                                                            ReadOnly="false" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-NumberFormat="Number" ShowInCustomizationForm="True"
                                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="2147483647"
                                                            PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-DecimalPlaces="2" Width="10%"
                                                            PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="FileSize" SummaryType="Sum" DisplayFormat="#,0.00' Kb'" />
                                                    </TotalSummary>
                                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                                                </dx:ASPxGridView>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:TabbedLayoutGroup>
            <dx:LayoutItem ShowCaption="false" Width="15%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnUpload" ClientInstanceName="btnUpload" Text="Upload All Document" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('UPLOADCONFIRM;UPLOADCONFIRM')}"/>
                            </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
<%--            <dx:LayoutItem ShowCaption="false" Width="15%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxLabel runat="server" Text="maximum 10 mb/file." Font-Italic="true" Font-Size="Smaller" ForeColor="Red"></dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>--%>
            <dx:LayoutGroup Caption="" GroupBoxDecoration="None" ColCount="2" Width="100%" HorizontalAlign="Center">
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
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
    <asp:SqlDataSource ID="sdsMasterDocument" runat="server" SelectCommand="SELECT * FROM [dbo].[MasterDocumentDesc]">
    </asp:SqlDataSource>
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
                        WHERE a.APPSTATUS NOT IN ('CANCEL', 'REJECT') --
                        UNION ALL
                        SELECT '' C_NAME, DocNo APPLICNO, '' LSAGREE, '' NAME,
                        'Internal Memo' Module, '' CONTRACT_STATUS, '' APPLICDT, '' DISBURSEDT
                        FROM [INFORMA].[dbo].[InternalMemo]
                        ORDER BY a.APPLICDT DESC"></asp:SqlDataSource>--%>
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
                        WHERE a.APPSTATUS NOT IN ('CANCEL', 'REJECT') --
                        UNION ALL
                        SELECT '' C_NAME, DocNo APPLICNO, '' LSAGREE, '' NAME,
                        'Internal Memo' Module, '' CONTRACT_STATUS, '' APPLICDT, '' DISBURSEDT
                        FROM [DBNONCORE].[INFORMA].[dbo].[InternalMemo]
                        ORDER BY a.APPLICDT DESC"></asp:SqlDataSource>
</asp:Content>
