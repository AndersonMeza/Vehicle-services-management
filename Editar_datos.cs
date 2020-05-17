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
    public partial class Editar_datos : Form
    {
        public Editar_datos()
        {
            InitializeComponent();
            string[,] vehiculo;
            List<string> elementos;
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            vehiculo = pp.vehiculo;
            elementos = pp.elementos;
            textBox1.Text = vehiculo[0, 0];
            comboBox1.Text = vehiculo[0, 1];
            comboBox2.Text = vehiculo[0, 3];
            textBox2.Text = vehiculo[0, 2];

            TextReader colores = new StreamReader("colores.txt");
            string texto = colores.ReadToEnd();
            string[] lineas = texto.Split('\n');
            foreach (string i in lineas)
            {
                if (i != "")
                {
                    comboBox2.Items.Add(i);
                }
            }
            colores.Close();

            TextReader tipos = new StreamReader("tipos.txt");
            string texto2 = tipos.ReadToEnd();
            string[] lineas2 = texto2.Split('\n');
            foreach (string i in lineas2)
            {
                if (i != "")
                {
                    comboBox1.Items.Add(i);
                }
            }
            tipos.Close();

        }

        private void Editar_datos_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            TextReader vehiculos = new StreamReader("vehiculos.txt");
            string[] lineas = vehiculos.ReadToEnd().Split('\n');
            vehiculos.Close();
            foreach (string i in lineas)
            {
                string[] argumentos = i.Split('|');
                if (argumentos[0] == textBox1.Text.ToUpper())
                {
                    count = 1;
                }
            }
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            string placa_anterior = pp.vehiculo[0, 0];
            if (count != 1 || textBox1.Text.ToUpper()==pp.vehiculo[0,0])
            {
                

                int cc8 = 0;
                int posicion = 0;
                TextReader vehiculos2 = new StreamReader("vehiculos.txt");
                string[] lineas2 = vehiculos2.ReadToEnd().Split('\n');
                vehiculos2.Close();
                foreach (string i in lineas2)
                {
                    string[] argumentos = i.Split('|');
                    if (argumentos[0] == pp.vehiculo[0, 0] && argumentos[1] == pp.vehiculo[0, 1] && argumentos[2] == pp.vehiculo[0, 2] && argumentos[3] == pp.vehiculo[0, 3] && argumentos[4] == pp.vehiculo[0, 4] && argumentos[5] == pp.vehiculo[0, 5] && argumentos[6] == pp.vehiculo[0, 6] && argumentos[7].Split('\r')[0] == pp.vehiculo[0, 7])
                    {
                        cc8 = 1;
                        break;
                    }
                    posicion++;
                }
                
                int count2 = 0;
                if (cc8 == 1)
                {
                    TextWriter vehiculosw = new StreamWriter("vehiculos.txt");
                    foreach (string i in lineas2)
                    {
                        if (i != "" && i != "\r")
                        {
                            if (count2 != posicion)
                            {
                                vehiculosw.Write(i);
                            }
                            else
                            {
                                string[] jjj = i.Split('|');

                                pp.vehiculo[0, 0] = textBox1.Text.ToUpper();
                                pp.vehiculo[0, 1] = comboBox1.Text.ToUpper();
                                pp.vehiculo[0, 2] = textBox2.Text.ToUpper();
                                pp.vehiculo[0, 3] = comboBox2.Text.ToUpper();
                                jjj[0] = textBox1.Text.ToUpper();
                                jjj[1] = comboBox1.Text.ToUpper();
                                jjj[2] = textBox2.Text.ToUpper();
                                jjj[3] = comboBox2.Text.ToUpper();

                                vehiculosw.WriteLine(jjj[0] + "|" + jjj[1] + "|" + jjj[2] + "|" + jjj[3] + "|" + jjj[4] + "|" + jjj[5] + "|" + jjj[6] + "|" + jjj[7].Split('\r')[0]);
                            }
                        }
                        count2++;
                    }
                    vehiculosw.Close();
                    
                    
                }

                //ELEMENTOS

                TextReader elementosrr = new StreamReader("elementos.txt");
                string[] lineasele = elementosrr.ReadToEnd().Split('\n');
                List<string> elenuevo = new List<string>();
                elementosrr.Close();
                foreach (string i in lineasele)
                {
                    if (i != "" && i != "\r")
                    {
                        string[] argumentos = i.Split('|');
                        if (argumentos[0] == placa_anterior)
                        {
                            elenuevo.Add(textBox1.Text.ToUpper() + "|" + argumentos[1] + "|" + argumentos[2] + "|" + argumentos[3].Split('\r')[0]);
                        }
                        else
                        {
                            elenuevo.Add(i);
                        }
                    }

                }

                TextWriter elemew = new StreamWriter("elementos.txt");
                foreach (string i in elenuevo)
                {
                    elemew.WriteLine(i.Split('\r')[0]);
                }
                elemew.Close();

                pp.elementos.Clear();

                TextReader el = new StreamReader("elementos.txt");
                string[] filas = el.ReadToEnd().Split('\n');
                el.Close();

                foreach (string i in filas)
                {
                    if (i != "" && i != "\r")
                    {
                        string[] j = i.Split('|');

                        if (j[0] == pp.vehiculo[0,0])
                        {
                            pp.elementos.Add(i);
                        }


                    }
                }
                pp.vehiculo[0, 0] = textBox1.Text.ToUpper();
                pp.vehiculo[0, 1] = comboBox1.Text.ToUpper();
                pp.vehiculo[0, 2] = textBox2.Text.ToUpper();
                pp.vehiculo[0, 3] = comboBox2.Text.ToUpper();
                MessageBox.Show("Los Datos se han cambiado con éxito", "Edición de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ya existe un vehículo con esa placa", "Ingreso de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }                        
            
        }
    }
}
