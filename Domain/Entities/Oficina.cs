using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Oficina
{
    public string CodigoOficina { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public string Telefono { get; set; }

    public string LineaDireccion1 { get; set; }

    public string LineaDireccion2 { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
