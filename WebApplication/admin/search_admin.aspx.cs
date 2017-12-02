using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class search_admin : System.Web.UI.Page
    {
        dataDataContext data = new dataDataContext();
        User us =new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myscript", @"<script type=""text/javascript"">
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(success);
    } else {
        alert(""Geo Location is not supported on your current browser!"");
    }
    function success(position) {
        var lat = position.coords.latitude;
document.getElementById('HiddenField1').value = lat.toLocaleString();
        var long = position.coords.longitude;
document.getElementById('HiddenField2').value = long.toLocaleString();
        var city = position.coords.locality;
        var myLatlng = new google.maps.LatLng(lat, long);
        var myOptions = {
            center: myLatlng,
            zoom: 12,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById(""map_canvas""), myOptions);
        var marker = new google.maps.Marker({
            position: myLatlng,
            title: ""lat: "" + lat + "" long: "" + long
        });
 
       
    }
</script>");
            us = (User)Session["user"];
            if (!IsPostBack)
            {
                dataDataContext dd = new dataDataContext();
                var ad =(from a in dd.Users
                         where a.type==true
                         select new
                         {
                             a.ID,
                             a.First_Name,
                             a.last_name,
                             a.date_of_birth,
                             a.User_name,
                         });
                GridView1.DataSource=ad;
                GridView1.DataBind();
                var au = (from a in dd.Users
                          where a.type==false
                          select new
                          {
                              a.ID,
                              a.First_Name,
                              a.last_name,
                              a.date_of_birth,
                              a.User_name,
                          });
                GridView2.DataSource = au;
                GridView2.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            us = (User)Session["user"];
            string lat = (string)Session["lat"];
            string lon = (string)Session["long"];
            dataDataContext dd = new dataDataContext();
            var ad = (from a in dd.Users
                      where (a.User_name.StartsWith(TextBox1.Text)||a.First_Name.StartsWith(TextBox1.Text))&&(a.type==true)
                      select new
                      {
                          a.ID,
                          a.First_Name,
                          a.last_name,
                          a.date_of_birth,
                          a.User_name,
                      });
            GridView1.DataSource = ad;
            GridView1.DataBind();
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int I = e.NewSelectedIndex;
            string pid = GridView1.Rows[I].Cells[1].Text;
            dataDataContext dd = new dataDataContext();
            User ddd = new User();

             ddd = (from a in dd.Users
                               where a.ID == Convert.ToInt32(pid)
                               select a).SingleOrDefault();
            Session["file"] = ddd;
            Response.Redirect("http://localhost:3359/admin/searchfile.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            dataDataContext dd = new dataDataContext();
            var au = (from a in dd.Users
                      where a.User_name.StartsWith(TextBox1.Text) || a.First_Name.StartsWith(TextBox1.Text)
                      select new
                      {
                          a.ID,
                          a.First_Name,
                          a.last_name,
                          a.date_of_birth,
                          a.User_name,
                          
                       }
                       ) ;
            GridView2.DataSource =au;
            GridView2.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("http://localhost:3359/login.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/fileupload.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/search.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/Add.aspx");
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/search_m.aspx");
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/search user.aspx");
        }

 
    }
}