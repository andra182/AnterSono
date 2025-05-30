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

namespace AnterSono
{
    public partial class DPHome : UserControl
    {
        private int userId;
        public DPHome(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void btnKirimPaket_Click(object sender, EventArgs e)
        {
            FormPaket formPaket = new FormPaket(userId);
            formPaket.ShowDialog();
        }

        private void LoadDataPaket()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"
            SELECT 
                resi AS [Resi],
                nama_barang AS [Barang],
                berat AS [Berat (Kg)],
                tipe_pengiriman AS [Tipe],
                jarak AS [Jarak (Km)],
                harga_total AS [Total Harga],
                status_pengiriman AS [Status],
                created_at AS [Dibuat Pada]
            FROM paket
            WHERE id_pengirim = @userId
            ORDER BY created_at DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewPaket.DataSource = dt;

                    if (dataGridViewPaket.Columns.Contains("Total Harga"))
                    {
                        dataGridViewPaket.Columns["Total Harga"].DefaultCellStyle.Format = "N0";
                    }

                    if (dataGridViewPaket.Columns.Contains("Dibuat Pada"))
                    {
                        dataGridViewPaket.Columns["Dibuat Pada"].DefaultCellStyle.Format = "dd MMM yyyy HH:mm";
                    }

                }
            }
        }

        private void DPHome_Load(object sender, EventArgs e)
        {
            LoadDataPaket();
        }

        private void dataGridViewPaket_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DetailPaket detailPaket = new DetailPaket(dataGridViewPaket.Rows[e.RowIndex].Cells["Resi"].Value.ToString(), -2);
            detailPaket.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataPaket();
        }
    }
}
