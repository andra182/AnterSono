namespace AnterSono
{
    partial class DAHome
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewPaket = new System.Windows.Forms.DataGridView();
            this.dataGridViewPaketNoPickup = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPaket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPaketNoPickup)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pengiriman";
            // 
            // dataGridViewPaket
            // 
            this.dataGridViewPaket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPaket.Location = new System.Drawing.Point(36, 67);
            this.dataGridViewPaket.Name = "dataGridViewPaket";
            this.dataGridViewPaket.Size = new System.Drawing.Size(553, 152);
            this.dataGridViewPaket.TabIndex = 2;
            this.dataGridViewPaket.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPaket_CellClick);
            this.dataGridViewPaket.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPaket_CellClick);
            this.dataGridViewPaket.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPaket_CellClick);
            // 
            // dataGridViewPaketNoPickup
            // 
            this.dataGridViewPaketNoPickup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPaketNoPickup.Location = new System.Drawing.Point(36, 269);
            this.dataGridViewPaketNoPickup.Name = "dataGridViewPaketNoPickup";
            this.dataGridViewPaketNoPickup.Size = new System.Drawing.Size(553, 126);
            this.dataGridViewPaketNoPickup.TabIndex = 3;
            this.dataGridViewPaketNoPickup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPaket_CellClick);
            this.dataGridViewPaketNoPickup.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPaket_CellClick);
            this.dataGridViewPaketNoPickup.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPaket_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 27);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pengiriman (Belum Di-Pickup)";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // DAHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewPaketNoPickup);
            this.Controls.Add(this.dataGridViewPaket);
            this.Controls.Add(this.label1);
            this.Name = "DAHome";
            this.Size = new System.Drawing.Size(625, 422);
            this.Load += new System.EventHandler(this.DAHome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPaket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPaketNoPickup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewPaket;
        private System.Windows.Forms.DataGridView dataGridViewPaketNoPickup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRefresh;
    }
}
