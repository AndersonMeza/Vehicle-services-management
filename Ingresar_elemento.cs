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
    public partial class Ingresar_elemento : Form
    {
        public Ingresar_elemento()
        {
            InitializeComponent();
            string[,] vehiculo;
            List<string> elementos;
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            vehiculo = pp.vehiculo;
            elementos = pp.elementos;
            
            foreach (string i in elementos)
            {
                if (i != "" && i != "\r")
                {
                    string[] argumentos = i.Split('|');
                    label13.Text = label13.Text + "\n" + argumentos[1] + "//" + argumentos[2] + " Km";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Principal principal = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            List<string> elementos = new List<string>();

            string text=textBox3.Text.ToUpper();
            int cc = 0;

            foreach(char i in text)
            {
                if(i!=' ')
                {
                    cc++;
                }
            }


            if(cc!=0)
            {
                if (!File.Exists("elementos.txt"))
                {
                    TextWriter elemento = new StreamWriter("elementos.txt");
                    elemento.WriteLine(principal.vehiculo[0, 0] + "|" + textBox3.Text.ToUpper() + "|" + numericUpDown1.Value + "|" + numericUpDown3.Value);
                    principal.elementos.Add(principal.vehiculo[0, 0] + "|" + textBox3.Text.ToUpper() + "|" + numericUpDown1.Value + "|" + numericUpDown3.Value);
                    elemento.Close();
                    MessageBox.Show("El elemento se añadido correctamente", "Añadir Elementos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    TextReader elementosr = new StreamReader("elementos.txt");
                    string texto2 = elementosr.ReadToEnd();
                    string[] lineas = texto2.Split('\n');
                    elementosr.Close();
                    int count = 0;
                    foreach (string i in lineas)
                    {
                        if(i!=""&&i!="\r")
                        {
                            string[] hh = i.Split('|');
                            if (textBox3.Text.ToUpper() == hh[1] && principal.vehiculo[0, 0] == hh[0])
                            {
                                count = 1;
                                break;
                            }
                        }
                    }

                    if (count == 0)
                    {
                        TextWriter elementosw = new StreamWriter("elementos.txt");
                        elementosw.Write(texto2);
                        elementosw.WriteLine(principal.vehiculo[0, 0] + "|" + textBox3.Text.ToUpper() + "|" + numericUpDown1.Value + "|" + numericUpDown3.Value);
                        principal.elementos.Add(principal.vehiculo[0, 0] + "|" + textBox3.Text.ToUpper() + "|" + numericUpDown1.Value + "|" + numericUpDown3.Value);
                        elementosw.Close();
                        MessageBox.Show("El elemento se añadido correctamente", "Añadir Elementos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        label13.Text = "Elementos\nNombre  //  Cambio cada";
                        ///REINICIAR
                        string[,] vehiculo;
                        List<string> elementos1;
                        Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
                        vehiculo = pp.vehiculo;
                        elementos1 = pp.elementos;

                        foreach (string i in elementos1)
                        {
                            if (i != "" && i != "\r")
                            {
                                string[] argumentos = i.Split('|');
                                label13.Text = label13.Text + "\n" + argumentos[1] + "//" + argumentos[2] + " Km";
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ya existe un elemento con ese nombre", "Añadir Elementos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
            else
            {
                MessageBox.Show("Ingrese un nombre al elemento", "Añadir Elementos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void Ingresar_elemento_Load(object sender, EventArgs e)
        {

        }
    }
}
