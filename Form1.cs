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
    public partial class Form1 : Form
    {

        TextReader camiones;
        string texto_Completo;
        string[] lineas;
        string[,] matriz_camion;
        public Form1()
        {
            InitializeComponent();

            abrir_todo();
            string ruta = "vehiculos.txt";
            if (File.Exists(ruta))
            {
                
                camiones= new StreamReader(ruta);
                texto_Completo = camiones.ReadToEnd();
                lineas = texto_Completo.Split('\n');
                matriz_camion = new string[lineas.Length, 8];

                for(int a=0; a<lineas.Length;a++)
                {
                    string[] segmentos = lineas[a].Split('|');

                    for(int b =0;b<segmentos.Length;b++)
                    {
                        if (segmentos[0]!="\r")
                        {
                            matriz_camion[a, b] = segmentos[b];
                        }
                                                 
                    }
                    int count = 0;
                    foreach(char h in segmentos[0])
                    {
                        if(h!=' '&&h!='\r')
                        {
                            count = 1;
                            break;
                        }
                    }
                    if(count==1)
                    {
                        comboBox1.Items.Add(segmentos[0]);
                    }
                    
                }

                camiones.Close();
            }
            else
            {
                button1.Enabled = false;

            }
                      
        }
        //crear o abrir archivos
        public void abrir_todo()
        {
            string ruta1 = "colores.txt";
            string ruta2 = "tipos.txt";            

            if(!File.Exists(ruta1))
            {
                TextWriter colores = new StreamWriter(ruta1);
                colores.WriteLine("AMARILLO\nAZUL\nAZUL MARINO\nBLANCO\nBEIGE\nCELESTE\nNARANJA\nNEGRO\nPLATA\nROJO\nVERDE\nVINO");
                colores.Close();
            }

            if (!File.Exists(ruta2))
            {
                TextWriter tipos = new StreamWriter(ruta2);
                tipos.WriteLine("AUTOMOVIL\nCAMIÓN\nCAMIONETA\nJEEP\nMOTO\nTANQUERO\nTRACTO CAMIÓN");
                tipos.Close();
            }
        }


        //abrimos el formulario para ingresar el nuevo vehiculo
        public void Abrir_ingresar_vehiculo()
        {
               
            Principal principal = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            
            if (principal.panel1.Controls.Count > 0)
            {
                principal.panel1.Controls.RemoveAt(0);
            }

            Agregar_vehiculo vehiculo = new Agregar_vehiculo();
            vehiculo.TopLevel = false;
            vehiculo.Dock = DockStyle.Fill;
            principal.panel1.Controls.Add(vehiculo);
            principal.panel1.Tag = vehiculo;
            principal.Location = new Point(0, 0);
            int ancho = vehiculo.Size.Width;
            int alto = vehiculo.Size.Height;
            principal.Size = new Size(ancho, alto+35);            
            vehiculo.Show();
        }

        public void Abrir_ingresar()
        {
            Principal principal = Application.OpenForms.OfType<Principal>().SingleOrDefault();

            string ruta = "vehiculos.txt";
            camiones = new StreamReader(ruta);
            texto_Completo = camiones.ReadToEnd();
            lineas = texto_Completo.Split('\n');
            matriz_camion = new string[lineas.Length, 8];
            int yy = 0;
            for (int a = 0; a < lineas.Length; a++)
            {
                string[] segmentos = lineas[a].Split('|');

                for (int b = 0; b < segmentos.Length; b++)
                {
                    matriz_camion[a, b] = segmentos[b];
                }
                int count = 0;
                foreach (char h in segmentos[0])
                {
                    if (h != ' ')
                    {
                        count = 1;
                        break;
                    }
                }
                if (segmentos[0] == comboBox1.Text)
                {
                    yy = 1;
                    break;
                }

            }
            camiones.Close();

            if(yy==1)
            {
                principal.placas = comboBox1.Text;

                if (principal.panel1.Controls.Count > 0)
                {
                    principal.panel1.Controls.RemoveAt(0);
                }

                Contenedor_controles controles = new Contenedor_controles();
                controles.TopLevel = false;
                controles.Dock = DockStyle.Fill;
                principal.panel1.Controls.Add(controles);
                principal.panel1.Tag = controles;
                principal.MaximizeBox = false;
                int ancho = controles.Size.Width;
                int alto = controles.Size.Height;
                principal.Size = new Size(ancho, alto + 35);
                int deskHeight = Screen.PrimaryScreen.Bounds.Height;
                int deskWidth = Screen.PrimaryScreen.Bounds.Width;
                principal.Location = new Point((deskWidth - principal.Width) / 2, ((deskHeight - principal.Height) / 2) - 20);
                //principal.Location = Screen.PrimaryScreen.WorkingArea.Location;
                //principal.Size = Screen.PrimaryScreen.WorkingArea.Size;
                controles.Show();
            }
            else
            {
                MessageBox.Show("El vehículo relacionado a esa placa no existe", "Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Abrir_ingresar_vehiculo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Abrir_ingresar();
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
