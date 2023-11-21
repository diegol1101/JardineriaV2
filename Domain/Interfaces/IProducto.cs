using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProducto:IGenericRepository<Producto>
    {
        Task<IEnumerable<object>> GamaOrnamentales();
        Task<object> ProductoConPrecioMasAlto();
        Task<string> ProductoMasVendido();
        Task<string> ProductoPrecioMasCaro();
        Task<IEnumerable<object>> ProductosSinPedidos();
    }
}