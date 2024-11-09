using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using Newtonsoft.Json;
using System;
using System.Linq;
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
                // Asegúrate de que se esté accediendo a la columna "SubTotal" correctamente
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
            txtCantidad.Text = "1";
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

        private async void btnCrearVenta_Click_1(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos en la venta. Agrega al menos un producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var data = new
            {
                empresa = new
                {
                    ruc = "20604051984",
                    razon_social = "FACTURACION ELECTRONICA MONSTRUO E.I.R.L.",
                    nombre_comercial = "FACTURACION INTEGRAL",
                    domicilio_fiscal = "AV. LA MOLINA NRO. 570",
                    ubigeo = "150114",
                    urbanizacion = "RESIDENCIAL MONTERRICO",
                    distrito = "LA MOLINA",
                    provincia = "LIMA",
                    departamento = "LIMA",
                    modo = "0",
                    usu_secundario_produccion_user = "MODDATOS",
                    usu_secundario_produccion_password = "MODDATOS"
                },
                cliente = new
                {
                    razon_social_nombres = txtNombreCliente.Text,
                    numero_documento = txtDNI.Text,
                    codigo_tipo_entidad = cboTipoDocumento.SelectedItem.ToString() == "Factura" ? "6" : "1",
                    cliente_direccion = "Dirección del cliente aquí"
                },
                venta = new
                {
                    serie = cboTipoDocumento.SelectedItem.ToString() == "Factura" ? "FF03" : "BB01",
                    numero = "53953",
                    fecha_emision = DateTime.Now.ToString("yyyy-MM-dd"),
                    hora_emision = DateTime.Now.ToString("HH:mm:ss"),
                    fecha_vencimiento = "",
                    moneda_id = "1",
                    forma_pago_id = "1",
                    total_gravada = (Convert.ToDecimal(txtTotalPagar.Text) / 1.18m).ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
                    total_igv = (Convert.ToDecimal(txtTotalPagar.Text) - (Convert.ToDecimal(txtTotalPagar.Text) / 1.18m)).ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
                    total_exonerada = "",
                    total_inafecta = "",
                    tipo_documento_codigo = ((OpcionCombo)cboTipoDocumento.SelectedItem).Valor == "Boleta" ? "03" : "01",
                    nota = "Los mejores productos panaderos",
                    descuento_global = "0"
                },
                items = dgvData.Rows.Cast<DataGridViewRow>().Select(row => new
                {
                    producto = row.Cells["Producto"].Value.ToString(),
                    cantidad = row.Cells["Cantidad"].Value.ToString(),
                    precio_base = Convert.ToDecimal(row.Cells["Precio"].Value).ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
                    codigo_sunat = "-",
                    codigo_producto = row.Cells["IdProducto"].Value.ToString(),
                    codigo_unidad = "NIU",
                    tipo_igv_codigo = "10"
                }).ToList()
            };

            var jsonRequest = JsonConvert.SerializeObject(data);
            var apiUrl = "https://facturaciondirecta.com/API_SUNAT/post.php";
            using (var client = new HttpClient())
            {
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    int jsonEndIndex = responseBody.LastIndexOf('}');
                    if (jsonEndIndex >= 0)
                    {
                        responseBody = responseBody.Substring(0, jsonEndIndex + 1);
                    }

                    try
                    {
                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);

                        // Asignar enlaces a los LinkLabels
                        LinkLabelXml.Text = "XML";
                        LinkLabelXml.Links.Clear();
                        LinkLabelXml.Links.Add(0, LinkLabelXml.Text.Length, jsonResponse.data.ruta_xml.ToString());

                        LinkLabelCdr.Text = "CDR";
                        LinkLabelCdr.Links.Clear();
                        LinkLabelCdr.Links.Add(0, LinkLabelCdr.Text.Length, jsonResponse.data.ruta_cdr.ToString());

                        LinkLabelPdf.Text = "PDF";
                        LinkLabelPdf.Links.Clear();
                        LinkLabelPdf.Links.Add(0, LinkLabelPdf.Text.Length, jsonResponse.data.ruta_pdf.ToString());

                        MessageBox.Show("Enlaces generados correctamente. Haga clic en el enlace para abrir.", "Enlaces de documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (JsonException jsonEx)
                    {
                        MessageBox.Show("Error al procesar la respuesta JSON: " + jsonEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Error al registrar la venta: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LinkLabelXml_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Link.LinkData.ToString()) { UseShellExecute = true });
        }

        private void LinkLabelCdr_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Link.LinkData.ToString()) { UseShellExecute = true });
        }

        private void LinkLabelPdf_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Link.LinkData.ToString()) { UseShellExecute = true });
        }

    }
}
