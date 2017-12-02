using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class searchfile : System.Web.UI.Page
    {
        dataDataContext dd = new dataDataContext();
        User us = new User();
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

            uploaded_file up=new uploaded_file();
            us=(User)Session["user"];
            us = dd.Users.Where(w => w.ID == us.ID).SingleOrDefault();

            var ep = (from a in dd.uploaded_files
                      where a.Admin_id == us.ID
                      select new
                      {
                          a.ID,
                          a.Uploaded_by,
                          a.file_name,
                          a.Admin_id,
                          a.date_uploaded
                      });
            GridView1.DataSource = ep;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
 
            uploaded_file up = new uploaded_file();
            us = (User)Session["user"];
            us = dd.Users.Where(w => w.ID == us.ID).SingleOrDefault();

            var ep = (from a in dd.uploaded_files
                      where a.Admin_id == us.ID &&a.file_name.StartsWith(TextBox1.Text)
                      
                      select new
                      {
                          a.ID,
                          a.Uploaded_by,
                          a.file_name,
                          a.Admin_id,
                          a.date_uploaded
                      });
            GridView1.DataSource = ep;
            GridView1.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/Add.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/fileupload.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/search.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/search_admin.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

            Session.Clear();
            Response.Redirect("http://localhost:3359/login.aspx");

        }

        protected void Button5_Click(object sender, EventArgs e)
        {

        }
    }
}