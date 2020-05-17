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
    public partial class Agregar_vehiculo : Form
    {
        string placa_general;
        List<string> elementos = new List<string>();
        string vehiculo;
        int count = 0;
        int countele = 0;
        public Agregar_vehiculo()
        {
            InitializeComponent();
            TextReader colores = new StreamReader("colores.txt");
            string texto = colores.ReadToEnd();
            string[] lineas = texto.Split('\n');
            foreach(string i in lineas)
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
                if(i!="")
                {
                    comboBox1.Items.Add(i);
                }
            }
            tipos.Close();

        }

        //Funciones
        private void Mostrar_datos_en_label(Label lb)
        {
            lb.Text = "";
            lb.Text = lb.Text + textBox1.Text.ToUpper()+"\n";
            lb.Text = lb.Text + comboBox1.Text.ToString().ToUpper() + "\n";
            lb.Text = lb.Text + comboBox2.Text.ToString().ToUpper() + "\n";
            lb.Text = lb.Text + textBox2.Text.ToUpper();

        }

        private void Mostrar_elementos_en_label(Label lb)
        {
            lb.Text = lb.Text + "\n";
            lb.Text = lb.Text + textBox3.Text.ToUpper() + " // ";
            lb.Text = lb.Text + numericUpDown2.Text.ToUpper();
        }

        public void Abrir_ingresar()
        {
            Principal principal = Application.OpenForms.OfType<Principal>().SingleOrDefault();

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
            controles.Show();
        }

        //Eventos
        private void button1_Click(object sender, EventArgs e)
        {

            
            
            
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {                                   
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox2.Text != "" && textBox1.Text != "" && textBox2.Text != "")
            {
                if(File.Exists("vehiculos.txt"))
                {
                    int count2 = 0;
                    TextReader vehiculos = new StreamReader("vehiculos.txt");
                    string[] lineas = vehiculos.ReadToEnd().Split('\n');
                    vehiculos.Close();
                    foreach (string i in lineas)
                    {
                        string[] argumentos = i.Split('|');
                        if (argumentos[0] == textBox1.Text.ToUpper())
                        {
                            count2 = 1;
                        }
                    }

                    if (count2 != 1)
                    {
                        Principal principal = Application.OpenForms.OfType<Principal>().SingleOrDefault();
                        principal.placas = textBox1.Text.ToUpper();
                        placa_general = textBox1.Text.ToUpper();
                        Mostrar_datos_en_label(label11);
                        string comentario = textBox4.Text;
                        if (textBox4.Text == "")
                        {
                            comentario = "sin comentario";
                        }
                        vehiculo = textBox1.Text.ToUpper() + "|" + comboBox1.Text.ToUpper() + "|" + textBox2.Text.ToUpper() + "|" + comboBox2.Text.ToUpper() + "|" + numericUpDown1.Value + "|0|" + comentario + "|";
                        count = 1;

                        if (textBox3.Text != "")
                        {
                            int con = 0;
                            foreach (string ss in elementos)
                            {
                                string[] ss2 = ss.Split('|');
                                if (textBox3.Text.ToUpper() == Convert.ToString(ss2[0]))
                                {
                                    con++;
                                }
                            }

                            if (con == 0)
                            {
                                Mostrar_elementos_en_label(label13);
                                elementos.Add(textBox3.Text.ToUpper() + "|" + numericUpDown2.Value + "|" + numericUpDown3.Value);
                                label17.Text = "Se ingresó el vehículo y el elemento";
                                countele = 1;
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    MessageBox.Show("Solo se ingresó el vehículo", "Ingreso de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                MessageBox.Show("Ya existe un elemento con ese nombre", "Ingreso de Elementos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                label17.Text = "";

                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un vehículo con esa placa", "Ingreso de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    Principal principal = Application.OpenForms.OfType<Principal>().SingleOrDefault();
                    principal.placas = textBox1.Text.ToUpper();
                    placa_general = textBox1.Text.ToUpper();
                    Mostrar_datos_en_label(label11);
                    string comentario = textBox4.Text;
                    if (textBox4.Text == "")
                    {
                        comentario = "sin comentario";
                    }
                    vehiculo = textBox1.Text.ToUpper() + "|" + comboBox1.Text.ToUpper() + "|" + textBox2.Text.ToUpper() + "|" + comboBox2.Text.ToUpper() + "|" + numericUpDown1.Value + "|0|" + comentario + "|";
                    count = 1;

                    if (textBox3.Text != "")
                    {
                        int con = 0;
                        foreach (string ss in elementos)
                        {
                            string[] ss2 = ss.Split('|');
                            if (textBox3.Text.ToUpper() == Convert.ToString(ss2[0]))
                            {
                                con++;
                            }
                        }

                        if (con == 0)
                        {
                            Mostrar_elementos_en_label(label13);
                            elementos.Add(textBox3.Text.ToUpper() + "|" + numericUpDown2.Value + "|" + numericUpDown3.Value);
                            label17.Text = "Se ingresó el vehículo y el elemento";
                            countele = 1;
                        }
                        else
                        {
                            if (count == 1)
                            {
                                MessageBox.Show("Solo se ingresó el vehículo", "Ingreso de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            MessageBox.Show("Ya existe un elemento con ese nombre", "Ingreso de Elementos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            label17.Text = "";

                        }

                    }
                }                                
                
            }
            else
            {
                MessageBox.Show("Debe ingresar todos los datos para el vehículo", "Ingreso de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (count == 1)
            {
                if (!File.Exists("vehiculos.txt"))
                {
                    TextWriter vehiculos = new StreamWriter("vehiculos.txt");
                    vehiculos.WriteLine(vehiculo);
                    vehiculos.Close();                    
                }
                else
                {
                    Principal principal = Application.OpenForms.OfType<Principal>().SingleOrDefault();
                    int count2 = 0;
                    TextReader vehiculos = new StreamReader("vehiculos.txt");
                    string[] lineas = vehiculos.ReadToEnd().Split('\n');
                    vehiculos.Close();
                    foreach (string i in lineas)
                    {
                        string[] argumentos = i.Split('|');
                        if (argumentos[0] == principal.placas)
                        {
                            count2 = 1;
                        }                                              
                    }

                    if (count2 != 1)
                    {
                        TextReader vehiculosr = new StreamReader("vehiculos.txt");
                        string texto = vehiculosr.ReadToEnd();
                        vehiculosr.Close();
                        TextWriter vehiculosw = new StreamWriter("vehiculos.txt");
                        vehiculosw.WriteLine(texto + vehiculo);
                        vehiculosw.Close();
                        TextReader vehiculosr2 = new StreamReader("vehiculos.txt");
                        string texto1 = vehiculosr2.ReadToEnd();
                        vehiculosr2.Close();
                    }
                    else
                    {
                        MessageBox.Show("La placa que ingreso ya existe en otro vehículo, cambie la placa y de clic en ingresar nuevamente", "Ingreso de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if(countele==1)
                {
                    if (!File.Exists("elementos.txt"))
                    {
                        TextWriter elemento = new StreamWriter("elementos.txt");
                        foreach (string i in elementos)
                        {
                            elemento.WriteLine(placa_general.ToUpper() + "|" + i);
                        }
                        elemento.Close();
                    }
                    else
                    {
                        TextReader elementosr = new StreamReader("elementos.txt");
                        string texto2 = elementosr.ReadToEnd();
                        elementosr.Close();
                        TextWriter elementosw = new StreamWriter("elementos.txt");
                        elementosw.Write(texto2);
                        foreach (string i in elementos)
                        {
                            elementosw.WriteLine(placa_general + "|" + i);
                            //doc = doc + placa_general + "|" + i+"\n";
                        }

                        elementosw.Close();
                    }
                }       
                else
                {
                    if (!File.Exists("elementos.txt"))
                    {
                        TextWriter elemento = new StreamWriter("elementos.txt");
                        elemento.WriteLine("");
                        elemento.Close();
                    }
                }
                
                Abrir_ingresar();

            }
            else
            {
                MessageBox.Show("No ha ingresado nada aún", "Agregar Vehículo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Agregar_vehiculo_Load(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}
