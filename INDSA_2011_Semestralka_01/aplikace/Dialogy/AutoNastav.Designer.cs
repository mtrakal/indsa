namespace aplikace {
    partial class AutoNastav {
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
            this.comboBoxSilnice = new System.Windows.Forms.ComboBox();
            this.hScrollBarPozice = new System.Windows.Forms.HScrollBar();
            this.labelMesto1 = new System.Windows.Forms.Label();
            this.labelMesto2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonZrus = new System.Windows.Forms.Button();
            this.labelOd1 = new System.Windows.Forms.Label();
            this.labelOd2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxSilnice
            // 
            this.comboBoxSilnice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSilnice.FormattingEnabled = true;
            this.comboBoxSilnice.Location = new System.Drawing.Point(13, 13);
            this.comboBoxSilnice.Name = "comboBoxSilnice";
            this.comboBoxSilnice.Size = new System.Drawing.Size(386, 21);
            this.comboBoxSilnice.TabIndex = 0;
            this.comboBoxSilnice.SelectedIndexChanged += new System.EventHandler(this.comboBoxSilnice_SelectedIndexChanged);
            // 
            // hScrollBarPozice
            // 
            this.hScrollBarPozice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBarPozice.LargeChange = 1;
            this.hScrollBarPozice.Location = new System.Drawing.Point(13, 37);
            this.hScrollBarPozice.Name = "hScrollBarPozice";
            this.hScrollBarPozice.Size = new System.Drawing.Size(386, 22);
            this.hScrollBarPozice.TabIndex = 1;
            this.hScrollBarPozice.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarPozice_Scroll);
            // 
            // labelMesto1
            // 
            this.labelMesto1.Location = new System.Drawing.Point(13, 63);
            this.labelMesto1.Name = "labelMesto1";
            this.labelMesto1.Size = new System.Drawing.Size(190, 13);
            this.labelMesto1.TabIndex = 2;
            this.labelMesto1.Text = "label1";
            // 
            // labelMesto2
            // 
            this.labelMesto2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMesto2.Location = new System.Drawing.Point(209, 63);
            this.labelMesto2.Name = "labelMesto2";
            this.labelMesto2.Size = new System.Drawing.Size(190, 13);
            this.labelMesto2.TabIndex = 3;
            this.labelMesto2.Text = "label1";
            this.labelMesto2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(127, 85);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "Nastav";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonZrus
            // 
            this.buttonZrus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonZrus.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonZrus.Location = new System.Drawing.Point(209, 85);
            this.buttonZrus.Name = "buttonZrus";
            this.buttonZrus.Size = new System.Drawing.Size(75, 23);
            this.buttonZrus.TabIndex = 5;
            this.buttonZrus.Text = "Zrušit";
            this.buttonZrus.UseVisualStyleBackColor = true;
            this.buttonZrus.Click += new System.EventHandler(this.buttonZrus_Click);
            // 
            // labelOd1
            // 
            this.labelOd1.AutoSize = true;
            this.labelOd1.Location = new System.Drawing.Point(16, 80);
            this.labelOd1.Name = "labelOd1";
            this.labelOd1.Size = new System.Drawing.Size(35, 13);
            this.labelOd1.TabIndex = 6;
            this.labelOd1.Text = "label1";
            // 
            // labelOd2
            // 
            this.labelOd2.AutoSize = true;
            this.labelOd2.Location = new System.Drawing.Point(363, 80);
            this.labelOd2.Name = "labelOd2";
            this.labelOd2.Size = new System.Drawing.Size(35, 13);
            this.labelOd2.TabIndex = 7;
            this.labelOd2.Text = "label1";
            this.labelOd2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // AutoNastav
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonZrus;
            this.ClientSize = new System.Drawing.Size(411, 120);
            this.Controls.Add(this.labelOd2);
            this.Controls.Add(this.labelOd1);
            this.Controls.Add(this.buttonZrus);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelMesto2);
            this.Controls.Add(this.labelMesto1);
            this.Controls.Add(this.hScrollBarPozice);
            this.Controls.Add(this.comboBoxSilnice);
            this.Name = "AutoNastav";
            this.Text = "AutoNastav";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSilnice;
        private System.Windows.Forms.HScrollBar hScrollBarPozice;
        private System.Windows.Forms.Label labelMesto1;
        private System.Windows.Forms.Label labelMesto2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonZrus;
        private System.Windows.Forms.Label labelOd1;
        private System.Windows.Forms.Label labelOd2;
    }
}