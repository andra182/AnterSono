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
    public partial class FormKurir : Form
    {
        public FormKurir()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtNoHp.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                pictureBox1.Image == null)
            {
                MessageBox.Show("Harap isi semua kolom!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nama = txtNama.Text;
            string email = txtEmail.Text;
            string noHp = txtNoHp.Text;
            string passwordPlain = txtPassword.Text;
            string passwordHash = SecurityHelper.HashPassword(passwordPlain);

            using (SqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO kurir 
                                (nama_kurir, no_hp_kurir, email, password, foto_profil)
                                 VALUES (@nama, @nohp, @email, @password, @foto)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@nohp", noHp);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", passwordHash);

                        MemoryStream stream = new MemoryStream();
                        pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                        cmd.Parameters.AddWithValue("@foto", stream.ToArray());

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Registrasi gagal.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        private void ClearForm()
        {
            txtNama.Clear();
            txtEmail.Clear();
            txtNoHp.Clear();
            txtPassword.Clear();
            pictureBox1.Image = null;
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
    }
}
