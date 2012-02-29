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

namespace aplikace {
    public partial class MainForm : Form {
        Vrcholy vrcholy = new Vrcholy();
        Hrany hrany = new Hrany();
        string gmHead;
        string gmBottom;

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

        public void Pokus() {
            Dictionary<string, int> seznamVrcholu = new Dictionary<string, int>();
            double[,] mapa = new double[vrcholy.Count, vrcholy.Count];
            foreach (Hrana item in hrany) {
                vrcholy.Dej(item.Vrchol1.Nazev).PridejHranu(item);
                vrcholy.Dej(item.Vrchol2.Nazev).PridejHranu(item);
            }
            int i = 0;
            foreach (Vrchol item in vrcholy) {
                seznamVrcholu.Add(item.Nazev, i);
                i++;
            }
            for (i = 0; i < seznamVrcholu.Count; i++) {
                for (int j = 0; j < seznamVrcholu.Count; j++) {
                    mapa[i, j] = -1;
                }
            }
            foreach (Hrana item in hrany) {
                if (item.Sjizdna) {
                    mapa[seznamVrcholu[item.Vrchol1.Nazev], seznamVrcholu[item.Vrchol2.Nazev]] = item.Metrika;
                    mapa[seznamVrcholu[item.Vrchol2.Nazev], seznamVrcholu[item.Vrchol1.Nazev]] = item.Metrika;
                }
            }
            Console.WriteLine();
        }

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
                konec();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void konec() {
            //idvHrany.UlozHrany(hrany.Dej());
            Close();
        }

        private void toolStripMenuItemNacist_Click(object sender, EventArgs e) {
            nactiSoubory();
            nactiStranku();
            //Pokus();
        }

        private void nactiSoubory() {
            if (idvVrcholy == null) {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    idvVrcholy = new TextSouborDV(openFileDialog.FileName);
                    vrcholy.Pridej(idvVrcholy.NactiVrcholy());
                }
            }
            if (idvHrany == null) {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    idvHrany = new TextSouborDV(openFileDialog.FileName);
                    hrany.Pridej(idvHrany.NactiHrany(ref vrcholy));
                }
            }
        }
        private void nactiStranku() {
            StringBuilder sb = new StringBuilder();
            foreach (Vrchol item in vrcholy) {
                sb.Append(string.Format(Konstanty.FORMATVRCHOL, item.Nazev, item.Souradnice.X, item.Souradnice.Y));
            }
            foreach (Hrana item in hrany) {
                sb.Append(string.Format(Konstanty.FORMATHRANA,
                    item.Vrchol1.Souradnice.X,
                    item.Vrchol1.Souradnice.Y,
                    item.Vrchol2.Souradnice.X,
                    item.Vrchol2.Souradnice.Y,
                    (item.Vrchol1.Souradnice.X + item.Vrchol2.Souradnice.X) / 2,
                    (item.Vrchol1.Souradnice.Y + item.Vrchol2.Souradnice.Y) / 2,
                    item.Nazev + ": " + item.Metrika));
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
            CestaPridatDialog cp = new CestaPridatDialog(vrcholy.Dej());
            if (cp.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                hrany.Pridej(new Hrana(cp.Nazev, cp.CestaZ, cp.CestaDo, cp.Metrika, true));
                nactiStranku();
                idvHrany.UlozHrany(hrany.Dej());
            }
        }

        private void odebratCestuToolStripMenuItem_Click(object sender, EventArgs e) {
            CestaOdeberDialog co = new CestaOdeberDialog(hrany.Dej());
            if (co.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                hrany.Odeber(co.HranaProOdebrani.Nazev);
                nactiStranku();
                idvHrany.UlozHrany(hrany.Dej());
            }
        }

        private void přidatToolStripMenuItem_Click(object sender, EventArgs e) {
            // načtení města
            MestoPridejDialog mp = new MestoPridejDialog(udalostMouseUpWebBrowser(this, null));
            if (mp.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                vrcholy.Pridej(mp.Mesto);
                nactiStranku();
                idvVrcholy.UlozVrcholy(vrcholy.Dej());
            }
        }
        private void odebratToolStripMenuItem_Click(object sender, EventArgs e) {
            MestoOdeberDialog mo = new MestoOdeberDialog(vrcholy.Dej());
            if (mo.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                vrcholy.Odeber(mo.MestoProOdebrani.Souradnice);
                nactiStranku();
                idvVrcholy.UlozVrcholy(vrcholy.Dej());
            }
        }
        Vrchol udalostMouseUpWebBrowser(object sender, HtmlElementEventArgs e) {
            //if (e.MouseButtonsPressed == MouseButtons.Left) {

            Thread.CurrentThread.CurrentCulture = ci;
            Object[] objArray = new Object[1];
            Vrchol v = new Vrchol();

            try {
                objArray[0] = (object)"bodNazev";
                v.Nazev = webBrowser1.Document.InvokeScript("getValue", objArray).ToString();
                v.Souradnice = new Bod();
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
            // TODO
        }

    }
}
