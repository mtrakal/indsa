using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using aplikace.DatoveStruktury;

namespace aplikace {
    public partial class CestaPridatDialog : Form {
        public CestyGraf.Vrchol CestaZ { get; set; }
        public CestyGraf.Vrchol CestaDo { get; set; }
        public string Nazev { get; set; }
        public double Metrika { get; set; }

        public CestaPridatDialog(List<CestyGraf.Vrchol> vrcholy) {
            InitializeComponent();
            try {
                if (vrcholy == null || vrcholy.Count == 0) {
                    MessageBox.Show("Prázdný seznam vrcholů");
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    Close();
                }
            } catch (Exception) {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close();
            }

            foreach (CestyGraf.Vrchol item in vrcholy) {
                comboBoxZ.Items.Add(item);
                comboBoxDo.Items.Add(item);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
        }

        private void comboBoxZ_SelectedIndexChanged(object sender, EventArgs e) {
            CestaZ = comboBoxZ.Items[comboBoxZ.SelectedIndex] as CestyGraf.Vrchol;
            textBoxMetrika.Text = vypocitejMetriku().ToString();
        }

        private void comboBoxDo_SelectedIndexChanged(object sender, EventArgs e) {
            CestaDo = comboBoxDo.Items[comboBoxDo.SelectedIndex] as CestyGraf.Vrchol;
            textBoxMetrika.Text = vypocitejMetriku().ToString();
        }
        private double vypocitejMetriku() {
            // http://stackoverflow.com/questions/365826/calculate-distance-between-2-gps-coordinates
            if (comboBoxDo.SelectedIndex == -1 || comboBoxZ.SelectedIndex == -1) {
                return 0;
            }
            return Konstanty.VypocitejVzdalenost(comboBoxZ.Items[comboBoxZ.SelectedIndex] as CestyGraf.Vrchol, comboBoxDo.Items[comboBoxDo.SelectedIndex] as CestyGraf.Vrchol);
        }

        private void textBoxMetrika_TextChanged(object sender, EventArgs e) {
            try {
                Metrika = double.Parse(textBoxMetrika.Text);
            } catch (Exception) {
                MessageBox.Show("Není zadáno číslo double!");
                textBoxMetrika.Text = "0";
            }
        }

        private void buttonPridat_Click(object sender, EventArgs e) {
            if ((CestaZ.Souradnice.Equals(CestaDo.Souradnice))) {
                MessageBox.Show("Zadejte nestejné hodnoty měst!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = System.Windows.Forms.DialogResult.None;
            } else {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void buttonZrusit_Click(object sender, EventArgs e) {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void textBoxNazev_TextChanged(object sender, EventArgs e) {
            Nazev = textBoxNazev.Text;
        }
    }
}
