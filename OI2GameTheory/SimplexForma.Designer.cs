﻿namespace OI2GameTheory
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
            this.txtRjesenje = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.dgvSimplexTablica.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSimplexTablica.Size = new System.Drawing.Size(1226, 552);
            this.dgvSimplexTablica.TabIndex = 0;
            // 
            // txtRjesenje
            // 
            this.txtRjesenje.Location = new System.Drawing.Point(12, 603);
            this.txtRjesenje.Multiline = true;
            this.txtRjesenje.Name = "txtRjesenje";
            this.txtRjesenje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRjesenje.Size = new System.Drawing.Size(1226, 73);
            this.txtRjesenje.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Simplex tablice iteracija: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(9, 587);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Rješenje zadatka: ";
            // 
            // SimplexForma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OI2GameTheory.Properties.Resources.smplxPoz;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1251, 700);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRjesenje);
            this.Controls.Add(this.dgvSimplexTablica);
            this.DoubleBuffered = true;
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
    }
}