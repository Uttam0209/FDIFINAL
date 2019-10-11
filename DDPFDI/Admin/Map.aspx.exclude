<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Map.aspx.cs" Inherits="Admin_Map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
            <script type="text/javascript">
                var markers = [
                <asp:Repeater ID="rptMarkers" runat="server">
                <ItemTemplate>
                            {
                                "title": '<%# Eval("SCAddress1") %>',
                            "lat": '<%# Eval("Latitude") %>',
                            "lng": '<%# Eval("Longitude") %>',
                            "description": '<%# Eval("Description") %>'
                        }
</ItemTemplate>
<SeparatorTemplate>
    ,
</SeparatorTemplate>
</asp:Repeater>
            ];
            </script>
            <script type="text/javascript">
                window.onload = function () {
                    var mapOptions = {
                        center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                        zoom: 12,
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    };
                    var infoWindow = new google.maps.InfoWindow();
                    var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
                    for (i = 0; i < markers.length; i++) {
                        var data = markers[i]
                        var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                        var marker = new google.maps.Marker({
                            position: myLatlng,
                            map: map,
                            title: data.title
                        });
                        (function (marker, data) {
                            google.maps.event.addListener(marker, "click", function (e) {
                                infoWindow.setContent(data.description);
                                infoWindow.open(map, marker);
                            });
                        })(marker, data);
                    }
                }
            </script>

            <div id="dvMap" style="width: 100%; height: 400px">
            </div>
        </div>
    </form>
</body>
</html>
