namespace aplikace {
    partial class MestoPridejDialog {
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
            this.textBoxNazev = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonPridat
            // 
            this.buttonPridat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPridat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonPridat.Location = new System.Drawing.Point(79, 95);
            this.buttonPridat.Name = "buttonPridat";
            this.buttonPridat.Size = new System.Drawing.Size(75, 23);
            this.buttonPridat.TabIndex = 6;
            this.buttonPridat.Text = "Přidat";
            this.buttonPridat.UseVisualStyleBackColor = true;
            this.buttonPridat.Click += new System.EventHandler(this.buttonPridat_Click);
            // 
            // buttonZrusit
            // 
            this.buttonZrusit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonZrusit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonZrusit.Location = new System.Drawing.Point(160, 95);
            this.buttonZrusit.Name = "buttonZrusit";
            this.buttonZrusit.Size = new System.Drawing.Size(75, 23);
            this.buttonZrusit.TabIndex = 7;
            this.buttonZrusit.Text = "Zrušit";
            this.buttonZrusit.UseVisualStyleBackColor = true;
            // 
            // textBoxNazev
            // 
            this.textBoxNazev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNazev.Location = new System.Drawing.Point(79, 12);
            this.textBoxNazev.Name = "textBoxNazev";
            this.textBoxNazev.Size = new System.Drawing.Size(223, 20);
            this.textBoxNazev.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Název";
            // 
            // textBoxX
            // 
            this.textBoxX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxX.Location = new System.Drawing.Point(79, 39);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(223, 20);
            this.textBoxX.TabIndex = 3;
            // 
            // textBoxY
            // 
            this.textBoxY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxY.Location = new System.Drawing.Point(79, 65);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(223, 20);
            this.textBoxY.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y";
            // 
            // MestoPridejDialog
            // 
            this.AcceptButton = this.buttonPridat;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonZrusit;
            this.ClientSize = new System.Drawing.Size(314, 130);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxY);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNazev);
            this.Controls.Add(this.buttonPridat);
            this.Controls.Add(this.buttonZrusit);
            this.Name = "MestoPridejDialog";
            this.Text = "Přidání města";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPridat;
        private System.Windows.Forms.Button buttonZrusit;
        private System.Windows.Forms.TextBox textBoxNazev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}