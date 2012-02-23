using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikace {
    public static class Konstanty {
        public static string FORMAT = "{0};{1};{2};{3};{4};{5};{6}";
        public static string ABECEDA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string FORMATVRCHOL = "var myLatLng = new google.maps.LatLng({1}, {2});\r\n" +
            "var marker = new google.maps.Marker({{position: myLatLng, map: map, title:\"{0}\", icon:icon}});\r\n" +
            "marker.setMap(map);\r\n";
        public static string FORMATHRANA = "var routeCoords = [new google.maps.LatLng({0}, {1}), new google.maps.LatLng({2},{3})];\r\n" +
            "var route = new google.maps.Polyline({{path: routeCoords, strokeColor: \"#0000ff\", strokeOpacity: 0.5, strokeWeight: 1.5}});\r\n" +
            "route.setMap(map);\r\n" +
            "var marker = new google.maps.Marker({{icon:\"pixel.gif\", position: new google.maps.LatLng({4}, {5}), draggable: false, map: map}});\r\n" +
            "var label = new Label({{map: map}}); label.bindTo('position', marker, 'position'); label.set('text', '{6}');\r\n";
    }
}
