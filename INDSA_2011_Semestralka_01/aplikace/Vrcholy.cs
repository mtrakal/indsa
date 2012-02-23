using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace aplikace {
    class Vrcholy : IEnumerable {
        Dictionary<Bod, Vrchol> vrcholy = new Dictionary<Bod, Vrchol>();

        public void Pridej(Vrchol vrchol) {
            if (vrcholy.ContainsKey(vrchol.Souradnice)) {
                throw new Exception("Klíč již existuje Vrcholy");
                //return;
            }
            vrcholy.Add(vrchol.Souradnice, vrchol);
        }
        public void Pridej(string nazev, Bod souradnice) {
            Pridej(new Vrchol(nazev, souradnice));
        }
        public void Pridej(List<Vrchol> vrcholy) {
            foreach (Vrchol item in vrcholy) {
                Pridej(item);
            }
        }
        public void Odeber(Bod souradnice) {
            if (vrcholy.ContainsKey(souradnice)) {
                vrcholy.Remove(souradnice);
            } else {
                throw new ArgumentException("Neznámý parametr klíče v Vrcholy-Odeber!");
            }
        }
        public IEnumerator GetEnumerator() {
            foreach (KeyValuePair<Bod, Vrchol> item in vrcholy) {
                yield return item.Value;
            }
        }
        public Vrchol Dej(Bod souradnice) {
            if (vrcholy.ContainsKey(souradnice)) {
                return vrcholy[souradnice];
            } else {
                return null;
            }
        }
        public Vrchol Dej(string nazev) {
            foreach (KeyValuePair<Bod, Vrchol> item in vrcholy) {
                if (item.Value.Nazev == nazev) {
                    return item.Value;
                }
            }
            return null;
        }
        public List<Vrchol> Dej() {
            List<Vrchol> vystup = new List<Vrchol>();
            foreach (KeyValuePair<Bod, Vrchol> item in vrcholy) {
                vystup.Add(item.Value);
            }
            return vystup;
        }

    }
}
