using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aplikace {
    public partial class AutoNastav : Form {
        public Auto Automobil { get; set; }

        public AutoNastav(List<CestyGraf.Hrana> seznamHran) {
            InitializeComponent();
            buttonOK.Enabled = false;

            foreach (CestyGraf.Hrana item in seznamHran) {
                comboBoxSilnice.Items.Add(item);
            }
        }

        private void buttonZrus_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            Automobil = new Auto();
            Automobil.HranaPoloha = comboBoxSilnice.Items[comboBoxSilnice.SelectedIndex] as CestyGraf.Hrana;
            Automobil.VzdalenostOdV1 = hScrollBarPozice.Value;
            Automobil.VzdalenostOdV2 = hScrollBarPozice.Maximum - hScrollBarPozice.Value;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void comboBoxSilnice_SelectedIndexChanged(object sender, EventArgs e) {
            CestyGraf.Hrana h = comboBoxSilnice.Items[comboBoxSilnice.SelectedIndex] as CestyGraf.Hrana;
            if (!h.Sjizdna) {
                MessageBox.Show("Nesjízdná silnice!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonOK.Enabled = false;
                return;
            }
            buttonOK.Enabled = true;
            hScrollBarPozice.Maximum = (int)Math.Round(h.Metrika, 0);
            hScrollBarPozice.Value = hScrollBarPozice.Maximum / 2;
            labelMesto1.Text = h.Vrchol1.Data;
            labelMesto2.Text = h.Vrchol2.Data;
            hScrollBarPozice_Scroll(null, null);
        }

        private void hScrollBarPozice_Scroll(object sender, ScrollEventArgs e) {
            labelOd1.Text = hScrollBarPozice.Value.ToString();
            labelOd2.Text = (hScrollBarPozice.Maximum - hScrollBarPozice.Value).ToString();
        }

    }
}
