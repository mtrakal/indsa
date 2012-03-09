using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aplikace {
    public partial class CestaPridatDialog : Form {
        public Vrchol CestaZ { get; set; }
        public Vrchol CestaDo { get; set; }
        public string Nazev { get; set; }
        public double Metrika { get; set; }

        public CestaPridatDialog(List<Vrchol> vrcholy) {
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

            foreach (Vrchol item in vrcholy) {
                comboBoxZ.Items.Add(item);
                comboBoxDo.Items.Add(item);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            /*
            if (CestaZ.Equals(CestaDo) || true) {
                MessageBox.Show("Zadejte nestejné hodnoty měst!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
             */
            base.OnFormClosing(e);
        }

        private void comboBoxZ_SelectedIndexChanged(object sender, EventArgs e) {
            CestaZ = comboBoxZ.Items[comboBoxZ.SelectedIndex] as Vrchol;
            textBoxMetrika.Text = vypocitejMetriku().ToString();
        }

        private void comboBoxDo_SelectedIndexChanged(object sender, EventArgs e) {
            CestaDo = comboBoxDo.Items[comboBoxDo.SelectedIndex] as Vrchol;
            textBoxMetrika.Text = vypocitejMetriku().ToString();
        }
        private double vypocitejMetriku() {
            if (comboBoxDo.SelectedIndex == -1 || comboBoxZ.SelectedIndex == -1) {
                return 0;
            }
            double d2r = Math.PI / 180.0;
            double dlong = (((comboBoxDo.Items[comboBoxDo.SelectedIndex] as Vrchol).Souradnice.X - (comboBoxZ.Items[comboBoxZ.SelectedIndex] as Vrchol).Souradnice.X)) * d2r;
            double dlat = (((comboBoxDo.Items[comboBoxDo.SelectedIndex] as Vrchol).Souradnice.Y - (comboBoxZ.Items[comboBoxZ.SelectedIndex] as Vrchol).Souradnice.Y)) * d2r;
            double a = Math.Pow(Math.Sin(dlat / 2.0), 2) + Math.Cos((comboBoxZ.Items[comboBoxZ.SelectedIndex] as Vrchol).Souradnice.Y * d2r) * Math.Cos((comboBoxDo.Items[comboBoxDo.SelectedIndex] as Vrchol).Souradnice.Y * d2r) * Math.Pow(Math.Sin(dlong / 2.0), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = 6367 * c;
            return Math.Round(d, 2);
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
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
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
