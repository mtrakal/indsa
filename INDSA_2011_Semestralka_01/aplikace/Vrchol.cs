using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikace {
    public class Vrchol {
        public Bod Souradnice { get; set; }
        public string Nazev { get; set; }

        Hrany seznamHran = new Hrany();

        public void PridejHranu(Hrana hrana) {
            seznamHran.Pridej(hrana);
        }

        public Hrana DejHranu(string nazevHrany) {
            return seznamHran.Dej(nazevHrany);
        }

        public void OdeberHranu(string nazevHrany) {
            seznamHran.Odeber(nazevHrany);
        }

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
