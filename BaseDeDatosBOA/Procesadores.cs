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

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            logica.ProcesarInsertar(
         txtIdProcesador.Text,
         procesador,
         item => item.IdProcesador.ToString(),
         () => new Procesador
         {
             IdProcesador = txtIdProcesador.Text,
             Marca = txtMarca.Text,
             Modelo = txtModelo.Text
         },
         @"^P\d+$",
         () => logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray()),
         new Control[] { txtIdProcesador },
         this.Controls.OfType<Control>().Where(c => c.Name != "txtIdProcesador" && c is Guna2TextBox).ToArray()
     );
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            logica.ModificarEntidad(
        txtIdProcesador.Text,
        @"^P\d+$",
        () => new Procesador
        {
            IdProcesador = txtIdProcesador.Text,
            Marca = txtMarca.Text,
            Modelo = txtModelo.Text,
        },
        logica.ModificarProcesadores,
        this.Controls.OfType<Label>().Where(label => label.Name != "label1").ToArray(),
        this.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().Where(txtBox => txtBox.Name != "txtIdProcesador").ToArray(),
        this.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().ToArray()
        );
        }

        private void Procesadores_Load(object sender, EventArgs e)
        {
            procesador = logica.LoadDataProcesador(procesador, dgvProcesadores);
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
            procesador = logica.LoadDataProcesador(procesador, dgvProcesadores);
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
            logica.ProcesarVerificacion(
          txtIdProcesador.Text,
          procesador,
          item => item.IdProcesador.ToString(),
          item =>
          {
              txtIdProcesador.Text = item.IdProcesador;
              txtMarca.Text = item.Marca;
              txtModelo.Text = item.Modelo;
          },
          item =>
          {
              txtIdProcesador.Enabled = false;
              btnInsertar.Enabled = false;
          },
          this.Controls.Cast<Control>().ToArray()
      );
        }
    }
}