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
/* R-strom v externí paměti

        neutříděné (z hlediska obsahu)

        soubory s přímým přístupem
        blokově orientované (mají organizační části, strukturované ze záznamů, má být realizováno pomocí blokového přenosu (celou strukturu tedy)
        nejhorší je přenos dat mezi interní a externí pamětí (samotná operace)

        soubor s přímým přístupem a s blokovým přístupem je jedna z impementací tabulek (každý záznam reprezentován klíčem v souboru nejspíš indexem, nebo NAJDI prvek s daným klíčem). Můžeme nahlížet jako specifické blokové pole.

        ArrayList je v podstatě něco podobného.

        operace: čti, zapiš, zruš, modifikuj

        Jak pracovat s blokově orientovaným souborem:
          Blok0 je speciální a obsahuje řídicí informace.
  
          Musím z hlavičky rozpoznat, že je soubor zcela plný a alokovat nový blok (por vkládání záznamů)
          pokud odeberu (vyprázdním celý blok), nemáme volné bloky? Jejich evidence abychom do nich mohli zase něco ukládat.
  
        implementace velmi volná, jak se mi to bude hodit.

        řídicí blok buď na začátku, nebo na konci (přepsat daty a posunout do nově alkované paměti)

        nefixované záznamy: primárně učiním přístup pouze k bloku, kde by se měl nacházet > načtu do bufferu a až v něm hledám příslušný záznam
        fixované záznamy: mám k dispozici o záznamu dvousložkovou informaci (adresu a cosi) a mohu přistupovat přímo k dat§m bez bufferu.

        heap file / heap

        Sekvenční průchod (hledání v každém prvku jen nějaké informace) - průchod postupně celou pamětí

        B+ strom (nad ním je i R-strom)

        Přístup s úplným (hustým) indexem:
        2 soubory: v jednoms turktura v druhém listy, do bufferu natáhnu root, najdu potomka co heldám postupně, opět natáhnu blok a projdu až se dostanu k listům, kde je blok na soubor s listy a najdu blok, kde bude (offset, nebo pořadí, blok, ...) a na něj sáhnu pomocí přímého přístupu.


        Musí být blokový přenos v obou směrech!


        Binární vyhledávání, v utříděném souboru, půlení intervalů (modifikace pro půlení bloku, ne nalezení koncového záznamu) O(log2n)
         *
        Ten strom přepíšeš tak, že když načítáš potomky (tzn. Uzel.getPotomci nebo tak něco) tak je načteš ze souboru. Místo přímo těch potomků tak budeš v prvku držet blok bloku na kterém se nachází ty potomci. Ty budou mít zase blok na další potomky atd. až budeš mít blok na listy, které ale budou v jiném souboru (takže si krom indexu držím i info jestli míří do indexového nebo bázového souboru). V bázovém souboru pak nebudeš ukádat další indexy ale přímo obsah toho listu (já si tam uložil id té cesty vždy). Jo a ještě krom id potomka/obsahu listu si k tomu ulož i souřadnice toho jejich čtverce pro porovnávání
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace cz.mtrakal.ADT {
    public class RTreeIO {
        /// <summary>
        /// Vnitřní datová složka, kam se ukládají všechny vrcholy i listy.
        /// </summary>
        public class RVrchol : IComparable<RVrchol> {
            public class OblastDat {
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
                public RectangleF VypoctiObdelnikThis() {
                    float minX = (V1.X < V2.X) ? V1.X : V2.X;
                    float minY = (V1.Y < V2.Y) ? V1.Y : V2.Y;
                    float maxX = (V1.X > V2.X) ? V1.X : V2.X;
                    float maxY = (V1.Y > V2.Y) ? V1.Y : V2.Y;
                    //Oblast = new RectangleF(minX, minY, maxX - minX, maxY - minY);
                    return new RectangleF(minX, minY, maxX - minX, maxY - minY);
                }
                public bool JeBod(PointF value) {
                    if ((V1.X == value.X && V1.Y == value.Y) || (V2.X == value.X && V2.Y == value.Y)) {
                        return true;
                    }
                    return false;
                }
                public override string ToString() {
                    return V1.X + ":" + V1.Y + " až " + V2.X + ":" + V2.Y;
                }
            }
            public const int maxdelkaretezce = 15;
            private string data;
            /// <summary>
            /// Datová složka, kam se ukládají objekty vstupních dat. V této datové složce budou hodnoty pouze u poslední vrstvy listů.
            /// </summary>
            public string Data { get { return data; } set { this.data = (value.Length > 15) ? value.Trim().Substring(0, maxdelkaretezce) : value; } }
            /// <summary>
            /// Předchůdce (vyšší vrchol, slouží k traverzování mezi vrcholy). Pokud se jedná o kořen, je Rodic == null.
            /// </summary>
            public RVrchol Rodic { get; set; }
            /// <summary>
            /// Pole potomků z nižších vrstev, pokud se jedná o poslední vrstvu, kde jsou již listy, mají nastavenou datovou složku Data.
            /// </summary>
            public List<RVrchol> Potomci { get; set; }
            public OblastDat Oblast = new OblastDat();
            public string GetZOrder() {
                StringBuilder sb = new StringBuilder();

                string x = Convert.ToString(Convert.ToInt32(Math.Floor(Oblast.Oblast.X + (Oblast.Oblast.Width / 2))), 2);
                string y = Convert.ToString(Convert.ToInt32(Math.Floor(Oblast.Oblast.Y + (Oblast.Oblast.Height / 2))), 2);
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
                float minX = Potomci[0].Oblast.Oblast.X;
                float minY = Potomci[0].Oblast.Oblast.Y;
                float maxX = Potomci[0].Oblast.Oblast.X + Potomci[0].Oblast.Oblast.Width;
                float maxY = Potomci[0].Oblast.Oblast.Y + Potomci[0].Oblast.Oblast.Height;
                foreach (RVrchol item in Potomci) {
                    if (item.Oblast.Oblast.X < minX) { minX = item.Oblast.Oblast.X; }
                    if (item.Oblast.Oblast.Y < minY) { minY = item.Oblast.Oblast.Y; }
                    if ((item.Oblast.Oblast.X + item.Oblast.Oblast.Width) > maxX) { maxX = item.Oblast.Oblast.X + item.Oblast.Oblast.Width; }
                    if ((item.Oblast.Oblast.Y + item.Oblast.Oblast.Height) > maxY) { maxY = item.Oblast.Oblast.Y + item.Oblast.Oblast.Height; }
                }
                Oblast.V1 = new PointF(minX, minY);
                Oblast.V2 = new PointF(maxX, maxY);
                Oblast.Oblast = new RectangleF(minX, minY, maxX - minX, maxY - minY);
            }
            public RVrchol() : this(3) { }
            public RVrchol(int kapacitaUzlu) { Potomci = new List<RVrchol>(kapacitaUzlu); }
            public RVrchol(PointF v1, PointF v2, string value) : this() { this.Oblast.V1 = v1; this.Oblast.V2 = v2; this.Data = value; this.Oblast.Oblast = Oblast.VypoctiObdelnikThis(); }

            public bool MaVolnySlot() { return (KapacitaUzlu - Potomci.Count > 0) ? true : false; }
            public bool JeKoren() { return (Rodic == null) ? true : false; }
            public bool JeList() { return (Potomci.Count == 0 && Data != null) ? true : false; }

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

            public void Nacti(FileStream fs) {
                BinaryReader br = new BinaryReader(fs, Encoding.UTF8);
                Data = br.ReadString();
                Oblast.V1 = new PointF(br.ReadSingle(), br.ReadSingle());
                Oblast.V2 = new PointF(br.ReadSingle(), br.ReadSingle());
            }

            internal byte[] GetByteArrayBase() {
                List<byte> byteArray = new List<byte>();
                byteArray.AddRange(Encoding.UTF8.GetBytes(Data.Trim().PadLeft(maxdelkaretezce)));
                byteArray.AddRange(BitConverter.GetBytes(Oblast.V1.X));
                byteArray.AddRange(BitConverter.GetBytes(Oblast.V1.Y));
                byteArray.AddRange(BitConverter.GetBytes(Oblast.V2.X));
                byteArray.AddRange(BitConverter.GetBytes(Oblast.V2.Y));
                return byteArray.ToArray();
            }
            internal byte[] GetByteArrayR() {
                List<byte> byteArray = new List<byte>();
                byteArray.AddRange(BitConverter.GetBytes(Oblast.V1.X));
                byteArray.AddRange(BitConverter.GetBytes(Oblast.V1.Y));
                byteArray.AddRange(BitConverter.GetBytes(Oblast.V2.X));
                byteArray.AddRange(BitConverter.GetBytes(Oblast.V2.Y));
                // pokud je Index -1 jedná se o R-list, pokud je index číslo, odkazuje blok i index do bázového souboru.
                byteArray.AddRange(BitConverter.GetBytes(Blok));
                byteArray.AddRange(BitConverter.GetBytes(Index));
                return byteArray.ToArray();
            }
            public override string ToString() {
                return Data + ", " + Oblast.ToString() + ", " + Blok + "-" + Index;
            }

            public int Blok { get; set; }
            public int Index { get; set; }
        }
        public static int KapacitaUzlu { get; private set; }
        public RVrchol root;
        PriorityQueue<string, RVrchol, RVrchol> priorQueue = new PriorityQueue<string, RVrchol, RVrchol>();
        public RTreeIO() : this(3) { }
        public RTreeIO(int kapacitaUzlu) {
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
                    item.Oblast.V1.X,
                    item.Oblast.V1.Y,
                    item.Oblast.V2.X,
                    item.Oblast.V2.Y,
                    item.Oblast.Oblast.Width,
                    item.Oblast.Oblast.Height,
                    item.JeList(),
                    level));
                debugPostrom(item, level + 1);
            }
        }

        bool postaven = false;

        public void PostavStrom() {
            if (!postaven) {
                postaven = true;
                foreach (RVrchol item in BazovyEnumerator()) {
                    priorQueue.Add(new KeyValuePair<string, RVrchol>(item.GetZOrder(), item), item);
                }
                List<RVrchol> list = new List<RVrchol>(priorQueue.Count);
                while (!priorQueue.IsEmpty) {
                    list.Add(priorQueue.DequeueValue());
                }
                vytvorR(fsR, kapacitaBlokuR);
                postavStrom(list, true);
            }
        }

        private int minimumPrvkuKapacity() {
            return ((KapacitaUzlu % 2 == 0) ? KapacitaUzlu / 2 : (KapacitaUzlu / 2) + 1);
        }
        public List<string> VyhledejBodove(PointF value) {
            List<string> list = new List<string>();

            BinaryReader br = new BinaryReader(fsR, Encoding.UTF8);
            br.ReadInt32();
            br.ReadInt32();
            int koren = br.ReadInt32();

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
                if (item.Oblast.Oblast.Contains(value)) {
                    if (item.JeList()) {
                        if (item.Oblast.JeBod(value)) {
                            // FIXED: fixnout vkládání celých souřadnic a nejen obdélníku, jelikož nedokážu určit, zda-li leží ten bod přímo v obdélníku, nebo je to prázdný bod... Mělo by být OK snad již :)
                            list.Add(item);
                        }
                    }
                    list.AddRange(vyhledejBodove(item, value));
                }
            }
            return list;
        }
        public List<string> VyhledejIntervalove(RectangleF value) {
            // prohledání a porovnání s přímkou. http://www.multilingualarchive.com/ma/enwiki/en/Cohen-Sutherland
            List<string> list = new List<string>();
            foreach (RVrchol item in vyhledejIntervalove(root, value)) {
                list.Add(item.Data);
            }
            return list;
        }
        public List<string> VyhledejIntervalove(PointF point1, PointF point2) {
            return VyhledejIntervalove(new RectangleF(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y), Math.Max(point1.X, point2.X) - Math.Min(point1.X, point2.X), Math.Max(point1.Y, point2.Y) - Math.Min(point1.Y, point2.Y)));
        }
        private List<RVrchol> vyhledejIntervalove(RVrchol uroven, RectangleF value) { // O(log n)
            if (root == null) {
                throw new NullReferenceException("Root neexistuje");
            }
            List<RVrchol> list = new List<RVrchol>();

            foreach (RVrchol item in uroven.Potomci) {
                if (value.IntersectsWith(item.Oblast.Oblast)) {
                    if (item.JeList()) {
                        if (leziVOblasti(value, item.Oblast.V1, item.Oblast.V2)) {
                            list.Add(item);
                        }
                    }
                    list.AddRange(vyhledejIntervalove(item, value));
                }
            }
            return list;
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

            if (u1 < u2) {
                return true;
            } else {
                return false;
            }
        }

        #region Bázový blok
        FileStream fsBase = new FileStream("fsBazovy.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
        static int pocetBytuZaznamu = RVrchol.maxdelkaretezce + sizeof(Single) * 4;
        static int kapacitaBloku = 3;
        int indexBlokuBaze = -1;
        int pocetZaznamuVMS = 0;
        MemoryStream ms = new MemoryStream(pocetBytuZaznamu * kapacitaBloku);

        public delegate void BufferJePlnyHandler();
        public event BufferJePlnyHandler BufferJePlny;

        public void ZapisDoBufferu(MemoryStream ms, byte[] data) {
            if (pocetZaznamuVMS < kapacitaBloku) {
                ms.Write(data, 0, pocetBytuZaznamu);
                pocetZaznamuVMS++;
            }
            if (pocetZaznamuVMS == kapacitaBloku) {
                if (ms.Length != kapacitaBloku * pocetBytuZaznamu) {
                    throw new OutOfMemoryException("Chybná velikost dat!");
                }
                if (BufferJePlny != null) {
                    BufferJePlny(); //zavolá událost
                    vymazBuffer(ms);
                }
            }
        }
        private void vymazBuffer(MemoryStream ms) {
            pocetZaznamuVMS = 0;
            ms.Seek(0, 0);
            ms.SetLength(0);
        }
        private void zapisDataBazova(MemoryStream ms, FileStream fs) {
            ms.WriteTo(fs);
            fs.Flush();
        }
        public void NaplnBazovySoubor(PointF v1, PointF v2, string value) { naplnBazovySoubor(new RVrchol(v1, v2, value)); }
        private void naplnBazovySoubor(RVrchol vrchol) {
            BufferJePlny += new BufferJePlnyHandler(zapisBazovaDataHandler);

            if (fsBase.Length == 0) {
                vytvorBazovy(fsBase, kapacitaBloku);
            }
            ZapisDoBufferu(this.ms, vrchol.GetByteArrayBase());

            BufferJePlny -= new BufferJePlnyHandler(zapisBazovaDataHandler);
        }
        public void UlozBazovySoubor() {
            if (pocetZaznamuVMS != 0) { // pokud není celý počet dat do bloku na konci, zapíše data
                ms.SetLength(pocetBytuZaznamu * kapacitaBloku); // nastaví velikost na celý blok
                zapisDataBazova(ms, fsBase);
                vymazBuffer(ms);
            }
        }
        //public void NaplnBazovySoubor() {
        //    BufferJePlny += new BufferJePlnyHandler(zapisBazovaDataHandler);


        //    foreach (RVrchol item in poleListu) {
        //        ZapisDoBufferu(this.ms, item.GetByteArrayBase());
        //    }
        //    if (pocetZaznamuVMS != 0) { // pokud není celý počet dat do bloku na konci, zapíše data
        //        ms.SetLength(pocetBytuZaznamu * kapacitaBloku); // nastaví velikost na celý blok
        //        zapisDataBazova(ms, fsBase);
        //        vymazBuffer(ms);
        //    }
        //    BufferJePlny -= new BufferJePlnyHandler(zapisBazovaDataHandler);
        //}
        void zapisBazovaDataHandler() {
            zapisDataBazova(ms, fsBase);
        }
        public void VymazBazovy() {
            fsBase.Seek(0, 0);
            if (fsBase.Length > 0) {
                fsBase.SetLength(0);
                //throw new NotImplementedException("Bázový soubor není prázdný");
            }
        }
        private void vytvorBazovy(FileStream fs, int kapacitaBloku) {
            BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8);
            bw.BaseStream.Seek(0, 0);

            //VymazBazovy();

            //hlavička
            bw.Write(kapacitaBloku); // počet záznamů na blok
            bw.Write(pocetBytuZaznamu); //velikost jednoho záznamu v bloku
            bw.Flush();
        }
        public RVrchol NactiZBazovehoSouboru(int indexBloku, int indexZaznamu) {
            if (indexBlokuBaze != indexBloku) { // pokud již je blok v paměti, nepřistupuj do souboru
                BinaryReader br = new BinaryReader(fsBase, Encoding.UTF8);
                if (br.BaseStream.Length == 0) {
                    throw new IOException("Prázdný soubor");
                }
                br.BaseStream.Seek(0, 0);
                int kapacitaBloku = br.ReadInt32();
                int velikostZaznamu = br.ReadInt32();
                if (velikostZaznamu != pocetBytuZaznamu) {
                    Debug.Write("Rozdílné velikost záznamů, upravuji.");
                    pocetBytuZaznamu = velikostZaznamu;
                }
                long velikostSouboru = br.BaseStream.Length;
                long pocetPretecenych = (velikostSouboru - sizeof(int) * 2) - (((velikostSouboru - sizeof(int) * 2) / (pocetBytuZaznamu * kapacitaBloku)) * kapacitaBloku * pocetBytuZaznamu);
                if ((pocetPretecenych % pocetBytuZaznamu != 0) && (pocetPretecenych / pocetBytuZaznamu > kapacitaBloku)) {
                    throw new Exception("Data nemají správný formát!");
                }
                if (pocetZaznamuVMS != 0) {
                    vymazBuffer(ms);
                }
                byte[] byteArray = new byte[pocetBytuZaznamu * kapacitaBloku];
                //byte[] zaznam = new byte[pocetBytuZaznamu];

                // sizeof(int) * 2 + indexBloku
                if (indexBloku * pocetBytuZaznamu * kapacitaBloku > br.BaseStream.Length) {
                    return null;
                    //throw new ArgumentOutOfRangeException("Index ukazuje za konec souboru blok bloku: " + indexBloku + "!");
                }
                br.BaseStream.Position = sizeof(int) * 2 + indexBloku * pocetBytuZaznamu * kapacitaBloku;
                if (br.BaseStream.Length - br.BaseStream.Position > pocetBytuZaznamu * kapacitaBloku) {
                    // pro prvky s obsazeným celým blokem
                    br.BaseStream.Read(byteArray, 0, pocetBytuZaznamu * kapacitaBloku);
                    ms.Write(byteArray, 0, pocetBytuZaznamu * kapacitaBloku);
                    pocetZaznamuVMS = 3;
                } else {
                    // Pokud na konci souboru není obsazen celý blok.
                    // Není aktivní, jelikož blok na konci je doplněm na celý blok při zápisu.
                    br.BaseStream.Read(byteArray, 0, (int)(br.BaseStream.Length - br.BaseStream.Position));
                    ms.Write(byteArray, 0, pocetBytuZaznamu * kapacitaBloku);
                    pocetZaznamuVMS = (int)(br.BaseStream.Length - indexBloku * pocetBytuZaznamu * kapacitaBloku) / pocetBytuZaznamu;
                }

                //for (int i = 0; i < kapacitaBloku; i++) {
                //    for (int j = 0; j < pocetBytuZaznamu; j++) {
                //        zaznam[j] = byteArray[i * pocetBytuZaznamu + j];
                //    }
                //    ZapisDoBufferu(ms, zaznam);
                //}
                indexBlokuBaze = indexBloku;
            }
            return nactiVrcholZBufferu(indexZaznamu);
        }
        private RVrchol nactiVrcholZBufferu(int index) {
            if (index >= ms.Length / pocetBytuZaznamu) {
                throw new ArgumentOutOfRangeException("Index záznamu se v bufferu nenalézá!");
            }
            byte[] buffer = new byte[pocetBytuZaznamu];
            ms.Seek(0, 0);
            ms.Position = index * pocetBytuZaznamu;
            ms.Read(buffer, 0, pocetBytuZaznamu);

            RVrchol vrchol = new RVrchol();
            vrchol.Data = Encoding.UTF8.GetString(buffer, 0, RVrchol.maxdelkaretezce).Trim();
            PointF v1 = new PointF(BitConverter.ToSingle(buffer, RVrchol.maxdelkaretezce), BitConverter.ToSingle(buffer, RVrchol.maxdelkaretezce + sizeof(Single)));
            PointF v2 = new PointF(BitConverter.ToSingle(buffer, RVrchol.maxdelkaretezce + sizeof(Single) * 2), BitConverter.ToSingle(buffer, RVrchol.maxdelkaretezce + sizeof(Single) * 3));
            vrchol.Oblast.V1 = v1;
            vrchol.Oblast.V2 = v2;
            vrchol.Oblast.Oblast = vrchol.Oblast.VypoctiObdelnikThis();
            return vrchol;
        }
        public List<RVrchol> BazovyEnumerator() {
            List<RVrchol> vrcholy = new List<RVrchol>((int)(fsBase.Length - 8) / pocetBytuZaznamu);
            int blok = 0;
            while (true) {
                for (int i = 0; i < 3; i++) {
                    RVrchol v = NactiZBazovehoSouboru(blok, i);
                    if (v == null) {
                        return vrcholy;
                    }
                    if (v.Oblast.V1.X == 0 && v.Oblast.V1.Y == 0 && v.Oblast.V2.X == 0 && v.Oblast.V2.Y == 0) {
                        continue;
                    }
                    v.Blok = blok;
                    v.Index = i;
                    vrcholy.Add(v);
                }
                blok++;
            }
        }
        #endregion

        #region RTree blok
        FileStream fsR = new FileStream("fsRStrom.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
        static int pocetBytuZaznamuR = sizeof(Single) * 4 + 2 * sizeof(int);
        static int kapacitaBlokuR = 3;
        int indexBlokuR = -1;
        int indexZacatkuUrovne = 0;
        int pocetZaznamuVMSR = 0;
        MemoryStream msR = new MemoryStream(pocetBytuZaznamuR * kapacitaBlokuR);
        public delegate void BufferJePlnyRHandler();
        public event BufferJePlnyRHandler BufferJePlnyR;

        private void vytvorR(FileStream fs, int kapacitaBlokuR) {
            VymazR();
            BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8);

            bw.BaseStream.Seek(0, 0);

            //hlavička
            bw.Write(kapacitaBlokuR); // počet záznamů na blok
            bw.Write(pocetBytuZaznamuR); //velikost jednoho záznamu v bloku
            bw.Write(-1); //index kořene
            bw.Flush();
        }
        private void vymazBufferR(MemoryStream ms) {
            pocetZaznamuVMSR = 0;
            ms.Seek(0, 0);
            ms.SetLength(0);
        }
        private void zapisDataR(MemoryStream ms, FileStream fs) {
            ms.WriteTo(fs);
            fs.Flush();
        }
        public void ZapisDoBufferuR(MemoryStream ms, byte[] data) {
            if (pocetZaznamuVMSR < kapacitaBlokuR) {
                ms.Write(data, 0, pocetBytuZaznamuR);
                pocetZaznamuVMSR++;
            }
            if (pocetZaznamuVMSR == kapacitaBlokuR) {
                if (ms.Length != kapacitaBlokuR * pocetBytuZaznamuR) {
                    throw new OutOfMemoryException("Chybná velikost dat!");
                }
                if (BufferJePlnyR != null) {
                    BufferJePlnyR(); //zavolá událost
                    vymazBufferR(ms);
                }
            }
        }
        public void VymazR() {
            fsR.Seek(0, 0);
            if (fsR.Length > 0) {
                fsR.SetLength(0);
            }
        }
        void zapisRDataHandler() {
            zapisDataR(msR, fsR);
        }
        public void UlozRSoubor() {
            if (pocetZaznamuVMSR != 0) { // pokud není celý počet dat do bloku na konci, zapíše data
                msR.SetLength(pocetBytuZaznamuR * kapacitaBlokuR); // nastaví velikost na celý blok
                zapisDataR(msR, fsR);
                vymazBufferR(msR);
            }
        }
        private void postavStrom(IList<RVrchol> uroven, bool prvni) {
            BufferJePlnyR += new BufferJePlnyRHandler(zapisRDataHandler);

            if (uroven.Count <= KapacitaUzlu) {
                // pokud je v úrovni již tak málo prvků, že může vzniknout kořen, vytvoříme kořen a ukončíme stavbu.
                root = new RVrchol(KapacitaUzlu);
                foreach (RVrchol item in uroven) {
                    item.Rodic = root; // nastavíme zpětnou vazbu na rodiče
                    root.Potomci.Add(item);
                }
                root.VypoctiObdelnik();
                root.Blok = indexZacatkuUrovne++;
                root.Index = -1;
                ZapisDoBufferuR(msR, root.GetByteArrayR());
                UlozRSoubor();

                msR.Seek(sizeof(int) * 2, SeekOrigin.Begin);
                //msR.Position = sizeof(int) * 2;
                msR.Write(BitConverter.GetBytes(indexZacatkuUrovne), 0, sizeof(int));
                msR.Flush();

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
                    if (prvni) { // přidání odkazu do bázového souboru
                        novyVrchol.Index = uroven[i].Index;
                        novyVrchol.Blok = uroven[i].Blok;
                    } else {
                        novyVrchol.Index = -1;
                        novyVrchol.Blok = indexZacatkuUrovne++; //(uroven.Count / kapacitaBlokuR) + 1 + indexZacatkuUrovne;
                    }
                    uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                    novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                    novyVrchol.VypoctiObdelnik();
                    ZapisDoBufferuR(msR, novyVrchol.GetByteArrayR());
                }
            } else if (pocetPretekajicich >= minimumPrvkuKapacity()) {
                // pokud je prvků více než polovina kapacity, stačí přidat jeden prvek do vrstvy
                pocetVrcholu++;
                for (int i = 0; i < uroven.Count; i++) {
                    if (i == 0 || !novyVrchol.MaVolnySlot()) { // po kapacitě uzlu vytvoříme nový vrchol
                        novyVrchol = new RVrchol(KapacitaUzlu); // vytvořím nový uzel
                        novaUroven.Add(novyVrchol); // přidání vrcholu do úrovně
                    }
                    if (prvni) { // přidání odkazu do bázového souboru
                        novyVrchol.Index = uroven[i].Index;
                        novyVrchol.Blok = uroven[i].Blok;
                    } else {
                        novyVrchol.Index = -1;
                        //TODO: ošetřit čísla bloků
                        novyVrchol.Blok = indexZacatkuUrovne++; //(uroven.Count / kapacitaBlokuR) + 2 + indexZacatkuUrovne;
                    }
                    uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                    novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                    novyVrchol.VypoctiObdelnik();
                    ZapisDoBufferuR(msR, novyVrchol.GetByteArrayR());
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
                        if (prvni) { // přidání odkazu do bázového souboru
                            novyVrchol.Index = uroven[i].Index;
                            novyVrchol.Blok = uroven[i].Blok;
                        } else {
                            novyVrchol.Index = -1;
                            //TODO: ošetřit čísla bloků
                            novyVrchol.Blok = indexZacatkuUrovne++; //(uroven.Count / kapacitaBlokuR) + 2 + indexZacatkuUrovne;
                        }
                        uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                        novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                        novyVrchol.VypoctiObdelnik();
                        ZapisDoBufferuR(msR, novyVrchol.GetByteArrayR());
                        continue;
                    }
                    if (i == 0 || !novyVrchol.MaVolnySlot()) {
                        // po kapacitě uzlu vytvoříme nový vrchol
                        novyVrchol = new RVrchol(KapacitaUzlu); // vytvořím nový uzel
                        novaUroven.Add(novyVrchol); // přidání vrcholu do úrovně
                    }
                    if (prvni) { // přidání odkazu do bázového souboru
                        novyVrchol.Index = uroven[i].Index;
                        novyVrchol.Blok = uroven[i].Blok;
                    } else {
                        novyVrchol.Index = -1;
                        //TODO: ošetřit čísla bloků
                        novyVrchol.Blok = indexZacatkuUrovne++; //(uroven.Count / kapacitaBlokuR) + 2 + indexZacatkuUrovne;
                    }
                    uroven[i].Rodic = novyVrchol; // nižší vrstvě nastavím rodiče
                    novyVrchol.Potomci.Add(uroven[i]); // linkuju no nové vrstvy
                    novyVrchol.VypoctiObdelnik();
                    ZapisDoBufferuR(msR, novyVrchol.GetByteArrayR());
                }
            }
            if (pocetVrcholu != novaUroven.Count) {
                throw new Exception("Něco je špatně, nerovnají se vypočítané vrcholy s počty vrcholů!");
            }
            //indexZacatkuUrovne = uroven.Count / kapacitaBlokuR + 1;
            postavStrom(novaUroven, false);
            BufferJePlnyR -= new BufferJePlnyRHandler(zapisRDataHandler);
        }
        public RVrchol NactiZRSouboru(int indexBloku, int indexZaznamu) {
            if (indexBlokuR != indexBloku) { // pokud již je blok v paměti, nepřistupuj do souboru
                BinaryReader br = new BinaryReader(fsR, Encoding.UTF8);
                if (br.BaseStream.Length == 0) {
                    throw new IOException("Prázdný soubor");
                }
                br.BaseStream.Seek(0, 0);
                int kapacitaBloku = br.ReadInt32();
                int velikostZaznamu = br.ReadInt32();
                int koren = br.ReadInt32();

                if (velikostZaznamu != pocetBytuZaznamuR) {
                    Debug.Write("Rozdílné velikost záznamů, upravuji.");
                    pocetBytuZaznamuR = velikostZaznamu;
                }
                long velikostSouboru = br.BaseStream.Length;
                long pocetPretecenych = (velikostSouboru - sizeof(int) * 3) - (((velikostSouboru - sizeof(int) * 3) / (pocetBytuZaznamuR * kapacitaBlokuR)) * kapacitaBlokuR * pocetBytuZaznamuR);
                if ((pocetPretecenych % pocetBytuZaznamuR != 0) && (pocetPretecenych / pocetBytuZaznamuR > kapacitaBlokuR)) {
                    throw new Exception("Data nemají správný formát!");
                }
                if (pocetZaznamuVMSR != 0) {
                    vymazBuffer(msR);
                }
                byte[] byteArray = new byte[pocetBytuZaznamuR * kapacitaBlokuR];
                //byte[] zaznam = new byte[pocetBytuZaznamu];

                // sizeof(int) * 2 + indexBloku
                if (indexBloku * pocetBytuZaznamuR * kapacitaBlokuR > br.BaseStream.Length) {
                    return null;
                    //throw new ArgumentOutOfRangeException("Index ukazuje za konec souboru blok bloku: " + indexBloku + "!");
                }
                br.BaseStream.Position = sizeof(int) * 3 + indexBloku * pocetBytuZaznamuR * kapacitaBlokuR;
                if (br.BaseStream.Length - br.BaseStream.Position > pocetBytuZaznamuR * kapacitaBlokuR) {
                    // pro prvky s obsazeným celým blokem
                    br.BaseStream.Read(byteArray, 0, pocetBytuZaznamuR * kapacitaBlokuR);
                    msR.Write(byteArray, 0, pocetBytuZaznamuR * kapacitaBlokuR);
                    pocetZaznamuVMSR = 3;
                } else {
                    // Pokud na konci souboru není obsazen celý blok.
                    // Není aktivní, jelikož blok na konci je doplněm na celý blok při zápisu.
                    br.BaseStream.Read(byteArray, 0, (int)(br.BaseStream.Length - br.BaseStream.Position));
                    msR.Write(byteArray, 0, pocetBytuZaznamuR * kapacitaBlokuR);
                    pocetZaznamuVMSR = (int)(br.BaseStream.Length - indexBloku * pocetBytuZaznamuR * kapacitaBlokuR) / pocetBytuZaznamuR;
                }

                //for (int i = 0; i < kapacitaBloku; i++) {
                //    for (int j = 0; j < pocetBytuZaznamu; j++) {
                //        zaznam[j] = byteArray[i * pocetBytuZaznamu + j];
                //    }
                //    ZapisDoBufferu(ms, zaznam);
                //}
                indexBlokuR = indexBloku;
            }
            return nactiVrcholZBufferuR(indexZaznamu);
        }
        private RVrchol nactiVrcholZBufferuR(int index) {
            if (index >= msR.Length / pocetBytuZaznamuR) {
                throw new ArgumentOutOfRangeException("Index záznamu se v bufferu nenalézá!");
            }
            byte[] buffer = new byte[pocetBytuZaznamuR];
            msR.Seek(0, 0);
            msR.Position = index * pocetBytuZaznamuR;
            msR.Read(buffer, 0, pocetBytuZaznamuR);

            RVrchol vrchol = new RVrchol();
            PointF v1 = new PointF(BitConverter.ToSingle(buffer, 0), BitConverter.ToSingle(buffer, 0 + sizeof(Single)));
            PointF v2 = new PointF(BitConverter.ToSingle(buffer, sizeof(Single) * 2), BitConverter.ToSingle(buffer, sizeof(Single) * 3));

            vrchol.Oblast.V1 = v1;
            vrchol.Oblast.V2 = v2;
            vrchol.Blok = BitConverter.ToInt32(buffer, sizeof(Single) * 4);
            vrchol.Index = BitConverter.ToInt32(buffer, sizeof(Single) * 4 + sizeof(int));
            vrchol.Oblast.Oblast = vrchol.Oblast.VypoctiObdelnikThis();
            return vrchol;
        }
        #endregion
    }
}