﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioUnidad { get; set; }
        public int Cantidad { get; set; }
        public DateTime Vencimiento { get; set; }
        public bool Estado { get; set; }

    }
}
