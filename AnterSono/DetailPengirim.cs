using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnterSono
{
    public partial class DetailPengirim : Form
    {
        private int idPengirim;
        public DetailPengirim(int idPengirim)
        {
            InitializeComponent();
            this.idPengirim = idPengirim;
        }

        private void DetailPengirim_Load(object sender, EventArgs e)
        {
            LoadDataPengirim(idPengirim);
        }

        private void LoadDataPengirim(int pengirimId)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string queryPengirim = "SELECT nama_pengirim, email, no_hp_pengirim, alamat_pengirim FROM pengirim WHERE id_pengirim = @id";
                using (SqlCommand cmd = new SqlCommand(queryPengirim, conn))
                {
                    cmd.Parameters.AddWithValue("@id", pengirimId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNama.Text = reader["nama_pengirim"].ToString();
                            txtEmail.Text = reader["email"].ToString();
                            txtNoHp.Text = reader["no_hp_pengirim"].ToString();
                            txtAlamat.Text = reader["alamat_pengirim"].ToString();
                        }
                    }
                }

                string queryRiwayat = @"SELECT resi, nama_barang, status_pengiriman, created_at 
                                FROM paket WHERE id_pengirim = @id ORDER BY created_at DESC";
                using (SqlCommand cmd = new SqlCommand(queryRiwayat, conn))
                {
                    cmd.Parameters.AddWithValue("@id", pengirimId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DetailPaket detailPaket = new DetailPaket(dataGridView1.Rows[e.RowIndex].Cells["resi"].Value.ToString(), -1);
            detailPaket.ShowDialog();
        }
    }
}
