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
    public partial class LandingPage : Form
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void btnCekResi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCekResi.Text))
            {
                MessageBox.Show("Harap masukkan nomor resi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DetailPaket detailPaket = new DetailPaket(txtCekResi.Text);
            detailPaket.Show();
        }
    }
}
