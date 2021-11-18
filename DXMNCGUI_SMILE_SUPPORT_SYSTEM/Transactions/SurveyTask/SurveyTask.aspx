<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="SurveyTask.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.SurveyTask.SurveyTask" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../../Scripts/Application.js"></script>
    <script type="text/javascript">
        function ShowAlert() {
            alert('Display Message Alert');
        }

        var CustomErrorText = "* Value can't be empty.";
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "ACTION":
                    break;
                case "SAVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "DBR":
                    txtDBR.SetValue(cplMain.cpDBR);
                    txtFreeCashFlow.SetValue(cplMain.cpFCF);
                    break;
                case "SLIK":
                    txtInsSLIK.SetValue(cplMain.cpInsSLIK);
                    break;
            }
            cplMain.cpCallbackParam = null;
        }

        function OnObjectChange(sender) {
            gvSurveyItem.Refresh();
        }

        function OnLuAppNoChanged(luAppNo) {
            var grid = luAppNo.GetGridView();
            if (luAppNo.GetText() != "") {

                grid.GetRowValues(grid.GetFocusedRowIndex(), 'APPLICNO;NAME;INBORNDT;INMARITAL;INSPOUNAME;ADDRESS1;INJAMIN;INJAMTELP;INJAMADD1;INJAMHUB;PHONE;', OnGetSelectedFieldValues);
                cplMain.PerformCallback("SLIK;");
                
                cplMain.PerformCallback("LUAPPNO_CHANGED;" + luAppNo.GetValue().toString());
                cplMain.PerformCallback("DBR;");
            }
        }

        function OnGetSelectedFieldValues(selectedValues) {

            //deb
            txtDebitur.SetValue(selectedValues[1]);
            txtTglLahirDeb.SetValue(selectedValues[2]);
            txtMaritalStat.SetValue(selectedValues[3]);
            txtSpouName.SetValue(selectedValues[4]);
            mmAddress.SetValue(selectedValues[5]);

            //emergency
            txtPenjamin.SetValue(selectedValues[6]);
            txtPhonePenjamin.SetValue(selectedValues[7]);
            mmAddressPenjamin.SetValue(selectedValues[8]);
            txtRelation.SetValue(selectedValues[9]);
            txtPhoneDebitur.SetValue(selectedValues[10]);

            //set 0
            //txtInsSLIK.SetValue(0);
            txtDBR.SetValue("");
            txtIncome.SetValue(0);
            txtIncomeOther.SetValue(0);
            txtIncomePenjamin.SetValue(0);
            txtIncomePasanganPenjamin.SetValue(0);
            txtInsDeb.SetValue(0);
            txtInsSpouse.SetValue(0);
            txtInsChild.SetValue(0);
            txtInsOther.SetValue(0);
            txtFreeCashFlow.SetValue("");
            txtBiayaHidup.SetValue(0);
        }
    </script>

    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout" ClientInstanceName="ASPxFormLayout" runat="server" Theme="Glass" ColCount="1">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutItem ShowCaption="True" Caption="SMILE Application No." Width="30%">
                <CaptionSettings Location="Left"/>
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxGridLookup runat="server" ID="luAppNo" ClientInstanceName="luAppNo" KeyFieldName="APPLICNO" OnDataBinding="gvNoApp_DataBinding" DisplayFormatString="{0}" TextFormatString="{0}" MultiTextSeparator=";" Width="100%">
                            <ClientSideEvents Init="function(s, e) { OnLuAppNoChanged(s); }" ValueChanged="function(s, e) { OnLuAppNoChanged(s); }"/>
                            <GridViewProperties EnablePagingCallbackAnimation="true">
                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                            </GridViewProperties>
                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxGridLookup>
                        <dx:ASPxTextBox runat="server" ID="txtAppNo" ClientInstanceName="txtAppNo" ClientEnabled="false" Visible="false"></dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>

            <dx:LayoutGroup Name="LayoutGroupListDebitur" ShowCaption="True" Caption="Debitur" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="true" Caption="Nama Debitur" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDebitur" ClientInstanceName="txtDebitur" ClientEnabled="false"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="No HP" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtPhoneDebitur" ClientInstanceName="txtPhoneDebitur" ClientEnabled="false"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Tanggal Lahir" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtTglLahirDeb" ClientInstanceName="txtTglLahirDeb" ClientEnabled="false"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Status Marital" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtMaritalStat" ClientInstanceName="txtMaritalStat" ClientEnabled="false"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Nama Pasangan" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtSpouName" ClientInstanceName="txtSpouName" ClientEnabled="false"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Address" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmAddress" ClientInstanceName="mmAddress" Height="100px" ClientEnabled="false">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                     <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupListPenjamin" ShowCaption="True" Caption="Emergency Contact" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="true" Caption="Nama Emergency Contact" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtPenjamin" ClientInstanceName="txtPenjamin" ClientEnabled="false"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="No HP Emergency Contact" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtPhonePenjamin" ClientInstanceName="txtPhonePenjamin" ClientEnabled="false"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Hubungan dengan debitur" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtRelation" ClientInstanceName="txtRelation" ClientEnabled="false"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Address" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmAddressPenjamin" ClientInstanceName="mmAddressPenjamin" Height="100px" ClientEnabled="false">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>

            <dx:LayoutGroup Name="LayoutGroupListDBR" ShowCaption="True" Caption="DBR & Free Cash Flow" GroupBoxDecoration="Box" ColCount="2" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="true" Caption="Income Debitur" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <%--<dx:ASPxTextBox runat="server" ID="txtIncomeSalary" ClientInstanceName="txtIncomeSalary" ClientEnabled="true"></dx:ASPxTextBox>--%>
                                <dx:ASPxSpinEdit ID="txtIncome" runat="server" ClientInstanceName="txtIncome" Number="0" DisplayFormatString="{0:#,0}" NullText="0" Width="250px">
                                    <ClientSideEvents KeyUp="function(s,e) { cplMain.PerformCallback('DBR;'); }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Other Income" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtIncomeOther" runat="server" ClientInstanceName="txtIncomeOther" Number="0" DisplayFormatString="{0:#,0}" NullText="0" Width="250px">
                                    <ClientSideEvents KeyUp="function(s,e) { cplMain.PerformCallback('DBR;'); }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Income Penjamin" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtIncomePenjamin" runat="server" ClientInstanceName="txtIncomePenjamin" Number="0" DisplayFormatString="{0:#,0}" NullText="0" Width="250px">
                                    <ClientSideEvents KeyUp="function(s,e) { cplMain.PerformCallback('DBR;'); }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Income Pasangan Penjamin" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtIncomePasanganPenjamin" runat="server" ClientInstanceName="txtIncomePasanganPenjamin" Number="0" DisplayFormatString="{0:#,0}" NullText="0" Width="250px">
                                    <ClientSideEvents KeyUp="function(s,e) { cplMain.PerformCallback('DBR;'); }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Angsuran SLIK" ColSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtInsSLIK" ClientInstanceName="txtInsSLIK" ClientEnabled="false" DisplayFormatString="{0:#,0}" Width="250px"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Angsuran Debitur" ColSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtInsDeb" runat="server" ClientInstanceName="txtInsDeb" Number="0" DisplayFormatString="{0:#,0}" Width="250px">
                                    <ClientSideEvents KeyUp="function(s,e) { cplMain.PerformCallback('DBR;'); }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Angsuran Pasangan Debitur" ColSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtInsSpouse" runat="server" ClientInstanceName="txtInsSpouse" Number="0" DisplayFormatString="{0:#,0}" Width="250px">
                                    <ClientSideEvents KeyUp="function(s,e) { cplMain.PerformCallback('DBR;'); }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Angsuran Anak Debitur" ColSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtInsChild" runat="server" ClientInstanceName="txtInsChild" Number="0" DisplayFormatString="{0:#,0}" Width="250px">
                                    <ClientSideEvents KeyUp="function(s,e) { cplMain.PerformCallback('DBR;'); }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Angsuran Anggota Keluarga Lainnya" ColSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtInsOther" runat="server" ClientInstanceName="txtInsOther" Number="0" DisplayFormatString="{0:#,0}" Width="250px">
                                    <ClientSideEvents KeyUp="function(s,e) { cplMain.PerformCallback('DBR;'); }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Hasil Perhitungan DBR" ColSpan="2" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDBR" ClientInstanceName="txtDBR" ClientEnabled="false" Width="250px"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Biaya Hidup" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtBiayaHidup" runat="server" ClientInstanceName="txtBiayaHidup" Number="0" DisplayFormatString="{0:#,0}"  Width="250px">
                                    <ClientSideEvents KeyUp="function(s,e) { cplMain.PerformCallback('DBR;'); }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Free Cash Flow" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtFreeCashFlow" ClientInstanceName="txtFreeCashFlow" ClientEnabled="false" Width="250px"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Remarks" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmRemarks" ClientInstanceName="mmRemarks" Width="250px" Height="100px">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>

            <dx:LayoutGroup Name="LayoutGroupListSurvey" ShowCaption="True" Caption="Hasil Survey" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Survey Object" Width="30%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" ID="cbSurveyObj" ClientInstanceName="cbSurveyObj">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) { OnObjectChange(s); }" />
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:TabbedLayoutGroup Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Height="100px" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                        <Items>
                            <dx:LayoutGroup Caption="Survey">
                                <items>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridView
                                                    ID="gvSurveyItem"
                                                    runat="server"
                                                    ClientInstanceName="gvSurveyItem"
                                                    KeyFieldName="APPLICNO"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    OnDataBinding="gvSurveyItem_DataBinding">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Name="colNo" Caption="No." FieldName="No" ReadOnly="True" UnboundType="String" Width="3%">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colItemDesc" Caption="Item Descirption" ReadOnly="True" FieldName="Item">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataCheckColumn Name="colCheck" Caption="Agreement" UnboundType="Boolean">  
                                                            <DataItemTemplate>  
                                                                <dx:ASPxCheckBox ID="chkItem" runat="server" AllowGrayed="false" ValueType="System.Boolean"  
                                                                    ValueChecked="true" ValueUnchecked="false">  
                                                                </dx:ASPxCheckBox>  
                                                            </DataItemTemplate>  
                                                        </dx:GridViewDataCheckColumn>  
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridView
                                                    ID="gvLoadSurvey"
                                                    runat="server"
                                                    ClientInstanceName="gvLoadSurvey"
                                                    Visible="false"
                                                    KeyFieldName="survey_item"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Name="colNo2" Caption="No." FieldName="No" ReadOnly="True" UnboundType="String" Width="3%">
                                                            <HeaderStyle Font-Bold="true"/>
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colItemDesc2" Caption="Item Descirption" ReadOnly="True" FieldName="Item">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colAgreement" Caption="Agreement" ReadOnly="True" FieldName="Jawaban">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
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
            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
            <dx:LayoutItem ShowCaption="True" Caption="" Width="30%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <%--<dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Height="30px" Text="Save" Theme="MaterialCompact" OnClick="btnSave_Click">
                            
                        </dx:ASPxButton>--%>

                        <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" AutoPostBack="false" Height="30px" Text="Save" Theme="MaterialCompact" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVE;' + 'CLICK'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
