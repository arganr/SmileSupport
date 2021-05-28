using DevExpress.Web;
using System;
using System.Text;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM {
    public class Global_asax : System.Web.HttpApplication {
        void Application_Start(object sender, EventArgs e) {
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);
        }

        void Application_End(object sender, EventArgs e) {
            // Code that runs on application shutdown
        }
    
        void Application_Error(object sender, EventArgs e) {
            //// Code that runs when an unhandled error occurs
            //// Use HttpContext.Current to get a Web request processing helper
            //HttpServerUtility server = HttpContext.Current.Server;
            //Exception exception = server.GetLastError();
            //if (exception is HttpUnhandledException)
            //    exception = exception.InnerException;
            //// Log an exception
            //AddToLog(exception.Message, exception.StackTrace);

            var exception = HttpContext.Current.Server.GetLastError();
            //Check exception type and specify error text for your exception
            if (exception.ToString().Length > 0)
            {
                ASPxWebControl.SetCallbackErrorMessage(exception.Message);
            }
        }
    
        void Session_Start(object sender, EventArgs e) {
        }
    
        void Session_End(object sender, EventArgs e) {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
        void AddToLog(string message, string stackTrace)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.ToLocalTime().ToString());
            sb.AppendLine(message);
            sb.AppendLine();
            sb.AppendLine("Source File: " + HttpContext.Current.Request.RawUrl);
            sb.AppendLine();
            sb.AppendLine("Stack Trace: ");
            sb.AppendLine(stackTrace);
            for (int i = 0; i < 150; i++)
                sb.Append("-");
            sb.AppendLine();
            HttpContext.Current.Session["Log"] += sb.ToString();
            sb.AppendLine();
        }
    }
}