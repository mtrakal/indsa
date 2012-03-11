using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using aplikace.Dialogy;
using aplikace.DatoveStruktury;

namespace aplikace {
    public partial class MainForm : Form {
        CestyGraf graf = new CestyGraf();
        //CestyGraf.Vrcholy vrcholy = new CestyGraf.Vrcholy();
        //CestyGraf.Hrany hrany = new CestyGraf.Hrany();
        Auto auto = null;
        string gmHead;
        string gmBottom;
        LinkedList<CestyGraf.Hrana> cesta = new LinkedList<CestyGraf.Hrana>();

        CultureInfo ci = Konstanty.CULTUREINFO;

        public MainForm() {
            InitializeComponent();
            //webBrowser1.Document.MouseUp += new HtmlElementEventHandler(udalostMouseUpWebBrowser);

            Thread.CurrentThread.CurrentCulture = ci;

            //webBrowser1.Url = new Uri(Directory.GetCurrentDirectory()+"../../../GoogleMaps.htm");
            StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + "../../../GoogleMaps_head.htm");
            gmHead = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader = new StreamReader(Directory.GetCurrentDirectory() + "../../../GoogleMaps_bottom.htm");
            gmBottom = streamReader.ReadToEnd();
            streamReader.Close();

            nactiStranku();
        }

        //public void Pokus() {
        //    Dictionary<string, int> seznamVrcholu = new Dictionary<string, int>();
        //    double[,] mapa = new double[graf.CountVrcholy, graf.CountVrcholy];
        //    foreach (CestyGraf.Hrana item in hrany) {
        //        vrcholy.Dej(item.Vrchol1.Data).PridejHranu(item);
        //        vrcholy.Dej(item.Vrchol2.Data).PridejHranu(item);
        //    }
        //    int i = 0;
        //    foreach (CestyGraf.Vrchol item in vrcholy) {
        //        seznamVrcholu.Add(item.Data, i);
        //        i++;
        //    }
        //    for (i = 0; i < seznamVrcholu.Count; i++) {
        //        for (int j = 0; j < seznamVrcholu.Count; j++) {
        //            mapa[i, j] = -1;
        //        }
        //    }
        //    foreach (CestyGraf.Hrana item in hrany) {
        //        if (item.Sjizdna) {
        //            mapa[seznamVrcholu[item.Vrchol1.Data], seznamVrcholu[item.Vrchol2.Data]] = item.Metrika;
        //            mapa[seznamVrcholu[item.Vrchol2.Data], seznamVrcholu[item.Vrchol1.Data]] = item.Metrika;
        //        }
        //    }
        //    Console.WriteLine();
        //}

        IDatovaVrstva idvHrany = null;
        IDatovaVrstva idvVrcholy = null;

        private void konecToolStripMenuItem_Click(object sender, EventArgs e) {
            konec();
        }

        private void button1_Click(object sender, EventArgs e) {
            konec();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == Keys.Escape) {
                this.BeginInvoke((MethodInvoker)delegate { konec(); });
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void konec() {
            //idvHrany.UlozHrany(seznamHran.Dej());
            Close();
        }

        private void toolStripMenuItemNacist_Click(object sender, EventArgs e) {
            nactiSoubory();
            //nactiStranku();
        }

        private void nactiSoubory() {
            if (idvVrcholy == null) {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    idvVrcholy = new TextSouborDV(openFileDialog.FileName);
                    graf.Pridej(idvVrcholy.NactiVrcholy());
                }
            }
            if (idvHrany == null) {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    idvHrany = new TextSouborDV(openFileDialog.FileName);

                    graf.Pridej(idvHrany.NactiHrany(ref graf));

                    foreach (CestyGraf.Hrana item in graf.DejHrany()) {
                        item.Vrchol1.PridejHranu(item);
                        item.Vrchol2.PridejHranu(item);
                    }
                }
            }
        }
        private void nactiStranku() {
            StringBuilder sb = new StringBuilder();
            foreach (CestyGraf.Vrchol item in graf.DejVrcholy()) {
                sb.Append(string.Format(Konstanty.FORMATVRCHOL, item.Data, item.Souradnice.X, item.Souradnice.Y));
            }
            foreach (CestyGraf.Hrana item in graf.DejHrany()) {
                sb.Append(string.Format((item.Sjizdna) ? Konstanty.FORMATHRANA : Konstanty.FORMATHRANANESJIZDNA,
                    item.Vrchol1.Souradnice.X,
                    item.Vrchol1.Souradnice.Y,
                    item.Vrchol2.Souradnice.X,
                    item.Vrchol2.Souradnice.Y,
                    (item.Vrchol1.Souradnice.X + item.Vrchol2.Souradnice.X) / 2,
                    (item.Vrchol1.Souradnice.Y + item.Vrchol2.Souradnice.Y) / 2,
                    item.Data + ": " + item.Metrika));
            }
            foreach (CestyGraf.Hrana item in cesta) {
                sb.Append(string.Format(Konstanty.FORMATHRANAVYBRANA,
                    item.Vrchol1.Souradnice.X,
                    item.Vrchol1.Souradnice.Y,
                    item.Vrchol2.Souradnice.X,
                    item.Vrchol2.Souradnice.Y,
                    item.Data + ": " + item.Metrika));
            }
            if (auto != null) {
                sb.Append(string.Format(Konstanty.FORMATAUTO, "auto", auto.DejPolohu().Souradnice.X, auto.DejPolohu().Souradnice.Y));
            }
            string s = gmHead + sb.ToString() + gmBottom;
            //webBrowser1.Document.Write(s);
            webBrowser1.Document.OpenNew(true);
            //webBrowser1.Navigate("about:blank");
            //Debug.Write(s);
            //            webBrowser1.Document.Write(s.ToString());
            webBrowser1.DocumentText = s.ToString();
            /*
            StreamWriter sw = new StreamWriter("./aaaa.txt");
            sw.WriteLine(gmHead + sb.ToString() + gmBottom);
            sw.Close();
             */
        }

        private void přidatCestuToolStripMenuItem_Click(object sender, EventArgs e) {
            CestaPridatDialog cp = new CestaPridatDialog(graf.DejVrcholy());
            if (cp.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                graf.Pridej(new CestyGraf.Hrana(cp.Nazev, cp.CestaZ, cp.CestaDo, cp.Metrika, true));
                nactiStranku();
                idvHrany.UlozHrany(graf.DejHrany());
            }
        }

        private void odebratCestuToolStripMenuItem_Click(object sender, EventArgs e) {
            CestaOdeberDialog co = new CestaOdeberDialog(graf.DejHrany());
            if (co.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (auto != null) {
                    if (auto.HranaPoloha.Data == co.HranaProOdebrani.Data) {
                        MessageBox.Show("Automobil je na této silnici, nelze ji odstranit.");
                        return;
                    }
                }
                co.HranaProOdebrani.Vrchol1.OdeberHranu(co.HranaProOdebrani.Data);
                co.HranaProOdebrani.Vrchol2.OdeberHranu(co.HranaProOdebrani.Data);
                graf.Odeber(co.HranaProOdebrani.Data); //hrana
                nactiStranku();
                idvHrany.UlozHrany(graf.DejHrany());
            }
        }

        private void přidatToolStripMenuItem_Click(object sender, EventArgs e) {
            // načtení města
            MestoPridejDialog mp = new MestoPridejDialog(udalostMouseUpWebBrowser(this, null));
            if (mp.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                graf.Pridej(mp.Mesto);
                nactiStranku();
                idvVrcholy.UlozVrcholy(graf.DejVrcholy());
            }
        }
        private void odebratToolStripMenuItem_Click(object sender, EventArgs e) {
            MestoOdeberDialog mo = new MestoOdeberDialog(graf.DejVrcholy());
            if (mo.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                foreach (CestyGraf.Hrana item in mo.MestoProOdebrani.DejHrany()) {
                    graf.Odeber(item.Data);
                }
                graf.Odeber((CestyGraf.Bod)mo.MestoProOdebrani.Souradnice);
                nactiStranku();
                idvVrcholy.UlozVrcholy(graf.DejVrcholy());
                idvHrany.UlozHrany(graf.DejHrany());
            }
        }
        CestyGraf.Vrchol udalostMouseUpWebBrowser(object sender, HtmlElementEventArgs e) {
            //if (e.MouseButtonsPressed == MouseButtons.Left) {

            Thread.CurrentThread.CurrentCulture = ci;
            Object[] objArray = new Object[1];
            CestyGraf.Vrchol v = new CestyGraf.Vrchol();

            try {
                objArray[0] = (object)"bodNazev";
                v.Data = webBrowser1.Document.InvokeScript("getValue", objArray).ToString();
                v.Souradnice = new CestyGraf.Bod();
                objArray[0] = (object)"bodPolohaX";
                v.Souradnice.X = double.Parse(webBrowser1.Document.InvokeScript("getValue", objArray).ToString().Trim(), ci.NumberFormat);
                objArray[0] = (object)"bodPolohaY";
                v.Souradnice.Y = double.Parse(webBrowser1.Document.InvokeScript("getValue", objArray).ToString().Trim(), ci.NumberFormat);
                return v;
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return null;
            }
            //}
        }

        private void umístitVozidloToolStripMenuItem_Click(object sender, EventArgs e) {
            AutoNastav an = new AutoNastav(graf.DejHrany());
            if (an.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                auto = an.Automobil;
                //nactiStranku();
            }
        }
        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            idvHrany = null;
            idvVrcholy = null;
        }

        private void nastavSjízdnostToolStripMenuItem_Click(object sender, EventArgs e) {
            CestaSjizdnost cs = new CestaSjizdnost(graf.DejHrany());
            if (cs.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (auto != null) {
                    if (auto.HranaPoloha.Data == cs.Silnice.Data) {
                        MessageBox.Show("Automobil je na této silnici, nelze ji nastavit na nesjízdnou");
                        return;
                    }
                }
                (graf.DejHranu(cs.Silnice.Data) as CestyGraf.Hrana).Sjizdna = cs.Silnice.Sjizdna;
                nactiStranku();
            }
        }

        private void najítCestuToolStripMenuItem_Click(object sender, EventArgs e) {
            CestaCil cc = new CestaCil(graf.DejHrany());
            if (cc.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                cesta = Dijkstra.doDijkstra(graf, auto, cc.HranaCil);
                nactiStranku();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) {
            auto = null;
        }
    }
}
