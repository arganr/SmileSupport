<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PeminjamanDocEntry.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.PeminjamanDocument.PeminjamanDocEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "LOAD":
                    break;
            }
            cplMain.cpCallbackParam = null;
        }

        
    </script>

    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout" ClientInstanceName="ASPxFormLayout" runat="server" Theme="Glass" Width="100%" >
        <Items>
            <dx:LayoutGroup Name="LayoutGroupAddItem" ShowCaption="True" Caption="Peminjaman Document" GroupBoxDecoration="Box" ColCount="3" >
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Doc Key">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDocKey" ClientInstanceName="txtDocKey" Width="100%" ReadOnly="true">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Doc No" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDocNo" ClientInstanceName="txtDocNo" Width="100%" ReadOnly="true">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Doc Date">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deDocDate" ClientInstanceName="deDocDate" Width="100%" ReadOnly="true">
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Category" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtCategory" ClientInstanceName="txtCategory" Width="100%" ReadOnly="true">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Department" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDept" ClientInstanceName="txtDept" Width="100%" ReadOnly="true">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Status" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <%--<dx:ASPxTextBox runat="server" ID="txtStatus" ClientInstanceName="txtStatus" Width="100%" ReadOnly="true">
                                </dx:ASPxTextBox>--%>

                                <dx:ASPxComboBox ID="cbStatusHDR" runat="server" ClientInstanceName="cbStatusHDR" Width="100%" OnSelectedIndexChanged="cbStatusHDR_SelectedIndexChanged" AutoPostBack="false">
                                    <Items>
                                        <dx:ListEditItem Value="OPEN" Text="OPEN"/>
                                        <dx:ListEditItem Value="ON USER" Text="ON USER"/>
                                        <dx:ListEditItem Value="CLOSE" Text="CLOSE"/>
                                    </Items>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Keperluan" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="txtKeperluan" ClientInstanceName="txtKeperluan" Width="100%" ReadOnly="true">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Remarks" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="txtRemarks" ClientInstanceName="txtRemarks" Width="100%" ReadOnly="true">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Tgl Peminjaman" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deTglPinjam" ClientInstanceName="deTglPinjam" Width="100%" ReadOnly="true"></dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Tgl Pengembalian" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deTglKembali" ClientInstanceName="deTglKembali" Width="100%" ReadOnly="true"></dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                    <dx:TabbedLayoutGroup Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Height="100px" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee" ColSpan="3">
                        <Styles>
                            <ContentStyle Font-Names="Calibri"></ContentStyle>
                        </Styles>
                        <Items>
                            <dx:LayoutGroup GroupBoxDecoration="None" Caption="Detail" GroupBoxStyle-Caption-BackColor="White" ColCount="3">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridView
                                                    runat="server"
                                                    ID="gvDetail"
                                                    ClientInstanceName="gvDetail"
                                                    Width="100%"
                                                    KeyFieldName="DtlKey"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Aqua"
                                                    Font-Names="Calibri"
                                                    OnDataBinding="gvDetail_DataBinding">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" />
                                                    <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Name="colDtlKey" Caption="DtlKey" FieldName="DtlKey" ReadOnly="True" Visible="false" VisibleIndex="0">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="1">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colSeq" Caption="Seq" FieldName="Seq" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="2">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colDocID" FieldName="DocID" ShowInCustomizationForm="True" Caption="Document" Width="20%" VisibleIndex="3">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#1872c4" />
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colDescription" FieldName="Description" ShowInCustomizationForm="True" Caption="Description" Width="20%" ReadOnly="true" VisibleIndex="4">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colStatus" FieldName="Status" ShowInCustomizationForm="True" Caption="Status" Width="20%" ReadOnly="true" VisibleIndex="5" Visible="false">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colCbStatus" FieldName="Status" ShowInCustomizationForm="True" Caption="Status" Width="20%" VisibleIndex="6" Visible="false">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#1872c4" />
                                                            <DataItemTemplate>
                                                                <dx:ASPxComboBox ID="cbStatus" runat="server" Width="100%">
                                                                    <Items>
                                                                        <dx:ListEditItem Value="New" Text="New"/>
                                                                        <dx:ListEditItem Value="On User" Text="On User"/>
                                                                        <dx:ListEditItem Value="Close" Text="Close"/>
                                                                    </Items>
                                                                </dx:ASPxComboBox>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataComboBoxColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
                    </dx:TabbedLayoutGroup>
                    <dx:EmptyLayoutItem ColSpan="3"></dx:EmptyLayoutItem>

                    <dx:LayoutItem ShowCaption="True" Caption="Note Approval" Name="NoteApproval">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmNoteApproval" ClientInstanceName="mmNoteApproval" Width="100%" ReadOnly="true">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="3"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Date Approval">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deDateApproval" ClientInstanceName="deDateApproval" Width="100%" Enabled="false"></dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem ColSpan="3"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnCancel" ClientInstanceName="btnCancel" Height="30px" Text="Cancel" OnClick="btnCancel_Click" Theme="MaterialCompact" BackColor="Gray">  
                                </dx:ASPxButton>
                                <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Height="30px" Text="Save" OnClick="btnSave_Click" Theme="MaterialCompact" ValidationGroup="ValidationSave">  
                                </dx:ASPxButton>
                                <dx:ASPxButton runat="server" ID="btnReject" ClientInstanceName="btnReject" Height="30px" Text="Reject" OnClick="btnReject_Click" Theme="MaterialCompact" ValidationGroup="ValidationSave" BackColor="Red" Visible="false">  
                                </dx:ASPxButton>
                                <dx:ASPxButton runat="server" ID="btnApprove" ClientInstanceName="btnApprove" Height="30px" Text="Approve" OnClick="btnApprove_Click" Theme="MaterialCompact" ValidationGroup="ValidationSave" Visible="false">  
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
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Modal="True" BackgroundImage-HorizontalPosition="center" Theme="Glass" Text="Loading Please Wait...">
        <BackgroundImage VerticalPosition="top" />
    </dx:ASPxLoadingPanel>
</asp:Content>
