using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public Cliente objCliente { get; set; }
        public Usuario objUsuario { get; set; }
        public Cronograma_Pago objCronogramaPago{ get; set; }
        public Pedido objPedido { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public double MontoPago { get; set; }
        public double MontoCambio { get; set; }
        public double MontoTotal { get; set; }
        public List<Detalle_Venta> objDetalle_Venta { get; set; }
        public string FechaRegistro { get; set; }
    }
}
