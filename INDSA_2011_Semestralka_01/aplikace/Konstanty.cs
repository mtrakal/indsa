using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikace {
    public static class Konstanty {
        public static string FORMAT = "{0};{1};{2};{3};{4};{5};{6}";
        public static System.Globalization.CultureInfo CULTUREINFO = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        public static string ABECEDA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string FORMATVRCHOL = "markers.push(new google.maps.Marker({{position: new google.maps.LatLng({1}, {2}), map: map, title:\"{0}\", icon:icon}}));\r\n";
        public static string FORMATAUTO = "markers.push(new google.maps.Marker({{position: new google.maps.LatLng({1}, {2}), map: map, title:\"{0}\", icon:auto}}));\r\n";
        public static string FORMATHRANA = "new google.maps.Polyline({{path: [new google.maps.LatLng({0}, {1}), new google.maps.LatLng({2},{3})], strokeColor: \"#0000ff\", strokeOpacity: 0.5, strokeWeight: 1.5}}).setMap(map);\r\n"+
        "var label = new Label({{map: map}}); label.bindTo('position', new google.maps.Marker({{icon:\"pixel.gif\", position: new google.maps.LatLng({4}, {5}), draggable: false, map: map}}), 'position'); label.set('text', '{6} km');\r\n";
        public static string FORMATHRANANESJIZDNA = "new google.maps.Polyline({{path: [new google.maps.LatLng({0}, {1}), new google.maps.LatLng({2},{3})], strokeColor: \"#ff0000\", strokeOpacity: 0.5, strokeWeight: 1.5}}).setMap(map);\r\n" +
        "var label = new Label({{map: map}}); label.bindTo('position', new google.maps.Marker({{icon:\"pixel.gif\", position: new google.maps.LatLng({4}, {5}), draggable: false, map: map}}), 'position'); label.set('text', '{6} km');\r\n";
        public static string FORMATHRANAVYBRANA = "new google.maps.Polyline({{path: [new google.maps.LatLng({0}, {1}), new google.maps.LatLng({2},{3})], strokeColor: \"#00ff00\", strokeOpacity: 0.5, strokeWeight: 4}}).setMap(map);\r\n";
    }
}
