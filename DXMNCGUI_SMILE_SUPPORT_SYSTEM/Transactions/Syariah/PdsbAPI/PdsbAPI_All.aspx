<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" Async="true" AutoEventWireup="true" CodeBehind="PdsbAPI_All.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.PdsbAPI.PdsbAPI_All" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function OnluKontrakChanged(luKontrak) {
            var grid = luKontrak.GetGridView();
            if (luKontrak.GetText() != "") {

                grid.GetRowValues(grid.GetFocusedRowIndex(), 'LSAGREE;name;', OnGetSelectedFieldValues);
            }
        }

        function OnGetSelectedFieldValues(selectedValues) {
            txtName.SetValue(selectedValues[1]);
        }
    </script>
    <dx:ASPxFormLayout ID="formLayout" runat="server" ClientInstanceName="formLayout" Theme="Glass" Font-Names="Arial" ColCount="1">
        <Items>
            <dx:LayoutGroup Name="LayoutGroupAPI" ShowCaption="True" Caption="PDSB API" GroupBoxDecoration="Box" ColCount="1" Width="30%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="No Kontrak" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup runat="server" ID="luKontrak" ClientInstanceName="luKontrak" KeyFieldName="LSAGREE" DataSourceID="sdsKontrak" DisplayFormatString="{0}" TextFormatString="{0}" MultiTextSeparator=";">
                                    <ClientSideEvents Init="function(s, e) { OnluKontrakChanged(s); }" ValueChanged="function(s, e) { OnluKontrakChanged(s); }"/>
                                    <%--<GridViewProperties EnablePagingCallbackAnimation="true">
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                    </GridViewProperties>--%>
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="No Kontrak" FieldName="LSAGREE" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Nama" FieldName="name" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Nama Nasabah" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtName" ClientInstanceName="txtName" ReadOnly="true">
                                    
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Kode Cabang" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup runat="server" ID="luBranch" ClientInstanceName="luBranch" KeyFieldName="kode_cabang" DataSourceID="sdsBranch" DisplayFormatString="{1}" TextFormatString="{1}" MultiTextSeparator=";">
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="Kode Cabang" FieldName="kode_cabang" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Nama Cabang" FieldName="nama_cabang" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <%--<dx:ASPxTextBox runat="server" ID="txtResult" ClientInstanceName="txtResult" ReadOnly="true">
                                    
                                </dx:ASPxTextBox>--%>
                                <label runat="server" ID="lblResult"></label>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnAPI" ClientInstanceName="btnAPI" Height="30px" Text="Generate" Theme="MaterialCompact" OnClick="btnAPI_Click">
                                    <ClientSideEvents Click="function(s, e) { lp.Show(); } " /> 
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>

    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Modal="True" BackgroundImage-HorizontalPosition="center" Theme="Glass" Text="Loading Please Wait...">
        <BackgroundImage VerticalPosition="top" />
    </dx:ASPxLoadingPanel>

    <asp:SqlDataSource ID="sdsBranch" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="select kode_cabang, nama_cabang from dbo.branch_pdsb">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsKontrak" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="SELECT LSAGREE, name FROM LS_AGREEMENT la join SYS_COMPANY sc on la.C_CODE = sc.c_code where 1=1 and la.PRODUCT_FACILITY_CODE = '112'">
    </asp:SqlDataSource>
</asp:Content>
