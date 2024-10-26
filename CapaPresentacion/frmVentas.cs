using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmVentas : Form
    {
        public frmVentas()
        {
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cboTipoDocumento.DisplayMember = "Texto";
            cboTipoDocumento.ValueMember = "Valor";
            cboTipoDocumento.SelectedIndex = 0;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdProducto.Text = "0";

            txtPagocon.Text = "";
            txtCambio.Text = "";
            txtTotalPagar.Text = "0";
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtDNI.Text = modal._Cliente.DNI;
                    txtNombreCliente.Text = modal._Cliente.NombreCompleto;
                    txtCodProducto.Select();
                }
                else
                {
                    txtDNI.Select();
                }
            }
        }

        private void bntBuscarProducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK && modal._Producto != null)
                {
                    txtCodProducto.Text = modal._Producto.IdProducto.ToString();
                    txtProducto.Text = modal._Producto.Nombre;
                    txtPrecio.Text = modal._Producto.PrecioUnidad.ToString("0.00");
                    txtStock.Text = modal._Producto.Cantidad.ToString();
                    txtCantidad.Select();
                }
                else
                {
                    txtCodProducto.Select();
                }
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnCrearVenta_Click(object sender, EventArgs e)
        {
            var venta = new
            {
                TipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Valor,
                DNICliente = txtDNI.Text,
                NombreCliente = txtNombreCliente.Text,
                FechaVenta = DateTime.Now,
                TotalPagar = decimal.Parse(txtTotalPagar.Text),
                Productos = new[]
                {
                    new {
                        IdProducto = int.Parse(txtCodProducto.Text),
                        Cantidad = int.Parse(txtCantidad.Text),
                        Precio = decimal.Parse(txtPrecio.Text)
                    }
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(venta);
            var apiUrl = "https://apisunat.com/671d79e5e0eb860015f3efde"; 

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "671d79e5e0eb860015f3efde"); 
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Venta registrada: " + responseBody);
                }
                else
                {
                    MessageBox.Show("Error al registrar la venta: " + response.StatusCode);
                }
            }
        }
    }
}
