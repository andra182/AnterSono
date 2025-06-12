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
        private int userId;

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
                            this.userId = reader["id_pengirim"] != DBNull.Value ? Convert.ToInt32(reader["id_pengirim"]) : 0;

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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // 1. Konstruksi model Paket
            var paket = new Paket
            {
                Resi = txtResi.Text,
                IdPengirim = this.userId, // atau field lain tempat kamu simpan ID pengirim
                NamaPenerima = txtNamaPenerima.Text,
                NoHpPenerima = txtNoHPPenerima.Text,
                NamaBarang = txtNamaBarang.Text,
                Berat = txtBerat.Value / 1000m, // asumsikan txtBerat dalam gram
                AlamatAsal = txtAlamatAsal.Text,
                AlamatTujuan = txtAlamatTujuan.Text,
                Jarak = (int)txtJarak.Value,
                NamaPengirim = txtPengirim.Text,
                NoHpPengirim = txtNoHPPengirim.Text,
                HargaTotal = decimal.Parse(txtHargaTotal.Text, System.Globalization.NumberStyles.Currency),
                MetodePembayaran = txtMetodePembayaran.Text == "COD" ? "COD" : "Non-COD",
                TipePengiriman = txtTipePengiriman.Text,
                CreatedAt = DateTime.ParseExact(
                                      txtCreatedAt.Text,
                                      "dd MMM yyyy HH:mm",
                                      System.Globalization.CultureInfo.InvariantCulture)
            };

            // 2. Ambil logo dari Resources
            byte[] logoBytes;
            try
            {
                logoBytes = GetLogoBytes();
            }
            catch
            {
                MessageBox.Show("Gagal memuat logo dari Resources.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Tampilkan SaveFileDialog
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF file|*.pdf";
                sfd.FileName = $"RESI_{paket.Resi}.pdf";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    // 4. Generate PDF
                    PdfResiGenerator.Generate(paket, logoBytes, sfd.FileName);
                    MessageBox.Show($"PDF resi berhasil disimpan:\n{sfd.FileName}",
                                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal generate PDF:\n{ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private byte[] GetLogoBytes()
        {
            // Langsung return resource byte[] tanpa Save
            return Properties.Resources.AnterSonoLogo;
        }
    }

}

