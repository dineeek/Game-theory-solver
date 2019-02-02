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
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrica)).BeginInit();
            this.gbOdabirIgraca.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(231, 371);
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
            this.btnGenerirajMatricu.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGenerirajMatricu.Location = new System.Drawing.Point(253, 74);
            this.btnGenerirajMatricu.Name = "btnGenerirajMatricu";
            this.btnGenerirajMatricu.Size = new System.Drawing.Size(117, 25);
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
            this.dgvMatrica.Location = new System.Drawing.Point(15, 104);
            this.dgvMatrica.MultiSelect = false;
            this.dgvMatrica.Name = "dgvMatrica";
            this.dgvMatrica.RowHeadersVisible = false;
            this.dgvMatrica.RowHeadersWidth = 10;
            this.dgvMatrica.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvMatrica.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMatrica.Size = new System.Drawing.Size(355, 259);
            this.dgvMatrica.TabIndex = 6;
            this.dgvMatrica.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMatrica_DataError);
            this.dgvMatrica.SizeChanged += new System.EventHandler(this.dgvMatrica_SizeChanged);
            // 
            // btnSimplex
            // 
            this.btnSimplex.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSimplex.Enabled = false;
            this.btnSimplex.Location = new System.Drawing.Point(114, 366);
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
            this.btnModelZadatka.Location = new System.Drawing.Point(15, 366);
            this.btnModelZadatka.Name = "btnModelZadatka";
            this.btnModelZadatka.Size = new System.Drawing.Size(93, 23);
            this.btnModelZadatka.TabIndex = 7;
            this.btnModelZadatka.Text = "Model zadatka";
            this.btnModelZadatka.UseVisualStyleBackColor = false;
            this.btnModelZadatka.Click += new System.EventHandler(this.btnModelZadatka_Click);
            // 
            // gbOdabirIgraca
            // 
            this.gbOdabirIgraca.BackColor = System.Drawing.Color.Transparent;
            this.gbOdabirIgraca.Controls.Add(this.rbIgracB);
            this.gbOdabirIgraca.Controls.Add(this.rbIgracA);
            this.gbOdabirIgraca.Location = new System.Drawing.Point(253, 12);
            this.gbOdabirIgraca.Name = "gbOdabirIgraca";
            this.gbOdabirIgraca.Size = new System.Drawing.Size(117, 60);
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
            this.rbIgracA.Location = new System.Drawing.Point(15, 20);
            this.rbIgracA.Name = "rbIgracA";
            this.rbIgracA.Size = new System.Drawing.Size(78, 17);
            this.rbIgracA.TabIndex = 0;
            this.rbIgracA.TabStop = true;
            this.rbIgracA.Text = "za igrača A";
            this.rbIgracA.UseVisualStyleBackColor = true;
            // 
            // PocetnaForma
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::OI2GameTheory.Properties.Resources.poz;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(385, 391);
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
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PocetnaForma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Theory Solver";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrica)).EndInit();
            this.gbOdabirIgraca.ResumeLayout(false);
            this.gbOdabirIgraca.PerformLayout();
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
    }
}

