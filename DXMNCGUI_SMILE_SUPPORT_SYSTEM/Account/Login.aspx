<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Account.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<script src="../../Scripts/soundmanager2.js"></script>
<script>
</script>
<script type="text/javascript">

</script>
<head runat="server">
    <title>SMILE SUPPORT SYSTEM</title>
    <link rel="icon" type="image/png" href="Content/Images/NumberingMenuIco-16x16.png" />
</head>
<body>
<form id="form1" runat="server">
    <dx:ASPxFormLayout runat="server" ColCount="3" EnableTheming="true">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <SettingsItemCaptions Location="Left"/>
        <Items>
            <dx:EmptyLayoutItem Width="100%" Height="120px"></dx:EmptyLayoutItem>
            <dx:EmptyLayoutItem Width="35%"></dx:EmptyLayoutItem>
            <dx:LayoutGroup Name="lgLogin" Width="378px" Height="417px" GroupBoxDecoration="None" Border-BorderStyle="Solid" Border-BorderColor="#233d75" BackColor="#235658" BackgroundImage-ImageUrl="../Content/Images/loginscreenSSS.jpg">
                <BackgroundImage Repeat="NoRepeat"/>
                <Items>
                    <dx:EmptyLayoutItem Height="130px" Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" HorizontalAlign="Right" CaptionStyle-Font-Names="Verdana" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxImage runat="server" Theme="Moderno" ImageUrl="~/Content/Images/IDIcon-32x32.png" Width="20px" Height="20px"></dx:ASPxImage>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" HorizontalAlign="Left" CaptionStyle-Font-Names="Verdana" Width="60%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtID" Theme="MetropolisBlue" Width="150px"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" HorizontalAlign="Right" CaptionStyle-Font-Names="Verdana" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxImage runat="server" ImageUrl="~/Content/Images/PassIcon-32x32.png" Width="20px" Height="20px"></dx:ASPxImage>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" HorizontalAlign="Left" CaptionStyle-Font-Names="Verdana" Width="60%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtPassword" Password="true" Theme="MetropolisBlue" Width="150px"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" HorizontalAlign="Left" Width="80%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnLogin" Theme="Moderno" Width="150px" Text="Login" Font-Names="Verdana" OnClick="btnLogin_Click"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" HorizontalAlign="Left" Width="80%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel runat="server" ID="lblMessage" Theme="Glass" ForeColor="Red" Width="150px" Text="Login" Font-Names="Verdana" Font-Size="XX-Small" Font-Italic="true"></dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
                <SettingsItems HorizontalAlign="Left" VerticalAlign="Middle"/>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</form>
</body>
</html>


