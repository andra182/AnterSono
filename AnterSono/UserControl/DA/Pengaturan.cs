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
    public partial class Pengaturan : UserControl
    {
        public Pengaturan()
        {
            InitializeComponent();
        }

        private void Pengaturan_Load(object sender, EventArgs e)
        {
            LoadDataTarif();
        }

        private void LoadDataTarif()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"
            SELECT 
                jenis_layanan,
                berat_max,
                harga
            FROM tarif";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string layanan = reader["jenis_layanan"].ToString().ToLower();
                            string berat = reader["berat_max"]?.ToString() ?? "";
                            string harga = reader["harga"]?.ToString() ?? "";

                            switch (layanan)
                            {
                                case "regular":
                                    txtRegKG.Text = berat;
                                    txtRegKM.Text = harga;
                                    break;
                                case "express":
                                    txtExpKG.Text = berat;
                                    txtExpKM.Text = harga;
                                    break;
                                case "sameday":
                                    txtSDKG.Text = berat;
                                    txtSDKM.Text = harga;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtRegKG.Text) || string.IsNullOrWhiteSpace(txtRegKM.Text) ||
                string.IsNullOrWhiteSpace(txtExpKG.Text) || string.IsNullOrWhiteSpace(txtExpKM.Text) ||
                string.IsNullOrWhiteSpace(txtSDKG.Text) || string.IsNullOrWhiteSpace(txtSDKM.Text))
            {
                MessageBox.Show("Semua field harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtRegKG.Text, out _) ||
                !decimal.TryParse(txtRegKM.Text, out _) ||
                !decimal.TryParse(txtExpKG.Text, out _) ||
                !decimal.TryParse(txtExpKM.Text, out _) ||
                !decimal.TryParse(txtSDKG.Text, out _) ||
                !decimal.TryParse(txtSDKM.Text, out _))
            {
                MessageBox.Show("Harap masukkan nilai numerik yang valid untuk berat dan harga.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                UpdateTarif(conn, "regular", txtRegKG.Text, txtRegKM.Text);
                UpdateTarif(conn, "express", txtExpKG.Text, txtExpKM.Text);
                UpdateTarif(conn, "sameday", txtSDKG.Text, txtSDKM.Text);

                MessageBox.Show("Data tarif berhasil disimpan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateTarif(SqlConnection conn, string jenis, string berat, string harga)
        {
            string updateQuery = "UPDATE tarif SET berat_max = @berat, harga = @harga WHERE jenis_layanan = @jenis";
            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
            {
                updateCmd.Parameters.AddWithValue("@berat", decimal.TryParse(berat, out var b) ? b : 0);
                updateCmd.Parameters.AddWithValue("@harga", decimal.TryParse(harga, out var h) ? h : 0);
                updateCmd.Parameters.AddWithValue("@jenis", jenis);
                updateCmd.ExecuteNonQuery();
            }
        }

    }
}
