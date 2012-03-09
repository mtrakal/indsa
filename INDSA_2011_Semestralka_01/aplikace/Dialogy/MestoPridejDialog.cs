using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aplikace {
    public partial class MestoPridejDialog : Form {
        public Vrchol Mesto { get; set; }
        public MestoPridejDialog() {
            InitializeComponent();
        }

        public MestoPridejDialog(Vrchol vrchol)
            : this() {
            Mesto = vrchol;

            if (Mesto != null) {
                textBoxNazev.Text = Mesto.Data;
                if (Mesto.Souradnice != null) {
                    textBoxX.Text = Mesto.Souradnice.X.ToString();
                    textBoxY.Text = Mesto.Souradnice.Y.ToString();
                }
            }
        }

        private void buttonPridat_Click(object sender, EventArgs e) {
            if (textBoxNazev.Text.Length == 0 || textBoxX.Left == 0 || textBoxY.Text.Length == 0) {
                MessageBox.Show("Některý z parametrů není zadán");
                return;
            }
            try {
                Vrchol v = new Vrchol(textBoxNazev.Text, double.Parse(textBoxX.Text), double.Parse(textBoxY.Text));
                Mesto = v;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            } catch (Exception) {
                MessageBox.Show("Některý z parametrů není zadán správně");
                return;
            }

        }
    }
}
