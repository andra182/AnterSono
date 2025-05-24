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

    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("Apakah anda yakin?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //{
            //    Environment.Exit(1);
            //}
            //else 
            //{
            //    e.Cancel = true;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Environment.Exit(1);    
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string input = txtLogin.Text.Trim();
            string passwordHash = SecurityHelper.HashPassword(txtPassword.Text);
            string role = "";
            string nama = "";

            using (SqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();

                    string queryAdmin = @"SELECT nama_admin FROM admin WHERE (email = @input OR no_hp_admin = @input) AND password = @password";
                    using (SqlCommand cmd = new SqlCommand(queryAdmin, conn))
                    {
                        cmd.Parameters.AddWithValue("@input", input);
                        cmd.Parameters.AddWithValue("@password", passwordHash);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            role = "admin";
                            nama = result.ToString();
                        }
                    }

                    if (role == "")
                    {
                        string queryKurir = @"SELECT nama_kurir FROM kurir WHERE (email = @input OR no_hp_kurir = @input) AND password = @password";
                        using (SqlCommand cmd = new SqlCommand(queryKurir, conn))
                        {
                            cmd.Parameters.AddWithValue("@input", input);
                            cmd.Parameters.AddWithValue("@password", passwordHash);
                            var result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                role = "kurir";
                                nama = result.ToString();
                            }
                        }
                    }

                    if (role == "")
                    {
                        string queryPengirim = @"SELECT nama_pengirim FROM pengirim WHERE (email = @input OR no_hp_pengirim = @input) AND password = @password";
                        using (SqlCommand cmd = new SqlCommand(queryPengirim, conn))
                        {
                            cmd.Parameters.AddWithValue("@input", input);
                            cmd.Parameters.AddWithValue("@password", passwordHash);
                            var result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                role = "pengirim";
                                nama = result.ToString();
                            }
                        }
                    }

                    if (role != "")
                    {
                        MessageBox.Show($"Login berhasil sebagai {role.ToUpper()}!\nSelamat datang, {nama}.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (role == "admin")
                            new DashboardAdmin().Show();
                        else if (role == "kurir")
                            new DashboardKurir().Show();
                        else if (role == "pengirim")
                            new DashboardPengirim().Show();

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login gagal! Email/No HP atau password salah.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }
    }
}
