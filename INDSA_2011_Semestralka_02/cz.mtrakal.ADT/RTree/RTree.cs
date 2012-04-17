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

namespace cz.mtrakal.ADT {
    public class RTree<TKey, TValue> where TKey : IComparable<TKey> {
        /// <summary>
        /// Vnitřní datová složka, kam se ukládají všehcny vrcholy i listy.
        /// </summary>
        class RVrchol {
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
            /// Předchůdce (vyšší vrchol, slouží k traverzování mezi vrcholy). Pokud se jedná o kořen, je Rodic == null.
            /// </summary>
            public RVrchol Rodic { get; set; }
            /// <summary>
            /// Pole potomků z nižších vrstev, pokud se jedná o poslední vrstvu, kde jsou již listy, mají nastavenou datovou složku Data.
            /// </summary>
            public List<RVrchol> Potomci { get; set; }
            public int GetZOrder() {
                return Convert.ToInt32(((Oblast.X + Oblast.Width / 2) * (Oblast.Y + Oblast.Height / 2)));
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
                    if (item.Oblast.X + item.Oblast.Width > maxX) { maxX = item.Oblast.X + item.Oblast.Width; }
                    if (item.Oblast.Y + item.Oblast.Height > maxY) { maxY = item.Oblast.Y + item.Oblast.Height; }
                }
                Oblast = new RectangleF(minX, minY, maxX - minX, maxY - minY);
            }
            public RVrchol() : this(3) { }
            public RVrchol(int kapacitaUzlu) { Potomci = new List<RVrchol>(kapacitaUzlu); }
            public RVrchol(TKey key, RectangleF oblast, TValue value) : this() { this.Key = key; this.Oblast = oblast; this.Data = value; }
            public bool maVolnySlot() {
                return (KapacitaUzlu - Potomci.Count > 0) ? true : false;
            }
            public bool JeKoren() { return (Rodic == null) ? true : false; }
            public bool JeList() { return (Potomci.Count == 0 && Data != null) ? true : false; }
        }

        public static int KapacitaUzlu { get; private set; }
        RVrchol root;
        List<RVrchol> poleListu = new List<RVrchol>();
        PriorityQueue<int, RVrchol, RVrchol> priorQueue = new PriorityQueue<int, RVrchol, RVrchol>();

        public RTree() : this(3) { }
        public RTree(int kapacitaUzlu) {

            if (kapacitaUzlu < 2) {
                throw new ArgumentOutOfRangeException("Parametr musí být nezáporné číslo vyšší, než 2");
            }
            KapacitaUzlu = kapacitaUzlu;
        }
        //private void pocet(bool inkrement) { if (inkrement) { Count++; } else { Count--; } }

        public void Vloz(TKey key, RectangleF oblast, TValue value) { vloz(new RVrchol(key, oblast, value)); }

        private void vloz(RVrchol value) { // O(log n) pokud možno
            // TODO: musím zajistit, aby se vkládal na správné místo do listu!!!
            //priorQueue.Add(new KeyValuePair<int, RVrchol>(value.GetZOrder(), value), value);
            poleListu.Add(value);
            //poleListu.Add(priorQueue.DequeueValue());
            //if (root == null) {
            //    poleListu.Add(value);
            //    root = value;
            //} else {
            //    //poleListu.AddAfter(najdiPredchudce(root, value), value);
            //    poleListu.Add(value);
            //}

            //postavStrom(poleListu);
        }

        private RVrchol najdiPredchudce(RVrchol predchudce, RVrchol value) {
            // dočasná berlička
            return poleListu.Last();

            // TODO najdiPredchudce dodelat
            if (root == null) {
                throw new Exception("Root null");
            }
            RVrchol novyPredchudce = null;
            foreach (RVrchol item in predchudce.Potomci) {
                if (item.JeList()) {
                    // todo
                    return novyPredchudce; // O(n) :(, půjde vyřešit přidáním ref do struktury nejspíš
                } else {
                    if (item.Oblast.Contains(value.Oblast)) {
                        return najdiPredchudce(item, value);
                    } else {
                        continue;
                    }
                }
            }
            return null;
            //    return poleListu.Find(novyPredchudce); // O(n) :(, půjde vyřešit přidáním ref do struktury nejspíš
        }

        public void postavStrom() {
            foreach (RVrchol item in poleListu) {
                priorQueue.Add(new KeyValuePair<int, RVrchol>(item.GetZOrder(), item), item);
            }
            List<RVrchol> list = new List<RVrchol>(poleListu.Count);
            while (!priorQueue.IsEmpty) {
                list.Add(priorQueue.DequeueValue());
            }
            //postavStrom(poleListu);
            postavStrom(list);
        }

        private void postavStrom(IList<RVrchol> uroven) {

            //if (root == null) {
            //    root = new RVrchol(KapacitaUzlu);
            //    root.Potomci.Add(value);
            //    return;
            //}
            // vlozDoVrcholu(root, value);

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

            if (pocetPretekajicich == 0) { // OK
                // nemusíme nic upravovat    
                for (int i = 0; i < uroven.Count; i++) {
                    if (i == 0 || !novyVrchol.maVolnySlot()) { // po kapacitě uzlu vytvoříme nový vrchol
                        novyVrchol = new RVrchol(KapacitaUzlu); // vytvořím nový uzel
                        novaUroven.Add(novyVrchol); // přidání vrcholu do úrovně
                    }
                    // TODO: zde řešit Z order ještě, aby vznikla oblast novýho obdélníku! + klíč
                    uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                    novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                    novyVrchol.VypoctiObdelnik();
                }
            } else if (pocetPretekajicich >= minimumPrvkuKapacity()) { // TODO
                // pokud je prvků více než polovina kapacity, stačí přidat jeden prvek do vrstvy
                pocetVrcholu++;
                for (int i = 0; i < uroven.Count; i++) {
                    if (i == 0 || !novyVrchol.maVolnySlot()) { // po kapacitě uzlu vytvoříme nový vrchol
                        novyVrchol = new RVrchol(KapacitaUzlu); // vytvořím nový uzel
                        novaUroven.Add(novyVrchol); // přidání vrcholu do úrovně
                    }
                    // TODO: zde řešit Z order ještě, aby vznikla oblast novýho obdélníku! + klíč
                    uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                    novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                    novyVrchol.VypoctiObdelnik();
                }
            } else { // OK
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
                    if (i == 0 || !novyVrchol.maVolnySlot()) { // po kapacitě uzlu vytvoříme nový vrchol
                        novyVrchol = new RVrchol(KapacitaUzlu); // vytvořím nový uzel
                        novaUroven.Add(novyVrchol); // přidání vrcholu do úrovně
                    }
                    // TODO: zde řešit Z order ještě, aby vznikla oblast novýho obdélníku! + klíč
                    uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                    novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                    novyVrchol.VypoctiObdelnik();
                }
            }

            // pro všehcny kontrola zda-li se něco nepokáklo
            if (pocetVrcholu != novaUroven.Count) {
                throw new Exception("Něco je špatně, nerovnají se vypočítané vrcholy s počty vrcholů!");
            }
            postavStrom(novaUroven);
        }

        private int minimumPrvkuKapacity() {
            return ((KapacitaUzlu % 2 == 0) ? KapacitaUzlu / 2 : (KapacitaUzlu / 2) + 1);
        }

        private void vlozDoVrcholu(RVrchol vrchol, RVrchol value) {
            if (vrchol.maVolnySlot()) {
                vrchol.Potomci.Add(value);
            } else {
                RVrchol rv = new RVrchol(KapacitaUzlu);
                //value.Potomci = vrchol;
                rv.Potomci.Add(value);
                rv.Rodic = vrchol.Rodic;
                vrchol.Rodic = rv;
                if (vrchol.JeKoren()) {
                    root = rv;
                }
                vlozDoVrcholu(rv, value);
            }
        }

        public TValue Odeber(TKey key) { // O(log n)
            //pocet(false);
            throw new NotImplementedException();
            //return default(TValue);
        }
        public TValue VyhledejBodove(TKey key) { // O(log n)
            throw new NotImplementedException();
            return default(TValue);
        }
        public TValue VyhledejIntervalove(TKey key, TKey key2) { // O(log n)
            throw new NotImplementedException();
            return default(TValue);
        }
    }
}
