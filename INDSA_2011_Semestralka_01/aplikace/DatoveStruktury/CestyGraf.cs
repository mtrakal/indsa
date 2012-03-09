using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace aplikace.DatoveStruktury {
    public class CestyGraf : Graf<string, string, double> {
        public class Bod : IBod {
            public double X { get; set; }
            public double Y { get; set; }
            public Bod() {

            }
            public Bod(double x, double y) {
                X = x;
                Y = y;
            }
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
        public class Hrana : IHrana {
            public double Metrika { get; set; }
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

            public string Data {
                get;
                set;
            }

            public IVrchol Vrchol1 {
                get;
                set;
            }

            public IVrchol Vrchol2 {
                get;
                set;
            }
        }
        public class Vrchol : IVrchol {
            IBod souradnice = new Bod();
            public IBod Souradnice { get { return this.souradnice; } set { this.souradnice = value; } }
            public string Data { get; set; }

            Hrany seznamHran = new Hrany();

            public void PridejHranu(IHrana hrana) {
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

            public List<IHrana> DejHrany() {
                return seznamHran.Dej();
            }

            IHrana IVrchol.DejHranu(string nazevHrany) {
                return seznamHran.Dej(nazevHrany);
            }
        }
        public class Vrcholy : IEnumerable {
            Dictionary<Bod, Vrchol> vrcholy = new Dictionary<Bod, Vrchol>();

            public void Pridej(Vrchol vrchol) {
                if (vrcholy.ContainsKey(vrchol.Souradnice as Bod)) {
                    throw new Exception("Klíč již existuje Vrcholy");
                }
                vrcholy.Add(vrchol.Souradnice as Bod, vrchol);
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
                    if (item.Value.Data == nazev) {
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
            public int Count { get { return vrcholy.Count; } }
        }
        public class Hrany : IEnumerable {
            Dictionary<string, Hrana> hrany = new Dictionary<string, Hrana>();

            public void Pridej(IHrana hrana) {
                if (hrany.ContainsKey(hrana.Data)) {
                    throw new Exception("Klíč již existuje Hrany");
                    //return;
                }
                hrany.Add(hrana.Data, hrana as Hrana);
            }
            public void Pridej(string nazev, IVrchol vrchol1, IVrchol vrchol2, double metrika, bool sjizdna) {
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
            public List<IHrana> Dej() {
                List<IHrana> vystup = new List<IHrana>();
                foreach (KeyValuePair<string, Hrana> item in hrany) {
                    vystup.Add(item.Value);
                }
                return vystup;
            }
        }
    }
}
