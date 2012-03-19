using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aplikace.DatoveStruktury;
using System.Collections;
using System.Diagnostics;

namespace aplikace {
    class Dijkstra {
        private class DVrchol : IEqualityComparer<DVrchol>, IEquatable<DVrchol>, IComparer, IComparable {
            public CestyGraf.Vrchol Data { get; set; }
            public DVrchol Predchudce { get; set; }
            public CestyGraf.Hrana Silnice { get; set; }
            public double MetrikaOdStartu { get; set; }
            public DVrchol() { Predchudce = null; Silnice = null; }
            public DVrchol(CestyGraf.Vrchol data) : this() { Data = data; }
            public DVrchol(CestyGraf.Vrchol data, DVrchol predchudce) : this(data) { Predchudce = predchudce; }
            public DVrchol(CestyGraf.Vrchol data, DVrchol predchudce, CestyGraf.Hrana silnice) : this(data, predchudce) { Silnice = silnice; }
            public DVrchol(CestyGraf.Vrchol data, DVrchol predchudce, CestyGraf.Hrana silnice, double metrikaOdStartu) : this(data, predchudce, silnice) { this.MetrikaOdStartu = metrikaOdStartu; }
            public override bool Equals(object obj) { return Data == (obj as DVrchol).Data; }
            public bool Equals(DVrchol x, DVrchol y) { return (x.Data == y.Data); }
            public int GetHashCode(DVrchol obj) { throw new NotImplementedException(); }
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
            public int CompareTo(object obj) { return Compare(this, obj); }
            // (Ohodnoceni(a.Silnice.Metrika,cil.Vrchol1,2))
            public double VzdalenostOd(CestyGraf.Vrchol od) { return Konstanty.VypocitejVzdalenost(Data, od); }
            public double Ohodnoceni(CestyGraf.Vrchol cil, double heuristika) { return heuristika * Math.Sqrt(Math.Pow(Data.Souradnice.X - cil.Souradnice.X, 2) + Math.Pow(Data.Souradnice.Y - cil.Souradnice.Y, 2)); }
        }

        public static LinkedList<CestyGraf.Hrana> doDijkstra(CestyGraf graf, Auto startPozice, CestyGraf.Hrana cil) {
            int pocetProhledanych = 0;
            Stopwatch stopky = new Stopwatch();
            stopky.Start();

            bool jizNalezenaCesta = false;
            double[] vzdalenost = new double[2];
            vzdalenost[0] = 0;
            vzdalenost[1] = 0;
            LinkedList<CestyGraf.Hrana>[] cilovaCesta = new LinkedList<CestyGraf.Hrana>[2];
            cilovaCesta[0] = new LinkedList<CestyGraf.Hrana>();
            cilovaCesta[1] = new LinkedList<CestyGraf.Hrana>();
            BST<DVrchol, double> otevrene = new BST<DVrchol, double>();
            List<DVrchol> otevreneList = new List<DVrchol>();
            List<DVrchol> uzavrene = new List<DVrchol>();

            DVrchol zkoumany = new DVrchol();

            DVrchol cilPosledni = new DVrchol();

            otevrene.Add(new DVrchol(startPozice.HranaPoloha.Vrchol1 as CestyGraf.Vrchol, null, startPozice.HranaPoloha, startPozice.VzdalenostOdV1), startPozice.VzdalenostOdV1);
            otevrene.Add(new DVrchol(startPozice.HranaPoloha.Vrchol2 as CestyGraf.Vrchol, null, startPozice.HranaPoloha, startPozice.VzdalenostOdV2), startPozice.VzdalenostOdV2);

            otevreneList.Add(new DVrchol(startPozice.HranaPoloha.Vrchol1 as CestyGraf.Vrchol, null, startPozice.HranaPoloha, startPozice.VzdalenostOdV1));
            otevreneList.Add(new DVrchol(startPozice.HranaPoloha.Vrchol2 as CestyGraf.Vrchol, null, startPozice.HranaPoloha, startPozice.VzdalenostOdV2));

            while (otevrene.Count != 0) {
                zkoumany = otevrene.DejMax();
                otevreneList.Remove(zkoumany);
                uzavrene.Add(zkoumany);
                if (zkoumany.Data == cil.Vrchol1 || zkoumany.Data == cil.Vrchol2) {
                    if (jizNalezenaCesta) {
                        if (zkoumany.MetrikaOdStartu < cilPosledni.MetrikaOdStartu) {
                            cilPosledni = zkoumany;
                            Debug.WriteLine("Další výskyt: " + cilPosledni.MetrikaOdStartu);
                        }
                    } else {
                        cilPosledni = zkoumany;
                        Debug.WriteLine("První výskyt: " + cilPosledni.MetrikaOdStartu);
                        jizNalezenaCesta = true;
                    }
                    #region MyRegion
                    //DVrchol aktualniCilovy = zkoumany;
                    //while (aktualniCilovy.Predchudce != null) {
                    //    vzdalenost[(jizNalezenaCesta ? 1 : 0)] += aktualniCilovy.Silnice.Metrika;
                    //    cilovaCesta[(jizNalezenaCesta ? 1 : 0)].AddFirst(aktualniCilovy.Silnice);
                    //    aktualniCilovy = aktualniCilovy.Predchudce;
                    //}
                    //cilovaCesta[(jizNalezenaCesta ? 1 : 0)].AddFirst(aktualniCilovy.Silnice);
                    //vzdalenost[(jizNalezenaCesta ? 1 : 0)] += aktualniCilovy.Silnice.Metrika;

                    //if (!jizNalezenaCesta) {
                    //    jizNalezenaCesta = true;
                    //} else {
                    //    if (vzdalenost[1] < vzdalenost[0]) {
                    //        return cilovaCesta[1];
                    //    }
                    //    return cilovaCesta[0];
                    //} 
                    #endregion
                }
                foreach (CestyGraf.Hrana item in zkoumany.Data.DejHrany()) { // z zkoum. pro každou hranu přidat do otevřených koncový bod         
                    if (!item.Sjizdna) {
                        continue;
                    }
                    pocetProhledanych++;
                    DVrchol v1 = new DVrchol(item.Vrchol1 as CestyGraf.Vrchol, zkoumany, item, zkoumany.MetrikaOdStartu + item.Metrika);
                    DVrchol v2 = new DVrchol(item.Vrchol2 as CestyGraf.Vrchol, zkoumany, item, zkoumany.MetrikaOdStartu + item.Metrika);

                    if (zkoumany.Data.Equals(item.Vrchol1)) {
                        if (uzavrene.Contains(v2)) {
                            if (uzavrene[uzavrene.IndexOf(v2)].MetrikaOdStartu > v2.MetrikaOdStartu) {
                                uzavrene.RemoveAt(uzavrene.IndexOf(v2));
                                otevrene.Add(v2, Konstanty.VypocitejVzdalenost(v2.Data, cil.Vrchol1 as CestyGraf.Vrchol));
                                otevreneList.Add(v2);
                            }
                            continue;
                        }
                        if (otevreneList.Contains(v2)) {
                            if (otevreneList[otevreneList.IndexOf(v2)].MetrikaOdStartu > v2.MetrikaOdStartu) {
                                otevreneList.RemoveAt(otevreneList.IndexOf(v2));
                                otevrene.Add(v2, Konstanty.VypocitejVzdalenost(v2.Data, cil.Vrchol1 as CestyGraf.Vrchol));
                                otevreneList.Add(v2);
                            }
                            continue;
                        } else {
                            otevrene.Add(v2, Konstanty.VypocitejVzdalenost(v2.Data, cil.Vrchol1 as CestyGraf.Vrchol));
                            otevreneList.Add(v2);
                        }
                    } else {
                        if (uzavrene.Contains(v1)) {
                            if (uzavrene[uzavrene.IndexOf(v1)].MetrikaOdStartu > v1.MetrikaOdStartu) {
                                uzavrene.RemoveAt(uzavrene.IndexOf(v1));
                                otevrene.Add(v1, Konstanty.VypocitejVzdalenost(v1.Data, cil.Vrchol1 as CestyGraf.Vrchol));
                                otevreneList.Add(v1);
                            }
                            continue;
                        }
                        if (otevreneList.Contains(v1)) {
                            if (otevreneList[otevreneList.IndexOf(v1)].MetrikaOdStartu > v1.MetrikaOdStartu) {
                                otevreneList.RemoveAt(otevreneList.IndexOf(v1));
                                otevrene.Add(v1, Konstanty.VypocitejVzdalenost(v1.Data, cil.Vrchol1 as CestyGraf.Vrchol));
                                otevreneList.Add(v1);
                            }
                            continue;
                        } else {
                            otevrene.Add(v1, Konstanty.VypocitejVzdalenost(v1.Data, cil.Vrchol1 as CestyGraf.Vrchol));
                            otevreneList.Add(v1);
                        }

                    }
                }
            }
            Debug.WriteLine("Poslední výskyt: " + cilPosledni.MetrikaOdStartu);
            while (cilPosledni.Predchudce != null) {
                vzdalenost[0] += cilPosledni.Silnice.Metrika;
                cilovaCesta[0].AddFirst(cilPosledni.Silnice);
                cilPosledni = cilPosledni.Predchudce;
            }

            stopky.Stop();
            Debug.WriteLine("Milisekund: " + stopky.ElapsedMilliseconds + ", Tiků: " + stopky.ElapsedTicks + ", Prohledaných stavů celkem: " + pocetProhledanych);
            return cilovaCesta[0];
        }
    }
}
