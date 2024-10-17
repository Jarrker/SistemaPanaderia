using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Detalle_Compra
    {
        public int IdDetalleCompra { get; set; }
        public Insumos objInsumos { get; set; }
        public double PrecioCompra { get; set; }
        public int Cantidad { get; set; }
        public double MontoTotal { get; set; }
        public string FechaRegistro { get; set; }
    }
}
