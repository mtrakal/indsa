using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikace {
    class Vrchol {
        public Bod Souradnice { get; set; }
        public string Nazev { get; set; }
        public override string ToString() {
            return string.Format("{0}: {1},{2}", Nazev, Souradnice.X, Souradnice.Y);
        }
        public Vrchol() {

        }
        public Vrchol(string nazev, double x, double y)
            : this(nazev, new Bod(x, y)) { }
        public Vrchol(string nazev, Bod souradnice) {
            Nazev = nazev;
            Souradnice = souradnice;
        }
    }
}
