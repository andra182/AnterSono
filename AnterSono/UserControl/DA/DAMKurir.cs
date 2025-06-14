using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;


namespace AnterSono
{
    public partial class DAMKurir : UserControl
    {
        private PrintDocument printDocument = new PrintDocument();
        private DataTable kurirData;
        private int currentRow = 0;

        public DAMKurir()
        {
            InitializeComponent();
        }

        private void btnAddKurir_Click(object sender, EventArgs e)
        {
            FormKurir formKurir = new FormKurir();
            formKurir.Show();
        }

        private void LoadDataKurir()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"
                   SELECT 
                    k.id_kurir AS [ID Kurir],
                    k.foto_profil AS [Foto Profil],
                    k.nama_kurir AS [Nama Kurir],
                    k.email AS [Email Kurir],
                    k.no_hp_kurir AS [No HP Kurir],
                    (
                        SELECT COUNT(*) 
                        FROM paket p 
                        WHERE p.id_kurir = k.id_kurir
                    ) AS [Total Pengiriman]
                    FROM kurir k";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.RowTemplate.Height = 75;
                    dataGridView1.DataSource = dt;
                    DataGridViewImageColumn pic1 = new DataGridViewImageColumn();
                    pic1 = (DataGridViewImageColumn)dataGridView1.Columns["Foto Profil"];
                    pic1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    kurirData = dt;
                }
            }
        }

        private void DAMKurir_Load(object sender, EventArgs e)
        {
            LoadDataKurir();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DetailKurir detailKurir = new DetailKurir(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID Kurir"].Value));
            detailKurir.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataKurir();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            previewDialog.Document = printDocument;
            previewDialog.ShowDialog();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 10);
            int y = 100;
            int x = 50;
            int rowHeight = 30;

            e.Graphics.DrawString("Data Kurir", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, x, y - 40);

            e.Graphics.DrawString("ID", font, Brushes.Black, x, y);
            e.Graphics.DrawString("Nama", font, Brushes.Black, x + 50, y);
            e.Graphics.DrawString("Email", font, Brushes.Black, x + 200, y);
            e.Graphics.DrawString("No HP", font, Brushes.Black, x + 400, y);
            e.Graphics.DrawString("Total Pengiriman", font, Brushes.Black, x + 550, y);

            y += rowHeight;

            while (currentRow < kurirData.Rows.Count)
            {
                DataRow row = kurirData.Rows[currentRow];

                e.Graphics.DrawString(row["ID Kurir"].ToString(), font, Brushes.Black, x, y);
                e.Graphics.DrawString(row["Nama Kurir"].ToString(), font, Brushes.Black, x + 50, y);
                e.Graphics.DrawString(row["Email Kurir"].ToString(), font, Brushes.Black, x + 200, y);
                e.Graphics.DrawString(row["No HP Kurir"].ToString(), font, Brushes.Black, x + 400, y);
                e.Graphics.DrawString(row["Total Pengiriman"].ToString(), font, Brushes.Black, x + 550, y);

                y += rowHeight;
                currentRow++;

                if (y > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            currentRow = 0;
            e.HasMorePages = false;
        }
    }
}
