using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PagoDto
    {
        public int CodigoCliente { get; set; }

        public string FormaPago { get; set; } = null!;

        public string IdTransaccion { get; set; } = null!;

        public DateOnly FechaPago { get; set; }

        public decimal Total { get; set; }
    }
}