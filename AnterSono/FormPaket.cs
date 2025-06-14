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
    public partial class FormPaket : Form
    {
        int userId;
        string jenisLayanan;
        int hargaPerKm;
        int maxBerat;
        public FormPaket(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public int HargaPerKm { get; set; }
            public int MaxBerat { get; set; }

            public ComboBoxItem(string text, string value, int hargaPerKm, int maxBerat)
            {
                Text = text;
                Value = value;
                HargaPerKm = hargaPerKm;
                MaxBerat = maxBerat;
            }

            public override string ToString()
            {
                return Text;
            }
        }


        private void FormPaket_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM tarif;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string jenis = reader["jenis_layanan"].ToString();
                            int harga = Convert.ToInt32(reader["harga"]);
                            int max = Convert.ToInt32(reader["berat_max"]);

                            string text = $"{ToTitle(jenis)} (Rp{harga:N0}/Km, Max {max}Gr)";
                            cmbTipe.Items.Add(new ComboBoxItem(text, jenis, harga, max));
                        }
                    }
                }
            }

            cmbTipe.DisplayMember = "Text";
            cmbTipe.ValueMember = "Value";
        }

        private string ToTitle(string input)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        private void btnBayar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaBarang.Text) ||
                string.IsNullOrWhiteSpace(txtBerat.Text) ||
                string.IsNullOrWhiteSpace(txtNamaPenerima.Text) ||
                string.IsNullOrWhiteSpace(txtNoHP.Text) ||
                string.IsNullOrWhiteSpace(txtAlamat.Text) ||
                string.IsNullOrWhiteSpace(txtJarak.Text) ||
                cmbTipe.SelectedItem == null)
            {
                MessageBox.Show("Harap isi semua kolom!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtBerat.Text, out int berat) || !int.TryParse(txtJarak.Text, out int jarak))
            {
                MessageBox.Show("Berat dan Jarak harus berupa angka!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedItem = (ComboBoxItem)cmbTipe.SelectedItem;

            if (berat > selectedItem.MaxBerat)
            {
                MessageBox.Show($"Berat melebihi maksimum untuk tipe {selectedItem.Value.ToUpper()} (Max {selectedItem.MaxBerat} Gr)", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string namaBarang = txtNamaBarang.Text;
            string namaPenerima = txtNamaPenerima.Text;
            string noHP = txtNoHP.Text;
            string alamat = txtAlamat.Text;
            string tipe = selectedItem.Value;

            KonfirmasiPembayaran bayarForm = new KonfirmasiPembayaran(namaBarang, berat, namaPenerima, noHP, alamat, jarak, tipe, userId, selectedItem.HargaPerKm);
            bayarForm.ShowDialog();
            this.Hide();
        }


    }
}
