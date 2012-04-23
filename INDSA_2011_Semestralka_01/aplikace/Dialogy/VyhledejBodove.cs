using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aplikace.Dialogy {
    public partial class VyhledejBodove : Form {
        public CestyGraf.Bod VystupniBod { get; set; }
        public VyhledejBodove(List<CestyGraf.Vrchol> mesta) {
            InitializeComponent();
            VystupniBod = new CestyGraf.Bod();

            foreach (CestyGraf.Vrchol item in mesta) {
                comboBoxMesta.Items.Add(item);
            }
        }

        public VyhledejBodove(List<CestyGraf.Vrchol> mesta, CestyGraf.Vrchol v)
            : this(mesta) {
                if (mesta.Contains(v)) {
                    comboBoxMesta.SelectedItem = v;
                } else {
                    textBoxX.Text = v.Souradnice.X.ToString();
                    textBoxY.Text = v.Souradnice.Y.ToString();
                }
        }

        private void comboBoxMesta_SelectedIndexChanged(object sender, EventArgs e) {
            textBoxX.Text = (comboBoxMesta.SelectedItem as CestyGraf.Vrchol).Souradnice.X.ToString();
            textBoxY.Text = (comboBoxMesta.SelectedItem as CestyGraf.Vrchol).Souradnice.Y.ToString();
        }

        private void textBoxX_TextChanged(object sender, EventArgs e) {
            VystupniBod.X = Double.Parse(textBoxX.Text);
        }

        private void textBoxY_TextChanged(object sender, EventArgs e) {
            VystupniBod.Y = Double.Parse(textBoxY.Text);
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonZrus_Click(object sender, EventArgs e) {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
