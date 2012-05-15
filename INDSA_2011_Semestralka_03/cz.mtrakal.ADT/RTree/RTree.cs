/* Poznatky ze cvičení
 * Vyhedávání jednoduché (leží ve čtverci)
 * Intervalové (leží na čtvercích)
 * modifikace sem A (přidání fcí)
 * B strom: má minimálně polovinu prvků kapacity uzlu (pro 3 alespoň 2 prvky)
 * Otázky: v jakém pořadí je zařadit
 * musíme udělat systematický průchod - křivku průchodu, které vyjadřuje pořadí průchodu, což je i pořadí vkládání
 * (g)hilbertovy, kánovy, z-křivky pro průchod (v příkladu Z křivka)
 * tam kde poprvé protne křivka hledaný objekt, zařadím ho do stromu
 * na začátku vybudovat pole listů (zákaldní vrstva), nad ní budovat vyšší vrstvu, která zapouzdří 3 prvky ze spodní vrstvy
 * a nad opět seskupit 3 prvky předchozích skupin.
 * Úrovně listů obsahují reference na bázový prvek na záznam/informaci/data na konkrétní objekt
 * 
 * Mám R-strom a chci bodoví vyhledávání:
 * postup: začnu v kořeni, ptám se (multidimenzionální, musím srovnávat s multidim daty, které jsou v prvcích stromu)
 * hledám bod 30:70, pokrývá obdélník R21 bod? Musí souhlasit obě souřadnice, postupně traverzuji do vrstvy listů, kde jsou
 * elementární prvky. Dojdeme do pole listů, kde najdeme Rm a dáme Rm. Bod leží v Rm, ale leží i na grafickém objektu, který je
 * prvkem Rm zapouzdřen? Na to použijeme znalosti o zapouzdřeném objektu (comparable)?
 * Hledání může skončit neúspěchem a můžeme hedat více větvemi (musíme pouštět paralelně dvěma větvema v tom případě)
 * 
 * Intervalové:
 * Na ploše nakreslíme obdélník (LH 7:3 a PD 17:15) a hledáme rozsah koordinátů (7...17 a 3...15)
 * neperspektivní větve zrovna zahodíme.
 * hledáme perspektivní, kde intervaly leží přímo.
 * 
 * Interpretovat hrany jako úsečky, 
 * minimálně 3 úrovně stromu (ne extrémní - vše v kořeni, nebo 2-3 strom :))
 * List musí mít indexy aby se dalo do posledního prvku přistupovat se složitostí O(1)
 * 
 * Můžeme využít znalostí ze semestrálky A (třeba z grafu) vytáhnout úsečky
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace cz.mtrakal.ADT {
    public class RTree<TKey, TValue> where TKey : IComparable<TKey> {
        /// <summary>
        /// Vnitřní datová složka, kam se ukládají všehcny vrcholy i listy.
        /// </summary>
        public class RVrchol : IComparable<RVrchol> {
            /// <summary>
            /// Datová složka, kam se ukládají objekty vstupních dat. V této datové složce budou hodnoty pouze u poslední vrstvy listů.
            /// </summary>
            public TValue Data { get; set; }
            /// <summary>
            /// Klíč, který slouží pro identifikaci datové složky. Nikterak nevyužitý, max pro název obdélníku.
            /// </summary>
            public TKey Key { get; set; }
            /// <summary>
            /// Ohraničení oblasti obdélníkem.
            /// </summary>
            public RectangleF Oblast { get; set; }
            /// <summary>
            /// Vrchol 1
            /// </summary>
            public PointF V1 { get; set; }
            /// <summary>
            /// Vrchol 2
            /// </summary>
            public PointF V2 { get; set; }
            /// <summary>
            /// Předchůdce (vyšší vrchol, slouží k traverzování mezi vrcholy). Pokud se jedná o kořen, je Rodic == null.
            /// </summary>
            public RVrchol Rodic { get; set; }
            /// <summary>
            /// Pole potomků z nižších vrstev, pokud se jedná o poslední vrstvu, kde jsou již listy, mají nastavenou datovou složku Data.
            /// </summary>
            public List<RVrchol> Potomci { get; set; }
            public string GetZOrder() {
                StringBuilder sb = new StringBuilder();

                string x = Convert.ToString(Convert.ToInt32(Math.Floor(Oblast.X + (Oblast.Width / 2))), 2);
                string y = Convert.ToString(Convert.ToInt32(Math.Floor(Oblast.Y + (Oblast.Height / 2))), 2);
                bool xDelsi = (x.Length > y.Length) ? true : false;
                int minDelka = (xDelsi ? y.Length : x.Length);
                for (int i = 0; i < (xDelsi ? x.Length : y.Length); i++) {
                    if (i >= minDelka) {
                        sb.Append(xDelsi ? "0" + x[i] : y[i] + "0");
                    } else {
                        sb.Append(y[i]);
                        sb.Append(x[i]);
                    }
                }
                //Debug.WriteLine(string.Format("{0,-5}\t{1}\t{2,15}\t{3,10}\t{4,10}\t{5,5}\t{6,5}\t{7,5}\t{8,5}\t{9,5}\t{10,5}", this.Data, this.Key, sb.ToString(), x, y, V1.X, V1.Y, V2.X, V2.Y, Oblast.Width, Oblast.Height));
                return sb.ToString();
            }
            /// <summary>
            /// Vypočítá oblast obdélníku z potomků, kteří jsou obsaženi ve vrcholu.
            /// </summary>
            public void VypoctiObdelnik() {
                if (Potomci == null || Potomci.Count == 0) {
                    return;
                }
                float minX = Potomci[0].Oblast.X;
                float minY = Potomci[0].Oblast.Y;
                float maxX = Potomci[0].Oblast.X + Potomci[0].Oblast.Width;
                float maxY = Potomci[0].Oblast.Y + Potomci[0].Oblast.Height;
                foreach (RVrchol item in Potomci) {
                    if (item.Oblast.X < minX) { minX = item.Oblast.X; }
                    if (item.Oblast.Y < minY) { minY = item.Oblast.Y; }
                    if ((item.Oblast.X + item.Oblast.Width) > maxX) { maxX = item.Oblast.X + item.Oblast.Width; }
                    if ((item.Oblast.Y + item.Oblast.Height) > maxY) { maxY = item.Oblast.Y + item.Oblast.Height; }
                }
                Oblast = new RectangleF(minX, minY, maxX - minX, maxY - minY);
            }
            public RVrchol() : this(3) { }
            public RVrchol(int kapacitaUzlu) { Potomci = new List<RVrchol>(kapacitaUzlu); }
            public RVrchol(TKey key, PointF v1, PointF v2, TValue value) : this() { this.Key = key; this.V1 = v1; this.V2 = v2; this.Data = value; this.Oblast = vypoctiObdelnikThis(); }

            private RectangleF vypoctiObdelnikThis() {
                float minX = (V1.X < V2.X) ? V1.X : V2.X;
                float minY = (V1.Y < V2.Y) ? V1.Y : V2.Y;
                float maxX = (V1.X > V2.X) ? V1.X : V2.X;
                float maxY = (V1.Y > V2.Y) ? V1.Y : V2.Y;
                return new RectangleF(minX, minY, maxX - minX, maxY - minY);
            }
            public bool MaVolnySlot() {
                return (KapacitaUzlu - Potomci.Count > 0) ? true : false;
            }
            public bool JeKoren() { return (Rodic == null) ? true : false; }
            public bool JeList() { return (Potomci.Count == 0 && Data != null) ? true : false; }

            public bool JeBod(PointF value) {
                if ((V1.X == value.X && V1.Y == value.Y) || (V2.X == value.X && V2.Y == value.Y)) {
                    return true;
                }
                return false;
            }

            public int CompareTo(RVrchol other) {
                if (other == null) {
                    return 1;
                }
                if (this.Equals(other)) {
                    return 0;
                }

                string zOrderThis = this.GetZOrder();
                string zOrderOther = other.GetZOrder();

                if (zOrderOther == zOrderThis) {
                    return 0;
                }

                bool thisDelsi = (zOrderThis.Length > zOrderOther.Length) ? true : false;
                int kratsi = thisDelsi ? zOrderOther.Length : zOrderThis.Length;
                for (int i = 0; i < (thisDelsi ? zOrderThis.Length : zOrderOther.Length); i++) { // TODO: zkontrolovat podmínku
                    if (i < (thisDelsi ? zOrderThis.Length : zOrderOther.Length) - kratsi) {
                        if ((thisDelsi ? zOrderThis[i] : zOrderOther[i]) == '1') {
                            return thisDelsi ? 1 : -1;
                        }
                        continue;
                    }
                    if (zOrderThis[i] == zOrderOther[i]) {
                        continue;
                    }
                    if (zOrderThis[i] == '1') {
                        return 1;
                    }
                    if (zOrderOther[i] == '1') {
                        return -1;
                    }
                }
                return 0;
            }
        }

        public static int KapacitaUzlu { get; private set; }
        public RVrchol root;
        public List<RVrchol> poleListu = new List<RVrchol>();
        PriorityQueue<string, RVrchol, RVrchol> priorQueue = new PriorityQueue<string, RVrchol, RVrchol>();

        public RTree() : this(3) { }
        public RTree(int kapacitaUzlu) {

            if (kapacitaUzlu < 2) {
                throw new ArgumentOutOfRangeException("Parametr musí být nezáporné číslo vyšší, než 2");
            }
            KapacitaUzlu = kapacitaUzlu;
        }

        public void DebugStrom() {
            if (root == null) {
                Debug.WriteLine("Root = null");
                return;
            }
            Debug.WriteLine(string.Format("{0,-5}\t{1,-5}\t{2,-5}\t{3,5}\t{4,5}\t{5,5}\t{6,5}\t{7,5}\t{8,5}\t{9,5}\t{10,5}",
                    "Rodic",
                    "Data",
                    "Key",
                    "V1.X",
                    "V1.Y",
                    "V2.X",
                    "V2.Y",
                    "Width",
                    "Height",
                    "JeList",
                    "Level"));
            debugPostrom(root, 0);
        }
        private void debugPostrom(RVrchol vrchol, int level) {
            foreach (RVrchol item in vrchol.Potomci) {
                Debug.WriteLine(string.Format("{0,-5}\t{1,-5}\t{2,-5}\t{3,5}\t{4,5}\t{5,5}\t{6,5}\t{7,5}\t{8,5}\t{9,5}\t{10,5}",
                    item.Rodic.Data,
                    item.Data,
                    item.Key,
                    item.V1.X,
                    item.V1.Y,
                    item.V2.X,
                    item.V2.Y,
                    item.Oblast.Width,
                    item.Oblast.Height,
                    item.JeList(),
                    level));
                debugPostrom(item, level + 1);
            }
        }

        public void Vloz(TKey key, PointF v1, PointF v2, TValue value) { vloz(new RVrchol(key, v1, v2, value)); }

        private void vloz(RVrchol value) { // O(log n) pokud možno
            // FIXED: musím zajistit, aby se vkládal na správné místo do listu! Vyřešeno metodou PostavStrom a poleListu.Sort()
            poleListu.Add(value);
        }

        //[System.Obsolete("Nevyužito")]
        //private RVrchol najdiPredchudce(RVrchol predchudce, RVrchol value) {
        //    // dočasná berlička
        //    return poleListu.Last(); // TODO: vymazat časem, bude potřeba vůbec hledat předchůdce, nbeo stačí PriorQueue?

        //    // TODO najdiPredchudce dodelat
        //    if (root == null) {
        //        throw new Exception("Root null");
        //    }
        //    RVrchol novyPredchudce = null;
        //    foreach (RVrchol item in predchudce.Potomci) {
        //        if (item.JeList()) {
        //            // todo
        //            return novyPredchudce; // O(n) :(, půjde vyřešit přidáním ref do struktury nejspíš
        //        } else {
        //            if (item.Oblast.Contains(value.Oblast)) {
        //                return najdiPredchudce(item, value);
        //            } else {
        //                continue;
        //            }
        //        }
        //    }
        //    return null;
        //    //    return poleListu.Find(novyPredchudce); // O(n) :(, půjde vyřešit přidáním ref do struktury nejspíš
        //}

        public void PostavStrom() {
            foreach (RVrchol item in poleListu) {
                priorQueue.Add(new KeyValuePair<string, RVrchol>(item.GetZOrder(), item), item);
            }
            List<RVrchol> list = new List<RVrchol>(poleListu.Count);
            while (!priorQueue.IsEmpty) {
                list.Add(priorQueue.DequeueValue());
            }
            postavStrom(list);

            //poleListu.Sort();
            //postavStrom(poleListu);
        }

        private void postavStrom(IList<RVrchol> uroven) {
            if (uroven.Count <= KapacitaUzlu) {
                // pokud je v úrovni již tak málo prvků, že může vzniknout kořen, vytvoříme kořen a ukončíme stavbu.
                root = new RVrchol(KapacitaUzlu);
                foreach (RVrchol item in uroven) {
                    item.Rodic = root; // nastavíme zpětnou vazbu na rodiče
                    root.Potomci.Add(item);
                }
                root.VypoctiObdelnik();
                return;
            }

            int pocetVrcholu = uroven.Count / KapacitaUzlu;
            int pocetPretekajicich = uroven.Count % KapacitaUzlu;
            List<RVrchol> novaUroven = new List<RVrchol>();
            RVrchol novyVrchol = null;

            if (pocetPretekajicich == 0) {
                // nemusíme nic upravovat    
                for (int i = 0; i < uroven.Count; i++) {
                    if (i == 0 || !novyVrchol.MaVolnySlot()) { // po kapacitě uzlu vytvoříme nový vrchol
                        novyVrchol = new RVrchol(KapacitaUzlu); // vytvořím nový uzel
                        novaUroven.Add(novyVrchol); // přidání vrcholu do úrovně
                    }
                    uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                    novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                    novyVrchol.VypoctiObdelnik();
                }
            } else if (pocetPretekajicich >= minimumPrvkuKapacity()) {
                // pokud je prvků více než polovina kapacity, stačí přidat jeden prvek do vrstvy
                pocetVrcholu++;
                for (int i = 0; i < uroven.Count; i++) {
                    if (i == 0 || !novyVrchol.MaVolnySlot()) { // po kapacitě uzlu vytvoříme nový vrchol
                        novyVrchol = new RVrchol(KapacitaUzlu); // vytvořím nový uzel
                        novaUroven.Add(novyVrchol); // přidání vrcholu do úrovně
                    }
                    uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                    novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                    novyVrchol.VypoctiObdelnik();
                }
            } else {
                // musíme přidat jeden prvek a předchozí rozhodit na více prvků
                pocetVrcholu++;
                for (int i = 0; i < uroven.Count; i++) {
                    if (i >= (pocetVrcholu - 2) * KapacitaUzlu + minimumPrvkuKapacity()) {
                        if (pocetVrcholu == novaUroven.Count + 1) {
                            novyVrchol = new RVrchol(KapacitaUzlu);
                            novaUroven.Add(novyVrchol); // přidání vrcholu do úrovně
                        }
                        uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                        novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                        novyVrchol.VypoctiObdelnik();
                        continue;
                    }
                    if (i == 0 || !novyVrchol.MaVolnySlot()) {
                        // po kapacitě uzlu vytvoříme nový vrchol
                        novyVrchol = new RVrchol(KapacitaUzlu); // vytvořím nový uzel
                        novaUroven.Add(novyVrchol); // přidání vrcholu do úrovně
                    }
                    uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                    novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                    novyVrchol.VypoctiObdelnik();
                }
            }
            if (pocetVrcholu != novaUroven.Count) {
                throw new Exception("Něco je špatně, nerovnají se vypočítané vrcholy s počty vrcholů!");
            }
            postavStrom(novaUroven);
        }

        private int minimumPrvkuKapacity() {
            return ((KapacitaUzlu % 2 == 0) ? KapacitaUzlu / 2 : (KapacitaUzlu / 2) + 1);
        }

        //[System.Obsolete("Nevyužito")]
        //private void vlozDoVrcholu(RVrchol vrchol, RVrchol value) {
        //    if (vrchol.MaVolnySlot()) {
        //        vrchol.Potomci.Add(value);
        //    } else {
        //        RVrchol rv = new RVrchol(KapacitaUzlu);
        //        //value.Potomci = vrchol;
        //        rv.Potomci.Add(value);
        //        rv.Rodic = vrchol.Rodic;
        //        vrchol.Rodic = rv;
        //        if (vrchol.JeKoren()) {
        //            root = rv;
        //        }
        //        vlozDoVrcholu(rv, value);
        //    }
        //}

        public TValue Odeber(TKey key) { // O(log n)
            throw new NotImplementedException();
        }

        public List<TValue> VyhledejBodove(PointF value) {
            List<TValue> list = new List<TValue>();
            foreach (RVrchol item in vyhledejBodove(root, value)) {
                list.Add(item.Data);
            }
            return list;
        }

        private List<RVrchol> vyhledejBodove(RVrchol uroven, PointF value) { // O(log n)
            if (root == null) {
                throw new NullReferenceException("Root neexistuje");
            }
            List<RVrchol> list = new List<RVrchol>();

            foreach (RVrchol item in uroven.Potomci) {
                if (item.Oblast.Contains(value)) {
                    if (item.JeList()) {
                        if (item.JeBod(value)) {
                            // FIXED: fixnout vkládání celých souřadnic a nejen obdélníku, jelikož nedokážu určit, zda-li leží ten bod přímo v obdélníku, nebo je to prázdný bod... Mělo by být OK snad již :)
                            list.Add(item);
                        }
                    }
                    list.AddRange(vyhledejBodove(item, value));
                }
            }
            return list;
        }
        public List<TValue> VyhledejIntervalove(RectangleF value) {
            // TODO: prohledání a porovnání s přímkou. http://www.multilingualarchive.com/ma/enwiki/en/Cohen-Sutherland
            List<TValue> list = new List<TValue>();
            foreach (RVrchol item in vyhledejIntervalove(root, value)) {
                list.Add(item.Data);
            }
            return list;
        }
        public List<TValue> VyhledejIntervalove(PointF point1, PointF point2) {
            return VyhledejIntervalove(new RectangleF(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y), Math.Max(point1.X, point2.X) - Math.Min(point1.X, point2.X), Math.Max(point1.Y, point2.Y) - Math.Min(point1.Y, point2.Y)));
        }
        private List<RVrchol> vyhledejIntervalove(RVrchol uroven, RectangleF value) { // O(log n)
            if (root == null) {
                throw new NullReferenceException("Root neexistuje");
            }
            List<RVrchol> list = new List<RVrchol>();

            foreach (RVrchol item in uroven.Potomci) {
                if (value.IntersectsWith(item.Oblast)) {
                    if (item.JeList()) {
                        if (leziVOblasti(value, item.V1, item.V2)) {
                            list.Add(item);
                        }
                    }
                    list.AddRange(vyhledejIntervalove(item, value));
                }
            }
            return list;
        }
        public List<RVrchol> DejStrukturu() {
            return poleListu;
        }
        /// <summary>
        /// Liang-Barsky algoritmus: http://www.zdrojovykod.cz/?p=105 pro výpočet, zda-li přímka leží v oblasti.
        /// </summary>
        /// <param name="hranice"></param>
        /// <param name="pocatek"></param>
        /// <param name="konec"></param>
        /// <returns></returns>
        private bool leziVOblasti(RectangleF hranice, PointF pocatek, PointF konec) {

            float dx = konec.X - pocatek.X;
            float dy = konec.Y - pocatek.Y;


            float p1 = -dx;
            float p2 = dx;
            float p3 = -dy;
            float p4 = dy;

            float q1 = pocatek.X - hranice.X;
            float q2 = hranice.X + hranice.Width - pocatek.X;
            float q3 = pocatek.Y - hranice.Y;
            float q4 = hranice.Y + hranice.Height - pocatek.Y;

            float[] p = { p1, p2, p3, p4 };
            float[] q = { q1, q2, q3, q4 };

            float u1 = 0, u2 = 1, r = 0;

            for (int i = 0; i < 4; i++) {

                if ((p[i] == 0) && (q[i] < 0)) {
                    return false; //vynechame
                }

                if (p[i] != 0) {
                    r = q[i] / p[i];
                    if (p[i] < 0) {
                        u1 = Math.Max(u1, r);
                    } else if (p[i] > 0) { //p[i] > 0
                        u2 = Math.Min(u2, r);
                    }
                }
            }

            //x_orezane1 = (int)Math.round(x1 + u1 * dx);
            //y_orezane1 = (int)Math.round(y1 + u1 * dy);

            //x_orezane2 = (int)Math.round(x1 + u2 * dx);
            //y_orezane2 = (int)Math.round(y1 + u2 * dy);

            if (u1 < u2) {
                return true;
            } else {
                return false;
            }
        }
    }
}

