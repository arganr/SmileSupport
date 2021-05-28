<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocViewer.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Shared.DocViewer" %>
<%@ Register Assembly="DevExpress.XtraReports.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxRoundPanel ID="pnlMain" runat="server" ShowCollapseButton="true" ClientInstanceName="pnlMain" Width="100%" Height="100%">
            <PanelCollection>
                <dx:PanelContent>
                    <dx:ASPxDocumentViewer ID="dvrMain" runat="server" ClientInstanceName="dvrMain" Theme="Material"></dx:ASPxDocumentViewer>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </div>
    </form>
</body>
</html>
