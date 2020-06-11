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
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public delegate void Enviar(string palabra);
        public event Enviar enviado;
        int contador;
        public Form3()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            enviado("Aceptada");
            this.Close();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            enviado("Rechazada");
            this.Close();
        }

        

        private void Form3_Load(object sender, EventArgs e)
        {
            //seria un timer general podemos poner los 5 minutos que dura como máximo la partida
            contador = 50;
            this.LBContador.Text = Convert.ToInt32(contador).ToString();
            this.timer1.Enabled = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // para que una vez termine mi time me abre un nuevo rormulario 
            contador = contador - 1;
            progressBar1.Value--;
            this.LBContador.Text = contador.ToString();
            if (contador == 0)
            {
                this.timer1.Enabled = false;
                Form5 formulario = new Form5();
                formulario.Show();
            }

        }
    }
}
