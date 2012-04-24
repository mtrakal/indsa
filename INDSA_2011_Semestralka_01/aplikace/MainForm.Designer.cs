namespace aplikace {
    partial class MainForm {
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.souborToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNacist = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.přidatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odebratToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.přidatCestuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odebratCestuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nastavSjízdnostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.umístitVozidloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.najítCestuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rstromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vypočtiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vyhledejBodověToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vyhledejIntervalověToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.nastavVšeNaSjízdnéToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.souborToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mapaToolStripMenuItem,
            this.operaceToolStripMenuItem,
            this.rstromToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(760, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // souborToolStripMenuItem
            // 
            this.souborToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNacist,
            this.tiskToolStripMenuItem,
            this.konecToolStripMenuItem});
            this.souborToolStripMenuItem.Name = "souborToolStripMenuItem";
            this.souborToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.souborToolStripMenuItem.Text = "Soubor";
            // 
            // toolStripMenuItemNacist
            // 
            this.toolStripMenuItemNacist.Name = "toolStripMenuItemNacist";
            this.toolStripMenuItemNacist.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItemNacist.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItemNacist.Text = "Načíst";
            this.toolStripMenuItemNacist.Click += new System.EventHandler(this.toolStripMenuItemNacist_Click);
            // 
            // tiskToolStripMenuItem
            // 
            this.tiskToolStripMenuItem.Name = "tiskToolStripMenuItem";
            this.tiskToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.tiskToolStripMenuItem.Text = "Tisk";
            this.tiskToolStripMenuItem.Click += new System.EventHandler(this.tiskToolStripMenuItem_Click);
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.přidatToolStripMenuItem,
            this.odebratToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.toolStripMenuItem1.Text = "Město";
            // 
            // přidatToolStripMenuItem
            // 
            this.přidatToolStripMenuItem.Name = "přidatToolStripMenuItem";
            this.přidatToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.přidatToolStripMenuItem.Text = "Přidat";
            this.přidatToolStripMenuItem.Click += new System.EventHandler(this.přidatToolStripMenuItem_Click);
            // 
            // odebratToolStripMenuItem
            // 
            this.odebratToolStripMenuItem.Name = "odebratToolStripMenuItem";
            this.odebratToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.odebratToolStripMenuItem.Text = "Odebrat";
            this.odebratToolStripMenuItem.Click += new System.EventHandler(this.odebratToolStripMenuItem_Click);
            // 
            // mapaToolStripMenuItem
            // 
            this.mapaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.přidatCestuToolStripMenuItem,
            this.odebratCestuToolStripMenuItem,
            this.nastavSjízdnostToolStripMenuItem});
            this.mapaToolStripMenuItem.Name = "mapaToolStripMenuItem";
            this.mapaToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.mapaToolStripMenuItem.Text = "Cesta";
            // 
            // přidatCestuToolStripMenuItem
            // 
            this.přidatCestuToolStripMenuItem.Name = "přidatCestuToolStripMenuItem";
            this.přidatCestuToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.přidatCestuToolStripMenuItem.Text = "Přidat";
            this.přidatCestuToolStripMenuItem.Click += new System.EventHandler(this.přidatCestuToolStripMenuItem_Click);
            // 
            // odebratCestuToolStripMenuItem
            // 
            this.odebratCestuToolStripMenuItem.Name = "odebratCestuToolStripMenuItem";
            this.odebratCestuToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.odebratCestuToolStripMenuItem.Text = "Odebrat";
            this.odebratCestuToolStripMenuItem.Click += new System.EventHandler(this.odebratCestuToolStripMenuItem_Click);
            // 
            // nastavSjízdnostToolStripMenuItem
            // 
            this.nastavSjízdnostToolStripMenuItem.Name = "nastavSjízdnostToolStripMenuItem";
            this.nastavSjízdnostToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.nastavSjízdnostToolStripMenuItem.Text = "Nastav sjízdnost";
            this.nastavSjízdnostToolStripMenuItem.Click += new System.EventHandler(this.nastavSjízdnostToolStripMenuItem_Click);
            // 
            // operaceToolStripMenuItem
            // 
            this.operaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.umístitVozidloToolStripMenuItem,
            this.toolStripMenuItem2,
            this.najítCestuToolStripMenuItem});
            this.operaceToolStripMenuItem.Name = "operaceToolStripMenuItem";
            this.operaceToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.operaceToolStripMenuItem.Text = "Operace";
            // 
            // umístitVozidloToolStripMenuItem
            // 
            this.umístitVozidloToolStripMenuItem.Name = "umístitVozidloToolStripMenuItem";
            this.umístitVozidloToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.umístitVozidloToolStripMenuItem.Text = "Umístit vozidlo";
            this.umístitVozidloToolStripMenuItem.Click += new System.EventHandler(this.umístitVozidloToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem2.Text = "Odstranit vozidlo";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // najítCestuToolStripMenuItem
            // 
            this.najítCestuToolStripMenuItem.Name = "najítCestuToolStripMenuItem";
            this.najítCestuToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.najítCestuToolStripMenuItem.Text = "Najít cestu";
            this.najítCestuToolStripMenuItem.Click += new System.EventHandler(this.najítCestuToolStripMenuItem_Click);
            // 
            // rstromToolStripMenuItem
            // 
            this.rstromToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vypočtiToolStripMenuItem,
            this.vyhledejBodověToolStripMenuItem,
            this.vyhledejIntervalověToolStripMenuItem,
            this.nastavVšeNaSjízdnéToolStripMenuItem});
            this.rstromToolStripMenuItem.Name = "rstromToolStripMenuItem";
            this.rstromToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.rstromToolStripMenuItem.Text = "R-strom";
            // 
            // vypočtiToolStripMenuItem
            // 
            this.vypočtiToolStripMenuItem.Name = "vypočtiToolStripMenuItem";
            this.vypočtiToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.vypočtiToolStripMenuItem.Text = "Vypočti";
            this.vypočtiToolStripMenuItem.Click += new System.EventHandler(this.vypočtiToolStripMenuItem_Click);
            // 
            // vyhledejBodověToolStripMenuItem
            // 
            this.vyhledejBodověToolStripMenuItem.Name = "vyhledejBodověToolStripMenuItem";
            this.vyhledejBodověToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.vyhledejBodověToolStripMenuItem.Text = "Vyhledej bodově";
            this.vyhledejBodověToolStripMenuItem.Click += new System.EventHandler(this.vyhledejBodověToolStripMenuItem_Click);
            // 
            // vyhledejIntervalověToolStripMenuItem
            // 
            this.vyhledejIntervalověToolStripMenuItem.Name = "vyhledejIntervalověToolStripMenuItem";
            this.vyhledejIntervalověToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.vyhledejIntervalověToolStripMenuItem.Text = "Vyhledej intervalově";
            this.vyhledejIntervalověToolStripMenuItem.Click += new System.EventHandler(this.vyhledejIntervalověToolStripMenuItem_Click);
            // 
            // buttonKonec
            // 
            this.buttonKonec.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonKonec.Location = new System.Drawing.Point(673, 0);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(75, 23);
            this.buttonKonec.TabIndex = 2;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Visible = false;
            this.buttonKonec.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 569);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(760, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.AutoSize = false;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(600, 17);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(0, 27);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(760, 539);
            this.webBrowser1.TabIndex = 4;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // nastavVšeNaSjízdnéToolStripMenuItem
            // 
            this.nastavVšeNaSjízdnéToolStripMenuItem.Name = "nastavVšeNaSjízdnéToolStripMenuItem";
            this.nastavVšeNaSjízdnéToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.nastavVšeNaSjízdnéToolStripMenuItem.Text = "Nastav vše na sjízdné";
            this.nastavVšeNaSjízdnéToolStripMenuItem.Click += new System.EventHandler(this.nastavVšeNaSjízdnéToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonKonec;
            this.ClientSize = new System.Drawing.Size(760, 591);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.buttonKonec);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INDSA_01";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem souborToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNacist;
        private System.Windows.Forms.ToolStripMenuItem mapaToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem přidatCestuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odebratCestuToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem přidatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odebratToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem umístitVozidloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem najítCestuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nastavSjízdnostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rstromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vypočtiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vyhledejBodověToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vyhledejIntervalověToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nastavVšeNaSjízdnéToolStripMenuItem;

    }
}