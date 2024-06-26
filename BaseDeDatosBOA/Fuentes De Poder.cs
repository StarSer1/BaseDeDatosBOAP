﻿using BOAEntidad;
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
    public partial class Fuentes_De_Poder : Form
    {
        private CLogica logica;
        List<FuentePoder> fuentesPoder = null;

        public Fuentes_De_Poder()
        {
            logica = new CLogica();
            InitializeComponent();
            logica.TurnOffLabels(this.Controls.OfType<Label>().Where((label) => label.Name.ToString() != "label1").ToArray());
            logica.TurnOffTxtB(this.Controls.OfType<Guna2TextBox>().Where((button) => button.Name.ToString() != "txtIdFuentePoder").ToArray());

            ValidadorForm.AgregarValidacion(btnInsertar, txtIdFuentePoder, txtMarca, txtModelo, txtPotencia, txtTipo, txtCertificacion);
            ValidadorForm.AgregarValidacion(btnModificar, txtIdFuentePoder, txtMarca, txtModelo, txtPotencia, txtTipo, txtCertificacion);
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            logica.ProcesarInsertar(
         txtIdFuentePoder.Text,
         fuentesPoder,
         item => item.IdFuentePoder.ToString(),
         () => new FuentePoder
         {
             IdFuentePoder = txtIdFuentePoder.Text,
             Marca = txtMarca.Text,
             Modelo = txtModelo.Text,
             Potencia = int.Parse(txtPotencia.Text),
             Tipo = txtTipo.Text,
             Certificacion = txtCertificacion.Text
         },
         @"^F\d+$",
         () => logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray()),
         new Control[] { txtIdFuentePoder },
         this.Controls.OfType<Control>().Where(c => c.Name != "txtIdFuentePoder" && c is Guna2TextBox).ToArray()
     );
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            logica.ModificarEntidad(
        txtIdFuentePoder.Text,
        @"^F\d+$",
        () => new FuentePoder
        {
            IdFuentePoder = txtIdFuentePoder.Text,
            Marca = txtMarca.Text,
            Modelo = txtModelo.Text,
            Potencia = int.Parse(txtPotencia.Text),
            Tipo = txtTipo.Text,
            Certificacion = txtCertificacion.Text,
        },
        logica.ModificarFuentesPoder,
        this.Controls.OfType<Label>().Where(label => label.Name != "label1").ToArray(),
        this.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().Where(txtBox => txtBox.Name != "txtIdFuentePoder").ToArray(),
        this.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().ToArray()
    );
        }

        private void Fuentes_De_Poder_Load(object sender, EventArgs e)
        {
            fuentesPoder = logica.LoadDataFuenteDePoder(fuentesPoder, dgvFuentesDePoder);
        }
        private void AbrirEliminar(string tablaDondeViene)
        {
            Eliminar formEliminar = new Eliminar();
            formEliminar.tablaDeDondeViene = tablaDondeViene;
            formEliminar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AbrirEliminar("idFuentePoder");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            fuentesPoder = logica.LoadDataFuenteDePoder(fuentesPoder, dgvFuentesDePoder);
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "FUENTEPODER";
            formConsulta.ShowDialog();
        }

        private void txtPotencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            logica.SoloNumeros(sender, e);
        }

        
        private void txtIdFuentePoder_TextChanged(object sender, EventArgs e)
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

        private void txtPotencia_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtTipo_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtCertificacion_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            logica.ProcesarVerificacion(
        txtIdFuentePoder.Text,
        fuentesPoder,
        item => item.IdFuentePoder.ToString(),
        item =>
        {
            txtIdFuentePoder.Text = item.IdFuentePoder.ToString();
            txtMarca.Text = item.Marca.ToString();
            txtModelo.Text = item.Modelo.ToString();
            txtPotencia.Text = item.Potencia.ToString();
            txtTipo.Text = item.Tipo.ToString();
            txtCertificacion.Text = item.Certificacion.ToString();
        },
        item =>
        {
            txtIdFuentePoder.Enabled = false;
            btnInsertar.Enabled = false;
        },
        this.Controls.Cast<Control>().ToArray()
    );
        }
    }
}
