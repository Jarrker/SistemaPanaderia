using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cronograma_Pago
    {
        public int IdCronogramaPago { get; set; }
        public Contrato objContrato { get; set; }
        public double MontoPago { get; set; }
        public string FechaRegistro { get; set; }
    }
}
