<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Default.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM._Default" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~")%>Scripts/Application.js"></script>
    <script type="text/javascript">
        var timeout;
        function OnLoad()
        {
            window.clearTimeout(timeout);
            //ASPxObjectContainer1.Play();
            //timeout = window.setTimeout (function (){ apcEvent.Show(); },1000);
        }
        function onShown(s, e) {
            setTimeout(function () { s.Hide(); }, 45000);
        }
        function onClick()
        {
            apcToast.Show();
        }

    </script>
    <dx:ASPxPopupControl ID="apcEvent" ClientInstanceName="apcEvent" runat="server" 
        Height="700" Width="1350" Opacity="85" Modal="true" CloseAction="CloseButton" ShowCloseButton="true"
        PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter"
        AllowDragging="false" 
        PopupAnimationType="Fade"
        CloseAnimationType="Fade"
        EnableCallbackAnimation="true"
        EnableViewState="False"
        BackColor="DarkGreen" 
        ForeColor="White" 
        Theme="Glass" 
        ShowHeader="true" HeaderText="Smile Support Peduli Kasih."
        ShowFooter="false"
        AllowResize="false" BackgroundImage-ImageUrl="~/Content/Gif/Covid-19-Masks-working.gif" BackgroundImage-Repeat="NoRepeat">
        <ClientSideEvents Shown="onShown"/>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcToast" ClientInstanceName="apcToast" runat="server" 
        Height="55" Width="300"
        PopupHorizontalOffset="600" 
        PopupVerticalOffset="-280"
        PopupHorizontalAlign="RightSides" 
        PopupVerticalAlign="WindowCenter"
        AllowDragging="false" 
        PopupAnimationType="Fade"
        CloseAnimationType="Fade"
        EnableCallbackAnimation="true"
        EnableViewState="False"
        BackColor="DarkGreen" 
        ForeColor="White" 
        Opacity="50" 
        Theme="Office365" 
        Text="......" 
        ShowHeader="false"
        ShowFooter="false"
        AllowResize="false">
        <ClientSideEvents Shown="onShown"/>
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%" Height="100%" BackColor="White">
        <ClientSideEvents Init="OnLoad" />
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
        <Items>
            <dx:LayoutGroup GroupBoxDecoration="None" Caption="" GroupBoxStyle-Caption-BackColor="Transparent">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxObjectContainer runat="server" ID="ASPxObjectContainer1" ClientInstanceName="ASPxObjectContainer1" Height="1" Width="1"></dx:ASPxObjectContainer>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%" Visible="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" AutoPostBack="false">
                                    <ClientSideEvents Click="onClick" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="60%">
                       <LayoutItemNestedControlCollection>
                           <dx:LayoutItemNestedControlContainer>
                               <dx:ASPxImage runat="server" ImageUrl="~/Content/Images/homescreenSSS.jpg"></dx:ASPxImage>
                           </dx:LayoutItemNestedControlContainer>
                       </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>