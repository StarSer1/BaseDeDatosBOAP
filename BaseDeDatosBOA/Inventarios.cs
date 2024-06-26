﻿using BOAEntidad;
using BOALogica;
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
    public partial class Inventarios : Form
    {
        private CLogica logica;
        List<Inventario> inventarios = null;

        public Inventarios()
        {
            logica = new CLogica();
            InitializeComponent();
            logica.TurnOffLabels(label2, label3, label4, label5);
            logica.TurnOffTxtB(txtIdComputadora, txtFechaLlegada, txtPrecioLLegada, txtStock);

            ValidadorForm.AgregarValidacion(btnInsertar, txtFechaLlegada, txtIdComputadora, txtIdInventario, txtPrecioLLegada, txtStock);
            ValidadorForm.AgregarValidacion(btnModificar, txtFechaLlegada, txtIdComputadora, txtIdInventario, txtPrecioLLegada, txtStock);//agregado
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
         txtIdInventario.Text,
         inventarios,
         item => item.IdInventario.ToString(),
         () => new Inventario
         {
             IdInventario = txtIdInventario.Text,
             IdComputadora = txtIdComputadora.Text,
             FechaLlegada = txtFechaLlegada.Text,
             PrecioLlegada = int.Parse(txtPrecioLLegada.Text),
             Stock = int.Parse(txtStock.Text)
         },
         @"^I\d+$",
         () => logica.ClearTextBoxs(txtIdInventario, txtPrecioLLegada, txtStock, txtFechaLlegada, txtIdComputadora),
         new Control[] { txtIdInventario },
         new Control[] { txtIdComputadora, txtFechaLlegada, txtPrecioLLegada, txtStock }
     );
        }

        private void Inventarios_Load(object sender, EventArgs e)
        {
            inventarios = logica.LoadDataInventario(inventarios, dgvVentas);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            List<Computadora> comp = logica.ObtenerComputadoras();
            bool checkExistence = logica.CheckExistenciaInventario(txtIdComputadora.Text, comp);
            if (checkExistence)
            {
                logica.ModificarEntidad(
                    txtIdInventario.Text,
                    @"^I\d+$",
                    () => new Inventario
                    {
                        IdInventario = txtIdInventario.Text,
                        IdComputadora = txtIdComputadora.Text,
                        FechaLlegada = txtFechaLlegada.Text,
                        PrecioLlegada = int.Parse(txtPrecioLLegada.Text),
                        Stock = int.Parse(txtStock.Text)
                    },
                    logica.ModificarInventario,
                    new Control[] { label2, label3, label4, label5 },
                    new Control[] { txtIdComputadora, txtFechaLlegada, txtPrecioLLegada, txtStock },
                    new Control[] { txtIdInventario, txtPrecioLLegada, txtStock, txtFechaLlegada, txtIdComputadora }
                );
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AbrirEliminar("idInventario");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            inventarios = logica.LoadDataInventario(inventarios, dgvVentas);
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "INVENTARIO";
            formConsulta.ShowDialog();
        }

        private void txtPrecioLLegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            logica.SoloNumeros(sender, e);
        }

       
        private void txtIdInventario_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdComputadora_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtFechaLlegada_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtPrecioLLegada_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            logica.ProcesarVerificacion(
         txtIdInventario.Text,
         inventarios,
         item => item.IdInventario.ToString(),
         item =>
         {
             txtIdInventario.Text = item.IdInventario;
             txtIdComputadora.Text = item.IdComputadora;
             txtFechaLlegada.Text = item.FechaLlegada;
             txtPrecioLLegada.Text = item.PrecioLlegada.ToString();
             txtStock.Text = item.Stock.ToString();
         },
         item =>
         {
             txtIdInventario.Enabled = false;
             btnInsertar.Enabled = false;
         },
         this.Controls.Cast<Control>().ToArray()
     );
        }
    }
}
