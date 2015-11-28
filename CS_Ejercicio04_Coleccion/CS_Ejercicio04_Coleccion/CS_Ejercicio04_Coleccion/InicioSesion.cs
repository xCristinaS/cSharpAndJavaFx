﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Ejercicio04_Coleccion {
    public partial class FormInicioSesion : Form {
        public FormInicioSesion() {
            InitializeComponent();
        }

        private void InicioSesion_Load(object sender, EventArgs e) {
            CenterToScreen();
        }

        private void imgCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e) {
            if (!txtUsuario.Text.Equals("") && !txtClave.Text.Equals("") && e.KeyChar == (char)Keys.Enter) {
                SqlConnection conexion = BddConection.newConnection();
                string usuario = txtUsuario.Text; string clave = txtClave.Text;
                string select = string.Format("select count(*) from usuario where nick = '{0}' and pass = '{1}';", usuario, clave);
                SqlCommand orden = new SqlCommand(select, conexion);

                int resultado = (int)orden.ExecuteScalar();
                if (resultado == 1) {
                    this.Hide();
                    Libros form2 = new Libros(usuario);
                    form2.FormClosed += (s, args) => this.Show();
                    form2.Show();
                } else
                    MessageBox.Show("No fue posible establecer la conexión.");

                BddConection.closeConnection(conexion);
            }
        }
    }
}
