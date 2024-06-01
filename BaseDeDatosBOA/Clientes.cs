using BOAEntidad;
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
using Guna.UI2.WinForms;
using static BOALogica.CLogica;

namespace BaseDeDatosBOA
{
    public partial class Clientes : Form
    {
        private CLogica logica;
        List<Cliente> clientes = null;

        public Clientes()
        {
            logica = new CLogica();
            InitializeComponent();
            logica.TurnOffLabels(label2, label3, label4, label5);
            logica.TurnOffTxtB(txtNombre, txtApellidoP, txtApellidoM, txtCorreo);

            ValidadorForm.AgregarValidacion(btnInsertar, txtIdCliente, txtNombre, txtApellidoP, txtApellidoM, txtCorreo);
            ValidadorForm.AgregarValidacion(btnModificar, txtIdCliente, txtNombre, txtApellidoP, txtApellidoM, txtCorreo);

        }
        

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            logica.ProcesarInsertar(
        txtIdCliente.Text,
        clientes,
        item => item.IdCliente.ToString(),
        () => new Cliente
        {
            IdCliente = txtIdCliente.Text,
            Nombre = txtNombre.Text,
            ApellidoP = txtApellidoP.Text,
            ApellidoM = txtApellidoM.Text,
            Correo = txtCorreo.Text
        },
        @"^C\d+$",
        () => logica.ClearTextBoxs(txtIdCliente, txtNombre, txtApellidoP, txtApellidoM, txtCorreo),
        new Control[] { txtIdCliente },
        new Control[] { txtNombre, txtApellidoP, txtApellidoM, txtCorreo }
         );
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            bool checkFormat = logica.CheckAllFormats(txtIdCliente.Text, @"^C\d+$");
            if (checkFormat == false)
            {
                MessageBox.Show("error de formato en ID");
            }
            else
            {
                Cliente cliente = null;
                try
                {
                    cliente = new Cliente
                    {
                        IdCliente = txtIdCliente.Text,
                        Nombre = txtNombre.Text,
                        ApellidoP = txtApellidoP.Text,
                        ApellidoM = txtApellidoM.Text,
                        Correo = txtCorreo.Text,
                    };
                    logica.ModificarCliente(cliente);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                txtIdCliente.Enabled = true;
                btnInsertar.Enabled = true;
                logica.TurnOffLabels(label2, label3, label4, label5);
                logica.TurnOffTxtB(txtNombre, txtApellidoP, txtApellidoM, txtCorreo);
                logica.ClearTextBoxs(txtIdCliente, txtNombre, txtApellidoP, txtApellidoM, txtCorreo);
            }
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            clientes = logica.LoadDataClientes(clientes, dgvClientes);
        }
        private void AbrirEliminar(string tablaDondeViene)
        {
            Eliminar formEliminar = new Eliminar();
            formEliminar.tablaDeDondeViene = tablaDondeViene;
            formEliminar.ShowDialog();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AbrirEliminar("idCliente");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            clientes = logica.LoadDataClientes(clientes, dgvClientes);
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "CLIENTES";
            formConsulta.ShowDialog();
        }

        private void txtIdCliente_TextChanged(object sender, EventArgs e)
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

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            logica.ProcesarVerificacion(
        txtIdCliente.Text,
        clientes,
        item => item.IdCliente.ToString(),
        item =>
        {
            // Asigna los valores correspondientes a los TextBox
            txtApellidoM.Text = item.ApellidoM.ToString();
            txtApellidoP.Text = item.ApellidoP.ToString();
            txtCorreo.Text = item.Correo.ToString();
            txtNombre.Text = item.Nombre.ToString();
        },
        item =>
        {
            // Deshabilita la edición del ID y el botón de insertar
            txtIdCliente.Enabled = false;
            btnInsertar.Enabled = false;
        },
        new Control[] { label2, label3, label4, label5, txtIdCliente, txtNombre, txtApellidoP, txtApellidoM, txtCorreo } // Controles a activar
    );
        }
    }
}