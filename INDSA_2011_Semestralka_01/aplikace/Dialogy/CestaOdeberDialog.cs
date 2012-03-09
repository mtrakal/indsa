﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aplikace {
    public partial class CestaOdeberDialog : Form {
        public Hrana HranaProOdebrani { get; set; }

        public CestaOdeberDialog(List<Graf<string,string,double>.IHrana> hrany) {
            InitializeComponent();
            foreach (Hrana item in hrany) {
                comboBoxHrana.Items.Add(item);
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

        private void comboBoxHrana_SelectedIndexChanged(object sender, EventArgs e) {
            HranaProOdebrani = comboBoxHrana.Items[comboBoxHrana.SelectedIndex] as Hrana;
        }

    }
}
