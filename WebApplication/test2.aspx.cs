using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string lat = (string)Session["lat"];
            string lon = (string)Session["long"];
            Label1.Text = lat;
            Label2.Text = lon;
        }
    }
}