namespace aplikace {
    partial class MestoOdeberDialog {
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
            this.buttonPridat = new System.Windows.Forms.Button();
            this.buttonZrusit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxMesto = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonPridat
            // 
            this.buttonPridat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPridat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonPridat.Location = new System.Drawing.Point(89, 43);
            this.buttonPridat.Name = "buttonPridat";
            this.buttonPridat.Size = new System.Drawing.Size(75, 23);
            this.buttonPridat.TabIndex = 7;
            this.buttonPridat.Text = "Odebrat";
            this.buttonPridat.UseVisualStyleBackColor = true;
            this.buttonPridat.Click += new System.EventHandler(this.buttonPridat_Click);
            // 
            // buttonZrusit
            // 
            this.buttonZrusit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonZrusit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonZrusit.Location = new System.Drawing.Point(170, 43);
            this.buttonZrusit.Name = "buttonZrusit";
            this.buttonZrusit.Size = new System.Drawing.Size(75, 23);
            this.buttonZrusit.TabIndex = 6;
            this.buttonZrusit.Text = "Zrušit";
            this.buttonZrusit.UseVisualStyleBackColor = true;
            this.buttonZrusit.Click += new System.EventHandler(this.buttonZrusit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Město";
            // 
            // comboBoxMesto
            // 
            this.comboBoxMesto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMesto.FormattingEnabled = true;
            this.comboBoxMesto.Location = new System.Drawing.Point(61, 12);
            this.comboBoxMesto.Name = "comboBoxMesto";
            this.comboBoxMesto.Size = new System.Drawing.Size(258, 21);
            this.comboBoxMesto.TabIndex = 4;
            this.comboBoxMesto.SelectedIndexChanged += new System.EventHandler(this.comboBoxMesto_SelectedIndexChanged);
            // 
            // MestoOdeberDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 78);
            this.Controls.Add(this.buttonPridat);
            this.Controls.Add(this.buttonZrusit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxMesto);
            this.Name = "MestoOdeberDialog";
            this.Text = "Odebrání města";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPridat;
        private System.Windows.Forms.Button buttonZrusit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxMesto;

    }
}