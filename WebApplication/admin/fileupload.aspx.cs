using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Searcharoo.Engine;
using Searcharoo.Common;
using Searcharoo.Indexer;
namespace WebApplication
{
    public partial class fileupload : System.Web.UI.Page
    {
        
      //  Spider cat = new Spider();
        dataDataContext dd = new dataDataContext();
        User us = new User();
        uploaded_file pp= new uploaded_file();
        File_type ft = new File_type();
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
            us = (User)Session["user"];
            
            if (DropDownList1.Text == "Arabic")
            {
                Searcharoo.Engine.Search.flag = 1;
            }
            else
            {
                Searcharoo.Engine.Search.flag = 0;
            }
            //string st = (FileUpload1.GetRouteUrl(FileUpload1.FileName,"").ToString());
            string bb = FileUpload22.FileName;
            //  string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName);
            string ss = @"D:\s5\WebApplication\content\" + bb;
            //Label1.Text = ss.TrimStart('~').TrimStart('/');

            DirectoryInfo d = new DirectoryInfo(@"D:\s5\WebApplication\content");
            FileInfo[] Files = d.GetFiles();
            // string[] File = Directory.GetFiles(@"C:\Users\Belal\Desktop\New folder (8)\Searcharoo_5\WebApplication\content", "*.txt",
           
            //      SearchOption.TopDirectoryOnly);

            string str = "";
            str = (@"<a href=""content/" + bb + @""">");
         //   Label2.Text = str;
            Session["d"] = bb;
        //  Session["x"] = Server.HtmlEncode(@"<a href =""content/" + bb + @""">" + bb + "</a>");
            string x = Server.HtmlEncode(@"<a href =""content/" + bb + @""">" +bb + "</a>");
            //Label2.Text = str;
         
         
            foreach (FileInfo f in Files)
            {

                str = Server.HtmlDecode(Server.HtmlEncode(str + @"<a href =""content/" + f + @""">" +f +"</a>" + "<br>"));
                
            }
           
           

            
           
         //   Label2.Text = str;

//           
           string sss=(@"<html>
       <head>
            		    <meta name=""robots"" content=""index,follow"">

      </head>
        <body>
       <p>
            <a href=""Search.aspx"">Search Page</a>
          
        </p>"
         + str + "<p>");
           Session["x"] = sss;
           TextWriter tsw = new StreamWriter(@"D:\s5\WebApplication\default.aspx");
           tsw.WriteLine(sss);
           tsw.Close();
           Session["lat"] = this.Request.QueryString["lat"];
           Session["lon"] = this.Request.QueryString["long"];
           Session["user"] = us;
           Response.Redirect("http://localhost:3359/searchspider.aspx");


           //Response.Redirect("http://localhost:3359/default.aspx");
           // ["http://localhost:3359/default.aspx"].Controls.Add(str);


//            // Build the catalog!
//            Spider cat = new Spider();
//            cat.SpiderProgressEvent += new SpiderProgressEventHandler(OnProgressEvent);
//            _Catalog = cat.BuildCatalog(new Uri(Preferences.StartPage));
//            Cache[Preferences.CatalogCacheKey] = _Catalog;
//            // Check if anything was found
//            if (_Catalog.Length > 0)
//            {
//                Response.Write("<br>Finished - now you can search!<p>");
//                Server.Transfer("Search.aspx");
//            }
//            else
//            {
//                Response.Write("<br><p font='color:red'>Sorry, nothing was cataloged. Check the settings in web.config.</p>");
//            }
      
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            us = (User)Session["user"];
            us = dd.Users.Where(w => w.ID == us.ID).SingleOrDefault();
            pp.Admin_id = us.ID;
            pp.Uploaded_by = us.User_name;
            System.IO.FileInfo file1 = new System.IO.FileInfo(FileUpload22.FileName);
            pp.date_uploaded = DateTime.Now;
            pp.file_name = FileUpload22.FileName;
           
            if (TextBox1.Text != null)
            {
                pp.Key_words = TextBox1.Text;
            }
            dd.uploaded_files.InsertOnSubmit(pp);
            dd.SubmitChanges();
            if (TextBox2.Text != null)
            {
                ft.Company = TextBox2.Text;
                ft.F_k_file = pp.ID;
                dd.File_types.InsertOnSubmit(ft);
                dd.SubmitChanges();
            }
            if (DropDownList1.Text == "Arabic")
            {
                Searcharoo.Engine.Search.flag = 1;
                
            }
            else
            {
                Searcharoo.Engine.Search.flag = 0;
             
            }
            //string st = (FileUpload1.GetRouteUrl(FileUpload1.FileName,"").ToString());
          
            string bb = FileUpload22.FileName;
            string fi = System.IO.Path.GetExtension(FileUpload22.FileName);
            //  string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName);
            //      SearchOption.TopDirectoryOnly);
            Encoding enc = new UnicodeEncoding();
            byte[] bytes = enc.GetBytes(bb);



            if (Searcharoo.Engine.Search.flag == 1)
            {
                string value2 = enc.GetString(bytes);
                string kk = Server.HtmlDecode(value2);
                pp.File_path = @"D:\s5\WebApplication\content\" + pp.ID + fi;
                pp.file_name = "ar-" + pp.ID + fi;
                dd.SubmitChanges();
                DirectoryInfo d = new DirectoryInfo(@"D:\s5\WebApplication\content");
                FileInfo[] Files = d.GetFiles();
                string ss = @"~/content/" + value2;
              //  Label1.Text = ss.TrimStart('~').TrimStart('/');
                FileUpload22.SaveAs(Server.MapPath(ss));
                string str = "";
                System.IO.File.Move(@"D:\s5\WebApplication\content\" + bb, @"D:\s5\WebApplication\content\" +pp.file_name);
                System.IO.File.Delete(@"D:\s5\WebApplication\content\" + bb);


                // string[] File = Directory.GetFiles(@"C:\Users\Belal\Desktop\New folder (8)\Searcharoo_5\WebApplication\content", "*.txt",




               
//                str = (@"<a href=""content/" + pp.file_name + @""">");
//                string sss = (@"<html>
//       <head>
//            		    <meta name=""robots"" content=""index,follow"">
//
//      </head>
//        <body>
//       <p>
//            <a href=""Search.aspx"">Search Page</a>
//          
//        </p>"
//+ str + "<p>");
//                Session["x"] = sss;
                //   Label2.Text = str;
            }
            else
            {
                string value2 = enc.GetString(bytes);
                string kk = Server.HtmlDecode(value2);
                pp.File_path = @"D:\s5\WebApplication\content\" + pp.ID + fi;
                pp.file_name = "enf-" + pp.ID + fi;
                dd.SubmitChanges();
                DirectoryInfo d = new DirectoryInfo(@"D:\s5\WebApplication\content");
                FileInfo[] Files = d.GetFiles();
                string ss = @"~/content/" + value2;
               // Label1.Text = ss.TrimStart('~').TrimStart('/');
                FileUpload22.SaveAs(Server.MapPath(ss));
                string str = "";
                System.IO.File.Move(@"D:\s5\WebApplication\content\" + bb, @"D:\s5\WebApplication\content\" + pp.file_name);
                System.IO.File.Delete(@"D:\s5\WebApplication\content\" + bb);

//                str = (@"<a href=""content/" +  pp.file_name  + @""">");
//                string sss = (@"<html>
//       <head>
//            		    <meta name=""robots"" content=""index,follow"">
//
//      </head>
//        <body>
//       <p>
//            <a href=""Search.aspx"">Search Page</a>
//          
//        </p>"
//+ str + "<p>");
//                Session["x"] = sss;
            }

//            Session["d"] = bb;
//            //  Session["x"] = Server.HtmlEncode(@"<a href =""content/" + bb + @""">" + bb + "</a>");
//            string x = Server.HtmlEncode(@"<a href =""content/" + bb + @""">" + bb + "</a>");
//            //Label2.Text = str;

//     string sse =(string)Session["x"];
//            //           

//            TextWriter tsw = new StreamWriter(@"D:\s5\WebApplication\default.aspx");
//            tsw.WriteLine(sse);
//            tsw.Close();
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
  

            Response.Redirect("http://localhost:3359/searchspider.aspx");


            //Response.Redirect("http://localhost:3359/default.aspx");
            // ["http://localhost:3359/default.aspx"].Controls.Add(str);


            //// Build the catalog!
            //Spider cat = new Spider();
            //cat.SpiderProgressEvent += new SpiderProgressEventHandler(OnProgressEvent);
            //_Catalog = cat.BuildCatalog(new Uri(Preferences.StartPage));
            //Cache[Preferences.CatalogCacheKey] = _Catalog;
            //// Check if anything was found
            //if (_Catalog.Length > 0)
            //{
            //    Response.Write("<br>Finished - now you can search!<p>");
            //    Server.Transfer("Search.aspx");
            //}
            //else
            //{
            //    Response.Write("<br><p font='color:red'>Sorry, nothing was cataloged. Check the settings in web.config.</p>");
            //}
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            us = (User)Session["user"];
            Session["lat"] = HiddenField1.Value.ToString();
            Session["long"] = HiddenField2.Value.ToString();
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/oursearch.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("http://localhost:3359/login.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            us = (User)Session["user"];
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/Add.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            us = (User)Session["user"];
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/search_admin.aspx");

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            us = (User)Session["user"];
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/Searchkey.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            us = (User)Session["user"];
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/testtm.aspx");
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            us = (User)Session["user"];
            Session["lat"] = this.Request.QueryString["lat"];
            Session["lon"] = this.Request.QueryString["long"];
            Session["user"] = us;
            Response.Redirect("http://localhost:3359/admin/search user.aspx");
        }
        //public void OnProgressEvent(object source, ProgressEventArgs pea)
        //{
        //    //Define the actions to be performed on
        //    //button click here.
        //    if (pea.Level < _ProgressEventLevel)
        //    {
        //        Response.Write(pea.Level + " :: " + pea.Message + "<br>");
        //        //Response.Flush();
        //    }
        //}
        //private Catalog _Catalog;
        //private int _ProgressEventLevel = 2;


        }

    }
