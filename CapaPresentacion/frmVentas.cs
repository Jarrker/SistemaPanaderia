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
        private void btnAgregarVenta_Click(object sender, EventArgs e)
        {
            // Validar que se hayan ingresado todos los datos necesarios
            if (string.IsNullOrEmpty(txtCodProducto.Text) || txtCodProducto.Text == "0")
            {
                MessageBox.Show("Por favor, selecciona un producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodProducto.Select();
                return;
            }

            // Validar cantidad
            if (string.IsNullOrEmpty(txtCantidad.Text) || Convert.ToInt32(txtCantidad.Text) <= 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantidad.Select();
                return;
            }

            // Obtener los datos del producto
            string nombreProducto = txtProducto.Text;
            double precioUnidad = Convert.ToDouble(txtPrecio.Text);
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            double subtotal = precioUnidad * cantidad;

            // Agregar el producto al DataGridView
            dgvData.Rows.Add(new object[] { txtCodProducto.Text, nombreProducto, cantidad, precioUnidad.ToString("0.00"), subtotal.ToString("0.00") });

            // Actualizar el total a pagar
            ActualizarTotal();

            // Limpiar los campos de producto después de agregar
            LimpiarCamposProducto();
        }


        private void ActualizarTotal()
        {
            double total = 0;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                // Acceder al valor de la columna "SubTotal" por su nombre exacto
                total += Convert.ToDouble(row.Cells["SubTotal"].Value);
            }
            txtTotalPagar.Text = total.ToString("0.00");
        }


        private void LimpiarCamposProducto()
        {
            txtCodProducto.Text = "0";
            txtProducto.Text = "";
            txtPrecio.Text = "0.00";
            txtStock.Text = "0";
            txtCantidad.Text = "1"; // Reiniciar cantidad a 1 o al valor predeterminado que desees
        }

        private void txtPagocon_TextChanged(object sender, EventArgs e)
        {
            // Calcular el cambio automáticamente cuando el usuario ingresa el monto pagado
            if (double.TryParse(txtPagocon.Text, out double pagocon) && double.TryParse(txtTotalPagar.Text, out double totalPagar))
            {
                double cambio = pagocon - totalPagar;
                txtCambio.Text = cambio >= 0 ? cambio.ToString("0.00") : "0.00";
            }
            else
            {
                txtCambio.Text = "0.00";
            }
        }

        private async void btnCrearVenta_Click(object sender, EventArgs e)
        {
            // Verificar que hay productos en el DataGridView
            if (dgvData.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos en la venta. Agrega al menos un producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear la estructura de la venta y enviarla a la API (o base de datos)
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
