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
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrica)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(231, 353);
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
            this.label2.Location = new System.Drawing.Point(12, 28);
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
            this.label3.Location = new System.Drawing.Point(12, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Broj strategija igrača B:";
            // 
            // txtStrA
            // 
            this.txtStrA.Location = new System.Drawing.Point(133, 25);
            this.txtStrA.Mask = "00000";
            this.txtStrA.Name = "txtStrA";
            this.txtStrA.PromptChar = ' ';
            this.txtStrA.Size = new System.Drawing.Size(100, 20);
            this.txtStrA.TabIndex = 3;
            this.txtStrA.ValidatingType = typeof(int);
            this.txtStrA.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtStrA_MouseClick);
            // 
            // txtStrB
            // 
            this.txtStrB.Location = new System.Drawing.Point(133, 56);
            this.txtStrB.Mask = "00000";
            this.txtStrB.Name = "txtStrB";
            this.txtStrB.PromptChar = ' ';
            this.txtStrB.Size = new System.Drawing.Size(100, 20);
            this.txtStrB.TabIndex = 4;
            this.txtStrB.ValidatingType = typeof(int);
            this.txtStrB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtStrB_MouseClick);
            // 
            // btnGenerirajMatricu
            // 
            this.btnGenerirajMatricu.Location = new System.Drawing.Point(254, 35);
            this.btnGenerirajMatricu.Name = "btnGenerirajMatricu";
            this.btnGenerirajMatricu.Size = new System.Drawing.Size(116, 41);
            this.btnGenerirajMatricu.TabIndex = 5;
            this.btnGenerirajMatricu.Text = "Generiraj matricu";
            this.btnGenerirajMatricu.UseVisualStyleBackColor = true;
            this.btnGenerirajMatricu.Click += new System.EventHandler(this.btnGenerirajMatricu_Click);
            // 
            // dgvMatrica
            // 
            this.dgvMatrica.AllowUserToAddRows = false;
            this.dgvMatrica.AllowUserToDeleteRows = false;
            this.dgvMatrica.AllowUserToResizeColumns = false;
            this.dgvMatrica.AllowUserToResizeRows = false;
            this.dgvMatrica.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMatrica.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatrica.Location = new System.Drawing.Point(15, 86);
            this.dgvMatrica.MultiSelect = false;
            this.dgvMatrica.Name = "dgvMatrica";
            this.dgvMatrica.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvMatrica.Size = new System.Drawing.Size(355, 259);
            this.dgvMatrica.TabIndex = 6;
            this.dgvMatrica.SizeChanged += new System.EventHandler(this.dgvMatrica_SizeChanged);
            // 
            // btnSimplex
            // 
            this.btnSimplex.Enabled = false;
            this.btnSimplex.Location = new System.Drawing.Point(114, 348);
            this.btnSimplex.Name = "btnSimplex";
            this.btnSimplex.Size = new System.Drawing.Size(93, 23);
            this.btnSimplex.TabIndex = 7;
            this.btnSimplex.Text = "Simplex metoda";
            this.btnSimplex.UseVisualStyleBackColor = true;
            this.btnSimplex.Click += new System.EventHandler(this.btnSimplex_Click);
            // 
            // btnModelZadatka
            // 
            this.btnModelZadatka.Enabled = false;
            this.btnModelZadatka.Location = new System.Drawing.Point(15, 348);
            this.btnModelZadatka.Name = "btnModelZadatka";
            this.btnModelZadatka.Size = new System.Drawing.Size(93, 23);
            this.btnModelZadatka.TabIndex = 8;
            this.btnModelZadatka.Text = "Model zadatka";
            this.btnModelZadatka.UseVisualStyleBackColor = true;
            this.btnModelZadatka.Click += new System.EventHandler(this.btnModelZadatka_Click);
            // 
            // PocetnaForma
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::OI2GameTheory.Properties.Resources.poz;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(381, 375);
            this.Controls.Add(this.btnModelZadatka);
            this.Controls.Add(this.btnSimplex);
            this.Controls.Add(this.dgvMatrica);
            this.Controls.Add(this.btnGenerirajMatricu);
            this.Controls.Add(this.txtStrB);
            this.Controls.Add(this.txtStrA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PocetnaForma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Teorija igara";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrica)).EndInit();
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
    }
}

