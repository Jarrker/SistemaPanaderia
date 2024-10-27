using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CapaPresentacion.Modales
{
    public partial class mdCliente : Form
    {
        public Cliente _Cliente { get; set; }

        public mdCliente()
        {
            InitializeComponent();
            dgvData.CellDoubleClick += dgvData_CellDoubleClick; // Vincula el evento CellDoubleClick
        }

        private void mdCliente_Load(object sender, EventArgs e)
        {
            // Configurar columnas del DataGridView
            dgvData.Columns.Clear();
            dgvData.Rows.Clear();

            dgvData.Columns.Add("DNI", "DNI");
            dgvData.Columns.Add("NombreCompleto", "Nombre Completo");

            // Configurar ComboBox de búsqueda
            cboBusqueda.Items.Clear();
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible)
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            // Cargar clientes en el DataGridView
            List<Cliente> lista = new CN_Cliente().Listar(); // Llama a la capa de negocio para obtener los clientes
            foreach (Cliente item in lista)
            {
                dgvData.Rows.Add(new object[] { item.DNI, item.NombreCompleto });
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;

            if (iRow >= 0) // Verifica si se ha hecho clic en una fila válida
            {
                try
                {
                    // Obtener los datos del cliente seleccionado y asignarlos al objeto _Cliente
                    _Cliente = new Cliente()
                    {
                        DNI = dgvData.Rows[iRow].Cells["DNI"].Value.ToString(),
                        NombreCompleto = dgvData.Rows[iRow].Cells["NombreCompleto"].Value.ToString()
                    };

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al seleccionar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBuscar.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
