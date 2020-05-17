using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_camion
{
    public partial class Principal : Form
    {
        public string placas;
        public string[,] vehiculo = new string[1, 8];
        public List<string> elementos = new List<string>();
        public Principal()
        {
            InitializeComponent();
            Abrirvehiculo();
        }

        public void Abrirvehiculo()
        {
            if(panel1.Controls.Count>0)
            {
                panel1.Controls.RemoveAt(0);
            }

            Form1 vehiculo = new Form1();
            vehiculo.TopLevel = false;
            vehiculo.Dock = DockStyle.Fill;
            panel1.Controls.Add(vehiculo);
            panel1.Tag = vehiculo;
            this.Width = vehiculo.Width;
            this.Height = vehiculo.Height+30;
            vehiculo.Show();
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
