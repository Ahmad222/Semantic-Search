<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fileupload.aspx.cs" Inherits="WebApplication.fileupload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <style type="text/css">
        .button
        {}
        .style1
        {
            font-size: xx-large;
        }
    </style>
    <script type="text/javascript"
src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">
</script>
<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places">
</script>
<script type="text/javascript">
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(success);
    } else {
        alert("Geo Location is not supported on your current browser!");
    }
    function success(position) {
        var lat = position.coords.latitude;
        var long = position.coords.longitude;
        var city = position.coords.locality;
        var myLatlng = new google.maps.LatLng(lat, long);
        var myOptions = {
            center: myLatlng,
            zoom: 12,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        var marker = new google.maps.Marker({
            position: myLatlng,
            title: "lat: " + lat + " long: " + long
        });

        marker.setMap(map);
        var infowindow = new google.maps.InfoWindow({ content: "<font color=black><b>User Address</b><br/> Latitude:" + lat + "<br /> Longitude:" + long + "" });
        infowindow.open(map, marker);
    }
</script>
    <title>ea-search-fileupload</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="css/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="css/coin-slider.css" />
<script type="text/javascript" src="js/cufon-yui.js"></script>
<script type="text/javascript" src="js/cufon-titillium-250.js"></script>
<script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="js/script.js"></script>
<script type="text/javascript" src="js/coin-slider.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main indexpage">
  <div class="header">
    <div class="header_resize">
      <div class="menu_nav">
        <ul>
         <li class="active">
             <asp:Button ID="Button4" runat="server" BackColor="#3399FF" BorderStyle="Ridge" 
                 Height="50px" onclick="Button4_Click" Text="logout" Width="75px" />
            </li>
        </ul>
      </div>
      <div class="logo">
        <h1><a href="index.html">Semantic <small>search engine</small></a></h1>
      </div>
      <div class="clr"></div>
<div id="map_canvas" style="width: 927px; height: 323px"></div>
      </div>
      <div class="clr"></div>
    </div>
  </div>
  <div class="content">
    <div class="content_resize">
      <div class="sidebar">
        <div class="clr"></div>
        <div class="gadget">
            <h2 class="star">&nbsp;</h2>
            <p class="star">&nbsp;</p>
            <h2 class="star">&nbsp;<span class="style1">Options</span></h2>
          <div class="clr"></div>
          <ul class="sb_menu">
            <li><a href="#">
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" 
                    Font-Size="14pt">Add users</asp:LinkButton>
                </a></li>
            <li><a href="#">
                <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click" 
                    Font-Size="14pt">search admins</asp:LinkButton>
                </a></li>
            <li><a href="#">
                <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click" 
                    Font-Size="14pt">Advanced Search</asp:LinkButton>
                </a></li>
              <li>
                  <asp:LinkButton ID="LinkButton6" runat="server" onclick="LinkButton6_Click" 
                      Font-Size="14pt">search Specfic user</asp:LinkButton>
              </li>
              <li>
                  <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton4_Click" 
                      Font-Size="14pt">Search map</asp:LinkButton>
              </li>
              <li>
              </li>
          </ul>
            <p>
                &nbsp;</p>
        </div>
      </div>
      <div class="mainbar">
        <div class="article">
          <h2>Search</h2>
            <p>
          <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="search" 
                  BackColor="#99CCFF" BorderStyle="Ridge" ForeColor="Gray" Width="364px" 
                    Font-Bold="True" Font-Names="Lucida Fax" Font-Size="20pt" Height="37px" />
            </p>
            <p>
                &nbsp;</p>
            <p class="style1">U<span>pload File</span></p>
          <p class="infopost">
        <asp:FileUpload ID="FileUpload22" runat="server" Height="26px" 
              style="margin-left: 60px" Width="211px" BackColor="#99CCFF" 
                  BorderColor="#99CCFF" BorderStyle="Ridge" CssClass="button" Font-Names="Arial" 
                  ForeColor="Gray" />
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>Arabic</asp:ListItem>
                    <asp:ListItem>English</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p class="infopost">
                <asp:Label ID="Label2" runat="server" Text="File keywords"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </p>
            <p class="infopost">
                <asp:Label ID="Label3" runat="server" Text="Copany file"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </p>
            <p class="infopost">
          <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Upload file" 
              style="margin-bottom: 0px" Width="350px" BackColor="#99CCFF" BorderStyle="Ridge" 
                    ForeColor="Gray" Height="34px" Font-Names="Lucida Fax" Font-Size="16pt" />
          
            </p>
          <div class="clr"></div>
          <div class="clr"></div>
            <p class="infopost">
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
              Text="Indexing to search" BorderStyle="Ridge" CssClass="button" ForeColor="Gray" 
              Height="34px" Width="349px" BackColor="#99CCFF" Font-Names="Lucida Fax" 
                    Font-Size="16pt" />
            </p>
        </div>
        <div class="article">
          <div class="clr"></div>
        </div>
      </div>
      <div class="clr">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
          </div>
    </div>
  </div>
</div>
    </form>
    </body>
</html>
