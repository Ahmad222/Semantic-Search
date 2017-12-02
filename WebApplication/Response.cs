using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.Text;



namespace WebApplication
{
    public  class Responsee
    {
     
            public string Ip, CountryCode, CountryName, RegionCode,
                RegionName, City, ZipCode, Latitude, Longitude;

            public Responsee()
            {
            }
            public static object GetIpLocation(string IpAddress)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Responsee));
                XmlReader reader = XmlReader.Create(new StringReader(GetIpData(IpAddress)));
               // object oo = (Responsee)serializer.Deserialize(reader); 
           //     return reader.Value.ToString(); ;
                foreach (var a in reader.ToString())
                {
                }
                return reader;
              //  return (Responsee)serializer.Deserialize(reader);
            }
            private static string GetIpData(string IpAddress)
            {
                var web = new WebClient() { Encoding = Encoding.UTF8 };
                string k= web.DownloadString(@"http://freegeoip.net/xml/");
                return k;
            }
        }
    }
