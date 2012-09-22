using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shTwiX
{
    public class shFunctions
    {
        public static string key = "6xGNGb89GVFWcuzn";
        public static string decryptBase64Url(String base64Url)
        {
            base64Url = base64Url.Replace("-", "+");
            base64Url = base64Url.Replace("_", "/");
            base64Url = base64Url.Replace("$", "=");
            return base64Url;
        }
        public static string encryptBase64Url(String text)
        {
            text = text.Replace("+","-");
            text = text.Replace("/","_");
            text = text.Replace("=","$");
            return text;
        }
    }
}