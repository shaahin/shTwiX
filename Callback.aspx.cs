using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitterizer;
using Newtonsoft.Json;
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
            values["AccessToken"] = tokens.Token;//AES.Encrypt(tokens.Token, shStrings.key1);
            values["AccessSecret"] = tokens.TokenSecret;//AES.Encrypt(tokens.TokenSecret, shStrings.key1);
            values["ScreenName"] = tokens.ScreenName;
            values["UserId"] = tokens.UserId.ToString();
            string fileName =  Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper();
            using (System.IO.StreamWriter str = new System.IO.StreamWriter(Server.MapPath("App_Data/Users/" +tokens.ScreenName + "." + fileName)))
            {
                str.Write(JsonConvert.SerializeObject(values));
            }
            Response.Write("Your API Proxy URL is:<br/>http://twproxy.shaahin.us/"+ tokens.ScreenName + "/" + fileName);
        }
        else
        {
            Response.Write("denied");
        }
    }
}