using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Pedido_Producto
    {
        
        public Pedido objPedido { get; set; }
        public Producto objProducto { get; set; }
        public int Cantidad { get; set; }

    }
}
