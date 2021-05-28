<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="SimulationReschedulingEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.SimulationRescheduling.SimulationReschedulingEntry" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function cplMain_EndCallback(s, e)
        {
            switch (cplMain.cpCallbackParam)
            {
                case "CALCULATE":
                    gvMain.Refresh();
                    lblClientName.SetText(cplMain.cpllblClientName);
                    lblEffRate.SetText(cplMain.cplblEffRate);
                    lblEffDate.SetText(cplMain.cplblEffDate);
                    lblOutstandingDenda.SetText(cplMain.cplblOutstandingDenda);
                    lblAccruedInterest.SetText(cplMain.cplblAccruedInterest);
                    lblInsuranceDueAmt.SetText(cplMain.cpllblInsuranceDueAmt);
                    seRestructureFee.SetText("0.00");
                    seInsuranceRenewalFee.SetText("0.00");
                    break;
                case "CALCULATE_TOTAL":
                    seTotalChargetoCustomer.GetInputElement().readOnly = true;
                    calculationNTF();
                    break;
                case "PRINT":
                    var DocNo = "ABCDE";
                    //window.open("../../../Shared/DocViewer.aspx?ReportType=RESCHDULING");
                    break;
            }
            cplMain.cpCallbackParam = null;
        }
        function calculationNTF() {
            var vOutDenda = parseFloat(0.0);
            if (lblOutstandingDenda.GetValue() != null && lblOutstandingDenda.GetValue().toString() != "" && lblOutstandingDenda.GetValue().toString().length != 0) {
                vOutDenda = parseFloat(lblOutstandingDenda.GetValue().toString().replace(',', '').replace(',', ''));
            }
            var vAccuredInterest = parseFloat(0.0);
            if (lblAccruedInterest.GetValue() != null && lblAccruedInterest.GetValue().toString() != "" && lblAccruedInterest.GetValue().toString().length != 0) {
                vAccuredInterest = parseFloat(lblAccruedInterest.GetValue().replace(',', '').replace(',', ''));
            }
            var vInsuranceDueAmt = parseFloat(0.0);
            if (lblInsuranceDueAmt.GetValue() != null && lblInsuranceDueAmt.GetValue().toString() != "" && lblInsuranceDueAmt.GetValue().toString().length != 0) {
                vInsuranceDueAmt = parseFloat(0.00);
            }
            var vRestruckFee = parseFloat(0.0);
            if (seRestructureFee.GetValue() != null && seRestructureFee.GetValue().toString() != "" && seRestructureFee.GetValue().toString().length != 0) {
                vRestruckFee = parseFloat(seRestructureFee.GetValue().toString());
            }
            var vInsuranceRenewalFee = parseFloat(0.0);
            if (seInsuranceRenewalFee.GetValue() != null && seInsuranceRenewalFee.GetValue().toString() != "" && seInsuranceRenewalFee.GetValue().toString().length != 0) {
                vInsuranceRenewalFee = parseFloat(seInsuranceRenewalFee.GetValue().toString());
            }

            seTotalChargetoCustomer.SetValue(vOutDenda + vAccuredInterest + vInsuranceDueAmt + vRestruckFee + vInsuranceRenewalFee);
        }
    </script>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMain">
        <PageHeader>
        </PageHeader>
    </dx:ASPxGridViewExporter>
    <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" Theme="Glass">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup Name="LayoutGroupSimulationReschedulingTools" ShowCaption="True" Caption="Simulation Rescheduling Tools" GroupBoxDecoration="Box" ColCount="3" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="true" Caption="Agreement No.">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtAgreeNo" ClientInstanceName="txtAgreeNo">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="New Tenor (Include Existing)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit runat="server" ID="seNewTenor" ClientInstanceName="seNewTenor" Number="0">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Effective Rate">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit runat="server" ID="seEffRate" ClientInstanceName="seEffRate" Number="0" DisplayFormatString="#,0.00">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Number of Step Down 1">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit runat="server" ID="seStepDown" ClientInstanceName="seStepDown" Number="0">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Effective Date">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deEffDate" ClientInstanceName="deEffDate" DisplayFormatString="dd/MM/yyyy" NullText="-- Select --">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Step Down Amount 1">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit runat="server" ID="seStepDownAmount" ClientInstanceName="seStepDownAmount" Number="0" DisplayFormatString="#,0.00">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Number of Step Down 2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit runat="server" ID="seStepDown2" ClientInstanceName="seStepDown2" Number="0">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Step Down Amount 2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit runat="server" ID="seStepDownAmount2" ClientInstanceName="seStepDownAmount2" Number="0" DisplayFormatString="#,0.00">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnGenerate" ClientInstanceName="btnGenerate" Text="Calculate New Amortize" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('CALCULATE;CALCULATE') }"/>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupSimulationReschedulingInformation" ShowCaption="True" Caption="Restructure Information" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="true" Caption="Client Name">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="lblClientName" ClientInstanceName="lblClientName" Text="-">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Effective Rate">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="lblEffRate" ClientInstanceName="lblEffRate" Text="-">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Effective Date">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="lblEffDate" ClientInstanceName="lblEffDate" Text="-"></dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupSimulationReschedulingRestructureCalculation" ShowCaption="True" Caption="Restructure Calculation" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="true" Caption="Outstanding Installment">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="lblOutstandingDenda" ClientInstanceName="lblOutstandingDenda" Text="-">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Accrued Interest">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="lblAccruedInterest" ClientInstanceName="lblAccruedInterest" Text="-">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Insurance Due Amt">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="lblInsuranceDueAmt" ClientInstanceName="lblInsuranceDueAmt" Text="-">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Restructure Fee">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit runat="server" ID="seRestructureFee" ClientInstanceName="seRestructureFee" Increment="1000000" DisplayFormatString="#,0.00" Width="20%">
                                    <ClientSideEvents ValueChanged="function(s, e) { cplMain.PerformCallback('CALCULATE_TOTAL;CALCULATE_TOTAL') }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Insurance Renewal Fee">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit runat="server" ID="seInsuranceRenewalFee" ClientInstanceName="seInsuranceRenewalFee" Width="20%" DisplayFormatString="#,0.00">
                                    <ClientSideEvents ValueChanged="function(s, e) { cplMain.PerformCallback('CALCULATE_TOTAL;CALCULATE_TOTAL') }" />
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Total Charge to Customer">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit runat="server" ID="seTotalChargetoCustomer" ClientInstanceName="seTotalChargetoCustomer" Font-Bold="true" Font-Size="10" DisplayFormatString="#,0.00" Width="20%" ReadOnly="true">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="Server" ID="btnExport" Text="Print" Image-Url="~/Content/Images/PrintIcon-16x16.png" Width="10%" OnClick="btnExport_Click" AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('PRINT;PRINT') }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutGroup Name="LayoutGroupSimulationReschedulingNewAmortization" ShowCaption="True" Caption="New Amortization" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView 
                                    runat="server" 
                                    ID="gvMain" 
                                    ClientInstanceName="gvMain" 
                                    OnDataBinding="gvMain_DataBinding"
                                    Width="100%" 
                                    KeyFieldName="Tenor"
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    EnableTheming="True"
                                    Font-Names="Calibri">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" ShowFooter="true"/>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                    <SettingsSearchPanel Visible="false" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsPager PageSize="10000" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Name="colTenor" Caption="Tenor" FieldName="Tenor">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDueDate" Caption="Due Date" FieldName="DueDate" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colInstallmentAmount" Caption="Installment Amount" FieldName="Installment Amount">
                                            <PropertiesSpinEdit DisplayFormatString="#,0.00"></PropertiesSpinEdit>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colPrincipal" Caption="Principal" FieldName="Principal">
                                            <PropertiesSpinEdit DisplayFormatString="#,0.00"></PropertiesSpinEdit>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colInterest" Caption="Interest" FieldName="Interest">
                                            <PropertiesSpinEdit DisplayFormatString="#,0.00"></PropertiesSpinEdit>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colOSPrincipal" Caption="Os Principal" FieldName="Os Principal">
                                            <PropertiesSpinEdit DisplayFormatString="#,0.00"></PropertiesSpinEdit>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colOSInterest" Caption="Os Interest" FieldName="OS Interest">
                                            <PropertiesSpinEdit DisplayFormatString="#,0.00"></PropertiesSpinEdit>
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true"/>
                                        </dx:GridViewDataSpinEditColumn>
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
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback" >
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
