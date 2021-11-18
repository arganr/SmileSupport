<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="DocLocationEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.PeminjamanDocument.MasterDocLoacation.DocLocationEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "LOAD":
                    break;
            }
            cplMain.cpCallbackParam = null;
        }

        function OnluKontrakChanged(luKontrak) {
            var grid = luKontrak.GetGridView();
            if (luKontrak.GetText() != "") {

                grid.GetRowValues(grid.GetFocusedRowIndex(), 'NAME;', OnGetSelectedFieldValues);
            }
        }

        function OnGetSelectedFieldValues(selectedValues) {
            mmDesc.SetValue(selectedValues[0]);
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout" ClientInstanceName="ASPxFormLayout" runat="server" Theme="Glass" Width="30%" >
        <Items>
            <dx:LayoutGroup Name="LayoutGroupAddItem" ShowCaption="True" Caption="Mater Document Location" GroupBoxDecoration="Box" ColCount="3" >
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Doc Category" ColSpan="3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" ID="cbCategory" ClientInstanceName="cbCategory" DropDownStyle="DropDownList" Height="23px" Width="100%">
                                    <Items>
                                        <dx:ListEditItem Text="FILE MARKETING" Value="FILE MARKETING" Selected="true" />
                                    </Items>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Doc ID" ColSpan="3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup runat="server" ID="luKontrak" ClientInstanceName="luKontrak" KeyFieldName="LSAGREE" DataSourceID="sdsKontrak" DisplayFormatString="{0}" TextFormatString="{0}" MultiTextSeparator=";" Width="100%">
                                    <GridViewProperties EnablePagingCallbackAnimation="true">
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                    </GridViewProperties>
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="No Kontrak" FieldName="LSAGREE" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Nama" FieldName="NAME" ShowInCustomizationForm="True" Visible="true" VisibleIndex="1">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Asset" FieldName="DESCS" ShowInCustomizationForm="True" Visible="true" VisibleIndex="2">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                    <ClientSideEvents Init="function(s, e) { OnluKontrakChanged(s); }" ValueChanged="function(s, e) { OnluKontrakChanged(s); }"/>
                                </dx:ASPxGridLookup>
                                <dx:ASPxTextBox runat="server" ID="txtEditKontrak" ClientInstanceName="txtEditKontrak" Width="100%" ClientVisible="false" ReadOnly="true">
                                    
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Doc Description" ColSpan="3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmDesc" ClientInstanceName="mmDesc" Width="100%">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Doc Location" ColSpan="3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <%--<dx:ASPxGridLookup runat="server" ID="luLocation" ClientInstanceName="luLocation" KeyFieldName="DocKey" DataSourceID="sdsLocation" DisplayFormatString="{1}" TextFormatString="{1}" MultiTextSeparator=";" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="Id" FieldName="DocKey" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Location" FieldName="LocationName" ShowInCustomizationForm="True" Visible="true" VisibleIndex="1">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxGridLookup>--%>
                                <dx:ASPxComboBox runat="server" ID="cbLocation" ClientInstanceName="cbLocation" DropDownStyle="DropDownList" Height="23px" Width="100%">
                                    <Items>
                                        <dx:ListEditItem Text="CUSTODIAN" Value="CUSTODIAN" />
                                        <dx:ListEditItem Text="MMI" Value="MMI" />
                                    </Items>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Reference ID (Optional)" ColSpan="3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtRefNum" ClientInstanceName="txtRefNum" Width="100%">
                                    
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="3"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                 <dx:ASPxButton runat="server" ID="btnCancel" ClientInstanceName="btnCancel" Height="30px" Text="Cancel" OnClick="btnCancel_Click" Theme="MaterialCompact" BackColor="Gray">  
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Height="30px" Text="Save" OnClick="btnSave_Click" Theme="MaterialCompact" ValidationGroup="ValidationSave">  
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%--<dx:LayoutItem ShowCaption="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnDelete" ClientInstanceName="btnDelete" Height="30px" Text="Delete" OnClick="btnDelete_Click" Theme="MaterialCompact" BackColor="Red" ClientVisible="false">  
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>--%>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>

    <asp:SqlDataSource ID="sdsKontrak" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="SELECT LSAGREE, NAME, DESCS FROM LS_AGREEMENT WHERE LSAGREE not in(SELECT DocID FROM [DBNONCORE].[SSS].[dbo].[mstDocLocation]) ">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsLocation" runat="server" ConnectionString="<%$ ConnectionStrings:SqlLocalConnectionString %>" ProviderName="<%$ ConnectionStrings:SqlConnectionString.ProviderName %>"
        SelectCommand="select DocKey, LocationName from mstLocationName where isActive = 1">
    </asp:SqlDataSource>

    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Modal="True" BackgroundImage-HorizontalPosition="center" Theme="Glass" Text="Loading Please Wait...">
        <BackgroundImage VerticalPosition="top" />
    </dx:ASPxLoadingPanel>
    
</asp:Content>
