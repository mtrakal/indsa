using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aplikace;

namespace aplikace {
    public static class Konstanty {
        public static string FORMAT = "{0};{1};{2};{3};{4};{5};{6}";
        public static System.Globalization.CultureInfo CULTUREINFO = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        public static string ABECEDA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string FORMATVRCHOL = "markers.push(new google.maps.Marker({{position: new google.maps.LatLng({1}, {2}), map: map, title:\"{0}\", icon:icon}}));\r\n";
        public static string FORMATAUTO = "markers.push(new google.maps.Marker({{position: new google.maps.LatLng({1}, {2}), map: map, title:\"{0}\", icon:auto}}));\r\n";
        public static string FORMATHRANA = "new google.maps.Polyline({{path: [new google.maps.LatLng({0}, {1}), new google.maps.LatLng({2},{3})], strokeColor: \"#0000ff\", strokeOpacity: 0.5, strokeWeight: 1.5}}).setMap(map);\r\n" +
        "var label = new Label({{map: map}}); label.bindTo('position', new google.maps.Marker({{icon:\"pixel.gif\", position: new google.maps.LatLng({4}, {5}), draggable: false, map: map}}), 'position'); label.set('text', '{6} km');\r\n";
        public static string FORMATHRANANESJIZDNA = "new google.maps.Polyline({{path: [new google.maps.LatLng({0}, {1}), new google.maps.LatLng({2},{3})], strokeColor: \"#ff0000\", strokeOpacity: 0.5, strokeWeight: 1.5}}).setMap(map);\r\n" +
        "var label = new Label({{map: map}}); label.bindTo('position', new google.maps.Marker({{icon:\"pixel.gif\", position: new google.maps.LatLng({4}, {5}), draggable: false, map: map}}), 'position'); label.set('text', '{6} km');\r\n";
        public static string FORMATHRANAVYBRANA = "new google.maps.Polyline({{path: [new google.maps.LatLng({0}, {1}), new google.maps.LatLng({2},{3})], strokeColor: \"#00ff00\", strokeOpacity: 0.5, strokeWeight: 4}}).setMap(map);\r\n";

        public static double VypocitejVzdalenost(CestyGraf.Vrchol pocatek, CestyGraf.Vrchol konec) {
            double d2r = Math.PI / 180.0;
            double dlong = ((konec.Souradnice.X - pocatek.Souradnice.X)) * d2r;
            double dlat = ((konec.Souradnice.Y - pocatek.Souradnice.Y)) * d2r;
            double a = Math.Pow(Math.Sin(dlat / 2.0), 2) + Math.Cos(pocatek.Souradnice.Y * d2r) * Math.Cos(konec.Souradnice.Y * d2r) * Math.Pow(Math.Sin(dlong / 2.0), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = 6367 * c;
            return Math.Round(d, 2);
        }
    }
}
