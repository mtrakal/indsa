using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using cz.mtrakal.ADT.ADTPriorityQueue;

namespace aplikace {
    class Dijkstra {
        private class DVrchol/* : IEqualityComparer<DVrchol>, IEquatable<DVrchol>*/ {
            public CestyGraf.Vrchol Data { get; set; }
            public DVrchol Predchudce { get; set; }
            public CestyGraf.Hrana SilniceDoPredchudce { get; set; }
            public double MetrikaOdStartu { get; set; }
            public DVrchol() { Predchudce = null; SilniceDoPredchudce = null; }
            public DVrchol(CestyGraf.Vrchol data) : this() { Data = data; }
            public DVrchol(CestyGraf.Vrchol data, DVrchol predchudce) : this(data) { Predchudce = predchudce; }
            public DVrchol(CestyGraf.Vrchol data, DVrchol predchudce, CestyGraf.Hrana silnice) : this(data, predchudce) { SilniceDoPredchudce = silnice; }
            public DVrchol(CestyGraf.Vrchol data, DVrchol predchudce, CestyGraf.Hrana silnice, double metrikaOdStartu) : this(data, predchudce, silnice) { this.MetrikaOdStartu = metrikaOdStartu; }
            //public override bool Equals(object obj) { return Data == (obj as DVrchol).Data; }
            //public bool Equals(DVrchol x, DVrchol y) { return (x.Data == y.Data); }
            //public int GetHashCode(DVrchol obj) { throw new NotImplementedException(); }
            //public bool Equals(DVrchol other) {
            //    if (ReferenceEquals(other, null))
            //        return false;

            //    if (ReferenceEquals(this, other))
            //        return true;

            //    return this.Data == other.Data;
            //}
        }

        public static LinkedList<CestyGraf.Hrana> doDijkstra(CestyGraf graf, Auto startPozice, CestyGraf.Hrana cil) {
            int pocetProhledanych = 0;
            Stopwatch stopky = new Stopwatch();
            stopky.Start();
            double vzdalenost = 0; ;
            LinkedList<CestyGraf.Hrana> cilovaCesta = new LinkedList<CestyGraf.Hrana>();
            DVrchol cilPosledni = new DVrchol();

            PriorityQueue<double, DVrchol, CestyGraf.IBod> otevrene = new PriorityQueue<double, DVrchol, CestyGraf.IBod>(graf.CountVrcholy());
            Dictionary<CestyGraf.IBod, DVrchol> uzavrene = new Dictionary<CestyGraf.IBod, DVrchol>(graf.CountVrcholy());

            DVrchol zkoumany = new DVrchol();
            otevrene.Enqueue(startPozice.VzdalenostOdV1, new DVrchol(startPozice.HranaPoloha.Vrchol1 as CestyGraf.Vrchol, null, startPozice.HranaPoloha, startPozice.VzdalenostOdV1), startPozice.HranaPoloha.Vrchol1.Souradnice);
            otevrene.Enqueue(startPozice.VzdalenostOdV2, new DVrchol(startPozice.HranaPoloha.Vrchol2 as CestyGraf.Vrchol, null, startPozice.HranaPoloha, startPozice.VzdalenostOdV2), startPozice.HranaPoloha.Vrchol2.Souradnice);

            while (otevrene.Count != 0) {
                zkoumany = otevrene.Dequeue().Value;

                if (zkoumany.Data == cil.Vrchol1 || zkoumany.Data == cil.Vrchol2) { // je nalezen cíl
                    if (cilPosledni.Data != null) {
                        if (cilPosledni.MetrikaOdStartu > zkoumany.MetrikaOdStartu) {
                            cilPosledni = zkoumany; // nebude pouze ukazatel, který se neustále mění?
                            Debug.WriteLine("Další výskyt: " + cilPosledni.MetrikaOdStartu + ", vrchol: " + zkoumany.Data.Data);
                        }
                    } else {
                        cilPosledni = zkoumany;
                        Debug.WriteLine("První výskyt: " + cilPosledni.MetrikaOdStartu + ", vrchol: " + zkoumany.Data.Data);
                        break;
                    }
                }

                foreach (CestyGraf.Hrana item in zkoumany.Data.DejHrany()) { // z zkoum. pro každou hranu přidat do otevřených koncový bod         
                    if (!item.Sjizdna) {
                        continue;
                    }
                    pocetProhledanych++;
                    DVrchol v1 = new DVrchol(item.Vrchol1 as CestyGraf.Vrchol, zkoumany, item, zkoumany.MetrikaOdStartu + item.Metrika);
                    DVrchol v2 = new DVrchol(item.Vrchol2 as CestyGraf.Vrchol, zkoumany, item, zkoumany.MetrikaOdStartu + item.Metrika);

                    if (zkoumany.Data.Equals(item.Vrchol1)) {
                        otevrene.Enqueue(v2.MetrikaOdStartu, v2, v2.Data.Souradnice);
                    } else {
                        otevrene.Enqueue(v1.MetrikaOdStartu, v1, v1.Data.Souradnice);
                    }
                }
            }
            Debug.WriteLine("Poslední výskyt: " + cilPosledni.MetrikaOdStartu + ", vrchol: " + cilPosledni.Data.Data);
            while (cilPosledni.Predchudce != null) {
                vzdalenost += cilPosledni.SilniceDoPredchudce.Metrika;
                cilovaCesta.AddFirst(cilPosledni.SilniceDoPredchudce);
                cilPosledni = cilPosledni.Predchudce;
            }

            stopky.Stop();
            Debug.WriteLine("Milisekund: " + stopky.ElapsedMilliseconds + ", Tiků: " + stopky.ElapsedTicks + ", Prohledaných stavů celkem: " + pocetProhledanych);
            return cilovaCesta;
        }
    }
}
