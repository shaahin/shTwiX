using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text;
namespace shTwix
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            using (System.IO.StreamWriter str = new System.IO.StreamWriter(Server.MapPath("server-log.txt")))
            {
                HttpContext ctx = HttpContext.Current;



                StringBuilder sb = new StringBuilder();
                sb.Append(ctx.Request.Url.ToString() + System.Environment.NewLine);
                sb.Append("Source:" + System.Environment.NewLine + ctx.Server.GetLastError().Source.ToString());
                sb.Append("Message:" + System.Environment.NewLine + ctx.Server.GetLastError().Message.ToString());
                sb.Append("Stack Trace:" + System.Environment.NewLine + ctx.Server.GetLastError().StackTrace.ToString());
                str.WriteLine(sb.ToString());

            }

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
