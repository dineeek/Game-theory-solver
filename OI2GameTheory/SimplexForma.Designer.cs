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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimplexForma));
            this.dgvSimplexTablica = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimplexTablica)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSimplexTablica
            // 
            this.dgvSimplexTablica.AllowUserToAddRows = false;
            this.dgvSimplexTablica.AllowUserToDeleteRows = false;
            this.dgvSimplexTablica.AllowUserToResizeColumns = false;
            this.dgvSimplexTablica.AllowUserToResizeRows = false;
            this.dgvSimplexTablica.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSimplexTablica.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSimplexTablica.Location = new System.Drawing.Point(12, 26);
            this.dgvSimplexTablica.Name = "dgvSimplexTablica";
            this.dgvSimplexTablica.Size = new System.Drawing.Size(1226, 657);
            this.dgvSimplexTablica.TabIndex = 0;
            // 
            // SimplexForma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OI2GameTheory.Properties.Resources.smplxPoz;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1251, 700);
            this.Controls.Add(this.dgvSimplexTablica);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SimplexForma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simplex postupak problema";
            this.Load += new System.EventHandler(this.SimplexForma_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimplexTablica)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSimplexTablica;
    }
}