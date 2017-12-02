using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;

using System.Net;
using System.IO;

namespace WebApplication
{
    public partial class testgoogle : System.Web.UI.Page
    {
    //    HtmlWeb getHtmlWeb = new HtmlWeb();
        protected void Page_Load(object sender, EventArgs e)
        {
            var getHtmlWeb = new HtmlWeb();
            var document = getHtmlWeb.Load(@"http://localhost:3359/testma.aspx");
            HtmlAgilityPack.HtmlDocument HtmlDoc = new HtmlAgilityPack.HtmlDocument();
            HtmlDoc.LoadHtml(@"http://localhost:3359/testma.aspx");
            var aTags = HtmlDoc.DocumentNode.SelectNodes("/a");
            List<string> le = new List<string>();
            int counter = 1;
            if (aTags != null)
            {
                foreach (var aTag in aTags)
                {
                    le.Add(counter + ". " + aTag.InnerHtml + " - " +
                      aTag.Attributes["href"].Value + "\t" + "<br />");
                    counter++;
                }
            }
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(@"http://localhost:3359/testma.aspx");
            //request.UserAgent = "A .NET Web Crawler";

            //WebResponse response = request.GetResponse();
            
            //Stream stream = response.GetResponseStream();
            
            //StreamReader reader = new StreamReader(stream);
            //string htmlText = reader.ReadToEnd();
           
        }
    }
}

