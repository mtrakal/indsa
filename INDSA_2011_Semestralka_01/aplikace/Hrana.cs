using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace aplikace {
    class Hrana {
        public string Nazev { get; set; }
        public Vrchol Vrchol1 { get; set; }
        public Vrchol Vrchol2 { get; set; }
        public double Metrika { get; set; }
        public bool Sjizdna { get; set; }
        public Hrana() {

        }
        public Hrana(string nazev, Vrchol p1, Vrchol p2, double metrika, bool sjizdna) {
            Nazev = nazev;
            Vrchol1 = p1;
            Vrchol2 = p2;
            Metrika = metrika;
            Sjizdna = sjizdna;
        }
        public override string ToString() {
            return Nazev + ": " + Vrchol1.Nazev + " - " + Vrchol2.Nazev + ": " + Metrika + "," + Sjizdna;
        }
    }
}
