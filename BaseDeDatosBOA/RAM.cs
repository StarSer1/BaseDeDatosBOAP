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
    public partial class RAM : Form
    {
        private CLogica logica;
        List<Ram> rams = null;

        public RAM()
        {
            logica = new CLogica();
            InitializeComponent();
            logica.TurnOffLabels(label2, label3, label4, label5, label6);
            logica.TurnOffTxtB(txtFrecuencia, txtMarca, txtTipoRam, txtVelocidadTrans, txtTamaño);

            ValidadorForm.AgregarValidacion(btnInsertar, txtIdRam, txtMarca, txtTipoRam, txtFrecuencia, txtTamaño, txtVelocidadTrans);
            ValidadorForm.AgregarValidacion(btnModificar, txtIdRam, txtMarca, txtTipoRam, txtFrecuencia, txtTamaño, txtVelocidadTrans);//agregado
        }
        

        private void RAM_Load(object sender, EventArgs e)
        {
            rams = logica.LoadDataRam(rams,dgvRam);
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            logica.ProcesarInsertar(
        txtIdRam.Text,
        rams,
        item => item.IdRam.ToString(),
        () => new Ram
        {
            IdRam = txtIdRam.Text,
            Marca = txtMarca.Text,
            TipoRam = txtTipoRam.Text,
            Frecuencia = int.Parse(txtFrecuencia.Text),
            Tamaño = int.Parse(txtTamaño.Text),
            VelocidadTransferencia = int.Parse(txtVelocidadTrans.Text)
        },
        @"^R\d+$",
        () => logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray()),
        new Control[] { txtIdRam },
        this.Controls.OfType<Control>().Where(c => c.Name != "txtIdRam" && c is Guna2TextBox).ToArray()
    );
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            logica.ModificarEntidad(
        txtIdRam.Text,
        @"^R\d+$",
        () => new Ram
        {
            IdRam = txtIdRam.Text,
            Marca = txtMarca.Text,
            TipoRam = txtTipoRam.Text,
            Frecuencia = int.Parse(txtFrecuencia.Text),
            Tamaño = int.Parse(txtTamaño.Text),
            VelocidadTransferencia = int.Parse(txtVelocidadTrans.Text),
        },
        logica.ModificarRam,
        this.Controls.OfType<Label>().Where(label => label.Name != "label1").ToArray(),
        this.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().Where(txtBox => txtBox.Name != "txtIdRam").ToArray(),
        this.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().ToArray()
    );
        }

        private void AbrirEliminar(string tablaDondeViene)
        {
            Eliminar formEliminar = new Eliminar();
            formEliminar.tablaDeDondeViene = tablaDondeViene;
            formEliminar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AbrirEliminar("idRam");
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "RAM";
            formConsulta.ShowDialog();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            rams = logica.LoadDataRam(rams, dgvRam);
        }

        private void txtFrecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            logica.SoloNumeros(sender, e);
        }

        private void txtIdRam_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtTipoRam_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtFrecuencia_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtTamaño_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtVelocidadTrans_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            logica.ProcesarVerificacion(
        txtIdRam.Text,
        rams,
        item => item.IdRam.ToString(),
        item =>
        {
            txtIdRam.Text = item.IdRam;
            txtMarca.Text = item.Marca;
            txtTipoRam.Text = item.TipoRam;
            txtFrecuencia.Text = item.Frecuencia.ToString();
            txtTamaño.Text = item.Tamaño.ToString();
            txtVelocidadTrans.Text = item.VelocidadTransferencia.ToString();
        },
        item =>
        {
            txtIdRam.Enabled = false;
            btnInsertar.Enabled = false;
        },
        this.Controls.Cast<Control>().ToArray()
    );
        }
    }
}