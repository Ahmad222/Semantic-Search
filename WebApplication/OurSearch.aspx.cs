using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Searcharoo.Common;
using System.Text;

namespace WebApplication
{
   
    public partial class OurSearch : System.Web.UI.Page
    {
        public string lat;
        public string lon;
        private Catalog _Catalog = null;
        private string _DisplayTime;
        /// <summary>Display string: matches (links and number of)</summary>
        private string _Matches = "";
        /// <summary>Display string: Number of pages that match the query</summary>
        private string _NumberOfMatches;
        public PagedDataSource _PagedResults = new PagedDataSource();
       dataDataContext data=new dataDataContext();
       Word_search we = new Word_search();
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
        }
   
        protected void Button1_Click(object sender, EventArgs e)
        {
            us=(User)Session["user"];
            string lat = (string)Session["lat"];
            string lon = (string)Session["long"];

            we.Word_search1 = TextBox1.Text;
            we.latid = lat;
            we.@long = lon;
            we.Searcher_id = us.ID;
            we.date_s = DateTime.Now;
            data.Word_searches.InsertOnSubmit(we);
            data.SubmitChanges();
         //  string ko= Cache.Get("OurSearch.aspx").ToString();
            foreach (char ss in TextBox1.Text)
            {
                if ((ss >= 0x600 && ss <= 0x6ff) || (ss >= 0x750 && ss <= 0x77f) || (ss >= 0xfb50 && ss <= 0xfc3f) || (ss >= 0xfe70 && ss <= 0xfefc))
                {
                    Searcharoo.Engine.Search.flag = 1;
                    
                    break;
                }
                else
                {
                    Searcharoo.Engine.Search.flag = 0;
                   
                    break;
                }
            }
            _Catalog = Catalog.Load();

             Searcharoo.Engine.Search se = new Searcharoo.Engine.Search();
            SortedList output = se.GetResults(TextBox1.Text, _Catalog);

            _NumberOfMatches = output.Count.ToString();
            _DisplayTime = se.DisplayTime;
            if (output.Count > 0)
            {
                Repeater1.DataSource = null;
                Repeater1.DataBind();
                lblNoSearchResults.Visible = false;
                _PagedResults.DataSource = output.GetValueList();
                _PagedResults.AllowPaging = true;
                _PagedResults.PageSize = Preferences.ResultsPerPage; //10;
                _PagedResults.CurrentPageIndex = Request.QueryString["page"] == null ? 0 : Convert.ToInt32(Request.QueryString["page"]) - 1;

                _Matches = se.SearchQueryMatchHtml;
                _DisplayTime = se.DisplayTime;
                string displaydate =datee;
                
                Repeater1.DataSource = _PagedResults;
                Repeater1.DataBind();
                Repeater1.Visible = true;
                
               
            }
            else
            {
                Repeater1.DataSource = null;
                Repeater1.DataBind();
                lblNoSearchResults.Visible = true;
            }
           //// Set the display info in the top & bottom user controls
           // ucSearchPanelHeader.Word = ucSearchPanelFooter.Word = this.SearchQuery;
           // ucSearchPanelFooter.Visible = true;
           // ucSearchPanelFooter.IsFooter = true;
           // ucSearchPanelHeader.IsSearchResultsPage = true;  


            Session["lat"] = HiddenField1.Value.ToString();
            Session["long"] = HiddenField2.Value.ToString();
          Session["user"] = us;

        }
        public string datee
        {
            get
            {
                return _DisplayTime;
            }
        }
        public string CreatePageUrl(string searchFor, int pageNumber)
        {
            return "Search.aspx?" + Preferences.QuerystringParameterName + "=" + TextBox1.Text + "&page=" + pageNumber;
        }
        public string CreatePagerLinks(PagedDataSource objPds, string BaseUrl)
        {
            StringBuilder sbPager = new StringBuilder();
            StringBuilder sbPager1 = new StringBuilder();



            sbPager1.Append("<td><font color=black></font><font color=red></font><font color=blue></font><font color=green></font><font color=darkgrey></font><font color=purple></font>");

            if (objPds.IsFirstPage)
            {	// lower link is blank
                sbPager.Append("<td></td>");
            }
            else
            {	// first+prev link
                sbPager.Append("<td align=right>");
                // first page link
                sbPager.Append("<a href=\"");
                sbPager.Append(CreatePageUrl(BaseUrl, 1));
                sbPager.Append("\" alt=\"First Page\" title=\"First Page\">|&lt;</a>&nbsp;");
                if (objPds.CurrentPageIndex != 1)
                {
                    // previous page link
                    sbPager.Append("<a href=\"");
                    sbPager.Append(CreatePageUrl(BaseUrl, objPds.CurrentPageIndex));
                    sbPager.Append("\" alt=\"Previous Page\" title=\"Previous Page\">&laquo;</a>&nbsp;");
                }
                sbPager.Append("</td>");
            }
            // calc low and high limits for numeric links
            int intLow = objPds.CurrentPageIndex - 1;
            int intHigh = objPds.CurrentPageIndex + 3;
            if (intLow < 1) intLow = 1;
            if (intHigh > objPds.PageCount) intHigh = objPds.PageCount;
            if (intHigh - intLow < 5) while ((intHigh < intLow + 4) && intHigh < objPds.PageCount) intHigh++;
            if (intHigh - intLow < 5) while ((intLow > intHigh - 4) && intLow > 1) intLow--;
            for (int x = intLow; x < intHigh + 1; x++)
            {
                // numeric links
                if (x == objPds.CurrentPageIndex + 1)
                {
                    sbPager1.Append("<td width=10 align=center><font color=orange><b></b></td>");
                    sbPager.Append("<td>" + x.ToString() + "</td>");
                }
                else
                {
                    sbPager1.Append("<td width=10 align=center><font color=orange><b></b></td>");
                    sbPager.Append("<td>");
                    sbPager.Append("<a href=\"");
                    sbPager.Append(CreatePageUrl(BaseUrl, x));
                    sbPager.Append("\" alt=\"Go to page\" title=\"Go to page\">");
                    sbPager.Append(x.ToString());
                    sbPager.Append("</a> ");
                    sbPager.Append("</td>");
                }
            }
            if (!objPds.IsLastPage)
            {
                sbPager.Append("<td>");
                if ((objPds.CurrentPageIndex + 2) != objPds.PageCount)
                {
                    // next page link
                    sbPager.Append("&nbsp;<a href=\"");
                    sbPager.Append(CreatePageUrl(BaseUrl, objPds.CurrentPageIndex + 2));
                    sbPager.Append("\" alt=\"Next Page\" title=\"Next Page\">&raquo;</a> ");
                }
                // last page link
                sbPager.Append("&nbsp;<a href=\"");
                sbPager.Append(CreatePageUrl(BaseUrl, objPds.PageCount));
                sbPager.Append("\" alt=\"Last Page\" title=\"Last Page\">&gt;|</a>");
                sbPager.Append("</td>");
            }
            else
            {
                if (objPds.PageCount == 1) sbPager.Append("<td> of 1</td>");
            }
            // convert the final links to a string and assign to labels
            return "<table cellpadding=0 cellspacing=1 border=0><tr>" + sbPager1.ToString() + "</tr><tr>" + sbPager.ToString() + "</tr></table>";

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("login.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (us.type == true)
            {
                Session["lat"] = this.Request.QueryString["lat"];
                Session["lon"] = this.Request.QueryString["long"];
                Session["user"] = us;
                Response.Redirect("http://localhost:3359/admin/fileupload.aspx");
            }
            else
            {
                Session["lat"] = this.Request.QueryString["lat"];
                Session["lon"] = this.Request.QueryString["long"];
                Session["user"] = us;
                Response.Redirect("http://localhost:3359/Oursearch.aspx");
            }
        }

    }
}