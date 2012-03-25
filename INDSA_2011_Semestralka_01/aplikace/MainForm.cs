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

namespace aplikace {
    public partial class MainForm : Form {
        CestyGraf graf = new CestyGraf();
        IDatovaVrstva idvHrany = null;
        IDatovaVrstva idvVrcholy = null;
        Auto auto = null;
        string gmHead;
        string gmBottom;
        LinkedList<CestyGraf.Hrana> cesta = new LinkedList<CestyGraf.Hrana>();

        CultureInfo ci = Konstanty.CULTUREINFO;

        public MainForm() {
            InitializeComponent();
            //webBrowser1.Document.MouseUp += new HtmlElementEventHandler(udalostMouseUpWebBrowser);

            Thread.CurrentThread.CurrentCulture = ci;

            StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + "../../../GoogleMaps_head.htm");
            gmHead = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader = new StreamReader(Directory.GetCurrentDirectory() + "../../../GoogleMaps_bottom.htm");
            gmBottom = streamReader.ReadToEnd();
            streamReader.Close();
            nactiStranku();
        }

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
            Close();
        }

        private void toolStripMenuItemNacist_Click(object sender, EventArgs e) {
            nactiSoubory();
            nactiStranku();
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
                        try {
                            item.Vrchol1.PridejHranu(item);
                            item.Vrchol2.PridejHranu(item);
                        } catch (Exception) {

                        }
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
            webBrowser1.Document.OpenNew(true);
            webBrowser1.DocumentText = s.ToString();
        }

        private void přidatCestuToolStripMenuItem_Click(object sender, EventArgs e) {
            CestaPridatDialog cp = new CestaPridatDialog(graf.DejVrcholy());
            if (cp.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                graf.Pridej(new CestyGraf.Hrana(cp.Nazev, cp.CestaZ, cp.CestaDo, cp.Metrika, true));
                nactiStranku();
                if (idvHrany != null) {
                    idvHrany.UlozHrany(graf.DejHrany());
                }
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
                if (idvHrany != null) {
                    idvHrany.UlozHrany(graf.DejHrany());
                }
            }
        }

        private void přidatToolStripMenuItem_Click(object sender, EventArgs e) {
            // načtení města
            MestoPridejDialog mp = new MestoPridejDialog(udalostMouseUpWebBrowser(this, null));
            if (mp.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                graf.Pridej(mp.Mesto);
                nactiStranku();
                if (idvVrcholy != null) {
                    idvVrcholy.UlozVrcholy(graf.DejVrcholy());
                }
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

                if (idvVrcholy != null) {
                    idvVrcholy.UlozVrcholy(graf.DejVrcholy());
                }
                if (idvHrany != null) {
                    idvHrany.UlozHrany(graf.DejHrany());
                }
            }
        }
        CestyGraf.Vrchol udalostMouseUpWebBrowser(object sender, HtmlElementEventArgs e) {
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
        }

        private void umístitVozidloToolStripMenuItem_Click(object sender, EventArgs e) {
            AutoNastav an = new AutoNastav(graf.DejHrany());
            if (an.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                auto = an.Automobil;
                nactiStranku();
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
                idvHrany.UlozHrany(graf.DejHrany());
                nactiStranku();
            }
        }

        private void najítCestuToolStripMenuItem_Click(object sender, EventArgs e) {
            CestaCil cc = new CestaCil(graf.DejHrany());
            if (cc.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                cesta = Dijkstra.doDijkstra(graf, auto, cc.HranaCil);
                if (cesta.Count == 0) {
                    Debug.WriteLine("nenalezena cesta z " + auto.HranaPoloha.Data + " do: " + cc.HranaCil.Data);
                }
                nactiStranku();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) {
            auto = null;
            cesta = new LinkedList<CestyGraf.Hrana>();
            nactiStranku();
        }

        //private void pokusToolStripMenuItem_Click(object sender, EventArgs e) {
        //    //StringBuilder sb = new StringBuilder();
        //    //sb.Append("javascript:function foo(){");
        //    //int i = 0;
        //    //foreach (CestyGraf.Vrchol item in graf.DejVrcholy()) {
        //    //    sb.Append(string.Format(Konstanty.FORMATVRCHOL, item.Data, item.Souradnice.X, item.Souradnice.Y));
        //    //    i++;
        //    //    if (i == 3) {
        //    //        break;
        //    //    }
        //    //}
        //    //sb.Append("}foo();");

        //    foreach (CestyGraf.Vrchol item in graf.DejVrcholy()) {
        //        string s = "javascript:function foo(){" + string.Format(Konstanty.FORMATVRCHOL, item.Data, item.Souradnice.X, item.Souradnice.Y) + "}foo();";
        //        Debug.WriteLine(s);
        //        webBrowser1.Navigate(s);
        //        break;
        //    }
        //}
    }
}
