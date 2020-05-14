namespace WindowsFormsApplication1
{
    partial class Form3
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Notificacion = new System.Windows.Forms.Label();
            this.Respuestas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(318, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Enviar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(40, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 58);
            this.button2.TabIndex = 1;
            this.button2.Text = "Pasar a modo Activo";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(149, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 58);
            this.button3.TabIndex = 2;
            this.button3.Text = "Pasar a modo Pasivo";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(40, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(138, 20);
            this.textBox1.TabIndex = 3;
            // 
            // Notificacion
            // 
            this.Notificacion.Location = new System.Drawing.Point(52, 213);
            this.Notificacion.Name = "Notificacion";
            this.Notificacion.Size = new System.Drawing.Size(126, 148);
            this.Notificacion.TabIndex = 7;
            this.Notificacion.Text = "label1";
            // 
            // Respuestas
            // 
            this.Respuestas.Location = new System.Drawing.Point(244, 213);
            this.Respuestas.Margin = new System.Windows.Forms.Padding(3, 0, 8, 0);
            this.Respuestas.Name = "Respuestas";
            this.Respuestas.Size = new System.Drawing.Size(127, 148);
            this.Respuestas.TabIndex = 10;
            this.Respuestas.Text = "label2";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 408);
            this.Controls.Add(this.Respuestas);
            this.Controls.Add(this.Notificacion);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Notificacion;
        private System.Windows.Forms.Label Respuestas;
    }
}