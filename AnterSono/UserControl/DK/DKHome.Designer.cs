namespace AnterSono
{
    partial class DKHome
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
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewPaketNoPickup = new System.Windows.Forms.DataGridView();
            this.dataGridViewTugasPengiriman = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPaketNoPickup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTugasPengiriman)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 27);
            this.label2.TabIndex = 8;
            this.label2.Text = "Pengiriman (Belum Di-Pickup)";
            // 
            // dataGridViewPaketNoPickup
            // 
            this.dataGridViewPaketNoPickup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPaketNoPickup.Location = new System.Drawing.Point(38, 269);
            this.dataGridViewPaketNoPickup.Name = "dataGridViewPaketNoPickup";
            this.dataGridViewPaketNoPickup.Size = new System.Drawing.Size(553, 126);
            this.dataGridViewPaketNoPickup.TabIndex = 7;
            this.dataGridViewPaketNoPickup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPaketNoPickup_CellClick);
            this.dataGridViewPaketNoPickup.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPaketNoPickup_CellClick);
            this.dataGridViewPaketNoPickup.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPaketNoPickup_CellClick);
            // 
            // dataGridViewTugasPengiriman
            // 
            this.dataGridViewTugasPengiriman.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTugasPengiriman.Location = new System.Drawing.Point(38, 65);
            this.dataGridViewTugasPengiriman.Name = "dataGridViewTugasPengiriman";
            this.dataGridViewTugasPengiriman.Size = new System.Drawing.Size(553, 152);
            this.dataGridViewTugasPengiriman.TabIndex = 6;
            this.dataGridViewTugasPengiriman.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTugasPengiriman_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tugas Pengiriman";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // DKHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewPaketNoPickup);
            this.Controls.Add(this.dataGridViewTugasPengiriman);
            this.Controls.Add(this.label1);
            this.Name = "DKHome";
            this.Size = new System.Drawing.Size(625, 422);
            this.Load += new System.EventHandler(this.DKHome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPaketNoPickup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTugasPengiriman)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewPaketNoPickup;
        private System.Windows.Forms.DataGridView dataGridViewTugasPengiriman;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
    }
}
