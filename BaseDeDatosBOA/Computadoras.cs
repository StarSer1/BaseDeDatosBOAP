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
    public partial class Computadoras : Form
    {
        private CLogica logica;
        List<Computadora> computadora = null;

        public Computadoras()
        {
            logica = new CLogica();
            InitializeComponent();
            logica.TurnOffLabels(this.Controls.OfType<Label>().Where((label) => label.Name.ToString() != "label1").ToArray());
            logica.TurnOffTxtB(this.Controls.OfType<Guna2TextBox>().Where((button) => button.Name.ToString() != "txtIdComputadora").ToArray());

            ValidadorForm.AgregarValidacion(btnInsertar, txtIdComputadora, txtModelo, txtIdRam, txtIdProcesador, txtIdGrafica, txtIdAlmacenamiento, txtIdTarjetaMadre, txtIdFuentePoder);
            ValidadorForm.AgregarValidacion(btnModificar, txtIdComputadora, txtModelo, txtIdRam, txtIdProcesador, txtIdGrafica, txtIdAlmacenamiento, txtIdTarjetaMadre, txtIdFuentePoder);
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
         txtIdComputadora.Text,
         computadora,
         item => item.IdComputadora.ToString(),
         () => new Computadora
         {
             IdComputadora = txtIdComputadora.Text,
             Modelo = txtModelo.Text,
             IdRam = txtIdRam.Text,
             IdProcesador = txtIdProcesador.Text,
             IdGrafica = txtIdGrafica.Text,
             IdAlmacenamiento = txtIdAlmacenamiento.Text,
             IdTarjetaMadre = txtIdTarjetaMadre.Text,
             IdFuentePoder = txtIdFuentePoder.Text
         },
         @"^COM\d+$",
         () => logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray()),
         new Control[] { txtIdComputadora },
         this.Controls.OfType<Control>().Where(c => c.Name != "txtIdComputadora" && c is Guna2TextBox).ToArray()
     );
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            List<Almacenamiento> alma = logica.ObtenerAlmacenamientos();
            List<Ram> ram = logica.ObtenerRam();
            List<Procesador> proce = logica.ObtenerProcesadores();
            List<Grafica> graf = logica.ObtenerGraficas();
            List<TarjetaMadre> tarjMadre = logica.ObtenerTarjetaMadres();
            List<FuentePoder> fuentePod = logica.ObtenerFuentesDePoder();

            bool checkExistence = logica.CheckExistenciaComputadora(txtIdRam.Text, txtIdProcesador.Text, txtIdGrafica.Text, txtIdAlmacenamiento.Text, txtIdTarjetaMadre.Text, txtIdFuentePoder.Text, ram, proce, graf, alma, tarjMadre, fuentePod);
            if (checkExistence == true)
            {
                bool checkFormatCOM = logica.CheckAllFormats(txtIdComputadora.Text, @"^COM\d+$");
                if (checkFormatCOM == false)
                {
                    MessageBox.Show("error de formato en ID de COMPUTADORA");
                }
                else
                {
                    bool checkFormatMOD = logica.CheckAllFormats(txtModelo.Text, @"^BOAC\d+$");
                    if (checkFormatMOD == false)
                    {
                        MessageBox.Show("error de formato en el MODELO");
                    }
                    else
                    {
                        bool checkFormatRAM = logica.CheckAllFormats(txtModelo.Text, @"^R\d+$");
                        if (checkFormatRAM == false)
                        {
                            MessageBox.Show("error de formato en ID de RAM");
                        }
                        else
                        {
                            bool checkFormatPROC = logica.CheckAllFormats(txtModelo.Text, @"^P\d+$");
                            if (checkFormatPROC == false)
                            {
                                MessageBox.Show("error de formato en ID de PROCESADOR");
                            }
                            else
                            {
                                bool checkFormatGRAF = logica.CheckAllFormats(txtModelo.Text, @"^G\d+$");
                                if (checkFormatGRAF == false)
                                {
                                    MessageBox.Show("error de formato en ID de GRAFICA");
                                }
                                else
                                {
                                    bool checkFormatALMA = logica.CheckAllFormats(txtModelo.Text, @"^A\d+$");
                                    if (checkFormatALMA == false)
                                    {
                                        MessageBox.Show("error de formato en ID de ALMACENAMIENTO");
                                    }
                                    else
                                    {
                                        bool checkFormatTARJ = logica.CheckAllFormats(txtModelo.Text, @"^T\d+$");
                                        if (checkFormatTARJ == false)
                                        {
                                            MessageBox.Show("error de formato en ID de TARJETA MADRE");
                                        }
                                        else
                                        {
                                            bool checkFormatFUENT = logica.CheckAllFormats(txtModelo.Text, @"^F\d+$");
                                            if (checkFormatFUENT == false)
                                            {
                                                MessageBox.Show("error de formato en ID de FUENTE DE PODER");
                                            }
                                            else
                                            {
                                                Computadora computadora = null;
                                                try
                                                {
                                                    computadora = new Computadora
                                                    {
                                                        IdComputadora = txtIdComputadora.Text,
                                                        Modelo = txtModelo.Text,
                                                        IdRam = txtIdRam.Text,
                                                        IdProcesador = txtIdProcesador.Text,
                                                        IdGrafica = txtIdGrafica.Text,
                                                        IdAlmacenamiento = txtIdAlmacenamiento.Text,
                                                        IdTarjetaMadre = txtIdTarjetaMadre.Text,
                                                        IdFuentePoder = txtIdFuentePoder.Text
                                                    };
                                                    logica.ModificarComputadora(computadora);
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    logica.ClearTextBoxs(this.Controls.OfType<Guna2TextBox>().ToArray());
                    txtIdComputadora.Enabled = true;
                    logica.TurnOffLabels(this.Controls.OfType<Label>().Where((label) => label.Name.ToString() != "label1").ToArray());
                    logica.TurnOffTxtB(this.Controls.OfType<Guna2TextBox>().Where((button) => button.Name.ToString() != "txtIdComputadora").ToArray());
                }
            }
        }

        private void Computadoras_Load(object sender, EventArgs e)
        {
            computadora = logica.LoadDataComputadora(computadora, dgvComputadora);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AbrirEliminar("idComputadora");
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            computadora = logica.LoadDataComputadora(computadora, dgvComputadora);
        }

        private void btnConsulta_Click_1(object sender, EventArgs e)
        {
            Consulta formConsulta = new Consulta();
            formConsulta.tablaDeDondeViene = "COMPUTADORA";
            formConsulta.ShowDialog();
        }

        
        private void textBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtIdComputadora_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdRam_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdProcesador_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdGrafica_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdAlmacenamiento_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdTarjetaMadre_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void txtIdFuentePoder_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            logica.ProcesarVerificacion(
         txtIdComputadora.Text,
         computadora,
         item => item.IdComputadora.ToString(),
         item =>
         {
             txtIdComputadora.Text = item.IdComputadora.ToString();
             txtIdAlmacenamiento.Text = item.IdAlmacenamiento.ToString();
             txtIdFuentePoder.Text = item.IdFuentePoder.ToString();
             txtIdGrafica.Text = item.IdGrafica.ToString();
             txtIdProcesador.Text = item.IdProcesador.ToString();
             txtIdRam.Text = item.IdRam.ToString();
             txtIdTarjetaMadre.Text = item.IdTarjetaMadre.ToString();
             txtModelo.Text = item.Modelo.ToString();
         },
         item =>
         {
             txtIdComputadora.Enabled = false;
             btnInsertar.Enabled = false;
         },
         this.Controls.Cast<Control>().ToArray()
     );
        }
    }
}
