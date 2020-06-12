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
    }
}
