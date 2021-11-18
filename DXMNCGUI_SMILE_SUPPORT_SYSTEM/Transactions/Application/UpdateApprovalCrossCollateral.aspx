<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="UpdateApprovalCrossCollateral.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.UpdateApprovalCrossCollateral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout" ClientInstanceName="ASPxFormLayout" runat="server" Theme="Glass" Width="100%">
        <Items>
            <dx:LayoutGroup Name="LayoutGroupAddItem" ShowCaption="True" Caption="Cross Collateral Maintenance" GroupBoxDecoration="Box" ColCount="2" >
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Cross Collateral" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtCrossColName" ClientInstanceName="txtCrossColName" ReadOnly="true" Width="30%"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False"  ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvTempData"
                                    runat="server"
                                    ClientInstanceName="gvTempData"
                                    OnDataBinding="gvTempData_DataBinding"
                                    OnHtmlDataCellPrepared="gvTempData_HtmlDataCellPrepared"
                                    Width="100%">
                                    <SettingsBehavior AllowDragDrop="false" AllowSort="false"/>
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="LSAGREE" Caption="Agreement No" VisibleIndex="0">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NAME" Caption="Nama" VisibleIndex="1">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ASSET_DESCS" Caption="Desc" VisibleIndex="2">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="REMARKS" Caption="Remarks" VisibleIndex="3">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="updateType" Caption="Alteration" VisibleIndex="4">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CODE" Caption="CODE" VisibleIndex="5" Visible="false">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataCheckColumn Name="colCheck" Caption="Approval" UnboundType="Boolean" VisibleIndex="6">
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
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Remarks" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmRemarks" ClientInstanceName="mmRemarks" Width="30%">

                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>

                    <dx:LayoutItem ShowCaption="false"  Caption=" " ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnReject" ClientInstanceName="btnReject" Height="30px" Text="Reject" BackColor="Gray" OnClick="btnReject_Click" HoverStyle-BackColor="Red" Theme="MaterialCompact">
                                </dx:ASPxButton>
                                <dx:ASPxButton runat="server" ID="btnApprove" ClientInstanceName="btnApprove" Height="30px" Text="Approve" OnClick="btnApprove_Click" AutoPostBack="false" Theme="MaterialCompact">
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem ></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Log Data"  ColSpan="2">
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False"  ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvLogData"
                                    runat="server"
                                    ClientInstanceName="gvLogData"
                                    OnDataBinding="gvLogData_DataBinding"
                                    Width="100%">
                                    <SettingsBehavior AllowDragDrop="false" AllowSort="false" AllowFocusedRow="false"/>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
