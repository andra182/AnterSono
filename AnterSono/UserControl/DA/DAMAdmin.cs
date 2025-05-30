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
    public partial class DAMAdmin : UserControl
    {
        public DAMAdmin()
        {
            InitializeComponent();
        }

        private void btnAddAdmin_Click(object sender, EventArgs e)
        {
            FormAdmin formAdmin = new FormAdmin();
            formAdmin.ShowDialog();
        }

        private void LoadDataAdmin()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"
            SELECT 
                nama_admin AS [Nama Admin],
                email AS [Email Admin],
                no_hp_admin AS [No HP Admin]
            FROM admin";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void DAMAdmin_Load(object sender, EventArgs e)
        {
            LoadDataAdmin();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataAdmin();
        }
    }
}
