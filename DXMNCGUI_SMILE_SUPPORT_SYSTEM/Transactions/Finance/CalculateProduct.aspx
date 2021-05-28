<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="CalculateProduct.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.CalculateProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "ACTION":
                    break;
                case "SAVE":
                    lblIncome.SetValue(cplMain.cpTotal);
                    lblTotalOther.SetValue(cplMain.cpTotalOther);
                    break;
                case "CLEAR":
                    gvProduct.SetValue('');
                    lblIncome.SetValue('0');
                    lblTotalOther.SetValue('0');
                    txtKet.SetValue('');
                    txtAmt.SetValue('0');
                    txtRateJual.SetValue('0');
                    txtOtherAmt.SetValue('0');
                    txtSurveyAmt.SetValue('0');
                    break;
            }
            cplMain.cpCallbackParam = null;
        }

        function OnProductChanged(product) {
            var grid = product.GetGridView();
            if (product.GetText() != "") {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'Product;adminfee;provision;id;', OnGetSelectedFieldValues);
            }
        }

        function OnGetSelectedFieldValues(selectedValues) {
            HiddenField.Set("fee", selectedValues[1]);
            HiddenField.Set("provision", selectedValues[2]);
            HiddenField.Set("id", selectedValues[3]);
        }
    </script>

    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>

    
    <dx:ASPxFormLayout ID="ASPxFormLayout" ClientInstanceName="ASPxFormLayout" runat="server" Theme="Glass" Width="50%">
        <Items>
            <dx:LayoutGroup Name="LayoutGroupListDebitur" ShowCaption="True" Caption="Kalkulator Pendapatan Awal (Estimasi)" GroupBoxDecoration="Box" ColCount="1" >
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Pilih Product" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup runat="server" ID="gvProduct" ClientInstanceName="gvProduct" KeyFieldName="Product" OnDataBinding="gvProduct_DataBinding" DisplayFormatString="{0}" TextFormatString="{0}" MultiTextSeparator=";" Width="100%">
                                    <ClientSideEvents Init="function(s, e) { OnProductChanged(s); }" ValueChanged="function(s, e) { OnProductChanged(s); }"/>
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="Product" FieldName="Product" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Admin Fee" FieldName="adminfee" ShowInCustomizationForm="True" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Provision" FieldName="provision" ShowInCustomizationForm="True" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="id" FieldName="id" ShowInCustomizationForm="True" Visible="false">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Input Pembiayaan" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtAmt" runat="server" ClientInstanceName="txtAmt" Number="0" DisplayFormatString="{0:#,0}" Width="100%">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Input Rate Jual (%)" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtRateJual" runat="server" ClientInstanceName="txtRateJual" Number="0" DisplayFormatString="{0:#,0.0}" Width="100%">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Input Biaya Survey" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtSurveyAmt" runat="server" ClientInstanceName="txtSurveyAmt" Number="0" DisplayFormatString="{0:#,0}" Width="100%">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Input Biaya Akuisisi Lainnya (Jika Ada)" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="txtOtherAmt" runat="server" ClientInstanceName="txtOtherAmt" Number="0" DisplayFormatString="{0:#,0}" Width="100%">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Keterangan" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="txtKet" ClientInstanceName="txtKet" Width="100%" Height="100px" Theme="MetropolisBlue"></dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnClear" ClientInstanceName="btnClear" AutoPostBack="false" Height="30px" Text="Clear" BackColor="Gray" HoverStyle-BackColor="Red" Theme="MaterialCompact">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('CLEAR;' + 'CLICK'); }" />
                                </dx:ASPxButton>
                                &nbsp;
                                <dx:ASPxButton runat="server" ID="btnCalculate" ClientInstanceName="btnCalculate" AutoPostBack="false" Height="30px" Text="Calculate" Theme="MaterialCompact">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVE;' + 'CLICK'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Estimasi Pendapatan Awal" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="lblIncome" Font-Size="Large" ClientInstanceName="lblIncome" Width="100%" Text="0,0"></dx:ASPxLabel>
                                <%--<dx:ASPxTextBox runat="server" ID="txtIncome" ClientInstanceName="txtIncome" ClientEnabled="false" DisplayFormatString="{0:#,0}" Width="100%"></dx:ASPxTextBox>--%>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Total Biaya Akuisisi" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="lblTotalOther" Font-Size="Large" ClientInstanceName="lblTotalOther" Width="100%" Text="0,0"></dx:ASPxLabel>
                                <%--<dx:ASPxTextBox runat="server" ID="txtTotalOther" ClientInstanceName="txtTotalOther" ClientEnabled="false" DisplayFormatString="{0:#,0}" Width="100%"></dx:ASPxTextBox>--%>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="ASPxLabel1" Font-Size="Smaller" ClientInstanceName="ASPxLabel1" Width="100%" Text="Catatan"></dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="ASPxLabel4" Font-Size="Smaller" ClientInstanceName="ASPxLabel4" Width="100%" Text="Pendapatan awal adalah perkiraan pendapatan yang diperoleh di bulan pertama dari angsuran berjalan (belum memperhitungkan expense cabang)."></dx:ASPxLabel>
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
