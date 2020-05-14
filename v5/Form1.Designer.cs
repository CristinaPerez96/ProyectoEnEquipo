namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Jugador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Respuestas = new System.Windows.Forms.Label();
            this.Notificacion = new System.Windows.Forms.Label();
            this.frasebx = new System.Windows.Forms.TextBox();
            this.Pasivo = new System.Windows.Forms.Button();
            this.Activo = new System.Windows.Forms.Button();
            this.Enviar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Conectar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(134, 18);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 38);
            this.button2.TabIndex = 1;
            this.button2.Text = "Desconectar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(23, 185);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 36);
            this.button3.TabIndex = 2;
            this.button3.Text = "Registrarse";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(134, 185);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 36);
            this.button4.TabIndex = 3;
            this.button4.Text = "Iniciar sesión";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 85);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 127);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(79, 85);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 20);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(79, 124);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(76, 20);
            this.textBox2.TabIndex = 7;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(31, 235);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(286, 17);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Dame el nombre del jugador que ganó en menor tiempo";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(31, 271);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(251, 17);
            this.radioButton2.TabIndex = 9;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Dame el menor timepo en el que ganó \"usuario\"";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Click += new System.EventHandler(this.radioButton2_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(31, 307);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(231, 17);
            this.radioButton3.TabIndex = 10;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Número de partias ganadas por el \"usuario\"";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Click += new System.EventHandler(this.radioButton3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Jugador});
            this.dataGridView1.Location = new System.Drawing.Point(340, 35);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(180, 217);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Jugador
            // 
            this.Jugador.HeaderText = "Jugador conectado";
            this.Jugador.Name = "Jugador";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(145, 376);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(84, 20);
            this.textBox3.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 379);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Palabra enviada:";
            // 
            // Respuestas
            // 
            this.Respuestas.Location = new System.Drawing.Point(566, 307);
            this.Respuestas.Margin = new System.Windows.Forms.Padding(3, 0, 8, 0);
            this.Respuestas.Name = "Respuestas";
            this.Respuestas.Size = new System.Drawing.Size(162, 85);
            this.Respuestas.TabIndex = 23;
            this.Respuestas.Click += new System.EventHandler(this.Respuestas_Click);
            // 
            // Notificacion
            // 
            this.Notificacion.Location = new System.Drawing.Point(353, 307);
            this.Notificacion.Name = "Notificacion";
            this.Notificacion.Size = new System.Drawing.Size(141, 85);
            this.Notificacion.TabIndex = 22;
            // 
            // frasebx
            // 
            this.frasebx.Location = new System.Drawing.Point(534, 36);
            this.frasebx.Name = "frasebx";
            this.frasebx.Size = new System.Drawing.Size(138, 20);
            this.frasebx.TabIndex = 21;
            // 
            // Pasivo
            // 
            this.Pasivo.Location = new System.Drawing.Point(534, 146);
            this.Pasivo.Name = "Pasivo";
            this.Pasivo.Size = new System.Drawing.Size(75, 58);
            this.Pasivo.TabIndex = 20;
            this.Pasivo.Text = "Pasar a modo Pasivo";
            this.Pasivo.UseVisualStyleBackColor = true;
            this.Pasivo.Click += new System.EventHandler(this.Pasivo_Click);
            // 
            // Activo
            // 
            this.Activo.Location = new System.Drawing.Point(534, 82);
            this.Activo.Name = "Activo";
            this.Activo.Size = new System.Drawing.Size(75, 58);
            this.Activo.TabIndex = 19;
            this.Activo.Text = "Pasar a modo Activo";
            this.Activo.UseVisualStyleBackColor = true;
            this.Activo.Click += new System.EventHandler(this.Activo_Click);
            // 
            // Enviar
            // 
            this.Enviar.Location = new System.Drawing.Point(534, 210);
            this.Enviar.Name = "Enviar";
            this.Enviar.Size = new System.Drawing.Size(75, 55);
            this.Enviar.TabIndex = 18;
            this.Enviar.Text = "Enviar";
            this.Enviar.UseVisualStyleBackColor = true;
            this.Enviar.Click += new System.EventHandler(this.button7_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 275);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Notificaciones";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(546, 275);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Respuestas";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 465);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Respuestas);
            this.Controls.Add(this.Notificacion);
            this.Controls.Add(this.frasebx);
            this.Controls.Add(this.Pasivo);
            this.Controls.Add(this.Activo);
            this.Controls.Add(this.Enviar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jugador;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Respuestas;
        private System.Windows.Forms.Label Notificacion;
        private System.Windows.Forms.TextBox frasebx;
        private System.Windows.Forms.Button Pasivo;
        private System.Windows.Forms.Button Activo;
        private System.Windows.Forms.Button Enviar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}

