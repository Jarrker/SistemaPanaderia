using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Http;
using CapaPresentacion.CodigoUsuario;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Keys = System.Windows.Forms.Keys;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CapaPresentacion
{
    public partial class frmClientes : Form
    {
        private readonly string apiToken = "apis-token-11329.FpNkJa70KtxsL2k6IKiuRxSqyQYckuWi";
        private ChromeDriverService _servicioChromeDriver;
        private IWebDriver _driver;
        private bool estaHabilitadoPagina = false;

        public frmClientes()
        {
            InitializeComponent();
            IniciarPaginaPrincipal();
        }

        private void IniciarPaginaPrincipal()
        {
            estaHabilitadoPagina = false;
            try
            {
                CuProceso oCuProceso = new CuProceso();
                oCuProceso.FinalizarArbolProcesos("chromedriver.exe");
            }
            catch { }

            _servicioChromeDriver = ChromeDriverService.CreateDefaultService();
            _servicioChromeDriver.HideCommandPromptWindow = true;

            var opcionChrome = new ChromeOptions();
            opcionChrome.AddArgument("headless");
            opcionChrome.AddArgument(
                "user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36");
            _driver = new ChromeDriver(_servicioChromeDriver, opcionChrome);
            Task.Run(async () =>
            {
                _driver.Navigate().GoToUrl("https://www.google.com/");
                await Task.Delay(500);
                _driver.Navigate().GoToUrl("https://aplicaciones007.jne.gob.pe/srop_publico/Consulta/Afiliado");
                await Task.Delay(100);
                estaHabilitadoPagina = true;
            });
        }

        private async Task<EnRespuesta.Respuesta> EsperarBusqueda(IWebDriver driver, By busqueda, int tiempoIntervalo = 500, int tiempoFinalizacion = 30000)
        {
            EnRespuesta.Respuesta oRespuesta = new EnRespuesta.Respuesta();
            oRespuesta.TipoRespuesta = EnRespuesta.TipoRespuesta.Inconveniente;

            try
            {
                Stopwatch oCronometro = new Stopwatch();
                oCronometro.Start();
                bool esElementoVisible = false;
                IWebElement elemento = null;

                while (!esElementoVisible && oCronometro.ElapsedMilliseconds <= tiempoFinalizacion)
                {
                    await Task.Delay(tiempoIntervalo);
                    try
                    {
                        elemento = driver.FindElement(busqueda);
                        if (elemento != null)
                            esElementoVisible = elemento.Displayed;
                    }
                    catch (NoSuchElementException ex)
                    {
                        oRespuesta.MensajeRespuesta = ex.Message;
                    }
                }

                oCronometro.Stop();

                if (elemento == null)
                {
                    if (oCronometro.ElapsedMilliseconds > tiempoFinalizacion)
                        oRespuesta.MensajeRespuesta = "Se terminó el tiempo de finalización " + tiempoFinalizacion + ". " + oRespuesta.MensajeRespuesta;
                }
                else
                {
                    oRespuesta.TipoRespuesta = esElementoVisible ? EnRespuesta.TipoRespuesta.Correcto : EnRespuesta.TipoRespuesta.Inconveniente;
                    oRespuesta.MensajeRespuesta = esElementoVisible.ToString();
                }
            }
            catch (Exception ex)
            {
                oRespuesta.TipoRespuesta = EnRespuesta.TipoRespuesta.Error;
                oRespuesta.MensajeRespuesta = ex.Message;
            }

            return oRespuesta;
        }

        private void frmClientes_Load(object sender, EventArgs e)
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

            List<Cliente> lista = new CN_Cliente().Listar();

            foreach (Cliente item in lista)
            {
                dgvData.Rows.Add(new object[] {"",item.IdCliente,item.DNI,item.NombreCompleto,item.Direccion,item.Telefono,
                    item.Estado == true ? 1 : 0 ,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            cboEstado.SelectedIndex = 0;
            txtDocumento.Select();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Cliente obj = new Cliente()
            {
                IdCliente = Convert.ToInt32(txtId.Text),
                DNI = txtDocumento.Text,
                NombreCompleto = txtNombreCompleto.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdCliente == 0)
            {
                int idgenerado = new CN_Cliente().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {

                    dgvData.Rows.Add(new object[] {"",idgenerado,txtDocumento.Text,txtNombreCompleto.Text,txtDireccion.Text,txtTelefono.Text,
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
                bool resultado = new CN_Cliente().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["DNI"].Value = txtDocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtNombreCompleto.Text;
                    row.Cells["Direccion"].Value = txtDireccion.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
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
                    txtDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtDireccion.Text = dgvData.Rows[indice].Cells["Direccion"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"].Value.ToString();

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
                if (MessageBox.Show("¿Desea eliminar el cliente", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Cliente obj = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtId.Text)
                    };

                    bool respuesta = new CN_Cliente().Eliminar(obj, out mensaje);

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

        private async void txtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string dni = txtDocumento.Text;
            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show("Por favor, ingrese un DNI válido.");
                return;
            }

            txtNombreCompleto.Text = "Consultando...";

            try
            {
                string nombreCompleto = await ObtenerNombrePorDNI(dni);

                if (!string.IsNullOrEmpty(nombreCompleto))
                {
                    txtNombreCompleto.Text = nombreCompleto;
                }
                else
                {
                    txtNombreCompleto.Text = "No se encontró el DNI.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar el DNI: {ex.Message}");
                txtNombreCompleto.Text = string.Empty;
            }
        }

        private async Task<string> ObtenerNombrePorDNI(string dni)
        {
            string apiUrl = $"https://api.apis.net.pe/v2/reniec/dni?numero={dni}";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var persona = JsonSerializer.Deserialize<Persona>(json);
                    return $"{persona.apellidoPaterno} {persona.apellidoMaterno}, {persona.nombres}";
                }
                else
                {
                    MessageBox.Show("Error al consultar la API.");
                    return null;
                }
            }
        }

        private class Persona
        {
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
            public string nombres { get; set; }
        }
    }
}
