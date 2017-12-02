using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class test1 : System.Web.UI.Page
    {
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["lat"] = HiddenField1.Value.ToString();
            Session["long"] = HiddenField2.Value.ToString();
            Response.Redirect("~/test2.aspx");

        }
    }
}