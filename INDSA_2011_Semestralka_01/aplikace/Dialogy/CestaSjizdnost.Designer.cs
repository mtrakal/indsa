namespace aplikace {
    partial class CestaSjizdnost {
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
            this.comboBoxCesta = new System.Windows.Forms.ComboBox();
            this.checkBoxSjizdna = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxCesta
            // 
            this.comboBoxCesta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCesta.FormattingEnabled = true;
            this.comboBoxCesta.Location = new System.Drawing.Point(13, 13);
            this.comboBoxCesta.Name = "comboBoxCesta";
            this.comboBoxCesta.Size = new System.Drawing.Size(328, 21);
            this.comboBoxCesta.TabIndex = 0;
            this.comboBoxCesta.SelectedIndexChanged += new System.EventHandler(this.comboBoxCesta_SelectedIndexChanged);
            // 
            // checkBoxSjizdna
            // 
            this.checkBoxSjizdna.AutoSize = true;
            this.checkBoxSjizdna.Location = new System.Drawing.Point(13, 41);
            this.checkBoxSjizdna.Name = "checkBoxSjizdna";
            this.checkBoxSjizdna.Size = new System.Drawing.Size(62, 17);
            this.checkBoxSjizdna.TabIndex = 1;
            this.checkBoxSjizdna.Text = "Sjízdná";
            this.checkBoxSjizdna.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.buttonOk.Location = new System.Drawing.Point(98, 58);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Nastav";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(180, 57);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Zruš";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // CestaSjizdnost
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(353, 93);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.checkBoxSjizdna);
            this.Controls.Add(this.comboBoxCesta);
            this.Name = "CestaSjizdnost";
            this.Text = "CestaSjizdnost";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCesta;
        private System.Windows.Forms.CheckBox checkBoxSjizdna;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}