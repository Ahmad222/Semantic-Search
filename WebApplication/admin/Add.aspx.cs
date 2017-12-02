using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;


namespace WebApplication
{
    public partial class Add : System.Web.UI.Page
    {
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
            us = (User)Session["user"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "admin")
            {
                dataDataContext dd = new dataDataContext();
                User ad = new User();
                ad.First_Name = TextBox1.Text;
                ad.last_name = TextBox2.Text;
                ad.User_name = TextBox4.Text;
                ad.date_of_birth = DateTime.Parse(TextBox3.Text);
                ad.password = TextBox5.Text;
                ad.type = true;
                MembershipCreateStatus status;
                MembershipUser newuser = Membership.CreateUser(TextBox4.Text, TextBox5.Text, TextBox4.Text + "@test.com", "none", "none", false, out status);
                Roles.AddUserToRole(TextBox4.Text, "Admin");
                dd.Users.InsertOnSubmit(ad);
                dd.SubmitChanges();
                Label1.Text = "admin added";

            }
            else
            {
                if (DropDownList1.Text == "user")
                {

                    dataDataContext dd = new dataDataContext();
                    User uus = new User();
                    uus.First_Name = TextBox1.Text;
                    uus.last_name = TextBox2.Text;
                    uus.User_name = TextBox4.Text;
                    uus.date_of_birth = DateTime.Parse(TextBox3.Text);
                    uus.password = TextBox5.Text;
                    MembershipCreateStatus status;
                    MembershipUser newuser = Membership.CreateUser(TextBox4.Text, TextBox5.Text, TextBox4.Text + "@test.com", "none", "none", false, out status);
                    Roles.AddUserToRole(TextBox4.Text, "user");
                    dd.Users.InsertOnSubmit(uus);
                    dd.SubmitChanges();
                    Label1.Text = "user added";
                }
                else
                {
                    Label1.Text = "please select a type of user";
                }
            }
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("http://localhost:3359/login");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
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
            Response.Redirect("http://localhost:3359/oursearch.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/search_admin.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/search_m.aspx");

        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/search user.aspx");
        }
    }
}