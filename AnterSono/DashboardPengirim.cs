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
    public partial class DashboardPengirim : Form
    {
        public DashboardPengirim(int id)
        {
            InitializeComponent();
            DPHome home = new DPHome(id);
            addUserControl(home);
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            content.Controls.Clear();
            content.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void DashboardPengirim_Load(object sender, EventArgs e)
        {

        }
    }
}
