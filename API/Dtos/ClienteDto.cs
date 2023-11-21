using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ClienteDto
    {
        public int CodigoCliente { get; set; }

        public string NombreCliente { get; set; } = null!;

        public string NombreContacto { get; set; } = null!;

        public string ApellidoContacto { get; set; }

        public string Telefono { get; set; } = null!;

        public string Fax { get; set; } = null!;

        public string LineaDireccion1 { get; set; } = null!;

        public string LineaDireccion2 { get; set; }

        public string Ciudad { get; set; } = null!;

        public string Region { get; set; }

        public string Pais { get; set; }

        public string CodigoPostal { get; set; }

        public int CodigoEmpleadoRepVentas { get; set; }

        public decimal? LimiteCredito { get; set; }
    }
}