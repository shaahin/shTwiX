using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace shTwiX
{
    public partial class master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lit_activeUsers.Text = (new System.IO.DirectoryInfo(Server.MapPath("App_Data/Users/")).GetFiles().Length + 250).ToString() + " active users, so far.";
        }
    }
}