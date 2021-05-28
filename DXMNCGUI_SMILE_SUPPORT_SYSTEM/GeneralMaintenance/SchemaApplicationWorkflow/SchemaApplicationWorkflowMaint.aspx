<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="SchemaApplicationWorkflowMaint.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.GeneralMaintenance.SchemaApplicationWorkflow.SchemaApplicationWorkflowMaint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Alert Confirmation !" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dx:ASPxFormLayout ID="ASPxFormLayout6" runat="server">
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
PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert Error!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
</dx:ASPxPopupControl>
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Theme="PlasticBlue" Font-Names="Calibri">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Name="FormCaption" GroupBoxDecoration="HeadingLine" Caption="Application Workflow Scheme Maintenance">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView 
                                    runat="server" 
                                    ID="gvMain" 
                                    ClientInstanceName="gvMain"
                                    DataSourceID="sdsApplicationWorkflowScheme"
                                    Width="50%" KeyFieldName="DocKey" 
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    EnableTheming="True" 
                                    Theme="Glass" 
                                    Font-Names="Calibri" 
                                    OnInit="gvMain_Init" 
                                    OnInitNewRow="gvMain_InitNewRow">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                    <ClientSideEvents />
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true"/>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true"/>
                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true"/>
                                    <SettingsSearchPanel Visible="false" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsPager PageSize="20"></SettingsPager>
                                    <SettingsText ConfirmDelete="Are you really want to Delete?"/>
                                    <SettingsEditing Mode="EditForm"></SettingsEditing>
                                    <SettingsCommandButton>
                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px"></NewButton>
                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                    </SettingsCommandButton>
                                    <Columns>
                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="1" Width="10%">
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colSeq" Caption="Seq" FieldName="Seq" ReadOnly="True" PropertiesSpinEdit-SpinButtons-Enabled="false" ShowInCustomizationForm="true" VisibleIndex="1" Width="10%">
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn Name="colStateCode" Caption="State Code" FieldName="StateCode" PropertiesTextEdit-MaxLength="10" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="2" Width="10%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colStateDescription" Caption="State Description" PropertiesTextEdit-MaxLength="50" FieldName="StateDescription" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="3" Width="40%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataCheckColumn Name="colCanCam" Caption="Cam?" FieldName="CanCam" VisibleIndex="4" Width="10%">
                                            <PropertiesCheckEdit ValueChecked="T" ValueGrayed="N" ClientInstanceName="colCanCam" ValueType="System.Char" ValueUnchecked="F">
                                            </PropertiesCheckEdit>
                                        </dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataCheckColumn Name="colCanReturn" Caption="Return?" FieldName="CanReturn" VisibleIndex="5" Width="10%">
                                            <PropertiesCheckEdit ValueChecked="T" ValueGrayed="N" ClientInstanceName="colCanReturn" ValueType="System.Char" ValueUnchecked="F">
                                            </PropertiesCheckEdit>
                                        </dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataTextColumn Name="colReleaseAccess" Caption="Release Role" PropertiesTextEdit-MaxLength="50" FieldName="ReleaseAccess" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="6" Width="10%">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <asp:SqlDataSource ID="sdsApplicationWorkflowScheme" runat="server"
        SelectCommand="SELECT * FROM ApplicationWorkflowScheme ORDER BY Seq" 
            InsertCommand="INSERT INTO ApplicationWorkflowScheme VALUES (@Seq, @StateCode, @StateDescription, @CanCam, @CanReturn, @ReleaseAccess)"
                UpdateCommand="UPDATE ApplicationWorkflowScheme SET Seq=@seq, StateCode=@StateCode, StateDescription=@StateDescription, CanCam=@CanCam, CanReturn=@CanReturn, ReleaseAccess=@ReleaseAccess WHERE DocKey=@DocKey" 
                    DeleteCommand="DELETE ApplicationWorkflowScheme WHERE (DocKey=@DocKey)">
            <UpdateParameters>
                <asp:Parameter Name="DocKey" Type="Int32"/>
                <asp:Parameter Name="Seq" Type="Int32"/>
                <asp:Parameter Name="StateCode" Type="String"/>
                <asp:Parameter Name="StateDescription" Type="String"/>
                <asp:Parameter Name="CanCam" Type="String"/>
                <asp:Parameter Name="CanReturn" Type="String"/>
                <asp:Parameter Name="ReleaseAccess" Type="String"/>
            </UpdateParameters>
            <DeleteParameters>
                <asp:Parameter Name="DocKey"/>
            </DeleteParameters>
        </asp:SqlDataSource>
</asp:Content>
