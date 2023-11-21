using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class ProductoRepository : GenericRepository<Producto>, IProducto
    {
        protected readonly JardineriaContext _context;

        public ProductoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _context.Productos
                .ToListAsync();
        }

        public override async Task<Producto> GetByIdAsync(string id)
        {
            return await _context.Productos
            .FirstOrDefaultAsync(p => p.CodigoProducto == id);
        }

        public async Task<IEnumerable<object>> GamaOrnamentales()
        {
            var orna = await _context.Productos
                .Where(p => p.Gama.ToUpper() == "ornamentales" && p.CantidadEnStock > 100)
                .OrderByDescending(p => p.PrecioVenta)
                .ToListAsync();

            return orna;
        }

        public async Task<object> ProductoConPrecioMasAlto()
        {
            var producto = await _context.Productos
                .OrderByDescending(p => p.PrecioVenta)
                .Select(p => new
                {
                    Nombre = p.Nombre,
                    PrecioVenta = p.PrecioVenta
                })
                .FirstOrDefaultAsync();

            return producto;
        }

        public async Task<string> ProductoMasVendido()
        {
            var productoMasVendido = await _context.DetallePedidos
                .GroupBy(detalle => detalle.CodigoProducto)
                .Select(g => new
                {
                    ProductoId = g.Key,
                    TotalUnidadesVendidas = g.Sum(detalle => detalle.Cantidad)
                })
                .OrderByDescending(resultado => resultado.TotalUnidadesVendidas)
                .FirstOrDefaultAsync();

            var nombreProductoMasVendido = await _context.Productos
                .Where(producto => producto.CodigoProducto == productoMasVendido.ProductoId)
                .Select(producto => $"El producto mas vendido fue: {producto.Nombre} con {productoMasVendido.TotalUnidadesVendidas}")
                .FirstOrDefaultAsync();

            return nombreProductoMasVendido;
        }

        public async Task<string> ProductoPrecioMasCaro()
        {
            var productoMasCaro = await _context.Productos
                .Where(p => _context.Productos.All(p2 => p.PrecioVenta >= p2.PrecioVenta))
                .Select(p => p.Nombre)
                .FirstOrDefaultAsync();

            return productoMasCaro;
        }

        public async Task<IEnumerable<object>> ProductosSinPedidos()
        {
            var productosSinPedidos = await _context.Productos
                .Where(producto => !_context.DetallePedidos.Any(detalle => detalle.CodigoProducto == producto.CodigoProducto))
                .ToListAsync();

            return productosSinPedidos;
        }




    }
}