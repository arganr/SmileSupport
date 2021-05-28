<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AddManualDeviation.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.AddManualDeviation.AddManualDeviation" %>
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
                case "APPNO_ONCHANGE":
                    OnluAppNoChanged();
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
            }
            cplMain.cpCallbackParam = null;
        }
        function OnBeginCallback(s, e)
        {
            command = e.command;
        }
        function OnluAppNoChanged(s, e) {
            luKemenag.GetGridView().PerformCallback();
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
            <dx:LayoutGroup Name="LayoutGroupAddManualDeviation" ShowCaption="True" Caption="Add Manual Deviation" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Application No." Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup
                                        runat="server"
                                        ID="luAppNo"
                                        ClientInstanceName="luAppNo" 
                                        DataSourceID="sdsApplicNo"
                                        OnDataBinding="luAppNo_DataBinding"
                                        OnInit="luAppNo_Init"
                                        AutoGenerateColumns="False"
                                        DisplayFormatString="{1}"
                                        TextFormatString="{1}"
                                        KeyFieldName="APPLICNO"
                                        SelectionMode="Single"
                                        AnimationType="Fade" NullText="-- Select --" HelpText="Please select Application number.">
                                        <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                        <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                        <ClientSideEvents ValueChanged="function(s, e) { cplMain.PerformCallback('APPNO_ONCHANGE;' + 'CHANGE'); }"/>
                                        <GridViewProperties EnablePagingCallbackAnimation="true">
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                            <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                            <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                        </GridViewProperties>
                                        <Columns>
                                            <dx:GridViewDataColumn Caption="C_CODE" FieldName="C_CODE" ShowInCustomizationForm="True" Visible="false">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Application No." FieldName="APPLICNO" ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Nama" FieldName="NAME" ShowInCustomizationForm="True" VisibleIndex="1">
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
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Jenis Deviasi" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" ID="cbJenisDeviasi" ClientInstanceName="cbJenisDeviasi" AutoPostBack="false" NullText="-- Select --">
                                    <Items>
                                        <dx:ListEditItem Text="Deviasi bukti penghasilan (tidak sesuai)" Value="Deviasi bukti penghasilan (tidak sesuai)" />
                                        <dx:ListEditItem Text="Deviasi dokumen identitas (tidak lengkap/sesuai)" Value="Deviasi dokumen identitas (tidak lengkap/sesuai)" />
                                        <dx:ListEditItem Text="Deviasi Kelengkapan PBB/Rek Telepon/Rek Listrik (tidak ada)" Value="Deviasi Kelengkapan PBB/Rek Telepon/Rek Listrik (tidak ada)" />
                                        <dx:ListEditItem Text="Deviasi Surat Keterangan Usaha (tidak ada/sesuai)" Value="Deviasi Surat Keterangan Usaha (tidak ada/sesuai)" />
                                        <dx:ListEditItem Text="Deviasi Penjamin (bukan kerabat inti)" Value="Deviasi Penjamin (bukan kerabat inti)" />
                                        <dx:ListEditItem Text="Deviasi Lainnya" Value="Deviasi Lainnya" />
                                    </Items>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Description" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmDescription" ClientInstanceName="mmDescription" Height="80px" NullText="...."></dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Text="Save" AutoPostBack="false" Theme="Glass">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVE_CONFIRM;' + 'SAVE_CONFIRM;' + luAppNo.GetText()); }" />
                                </dx:ASPxButton>
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
    <asp:SqlDataSource ID="sdsApplicNo" runat="server"
        SelectCommand="SELECT C_CODE, APPLICNO, NAME FROM LS_APPLICATION WHERE APPSTATUS = 'NEW' AND PRODUCT_FACILITY_CODE = '112' AND SUPP_CODE = '0S10010022'">
    </asp:SqlDataSource>
</asp:Content>
