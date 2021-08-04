<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="EditCrossCollateral.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.EditCrossCollateral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "ACTION":
                    break;
                case "ADD":
                    if (cplMain.cplblmessageError.length > 0) {
                        gvNewItem.SetValue(null);
                        apcalert.SetContentHtml(cplMain.cpAlertMessage);
                        apcalert.Show();
                        break;
                    } else {
                        gvNewItem.SetValue(null);
                        gvTempData.Refresh();
                        break;
                    }
                case "LOAD":
                    gvLogData.Refresh();
                    gvTempData.Refresh();
                    gvNewItem.Refresh();
                    
                    break;
                case "DELETE":
                    gvTempData.Refresh();
                    break;
                case "SAVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
            }
            cplMain.cpCallbackParam = null;
        }

        function OnCodeChanged(ccode) {
            var grid = gvCrossColl.GetGridView();
            if (gvCrossColl.GetText() != "") {
                cplMain.PerformCallback("LOAD;" + gvCrossColl.GetValue().toString());
            }
        }

        function gvTempData_CustomButtonClick(s, e) {
            if (e.buttonID != 'btnCancel') return;
            rowVisibleIndex = e.visibleIndex;
            s.GetRowValues(e.visibleIndex, 'LSAGREE', delNoApp);
        }

        function delNoApp(NoApp) {
            cplMain.PerformCallback("DELETE;" + NoApp);
        }

    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout" ClientInstanceName="ASPxFormLayout" runat="server" Theme="Glass" Width="100%">
        <Items>
            <dx:LayoutGroup Name="LayoutGroupAddItem" ShowCaption="True" Caption="Cross Collateral Maintenance" GroupBoxDecoration="Box" ColCount="2" >
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Find Cross Collateral" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup runat="server" ID="gvCrossColl" ClientInstanceName="gvCrossColl" KeyFieldName="CODE" OnDataBinding="gvCrossColl_DataBinding" DisplayFormatString="{1}" TextFormatString="{0}" MultiTextSeparator=";" Width="30%">
                                    <ClientSideEvents ValueChanged="function(s, e) { OnCodeChanged(s); }"/>
                                    <GridViewProperties EnablePagingCallbackAnimation="true">
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                    </GridViewProperties>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Add New Agreement No"  ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup runat="server" ID="gvNewItem" ClientInstanceName="gvNewItem" KeyFieldName="LSAGREE" OnDataBinding="gvNewItem_DataBinding" DisplayFormatString="{0}" TextFormatString="{0}" MultiTextSeparator=";" Width="30%">
                                    <GridViewProperties EnablePagingCallbackAnimation="true">
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                    </GridViewProperties>
                                    <ClientSideEvents ValueChanged="function(s,e) { cplMain.PerformCallback('ADD;' + 'CLICK'); }"/>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <%--<dx:LayoutItem ShowCaption="True" Caption="" >
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnAdd" ClientInstanceName="btnAdd" Height="30px" Text="Add" AutoPostBack="false" Theme="MaterialCompact">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('ADD;' + 'CLICK'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>--%>
                    <dx:LayoutItem ShowCaption="False"  ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvTempData"
                                    runat="server"
                                    ClientInstanceName="gvTempData"
                                    OnDataBinding="gvTempData_DataBinding"
                                    OnCustomButtonInitialize="gvTempData_CustomButtonInitialize"
                                    Width="100%">
                                    <SettingsBehavior AllowDragDrop="false" AllowSort="false"/>
                                    <ClientSideEvents CustomButtonClick="gvTempData_CustomButtonClick" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="LSAGREE" Caption="Agreement No" VisibleIndex="0">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NAME" Caption="Nama" VisibleIndex="1">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ASSET_DESCS" Caption="Desc" VisibleIndex="2">               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn Caption="Action" VisibleIndex="3" Width="100px">  
                                            <CustomButtons>  
                                                <dx:GridViewCommandColumnCustomButton Text="Remove" ID="btnCancel">  
                                                </dx:GridViewCommandColumnCustomButton>  
                                            </CustomButtons>  
                                        </dx:GridViewCommandColumn>
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

                    <dx:LayoutItem ShowCaption="True"  Caption=" " ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnReset" ClientInstanceName="btnReset" Height="30px" Text="Reset" OnClick="btnReset_onClick" BackColor="Gray" HoverStyle-BackColor="Red" Theme="MaterialCompact">
                                </dx:ASPxButton>
                                <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Height="30px" Text="Update" AutoPostBack="false" Theme="MaterialCompact">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVE;' + 'CLICK'); }" />
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

    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
