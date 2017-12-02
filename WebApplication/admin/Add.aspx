<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="WebApplication.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add user</title>
        <style type="text/css">
        .button
        {}
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
             <asp:Button ID="Button2" runat="server" BackColor="#3399FF" BorderStyle="Ridge" 
                 Height="42px" onclick="Button2_Click" Text="Logout" Width="76px" />
            </li>
          <li></li>
        </ul>
      </div>
      <div class="logo">
        <h1><a href="index.html">Semantic </a></h1>
      </div>
      <div class="clr"></div>
<div id="map_canvas" style="width: 927px; height: 323px"></div>
      </div>
      <div class="clr"></div>
    </div>
  </div>
  <div class="content">
    <div class="content_resize">
      <div class="mainbar">
        <div class="article">
          <h2>&nbsp;</h2>
          <div class="clr"></div>
          <div class="img">
            <p>
          
&nbsp;&nbsp;<br />
&nbsp;
                <asp:Label ID="Label2" runat="server" Text="First name:"></asp:Label>
                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        &nbsp;
                <asp:Label ID="Label3" runat="server" Text="Last name"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        &nbsp;
                <asp:Label ID="Label4" runat="server" Text="date of birth"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </p>
              <p>
          &nbsp;
                  <asp:Label ID="Label7" runat="server" Text="User type"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>admin</asp:ListItem>
            <asp:ListItem>user</asp:ListItem>
        </asp:DropDownList>
        <br />
        &nbsp;
                  <asp:Label ID="Label5" runat="server" Text="User name"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        &nbsp;
                  <asp:Label ID="Label6" runat="server" Text="User password"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
          <br />
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Add" 
                      Width="111px" />
        </p>
              <p>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
          <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
            </div>
          <div class="clr"></div>
        </div>
      </div>
      <div class="sidebar">
        <div class="searchform">
            <span>&nbsp;</span>
            </div>
        <div class="clr"></div>
        <div class="gadget">
          <h2 class="star"><span>Search</span> Menu</h2>
   Menu<div class="clr"></div>
          <ul class="sb_menu">
            <li>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">File upload</asp:LinkButton>
              </li>
            <li>
                <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Search</asp:LinkButton>
              </li>
            <li>
                <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">search admin</asp:LinkButton>
              </li>
              <li>
                  <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">Search word throw map</asp:LinkButton>
              </li>
              <li>
                  <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click">search all users</asp:LinkButton>
              </li>
          </ul>
        </div>
          <span>
      </div>
    </div>
  </div>
</div>
        </form>
</body>
</html>
