using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace aplikace {
    class Hrany : IEnumerable {
        Dictionary<string, Hrana> hrany = new Dictionary<string, Hrana>();

        public void Pridej(Graf<string, string, double>.IHrana hrana) {
            if (hrany.ContainsKey(hrana.Data)) {
                throw new Exception("Klíč již existuje Hrany");
                //return;
            }
            hrany.Add(hrana.Data, hrana as Hrana);
        }
        public void Pridej(string nazev, Graf<string, string, double>.IVrchol vrchol1, Graf<string, string, double>.IVrchol vrchol2, double metrika, bool sjizdna) {
            Pridej(new Hrana(nazev, vrchol1, vrchol2, metrika, sjizdna));
        }
        public void Pridej(List<Hrana> hrany) {
            foreach (Hrana item in hrany) {
                Pridej(item);
            }
        }
        public void Odeber(string nazev) {
            if (hrany.ContainsKey(nazev)) {
                hrany.Remove(nazev);
            } else {
                throw new ArgumentException("Neznámý parametr klíče v Hrany-Odeber!");
            }
        }

        public IEnumerator GetEnumerator() {
            foreach (KeyValuePair<string, Hrana> item in hrany) {
                yield return item.Value;
            }
        }
        public Graf<string, string, double>.IHrana Dej(string nazev) {
            if (hrany.ContainsKey(nazev)) {
                return hrany[nazev];
            } else {
                return null;
            }
        }
        public List<Graf<string, string, double>.IHrana> Dej() {
            List<Graf<string, string, double>.IHrana> vystup = new List<Graf<string, string, double>.IHrana>();
            foreach (KeyValuePair<string, Hrana> item in hrany) {
                vystup.Add(item.Value);
            }
            return vystup;
        }
    }
}
