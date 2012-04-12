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
        class RTreeData {
            public TValue Data { get; set; }
            public TKey Key { get; set; }
            public Rectangle Oblast { get; set; }
            public RVrchol Potomek { get; set; }
            public RTreeData(TKey key, Rectangle oblast, TValue value) { this.Key = key; this.Oblast = oblast; this.Data = value; }
            public bool JeList() { return (Potomek == null && Data != null) ? true : false; }
        }
        class RVrchol {
            public RVrchol Rodic { get; set; }
            public List<RTreeData> DataList { get; set; }
            public RVrchol() : this(3) { }
            public RVrchol(int kapacitaUzlu) { DataList = new List<RTreeData>(kapacitaUzlu); }
            public bool JeKoren() { return (Rodic == null) ? true : false; }
        }

        public int KapacitaUzlu { get; private set; }
        RVrchol root;
        List<RTreeData> poleObjektu = new List<RTreeData>();

        public RTree() : this(3) { }
        public RTree(int kapacitaUzlu) {
            ;
            if (kapacitaUzlu < 2) {
                throw new ArgumentOutOfRangeException("Parametr musí být nezáporné číslo vyšší, než 2");
            }
            KapacitaUzlu = kapacitaUzlu;
            root = new RVrchol();
        }
        //private void pocet(bool inkrement) { if (inkrement) { Count++; } else { Count--; } }

        public void Vloz(TKey key, Rectangle oblast, TValue value) { vloz(new RTreeData(key, oblast, value)); }

        private void vloz(RTreeData value) { // O(log n)
            poleObjektu.Add(value);
            postavStrom(value);
            //pocet(true);

        }

        private void postavStrom(RTreeData value) {
            //if (poleObjektu.Count == 0) {
            //    root = null;
            //    return;
            //}
            if (root == null) {
                root = new RVrchol(KapacitaUzlu);
                root.DataList.Add(value);
                return;
            }
            vlozDoVrcholu(root, value);
        }

        private void vlozDoVrcholu(RVrchol vrchol, RTreeData value) {
            if (maVolnySlot(vrchol)) {
                vrchol.DataList.Add(value);
            } else {
                RVrchol rv = new RVrchol(KapacitaUzlu);
                value.Potomek = vrchol;
                rv.DataList.Add(value);
                rv.Rodic = vrchol.Rodic;
                vrchol.Rodic = rv;
                if (vrchol.JeKoren()) {
                    root = rv;
                }
                vlozDoVrcholu(rv, value);
            }
        }

        private bool maVolnySlot(RVrchol value) {
            return (KapacitaUzlu - value.DataList.Count > 0) ? true : false;
        }

        public TValue Odeber(TKey key) { // O(log n)
            //pocet(false);
            return default(TValue);
        }
        public TValue VyhledejBodove(TKey key) { // O(log n)
            return default(TValue);
        }
        public TValue VyhledejIntervalove(TKey key, TKey key2) { // O(log n)

            return default(TValue);
        }
    }
}
