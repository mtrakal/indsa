using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aplikace {
    public partial class MestoOdeberDialog : Form {
        public Vrchol MestoProOdebrani { get; set; }
        public MestoOdeberDialog(List<Vrchol> vrcholy) {
            InitializeComponent();

            foreach (Vrchol item in vrcholy) {
                comboBoxMesto.Items.Add(item);
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

        private void comboBoxMesto_SelectedIndexChanged(object sender, EventArgs e) {
            MestoProOdebrani = comboBoxMesto.Items[comboBoxMesto.SelectedIndex] as Vrchol;
        }
    }
}
