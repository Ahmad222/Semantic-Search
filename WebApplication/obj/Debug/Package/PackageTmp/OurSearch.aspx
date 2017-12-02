<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OurSearch.aspx.cs" Inherits="WebApplication.OurSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places">
</script><script type="text/javascript">
             if (navigator.geolocation) {
                 navigator.geolocation.getCurrentPosition(success);
             } else {
                 alert("Geo Location is not supported on your current browser!");
             }
             function success(position) {
                var lat =position.coords.latitude;
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
                 return lat;
             }
</script>
<head runat="server">

    <title></title>
    <style type="text/css">
        .style1
        {
            width: 1215px;
            height: 268px;
        }
        .style2
        {
            width: 562px;
        }
        .style3
        {
            width: 113px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <br />
        <table style="width:80%;">
            <tr>
                <td colspan="4">
                    <img alt="keo" class="style1" height="800" src="admin/images/keo.png" /></td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="#66CCFF" 
                        onclick="LinkButton2_Click">Home</asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#66CCFF" 
                        onclick="LinkButton1_Click">Logout</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style2">
    <asp:HiddenField ID="HiddenField2" runat="server" />
                </td>
                <td>
        <asp:TextBox ID="TextBox1" runat="server" Width="225px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Search" />
                </td>
                <td colspan="2">
    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
        <asp:Repeater ID="Repeater1" runat="server">

         <HeaderTemplate>
			<font color="lightgreen">    <p> its took<%=datee %><</p>
		    </HeaderTemplate>
		    <ItemTemplate>
			<font color="black">    <a href="<%# DataBinder.Eval(Container.DataItem, "Url") %>"><b><%# DataBinder.Eval(Container.DataItem, "Title") %></b></a>
			    <!--(infile.Title==""?"&laquo; no title &raquo;":infile.Title)-->
			<font color="lightgreen">    <a href="<%# DataBinder.Eval(Container.DataItem, "Url") %>" target="_blank" title="open in new window" style="font-size:x-small">&uarr;</a>
			    <font color="black">(<%# DataBinder.Eval(Container.DataItem, "Rank") %>)</font>
			  <font color="black">  <br><%# DataBinder.Eval(Container.DataItem, "Description") %>...</font><br><font color="lightgreen"><%# DataBinder.Eval(Container.DataItem, "Url") %> - <%# DataBinder.Eval(Container.DataItem, "Size") %>
			    bytes</font>
			    <font color="lightgreen">- <%# DataBinder.Eval(Container.DataItem, "CrawledDate") %></font><p>
		    </ItemTemplate>
		    <FooterTemplate>
			    <p><%=CreatePagerLinks(_PagedResults, Request.Url.ToString() )%></p>
		    </FooterTemplate>
           
        </asp:Repeater>
        		</td>
            </tr>
        </table>
        <br />
        		<asp:Panel id="lblNoSearchResults" visible="false" runat="server" 
                Width="80%">
			Your search - <b><%=TextBox1.Text%></b> - did not match any documents. 

			It took <%=datee%>.

			<p>Suggestions:</p>
			<ul>
			<li>Check your spelling</li>
			<li>Try similar meaning words (synonyms)</li>
			</ul>
		</asp:Panel>
    </div>

    </form>
</body>
</html>
