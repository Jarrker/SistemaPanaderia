﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Detalle_Venta
    {
        public int IdDetalleVenta { get; set; }
        public Producto objProducto { get; set; }
        public double PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }
        public string FechaRegistro { get; set; }
    }
}
