using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitterizer;
using Newtonsoft.Json;
namespace shTwiX
{
    public partial class Callback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["denied"] == null)
            {
                OAuthTokenResponse tokens = OAuthUtility.GetAccessToken(
                    "ConsumerKey",
                    "ConsumerKeySecret",
                    Request.QueryString["oauth_token"], "");
                Dictionary<string, string> values = new Dictionary<string, string>();
                values["AccessToken"] = tokens.Token;
                values["AccessSecret"] = tokens.TokenSecret;
                values["ScreenName"] = tokens.ScreenName;
                values["UserId"] = tokens.UserId.ToString();
                string fileName =  Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper();
                string[] theFiles = System.IO.Directory.GetFiles(Server.MapPath("App_Data/Users/"), tokens.ScreenName + ".*");
                if (theFiles.Length > 0)
                    fileName = new System.IO.FileInfo(theFiles[0]).Extension.Substring(1);
                using (System.IO.StreamWriter str = new System.IO.StreamWriter(Server.MapPath("App_Data/Users/" + tokens.ScreenName + "." + fileName)))
                {
                    str.Write(JsonConvert.SerializeObject(values));
                }
                txt_APIURL.Text = "http://twittr.me/" + tokens.ScreenName + "/" + fileName;
            }
            else
            {
                Response.Redirect("/");
            }
        }
    }
}