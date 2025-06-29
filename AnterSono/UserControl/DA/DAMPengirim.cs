﻿using System;
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
    public partial class DAMPengirim : UserControl
    {
        public DAMPengirim()
        {
            InitializeComponent();
        }

        private void DAMPengirim_Load(object sender, EventArgs e)
        {
            LoadDataPengirim();
        }

        private void LoadDataPengirim()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"
                            SELECT 
                                id_pengirim AS [ID Pengirim],
                                nama_pengirim AS [Nama Pengirim],
                                email AS [Email Pengirim],
                                no_hp_pengirim AS [No HP Pengirim],
                                alamat_pengirim AS [Alamat Pengirim]
                            FROM pengirim";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DetailPengirim detailPengirim = new DetailPengirim(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID Pengirim"].Value));
            detailPengirim.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataPengirim();
        }
    }
}
