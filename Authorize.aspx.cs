using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using Twitterizer;
using Twitterizer.Core;
using Twitterizer.Entities;
using System.Configuration;
public partial class Authorize : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        OAuthTokenResponse authorizationTokens = OAuthUtility.GetRequestToken(ConfigurationManager.AppSettings["TwitterConsumerKey"], ConfigurationManager.AppSettings["TwitterConsumerSecret"], ConfigurationManager.AppSettings["baseUrl"] + "callback.aspx");
        //Response.Headers.Set("User-Agent", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3_3 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8J2 Safari/6533.18.5");
        Response.Redirect(string.Format("https://twitter.com/oauth/authorize?oauth_token={0}", authorizationTokens.Token), true);
    }
}