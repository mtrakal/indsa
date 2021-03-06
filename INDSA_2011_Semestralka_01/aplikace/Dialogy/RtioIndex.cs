﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aplikace.Dialogy {
    public partial class RtioIndex : Form {
        public int CisloBloku { get; set; }
        public int CisloIndexu { get; set; }
        public bool Bazovy { get; set; }

        public RtioIndex() {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            if (checkBox1.Checked) {
                Bazovy = true;
            } else {
                Bazovy = false;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            CisloBloku = int.Parse(textBox1.Text);
        }

        private void buttonZrus_Click(object sender, EventArgs e) {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {
            CisloIndexu = int.Parse(textBox2.Text);
        }
    }
}
