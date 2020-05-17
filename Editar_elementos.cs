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
    public partial class Editar_elementos : Form
    {
        public Editar_elementos()
        {
            InitializeComponent();
            string[,] vehiculo;
            List<string> elementos;
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            vehiculo = pp.vehiculo;
            elementos = pp.elementos;

            if(elementos.Count!=0)
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

            foreach(string i in elementos)
            {
                if (i != "" && i != "\r")
                {
                    string[] argumentos = i.Split('|');
                    label13.Text = label13.Text + "\n" + argumentos[1] + "//" + argumentos[2]+" Km";
                }
            }
                     
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Editar_elementos_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            TextReader elementosrr = new StreamReader("elementos.txt");
            string[] lineasele = elementosrr.ReadToEnd().Split('\n');
            List<string> elenuevo = new List<string>();
            int existe = 0;
            elementosrr.Close();

            int con = 0;
            foreach (string ss in lineasele)
            {
                if (ss != "" && ss != " " && ss != "\r")
                {
                    string[] ss2 = ss.Split('|');
                    if (textBox3.Text.ToUpper() == ss2[1])
                    {
                        con++;
                    }
                }
            }

            if (con == 0 || comboBox1.Text==textBox3.Text.ToUpper())
            {                
                if (textBox3.Text.ToUpper() != ""&& comboBox1.Text!="")
                {
                    foreach (string i in lineasele)
                    {

                        if (i != "" && i != "\r")
                        {
                            string[] argumentos = i.Split('|');
                            if (argumentos[0] == pp.vehiculo[0, 0] && argumentos[1] == comboBox1.Text)
                            {
                                elenuevo.Add(argumentos[0] + "|" + textBox3.Text.ToUpper() + "|" + numericUpDown1.Value + "|" + argumentos[3].Split('\r')[0]);
                                MessageBox.Show("Se ha modificado el elemento correctamente", "Modificar Elemento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                existe = 1;
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

                            if (j[0] == pp.vehiculo[0, 0])
                            {
                                pp.elementos.Add(i);
                            }


                        }
                    }

                    if (existe != 0)
                    {
                        //REINICIO
                        comboBox1.Items.Clear();
                        comboBox1.Text = textBox3.Text.ToUpper();
                        label13.Text = "Elementos\nNombre  //  Cambio cada";
                        string[,] vehiculo;
                        List<string> elementos;
                        vehiculo = pp.vehiculo;
                        elementos = pp.elementos;

                        if (elementos.Count != 0)
                        {
                            foreach (string i in elementos)
                            {
                                string[] filas2 = i.Split('|');

                                if (i != "")
                                {
                                    comboBox1.Items.Add(filas2[1]);
                                }
                            }
                        }

                        foreach (string i in elementos)
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
                        MessageBox.Show("Seleccione un elemento válido a editar en la caja de selección", "Modificar elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("No puede dejar vacío el nombre", "Modificar Elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ya existe un elemento con ese nombre", "Modificar elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            textBox3.Text = comboBox1.Text;

            TextReader elementosrr = new StreamReader("elementos.txt");
            string[] lineasele = elementosrr.ReadToEnd().Split('\n');            
            elementosrr.Close();

            foreach (string i in lineasele)
            {

                if (i != "" && i != "\r")
                {
                    string[] argumentos = i.Split('|');
                    if (argumentos[0] == pp.vehiculo[0, 0] && argumentos[1] == comboBox1.Text)
                    {
                        numericUpDown1.Value = Convert.ToInt32(argumentos[2]);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            TextReader elementosrr = new StreamReader("elementos.txt");
            string[] lineasele = elementosrr.ReadToEnd().Split('\n');
            List<string> elenuevo = new List<string>();            
            elementosrr.Close();

            int con = 0;
            foreach (string ss in lineasele)
            {
                if (ss != "" && ss != " " && ss != "\r")
                {
                    string[] ss2 = ss.Split('|');
                    if (comboBox1.Text == ss2[1])
                    {
                        con++;
                    }
                }
            }

            if (con ==1)
            {

                foreach (string i in lineasele)
                {

                    if (i != "" && i != "\r")
                    {
                        string[] argumentos = i.Split('|');
                        if (argumentos[0] == pp.vehiculo[0, 0] && argumentos[1] == comboBox1.Text)
                        {
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

                        if (j[0] == pp.vehiculo[0, 0])
                        {
                            pp.elementos.Add(i);
                        }


                    }
                }
                //REINICIO
                comboBox1.Items.Clear();
                comboBox1.Text = textBox3.Text.ToUpper();
                label13.Text = "Elementos\nNombre  //  Cambio cada";
                string[,] vehiculo;
                List<string> elementos;
                vehiculo = pp.vehiculo;
                elementos = pp.elementos;

                if (elementos.Count != 0)
                {
                    foreach (string i in elementos)
                    {
                        string[] filas2 = i.Split('|');

                        if (i != "")
                        {
                            comboBox1.Items.Add(filas2[1]);
                        }
                    }
                }

                foreach (string i in elementos)
                {
                    if (i != "" && i != "\r")
                    {
                        string[] argumentos = i.Split('|');
                        label13.Text = label13.Text + "\n" + argumentos[1] + "//" + argumentos[2] + " Km";
                    }
                }

                MessageBox.Show("Se ha eliminado el elemento correctamente", "Eliminar Elemento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Seleccione un elemento válido a eliminar en la caja de selección", "Eliminar elemento ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
