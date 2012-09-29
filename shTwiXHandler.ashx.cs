using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Text;
using Twitterizer;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Shaahin.us.Encryption;

namespace shTwiX
{
    /// <summary>
    /// Summary description for shTwiX
    /// </summary>
    public class shTwiXHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            StringBuilder log = new StringBuilder();
            string screenName = "";
            try
            {
                string query = "";
                
                UriBuilder urlBuilder = new UriBuilder("https://api.twitter.com/" + context.Server.UrlDecode(context.Request.QueryString["query"]));
                // Adds query strings to the url.
                // some headers may not be copied because they can't be used with clients other than twitter
                foreach (var queryString in context.Request.QueryString.AllKeys)
                {
                    switch (queryString)
                    {
                        case "u":
                        case "p":
                        case "query":
                        case "earned":
                        case "pc":
                            break;
                        default:
                            query += string.Format("&{0}={1}", queryString, context.Request.QueryString[queryString]);
                            break;
                    }
                }
                if (query.Length > 1)
                    query = query.Substring(1);
                urlBuilder.Query = query;
                log.AppendLine(query);
                log.AppendLine("URL: " + urlBuilder.Uri.ToString());
                OAuthTokens tokens = new OAuthTokens();
                tokens.ConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
                tokens.ConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];

                JToken accessToken;
                String userFileName = context.Server.MapPath("/App_Data/Users/" + context.Request.QueryString["u"] + "." + context.Request.QueryString["p"]);
                if (File.Exists(userFileName))
                {
                    using (StreamReader str = new StreamReader(userFileName))
                    {
                        accessToken = JObject.Parse(str.ReadToEnd());
                        ;
                    }
                    tokens.AccessToken = (string)accessToken.SelectToken("AccessToken");
                    tokens.AccessTokenSecret = (string)accessToken.SelectToken("AccessSecret");
                    screenName = (string)accessToken.SelectToken("ScreenName");
                    log.AppendLine("User FOUND!");
                }
                else
                {
                    log.AppendLine("User NOT FOUND");
                    context.Response.StatusCode = 404;
                    return;
                }
                //check if the request is xAuth, if it is, simulates the xAuth result for fooling Twitter for iPhone.
                if (context.Request.QueryString["query"].Contains("oauth/access_token"))
                {

                    string xAuthResponse = string.Format("oauth_token={0}&oauth_token_secret={1}&user_id={2}&screen_name={3}&x_auth_expires=0",
                        (string)accessToken.SelectToken("AccessToken"),
                        (string)accessToken.SelectToken("AccessSecret"),
                        (string)accessToken.SelectToken("UserId"),
                        (string)accessToken.SelectToken("ScreenName"));
                    context.Response.Write(xAuthResponse);
                    screenName = (string)accessToken.SelectToken("ScreenName");
                    return;
                }

                HTTPVerb verb = HTTPVerb.GET;
                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        verb = HTTPVerb.GET;
                        break;
                    case "POST":
                        verb = HTTPVerb.POST;
                        break;
                    case "DELETE":
                        verb = HTTPVerb.DELETE;
                        break;
                }
                if (context.Request.Headers["Authorization"] == null)
                {
                    tokens = null;
                    log.AppendLine("Request NOT authenticated");
                }
                WebRequestBuilder webreq = new WebRequestBuilder(urlBuilder.Uri, verb, tokens);
                webreq.Multipart = (context.Request.ContentType.Contains("multipart"));
                if (verb != HTTPVerb.GET)
                {
                    // adds body parameters to request
                    foreach (var key in context.Request.Form.AllKeys)
                    {
                        webreq.Parameters.Add(key, context.Request.Form[key]);
                    }
                    foreach (var fileKey in context.Request.Files.AllKeys)
                    {
                        webreq.Parameters.Add(fileKey, context.Request.Form[fileKey]);
                    }

                }
                ServicePointManager.Expect100Continue = false;
                HttpWebRequest req = webreq.PrepareRequest();
                try
                {
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    log.AppendLine("get Response");
                    StreamReader strReader = new StreamReader(resp.GetResponseStream());
                    String response = strReader.ReadToEnd();
                    if (ConfigurationManager.AppSettings["debugMode"] == "true")
                        log.AppendLine(response);
                    // replaces images url to use TwiX to load them
                    response = Regex.Replace(response, @"(((http:\\/\\/www)|(http:\\/\\/)|(https:\\/\\/www)|(https:\\/\\/)|(www))[-a-zA-Z0-9@:%_\\\+.~#?&//=]+)\.(jpg|jpeg|gif|png|bmp|tiff|tga|svg)", delegate(Match match)
                    {
                        string v = match.ToString();
                        return ConfigurationManager.AppSettings["baseUrl"] + "image/" + shFunctions.encryptBase64Url(DES.Encrypt(v.Replace(@"\/", "/").Replace("_normal", ""), shFunctions.key) + ".jpg");
                    });

                    strReader.Close();
                    context.Response.ClearContent();
                    context.Response.Write(response);
                }
                catch (WebException webex)
                {
                    if (webex.Status == WebExceptionStatus.ProtocolError)
                    {
                        context.Response.StatusCode = (int)((HttpWebResponse)webex.Response).StatusCode;

                    }
                    log.AppendLine("ERROR: " + webex.Message);
                    return;

                }
            }
            catch (Exception ee){
                log.AppendLine("Error: "+ ee.Message);
                log.AppendLine("stack: " + ee.StackTrace);
            }

            if (ConfigurationManager.AppSettings["log"] == "true")
                writeLogToFile(log.ToString(), screenName, context.Request.HttpMethod, context);
            //throw new Exception(context.Request.QueryString.ToString());
        }
        public static void writeLogToFile(string log, string username, string httpMethod, HttpContext context)
        {
            using (System.IO.StreamWriter strw = new System.IO.StreamWriter(context.Server.MapPath("/logs/" + DateTime.Now.Ticks.ToString() + "-"+ username +"-" +httpMethod+ ".txt"), false))
            {
                strw.Write(log);
            }
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