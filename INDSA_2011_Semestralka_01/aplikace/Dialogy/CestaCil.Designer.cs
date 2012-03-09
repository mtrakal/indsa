namespace aplikace.Dialogy {
    partial class CestaCil {
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
            this.comboBoxCil = new System.Windows.Forms.ComboBox();
            this.buttonNajdi = new System.Windows.Forms.Button();
            this.buttonZrus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxCil
            // 
            this.comboBoxCil.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCil.FormattingEnabled = true;
            this.comboBoxCil.Location = new System.Drawing.Point(13, 13);
            this.comboBoxCil.Name = "comboBoxCil";
            this.comboBoxCil.Size = new System.Drawing.Size(345, 21);
            this.comboBoxCil.TabIndex = 0;
            this.comboBoxCil.SelectedIndexChanged += new System.EventHandler(this.comboBoxCil_SelectedIndexChanged);
            // 
            // buttonNajdi
            // 
            this.buttonNajdi.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonNajdi.Location = new System.Drawing.Point(107, 45);
            this.buttonNajdi.Name = "buttonNajdi";
            this.buttonNajdi.Size = new System.Drawing.Size(75, 23);
            this.buttonNajdi.TabIndex = 1;
            this.buttonNajdi.Text = "Najdi";
            this.buttonNajdi.UseVisualStyleBackColor = true;
            this.buttonNajdi.Click += new System.EventHandler(this.buttonNajdi_Click);
            // 
            // buttonZrus
            // 
            this.buttonZrus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonZrus.Location = new System.Drawing.Point(188, 45);
            this.buttonZrus.Name = "buttonZrus";
            this.buttonZrus.Size = new System.Drawing.Size(75, 23);
            this.buttonZrus.TabIndex = 2;
            this.buttonZrus.Text = "Zruš";
            this.buttonZrus.UseVisualStyleBackColor = true;
            this.buttonZrus.Click += new System.EventHandler(this.buttonZrus_Click);
            // 
            // CestaCil
            // 
            this.AcceptButton = this.buttonNajdi;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonZrus;
            this.ClientSize = new System.Drawing.Size(370, 80);
            this.Controls.Add(this.buttonZrus);
            this.Controls.Add(this.buttonNajdi);
            this.Controls.Add(this.comboBoxCil);
            this.Name = "CestaCil";
            this.Text = "CestaCil";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCil;
        private System.Windows.Forms.Button buttonNajdi;
        private System.Windows.Forms.Button buttonZrus;
    }
}