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
    public partial class Tarjetas_Madre : Form
    {
        private CLogica logica;
        List<TarjetaMadre> tarjetasMadres = null;

        public Tarjetas_Madre()
        {
            logica = new CLogica();
            InitializeComponent();
            logica.TurnOffLabels(label2, label3, label4, label5, label6);
            logica.TurnOffTxtB(txtDimensiones, txtIdModelo, txtMarca, txtRanurasDIMM, txtSocket);

            ValidadorForm.AgregarValidacion(btnInsertar, txtIdTarjetaMadre, txtMarca, txtIdModelo, txtRanurasDIMM, txtSocket, txtDimensiones);
            ValidadorForm.AgregarValidacion(btnModificar, txtIdTarjetaMadre, txtMarca, txtIdModelo, txtRanurasDIMM, txtSocket, txtDimensiones);
        }

        private void Tarjetas_Madre_Load(object sender, EventArgs e)
        {
            tarjetasMadres = logica.LoadDataTarjetaMadre(tarjetasMadres, dgvTarjetasMadre);
        }
        

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            logica.ProcesarInsertar(
        txtIdTarjetaMadre.Text,
        tarjetasMadres,
        item => item.IdTarjetaMadre.ToString(),
        () => new TarjetaMadre
        {
            IdTarjetaMadre = txtIdTarjetaMadre.Text,
            Marca = txtMarca.Text,
            Modelo = txtIdModelo.Text,
            RanurasDIMM = int.Parse(txtRanurasDIMM.Text),
            Socket = txtSocket.Text,
            Dimensiones = txtDimensiones.Text
        },
        @"^T\d+$",
        () => logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray()),
        new Control[] { txtIdTarjetaMadre },
        this.Controls.OfType<Control>().Where(c => c.Name != "txtIdTarjetaMadre" && c is Guna2TextBox).ToArray()
    );
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            logica.ModificarEntidad(
        txtIdTarjetaMadre.Text,
        @"^T\d+$",
        () => new TarjetaMadre
        {

            IdTarjetaMadre = txtIdTarjetaMadre.Text,
            Marca = txtMarca.Text,
            Modelo = txtIdModelo.Text,
            RanurasDIMM = int.Parse(txtRanurasDIMM.Text),
            Socket = txtSocket.Text,
            Dimensiones = txtDimensiones.Text,

        },
        logica.ModificarTarjetasMadre,
        this.Controls.OfType<Label>().Where(label => label.Name != "label1").ToArray(),
        this.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().Where(txtBox => txtBox.Name != "txtIdTarjetaMadre").ToArray(),
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
            AbrirEliminar("idTarjetaMadre");
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "TARJETAMADRE";
            formConsulta.ShowDialog();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            tarjetasMadres = logica.LoadDataTarjetaMadre(tarjetasMadres, dgvTarjetasMadre);
        }

        private void txtRanurasDIMM_KeyPress(object sender, KeyPressEventArgs e)
        {
            logica.SoloNumeros(sender, e);
        }

        private void txtIdTarjetaMadre_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdModelo_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtRanurasDIMM_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtSocket_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtDimensiones_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            logica.ProcesarVerificacion(
        txtIdTarjetaMadre.Text,
        tarjetasMadres,
        item => item.IdTarjetaMadre.ToString(),
        item =>
        {
            txtIdTarjetaMadre.Text = item.IdTarjetaMadre;
            txtMarca.Text = item.Marca;
            txtIdModelo.Text = item.Modelo;
            txtRanurasDIMM.Text = item.RanurasDIMM.ToString();
            txtSocket.Text = item.Socket;
            txtDimensiones.Text = item.Dimensiones;
        },
        item =>
        {
            txtIdTarjetaMadre.Enabled = false;
            btnInsertar.Enabled = false;
        },
        this.Controls.Cast<Control>().ToArray()
    );
        }
    }
}