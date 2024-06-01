using BOAEntidad;
using BOALogica;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BOALogica.CLogica;

namespace BaseDeDatosBOA
{
    public partial class Graficas : Form
    {
        private CLogica logica;
        List<Grafica> graficas = null;

        public Graficas()
        {
            logica = new CLogica();
            InitializeComponent();

            logica.TurnOffLabels(label2, label3, label4, label5);
            logica.TurnOffTxtB(txtMarca, txtModelo, txtTipo, txtVram);

            ValidadorForm.AgregarValidacion(btnInsertar, txtIdGrafica, txtMarca, txtModelo, txtTipo, txtVram);
            ValidadorForm.AgregarValidacion(btnModificar, txtIdGrafica, txtMarca, txtModelo, txtTipo, txtVram);
        }
        

        private void AbrirEliminar(string tablaDondeViene)
        {
            Eliminar formEliminar = new Eliminar();
            formEliminar.tablaDeDondeViene = tablaDondeViene;
            formEliminar.ShowDialog();
        }


        private void btnInsertar_Click(object sender, EventArgs e)
        {
            logica.ProcesarInsertar(
        txtIdGrafica.Text,
        graficas,
        item => item.IdGrafica.ToString(),
        () => new Grafica
        {
            IdGrafica = txtIdGrafica.Text,
            Marca = txtMarca.Text,
            Modelo = txtModelo.Text,
            Tipo = txtTipo.Text,
            Vram = int.Parse(txtVram.Text)
        },
        @"^G\d+$",
        () => logica.ClearTextBoxs(txtIdGrafica, txtMarca, txtModelo, txtTipo, txtVram),
        new Control[] { txtIdGrafica },
        new Control[] { txtMarca, txtModelo, txtTipo, txtVram }
    );
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            logica.ModificarEntidad(
        txtIdGrafica.Text,
        @"^G\d+$",
        () => new Grafica
        {
            IdGrafica = txtIdGrafica.Text,
            Marca = txtMarca.Text,
            Modelo = txtModelo.Text,
            Tipo = txtTipo.Text,
            Vram = int.Parse(txtVram.Text),
        },
        logica.ModificarGraficas,
        new Control[] { label2, label3, label4, label5 },
        new Control[] { txtMarca, txtModelo, txtTipo, txtVram },
        new Control[] { txtIdGrafica, txtMarca, txtModelo, txtTipo, txtVram }
    );
        }

        private void Graficas_Load(object sender, EventArgs e)
        {
            graficas = logica.LoadDataGraficas(graficas, dgvGraficas);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AbrirEliminar("idGrafica");
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            graficas = logica.LoadDataGraficas(graficas, dgvGraficas);
        }

        private void btnConsulta_Click_1(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "GRAFICA";
            formConsulta.ShowDialog();
        }

        private void txtVram_KeyPress(object sender, KeyPressEventArgs e)
        {
            logica.SoloNumeros(sender, e);
        }

        private void txtIdGrafica_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtTipo_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtVram_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            logica.ProcesarVerificacion(
       txtIdGrafica.Text,
       graficas,
       item => item.IdGrafica.ToString(),
       item =>
       {
           txtIdGrafica.Text = item.IdGrafica.ToString();
           txtMarca.Text = item.Marca.ToString();
           txtModelo.Text = item.Modelo.ToString();
           txtTipo.Text = item.Tipo.ToString();
           txtVram.Text = item.Vram.ToString();
       },
       item =>
       {
           txtIdGrafica.Enabled = false;
           btnInsertar.Enabled = false;
       },
       this.Controls.Cast<Control>().ToArray()
   );
        }
    }
}