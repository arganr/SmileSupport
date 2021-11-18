<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="SyariahCreditProcessList.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.Syariah.SyariahCreditProcessList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../../Scripts/Application.js"></script>
    <script type="text/javascript">
        var DocNo;
        function gvMain_EndCallback(s, e) {
            switch (gvMain.cpCallbackParam) {
                case "INDEX":
                    gvMain.SetFocusedRowIndex(gvMain.cpVisibleIndex);
                    break;
            }

            gvMain.cplblmessageError = "";
            gvMain.cpCallbackParam = "";
        }
        function cplMain_EndCallback() {
            switch (cplMain.cpCallbackParam) {
                case "REFRESH":
                    gvMain.Refresh();
                    break;
            }
        }
        function GetDocNo(values) {
            DocNo = values;
        }
        window.onload = function () {
            if (gvMain.GetFocusedRowIndex() > -1) {
                gvMain.GetRowValues(gvMain.GetFocusedRowIndex(), 'APPLICNO', GetDocNo);
            }
        }
        function FocusedRowChanged(s) {
            if (gvMain.GetFocusedRowIndex() > -1) {
                gvHistory.PerformCallback('LOAD_HISTORY;' + s.GetRowKey(s.GetFocusedRowIndex()));
                gvComment.PerformCallback('LOAD_COMMENT;' + s.GetRowKey(s.GetFocusedRowIndex()));
            }
        }
        function gvMain_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case "GridbtnProceed":
                    gvMain.GetRowValues(e.visibleIndex, "STATUS;APPLICNO", btnProceedOnCLick);
                    break;
                case "GridbtnComment":
                    gvMain.GetRowValues(e.visibleIndex, "APPLICNO", btnCommentOnCLick);
                    break;
            }
        }
        function btnProceedOnCLick() {
            apcProceed.Show();
        }
        function btnCommentOnCLick() {
            apcFormComment.Show();
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />
    <dx:ASPxPopupControl ID="apcFormComment" ClientInstanceName="apcFormComment" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Add Comment" AllowDragging="True" PopupAnimationType="Fade"
        EnableCallbackAnimation="true" CloseAction="CloseButton"
        EnableViewState="False"
        Width="400px"
        Height="200px"
        FooterStyle-Wrap="False" ShowFooter="false" FooterText="" AllowResize="false">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2" Width="100%">
                    <Items>                       
                        <dx:LayoutItem ShowCaption="True" Caption="Comment" Width="100%" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxMemo runat="server" ID="mmComment" ClientInstanceName="mmComment" Width="100%" Height="100px"></dx:ASPxMemo>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="" Width="100%" ColSpan="2" HorizontalAlign="Right">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
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
    <dx:ASPxPopupControl ID="apcProceed" ClientInstanceName="apcProceed" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="please add note before proceed ..." AllowDragging="True" PopupAnimationType="Fade"
        EnableCallbackAnimation="true" CloseAction="CloseButton"
        EnableViewState="False"
        Width="350px"
        Height="100px"
        FooterStyle-Wrap="False" ShowFooter="false" FooterText="" AllowResize="false">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" ColCount="2" Width="100%">
                    <Items>                       
                        <dx:LayoutItem ShowCaption="False" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                    <dx:ASPxMemo runat="server" ID="mmDecisionNote" ClientInstanceName="mmDecisionNote" Width="100%" Height="50px" Theme="Aqua">
                                        <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                            <RequiredField ErrorText="*" IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxMemo>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="false" Caption="" Width="100%" ColSpan="2" HorizontalAlign="Right">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                    <dx:ASPxButton ID="btnSaveAssign" runat="server" Text="Proceed" AutoPostBack="False" UseSubmitBehavior="false" Width="50%">
                                        <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('PROCEED'); apcProceed.Hide(); }" />
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="btnCancelAssign" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="50%">
                                        <ClientSideEvents Click="function(s, e) { apcProceed.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Font-Names="Calibri">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Credit Process Syariah" ColCount="5">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                <dx:ASPxButton ID="btnRefresh" ClientInstanceName="btnRefresh" runat="server" Text="Refresh" BackColor="LightGray" OnClick="btnRefresh_Click" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup ShowCaption="False" ColCount="1" GroupBoxDecoration="None">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvMain"
                                    ClientInstanceName="gvMain"
                                    runat="server"
                                    KeyFieldName="APPLICNO"
                                    Width="100%" 
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnDataBinding="gvMain_DataBinding"
                                    OnFocusedRowChanged="gvMain_FocusedRowChanged"
                                    OnCustomCallback="gvMain_CustomCallback"
                                    OnCustomColumnDisplayText="gvMain_CustomColumnDisplayText" OnInit="gvMain_Init" OnCustomButtonInitialize="gvMain_CustomButtonInitialize"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri" >
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                    </SettingsAdaptivity>
                                    <ClientSideEvents
                                        EndCallback="gvMain_EndCallback"
                                        RowDblClick="function(s, e) {gvMain.PerformCallback('DOUBLECLICK;' +e.visibleIndex);}"
                                        FocusedRowChanged="FocusedRowChanged" CustomButtonClick="function(s,e) { gvMain_CustomButtonClick(s,e); }"/>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="Disabled" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                    <SettingsPager PageSize="15"></SettingsPager>
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
                                        <dx:GridViewDataTextColumn Name="colID" Caption="ID" FieldName="ID" ReadOnly="True" ShowInCustomizationForm="true" Visible="false">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataMemoColumn Name="colBranch" Caption="Branch" FieldName="BRANCH" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="1">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataTextColumn Name="colSTEP" Caption="Current Status" FieldName="STATUS" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="2">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colAPPLICNO" Caption="App No." FieldName="APPLICNO" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3" >
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDebiturName" Caption="Debitur" FieldName="DEBITUR_NAME" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="4">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colPackage" Caption="Package" FieldName="CAMPAIGN_DESC" ReadOnly="true" ShowInCustomizationForm="true" Visible="false" VisibleIndex="5">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colTenor" Caption="Tenor" FieldName="TENOR" ReadOnly="true" ShowInCustomizationForm="true" VisibleIndex="6" Width="5%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colNTF" Caption="NTF" FieldName="NTF" ReadOnly="true" ShowInCustomizationForm="true" PropertiesSpinEdit-DisplayFormatString="#,0.00" VisibleIndex="7" >
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn Name="colCRE_BY" Caption="Created By" FieldName="CRE_BY" ReadOnly="True" ShowInCustomizationForm="true" VisibleIndex="8">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colCRE_DATE" Caption="Created Date" FieldName="CRE_DATE" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" VisibleIndex="9">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button"  Caption="" Width="20%" Name="colAction" VisibleIndex="10">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="GridbtnProceed" Text="Proceed" Styles-Style-HoverStyle-ForeColor="Green">
                                                    <Image Height="20px" Width="20px" ToolTip="Click here to proceed application."></Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                                <dx:GridViewCommandColumnCustomButton ID="GridbtnComment" Text="Comment" Styles-Style-HoverStyle-ForeColor="Green">
                                                    <Image Height="20px" Width="20px" ToolTip="Click here to add comment."></Image>
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
            <dx:LayoutGroup ShowCaption="False" ColCount="1" GroupBoxDecoration="None">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxLabel ID="lblHistory" runat="server" Text="History :" Font-Bold="true" Font-Names="Calibri" ForeColor="SlateGray"></dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxGridView ID="gvHistory" runat="server" ClientInstanceName="gvHistory"
                            EnableTheming="True"
                            Theme="Glass"
                            OnDataBinding="gvHistory_DataBinding"
                            EnableCallBacks="true"
                            EnablePagingCallbackAnimation="true"
                            EnableCallbackAnimation="true"
                            OnCustomCallback="gvHistory_CustomCallback"
                            OnCustomCellMerge="gvHistory_CustomCellMerge" OnCustomColumnDisplayText="gvHistory_CustomColumnDisplayText"
                            EndCallback="gvHistory_EndCallback" Font-Size="8" Font-Names="Calibri">
                            <Settings ShowFooter="true" />
                            <SettingsBehavior AllowCellMerge="true" AllowFocusedRow="true" AllowSelectByRowClick="True" EnableRowHotTrack="true" AllowSort="false" />
                            <SettingsLoadingPanel Mode="ShowOnStatusBar"/>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Application No" FieldName="APPLICNO" Name="colDocNo" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0" Width="10%">
                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Action By" FieldName="USER_NAME" Name="colTransBy" ShowInCustomizationForm="True" VisibleIndex="1" Width="20%">
                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="Action Date" FieldName="CRE_DATE" Name="colTransDate" ShowInCustomizationForm="True" VisibleIndex="2" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" Width="55%">
                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="Status" FieldName="STEP" Name="colStatus" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Visible="true" Width="10%">
                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="Duration" FieldName="DURATION" Name="colDuration" ShowInCustomizationForm="True" VisibleIndex="5" Width="5%">
                                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                </dx:GridViewDataDateColumn>
                            </Columns>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="DURATION" SummaryType="Sum" DisplayFormat="#,# 'Minutes'" />
                            </TotalSummary>
                            <SettingsPager PageSize="30"></SettingsPager>
                            <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                        </dx:ASPxGridView>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxLabel ID="lblComment" runat="server" Text="Comment :" Font-Bold="true" Font-Names="Calibri" ForeColor="SlateGray"></dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView 
                                    runat="server" 
                                    ID="gvComment" 
                                    ClientInstanceName="gvComment" 
                                    Width="100%"
                                    AutoGenerateColumns="False" 
                                    EnableTheming="true" 
                                    Theme="Glass" OnDataBinding="gvComment_DataBinding"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true" 
                                    EnableCallbackAnimation="true" 
                                    OnCustomCallback="gvComment_CustomCallback" 
                                    EndCallback="gvComment_EndCallBack"
                                    Font-Size="8" Font-Names="Calibri">
                                    <Settings ShowFooter="true"/>
                                    <SettingsBehavior AllowFocusedRow="false" EnableRowHotTrack="true" AllowSort="false"/>
                                    <SettingsLoadingPanel Mode="ShowOnStatusBar"/>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Comment By" FieldName="CommentBy" Name="colCommentBy" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="20%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Caption="Comment Date" FieldName="CommentDate" Name="colCommentDate" ReadOnly="True" ShowInCustomizationForm="True" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" VisibleIndex="1" Width="15%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataMemoColumn Caption="Comment" FieldName="CommentNote" Name="colCommentNote" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="65%">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataMemoColumn>
                                    </Columns>
                                    <SettingsPager PageSize="30"></SettingsPager>
                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="false"></Styles>
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
    <asp:SqlDataSource ID="sdsApplication" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="SELECT DISTINCT
                A.APPLICNO,
                (SELECT TOP 1 STEP FROM MNCL_APP_TIME_STAMP WHERE MNCL_APP_TIME_STAMP.APPLICNO=A.APPLICNO ORDER BY ID DESC) STATUS,
                D.USER_NAME CRE_BY,
                A.CRE_DATE,
                C.C_NAME As BRANCH,
                A.NAME DEBITUR_NAME,
                A.CAMPAIGN_DESC,
                A.CAMPAIGN_TENOR_DESC TENOR
                FROM LS_APPLICATION A
                INNER JOIN MNCL_APP_TIME_STAMP B ON A.APPLICNO = B.APPLICNO
                INNER JOIN SYS_COMPANY C ON A.C_CODE = C.C_CODE
                INNER JOIN MASTER_USER D ON A.CRE_BY = D.USER_ID
                WHERE A.CAMPAIGN_CODE='PCKGHJREG'"></asp:SqlDataSource>
</asp:Content>