using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebApplication
{
    public partial class login : System.Web.UI.Page
    {
        dataDataContext data = new dataDataContext();
        User ad = new User();
        
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
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {

        }

        protected void Login1_Authenticate1(object sender, AuthenticateEventArgs e)
        {

            if (Roles.IsUserInRole(Login1.UserName, "Admin"))
            {
                e.Authenticated = true;
                ad = data.Users.Where(d => d.User_name == Login1.UserName).SingleOrDefault();

                if (Login1.UserName == ad.User_name && Login1.Password == ad.password)
                {
 
                    Session["lat"] = HiddenField1.Value.ToString();
                    Session["long"] = HiddenField2.Value.ToString();
                    Session["user"] = ad;
                    Response.Redirect("http://localhost:3359/admin/fileupload.aspx");

                    
                }
                else
                {
                    Label1.Text = "please check the admin password";
                }
            }
            else
            {
                if (Roles.IsUserInRole(Login1.UserName, "user"))
                {
                    e.Authenticated = true;
                    ad = data.Users.Where(d => d.User_name == Login1.UserName).SingleOrDefault();

                    if (Login1.UserName == ad.User_name && Login1.Password == ad.password)
                    {

                        Session["user"] = ad;
//                        ClientScript.RegisterStartupScript(this.GetType(), "myscript", @"<script type=""text/javascript"">
//    if (navigator.geolocation) {
//        navigator.geolocation.getCurrentPosition(success);
//    } else {
//        alert(""Geo Location is not supported on your current browser!"");
//    }
//    function success(position) {
//        var lat = position.coords.latitude;
//document.getElementById('HiddenField1').value = lat.toLocaleString();
//        var long = position.coords.longitude;
//document.getElementById('HiddenField2').value = long.toLocaleString();
//        var city = position.coords.locality;
//        var myLatlng = new google.maps.LatLng(lat, long);
//        var myOptions = {
//            center: myLatlng,
//            zoom: 12,
//            mapTypeId: google.maps.MapTypeId.ROADMAP
//        };
//        var map = new google.maps.Map(document.getElementById(""map_canvas""), myOptions);
//        var marker = new google.maps.Marker({
//            position: myLatlng,
//            title: ""lat: "" + lat + "" long: "" + long
//        });
// 
//       
//    }
//</script>");
//                        Session["lat"] = HiddenField1.Value.ToString();
//                        Session["long"] = HiddenField2.Value.ToString();
                       
                        Response.Redirect("http://localhost:3359/oursearch.aspx");
                        
                    }
                    else
                    {
                        Label1.Text = "please check user password";
                    }

                }
                else
                {
                    Label1.Text = "please check user and password";
                }



            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
         //   Session["user"]=us;

            Session["lat"] = HiddenField1.Value.ToString();
            Session["long"] = HiddenField2.Value.ToString();
        }

   
    }
}
