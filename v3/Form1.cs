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

    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                
                string [] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje;
                switch (codigo)
                {
                    case 1: //Registra al nuevo usuario
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show(mensaje);
                        break;

                    case 2: //Inicia sesión
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show(mensaje);
                        break;

                    case 3: //Consulta el jugador que ganó en menor tiempo
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show(mensaje);
                        break;

                    case 4: //Consulta el menor tiempo en ganar del jugador "usuario"
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show(mensaje);
                        break;

                    case 5: //Consulta el número de partidas ganadas por el jugador "usuario"
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show(mensaje);
                        break;

                    case 6: //Notificación lista conectados
                        int i = 0;
                        int n;
                        dataGridView1.Rows.Clear();
                        dataGridView1.Refresh();

                        if (trozos[1] == null)
                        {
                            MessageBox.Show("No hay conectados");
                        }
                        else
                        {
                            mensaje = trozos[1].Split('/')[0];
                            int trozo = Int32.Parse(mensaje);
                            while (i < trozo - 1)
                            {
                                n = dataGridView1.Rows.Add();
                                i++;
                            }
                        }
                        for (i = 0; i < trozos.Length-2; i++)
                        {
                            dataGridView1.Rows[i].Cells[0].Value = trozos[i+2];
                        }
                        break;

                }
            }
        }

        //Botón para conectar con el servidor
        private void button1_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPEndPoint ipep = new IPEndPoint(direc, 50057);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            //pongo en marcha el thread que atenderá los mensajes de servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
        }

        //Botón para desconectar
        private void button2_Click(object sender, EventArgs e)
        {
            // Se terminó el servicio. 
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            this.Close();
        }

        //Botón para registrar al nuevo usuario
        private void button3_Click(object sender, EventArgs e)
        {
            // Quiere realizar un registro en la BBDD
            string mensaje = "1/" + textBox1.Text + "/" + textBox2.Text;
            // Enviamos al servidor el user tecleado y la contraseña
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        //Botón para iniciar sesión
        private void button4_Click(object sender, EventArgs e)
        {
            // Quiere realizar un inicio de sesión
            string mensaje = "2/" + textBox1.Text + "/" + textBox2.Text;
            // Enviamos al servidor el user tecleado y la contraseña
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        //Consulta el ganador que ganó en menor tiempo
        private void radioButton1_Click(object sender, EventArgs e)
        {
            // Quiere realizar la consulta escogida
            string mensaje = "3/";
            // Enviamos al servidor el user tecleado 
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }


        //Consulta el menor tiempo en ganar del usuario dado
        private void radioButton2_Click(object sender, EventArgs e)
        {
            string mensaje = "4/" + textBox1.Text;
            // Enviamos al servidor el user tecleado 
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        //Consulta las partidas ganadas por el usuario dado
        private void radioButton3_Click(object sender, EventArgs e)
        {
            string mensaje = "5/" + textBox1.Text;
            // Enviamos al servidor el user tecleado 
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        

    }
}
