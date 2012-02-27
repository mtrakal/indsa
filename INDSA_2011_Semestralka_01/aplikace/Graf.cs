using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace aplikace {
    class Graf<Tvrchol> {
        public interface IBod {
            double X { get; set; }
            double Y { get; set; }
        }
        public interface IVrchol {
            string Data { get; set; }
            IBod Souradnice { get; set; }
        }
        public interface IHrana {
            string Nazev { get; set; }
            IVrchol Vrchol1 { get; set; }
            IVrchol Vrchol2 { get; set; }
            double Metrika { get; set; }
        }
        Dictionary<IBod, IVrchol> vrcholy = new Dictionary<IBod, IVrchol>();
        Dictionary<string, IHrana> hrany = new Dictionary<string, IHrana>();

        #region Vrchol
        public void Pridej(IVrchol vrchol) {
            if (vrcholy.ContainsKey(vrchol.Souradnice)) {
                throw new Exception("Klíč již existuje Vrcholy-Pridej");
            }
            vrcholy.Add(vrchol.Souradnice, vrchol);
        }
        public void Pridej(List<IVrchol> vrcholy) {
            foreach (IVrchol item in vrcholy) {
                Pridej(item);
            }
        }
        public void Odeber(IBod souradnice) {
            if (vrcholy.ContainsKey(souradnice)) {
                vrcholy.Remove(souradnice);
            } else {
                throw new ArgumentException("Neznámý parametr klíče Vrcholy-Odeber!");
            }
        }
        public IEnumerator GetEnumeratorVrcholy() {
            foreach (KeyValuePair<IBod, IVrchol> item in vrcholy) {
                yield return item.Value;
            }
        }
        public IVrchol Dej(IBod souradnice) {
            if (vrcholy.ContainsKey(souradnice)) {
                return vrcholy[souradnice];
            } else {
                return null;
            }
        }
        public List<IVrchol> DejVrcholy() {
            List<IVrchol> vystup = new List<IVrchol>();
            foreach (KeyValuePair<IBod, IVrchol> item in vrcholy) {
                vystup.Add(item.Value);
            }
            return vystup;
        }
        #endregion

        #region Hrany
        public void Pridej(IHrana hrana) {
            if (hrany.ContainsKey(hrana.Nazev)) {
                throw new Exception("Klíč již existuje Hrany-Pridej");
            }
            hrany.Add(hrana.Nazev, hrana);
        }
        public void Pridej(List<IHrana> hrany) {
            foreach (IHrana item in hrany) {
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

        public IEnumerator GetEnumeratorHrany() {
            foreach (KeyValuePair<string, IHrana> item in hrany) {
                yield return item.Value;
            }
        }
        public IHrana Dej(string nazev) {
            if (hrany.ContainsKey(nazev)) {
                return hrany[nazev];
            } else {
                return null;
            }
        }
        public List<IHrana> DejHrany() {
            List<IHrana> vystup = new List<IHrana>();
            foreach (KeyValuePair<string, IHrana> item in hrany) {
                vystup.Add(item.Value);
            }
            return vystup;
        }
        #endregion
    }
}
