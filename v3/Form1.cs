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

        public delegate void Enviar(string palabra);
        public delegate void EnviarTrozos(string [] trozos);

        List<Form4> formularios = new List<Form4>();

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        
        private void PonMensaje (string mensaje)
        {
            Registrado.Text= mensaje;
        }

        //private void PonConectados(string [] trozos)
        //{
        //    int n = Convert.ToInt32(trozos[1]);
        //    int i = 0;
        //    dataGridView1.Rows.Clear();
        //    dataGridView1.Refresh();

        //    while (i < n)
        //    {
        //        if (i < n - 1)
        //        {
        //            dataGridView1.Rows.Add();
        //        }
        //        dataGridView1.Rows[i].Cells[0].Value = trozos[i + 2];
        //        i++;
        //    }
        //}

        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                
                string [] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int numtrozos = trozos.Length;
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje;
                int nForm;
                switch (codigo)
                {
                    case 1: //Registra al nuevo usuario
                        mensaje = trozos[1].Split('\0')[0];
                        Enviar de = new Enviar(PonMensaje);
                        this.Invoke(de, new object[] { mensaje });
                        ThreadStart ts = delegate { PonerEnMarchaFormulario4(); };
                        Thread T = new Thread(ts);
                        T.Start();
                        //MessageBox.Show(mensaje);
                        break;

                    case 2: //Inicia sesión
                        mensaje = trozos[1].Split('\0')[0];
                        de = new Enviar(PonMensaje);
                        this.Invoke(de, new object[] { mensaje });
                        ts = delegate { PonerEnMarchaFormulario4(); };
                        T = new Thread(ts);
                        T.Start();
                        //MessageBox.Show(mensaje);
                        break;

                    case 3: //Consulta el jugador que ganó en menor tiempo
                        mensaje = trozos[1].Split('\0')[0];
                        nForm = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2];
                        formularios[nForm].TomaJugador(mensaje);

                        //de = new Enviar(PonMensaje);
                        //this.Invoke(de, new object[] { mensaje });
                        //MessageBox.Show(mensaje);
                        break;

                    case 4: //Consulta el menor tiempo en ganar del jugador "usuario"
                        mensaje = trozos[1].Split('\0')[0];
                        nForm = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2];
                        formularios[nForm].TomaTiempo(mensaje);

                        //de = new Enviar(PonMensaje);
                        //this.Invoke(de, new object[] { mensaje });
                        //MessageBox.Show(mensaje);
                        break;

                    case 5: //Consulta el número de partidas ganadas por el jugador "usuario"
                        mensaje = trozos[1].Split('\0')[0];
                        nForm = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2];
                        formularios[nForm].TomaPartidas(mensaje);

                        //de = new Enviar(PonMensaje);
                        //this.Invoke(de, new object[] { mensaje });
                        //MessageBox.Show(mensaje);
                        break;

                    case 6: //Notificación lista conectados
                        //EnviarTrozos de1 = new EnviarTrozos(PonConectados);
                        //this.Invoke(de1, new object[] { trozos });
                        mensaje = trozos[2];
                        nForm = Convert.ToInt32(trozos[1]);
                        int i = 3;
                        while (i<trozos.Length)
                        {
                            mensaje = mensaje + "/" + trozos[i];
                            i++;
                        }
                        formularios[nForm].TomaConectados(mensaje);
                        break;

                    case 7: //Recibe invitación del invitador
                        string invitador = trozos[2].Split('\0')[0];
                        nForm = Convert.ToInt32(trozos[1]);
                        Form2 f2 = new Form2();
                        f2.enviado += new Form2.Enviar(Ejecutar);
                        f2.textBox1.Text = invitador;
                        f2.ShowDialog();
                        if (textBox3.Text == "Rechazada")
                        {
                            string mensaje2 = "7/" + nForm + "/Rechazada/" + invitador;
                            // Enviamos al servidor  
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                            server.Send(msg);
                        }
                        else
                        {
                            string mensaje2 = "7/" + nForm + "/Aceptada/" + textBox3.Text + "/" + invitador;
                            //Enviamos al servidor  
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                            server.Send(msg);
                        }
                        break;

                    case 8: //Recibe contestación del invitado
                        mensaje = trozos[3].Split('\0')[0];
                        nForm = Convert.ToInt32(trozos[1]);
                        if (trozos[2] == "Rechazada")
                        {
                            string invitado = trozos[3];
                            MessageBox.Show(": ha finalizado la partida",invitado);
                        }
                        else
                        {
                            string invitado = trozos[4];
                            MessageBox.Show(": continua con la partida", invitado);
                            MessageBox.Show( trozos[3],"La palabra enviada por el jugador es:");
                            Form3 f3 = new Form3();
                            f3.enviado += new Form3.Enviar(Ejecutar);
                            f3.textBox1.Text = invitado;
                            f3.ShowDialog();
                            if (textBox3.Text == "Rechazada")
                            {
                                string mensaje2 = "8/" + nForm + "/Rechazada/" + invitado;
                                // Enviamos al servidor  
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                                server.Send(msg);
                            }
                            else
                            {
                                string mensaje2 = "8/" + nForm + "/Aceptada/" + textBox3.Text + "/" + invitado;
                                //Enviamos al servidor  
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                                server.Send(msg);
                            }

                        }
                        break;

                    case 9: //recibe contestación del invitador
                        mensaje = trozos[2].Split('\0')[0];
                        nForm = Convert.ToInt32(trozos[1]);
                        if (trozos[2] == "Rechazada")
                        {
                            string invitador2 = trozos[3];
                            MessageBox.Show(": ha finalizado la partida",invitador2);
                        }
                        else
                        {
                            string invitador2 = trozos[4];
                            MessageBox.Show(": continua con la partida", invitador2);
                            MessageBox.Show( trozos[3],"La palabra enviada por el jugador es:");
                            Form3 f3 = new Form3();
                            f3.enviado += new Form3.Enviar(Ejecutar);
                            f3.textBox1.Text = invitador2;
                            f3.ShowDialog();
                            if (textBox3.Text == "Rechazada")
                            {
                                string mensaje2 = "7/" + nForm + "/Rechazada/" + invitador2;
                                // Enviamos al servidor  
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                                server.Send(msg);
                            }
                            else
                            {
                                string mensaje2 = "7/" + nForm + "/Aceptada/" + textBox3.Text + "/" + invitador2;
                                //Enviamos al servidor  
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                                server.Send(msg);
                            }

                        }
                        break;

                    
                }
            }
        }

        public void Ejecutar(string palabra)
        {
            textBox3.Text = palabra;
        }

        //Botón para conectar con el servidor
        private void button1_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.103");
            IPEndPoint ipep = new IPEndPoint(direc, 9400);

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

        ////Consulta el ganador que ganó en menor tiempo
        //private void radioButton1_Click(object sender, EventArgs e)
        //{
        //    // Quiere realizar la consulta escogida
        //    string mensaje = "3/";
        //    // Enviamos al servidor el user tecleado 
        //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
        //    server.Send(msg);

        //}


        ////Consulta el menor tiempo en ganar del usuario dado
        //private void radioButton2_Click(object sender, EventArgs e)
        //{
        //    string mensaje = "4/" + textBox1.Text;
        //    // Enviamos al servidor el user tecleado 
        //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
        //    server.Send(msg);

        //}

        ////Consulta las partidas ganadas por el usuario dado
        //private void radioButton3_Click(object sender, EventArgs e)
        //{
        //    string mensaje = "5/" + textBox1.Text;
        //    // Enviamos al servidor el user tecleado 
        //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
        //    server.Send(msg);

        //}

        ////Envía invitación para crear partida con el invitado
        //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    var invitado = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
        //    MessageBox.Show(invitado.ToString(), "Has invitado a:");
        //    string mensaje = "6/" + invitado;
        //    // Enviamos al servidor el user tecleado 
        //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
        //    server.Send(msg);
        //}

        //Función para abrir Form4
        private void PonerEnMarchaFormulario4()
        {
            int cont = formularios.Count;
            string usuario= textBox1.Text;
            Form4 f4 = new Form4(cont, server, usuario);
            formularios.Add(f4);
            f4.ShowDialog();
        }

        //Solicitamos dar de baja un usuario
        private void button5_Click(object sender, EventArgs e)
        {
            // Quiere realizar un inicio de sesión
            string mensaje = "10/" + textBox1.Text + "/" + textBox2.Text;
            // Enviamos al servidor el user tecleado y la contraseña
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        
    }
}
