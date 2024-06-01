using BOAEntidad;
using BOALogica;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BOALogica.CLogica;

namespace BaseDeDatosBOA
{
    public partial class Ventas : Form
    {
        private CLogica logica;
        List<Venta> ventas = null;
        public Ventas()
        {
            logica = new CLogica();
            InitializeComponent();

            logica.TurnOffLabels(label2, label3, label4, label5, label6, label7, label8);
            logica.TurnOffTxtB(txtIdEmpleado, txtIdComputadora, txtIdCliente, txtFechaCliente, txtPrecioFinal, txtPrecioBase, txtDescuento);

            ValidadorForm.AgregarValidacion(btnInsertar, txtIdVenta, txtIdEmpleado, txtIdComputadora, txtIdCliente, txtFechaCliente, txtPrecioFinal, txtPrecioBase, txtDescuento);
            ValidadorForm.AgregarValidacion(btnModificar, txtIdVenta, txtIdEmpleado, txtIdComputadora, txtIdCliente, txtFechaCliente, txtPrecioFinal, txtPrecioBase, txtDescuento);

        }

       

        private void btnInsertar_Click_1(object sender, EventArgs e)
        {
            List<Empleado> emple = logica.ObtenerEmpleado();
            List<Computadora> compu = logica.ObtenerComputadoras();
            List<Cliente> clien = logica.ObtenerClientes();

            logica.ProcesarInsertar(
                txtIdVenta.Text,
                ventas,
                item => item.IdVenta.ToString(),
                () => new Venta
                {
                    IdVenta = txtIdVenta.Text,
                    IdEmpleado = txtIdEmpleado.Text,
                    IdComputadora = txtIdComputadora.Text,
                    IdCliente = txtIdCliente.Text,
                    FechaVenta = txtFechaCliente.Text,
                    PrecioFinal = int.Parse(txtPrecioFinal.Text),
                    PrecioBase = int.Parse(txtPrecioBase.Text),
                    Descuento = int.Parse(txtDescuento.Text)
                },
                @"^V\d+$",
                () => logica.ClearTextBoxs(txtIdVenta, txtIdEmpleado, txtIdComputadora, txtIdCliente, txtFechaCliente, txtPrecioFinal, txtPrecioBase, txtDescuento),
                Controls.OfType<Guna2TextBox>().Where(txt => txt.Name != "txtIdVenta").ToArray(),
                Controls.OfType<Label>().Where(label => label.Name != "label1").ToArray()
            );
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            List<Computadora> comp = logica.ObtenerComputadoras();
            bool checkExistence = logica.CheckExistenciaInventario(txtIdComputadora.Text, comp);
            if (checkExistence == true)
            {
                bool checkFormat = logica.CheckAllFormats(txtIdVenta.Text, @"^V\d+$");
                if (checkFormat == false)
                {
                    MessageBox.Show("error de formato en ID");
                }
                else
                {
                    Venta venta = null;
                    try
                    {
                        venta = new Venta
                        {
                            IdVenta = txtIdVenta.Text,
                            IdEmpleado = txtIdEmpleado.Text,
                            IdComputadora = txtIdComputadora.Text,
                            IdCliente = txtIdCliente.Text,
                            FechaVenta = txtFechaCliente.Text,
                            PrecioFinal = int.Parse(txtPrecioFinal.Text),
                            PrecioBase = int.Parse(txtPrecioBase.Text),
                            Descuento = int.Parse(txtDescuento.Text)
                        };
                        logica.ModificarVenta(venta);
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                    txtIdVenta.Enabled = true;
                    btnInsertar.Enabled = true;
                    logica.TurnOffLabels(label2, label3, label4, label5, label6, label7, label8);
                    logica.TurnOffTxtB(txtIdVenta, txtIdEmpleado, txtIdComputadora, txtIdCliente, txtFechaCliente, txtPrecioFinal, txtPrecioBase, txtDescuento);
                    logica.ClearTextBoxs(txtIdVenta, txtIdEmpleado, txtIdComputadora, txtIdCliente, txtFechaCliente, txtPrecioFinal, txtPrecioBase, txtDescuento);
                }
            }
        }

        private void Ventas_Load_1(object sender, EventArgs e)
        {
            ventas =logica.LoadDataVentas(ventas, dgvVentas);
        }

        private void AbrirEliminar(string tablaDondeViene)
        {
            Eliminar formEliminar = new Eliminar();
            formEliminar.tablaDeDondeViene = tablaDondeViene;
            formEliminar.ShowDialog();
        }
        private void BtnEliminarVenta_Click(object sender, EventArgs e)
        {
            AbrirEliminar("idVenta");
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ventas = logica.LoadDataVentas(ventas, dgvVentas);
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "VENTA";
            formConsulta.ShowDialog();
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            logica.SoloNumeros(sender, e);
        }


        private void txtIdVenta_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdEmpleado_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdComputadora_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdCliente_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtFechaCliente_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtPrecioFinal_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtPrecioBase_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            logica.ProcesarVerificacion(
          txtIdVenta.Text,
          ventas,
          item => item.IdVenta.ToString(),
          item =>
          {
              txtIdVenta.Text = item.IdVenta;
              txtIdEmpleado.Text = item.IdEmpleado;
              txtIdComputadora.Text = item.IdComputadora;
              txtIdCliente.Text = item.IdCliente;
              txtFechaCliente.Text = item.FechaVenta;
              txtPrecioFinal.Text = item.PrecioFinal.ToString();
              txtPrecioBase.Text = item.PrecioBase.ToString();
              txtDescuento.Text = item.Descuento.ToString();
          },
          item =>
          {
              txtIdVenta.Enabled = false;
              btnInsertar.Enabled = false;
          },
          this.Controls.Cast<Control>().ToArray()
      );
        }
    }
}