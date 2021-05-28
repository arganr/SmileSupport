<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="UpdateApplicClient.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.UpdateApplicationClient.UpdateApplicClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../../Scripts/Application.js"></script>
    <script type="text/javascript">
        function ShowAlert() {
            alert('Display Message Alert');
        }
        function ShowAlertSaveWindow() {
            lblmessage.SetValue("Apakah anda yakin ingin Simpan ?");
            cplMain.PerformCallback("SAVE");
            pcsave.Show();
        }
        var CustomErrorText = "* Value can't be empty.";
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "ACTION":
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


        function OnLuAppNoChanged(luAppNo) {
            var grid = luAppNo.GetGridView();
            if (luAppNo.GetText() != "") {
                
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'NAME;KTP;IBUKANDUNG;SALARY;StatusPekerjaan;INJAMIN;INJAMADD1;INJAMADD2;INJAMADD3;INJAMKTP;INPHONE;INJAMHUB;INEMERNAME;INSPOUNAME;INSPOUPLC;INSPOUBRDT;INSPOUKT;INSPOUEDU;INMARITAL;', OnGetSelectedFieldValues);
                cplMain.PerformCallback("LUAPPNO_CHANGED;" + luAppNo.GetValue().toString());
            }
        }
        function OnGetSelectedFieldValues(selectedValues) {
            txtClient.SetValue(selectedValues[0]);
            txtKTP.SetValue(selectedValues[1]);
            txtIbu.SetValue(selectedValues[2]);
            txtIncome.SetValue(selectedValues[3]);
            cbJobStat.SetValue(selectedValues[4]);
            txtRefName.SetValue(selectedValues[5]);
            txtAddress1.SetValue(selectedValues[6]);
            txtAddress2.SetValue(selectedValues[7]);
            txtAddress3.SetValue(selectedValues[8]);
            txtRefID.SetValue(selectedValues[9]);
            txtTelp.SetValue(selectedValues[10]);
            txtRelation.SetValue(selectedValues[11]);
            txtEmergencyName.SetValue(selectedValues[12]);

            txtSpousName.SetValue(selectedValues[13]);
            txtSpousPlc.SetValue(selectedValues[14]);
            deSpousBrth.SetValue(selectedValues[15]);
            txtSpousKTP.SetValue(selectedValues[16]);
            cbSpousEdu.SetValue(selectedValues[17]);

            //txtSpousName.SetEnabled(false);
            var marital = selectedValues[18];
            if (marital != 2) {
                formLayout.GetItemByName("liSpouseName").SetVisible(false);
                formLayout.GetItemByName("liSpousPlc").SetVisible(false);
                formLayout.GetItemByName("liSpousBrth").SetVisible(false);
                formLayout.GetItemByName("liSpousKTP").SetVisible(false);
                formLayout.GetItemByName("liSpousEdu").SetVisible(false);
                formLayout.GetItemByName("liSpousEmpty").SetVisible(false);
            } else {
                formLayout.GetItemByName("liSpouseName").SetVisible(true);
                formLayout.GetItemByName("liSpousPlc").SetVisible(true);
                formLayout.GetItemByName("liSpousBrth").SetVisible(true);
                formLayout.GetItemByName("liSpousKTP").SetVisible(true);
                formLayout.GetItemByName("liSpousEdu").SetVisible(true);
                formLayout.GetItemByName("liSpousEmpty").SetVisible(true);
            } 
        }

        function OnAppNoValidation(s, e) {
            cplMain.PerformCallback('ON_APPNO_VALIDATION;ON_APPNO_VALIDATION');
        }

    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Alert Confirmation !" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout6" runat="server" ClientInstanceName="ASPxFormLayout6">
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
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>

    <dx:ASPxFormLayout ID="formLayout" runat="server" ClientInstanceName="formLayout" Theme="Glass" Font-Names="Arial" ColCount="1">
        <Items>
            <dx:LayoutGroup Name="LayoutGroupApplicationEntry" ShowCaption="True" Caption="Update Client Info" GroupBoxDecoration="Box" ColCount="1" Width="30%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Application No." Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup runat="server" ID="luAppNo" ClientInstanceName="luAppNo" KeyFieldName="APPLICNO" DataSourceID="sdsClient" DisplayFormatString="{0}" TextFormatString="{0}" MultiTextSeparator=";" Width="100%">
                                    <ClientSideEvents Init="function(s, e) { OnLuAppNoChanged(s); }" ValueChanged="function(s, e) { OnLuAppNoChanged(s); }" Validation="OnAppNoValidation"/>
                                    <GridViewProperties EnablePagingCallbackAnimation="true">
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                    </GridViewProperties>
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="APPLICNO" FieldName="APPLICNO" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="NAME" FieldName="NAME" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="KTP" FieldName="KTP" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="IBUKANDUNG" FieldName="IBUKANDUNG" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="SALARY" FieldName="SALARY" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="StatusPekerjaan" FieldName="StatusPekerjaan" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INJAMIN" FieldName="INJAMIN" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INJAMADD1" FieldName="INJAMADD1" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INJAMADD2" FieldName="INJAMADD2" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INJAMADD3" FieldName="INJAMADD3" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INJAMKTP" FieldName="INJAMKTP" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INPHONE" FieldName="INPHONE" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INJAMHUB" FieldName="INJAMHUB" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INEMERNAME" FieldName="INEMERNAME" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INSPOUNAME" FieldName="INSPOUNAME" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INSPOUPLC" FieldName="INSPOUPLC" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INSPOUBRDT" FieldName="INSPOUBRDT" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INSPOUKT" FieldName="INSPOUKT" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INSPOUEDU" FieldName="INSPOUEDU" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="INMARITAL" FieldName="INMARITAL" ShowInCustomizationForm="True" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        
                                    </Columns>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Nama Client" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtClient" ClientInstanceName="txtClient">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="No KTP" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtKTP" ClientInstanceName="txtKTP">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Nama Ibu Kandung" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtIbu" ClientInstanceName="txtIbu">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Income" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtIncome" ClientInstanceName="txtIncome">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Status Pekerjaan">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" ID="cbJobStat" ClientInstanceName="cbJobStat" Width="100%">
                                    <Items>
                                        <dx:ListEditItem Text="PERMANENT" Value="PERMANENT"/>
                                        <dx:ListEditItem Text="KONTRAK" Value="KONTRAK"/>
                                    </Items>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>

                    
                    <dx:LayoutItem ShowCaption="True" Caption="Spouse Name" Width="100%" Name="liSpouseName">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtSpousName" ClientInstanceName="txtSpousName">
                                    <%--<ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>--%>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Spouse Birth Place" Width="100%" Name="liSpousPlc">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtSpousPlc" ClientInstanceName="txtSpousPlc">
                                    <%--<ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>--%>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Spouse Birth Date" Width="100%" Name="liSpousBrth">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deSpousBrth" ClientInstanceName="deSpousBrth" DisplayFormatString="dd/MM/yyyy" NullText="-- Select --">
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Spouse KTP" Width="100%" Name="liSpousKTP">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtSpousKTP" ClientInstanceName="txtSpousKTP">
                                    <%--<ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>--%>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Spouse Education" Name="liSpousEdu">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" ID="cbSpousEdu" ClientInstanceName="cbSpousEdu" Width="100%" NullText="-- Select --">
                                    <Items>
                                        <dx:ListEditItem Text="SD" Value="SD"/>
                                        <dx:ListEditItem Text="SMP" Value="SMP"/>
                                        <dx:ListEditItem Text="SMU" Value="SMU"/>
                                        <dx:ListEditItem Text="DIPLOMA" Value="DIPLOMA"/>
                                        <dx:ListEditItem Text="S1" Value="S1"/>
                                        <dx:ListEditItem Text="S2" Value="S2"/>
                                        <dx:ListEditItem Text="S3" Value="S3"/>
                                    </Items>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Name="liSpousEmpty"></dx:EmptyLayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Ref Name" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtRefName" ClientInstanceName="txtRefName">
                                    <%--<ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>--%>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Address" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtAddress1" ClientInstanceName="txtAddress1">
                                    <%--<ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>--%>
                                </dx:ASPxTextBox>
                                <dx:ASPxTextBox runat="server" ID="txtAddress2" ClientInstanceName="txtAddress2">
                                </dx:ASPxTextBox>
                                <dx:ASPxTextBox runat="server" ID="txtAddress3" ClientInstanceName="txtAddress3">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Identity No" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtRefID" ClientInstanceName="txtRefID">
                                    <%--<ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>--%>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Telephone" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtTelp" ClientInstanceName="txtTelp">
                                    <%--<ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>--%>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Relationship" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtRelation" ClientInstanceName="txtRelation">
                                    <%--<ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>--%>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Emergency Contact" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtEmergencyName" ClientInstanceName="txtEmergencyName">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Height="30px" Text="Save" AutoPostBack="false" Theme="MaterialCompact">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVE_CONFIRM;' + 'CLICK'); }" />
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

    <%--<asp:SqlDataSource ID="sdsApplication" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="exec [dbo].[spSmileSupport_SearchAPPBeforeAgr] '',''">
    </asp:SqlDataSource>--%>

    <asp:SqlDataSource ID="sdsClient" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="exec spSmileSupport_LoadDataClientUpdate">
    </asp:SqlDataSource>
</asp:Content>
