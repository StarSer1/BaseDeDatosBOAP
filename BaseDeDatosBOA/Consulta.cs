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
using static BOALogica.CLogica;

namespace BaseDeDatosBOA
{
    public partial class Consulta : Form
    {
        private CLogica logica;
        public string tablaDeDondeViene { get; set; }

        public Consulta()
        {
            logica = new CLogica();
            InitializeComponent();
            ValidadorForm.AgregarValidacion(btnBuscar, txtId);
        }

        private void Consulta_Load(object sender, EventArgs e)
        {
            btnBuscar.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            logica.consulta(tablaDeDondeViene, txtId,dgvConsulta);
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            logica.CambioAMayusculas(sender, e);
        }
    }
}
