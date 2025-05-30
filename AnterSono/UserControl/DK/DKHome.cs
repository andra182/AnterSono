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
    public partial class DKHome : UserControl
    {
        private int idKurir;
        public DKHome(int idKurir)
        {
            InitializeComponent();
            this.idKurir = idKurir;
        }

        private void DKHome_Load(object sender, EventArgs e)
        {
            LoadDataPaketNoPickup();
            LoadDataTugasPengiriman();
        }

        private void LoadDataPaketNoPickup()
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
            WHERE status_pengiriman = 'Menunggu Pickup'
            ORDER BY created_at DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewPaketNoPickup.DataSource = dt;

                    if (dataGridViewPaketNoPickup.Columns.Contains("Total Harga"))
                    {
                        dataGridViewPaketNoPickup.Columns["Total Harga"].DefaultCellStyle.Format = "N0";
                    }

                    if (dataGridViewPaketNoPickup.Columns.Contains("Dibuat Pada"))
                    {
                        dataGridViewPaketNoPickup.Columns["Dibuat Pada"].DefaultCellStyle.Format = "dd MMM yyyy HH:mm";
                    }

                }
            }
        }

        private void LoadDataTugasPengiriman()
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
            WHERE id_kurir = @idKurir
            ORDER BY created_at DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idKurir", idKurir);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewTugasPengiriman.DataSource = dt;


                    if (dataGridViewTugasPengiriman.Columns.Contains("Total Harga"))
                    {
                        dataGridViewTugasPengiriman.Columns["Total Harga"].DefaultCellStyle.Format = "N0";
                    }

                    if (dataGridViewTugasPengiriman.Columns.Contains("Dibuat Pada"))
                    {
                        dataGridViewTugasPengiriman.Columns["Dibuat Pada"].DefaultCellStyle.Format = "dd MMM yyyy HH:mm";
                    }

                }
            }
        }

        private void dataGridViewPaketNoPickup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DetailPaket detailPaket = new DetailPaket(dataGridViewPaketNoPickup.Rows[e.RowIndex].Cells["Resi"].Value.ToString(), idKurir);
            detailPaket.Show();
        }

        private void dataGridViewTugasPengiriman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DetailPaket detailPaket = new DetailPaket(dataGridViewTugasPengiriman.Rows[e.RowIndex].Cells["Resi"].Value.ToString(), idKurir);
            detailPaket.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataPaketNoPickup();
            LoadDataTugasPengiriman();
        }
    }
}
