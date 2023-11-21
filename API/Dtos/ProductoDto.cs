using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductoDto
    {
        public string CodigoProducto { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Gama { get; set; } = null!;

        public string Dimensiones { get; set; }

        public string Proveedor { get; set; }

        public string DescripcionText { get; set; }

        public short CantidadEnStock { get; set; }

        public decimal? PrecioVenta { get; set; }

        public decimal? PrecioProveedor { get; set; }
    }
}