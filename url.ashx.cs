using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
namespace shTwiX
{
    /// <summary>
    /// Summary description for url
    /// </summary>
    public class url : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //try
            //{
                WebRequest request = WebRequest.Create("http://t.co/" + context.Request.QueryString[0]);
                var response = request.GetResponse();

                context.Response.Redirect(response.ResponseUri.ToString());
           /* }
            catch
            {
                context.Response.Redirect("http://t.co/" + context.Request.QueryString[0]);
            }*/
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}