using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace AnterSono
{
    public partial class DetailPaket : Form
    {
        private string resi;
        private int idKurir;

        public DetailPaket(string resi, int idKurir = 0)
        {
            InitializeComponent();
            this.resi = resi;
            this.idKurir = idKurir;
        }

        private void DetailPaket_Load(object sender, EventArgs e)
        {
            if (idKurir < 0)
            {

                btnBatalkanPaket.Visible = true;
            }
            else
            {
                btnBatalkanPaket.Visible = false;
            }

            if (idKurir > 0)
            {
                btnAmbilTugasPaket.Visible = true;
            }
            else
            {
                btnAmbilTugasPaket.Visible = false;
            }

            LoadDataPaket();
        }

        private void LoadDataPaket()
        {
            cmbStatusPengiriman.Items.Clear();
            cmbStatusPengiriman.Items.Add("Menunggu Pickup");
            cmbStatusPengiriman.Items.Add("Dalam Perjalanan");
            cmbStatusPengiriman.Items.Add("Sudah Diterima");
            cmbStatusPengiriman.Items.Add("Dibatalkan");
            cmbStatusPengiriman.Items.Add("Gagal Dikirim");

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"
                    SELECT p.*, 
                           pg.nama_pengirim, pg.no_hp_pengirim, pg.alamat_pengirim,
                           k.id_kurir, k.nama_kurir, k.no_hp_kurir
                    FROM paket p
                    LEFT JOIN pengirim pg ON p.id_pengirim = pg.id_pengirim
                    LEFT JOIN kurir k ON p.id_kurir = k.id_kurir
                    WHERE p.resi = @resi";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@resi", resi);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtResi.Text = reader["resi"].ToString();
                            txtNamaBarang.Text = reader["nama_barang"].ToString();
                            txtBerat.Value = reader["berat"] != DBNull.Value ? Convert.ToInt32(reader["berat"]) : 0;
                            txtTipePengiriman.Text = reader["tipe_pengiriman"].ToString();
                            txtJarak.Value = reader["jarak"] != DBNull.Value ? Convert.ToInt32(reader["jarak"]) : 0;
                            txtHargaTotal.Text = string.Format("Rp{0:N0}", Convert.ToDecimal(reader["harga_total"]));
                            cmbStatusPengiriman.SelectedItem = reader["status_pengiriman"].ToString();

                            if (reader["id_kurir"] != DBNull.Value && Convert.ToInt32(reader["id_kurir"]) == idKurir) 
                                cmbStatusPengiriman.Enabled = true;

                            txtCreatedAt.Text = Convert.ToDateTime(reader["created_at"]).ToString("dd MMM yyyy HH:mm");

                            txtPengirim.Text = reader["nama_pengirim"].ToString();
                            txtNoHPPengirim.Text = reader["no_hp_pengirim"].ToString();
                            txtAlamatAsal.Text = reader["alamat_asal"].ToString();

                            txtNamaPenerima.Text = reader["nama_penerima"].ToString();
                            txtNoHPPenerima.Text = reader["no_hp_penerima"].ToString();
                            txtAlamatTujuan.Text = reader["alamat_tujuan"].ToString();

                            txtMetodePembayaran.Text = reader["metode_pembayaran"].ToString();
                            txtHargaTotal.Text = reader["harga_total"].ToString();

                            if (reader["nama_kurir"] != DBNull.Value)
                            {
                                txtNamaKurir.Text = reader["nama_kurir"].ToString();
                                txtNoHPKurir.Text = reader["no_hp_kurir"].ToString();
                            }
                            else
                            {
                                txtNamaKurir.Text = "Belum ditugaskan";
                                txtNoHPKurir.Text = "-";
                            }

                            if (reader["status_pengiriman"].ToString() == "Menunggu Pickup" && idKurir == -2)
                            {
                                btnBatalkanPaket.Visible = true;
                            } else
                            {
                                btnBatalkanPaket.Visible = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Data paket tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                }
            }
        }

        private void cmbStatusPengiriman_GantiStatus(object sender, EventArgs e)
        {
            if (idKurir == 0 || idKurir == -2) return;

            string newStatus = cmbStatusPengiriman.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(newStatus)) return;

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "UPDATE paket SET status_pengiriman = @status WHERE resi = @resi";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@resi", resi);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Status berhasil diperbarui.", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Gagal memperbarui status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBatalkanPaket_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Yakin ingin membatalkan pengiriman ini?",
                "Konfirmasi Pembatalan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    string query = "UPDATE paket SET status_pengiriman = @status WHERE resi = @resi";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", "Dibatalkan");
                        cmd.Parameters.AddWithValue("@resi", resi);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Paket berhasil dibatalkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); 
                        }
                        else
                        {
                            MessageBox.Show("Gagal membatalkan paket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }


        private void btnAmbilTugasPaket_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Ambil tugas pengiriman ini?",
                "Konfirmasi Ambil Tugas",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    string query = @"
                UPDATE paket 
                SET id_kurir = @idKurir 
                WHERE resi = @resi";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idKurir", idKurir);
                        cmd.Parameters.AddWithValue("@resi", resi);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Tugas berhasil diambil.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Gagal mengambil tugas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

    }
}
