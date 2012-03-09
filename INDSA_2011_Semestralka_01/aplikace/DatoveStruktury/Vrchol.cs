using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikace {
    public class Vrchol : Graf<string, string, double>.IVrchol {
        public Graf<string, string, double>.IBod Souradnice { get; set; }
        public string Data { get; set; }

        Hrany seznamHran = new Hrany();

        public void PridejHranu(Graf<string, string, double>.IHrana hrana) {
            seznamHran.Pridej(hrana);
        }

        public void OdeberHranu(string nazevHrany) {
            seznamHran.Odeber(nazevHrany);
        }

        public override string ToString() {
            return string.Format("{0}: {1},{2}", Data, Souradnice.X, Souradnice.Y);
        }
        public Vrchol() {

        }
        public Vrchol(string nazev, double x, double y)
            : this(nazev, new Bod(x, y)) { }
        public Vrchol(string nazev, Bod souradnice) {
            Data = nazev;
            Souradnice = souradnice;
        }

        public List<Graf<string, string, double>.IHrana> DejHrany() {
            return seznamHran.Dej();
        }

        Graf<string, string, double>.IHrana Graf<string, string, double>.IVrchol.DejHranu(string nazevHrany) {
            return seznamHran.Dej(nazevHrany);
        }
    }
}
