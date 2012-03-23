using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aplikace {
    public partial class CestaSjizdnost : Form {
        public CestyGraf.Hrana Silnice { get; set; }
        public CestaSjizdnost(List<CestyGraf.Hrana> seznamHran) {
            InitializeComponent();

            foreach (CestyGraf.Hrana item in seznamHran) {
                comboBoxCesta.Items.Add(item);
            }
        }

        private void comboBoxCesta_SelectedIndexChanged(object sender, EventArgs e) {
            checkBoxSjizdna.Checked = (comboBoxCesta.Items[comboBoxCesta.SelectedIndex] as CestyGraf.Hrana).Sjizdna;
        }

        private void buttonOk_Click(object sender, EventArgs e) {
            Silnice = comboBoxCesta.Items[comboBoxCesta.SelectedIndex] as CestyGraf.Hrana;
            Silnice.Sjizdna = checkBoxSjizdna.Checked;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
