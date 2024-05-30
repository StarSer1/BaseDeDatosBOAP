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
    public partial class Procesadores : Form
    {
        private CLogica logica;
        List<Procesador> procesador = null;

        public Procesadores()
        {
            logica = new CLogica();
            InitializeComponent();
            logica.TurnOffLabels(label2, label3);
            logica.TurnOffTxtB(txtMarca, txtModelo);

            ValidadorForm.AgregarValidacion(btnInsertar, txtIdProcesador, txtMarca, txtModelo);
            ValidadorForm.AgregarValidacion(btnModificar, txtIdProcesador, txtMarca, txtModelo);
        }
        public void LoadData()
        {
            try
            {
                this.procesador = logica.ObtenerProcesadores();
                List<Procesador> procesador = logica.ObtenerProcesadores();
                dgvProcesadores.DataSource = procesador;
                dgvProcesadores.Tag = "procesador";
                dgvProcesadores.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(logica.dgvVentasChangeSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
       
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            bool checkFormat = logica.CheckAllFormats(txtIdProcesador.Text, @"^P\d+$");
            if (checkFormat == false)
            {
                MessageBox.Show("error de formato en ID");
            }
            else
            {
                bool checkId = logica.VerifyID(txtIdProcesador.Text, procesador, item => item.IdProcesador.ToString());
                if (checkId == true)
                {
                    Procesador procesador = null;
                    try
                    {
                        procesador = new Procesador
                        {
                            IdProcesador = txtIdProcesador.Text,
                            Marca = txtMarca.Text,
                            Modelo = txtModelo.Text,
                        };
                        logica.RegistrarProcesador(procesador);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                
                logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray());
                txtIdProcesador.Enabled = true;
                logica.TurnOffLabels(this.Controls.OfType<Label>().Where((label) => label.Name.ToString() != "label1").ToArray());
                logica.TurnOffTxtB(this.Controls.OfType<Guna2TextBox>().Where((button) => button.Name.ToString() != "txtIdProcesador").ToArray());
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            bool checkFormat = logica.CheckAllFormats(txtIdProcesador.Text, @"^P\d+$");
            if (checkFormat == false)
            {
                MessageBox.Show("error de formato en ID");
            }
            else
            {
                Procesador procesador = null;
                try
                {
                    procesador = new Procesador
                    {
                        IdProcesador = txtIdProcesador.Text,
                        Marca = txtMarca.Text,
                        Modelo = txtModelo.Text,
                    };
                    logica.ModificarProcesadores(procesador);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
                txtIdProcesador.Enabled = true;
                btnInsertar.Enabled = true;
                logica.TurnOffLabels(this.Controls.OfType<Label>().Where((label) => label.Name.ToString() != "label1").ToArray());
                logica.TurnOffTxtB(this.Controls.OfType<Guna2TextBox>().Where((button) => button.Name.ToString() != "txtIdProcesador").ToArray());
                logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray());
            }
        }

        private void Procesadores_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void AbrirEliminar(string tablaDondeViene)
        {
            Eliminar formEliminar = new Eliminar();
            formEliminar.tablaDeDondeViene = tablaDondeViene;
            formEliminar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AbrirEliminar("idProcesador");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "PROCESADOR";
            formConsulta.ShowDialog();
        }

        private void txtIdProcesador_TextChanged(object sender, EventArgs e)
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

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            bool checkId = logica.VerifyID(txtIdProcesador.Text, procesador, item => item.IdProcesador.ToString());
            if (checkId == true)
            {


                logica.TurnOnLabels(this.Controls.OfType<Label>().Where((label) => label.Name.ToString() != "label1").ToArray());
                logica.TurnOnTxtB(this.Controls.OfType<Guna2TextBox>().ToArray());
            }
            else
            {
                for (int i = 0; i < procesador.Count; i++)
                {
                    if (procesador[i].IdProcesador.ToString() == txtIdProcesador.Text)
                    {
                        logica.TurnOnLabels(this.Controls.OfType<Label>().Where((label) => label.Name.ToString() != "label1").ToArray());
                        logica.TurnOnTxtB(this.Controls.OfType<Guna2TextBox>().ToArray());
                        txtIdProcesador.Text = procesador[i].IdProcesador.ToString();
                        txtMarca.Text = procesador[i].Marca.ToString();
                        txtModelo.Text = procesador[i].Modelo.ToString();


                        txtIdProcesador.Enabled = false;
                        btnInsertar.Enabled = false;

                    }
                }
            }
        }
    }
}