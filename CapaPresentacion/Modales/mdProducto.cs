using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Modales
{
    public partial class mdProducto : Form
    {
        public Producto _Producto { get; set; }

        public mdProducto()
        {
            InitializeComponent();
            dgvData.CellDoubleClick += dgvData_CellDoubleClick; // Vincula el evento CellDoubleClick
        }



        private void mdProducto_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true)
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            List<Producto> lista = new CN_Producto().Listar();

            foreach (Producto item in lista)
            {
                dgvData.Rows.Add(new object[] { item.IdProducto, item.Nombre, item.PrecioUnidad, item.Cantidad, item.Vencimiento });
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;

            if (iRow >= 0) // Verifica si se ha hecho clic en una fila válida
            {
                try
                {
                    _Producto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(dgvData.Rows[iRow].Cells[0].Value), // IdProducto
                        Nombre = dgvData.Rows[iRow].Cells[1].Value.ToString(), // Nombre
                        PrecioUnidad = Convert.ToDouble(dgvData.Rows[iRow].Cells[2].Value), // PrecioUnidad
                        Cantidad = Convert.ToInt32(dgvData.Rows[iRow].Cells[3].Value), // Cantidad
                        Vencimiento = Convert.ToDateTime(dgvData.Rows[iRow].Cells[4].Value) // Vencimiento
                    };

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al seleccionar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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