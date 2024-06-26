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
    public partial class Empleados : Form
    {
        private CLogica logica;
        List<Empleado> empleados = null;

        public Empleados()
        {
            logica = new CLogica();
            InitializeComponent();

            logica.TurnOffLabels(label2, label3, label4, label5, label6);
            logica.TurnOffTxtB(txtNombre, txtApellidoP, txtApellidoM, txtRFC, txtSueldo);

            ValidadorForm.AgregarValidacion(btnModificar, txtIdEmp, txtNombre, txtApellidoP, txtApellidoM, txtRFC, txtSueldo);
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            logica.ProcesarInsertar(
       txtIdEmp.Text,
       empleados,
       item => item.IdEmpleado.ToString(),
       () => new Empleado
       {
           IdEmpleado = txtIdEmp.Text,
           Nombre = txtNombre.Text,
           ApellidoP = txtApellidoP.Text,
           ApellidoM = txtApellidoM.Text,
           RFC = txtRFC.Text,
           Sueldo = int.Parse(txtSueldo.Text)
       },
       @"^E\d+$",
       () => logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray()),
       new Control[] { txtIdEmp },
       this.Controls.OfType<Control>().Where(c => c.Name != "txtIdEmp" && c is Guna2TextBox).ToArray()
   );
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            logica.ModificarEntidad(
        txtIdEmp.Text,
        @"^E\d+$",
        () => new Empleado
        {
            IdEmpleado = txtIdEmp.Text,
            Nombre = txtNombre.Text,
            ApellidoP = txtApellidoP.Text,
            ApellidoM = txtApellidoM.Text,
            RFC = txtRFC.Text,
            Sueldo = int.Parse(txtSueldo.Text),
        },
        logica.ModificarEmpleado,
        this.Controls.OfType<Label>().Where(label => label.Name != "label1").ToArray(),
        this.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().Where(txtBox => txtBox.Name != "txtIdEmp").ToArray(),
        this.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().ToArray()
    );
        }

        private void Empleados_Load(object sender, EventArgs e)
        {
            empleados = logica.LoadDataEmpleado(empleados, dgvEmpleado);
        }
        private void AbrirEliminar(string tablaDondeViene)
        {
            Eliminar formEliminar = new Eliminar();
            formEliminar.tablaDeDondeViene = tablaDondeViene;
            formEliminar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AbrirEliminar("idEmpleado");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            empleados = logica.LoadDataEmpleado(empleados, dgvEmpleado);
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "EMPLEADO";
            formConsulta.ShowDialog();
        }

        private void txtSueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            logica.SoloNumeros(sender, e);
        }

        private void txtIdEmp_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtApellidoP_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtApellidoM_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtRFC_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtSueldo_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            logica.ProcesarVerificacion(
        txtIdEmp.Text,
        empleados,
        item => item.IdEmpleado.ToString(),
        item =>
        {
            txtApellidoM.Text = item.ApellidoM.ToString();
            txtApellidoP.Text = item.ApellidoP.ToString();
            txtIdEmp.Text = item.IdEmpleado.ToString();
            txtNombre.Text = item.Nombre.ToString();
            txtRFC.Text = item.RFC.ToString();
            txtSueldo.Text = item.Sueldo.ToString();
        },
        item =>
        {
            txtIdEmp.Enabled = false;
            btnInsertar.Enabled = false;
        },
        this.Controls.Cast<Control>().ToArray()
    );
        }
    }
}