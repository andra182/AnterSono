using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnterSono
{
    public partial class DashboardAdmin : Form
    {
        public DashboardAdmin(bool masteradmin = false)
        {
            InitializeComponent();
            DAHome home = new DAHome();
            addUserControl(home);
            if (masteradmin)
            {
                panel6.Visible = true;
            } else
            {
                panel6.Visible = false;
            }
        }

        private void manajemenKurirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            content.Controls.Clear();
            content.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(0, 90, 192);
            panel3.BackColor = Color.FromArgb(6, 122, 255);
            panel4.BackColor = Color.FromArgb(6, 122, 255);
            panel5.BackColor = Color.FromArgb(6, 122, 255);
            panel6.BackColor = Color.FromArgb(6, 122, 255);
            DAHome home = new DAHome();
            addUserControl(home);
        }
        
        private void MKurir_Click(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(0, 90, 192);
            panel2.BackColor = Color.FromArgb(6, 122, 255);
            panel4.BackColor = Color.FromArgb(6, 122, 255);
            panel5.BackColor = Color.FromArgb(6, 122, 255);
            panel6.BackColor = Color.FromArgb(6, 122, 255);
            DAMKurir kurir = new DAMKurir();
            addUserControl(kurir);
        }
        
        private void MPengirim_Click(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(0, 90, 192);
            panel3.BackColor = Color.FromArgb(6, 122, 255);
            panel2.BackColor = Color.FromArgb(6, 122, 255);
            panel5.BackColor = Color.FromArgb(6, 122, 255);
            panel6.BackColor = Color.FromArgb(6, 122, 255);
            DAMPengirim pengirim = new DAMPengirim();
            addUserControl(pengirim);
        }
        
        private void Pengaturan_Click(object sender, EventArgs e)
        {
            panel5.BackColor = Color.FromArgb(0, 90, 192);
            panel3.BackColor = Color.FromArgb(6, 122, 255);
            panel4.BackColor = Color.FromArgb(6, 122, 255);
            panel2.BackColor = Color.FromArgb(6, 122, 255);
            panel6.BackColor = Color.FromArgb(6, 122, 255);
            Pengaturan pengaturan = new Pengaturan();
            addUserControl(pengaturan);
        }
        private void MAdmin_Click(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(0, 90, 192);
            panel3.BackColor = Color.FromArgb(6, 122, 255);
            panel4.BackColor = Color.FromArgb(6, 122, 255);
            panel2.BackColor = Color.FromArgb(6, 122, 255);
            panel5.BackColor = Color.FromArgb(6, 122, 255);
            DAMAdmin admin = new DAMAdmin();
            addUserControl(admin);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
