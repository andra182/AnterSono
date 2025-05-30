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
    public partial class DAMKurir : UserControl
    {
        public DAMKurir()
        {
            InitializeComponent();
        }

        private void btnAddKurir_Click(object sender, EventArgs e)
        {
            FormKurir formKurir = new FormKurir();
            formKurir.Show();
        }

        private void LoadDataKurir()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"
            SELECT 
                id_kurir AS [ID Kurir],
                foto_profil AS [Foto Profil],
                nama_kurir AS [Nama Kurir],
                email AS [Email Kurir],
                no_hp_kurir AS [No HP Kurir]
            FROM kurir";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.RowTemplate.Height = 75;
                    dataGridView1.DataSource = dt;
                    DataGridViewImageColumn pic1 = new DataGridViewImageColumn();
                    pic1 = (DataGridViewImageColumn)dataGridView1.Columns["Foto Profil"];
                    pic1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }
            }
        }

        private void DAMKurir_Load(object sender, EventArgs e)
        {
            LoadDataKurir();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DetailKurir detailKurir = new DetailKurir(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID Kurir"].Value));
            detailKurir.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataKurir();
        }
    }
}
