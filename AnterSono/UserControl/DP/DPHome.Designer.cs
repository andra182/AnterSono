﻿namespace AnterSono
{
    partial class DPHome
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewPaket = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnKirimPaket = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPaket)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPaket
            // 
            this.dataGridViewPaket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPaket.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dataGridViewPaket.Location = new System.Drawing.Point(38, 66);
            this.dataGridViewPaket.Name = "dataGridViewPaket";
            this.dataGridViewPaket.ReadOnly = true;
            this.dataGridViewPaket.Size = new System.Drawing.Size(553, 298);
            this.dataGridViewPaket.TabIndex = 10;
            this.dataGridViewPaket.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPaket_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 27);
            this.label1.TabIndex = 9;
            this.label1.Text = "Riwayat Pengiriman";
            // 
            // btnKirimPaket
            // 
            this.btnKirimPaket.Location = new System.Drawing.Point(525, 3);
            this.btnKirimPaket.Name = "btnKirimPaket";
            this.btnKirimPaket.Size = new System.Drawing.Size(97, 23);
            this.btnKirimPaket.TabIndex = 11;
            this.btnKirimPaket.Text = "Buat Pengiriman";
            this.btnKirimPaket.UseVisualStyleBackColor = true;
            this.btnKirimPaket.Click += new System.EventHandler(this.btnKirimPaket_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // DPHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnKirimPaket);
            this.Controls.Add(this.dataGridViewPaket);
            this.Controls.Add(this.label1);
            this.Name = "DPHome";
            this.Size = new System.Drawing.Size(625, 422);
            this.Load += new System.EventHandler(this.DPHome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPaket)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewPaket;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnKirimPaket;
        private System.Windows.Forms.Button btnRefresh;
    }
}
