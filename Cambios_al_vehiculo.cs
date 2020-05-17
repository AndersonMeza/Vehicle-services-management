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
    public partial class Cambios_al_vehiculo : Form
    {
        public Cambios_al_vehiculo()
        {
            InitializeComponent();
            
            string[,] vehiculo;
            List<string> elementos;
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            vehiculo = pp.vehiculo;
            elementos = pp.elementos;

            if (elementos.Count != 0)
            {
                foreach (string i in elementos)
                {
                    string[] filas = i.Split('|');

                    if (i != "")
                    {
                        comboBox1.Items.Add(filas[1]);
                    }
                }
            }
            numericUpDown1.Maximum = Convert.ToInt32(vehiculo[0, 4]);
            numericUpDown1.Value = Convert.ToInt32(vehiculo[0, 4]);
        }

        private void Cambios_al_vehiculo_Load(object sender, EventArgs e)
        {

        }

        public bool elemento_existe(string e)
        {
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            TextReader elementosrr = new StreamReader("elementos.txt");
            string[] lineasele = elementosrr.ReadToEnd().Split('\n');
            bool existe = false;
            elementosrr.Close();

            foreach (string i in lineasele)
            {

                if (i != "" && i != "\r")
                {
                    string[] argumentos = i.Split('|');
                    if (argumentos[0] == pp.vehiculo[0, 0] && argumentos[1] == comboBox1.Text)
                    {
                        existe = true;
                    }
                }

            }

            return existe;
        }

        public bool es_vacio(string a)
        {
            bool es = true;

            foreach (char i in a)
            {
                if (i != ' ' && i != '\r' && i != '\n')
                {
                    es = false;
                    break;
                }
            }

            return es;
        }

        public void editar_vehiculo(int precio, string informe)
        {
            //Editamos en el programa
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            pp.vehiculo[0, 5] = Convert.ToString(Convert.ToInt32(pp.vehiculo[0, 5])+ precio);
            pp.vehiculo[0, 7] = pp.vehiculo[0,7].Split('\r')[0]+informe + "`";

            //editamos en el documento
            TextReader vehiculo = new StreamReader("vehiculos.txt");
            string texto_completo = vehiculo.ReadToEnd();
            vehiculo.Close();
            string[] lineas = texto_completo.Split('\n');
            int posicion=65156165;

            for(int a=0;a<lineas.Length;a++)
            {
                string[] argumentos = lineas[a].Split('|');
                if(!es_vacio(argumentos[0]))
                {
                    if(argumentos[0]==pp.vehiculo[0,0])
                    {
                        posicion = a;
                        break;
                    }
                }
            }

            TextWriter vehiculow = new StreamWriter("vehiculos.txt");
            for(int a=0;a<lineas.Length;a++)
            {
                string[] argumentos = lineas[a].Split('|');
                if (!es_vacio(argumentos[0]))
                {
                    if (posicion==a)
                    {
                        vehiculow.WriteLine(pp.vehiculo[0,0] +"|"+ pp.vehiculo[0,1] + "|" + pp.vehiculo[0,2] + "|" + pp.vehiculo[0,3] + "|" + pp.vehiculo[0,4] + "|" + pp.vehiculo[0,5] + "|" + pp.vehiculo[0,6] + "|" + pp.vehiculo[0,7].Split('\r')[0]);
                    }
                    else
                    {
                        vehiculow.WriteLine(lineas[a].Split('\r')[0]);
                    }
                }
            }

            vehiculow.Close();
        }

        public void editar_elemento(string elemento, int diferencia)
        {
            //Editamos en el programa
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            List<string> elementos = new List<string>();
            for(int a =0;a<pp.elementos.Count;a++)
            {
                elementos.Add(pp.elementos[a]);
            }
            pp.elementos.Clear();
            
            foreach(string i in elementos)
            {
                string[] argumentos = i.Split('|');
                if (!es_vacio(argumentos[0]))
                {
                    if (argumentos[0] == pp.vehiculo[0, 0]&&argumentos[1]==elemento)
                    {
                        if(diferencia==0)
                        {
                            pp.elementos.Add(argumentos[0] + "|" + argumentos[1] + "|" + argumentos[2] + "|" + argumentos[2]);
                        }
                        else if(diferencia<Convert.ToInt32(argumentos[2])-Convert.ToInt32(argumentos[3]))
                        {                            
                            int proximo_cambio =Convert.ToInt32(argumentos[2]) - diferencia;
                            pp.elementos.Add(argumentos[0] + "|" + argumentos[1] + "|" + argumentos[2] + "|" + proximo_cambio);
                        }
                        else
                        {
                            pp.elementos.Add(argumentos[0] + "|" + argumentos[1] + "|" + argumentos[2] + "|" + argumentos[3].Split('\r')[0]);
                        }
                    }
                    else
                    {
                        pp.elementos.Add(i.Split('\r')[0]);
                    }
                }
            }

            //editamos en el documento
            TextReader elementor = new StreamReader("elementos.txt");
            string texto_completo = elementor.ReadToEnd();
            elementor.Close();
            string[] lineas = texto_completo.Split('\n');
            int posicion = 65156165;

            for (int a = 0; a < lineas.Length; a++)
            {
                string[] argumentos = lineas[a].Split('|');
                if (!es_vacio(argumentos[0]))
                {
                    if (argumentos[0] == pp.vehiculo[0, 0]&&argumentos[1]==elemento)
                    {
                        posicion = a;
                        break;
                    }
                }
            }

            TextWriter elementow = new StreamWriter("elementos.txt");
            for (int a = 0; a < lineas.Length; a++)
            {
                string[] argumentos = lineas[a].Split('|');
                if (!es_vacio(argumentos[0]))
                {
                    if (posicion == a)
                    {
                        if (diferencia == 0)
                        {
                            elementow.WriteLine(argumentos[0] + "|" + argumentos[1] + "|" + argumentos[2] + "|" + argumentos[2]);                            
                        }
                        else if (diferencia < Convert.ToInt32(argumentos[2]) - Convert.ToInt32(argumentos[3]))
                        {
                            int proximo_cambio = Convert.ToInt32(argumentos[2]) - diferencia;
                            elementow.WriteLine(argumentos[0] + "|" + argumentos[1] + "|" + argumentos[2] + "|" + proximo_cambio);                            
                        }
                        else
                        {
                            elementow.WriteLine(argumentos[0] + "|" + argumentos[1] + "|" + argumentos[2] + "|" + argumentos[3].Split('\r')[0]);
                        }
                    }
                    else
                    {
                        elementow.WriteLine(lineas[a].Split('\r')[0]);
                    }
                }
            }

            elementow.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            string elemento = comboBox1.Text;
            int kilometraje = Convert.ToInt32(numericUpDown1.Value);
            string cantidad_unidad = numericUpDown2.Value + comboBox2.Text;
            string fecha_cambio = dateTimePicker1.Value.ToString().Split('/')[0] + "/" + dateTimePicker1.Value.ToString().Split('/')[1] + "/" + dateTimePicker1.Value.ToString().Split('/')[2].Substring(0, dateTimePicker1.Value.ToString().Split('/')[2].Length - 9);
            int diferencia_kmactual = Convert.ToInt32(pp.vehiculo[0, 4]) - Convert.ToInt32(numericUpDown1.Value);
            int precio = Convert.ToInt32(numericUpDown3.Value);
            int inv = Convert.ToInt32(pp.vehiculo[0, 5])+precio;
            string informe = "El " + fecha_cambio + " se realizó el cambio de " + elemento + " por un valor de " + precio + "$, con un kilometraje de " + kilometraje + " Km, y una inversión total de " + inv+ "$";

            if (elemento_existe(elemento))
            {
                if(!es_vacio(comboBox2.Text) && comboBox2.Text!="Seleccione")
                {                    
                    editar_vehiculo(precio, informe);
                    editar_elemento(elemento, diferencia_kmactual);
                    MessageBox.Show("Se ha realizado el cambio del elemento correctamente", "Cambio elemento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Debe ingresar el tipo de unidad", "Cambio elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un elemento válido en la caja de selección", "Cambio elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
