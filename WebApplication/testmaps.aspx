<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testmaps.aspx.cs" Inherits="WebApplication.testmaps" %>

<%@ Register assembly="GMaps" namespace="Subgurim.Controles" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places">
</script>
<script type="text/javascript">
    $('#gmap-3').live("pageshow", function () {
        // Set the map to some default center
        $('#map_canvas').gmap({ 'center': getLatLng() });
        // Try to get the user's position
        $('#map_canvas').gmap('getCurrentPosition', function (status, position) {
            if (status === 'OK') {
                $('#map_canvas').gmap({ 'center': new google.maps.LatLng(position.coords.latitude, position.coords.longitude) });
                $('#from').val(position.coords.latitude + ',' + position.coords.longitude);
            } else {
                // do some error handling
            }
        });

        // Get the google loaders client location
        // If it fails, return some defult value
        function getLatLng() {
            if (google.loader.ClientLocation != null)
                return new google.maps.LatLng(google.loader.ClientLocation.latitude, google.loader.ClientLocation.longitude);
            return new google.maps.LatLng(59.3426606750, 18.0736160278);
        }

    });

    // Bind the click event to the  displayDirections function so that 
    // whenever a user clicks on the 'submit' button it will call and show directions
    $('#gmap-3').live("pagecreate", function () {
        $('#submit').click(function () {
            $('#map_canvas').gmap('displayDirections', { 'origin': $('#from').val(), 'destination': $('#to').val(), 'travelMode': google.maps.DirectionsTravelMode.DRIVING }, { 'panel': document.getElementById('directions') }, function (success, response) {
                if (success) {
                    $('#results').show();
                } else {
                    $('#results').hide();
                }
            });
            return false;
        });
    });
</script>
</head>

<body>
    <form id="form1" runat="server">
    
    <div>
    
    
    </div>
   <div>   
        <!-- The map will be loaded here, set width and height in the css to the map_canvas element -->
        <div id="map_canvas" style="height:300px;width:300px;"></div>
        <!-- The javascript will set the from ID -->
        <p style="display:none;">
                <label for="from">From</label>
                <input id="from" type="hidden" class="ui-bar-c" value="" />
        </p>
        <!-- The user will set the 'to' field -->
        <p>
                <label for="to">To</label>
                <input id="to" class="ui-bar-c" value="Stockholm, Sweden" />
        </p>
        <a id="submit" href="#" data-role="button" data-icon="search">Get directions</a>
</div>

<!-- results will ony show if there is a result to show -->
<div id="results" style="display:none;">
        <!-- the directions will be loaded in the directions div -->
        <div id="directions"></div>
</div>
    </form>
</body>
</html>
