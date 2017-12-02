using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.ClientServices;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.admin
{
    public partial class Searchkey : System.Web.UI.Page
    {
        dataDataContext dd = new dataDataContext();
            User us = new User();
            uploaded_file up = new uploaded_file();

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
                us = dd.Users.Where(w => w.ID == us.ID).SingleOrDefault();
                if (!IsPostBack)
                {
                   // ad = (User)Session["file"];
                     //ad = dd.Users.Where(w => w.ID == ad.ID).SingleOrDefault();

                    //var ec = (from a in dd.uploaded_files
                    //          select new
                    //          {
                    //              a.ID,
                    //              a.Uploaded_by,
                    //              a.file_name,
                    //              a.Admin_id,
                    //              a.date_uploaded,
                    //              a.File_path
                    //          });
                    //GridView1.DataSource = ec;
                    //GridView1.DataBind();
                    //var epp = (from cc in dd.File_types

                    //           select new
                    //           {
                    //               cc.uploaded_file.ID,
                    //               cc.uploaded_file.Uploaded_by,
                    //               cc.uploaded_file.file_name,
                    //               cc.uploaded_file.Admin_id,
                    //               cc.uploaded_file.date_uploaded,
                    //               cc.uploaded_file.File_path
                    //           });
                    //GridView2.DataSource = epp;
                    //GridView2.DataBind();

                }
            }
        protected void Button1_Click(object sender, EventArgs e)
        {
            
                if (DropDownList1.Text == "Keyword")
                {
                    var ep = (from a in dd.uploaded_files
                              where a.Key_words == TextBox1.Text
                              select new
                              {
                                  a.ID,
                                  a.Uploaded_by,
                                  a.file_name,
                                  a.Admin_id,
                                  a.date_uploaded,
                                  a.File_path
                              });
                    GridView3.DataSource = ep;
                    GridView3.DataBind();
                }
                else if (DropDownList1.Text == "company")
                {
                    var epp = (from cc in dd.File_types
                               where cc.Company == TextBox1.Text

                               select new
                               {
                                   cc.uploaded_file.ID,
                                   cc.uploaded_file.Uploaded_by,
                                   cc.uploaded_file.file_name,
                                   cc.uploaded_file.Admin_id,
                                   cc.uploaded_file.date_uploaded,
                                   cc.uploaded_file.File_path
                               });
                    GridView3.DataSource = epp;
                    GridView3.DataBind();
                }
            

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
                                var epp = (from cc in dd.File_types
                                           where cc.Company==TextBox2.Text
                                
                      select new
                      {
                          cc.uploaded_file.ID,
                          cc.uploaded_file.Uploaded_by,
                         cc.uploaded_file.file_name,
                         cc.uploaded_file.Admin_id,
                          cc.uploaded_file.date_uploaded,
                         cc.uploaded_file.File_path
                      });
            GridView2.DataSource = epp;
            GridView2.DataBind();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;

        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {


        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void GridView3_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (DropDownList1.Text == "Keyword")
            {
                int I = e.NewSelectedIndex;
                string pid = GridView3.Rows[I].Cells[1].Text;
                dataDataContext dd = new dataDataContext();


                var ddd = (from a in dd.uploaded_files
                           where a.ID == Convert.ToInt32(pid)
                           select a.file_name).SingleOrDefault();

                Response.ClearContent();
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", "attachment; filename=_" + ddd + ";");
                Response.TransmitFile(@"D:\s5\WebApplication\content\" + "_" + ddd);
                Response.Flush();
                Response.End();
            }
            else if (DropDownList1.Text == "company")
            {
                int I = e.NewSelectedIndex;
                string pid = GridView3.Rows[I].Cells[1].Text;
                var ddd = (from a in dd.File_types
                           where a.F_k_file == Convert.ToInt32(pid)
                           select a.uploaded_file.file_name).SingleOrDefault();

                Response.ClearContent();
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", "attachment; filename=_" + ddd + ";");
                Response.TransmitFile(@"D:\s5\WebApplication\content\" + "_" + ddd);
                Response.Flush();
                Response.End();
            }

        }
        }
    }