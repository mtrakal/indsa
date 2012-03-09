using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace aplikace {
    public class Hrana : Graf<string, string, double>.IHrana {
        public string Data { get; set; }
        public Graf<string, string, double>.IVrchol Vrchol1 { get; set; }
        public Graf<string, string, double>.IVrchol Vrchol2 { get; set; }
        public double Metrika { get; set; }
        public bool Sjizdna { get; set; }
        public Hrana() {

        }
        public Hrana(string nazev, Graf<string, string, double>.IVrchol p1, Graf<string, string, double>.IVrchol p2, double metrika, bool sjizdna) {
            Data = nazev;
            Vrchol1 = p1;
            Vrchol2 = p2;
            Metrika = metrika;
            Sjizdna = sjizdna;
        }
        public override string ToString() {
            return Data + ": " + Vrchol1.Data + " - " + Vrchol2.Data + ": " + Metrika + "km ," + ((Sjizdna == true) ? "Sjízdná" : "Nesjízdná");
        }
    }
}
