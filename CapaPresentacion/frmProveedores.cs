using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace CapaPresentacion
{
    public partial class frmProveedores : Form
    {

        private readonly string apiToken = "apis-token-11329.FpNkJa70KtxsL2k6IKiuRxSqyQYckuWi";

        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
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

            List<Proveedor> lista = new CN_Proveedor().Listar();

            foreach (Proveedor item in lista)
            {
                dgvData.Rows.Add(new object[] {"",item.IdProveedor,item.RUC,item.RazonSocial,item.Rubro,item.Ciudad,
                    item.Estado == true ? 1 : 0 ,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtRUC.Text = "";
            txtRazonSocial.Text = "";
            txtRubro.Text = "";
            txtCiudad.Text = "";
            cboEstado.SelectedIndex = 0;
            txtRUC.Select();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Proveedor obj = new Proveedor()
            {
                IdProveedor = Convert.ToInt32(txtId.Text),
                RUC = txtRUC.Text,
                RazonSocial = txtRazonSocial.Text,
                Rubro = txtRubro.Text,
                Ciudad = txtCiudad.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdProveedor == 0)
            {
                int idgenerado = new CN_Proveedor().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {

                    dgvData.Rows.Add(new object[] {"",idgenerado,txtRUC.Text,txtRazonSocial.Text,txtRubro.Text,txtCiudad.Text,
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
                bool resultado = new CN_Proveedor().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["RUC"].Value = txtRUC.Text;
                    row.Cells["RazonSocial"].Value = txtRazonSocial.Text;
                    row.Cells["Rubro"].Value = txtRubro.Text;
                    row.Cells["Ciudad"].Value = txtCiudad.Text;
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
                return;

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
                    txtRUC.Text = dgvData.Rows[indice].Cells["RUC"].Value.ToString();
                    txtRazonSocial.Text = dgvData.Rows[indice].Cells["RazonSocial"].Value.ToString();
                    txtRubro.Text = dgvData.Rows[indice].Cells["Rubro"].Value.ToString();
                    txtCiudad.Text = dgvData.Rows[indice].Cells["Ciudad"].Value.ToString();

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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el proveedor", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Proveedor obj = new Proveedor()
                    {
                        IdProveedor = Convert.ToInt32(txtId.Text)
                    };

                    bool respuesta = new CN_Proveedor().Eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private async void txtRUC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string ruc = txtRUC.Text;
            if (string.IsNullOrEmpty(ruc))
            {
                MessageBox.Show("Por favor, ingrese un RUC válido.");
                return;
            }

            txtRazonSocial.Text = "Consultando...";
            txtCiudad.Text = "Consultando...";

            try
            {
                var empresa = await ObtenerDatosPorRUC(ruc);

                if (empresa != null)
                {
                    txtRazonSocial.Text = empresa.nombre ?? "No encontrado";
                    txtCiudad.Text = empresa.distrito ?? "No encontrado";
                }
                else
                {
                    txtRazonSocial.Text = "No se encontró el RUC.";
                    txtCiudad.Text = "No se encontró el RUC.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar el RUC: {ex.Message}");
                txtRazonSocial.Text = string.Empty;
                txtCiudad.Text = string.Empty;
            }
        }

        private async Task<Empresa> ObtenerDatosPorRUC(string ruc)
        {
            string apiUrl = $"https://api.apis.net.pe/v2/sunat/ruc?numero={ruc}";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Empresa>(json);
                }
                else
                {
                    MessageBox.Show("Error al consultar la API.");
                    return null;
                }
            }
        }

        private class Empresa
        {
            public string nombre { get; set; }
            public string distrito { get; set; }
        }
    }
}
