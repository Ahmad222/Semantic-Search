using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Subgurim.Controles;
using Subgurim.Controles.GoogleChartIconMaker;
using System.Drawing;

namespace WebApplication.admin
{
    public partial class search_user : System.Web.UI.Page
    {
        dataDataContext data = new dataDataContext();
        User us = new User();
        Word_search sea = new Word_search();
    static   string  kk;
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
            GMap1.Visible = false;
            if (!IsPostBack)
            {
                us = (User)Session["user"];
                if (!IsPostBack)
                {
                    dataDataContext dd = new dataDataContext();
                    var ad = (from a in dd.Users
                             
                              select new
                              {
                                  a.ID,
                                  a.First_Name,
                                  a.last_name,
                                  a.date_of_birth,
                                 
                              });
                    GridView1.DataSource = ad;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int I = e.NewSelectedIndex;
             kk = GridView1.Rows[I].Cells[1].Text;
            dataDataContext dd = new dataDataContext();
            User ddd = new User();

            ddd = (from a in dd.Users
                   where a.ID == Convert.ToInt32(kk)
                   select a).SingleOrDefault();
            var sw = (from bb in data.Word_searches
                      where bb.Searcher_id == Convert.ToInt32(kk)
                      select new
                      {
                          bb.ID,
                          bb.Word_search1,
                          bb.latid,
                          bb.@long,
                          bb.Searcher_id,
                          
                      });
            GridView2.DataSource = sw;
            GridView2.DataBind();
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
           
        }

        protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //string ah = GridView2.SelectedRow.Cells[1].Text;
            int I = e.NewSelectedIndex;
           // GridViewRow row = GridView2.SelectedRow;
           // string lo = row.ID;
            string pid = GridView2.Rows[I].Cells[1].Text;
            dataDataContext dd = new dataDataContext();
            Word_search word = new Word_search();

         var   eddd = (from a in dd.Word_searches
                   where a.ID == Convert.ToInt32(pid)
                   select a).SingleOrDefault();
         GMap1.Visible = true;
         GMap1.reset();
         PinIcon p;
         GMarker gm;
         GInfoWindow gin;
         GLatLng mainarea = new GLatLng(Convert.ToDouble(eddd.latid), Convert.ToDouble(eddd.@long));
         GMap1.setCenter(mainarea, 15);
         XPinLetter xpinletter = new XPinLetter((PinShapes.pin_star), "W", Color.Blue, Color.White, Color.Chocolate);
         GMap1.Add(new GMarker(mainarea, new GIcon(xpinletter.ToString(), xpinletter.Shadow())));
         p = new PinIcon(PinIcons.computer, Color.Cyan);
         gm = new GMarker(new GLatLng(Convert.ToDouble(eddd.latid), (Convert.ToDouble(eddd.@long))), new GMarkerOptions(new GIcon(p.ToString(), p.Shadow())));
        
         gin = new GInfoWindow(gm, "<font color=black><b>Search info</b><br/>  Word search:" + eddd.Word_search1 + "<br /> date_of_search:" + eddd.date_s, false, GListener.Event.mouseover);
         GMap1.Add(gin);
         Session["lat"] = this.Request.QueryString["lat"];
         Session["lon"] = this.Request.QueryString["long"];
         Session["user"] = us;

        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            var sw = (from bb in data.Word_searches
                      where bb.Searcher_id == Convert.ToInt32(kk)
                      select new
                      {
                          bb.ID,
                          bb.Word_search1,
                          bb.latid,
                          bb.@long,
                          bb.Searcher_id,

                      });
            GridView2.DataSource = sw;
            GridView2.DataBind();

        }



        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("");

        }
    }
}