using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Rubro { get; set; }
        public string Ciudad { get; set; }
        public bool Estado { get; set; }
        public List<Detalle_Compra> objDetalleCompra { get; set; }
        public string FechaRegistro { get; set; }
    }
}
