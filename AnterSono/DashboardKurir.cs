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
    public partial class DashboardKurir : Form
    {
        private int idKurir;
        public DashboardKurir(int idKurir)
        {
            InitializeComponent();
            DKHome home = new DKHome(idKurir);
            addUserControl(home);
            this.idKurir = idKurir;
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            content.Controls.Clear();
            content.Controls.Add(userControl);
            userControl.BringToFront();
        }


    }
}
