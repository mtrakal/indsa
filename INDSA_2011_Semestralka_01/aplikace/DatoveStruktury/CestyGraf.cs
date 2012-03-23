using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cz.mtrakal.ADT;

namespace aplikace {
    public class CestyGraf : Graf<double, string, string, double> {
        public class Bod : Graf<double, string, string, double>.GBod {
            public Bod() : base() { }
            public Bod(double x, double y) : base(x, y) { }
            public override bool Equals(object obj) {
                if (this.X == (obj as Bod).X && this.Y == (obj as Bod).Y) {
                    return true;
                }
                return false;
            }
            public override int GetHashCode() {
                if (X == 0 && Y == 0) {
                    // Ensure that 0 and -0 have the same hash code 
                    return 0;
                }
                long value = (long)(X + Y);
                return unchecked((int)value) ^ ((int)(value >> 32));
            }
        }
        public class Hrana : Graf<double, string, string, double>.GHrana, IComparable, IComparer {
            public bool Sjizdna { get; set; }
            public Hrana() { }
            public Hrana(string nazev, IVrchol p1, IVrchol p2, double metrika, bool sjizdna) {
                Data = nazev;
                Vrchol1 = p1;
                Vrchol2 = p2;
                Metrika = metrika;
                Sjizdna = sjizdna;
            }
            public override string ToString() {
                return Data + ": " + Vrchol1.Data + " - " + Vrchol2.Data + ": " + Metrika + "km ," + ((Sjizdna == true) ? "Sjízdná" : "Nesjízdná");
            }
            public int Compare(object x, object y) {
                if ((x as Hrana).Metrika > (y as Hrana).Metrika) {
                    return 1;
                }
                if ((x as Hrana).Metrika < (y as Hrana).Metrika) {
                    return -1;
                }
                return 0;
            }

            public int CompareTo(object obj) {
                return Compare(this, obj);
            }
        }
        public class Vrchol : Graf<double, string, string, double>.GVrchol {
            public override string ToString() {
                return string.Format("{0}: {1},{2}", Data, Souradnice.X, Souradnice.Y);
            }
            public Vrchol() { }
            public Vrchol(string nazev, double x, double y)
                : this(nazev, new Bod(x, y)) { }
            public Vrchol(string nazev, Bod souradnice) {
                Data = nazev;
                Souradnice = souradnice;
            }
        }

        public void Pridej(string nazev, Bod souradnice) {
            Pridej(new Vrchol(nazev, souradnice));
        }
        public Vrchol DejVrchol(string nazev) {
            foreach (KeyValuePair<IBod, IVrchol> item in vrcholy) {
                if (item.Value.Data == nazev) {
                    return item.Value as CestyGraf.Vrchol;
                }
            }
            return null;
        }
        public void Pridej(string nazev, IVrchol vrchol1, IVrchol vrchol2, double metrika, bool sjizdna) {
            Pridej(new Hrana(nazev, vrchol1, vrchol2, metrika, sjizdna));
        }
        new public List<Hrana> DejHrany() {
            List<Hrana> vystup = new List<Hrana>();
            foreach (KeyValuePair<string, IHrana> item in hrany) {
                vystup.Add(item.Value as Hrana);
            }
            return vystup;
        }
        new public List<Vrchol> DejVrcholy() {
            List<Vrchol> vystup = new List<Vrchol>();
            foreach (KeyValuePair<IBod, IVrchol> item in vrcholy) {
                vystup.Add(item.Value as Vrchol);
            }
            return vystup;
        }
        internal void Pridej(List<Hrana> list) {
            foreach (Hrana item in list) {
                hrany.Add(item.Data, item);
            }
        }
        internal void Pridej(List<Vrchol> list) {
            foreach (Vrchol item in list) {
                vrcholy.Add(item.Souradnice, item);
            }
        }
        new public IVrchol DejVrchol(double x, double y) {
            return DejVrchol(new Bod(x, y));
        }
    }
}