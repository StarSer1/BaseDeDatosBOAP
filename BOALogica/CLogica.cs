﻿using System;
using System.Collections.Generic;
using BOAEntidad;
using BOADatos;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq;
using Guna.UI2.WinForms;

namespace BOALogica
{
    public class CLogica
    {
        private CDatos datos;

        public CLogica()
        {
            datos = new CDatos();
        }
        public void LoadData<T>(List<T> datos, Guna2DataGridView dgvX, string Tabla)
        {
            switch (Tabla)
            {
                
                case "VENTAS":

                    break;
                case "INVENTARIO":

                    break;
                case "CLIENTES":

                    break;
                case "EMPLEADO":

                    break;
                case "COMPUTADORA":

                    break;
                case "TARJETAMADRE":

                    break;
                case "PROCESADOR":

                    break;
                case "GRAFICA":

                    break;
                case "RAM":

                    break;
                case "ALMACENAMIENTO":
                    try
                    {
                        datos = ObtenerAlmacenamientos().Cast<T>().ToList(); ;
                        dgvX.DataSource = datos;
                        dgvX.Tag = "almacenamiento";
                        dgvX.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgvVentasChangeSize);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    break;
                case "FUENTEPODER":

                    break;
            }

        }
        #region Obtener Tablas
        public List<Computadora> ObtenerComputadoras()
        {
            return datos.ObtenerTabla("COMPUTADORA", datos.MapComputadora, "idComputadora");
        }
        public List<Venta> ObtenerVentas()
        {
            return datos.ObtenerTabla("VENTAS", datos.MapVenta, "idVenta");
        }
        public List<Inventario> ObtenerInventarios()
        {
            return datos.ObtenerTabla("INVENTARIO", datos.MapInventario, "idInventario");
        }
        public List<Cliente> ObtenerClientes()
        {
            return datos.ObtenerTabla("CLIENTES", datos.MapCliente, "idCliente");
        }
        public List<Empleado> ObtenerEmpleado()
        {
            return datos.ObtenerTabla("EMPLEADO", datos.MapEmpleado, "idEmpleado");
        }
        public List<TarjetaMadre> ObtenerTarjetaMadres()
        {
            return datos.ObtenerTabla("TARJETAMADRE", datos.MapTarjetaMadre, "idTarjetaMadre");
        }
        public List<Procesador> ObtenerProcesadores()
        {
            return datos.ObtenerTabla("PROCESADOR", datos.MapProcesador,"idProcesador");
        }
        public List<Grafica> ObtenerGraficas()
        {
            return datos.ObtenerTabla("GRAFICA", datos.MapGrafica, "idGrafica");
        }
        public List<Ram> ObtenerRam()
        {
            return datos.ObtenerTabla("RAM", datos.MapRam, "idRam");
        }
        public List<Almacenamiento> ObtenerAlmacenamientos()
        {
            return datos.ObtenerTabla("ALMACENAMIENTO", datos.MapAlmacenamiento, "idAlmacenamiento");
        }
        public List<FuentePoder> ObtenerFuentesDePoder()
        {
            return datos.ObtenerTabla("FUENTEPODER", datos.MapFuentePoder, "idFuentePoder");
        }
        #endregion
        #region Registros
        public void RegistrarVenta(Venta venta)
        {
            datos.Insertar("VENTAS", venta, datos.ConfigureVentaParameters);
        }
        public void RegistrarInventario(Inventario inventario)
        {
            datos.Insertar("INVENTARIO", inventario, datos.ConfigureInventoryParameters);
        }
        public void RegistrarCliente(Cliente cliente)
        {
            datos.Insertar("CLIENTES", cliente, datos.ConfigureClientParameters);
        }
        public void RegistrarEmpleado(Empleado empleado)
        {
            datos.Insertar("CLIENTES", empleado, datos.ConfigureEmployeeParameters);
        }
        public void RegistrarComputadora(Computadora computadora)
        {
            datos.Insertar("COMPUTADORA", computadora, datos.ConfigureComputadoraParameters);
        }
        public void RegistrarTarjetasMadre(TarjetaMadre tarjetaMadre)
        {
            datos.Insertar("TARJETAMADRE", tarjetaMadre, datos.ConfigureTarjetaMadreParameters);
        }
        public void RegistrarProcesador(Procesador procesador)
        {
            datos.Insertar("PROCESADOR", procesador, datos.ConfigureProcesadorParameters);
        }
        public void RegistrarGrafica(Grafica grafica)
        {
            datos.Insertar("GRAFICA", grafica, datos.ConfigureGraficaParameters);
        }
        public void RegistrarRam(Ram ram)
        {
            datos.Insertar("RAM", ram, datos.ConfigureRamParameters);
        }
        public void RegistrarAlmacenamiento(Almacenamiento almacenamiento)
        {
            datos.Insertar("ALMACENAMIENTO", almacenamiento, datos.ConfigureAlmacenamientoParameters);
        }
        public void RegistrarFuentesPoder(FuentePoder fuentePoder)
        {
            datos.Insertar("FUENTEPODER", fuentePoder, datos.ConfigureFuentePoderParameters);
        }
        #endregion
        #region Modificaciones
        public void ModificarVenta(Venta venta)
        {
            datos.ActualizarVentas(venta);
        }
        public void ModificarInventario(Inventario inventario)
        {
            datos.ActualizarInventarios(inventario);
        }
        public void ModificarCliente(Cliente cliente)
        {
            datos.ActualizarClientes(cliente);
        }
        public void ModificarEmpleado(Empleado empleado)
        {
            datos.ActualizarEmpleados(empleado);
        }
        public void ModificarComputadora(Computadora computadora)
        {
            datos.ActualizarComputadora(computadora);
        }
        public void ModificarTarjetasMadre(TarjetaMadre tarjetaMadre)
        {
            datos.ActualizarTarjetaMadre(tarjetaMadre);
        }
        public void ModificarProcesadores(Procesador procesador)
        {
            datos.ActualizarProcesadores(procesador);
        }
        public void ModificarGraficas(Grafica grafica)
        {
            datos.ActualizarGraficas(grafica);
        }
        public void ModificarRam(Ram ram)
        {
            datos.ActualizarRam(ram);
        }
        public void ModificarAlmacenamientos(Almacenamiento almacenamiento)
        {
            datos.ActualizarAlmacenamientos(almacenamiento);
        }
        public void ModificarFuentesPoder(FuentePoder fuentePoder)
        {
            datos.ActualizarFuentePoder(fuentePoder);
        }

        #endregion
        public bool Eliminar(string id, string tablaDelId)
        {
            bool refExit = false;
            try
            {
                datos.Eliminar(id, tablaDelId);
                refExit = true;
                return refExit;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Favor de primero eliminar el registro donde esta tabla es utilizada");
                return refExit;
            }
        }
        #region Consultas
        public List<Venta> ConsultarVenta(string id)
        {
            return datos.Consultar("VENTAS", "idVenta", id, datos.MapVenta);
        }
        public List<Inventario> ConsultarInventario(string id)
        {
            return datos.Consultar("INVENTARIO", "idInventario", id, datos.MapInventario);
        }
        public List<Cliente> ConsultarCliente(string id)
        {
            return datos.Consultar("CLIENTES", "idCliente", id, datos.MapCliente);
        }
        public List<Empleado> ConsultarEmpleado(string id)
        {
            return datos.Consultar("EMPLEADO", "idEmpleado", id, datos.MapEmpleado);
        }
        public List<Computadora> ConsultarComputadora(string id)
        {
            return datos.Consultar("COMPUTADORA", "idComputadora", id, datos.MapComputadora);
        }
        public List<TarjetaMadre> ConsultarTarjetaMadre(string id)
        {
            return datos.Consultar("TARJETAMADRE", "idTarjetaMadre", id, datos.MapTarjetaMadre);
        }
        public List<Procesador> ConsultarProcesador(string id)
        {
            return datos.Consultar("PROCESADOR", "idProcesador", id, datos.MapProcesador);
        }
        public List<Grafica> ConsultarGrafica(string id)
        {
            return datos.Consultar("GRAFICA", "idGrafica", id, datos.MapGrafica);
        }
        public List<Ram> ConsultarRam(string id)
        {
            return datos.Consultar("RAM", "idRam", id, datos.MapRam);
        }
        public List<Almacenamiento> ConsultarAlmacenamiento(string id)
        {
            return datos.Consultar("ALMACENAMIENTO", "idAlmacenamiento", id, datos.MapAlmacenamiento);
        }
        public List<FuentePoder> ConsultarFuentePoder(string id)
        {
            return datos.Consultar("FUENTEPODER", "idFuentePoder", id, datos.MapFuentePoder);
        }
        #endregion
        #region Validaciones
        public void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public bool VerifyID<T>(string id, List<T> datos, Func<T, string> getId)
        {
            bool pass = false;
            foreach (var item in datos)
            {
                if (getId(item) == id)
                {
                    MessageBox.Show("ID Repetido");
                    pass = false;
                    break;
                }
                else
                {
                    pass = true;
                }
            }
            return pass;
        }
        public bool CheckAllFormats(string id, string patron)
        {
            Regex regx = new Regex(patron);
            return regx.IsMatch(id);
        }

        public bool CheckExistence<T>(string id, List<T> items, Func<T, string> getId, string errorMessage)
        {
            foreach (var item in items)
            {
                if (getId(item) == id)
                {
                    return true;
                }
            }
            MessageBox.Show(errorMessage);
            return false;
        }

        public bool CheckExistenciaComputadora(string idR, string idP, string idG, string idA, string idT, string idF, List<Ram> rams, List<Procesador> procesadores, List<Grafica> graficas, List<Almacenamiento> almacenamientos, List<TarjetaMadre> tarjetaMadres, List<FuentePoder> fuentesPoder)
        {
            bool pass = CheckExistence(idR, rams, ram => ram.IdRam, "La RAM ingresada no se encuentra en la base de datos");
            if (!pass) return false;

            pass = CheckExistence(idP, procesadores, procesador => procesador.IdProcesador, "El procesador ingresado no se encuentra en la base de datos");
            if (!pass) return false;

            pass = CheckExistence(idG, graficas, grafica => grafica.IdGrafica, "La tarjeta gráfica ingresada no se encuentra en la base de datos");
            if (!pass) return false;

            pass = CheckExistence(idA, almacenamientos, almacenamiento => almacenamiento.IdAlmacenamiento, "El almacenamiento ingresado no se encuentra en la base de datos");
            if (!pass) return false;

            pass = CheckExistence(idT, tarjetaMadres, tarjetaMadre => tarjetaMadre.IdTarjetaMadre, "La tarjeta madre ingresada no se encuentra en la base de datos");
            if (!pass) return false;

            pass = CheckExistence(idF, fuentesPoder, fuentePoder => fuentePoder.IdFuentePoder, "La fuente de poder ingresada no se encuentra en la base de datos");
            if (!pass) return false;

            return true;
        }

        public bool CheckExistenciaVenta(string idE, string idC, string idClt, List<Empleado> empleado, List<Computadora> computadora, List<Cliente> cliente)
        {
            bool pass = CheckExistence(idE, empleado, empleados => empleados.IdEmpleado, "El empleado ingresado no se encuentra en la base de datos");
            if (!pass) return false;

            pass = CheckExistence(idC, computadora, computadoras => computadoras.IdComputadora, "La computadora ingresada no se encuentra en la base de datos");
            if (!pass) return false;

            pass = CheckExistence(idClt, cliente, Clientes => Clientes.IdCliente, "El cliente ingresado no se encuentra en la base de datos");
            if (!pass) return false;

            return true;
        }
        public bool CheckExistenciaInventario(string idC, List<Computadora> computadoras)
        {
            bool pass = CheckExistence(idC, computadoras, comp => comp.IdComputadora, "La computadora ingresada no se encuentra en la base de datos");
            if (!pass) return false;


            return true;
        }

        public void RevisionDeDatos()
        {

        }

        public static class ValidadorForm
        {
            public static bool TodosTextBoxLlenos(params Guna2TextBox[] textBoxes)
            {
                return textBoxes.All(tb => !string.IsNullOrWhiteSpace(tb.Text));
            }

            public static void AgregarValidacion(Guna2Button buttonInsertar, params Guna2TextBox[] textBoxes)
            {
                foreach (var textBox in textBoxes)
                {
                    textBox.TextChanged += (sender, e) =>
                    {
                        buttonInsertar.Enabled = TodosTextBoxLlenos(textBoxes);
                    };
                }

                buttonInsertar.Enabled = TodosTextBoxLlenos(textBoxes);
            }
        }
        public void ClearTextBoxs(params Guna2TextBox[] textBoxs)
        {
            foreach (Guna2TextBox textBox in textBoxs)
            {
                textBox.Clear();
            }
        }
        public void TurnOffTxtB(params Guna2TextBox[] textBoxs)
        {
            foreach(Guna2TextBox textBox in textBoxs)
            {
                textBox.Visible = false;
            }

        }
        public void TurnOffLabels(params Label[] labels)
        {
            foreach(Label label in labels)
            {
                label.Visible = false;
            }
        }
        public void TurnOnTxtB(params Guna2TextBox[] textBoxs)
        {
            foreach (Guna2TextBox textBox in textBoxs)
            {
                textBox.Visible = true;
            }

        }
        public void TurnOnLabels(params Label[] labels)
        {
            foreach (Label label in labels)
            {
                label.Visible = true;
            }
        }


        #endregion
        public void CambioAMayusculas(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2TextBox gunaTextBox = sender as Guna.UI2.WinForms.Guna2TextBox;
            if (gunaTextBox != null)
            {
                int selectionStart = gunaTextBox.SelectionStart;
                int selectionLength = gunaTextBox.SelectionLength;

                gunaTextBox.Text = gunaTextBox.Text.ToUpper();

                gunaTextBox.SelectionStart = selectionStart;
                gunaTextBox.SelectionLength = selectionLength;
            }
        }
        public void dgvVentasChangeSize(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv != null)
            {
                if (dgv.Tag.ToString() == "venta")
                {
                    dgv.Columns["idVenta"].Width = 55;
                    dgv.Columns["idEmpleado"].Width = 95;
                    dgv.Columns["idComputadora"].Width = 115;
                    dgv.Columns["idCliente"].Width = 65;
                    dgv.Columns["fechaVenta"].Width = 100;
                    dgv.Columns["precioFinal"].Width = 90;
                    dgv.Columns["precioBase"].Width = 90;
                    dgv.Columns["Descuento"].Width = 80;
                }
                if (dgv.Tag.ToString() == "tarjetaMadre")
                {
                    dgv.Columns["idTarjetaMadre"].Width = 100;
                    dgv.Columns["marca"].Width = 100;
                    dgv.Columns["modelo"].Width = 180;
                    dgv.Columns["RanurasDIMM"].Width = 100;
                    dgv.Columns["socket"].Width = 100;
                    dgv.Columns["dimensiones"].Width = 100;
                }
                if (dgv.Tag.ToString() == "ram")
                {
                    dgv.Columns["idRam"].Width = 50;
                    dgv.Columns["marca"].Width = 100;
                    dgv.Columns["tipoRam"].Width = 150;
                    dgv.Columns["frecuencia"].Width = 100;
                    dgv.Columns["tamaño"].Width = 100;
                    dgv.Columns["velocidadTransferencia"].Width = 180;
                }
                if (dgv.Tag.ToString() == "procesador")
                {
                    dgv.Columns["idProcesador"].Width = 200;
                    dgv.Columns["marca"].Width = 200;
                    dgv.Columns["modelo"].Width = 280;

                }
                if (dgv.Tag.ToString() == "inventario")
                {
                    dgv.Columns["idInventario"].Width = 100;
                    dgv.Columns["idComputadora"].Width = 120;
                    dgv.Columns["fechaLlegada"].Width = 180;
                    dgv.Columns["precioLlegada"].Width = 180;
                    dgv.Columns["stock"].Width = 100;
                }
                if (dgv.Tag.ToString() == "grafica")
                {
                    dgv.Columns["idGrafica"].Width = 110;
                    dgv.Columns["marca"].Width = 110;
                    dgv.Columns["modelo"].Width = 200;
                    dgv.Columns["tipo"].Width = 150;
                    dgv.Columns["vram"].Width = 110;
                }
                if (dgv.Tag.ToString() == "fuentePoder")
                {
                    dgv.Columns["idFuentePoder"].Width = 110;
                    dgv.Columns["marca"].Width = 130;
                    dgv.Columns["modelo"].Width = 170;
                    dgv.Columns["potencia"].Width = 70;
                    dgv.Columns["tipo"].Width = 70;
                    dgv.Columns["certificacion"].Width = 130;
                }
                if (dgv.Tag.ToString() == "computadora")
                {
                    dgv.Columns["idComputadora"].Width = 105;
                    dgv.Columns["modelo"].Width = 58;
                    dgv.Columns["idRam"].Width = 50;
                    dgv.Columns["idProcesador"].Width = 92;
                    dgv.Columns["idGrafica"].Width = 65;
                    dgv.Columns["idAlmacenamiento"].Width = 125;
                    dgv.Columns["idTarjetaMadre"].Width = 105;
                    dgv.Columns["idFuentePoder"].Width = 105;
                }
                if (dgv.Tag.ToString() == "cliente")
                {
                    dgv.Columns["idCliente"].Width = 60;
                    dgv.Columns["nombre"].Width = 120;
                    dgv.Columns["apellidoP"].Width = 170;
                    dgv.Columns["apellidoM"].Width = 170;
                    dgv.Columns["correo"].Width = 160;
                }
                if (dgv.Tag.ToString() == "almacenamiento")
                {
                    dgv.Columns["idAlmacenamiento"].Width = 125;
                    dgv.Columns["marca"].Width = 125;
                    dgv.Columns["tipo"].Width = 50;
                    dgv.Columns["capacidad"].Width = 100;
                    dgv.Columns["frecuencia"].Width = 100;
                    dgv.Columns["velocidadTransferencia"].Width = 180;
                }
                if (dgv.Tag.ToString() == "empleado")
                {
                    dgv.Columns["idEmpleado"].Width = 85;
                    dgv.Columns["nombre"].Width = 95;
                    dgv.Columns["apellidoP"].Width = 150;
                    dgv.Columns["apellidoM"].Width = 150;
                    dgv.Columns["rfc"].Width = 150;
                    dgv.Columns["sueldo"].Width = 50;
                }
            }
        }
    
    }

}

