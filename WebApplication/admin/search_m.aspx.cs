using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using GMap;
using GMaps;
using Subgurim.Controles;
using Subgurim.Controles.GoogleChartIconMaker;
namespace WebApplication
{
    public partial class testtm : System.Web.UI.Page
    {
        User us = new User();
        Word_search we = new Word_search();
        dataDataContext data = new dataDataContext();
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
            if (!IsPostBack)
            {

                //   GLatLng mainarea = new GLatLng(33.2,36.3);
                GLatLng mainarea = new GLatLng(33.50, 36.30);
                GMap1.setCenter(mainarea, 13);
                XPinLetter xpinletter = new XPinLetter((PinShapes.pin_star), "W", Color.Blue, Color.White, Color.Chocolate);
                GMap1.Add(new GMarker(mainarea, new GIcon(xpinletter.ToString(), xpinletter.Shadow())));
               
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            us = (User)Session["user"];
            string lat = (string)Session["lat"];
            string lon = (string)Session["long"];
            GLatLng mainarea = new GLatLng(33.50, 36.30);
            GMap1.setCenter(mainarea, 13);
            XPinLetter xpinletter = new XPinLetter((PinShapes.pin_star), "W", Color.Blue, Color.White, Color.Chocolate);
            GMap1.Add(new GMarker(mainarea, new GIcon(xpinletter.ToString(), xpinletter.Shadow())));

            PinIcon p;
            GMarker gm;
            GInfoWindow gin;
            foreach (var i in data.Word_searches)
            {
                if (i.Word_search1.ToString().Trim() == TextBox1.Text)
                {
                    p = new PinIcon(PinIcons.computer, Color.Cyan);
                    gm = new GMarker(new GLatLng(Convert.ToDouble(i.latid), (Convert.ToDouble(i.@long))), new GMarkerOptions(new GIcon(p.ToString(), p.Shadow())));
                    us = data.Users.Where(pm => pm.ID == i.Searcher_id).Single();
                    gin = new GInfoWindow(gm, "<font color=black><b>Search info</b><br/> User:" + us.User_name + "<br /> Word search:" + i.Word_search1 + "<br /> date_of_search:"+i.date_s, false, GListener.Event.mouseover);
                    GMap1.Add(gin);
                }
            }
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
        }
    }
}