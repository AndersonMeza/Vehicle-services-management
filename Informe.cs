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
    public partial class Informe : Form
    {
        public Informe()
        {
            InitializeComponent();
            string[,] vehiculo;
            List<string> elementos;
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            vehiculo = pp.vehiculo;
            elementos = pp.elementos;
            
            string [] infome = vehiculo[0, 7].Split('`');

            int c = 0;
            

            if(infome[0]!="\r"&&infome[0]!="\r\n")
            {
                c = 1;
            }

            if(c==1)
            {
                foreach(string t in infome)
                {
                    richTextBox1.Text = richTextBox1.Text + t + "\n";
                }
            }
            else
            {
                richTextBox1.Text = "No se han hecho cambios aún";
            }
        }

        private void Informe_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string texto = richTextBox1.Text;
            Clipboard.SetText(texto);            
            label3.Text = "Se ha copiado el informe en el portapapeles";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
