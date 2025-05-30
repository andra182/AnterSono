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
    public partial class DetailKurir : Form
    {
        private int idKurir;
        public DetailKurir(int idKurir)
        {
            InitializeComponent();
            btnSave.Enabled = false;
            btnUpload.Enabled = false;
            this.idKurir = idKurir;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Apakah Anda yakin ingin menghapus data kurir ini? Tindakan ini tidak dapat dibatalkan.",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM kurir WHERE id_kurir = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idKurir);
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Data kurir berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Data kurir gagal dihapus.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnWantUpdate_Click(object sender, EventArgs e)
        {
            if (!btnSave.Enabled)
            {
                btnWantUpdate.Text = "Batalkan Ubah";
                btnSave.Enabled = true;
                btnUpload.Enabled = true;
                txtNama.Enabled = true;
                txtNoHp.Enabled = true;
                txtEmail.Enabled = true;
            }
            else
            {
                btnWantUpdate.Text = "Ubah Data Kurir";
                btnSave.Enabled = false;
                btnUpload.Enabled = false;
                txtNama.Enabled = false;
                txtNoHp.Enabled = false;
                txtEmail.Enabled = false;
            }
        }

        private void DetailKurir_Load(object sender, EventArgs e)
        {
            LoadDataKurir(idKurir);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE kurir SET 
                            nama_kurir = @nama, 
                            email = @email, 
                            no_hp_kurir = @nohp, 
                            foto_profil = @foto
                         WHERE id_kurir = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@nohp", txtNoHp.Text);
                    cmd.Parameters.AddWithValue("@id", idKurir);

                    if (pictureBox1.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            cmd.Parameters.AddWithValue("@foto", ms.ToArray());
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@foto", DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data kurir berhasil diupdate.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void LoadDataKurir(int kurirId)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string queryKurir = "SELECT nama_kurir, email, no_hp_kurir, foto_profil FROM kurir WHERE id_kurir = @id";
                using (SqlCommand cmd = new SqlCommand(queryKurir, conn))
                {
                    cmd.Parameters.AddWithValue("@id", kurirId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNama.Text = reader["nama_kurir"].ToString();
                            txtEmail.Text = reader["email"].ToString();
                            txtNoHp.Text = reader["no_hp_kurir"].ToString();

                            if (reader["foto_profil"] != DBNull.Value)
                            {
                                byte[] imgBytes = (byte[])reader["foto_profil"];
                                using (MemoryStream ms = new MemoryStream(imgBytes))
                                {
                                    pictureBox1.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                pictureBox1.Image = null;
                            }
                        }
                    }
                }

                string queryRiwayat = @"SELECT resi, nama_barang, status_pengiriman, created_at 
                                FROM paket WHERE id_kurir = @id ORDER BY created_at DESC";
                using (SqlCommand cmd = new SqlCommand(queryRiwayat, conn))
                {
                    cmd.Parameters.AddWithValue("@id", kurirId);
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
