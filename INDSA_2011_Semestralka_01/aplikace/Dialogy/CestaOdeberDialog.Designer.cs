namespace aplikace {
    partial class CestaOdeberDialog {
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
            this.comboBoxHrana = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPridat = new System.Windows.Forms.Button();
            this.buttonZrusit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxHrana
            // 
            this.comboBoxHrana.FormattingEnabled = true;
            this.comboBoxHrana.Location = new System.Drawing.Point(62, 12);
            this.comboBoxHrana.Name = "comboBoxHrana";
            this.comboBoxHrana.Size = new System.Drawing.Size(258, 21);
            this.comboBoxHrana.TabIndex = 0;
            this.comboBoxHrana.SelectedIndexChanged += new System.EventHandler(this.comboBoxHrana_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cesta";
            // 
            // buttonPridat
            // 
            this.buttonPridat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPridat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonPridat.Location = new System.Drawing.Point(88, 42);
            this.buttonPridat.Name = "buttonPridat";
            this.buttonPridat.Size = new System.Drawing.Size(75, 23);
            this.buttonPridat.TabIndex = 3;
            this.buttonPridat.Text = "Odebrat";
            this.buttonPridat.UseVisualStyleBackColor = true;
            this.buttonPridat.Click += new System.EventHandler(this.buttonPridat_Click);
            // 
            // buttonZrusit
            // 
            this.buttonZrusit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonZrusit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonZrusit.Location = new System.Drawing.Point(169, 42);
            this.buttonZrusit.Name = "buttonZrusit";
            this.buttonZrusit.Size = new System.Drawing.Size(75, 23);
            this.buttonZrusit.TabIndex = 2;
            this.buttonZrusit.Text = "Zrušit";
            this.buttonZrusit.UseVisualStyleBackColor = true;
            this.buttonZrusit.Click += new System.EventHandler(this.buttonZrusit_Click);
            // 
            // CestaOdeberDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 77);
            this.Controls.Add(this.buttonPridat);
            this.Controls.Add(this.buttonZrusit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxHrana);
            this.Name = "CestaOdeberDialog";
            this.Text = "Odebrání cesty";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxHrana;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPridat;
        private System.Windows.Forms.Button buttonZrusit;
    }
}