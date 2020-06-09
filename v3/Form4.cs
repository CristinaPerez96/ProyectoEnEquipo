using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        int nForm;
        Socket server;
        string usuario;

        public delegate void Enviar(string mensaje);

        public Form4(int nForm, Socket server, string usuario)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            this.usuario = usuario;
        }

        //Consulta el ganador que ganó en menor tiempo
        private void radioButton4_Click(object sender, EventArgs e)
        {
            // Quiere realizar la consulta escogida
            string mensaje = "3/" + nForm + "/";
            // Enviamos al servidor el user tecleado 
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        //Consulta el menor tiempo en ganar del usuario dado
        private void radioButton2_Click(object sender, EventArgs e)
        {
            string mensaje = "4/" + nForm + "/" + textBox1.Text;
            // Enviamos al servidor el user tecleado 
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        //Consulta las partidas ganadas por el usuario dado
        private void radioButton3_Click(object sender, EventArgs e)
        {
            string mensaje = "5/" + nForm + "/" + textBox1.Text;
            // Enviamos al servidor el user tecleado 
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        //Envía invitación para crear partida con el invitado
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var invitado = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            MessageBox.Show(invitado.ToString(), "Has invitado a:");
            string mensaje = "6/" + nForm + "/" + invitado;
            // Enviamos al servidor el user tecleado 
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label2.Text = usuario;
            label5.Text = nForm.ToString();
        }

        public void TomaJugador(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public void TomaTiempo(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public void TomaPartidas(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public void TomaConectados(string mensaje)
        {
            Enviar de = new Enviar(PonConectados);
            this.Invoke(de, new object[] { mensaje });
        }

        private void PonConectados(string mensaje)
        {
            string[] trozos = mensaje.Split('/');
            int n = Convert.ToInt32(trozos[0]);
            int i = 0;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            while (i < n)
            {
                if (i < n - 1)
                {
                    dataGridView1.Rows.Add();
                }
                dataGridView1.Rows[i].Cells[0].Value = trozos[i + 1];
                i++;
            }
        }

        

        
    }
}
