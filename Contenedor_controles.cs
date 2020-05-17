using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Control_camion
{
    public partial class Contenedor_controles : Form
    {
        
        public Contenedor_controles()
        {
            InitializeComponent();
            Principal principal = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            TextReader ve = new StreamReader("vehiculos.txt");
            string[] lineas = ve.ReadToEnd().Split('\n');
            ve.Close();

            foreach (string i in lineas)
            {
                string[] j = i.Split('|');
                if(j[0]==principal.placas)
                {
                    principal.vehiculo[0,0] = j[0];
                    principal.vehiculo[0, 1] = j[1];
                    principal.vehiculo[0, 2] = j[2];
                    principal.vehiculo[0, 3] = j[3];
                    principal.vehiculo[0, 4] = j[4];
                    principal.vehiculo[0, 5] = j[5];
                    principal.vehiculo[0, 6] = j[6];
                    principal.vehiculo[0, 7] = j[7];
                    break;
                }
            }

            

            if (File.Exists("elementos.txt"))
            {
                TextReader el = new StreamReader("elementos.txt");
                string[] filas = el.ReadToEnd().Split('\n');
                el.Close();

                foreach(string i in filas)
                {
                    if(i!=""&&i!="\r")
                    {
                        string[] j = i.Split('|');
                        
                        if (j[0] == principal.placas)
                        {
                            principal.elementos.Add(i);
                        }


                    }                    
                }
            }

            Abrir(new Control());
        }

        //Funciones
        public void Abrir(Object formulario)
        {
            //Principal principal = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            Contenedor_controles principal = (Contenedor_controles)this;

            if (principal.panel1.Controls.Count > 0)
            {
                principal.panel1.Controls.RemoveAt(0);
            }

            Form controles = formulario as Form;
            controles.TopLevel = false;
            controles.Dock = DockStyle.Fill;
            principal.panel1.Controls.Add(controles);
            principal.panel1.Tag = controles;
            int ancho = controles.Size.Width;
            int alto = controles.Size.Height;
            principal.Size = new Size(ancho, alto + 35);
            controles.Show();
        }

        //Eventos
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void generalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new Control());
        }

        private void editarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new Editar_datos());
        }

        private void editarElementosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new Editar_elementos());
        }

        private void ingresarElementoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new Ingresar_elemento());
        }

        private void cambiosAlVehículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new Cambios_al_vehiculo());
        }

        private void informeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new Informe());
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new Acerca_de());
        }
    }
}
