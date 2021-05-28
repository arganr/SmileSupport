<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="UploadDocument.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Reporting.CreditProcess.UploadDocument.UploadDocument" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
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
        function onUploadControlFileUploadComplete(s, e) {
            UploadCtrl.SetVisible(false);
            btnDownload.SetVisible(true);
        }
        var lastDocument = null;
        function OncbDocumentChanged(cbDocument) {
            if (cbDocument.GetText() != "") {
                if (cbSubDocument.InCallback())
                    lastDocument = cbDocument.GetValue().toString();
                else
                    cbSubDocument.PerformCallback(cbDocument.GetValue().toString());
            }
        }
        function OncbSubDocument_EndCallback(s, e) {
            if (lastDocument) {
                cbSubDocument.PerformCallback(lastDocument);
                lastDocument = null;
            }
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
    <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" Theme="Glass" ColCount="3">
        <Items>
            <dx:LayoutItem ShowCaption="false" Caption="" ColSpan="3" HorizontalAlign="Right">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxHyperLink runat="server" Text="Klik here for multiple upload »" ForeColor="Blue" NavigateUrl="~/Reporting/CreditProcess/UploadDocument/MultipleUploadDocument.aspx"></dx:ASPxHyperLink>    
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutGroup Name="LayoutGroupUploadDocument" ShowCaption="True" Caption="Upload Document" GroupBoxDecoration="Box" ColCount="1">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="true" Caption="Application No." ColSpan="1">
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
                    <dx:LayoutItem ShowCaption="true" Caption="File Type" ColSpan="1" Visible="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cbDocType" ClientInstanceName="cbDocType" runat="server"
                                    ItemStyle-Wrap="True" IncrementalFilteringMode="Contains" DropDownStyle="DropDownList" Width="100%">
                                    <ItemStyle Wrap="True"></ItemStyle>
                                    <Items>
                                        <dx:ListEditItem Selected="True" Text="-- Select --" Value=""></dx:ListEditItem>
                                        <%--<dx:ListEditItem Text="Dokumen Legalitas" Value="1"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Dokumen Kredit 1 (Sebelum Pencairan)" Value="2"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Dokumen Jaminan" Value="3"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Dokumen Kontrak" Value="4"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Dokumen Finance" Value="5"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Dokumen Kredit 2 (Sesudah Pencairan)" Value="6"></dx:ListEditItem>
                                        <dx:ListEditItem Text="Dokumen Lainnya" Value="7"></dx:ListEditItem>--%>
                                        <dx:ListEditItem Text="BAST" Value="1"/>
                                        <dx:ListEditItem Text="BPKB" Value="2"/>
                                        <dx:ListEditItem Text="CEK ABSAH & BLOKIR" Value="3"/>
                                        <dx:ListEditItem Text="COPY KTP AN BPKB / SP HAK AN BPKB" Value="4"/>
                                        <dx:ListEditItem Text="FAKTUR" Value="5"/>
                                        <dx:ListEditItem Text="FORM A" Value="6"/>
                                        <dx:ListEditItem Text="FOTO / GESEKAN S/N" Value="7"/>
                                        <dx:ListEditItem Text="FOTO UNIT" Value="8"/>
                                        <dx:ListEditItem Text="GESEKAN NOKA & NOSIN" Value="9"/>
                                        <dx:ListEditItem Text="INVOICE" Value="10"/>
                                        <dx:ListEditItem Text="INVOICE AWAL DARI IMPORTIR / PARKING LIST" Value="11"/>
                                        <dx:ListEditItem Text="KWITANSI DP" Value="12"/>
                                        <dx:ListEditItem Text="KWITANSI OTR / PELUNASAN" Value="13"/>
                                        <dx:ListEditItem Text="KWITANSI PEMBIAYAAN" Value="14"/>
                                        <dx:ListEditItem Text="KWITANSI RANGKAP 2 AN BPKB" Value="15"/>
                                        <dx:ListEditItem Text="KWITANSI RANGKAP 3 AN DEBITUR" Value="16"/>
                                        <dx:ListEditItem Text="LPOS" Value="17"/>
                                        <dx:ListEditItem Text="PEMBERITAHUAN BARANG IMPOR (PIB)" Value="18"/>
                                        <dx:ListEditItem Text="PO" Value="19"/>
                                        <dx:ListEditItem Text="SP BPKB" Value="20"/>
                                        <dx:ListEditItem Text="SP HAK ATAS NAMA JAMINAN" Value="21"/>
                                        <dx:ListEditItem Text="SP INVOICE / INVOICE" Value="22"/>
                                        <dx:ListEditItem Text="SP TRANSFER DARI SUPPLIER" Value="23"/>
                                        <dx:ListEditItem Text="VERIFIKASI INVOICE" Value="24"/>
                                        <dx:ListEditItem Text="DOKUMEN GABUNGAN" Value="25"/>
                                        <dx:ListEditItem Text="DOKUMEN SURVEY" Value="26"/>
                                        <dx:ListEditItem Text="DOKUMEN PENAGIHAN" Value="27"/>
                                        <dx:ListEditItem Text="DOKUMEN UMUM / LAINNYA" Value="28"/>
                                        <dx:ListEditItem Text="DOKUMEN AMENDEMEN KONTRAK" Value="29"/>
                                        <dx:ListEditItem Text="DOKUMEN LEGALITAS / IDENTITAS" Value="30"/>
                                        <dx:ListEditItem Text="DOKUMEN ANALISA KREDIT / SKK" Value="31"/>
                                        <dx:ListEditItem Text="INTERNAL MEMO" Value="32"/>
                                    </Items>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Document" ColSpan="1">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" ID="cbDocument" ClientInstanceName="cbDocument" NullText="-- Select --" DropDownStyle="DropDownList" OnDataBinding="cbDocument_DataBinding" TextField="DocumentDesc" ValueField="DocumentDesc" ValueType="System.String" Width="100%">
                                    <ClientSideEvents Init="function(s, e) { OncbDocumentChanged(s); }" SelectedIndexChanged="function(s, e) { OncbDocumentChanged(s); }" />
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Document" FieldName="DocumentDesc" Name="colDocumentDesc" Width="100px" />
                                    </Columns>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Sub Document" ColSpan="1">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox runat="server" ID="cbSubDocument" ClientInstanceName="cbSubDocument" NullText="-- Select --" OnDataBinding="cbSubDocument_DataBinding" OnCallback="cbSubDocument_Callback" TextField="SubDocumentDesc" ValueField="SubDocumentDesc" ValueType="System.String" Width="100%" DropDownWidth="550px">
                                        <ClientSideEvents EndCallback="OncbSubDocument_EndCallback"/>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Sub Document" FieldName="SubDocumentDesc" Name="colSubDocumentDesc" Width="100px" />
                                        </Columns>
                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Remark" ColSpan="1">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                   <dx:ASPxMemo runat="server" ID="mmRemark1" ClientInstanceName="mmRemark1" Width="100%" Height="100px"></dx:ASPxMemo>     
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="File" ColSpan="1">
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
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnUpload" ClientInstanceName="btnUpload" Text="Upload" AutoPostBack="false" Width="50%">
                                    <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('UPLOADCONFIRM;UPLOADCONFIRM')}"/>
                                </dx:ASPxButton>
                                <dx:ASPxLabel runat="server" Text="maximum file size is 10mb." Font-Italic="true" Font-Size="Smaller" ForeColor="Red"></dx:ASPxLabel>

                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
                <SettingsItems VerticalAlign="Middle"/>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupMultipleUpload" ShowCaption="True" Caption="Multiple Upload Document" GroupBoxDecoration="Box" ColCount="1" Visible="false">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>

                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupUploadDocument" ShowCaption="False" Caption="" GroupBoxDecoration="None" ColSpan="2" ColCount="1">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Caption="" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxImage runat="server" ImageUrl="~/Content/Images/iDoc1.png" Width="330px" Height="250px"></dx:ASPxImage>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
                <SettingsItems VerticalAlign="Middle" HorizontalAlign="Right"/>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupUploadDocument2" ShowCaption="True" Caption="Document Library" GroupBoxDecoration="Box" ColSpan="3" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvMain"
                                    ClientInstanceName="gvMain"
                                    runat="server"
                                    KeyFieldName="ID"
                                    Width="100%"
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri" OnCustomButtonCallback="gvMain_CustomButtonCallback" OnDataBinding="gvMain_DataBinding">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                    </SettingsAdaptivity>
                                    <%--<ClientSideEvents
                                        EndCallback="gvMain_EndCallback"
                                        RowDblClick="function(s, e) {gvMain.PerformCallback('DOUBLECLICK;' +e.visibleIndex);}"
                                        FocusedRowChanged="FocusedRowChanged" Init="gvMain_Init" BeginCallback="gvMain_BeginCallback" 
                                        CustomButtonClick="function(s,e) { gvMain_CustomButtonClick(s,e); }"/>--%>
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
                                        <dx:GridViewDataTextColumn Name="colBranch" Caption="Branch" FieldName="Branch" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
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
                                        <dx:GridViewDataTextColumn Name="colName" Caption="Document Type" FieldName="Name" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colName" Caption="Sub Document Type" FieldName="SubType" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke"/>
                                        </dx:GridViewDataTextColumn>
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
