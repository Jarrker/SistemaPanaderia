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

namespace CapaPresentacion
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
            ///adsadadadsasdasd
            ///asddasda
            ///adssadads
            ///dasdassd
            ///ASDASDAS
            ///DASDADS
            ///A
            ///DSASD
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;



            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;


            //Mostrar lista de Productos
            List<Producto> listaProducto = new CN_Producto().Listar();

            foreach (Producto item in listaProducto)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.PrecioUnidad,
                    item.Cantidad,
                    item.Vencimiento,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            dtpFechaVencimiento.Text = "";
            cboEstado.SelectedIndex = 0;

            txtCodigo.Select();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            String mensaje = string.Empty;

            Producto objProducto = new Producto()
            {
                IdProducto = Convert.ToInt32(txtId.Text),
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Vencimiento = dtpFechaVencimiento.Value,
                Cantidad = 1,
                PrecioUnidad = 10,
                //objCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (objProducto.IdProducto == 0)
            {
                int idgenerado = new CN_Producto().Registrar(objProducto, out mensaje);

                if (idgenerado != 0)
                {
                    dgvData.Rows.Add(new object[] {
                        "",
                        idgenerado,
                        txtCodigo.Text,
                        txtNombre.Text,
                        txtDescripcion.Text,
                        //((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString(),
                        //((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString(),
                        "0.00",
                        "0",
                        //Vencimiento.HeaderText = dtpFechaVencimiento.Value,
                        ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
                    });
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Producto().Editar(objProducto, out mensaje); ///

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["Codigo"].Value = txtCodigo.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Descripcion"].Value = txtDescripcion.Text;
                    row.Cells["PrecioUnidad"].Value = "0.0";
                    row.Cells["Cantidad"].Value = "0";
                    row.Cells["Vencimiento"].Value = dtpFechaVencimiento;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check32.Width;
                var h = Properties.Resources.check32.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check32, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtCodigo.Text = dgvData.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["Descripcion"].Value.ToString();



                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            //if (dgvData.Rows.Count < 1)
            //{
            //    MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else
            //{
            //    DataTable dt = new DataTable();

            //    foreach (DataGridViewColumn columna in dgvData.Columns)
            //    {
            //        if (columna.HeaderText != "" && columna.Visible)
            //            dt.Columns.Add(columna.HeaderText, typeof(string));
            //    }

            //    foreach (DataGridViewRow row in dgvData.Rows)
            //    {
            //        if (row.Visible)
            //            dt.Rows.Add(new object[] {
            //                row.Cells[2].Value.ToString(),
            //                row.Cells[3].Value.ToString(),
            //                row.Cells[4].Value.ToString(),
            //                row.Cells[6].Value.ToString(),
            //                row.Cells[7].Value.ToString(),
            //                //row.Cells[8].Value.ToString(),
            //                row.Cells[9].Value.ToString(),
            //                //row.Cells[11].Value.ToString(),

            //            });
            //    }

            //    SaveFileDialog savefile = new SaveFileDialog();
            //    savefile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
            //    savefile.Filter = "Excel Files | *.xlsx";

            //    if (savefile.ShowDialog() == DialogResult.OK)
            //    {
            //        try
            //        {
            //            XLWorkbook wb = new XLWorkbook();
            //            var hoja = wb.Worksheets.Add(dt, "Informe");
            //            hoja.ColumnsUsed().AdjustToContents();
            //            wb.SaveAs(savefile.FileName);
            //            MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        }
            //        catch
            //        {
            //            MessageBox.Show("Error al generar reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        }

            //    }

            //}
        }
    }
}
