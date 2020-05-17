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
    public partial class Control : Form
    {

        
        public Control()
        {
            InitializeComponent();
            string[,] vehiculo;
            List<string> elementos;
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            vehiculo = pp.vehiculo;
            elementos = pp.elementos;
            label7.Text = vehiculo[0,0];
            label8.Text = vehiculo[0, 1];
            label10.Text =vehiculo[0, 2];
            label9.Text = vehiculo[0, 3];
            numericUpDown1.Value = Convert.ToInt32(vehiculo[0, 4]);
            label14.Text = vehiculo[0, 5] + "$";
            textBox4.Text = vehiculo[0, 6];

            if(elementos.Count==0)
            {
                label15.Text = "No existen elementos aún";
            }
            else
            {
                string[,] valores = new string[elementos.Count, 4];
                for(int a=0;a<elementos.Count;a++)
                {
                    string[] hola = elementos[a].Split('|');
                    int o = 0;
                    foreach(string h in hola)
                    {
                        valores[a, o] = h;
                        o++;
                    }
                }

                string t1,t2,t3,t0;
                for (int a = 1; a < elementos.Count; a++)
                {
                    for (int b = elementos.Count- 1; b >= a; b--)
                    {
                        if (Convert.ToInt32(valores[b - 1,3]) > Convert.ToInt32(valores[b,3]))
                        {
                            t0 = valores[b - 1,0];
                            t1 = valores[b - 1, 1];
                            t2 = valores[b - 1, 2];
                            t3 = valores[b - 1, 3];
                            valores[b - 1,0] = valores[b,0];
                            valores[b - 1, 1] = valores[b, 1];
                            valores[b - 1, 2] = valores[b, 2];
                            valores[b - 1, 3] = valores[b, 3];
                            valores[b,0] = t0;
                            valores[b, 1] = t1;
                            valores[b, 2] = t2;
                            valores[b, 3] = t3;
                        }
                    }
                }

                label15.Text = "";
                for(int b=0;b<elementos.Count;b++)
                {
                    label15.Text = label15.Text + "Es necesario hacer el cambio de " + valores[b, 1] + " dentro de " + valores[b, 3].Split('\r')[0] +" Km"+ "\n";
                }
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string comentario = textBox4.Text;
            string kilometraje = Convert.ToString(numericUpDown1.Value);
            Principal pp = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            int diferencia = Convert.ToInt32(numericUpDown1.Value) - Convert.ToInt32(pp.vehiculo[0, 4]);

            int count = 0;
            int posicion = 0;
            TextReader vehiculos = new StreamReader("vehiculos.txt");
            string[] lineas = vehiculos.ReadToEnd().Split('\n');
            vehiculos.Close();
            foreach (string i in lineas)
            {
                string[] argumentos = i.Split('|');
                if (argumentos[0] == pp.vehiculo[0,0]&& argumentos[1] == pp.vehiculo[0, 1]&& argumentos[2] == pp.vehiculo[0, 2]&& argumentos[3] == pp.vehiculo[0, 3]&& argumentos[4] == pp.vehiculo[0, 4]&& argumentos[5] == pp.vehiculo[0, 5]&& argumentos[6] == pp.vehiculo[0, 6]&& argumentos[7] == pp.vehiculo[0, 7])
                {
                    count = 1;
                    break;
                }
                posicion++;
            }
            string tt="";
            int count2 = 0;
            if(count==1)
            {
                TextWriter vehiculosw = new StreamWriter("vehiculos.txt");
                foreach (string i in lineas)
                {
                    if(i!=""&&i!="\r")
                    {
                        if (count2 != posicion)
                        {
                            vehiculosw.WriteLine(i.Split('\r')[0]);                            
                        }
                        else
                        {
                            string[] jjj = i.Split('|');
                            jjj[6] = comentario;
                            jjj[4] = kilometraje;
                            vehiculosw.WriteLine(jjj[0] + "|" + jjj[1] + "|" + jjj[2] + "|" + jjj[3] + "|" + jjj[4] + "|" + jjj[5] + "|" + jjj[6] + "|" + jjj[7].Split('\r')[0]);

                        }                        
                    }
                    count2++;
                }
                vehiculosw.Close();
            }


            TextReader elementosrr = new StreamReader("elementos.txt");
            string[] lineasele = elementosrr.ReadToEnd().Split('\n');
            List<string> elenuevo = new List<string>();
            elementosrr.Close();
            foreach (string i in lineasele)
            {
                if (i != "" && i != "\r")
                {
                    string[] argumentos = i.Split('|');
                    if (argumentos[0] == pp.vehiculo[0, 0])
                    {
                        argumentos[3] = Convert.ToString(Convert.ToInt32(argumentos[3]) - diferencia);
                        elenuevo.Add(argumentos[0] + "|" + argumentos[1] + "|" + argumentos[2] + "|" + argumentos[3].Split('\r')[0]);
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
                if (i!= ""&&i!="\r")
                {
                    string[] j = i.Split('|');

                    if (j[0] == pp.placas)
                    {
                        pp.elementos.Add(i);
                    }


                }
            }
            pp.vehiculo[0, 6] = comentario;
            pp.vehiculo[0, 4] = kilometraje;

            //REINICIO

            string[,] vehiculo;
            List<string> elementos;
            Principal pp4 = Application.OpenForms.OfType<Principal>().SingleOrDefault();
            vehiculo = pp4.vehiculo;
            elementos = pp4.elementos;
            label7.Text = vehiculo[0, 0];
            label8.Text = vehiculo[0, 1];
            label10.Text = vehiculo[0, 2];
            label9.Text = vehiculo[0, 3];
            numericUpDown1.Value = Convert.ToInt32(vehiculo[0, 4]);
            label14.Text = vehiculo[0, 5] + "$";
            textBox4.Text = vehiculo[0, 6];

            if (elementos.Count == 0)
            {
                label15.Text = "No existen elementos aún";
            }
            else
            {
                string[,] valores = new string[elementos.Count, 4];
                for (int a = 0; a < elementos.Count; a++)
                {
                    string[] hola = elementos[a].Split('|');
                    int o = 0;
                    foreach (string h in hola)
                    {
                        valores[a, o] = h;
                        o++;
                    }
                }

                string t1, t2, t3, t0;
                for (int a = 1; a < elementos.Count; a++)
                {
                    for (int b = elementos.Count - 1; b >= a; b--)
                    {
                        if (Convert.ToInt32(valores[b - 1, 3]) > Convert.ToInt32(valores[b, 3]))
                        {
                            t0 = valores[b - 1, 0];
                            t1 = valores[b - 1, 1];
                            t2 = valores[b - 1, 2];
                            t3 = valores[b - 1, 3];
                            valores[b - 1, 0] = valores[b, 0];
                            valores[b - 1, 1] = valores[b, 1];
                            valores[b - 1, 2] = valores[b, 2];
                            valores[b - 1, 3] = valores[b, 3];
                            valores[b, 0] = t0;
                            valores[b, 1] = t1;
                            valores[b, 2] = t2;
                            valores[b, 3] = t3;
                        }
                    }
                }

                label15.Text = "";
                for (int b = 0; b < elementos.Count; b++)
                {
                    label15.Text = label15.Text + "Es necesario hacer el cambio de " + valores[b, 1] + " dentro de " + valores[b, 3].Split('\r')[0] + " Km" + "\n";
                }
            }
            label1.Text = "Se ha cambiado el kilometraje\ny se guardo el comentario";
        }

        private void Control_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
