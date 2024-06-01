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
    public partial class Almacenamientos : Form
    {
        private CLogica logica;
        List<Almacenamiento> almacenamiento = null;

        public Almacenamientos()
        {
            logica = new CLogica();
            InitializeComponent();
            logica.TurnOffLabels(this.Controls.OfType<Label>().Where((label) => label.Name.ToString() != "label1").ToArray());
            logica.TurnOffTxtB(this.Controls.OfType<Guna2TextBox>().Where((button) => button.Name.ToString() != "txtIdAlmacenamiento").ToArray());

            ValidadorForm.AgregarValidacion(btnInsertar, txtCapacidad, txtFrecuencia, txtIdAlmacenamiento, txtMarca, txtTipo, txtVelocidadTrans);
            ValidadorForm.AgregarValidacion(btnModificar, txtCapacidad, txtFrecuencia, txtIdAlmacenamiento, txtMarca, txtTipo, txtVelocidadTrans);
        }
        //public void LoadData <T>(List<T> datos,Guna2DataGridView dgvX, string Tabla)
        //{
        //    switch (Tabla)
        //    {
        //        case "ALMACENAMIENTO":
        //            try
        //            {
        //                datos = logica.ObtenerAlmacenamientos();
        //                dgvX.DataSource = almacenamiento;
        //                dgvX.Tag = "almacenamiento";
        //                dgvX.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(logica.dgvVentasChangeSize);
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error: " + ex.Message);
        //            }
        //            break;
        //    }

        //}


        private void btnInsertar_Click(object sender, EventArgs e)
        {
            logica.ProcesarInsertar(
         txtIdAlmacenamiento.Text,
         almacenamiento,
         item => item.IdAlmacenamiento.ToString(),
         () => new Almacenamiento
         {
             IdAlmacenamiento = txtIdAlmacenamiento.Text,
             Marca = txtMarca.Text,
             Tipo = txtTipo.Text,
             Capacidad = int.Parse(txtCapacidad.Text),
             Frecuencia = int.Parse(txtFrecuencia.Text),
             VelocidadTransferencia = int.Parse(txtVelocidadTrans.Text)
         },
         @"^A\d+$",
         () => logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray()),
         new Control[] { txtIdAlmacenamiento },
         this.Controls.OfType<Control>().Where(c => c.Name != "txtIdAlmacenamiento" && c is Guna2TextBox).ToArray()
          );

        }

       
        private void textBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            bool checkFormat = logica.CheckAllFormats(txtIdAlmacenamiento.Text, @"^A\d+$");
            if (checkFormat == false)
            {
                MessageBox.Show("error de formato en ID");
            }
            else
            {
                try
                {
                    Almacenamiento almacenamiento = null;
                    almacenamiento = new Almacenamiento
                    {
                        IdAlmacenamiento = txtIdAlmacenamiento.Text,
                        Marca = txtMarca.Text,
                        Tipo = txtTipo.Text,
                        Capacidad = int.Parse(txtCapacidad.Text),
                        Frecuencia = int.Parse(txtFrecuencia.Text),
                        VelocidadTransferencia = int.Parse(txtVelocidadTrans.Text)
                    };
                    logica.ModificarAlmacenamientos(almacenamiento);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                txtIdAlmacenamiento.Enabled = true;
                btnInsertar.Enabled = true;
                logica.TurnOffLabels(this.Controls.OfType<Label>().Where((label) => label.Name.ToString() != "label1").ToArray());
                logica.TurnOffTxtB(this.Controls.OfType<Guna2TextBox>().Where((button) => button.Name.ToString() != "txtIdAlmacenamiento").ToArray());
                logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray());
            }
        }

        private void Almacenamientos_Load(object sender, EventArgs e)
        {
            almacenamiento= logica.LoadDataAlmacenamiento(almacenamiento, dgvAlmacenamiento);
        }
        private void AbrirEliminar(string tablaDondeViene)
        {
            Eliminar formEliminar = new Eliminar();
            formEliminar.tablaDeDondeViene = tablaDondeViene;
            formEliminar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AbrirEliminar("idAlmacenamiento");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        { 
            almacenamiento = logica.LoadDataAlmacenamiento(almacenamiento, dgvAlmacenamiento);
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "ALMACENAMIENTO";
            formConsulta.ShowDialog();
        }

        private void txtCapacidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            logica.SoloNumeros(sender, e);
        }

        private void txtIdAlmacenamiento_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtTipo_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtCapacidad_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtFrecuencia_TextChanged(object sender, EventArgs e)
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
      txtIdAlmacenamiento.Text,
      almacenamiento,
      item => item.IdAlmacenamiento.ToString(),
      item =>
      {
          // Asigna los valores correspondientes a los TextBox
          txtIdAlmacenamiento.Text = item.IdAlmacenamiento.ToString();
          txtMarca.Text = item.Marca.ToString();
          txtTipo.Text = item.Tipo.ToString();
          txtCapacidad.Text = item.Capacidad.ToString();
          txtFrecuencia.Text = item.Frecuencia.ToString();
          txtVelocidadTrans.Text = item.VelocidadTransferencia.ToString();
      },
      item =>
      {
          // Deshabilita la edición del ID y el botón de insertar
          txtIdAlmacenamiento.Enabled = false;
          btnInsertar.Enabled = false;
      },
      this.Controls.Cast<Control>().ToArray() // Convertir la colección Controls en un arreglo
  );
        }
    }
}
