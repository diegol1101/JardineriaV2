using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Producto
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

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual GamaProducto GamaNavigation { get; set; } = null!;
}
