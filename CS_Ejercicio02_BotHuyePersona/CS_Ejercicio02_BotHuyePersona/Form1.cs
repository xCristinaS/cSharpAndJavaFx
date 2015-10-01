﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace CS_Ejercicio02_BotHuyePersona
{
    public partial class Form1 : Form
    {
        private static Point posRat = new Point(0, 0);
        private static int cont = 0;
        Persona persona1;
        LinkedList<PersonaBoton> lista = new LinkedList<PersonaBoton>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            persona1 = new Persona(this.Size);
            button1.Size = persona1.getTamanio();
            button1.Location = persona1.getPosicion();
            button1.BackgroundImage = persona1.getImagen();

            lista.AddLast(new PersonaBoton(persona1, button1));
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            cont++;
            if (!posRat.Equals(e.Location) && cont % 2 == 0)
            {
                foreach (PersonaBoton pB in lista)
                {
                    pB.getPersona().huir(e.Location, this.Size);
                    pB.actualizar();
                }
            }
            posRat = e.Location;
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            PersonaBoton eliminar = null;
            foreach (PersonaBoton pB in lista)
            {
                if (pB.getBoton().Equals(sender))
                {
                    pB.morir();
                    eliminar = pB;
                }
            }
            lista.Remove(eliminar);
            if (lista.Count == 0)
            {
                timer1.Dispose();
                MessageBox.Show("El monstruo ha acabado con todos!");
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lista.AddLast(new PersonaBoton(new Persona(this.Size)));
            this.Controls.Add(lista.Last.Value.getBoton());
            lista.Last.Value.getBoton().Click += new EventHandler(this.button1_Click);
            foreach (PersonaBoton pB in lista)
            {
                pB.getPersona().crecer();
                pB.actualizar();
            }
        }
    }
}
