using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Drawing;
using Shaahin.us.Encryption;
public partial class image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected override void OnPreRender(EventArgs e)
    {
        //throw new Exception(shTwiX.shFunctions.decryptBase64Url(Request.QueryString[0]));
        Response.Clear();
        Response.ContentType = "image/jpeg";
        WebClient webclient = new WebClient();
        webclient.Headers.Clear();
        webclient.Headers.Add("Accept: image/jpeg, application/x-ms-application, image/gif, image/png, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-shockwave-flash, */*");
        //webclient.Headers.Add("Accept-Encoding: gzip, deflate");
        webclient.Headers.Add("User-Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.2)");

        byte[] data = webclient.DownloadData(DES.Decrypt(shTwiX.shFunctions.decryptBase64Url(Request.QueryString[0]), shTwiX.shFunctions.key));
        do
        {
        } while (webclient.IsBusy);
        ImageConverter imageConverter = new System.Drawing.ImageConverter();
        try
        {
            System.Drawing.Image image = (System.Drawing.Image)imageConverter.ConvertFrom(data);
            image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        catch
        {
            Response.Write(data.Length.ToString() + "<br/>" + Request.QueryString[0]);
        }
    }

}
