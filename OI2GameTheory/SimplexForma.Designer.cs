namespace OI2GameTheory
{
    partial class SimplexForma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimplexForma));
            this.dgvSimplexTablica = new System.Windows.Forms.DataGridView();
            this.txtRjesenje = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnIzracun = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimplexTablica)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSimplexTablica
            // 
            this.dgvSimplexTablica.AllowUserToAddRows = false;
            this.dgvSimplexTablica.AllowUserToDeleteRows = false;
            this.dgvSimplexTablica.AllowUserToResizeColumns = false;
            this.dgvSimplexTablica.AllowUserToResizeRows = false;
            this.dgvSimplexTablica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSimplexTablica.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSimplexTablica.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSimplexTablica.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSimplexTablica.Location = new System.Drawing.Point(12, 26);
            this.dgvSimplexTablica.Name = "dgvSimplexTablica";
            this.dgvSimplexTablica.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSimplexTablica.Size = new System.Drawing.Size(1226, 552);
            this.dgvSimplexTablica.TabIndex = 0;
            // 
            // txtRjesenje
            // 
            this.txtRjesenje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRjesenje.Location = new System.Drawing.Point(12, 603);
            this.txtRjesenje.Multiline = true;
            this.txtRjesenje.Name = "txtRjesenje";
            this.txtRjesenje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRjesenje.Size = new System.Drawing.Size(1226, 73);
            this.txtRjesenje.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Simplex tablice iteracija: ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(9, 587);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Rješenje zadatka: ";
            // 
            // btnIzracun
            // 
            this.btnIzracun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzracun.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnIzracun.Location = new System.Drawing.Point(1139, 579);
            this.btnIzracun.Name = "btnIzracun";
            this.btnIzracun.Size = new System.Drawing.Size(99, 23);
            this.btnIzracun.TabIndex = 4;
            this.btnIzracun.Text = "Prikaz izračuna";
            this.btnIzracun.UseVisualStyleBackColor = false;
            this.btnIzracun.Click += new System.EventHandler(this.btnIzracun_Click);
            // 
            // SimplexForma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1251, 700);
            this.Controls.Add(this.btnIzracun);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRjesenje);
            this.Controls.Add(this.dgvSimplexTablica);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SimplexForma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simplex postupak problema";
            this.Load += new System.EventHandler(this.SimplexForma_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimplexTablica)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSimplexTablica;
        private System.Windows.Forms.TextBox txtRjesenje;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnIzracun;
    }
}