using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GMaps.Classes;
using Artem.Google.UI;
using System.Text;
using System.Net;
using System.Xml.Linq;
using Subgurim.Controles;
using Subgurim.Maps.Collections;
using Subgurim.Maps.Google;
using Subgurim.Controls;
using Subgurim.Web;
using System.Web.UI.HtmlControls;
using System.Net.Security;
using System.Net.NetworkInformation;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
namespace WebApplication
{
    public partial class testmaps : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
          
            
        }

        public static void RetrieveFormatedAddress(string lat, string lng)
        {
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            // To Get IP Address


        //    string IPHost = Dns.GetHostName();
        //    string IP = Dns.GetHostByName(IPHost).AddressList[0].ToString();
                  var web = new WebClient() { Encoding = Encoding.UTF8 };
         //       string k= web.DownloadString(@"http://freegeoip.net/xml/");
   //     XmlReader reader=      XmlReader.Create(@"http://freegeoip.net/xml/");
             string kk= web.DownloadString(@"http://www.checkip.org/");
             char[] arr = new char[5];
             arr[0] = '<';
             arr[1] = '/';
             arr[2] = 'b';
             arr[3] = 'r';
             arr[4] = '>';
             string[] source = kk.Split('\n');
           
            foreach(string keyword in source)
            {
                if (keyword.StartsWith("<li>Latitude:"))
                {
                    string lat = keyword.Substring(14).TrimEnd(arr);
                }
                if (keyword.StartsWith("<li>Longitude:"))
                {
                    

                    string lon = keyword.Substring(14).TrimEnd(arr);
                }

            }
             //            <li>Latitude: 33.5</br>
            //<li>Longitude: 36.3</br>
         //       XmlReader reader = XmlReader.Create(k);
        //    string k = web.
               
               //     foreach (var a in ll.ToList()) ;
                
           // Responsee.GetIpLocation(IP);
                    
           // GMap1.DataSource = GMap.NET.MapProviders.BingMapProvider.Instance;
   //         string kk =Convert.ToString(GMap.NET.MapProviders.BingMapProvider.Instance);
          //  GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            StringBuilder sb = new StringBuilder();
        }
        //    GMap1.enableHookMouseWheelToZoom = true;
        //    GMap1.enableGetGMapElementById = true;
            //var coder = new GClientGeocoder();
            //     coder.getLatLng(
            //    "Addr to Geocode",
            //    function(point) {
            //        if (point) {
            //            // Do something with GLatLng point                
            //        }
            //    }
            //);
            //var pos = new GControlPosition(0, new GSize(100, 10));
            //pos.apply(document.getElementById("control"));
            //document.getElementById("map").appendChild(document.getElementById("control"));
            //GMap1.enableGetGMapElementById = true;
          
            //string k=Convert.ToString(GMap1.GCenter.lat);
            //string l = Convert.ToString(GMap1.GCenter.lng);
            //foreach (var a in Page.Controls)
            //{
            //    a.GetType();
            //}
           // foreach (var a in GMap1) ;
            ////////////////////////////////foreach (var a in Page.Controls)
            ////////////////////////////////    if (a is HtmlContainerControl)
            ////////////////////////////////    {
            ////////////////////////////////        HtmlContainerControl h = (HtmlContainerControl)a;
            ////////////////////////////////        foreach (var ih in h.Controls) ;
            ////////////////////////////////    }
            
            
        //           string baseUri ="http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
        //string location = string.Empty;
        //    RetrieveFormatedAddress("42","-2.2");
        //    string requestUri = string.Format(baseUri, k, l);

        //    using (WebClient wc = new WebClient())
        //    {
        //        string result = wc.DownloadString(requestUri);
        //        var xmlElm = XElement.Parse(result);
        //        var status = (from elm in xmlElm.Descendants()
        //                      where
        //                          elm.Name == "status"
        //                      select elm).FirstOrDefault();
        //        if (status.Value.ToLower() == "ok")
        //        {
        //            var res = (from elm in xmlElm.Descendants()
        //                       where
        //                           elm.Name == "formatted_address"
        //                       select elm).FirstOrDefault();
        //            requestUri = res.Value;
        //        }
        //    }
      

   
        }
    }
