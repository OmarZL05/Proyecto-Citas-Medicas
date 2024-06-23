using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMC
{
    public partial class MenuGeneral : Form
    {
        public MenuGeneral()
        {
            InitializeComponent();
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuCitas menuCitas = new MenuCitas();
            menuCitas.ShowDialog();
            this.Show();
        }

        private void btnMedicos_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuMedicos menuMedicos = new MenuMedicos();
            menuMedicos.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuMedicosV2 menuMedicos = new MenuMedicosV2();
            menuMedicos.ShowDialog();
            this.Show();
        }
    }
}
