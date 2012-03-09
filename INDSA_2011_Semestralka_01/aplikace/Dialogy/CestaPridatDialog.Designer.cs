namespace aplikace {
    partial class CestaPridatDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonZrusit = new System.Windows.Forms.Button();
            this.buttonPridat = new System.Windows.Forms.Button();
            this.comboBoxZ = new System.Windows.Forms.ComboBox();
            this.comboBoxDo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNazev = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMetrika = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonZrusit
            // 
            this.buttonZrusit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonZrusit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonZrusit.Location = new System.Drawing.Point(199, 127);
            this.buttonZrusit.Name = "buttonZrusit";
            this.buttonZrusit.Size = new System.Drawing.Size(75, 23);
            this.buttonZrusit.TabIndex = 9;
            this.buttonZrusit.Text = "Zrušit";
            this.buttonZrusit.UseVisualStyleBackColor = true;
            this.buttonZrusit.Click += new System.EventHandler(this.buttonZrusit_Click);
            // 
            // buttonPridat
            // 
            this.buttonPridat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPridat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonPridat.Location = new System.Drawing.Point(118, 127);
            this.buttonPridat.Name = "buttonPridat";
            this.buttonPridat.Size = new System.Drawing.Size(75, 23);
            this.buttonPridat.TabIndex = 8;
            this.buttonPridat.Text = "Přidat";
            this.buttonPridat.UseVisualStyleBackColor = true;
            this.buttonPridat.Click += new System.EventHandler(this.buttonPridat_Click);
            // 
            // comboBoxZ
            // 
            this.comboBoxZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxZ.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxZ.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxZ.FormattingEnabled = true;
            this.comboBoxZ.Location = new System.Drawing.Point(67, 12);
            this.comboBoxZ.Name = "comboBoxZ";
            this.comboBoxZ.Size = new System.Drawing.Size(310, 21);
            this.comboBoxZ.TabIndex = 1;
            this.comboBoxZ.SelectedIndexChanged += new System.EventHandler(this.comboBoxZ_SelectedIndexChanged);
            // 
            // comboBoxDo
            // 
            this.comboBoxDo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDo.FormattingEnabled = true;
            this.comboBoxDo.Location = new System.Drawing.Point(67, 40);
            this.comboBoxDo.Name = "comboBoxDo";
            this.comboBoxDo.Size = new System.Drawing.Size(310, 21);
            this.comboBoxDo.TabIndex = 3;
            this.comboBoxDo.SelectedIndexChanged += new System.EventHandler(this.comboBoxDo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Do:";
            // 
            // textBoxNazev
            // 
            this.textBoxNazev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNazev.Location = new System.Drawing.Point(67, 68);
            this.textBoxNazev.Name = "textBoxNazev";
            this.textBoxNazev.Size = new System.Drawing.Size(310, 20);
            this.textBoxNazev.TabIndex = 5;
            this.textBoxNazev.TextChanged += new System.EventHandler(this.textBoxNazev_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Název";
            // 
            // textBoxMetrika
            // 
            this.textBoxMetrika.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMetrika.Location = new System.Drawing.Point(67, 95);
            this.textBoxMetrika.Name = "textBoxMetrika";
            this.textBoxMetrika.Size = new System.Drawing.Size(310, 20);
            this.textBoxMetrika.TabIndex = 7;
            this.textBoxMetrika.TextChanged += new System.EventHandler(this.textBoxMetrika_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Metrika";
            // 
            // CestaPridatDialog
            // 
            this.AcceptButton = this.buttonPridat;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonZrusit;
            this.ClientSize = new System.Drawing.Size(392, 162);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxMetrika);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxNazev);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxDo);
            this.Controls.Add(this.comboBoxZ);
            this.Controls.Add(this.buttonPridat);
            this.Controls.Add(this.buttonZrusit);
            this.Name = "CestaPridatDialog";
            this.Text = "CestaPridat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonZrusit;
        private System.Windows.Forms.Button buttonPridat;
        private System.Windows.Forms.ComboBox comboBoxZ;
        private System.Windows.Forms.ComboBox comboBoxDo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNazev;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMetrika;
        private System.Windows.Forms.Label label4;
    }
}