namespace OI2GameTheory
{
    partial class PocetnaForma
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PocetnaForma));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStrA = new System.Windows.Forms.MaskedTextBox();
            this.txtStrB = new System.Windows.Forms.MaskedTextBox();
            this.btnGenerirajMatricu = new System.Windows.Forms.Button();
            this.dgvMatrica = new System.Windows.Forms.DataGridView();
            this.btnSimplex = new System.Windows.Forms.Button();
            this.btnModelZadatka = new System.Windows.Forms.Button();
            this.gbOdabirIgraca = new System.Windows.Forms.GroupBox();
            this.rbIgracB = new System.Windows.Forms.RadioButton();
            this.rbIgracA = new System.Windows.Forms.RadioButton();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.polaznoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novaIgraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.izlazToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodatnoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ispisTablicaIteracijaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ispisPostupkaIzračunaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomoćToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ispisModelaZadatkaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrica)).BeginInit();
            this.gbOdabirIgraca.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(271, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Autor aplikacije: Dino Kliček";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(21, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Broj strategija igrača A:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(21, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Broj strategija igrača B:";
            // 
            // txtStrA
            // 
            this.txtStrA.Location = new System.Drawing.Point(142, 37);
            this.txtStrA.Mask = "00000";
            this.txtStrA.Name = "txtStrA";
            this.txtStrA.PromptChar = ' ';
            this.txtStrA.Size = new System.Drawing.Size(69, 20);
            this.txtStrA.TabIndex = 3;
            this.txtStrA.ValidatingType = typeof(int);
            this.txtStrA.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtStrA_MouseClick);
            // 
            // txtStrB
            // 
            this.txtStrB.Location = new System.Drawing.Point(142, 68);
            this.txtStrB.Mask = "00000";
            this.txtStrB.Name = "txtStrB";
            this.txtStrB.PromptChar = ' ';
            this.txtStrB.Size = new System.Drawing.Size(69, 20);
            this.txtStrB.TabIndex = 4;
            this.txtStrB.ValidatingType = typeof(int);
            this.txtStrB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtStrB_MouseClick);
            // 
            // btnGenerirajMatricu
            // 
            this.btnGenerirajMatricu.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGenerirajMatricu.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnGenerirajMatricu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnGenerirajMatricu.Location = new System.Drawing.Point(328, 47);
            this.btnGenerirajMatricu.Name = "btnGenerirajMatricu";
            this.btnGenerirajMatricu.Size = new System.Drawing.Size(82, 43);
            this.btnGenerirajMatricu.TabIndex = 5;
            this.btnGenerirajMatricu.Text = "Generiraj matricu";
            this.btnGenerirajMatricu.UseVisualStyleBackColor = false;
            this.btnGenerirajMatricu.Click += new System.EventHandler(this.btnGenerirajMatricu_Click);
            // 
            // dgvMatrica
            // 
            this.dgvMatrica.AllowUserToAddRows = false;
            this.dgvMatrica.AllowUserToDeleteRows = false;
            this.dgvMatrica.AllowUserToResizeColumns = false;
            this.dgvMatrica.AllowUserToResizeRows = false;
            this.dgvMatrica.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMatrica.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMatrica.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMatrica.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatrica.Location = new System.Drawing.Point(19, 96);
            this.dgvMatrica.MultiSelect = false;
            this.dgvMatrica.Name = "dgvMatrica";
            this.dgvMatrica.RowHeadersVisible = false;
            this.dgvMatrica.RowHeadersWidth = 10;
            this.dgvMatrica.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvMatrica.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMatrica.Size = new System.Drawing.Size(391, 259);
            this.dgvMatrica.TabIndex = 6;
            this.dgvMatrica.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMatrica_DataError);
            this.dgvMatrica.SizeChanged += new System.EventHandler(this.dgvMatrica_SizeChanged);
            // 
            // btnSimplex
            // 
            this.btnSimplex.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSimplex.Enabled = false;
            this.btnSimplex.Location = new System.Drawing.Point(118, 358);
            this.btnSimplex.Name = "btnSimplex";
            this.btnSimplex.Size = new System.Drawing.Size(93, 23);
            this.btnSimplex.TabIndex = 8;
            this.btnSimplex.Text = "Simplex metoda";
            this.btnSimplex.UseVisualStyleBackColor = false;
            this.btnSimplex.Click += new System.EventHandler(this.btnSimplex_Click);
            // 
            // btnModelZadatka
            // 
            this.btnModelZadatka.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModelZadatka.Enabled = false;
            this.btnModelZadatka.Location = new System.Drawing.Point(19, 358);
            this.btnModelZadatka.Name = "btnModelZadatka";
            this.btnModelZadatka.Size = new System.Drawing.Size(93, 23);
            this.btnModelZadatka.TabIndex = 7;
            this.btnModelZadatka.Text = "Model problema";
            this.btnModelZadatka.UseVisualStyleBackColor = false;
            this.btnModelZadatka.Click += new System.EventHandler(this.btnModelZadatka_Click);
            // 
            // gbOdabirIgraca
            // 
            this.gbOdabirIgraca.BackColor = System.Drawing.Color.Transparent;
            this.gbOdabirIgraca.Controls.Add(this.rbIgracB);
            this.gbOdabirIgraca.Controls.Add(this.rbIgracA);
            this.gbOdabirIgraca.Location = new System.Drawing.Point(217, 30);
            this.gbOdabirIgraca.Name = "gbOdabirIgraca";
            this.gbOdabirIgraca.Size = new System.Drawing.Size(105, 60);
            this.gbOdabirIgraca.TabIndex = 9;
            this.gbOdabirIgraca.TabStop = false;
            this.gbOdabirIgraca.Text = "Simplex postupak";
            // 
            // rbIgracB
            // 
            this.rbIgracB.AutoSize = true;
            this.rbIgracB.Location = new System.Drawing.Point(15, 37);
            this.rbIgracB.Name = "rbIgracB";
            this.rbIgracB.Size = new System.Drawing.Size(78, 17);
            this.rbIgracB.TabIndex = 1;
            this.rbIgracB.Text = "za igrača B";
            this.rbIgracB.UseVisualStyleBackColor = true;
            // 
            // rbIgracA
            // 
            this.rbIgracA.AutoSize = true;
            this.rbIgracA.Checked = true;
            this.rbIgracA.Location = new System.Drawing.Point(15, 16);
            this.rbIgracA.Name = "rbIgracA";
            this.rbIgracA.Size = new System.Drawing.Size(78, 17);
            this.rbIgracA.TabIndex = 0;
            this.rbIgracA.TabStop = true;
            this.rbIgracA.Text = "za igrača A";
            this.rbIgracA.UseVisualStyleBackColor = true;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.OldLace;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.polaznoToolStripMenuItem,
            this.dodatnoToolStripMenuItem,
            this.pomoćToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(423, 24);
            this.menuStrip.TabIndex = 10;
            this.menuStrip.Text = "menuStrip";
            // 
            // polaznoToolStripMenuItem
            // 
            this.polaznoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novaIgraToolStripMenuItem,
            this.izlazToolStripMenuItem});
            this.polaznoToolStripMenuItem.Name = "polaznoToolStripMenuItem";
            this.polaznoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.polaznoToolStripMenuItem.Text = "Polazno";
            // 
            // novaIgraToolStripMenuItem
            // 
            this.novaIgraToolStripMenuItem.Name = "novaIgraToolStripMenuItem";
            this.novaIgraToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.novaIgraToolStripMenuItem.Text = "Nova igra";
            this.novaIgraToolStripMenuItem.Click += new System.EventHandler(this.novaIgraToolStripMenuItem_Click);
            // 
            // izlazToolStripMenuItem
            // 
            this.izlazToolStripMenuItem.Name = "izlazToolStripMenuItem";
            this.izlazToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.izlazToolStripMenuItem.Text = "Izlaz";
            this.izlazToolStripMenuItem.Click += new System.EventHandler(this.izlazToolStripMenuItem_Click);
            // 
            // dodatnoToolStripMenuItem
            // 
            this.dodatnoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ispisModelaZadatkaToolStripMenuItem,
            this.ispisTablicaIteracijaToolStripMenuItem,
            this.ispisPostupkaIzračunaToolStripMenuItem});
            this.dodatnoToolStripMenuItem.Name = "dodatnoToolStripMenuItem";
            this.dodatnoToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.dodatnoToolStripMenuItem.Text = "Dodatno";
            // 
            // ispisTablicaIteracijaToolStripMenuItem
            // 
            this.ispisTablicaIteracijaToolStripMenuItem.Name = "ispisTablicaIteracijaToolStripMenuItem";
            this.ispisTablicaIteracijaToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ispisTablicaIteracijaToolStripMenuItem.Text = "Ispis tablica iteracija";
            this.ispisTablicaIteracijaToolStripMenuItem.Click += new System.EventHandler(this.ispisTablicaIteracijaToolStripMenuItem_Click);
            // 
            // ispisPostupkaIzračunaToolStripMenuItem
            // 
            this.ispisPostupkaIzračunaToolStripMenuItem.Name = "ispisPostupkaIzračunaToolStripMenuItem";
            this.ispisPostupkaIzračunaToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ispisPostupkaIzračunaToolStripMenuItem.Text = "Ispis postupka izračuna";
            this.ispisPostupkaIzračunaToolStripMenuItem.Click += new System.EventHandler(this.ispisPostupkaIzračunaToolStripMenuItem_Click);
            // 
            // pomoćToolStripMenuItem
            // 
            this.pomoćToolStripMenuItem.Name = "pomoćToolStripMenuItem";
            this.pomoćToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.pomoćToolStripMenuItem.Text = "Pomoć";
            this.pomoćToolStripMenuItem.Click += new System.EventHandler(this.pomoćToolStripMenuItem_Click);
            // 
            // ispisModelaZadatkaToolStripMenuItem
            // 
            this.ispisModelaZadatkaToolStripMenuItem.Name = "ispisModelaZadatkaToolStripMenuItem";
            this.ispisModelaZadatkaToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ispisModelaZadatkaToolStripMenuItem.Text = "Ispis modela problema";
            this.ispisModelaZadatkaToolStripMenuItem.Click += new System.EventHandler(this.ispisModelaZadatkaToolStripMenuItem_Click);
            // 
            // PocetnaForma
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::OI2GameTheory.Properties.Resources.poz;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(423, 384);
            this.Controls.Add(this.gbOdabirIgraca);
            this.Controls.Add(this.btnModelZadatka);
            this.Controls.Add(this.btnSimplex);
            this.Controls.Add(this.dgvMatrica);
            this.Controls.Add(this.btnGenerirajMatricu);
            this.Controls.Add(this.txtStrB);
            this.Controls.Add(this.txtStrA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "PocetnaForma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Theory Solver";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrica)).EndInit();
            this.gbOdabirIgraca.ResumeLayout(false);
            this.gbOdabirIgraca.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtStrA;
        private System.Windows.Forms.MaskedTextBox txtStrB;
        private System.Windows.Forms.Button btnGenerirajMatricu;
        private System.Windows.Forms.DataGridView dgvMatrica;
        private System.Windows.Forms.Button btnSimplex;
        private System.Windows.Forms.Button btnModelZadatka;
        private System.Windows.Forms.GroupBox gbOdabirIgraca;
        private System.Windows.Forms.RadioButton rbIgracA;
        private System.Windows.Forms.RadioButton rbIgracB;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem polaznoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novaIgraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem izlazToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodatnoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ispisTablicaIteracijaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ispisPostupkaIzračunaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomoćToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ispisModelaZadatkaToolStripMenuItem;
    }
}

