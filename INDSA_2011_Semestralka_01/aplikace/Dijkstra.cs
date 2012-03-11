using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aplikace.DatoveStruktury;
using System.Collections;

namespace aplikace {
    class Dijkstra {
        private class DVrchol : IEqualityComparer<DVrchol>, IEquatable<DVrchol>, IComparer, IComparable {
            public CestyGraf.Vrchol Data { get; set; }
            public DVrchol Predchudce { get; set; }
            public CestyGraf.Hrana Silnice { get; set; }
            public DVrchol() {
                Predchudce = null;
                Silnice = null;
            }
            public DVrchol(CestyGraf.Vrchol data)
                : this() {
                Data = data;
            }
            public DVrchol(CestyGraf.Vrchol data, DVrchol predchudce)
                : this(data) {
                Predchudce = predchudce;
            }
            public DVrchol(CestyGraf.Vrchol data, DVrchol predchudce, CestyGraf.Hrana silnice)
                : this(data, predchudce) {
                Silnice = silnice;
            }
            public override bool Equals(object obj) {
                return Data == (obj as DVrchol).Data;
            }

            public bool Equals(DVrchol x, DVrchol y) {
                return (x.Data == y.Data);
            }

            public int GetHashCode(DVrchol obj) {
                throw new NotImplementedException();
            }

            public bool Equals(DVrchol other) {
                if (ReferenceEquals(other, null))
                    return false;

                if (ReferenceEquals(this, other))
                    return true;

                return this.Data == other.Data;
            }

            public int Compare(object x, object y) {
                if ((x as DVrchol).Silnice.Metrika > (y as DVrchol).Silnice.Metrika) {
                    return 1;
                }
                if ((x as DVrchol).Silnice.Metrika < (y as DVrchol).Silnice.Metrika) {
                    return -1;
                }
                return 0;
            }

            public int CompareTo(object obj) {
                return Compare(this, obj);
            }
            // (Ohodnoceni(a.Silnice.Metrika,cil.Vrchol1,2))
            public double Ohodnoceni(CestyGraf.Vrchol cil, double heuristika) {
                return heuristika * Math.Sqrt(Math.Pow(Data.Souradnice.X - cil.Souradnice.X, 2) + Math.Pow(Data.Souradnice.Y - cil.Souradnice.Y, 2));
            }
        }

        public static LinkedList<CestyGraf.Hrana> doDijkstra(CestyGraf graf, Auto startPozice, CestyGraf.Hrana cil) {
            bool nalezenPrvniVyskyt = false;
            double[] vzdalenost = new double[2];
            vzdalenost[0] = 0;
            vzdalenost[1] = 0;
            LinkedList<CestyGraf.Hrana>[] cilovaCesta = new LinkedList<CestyGraf.Hrana>[2];
            cilovaCesta[0] = new LinkedList<CestyGraf.Hrana>();
            cilovaCesta[1] = new LinkedList<CestyGraf.Hrana>();
            List<DVrchol> otevrene = new List<DVrchol>();
            List<DVrchol> uzavrene = new List<DVrchol>();
            DVrchol zkoumany = new DVrchol();
            //TODO zkontrolovat nedestruktivnost foreachů vůči datům!
            foreach (CestyGraf.Hrana item in startPozice.HranaPoloha.Vrchol1.DejHrany()) {
                item.Metrika += startPozice.VzdalenostOdV1;
            }
            foreach (CestyGraf.Hrana item in startPozice.HranaPoloha.Vrchol2.DejHrany()) {
                item.Metrika += startPozice.VzdalenostOdV2;
            }
            otevrene.Add(new DVrchol(startPozice.HranaPoloha.Vrchol1 as CestyGraf.Vrchol, null, startPozice.HranaPoloha));
            otevrene.Add(new DVrchol(startPozice.HranaPoloha.Vrchol2 as CestyGraf.Vrchol, null, startPozice.HranaPoloha));

            while (otevrene.Count != 0) {
                //zkoumany = otevrene[0]; // prozkoumá první vrchol
                //otevrene.Sort((b, a) => (a.Ohodnoceni(cil.Vrchol1 as CestyGraf.Vrchol, 4)).CompareTo(b.Ohodnoceni(cil.Vrchol1 as CestyGraf.Vrchol, 4)));
                otevrene.Sort((b, a) => (a.Silnice.Metrika).CompareTo(b.Silnice.Metrika));
                zkoumany = otevrene[0];
                otevrene.RemoveAt(otevrene.IndexOf(zkoumany));
                uzavrene.Add(zkoumany);
                if (zkoumany.Data == cil.Vrchol1 || zkoumany.Data == cil.Vrchol2) {
                    DVrchol aktualniCilovy = zkoumany;
                    while (aktualniCilovy.Predchudce != null) {
                        vzdalenost[(nalezenPrvniVyskyt ? 1 : 0)] += aktualniCilovy.Silnice.Metrika;
                        cilovaCesta[(nalezenPrvniVyskyt ? 1 : 0)].AddFirst(aktualniCilovy.Silnice);
                        aktualniCilovy = aktualniCilovy.Predchudce;
                    }
                    cilovaCesta[(nalezenPrvniVyskyt ? 1 : 0)].AddFirst(aktualniCilovy.Silnice);
                    vzdalenost[(nalezenPrvniVyskyt ? 1 : 0)] += aktualniCilovy.Silnice.Metrika;

                    if (!nalezenPrvniVyskyt) {
                        nalezenPrvniVyskyt = true;
                    } else {
                        if (vzdalenost[1] < vzdalenost[0]) {
                            return cilovaCesta[1];
                        }
                        return cilovaCesta[0];
                    }

                }
                //throw new Exception("Nalezena cesta :)");
                foreach (CestyGraf.Hrana item in zkoumany.Data.DejHrany()) { // z zkoum.pro každou hranu přidat do otevřených koncový bod
                    DVrchol v1 = new DVrchol(item.Vrchol1 as CestyGraf.Vrchol, zkoumany, item);
                    DVrchol v2 = new DVrchol(item.Vrchol2 as CestyGraf.Vrchol, zkoumany, item);
                    if (zkoumany.Data.Equals(item.Vrchol1)) { // pokud je vrchol vrcholem 1, potřebujeme přidat vrchol 2, jinak vrchol 1 do seznamu zkoumaných vrcholů
                        if (!(otevrene.Contains(v2) || uzavrene.Contains(v2))) {
                            otevrene.Add(v2);
                        }
                    } else {
                        if (!(otevrene.Contains(v1) || uzavrene.Contains(v1))) {
                            otevrene.Add(v1);
                        }
                    }
                }

            }
            return cilovaCesta[0];
        }

    }
}
