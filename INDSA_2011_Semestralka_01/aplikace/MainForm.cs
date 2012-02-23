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

namespace aplikace {
    public partial class MainForm : Form {
        Vrcholy vrcholy = new Vrcholy();
        Hrany hrany = new Hrany();
        string gmHead;
        string gmBottom;

        public MainForm() {
            InitializeComponent();

            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = ci;

            //webBrowser1.Url = new Uri(Directory.GetCurrentDirectory()+"../../../GoogleMaps.htm");
            StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + "../../../GoogleMaps_head.htm");
            gmHead = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader = new StreamReader(Directory.GetCurrentDirectory() + "../../../GoogleMaps_bottom.htm");
            gmBottom = streamReader.ReadToEnd();
            streamReader.Close();
        }

        #region GoogleMaps

        #endregion

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
            nactiStranku();
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
                    (item.Vrchol1.Souradnice.X+item.Vrchol2.Souradnice.X)/2,
                    (item.Vrchol1.Souradnice.Y+item.Vrchol2.Souradnice.Y)/2,
                    item.Nazev + ": " + item.Metrika));
            }
            webBrowser1.DocumentText = gmHead + sb.ToString() + gmBottom;
        }

        private void přidatToolStripMenuItem_Click(object sender, EventArgs e) {

        }
    }
}
