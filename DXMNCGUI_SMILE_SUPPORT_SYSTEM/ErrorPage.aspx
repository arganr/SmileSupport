<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    Error log:
    <dx:ASPxMemo ID="Memo" runat="server" Height="500px" Width="100%">
    </dx:ASPxMemo>
    <asp:LinkButton ID="ClearLinkButton" runat="server" Text="Clear" OnClick="ClearLinkButton_Click"></asp:LinkButton>
    </form>
</body>
</html>
