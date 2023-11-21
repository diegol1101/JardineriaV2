using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class EmpleadoDto
    {
        public int CodigoEmpleado { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido1 { get; set; } = null!;

        public string Apellido2 { get; set; } = null!;

        public string Extension { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string CodigoOficina { get; set; }

        public int CodigoJefe { get; set; }

        public string Puesto { get; set; }
    }
}