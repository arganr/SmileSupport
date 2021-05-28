using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.InternalMemo
{
    public partial class InternalMemoEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                NewDocument();
        }

        private void NewDocument()
        {
            RichEditDocumentServer server = new RichEditDocumentServer();
            server.Document.Sections[0].Page.Landscape = false;
            server.Document.Unit = DevExpress.Office.DocumentUnit.Millimeter;
            server.Document.Sections[0].Margins.Left = 0.5f;
            server.Document.Sections[0].Margins.Right = 0.5f;
            server.Document.Sections[0].Margins.Top = 0.5f;
            server.Document.Sections[0].Margins.Bottom = 0.5f;

            server.Document.DefaultCharacterProperties.FontName = "Calibri";
            server.Document.DefaultCharacterProperties.FontSize = 12f;
            server.Document.DefaultCharacterProperties.ForeColor = Color.Red;

            server.Document.Sections[0].Page.PaperKind = System.Drawing.Printing.PaperKind.A4;

            MemoryStream memoryStream = new MemoryStream();
            server.SaveDocument(memoryStream, DocumentFormat.Doc);
            DemoRichEdit.Open("document" + Guid.NewGuid().ToString(), DocumentFormat.Doc, () => { return memoryStream.ToArray(); });
        }

        protected void DemoRichEdit_PreRender(object sender, EventArgs e)
        {
            DemoRichEdit.Focus();
        }
    }
}