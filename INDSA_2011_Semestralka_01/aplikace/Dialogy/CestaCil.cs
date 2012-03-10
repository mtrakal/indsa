using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using aplikace.DatoveStruktury;

namespace aplikace.Dialogy {
    public partial class CestaCil : Form {
        public CestyGraf.Hrana HranaCil { get; private set; }
        public CestaCil(List<CestyGraf.Hrana> seznamHran) {
            InitializeComponent();

            foreach (CestyGraf.Hrana item in seznamHran) {
                comboBoxCil.Items.Add(item);
            }
        }

        private void comboBoxCil_SelectedIndexChanged(object sender, EventArgs e) {
            this.HranaCil = comboBoxCil.Items[comboBoxCil.SelectedIndex] as CestyGraf.Hrana;
        }

        private void buttonNajdi_Click(object sender, EventArgs e) {
            if (comboBoxCil.SelectedIndex > 0) {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void buttonZrus_Click(object sender, EventArgs e) {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
