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
    public partial class KonfirmasiPembayaran : Form
    {
        private int userId;
        private string namaBarang;
        private int berat;
        private string namaPenerima;
        private string noHP;
        private string alamatTujuan;
        private int jarak;
        private string tipe;
        private int hargaPerKm ;
        private decimal totalHarga ;

        public KonfirmasiPembayaran(string namaBarang, int berat, string namaPenerima, string noHP, string alamat, int jarak, string tipe, int userId, int hargaPerKm)
        {
            InitializeComponent();

            this.userId = userId;
            this.namaBarang = namaBarang;
            this.berat = berat;
            this.namaPenerima = namaPenerima;
            this.noHP = noHP;
            this.alamatTujuan = alamat;
            this.jarak = jarak;
            this.tipe = tipe;
            this.hargaPerKm = hargaPerKm;

            this.totalHarga = (hargaPerKm * jarak) + 1500;
            lblHarga.Text = $"Total Harga: Rp {totalHarga:N0}";
        }

        private void createPaket(string metodePembayaran)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                SqlCommand cmdPengirim = new SqlCommand("SELECT alamat_pengirim FROM pengirim WHERE id_pengirim = @id", conn);
                cmdPengirim.Parameters.AddWithValue("@id", userId);
                var result = cmdPengirim.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("Data pengirim tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string alamatAsal = result.ToString();

                string prefix;
                switch (tipe)
                {
                    case "regular":
                        prefix = "REG";
                        break;
                    case "express":
                        prefix = "EXP";
                        break;
                    case "sameday":
                        prefix = "SD";
                        break;
                    default:
                        prefix = "";
                        break;
                }

                Random rnd = new Random();
                string resi = prefix + rnd.Next(100000, 999999);

                SqlCommand insertCmd = new SqlCommand(@"
            INSERT INTO paket (resi, id_pengirim, nama_penerima, no_hp_penerima, id_kurir, nama_barang, berat,
                               alamat_asal, alamat_tujuan, tipe_pengiriman, jarak, harga_total, metode_pembayaran, status_pengiriman)
            VALUES (@resi, @id_pengirim, @nama_penerima, @no_hp, NULL, @nama_barang, @berat,
                    @alamat_asal, @alamat_tujuan, @tipe, @jarak, @harga_total, @metode, @status)
        ", conn);
                insertCmd.Parameters.AddWithValue("@resi", resi);
                insertCmd.Parameters.AddWithValue("@id_pengirim", userId);
                insertCmd.Parameters.AddWithValue("@nama_penerima", namaPenerima);
                insertCmd.Parameters.AddWithValue("@no_hp", noHP);
                insertCmd.Parameters.AddWithValue("@nama_barang", namaBarang);
                insertCmd.Parameters.AddWithValue("@berat", berat);
                insertCmd.Parameters.AddWithValue("@alamat_asal", alamatAsal);
                insertCmd.Parameters.AddWithValue("@alamat_tujuan", alamatTujuan);
                insertCmd.Parameters.AddWithValue("@tipe", tipe);
                insertCmd.Parameters.AddWithValue("@jarak", jarak);
                insertCmd.Parameters.AddWithValue("@harga_total", totalHarga);
                insertCmd.Parameters.AddWithValue("@metode", metodePembayaran);
                insertCmd.Parameters.AddWithValue("@status", "Menunggu Pickup");

                insertCmd.ExecuteNonQuery();

                MessageBox.Show($"Paket berhasil dibuat!\nResi Anda: {resi}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void mtdDana_Click(object sender, EventArgs e)
        {
            createPaket("DANA");
        }

        private void mtdIndomaret_Click(object sender, EventArgs e)
        {
            createPaket("Indomaret");
        }

        private void mtdSpay_Click(object sender, EventArgs e)
        {
            createPaket("ShopeePay");
        }

        private void mtdGopay_Click(object sender, EventArgs e)
        {
            createPaket("Gopay");
        }

        private void mtdCod_Click(object sender, EventArgs e)
        {
            createPaket("COD");
        }
    }
}