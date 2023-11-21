using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedido
    {
        protected readonly JardineriaContext _context;

        public PedidoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos
                .ToListAsync();
        }

        public override async Task<Pedido> GetByIdAsync(int id)
        {
            return await _context.Pedidos
            .FirstOrDefaultAsync(p => p.CodigoPedido == id);
        }

        public async Task<IEnumerable<object>> EstadosDePedidos()
        {
            var estados = await (
                from e in _context.Pedidos
                where e.Estado != null
                select new
                {
                    Estado = e.Estado
                }
            )
            .Distinct()
            .ToListAsync();

            return estados;
        }

        public async Task<IEnumerable<Pedido>> PedidosNoEntregadosATiempo()
        {
            var pedidos = await _context.Pedidos
                            .Where(p => p.FechaEntrega.HasValue && p.FechaEntrega > p.FechaEsperada)
                            .ToListAsync();
            return pedidos;
        }

        public async Task<IEnumerable<object>> PedidosDosDiasAntes()
        {
            var dias = await (

                from pe in _context.Pedidos
                where EF.Functions.DateDiffDay(pe.FechaEntrega, pe.FechaEsperada) > 2
                select new
                {
                    IdPedido = pe.CodigoPedido,
                    IdCliente = pe.CodigoCliente,
                    FechaEsperada = pe.FechaEsperada,
                    FechaEntrega = pe.FechaEntrega
                }
            )
            .Distinct()
            .ToListAsync();
            return dias;
        }

        public async Task<IEnumerable<Pedido>> PedidosRechazadosEn2009()
        {
            var pedidosRechazados = await _context.Pedidos
                .Where(p => p.Estado == "Rechazado" && p.FechaPedido.Year == 2009)
                .ToListAsync();

            return pedidosRechazados;
        }

        public async Task<IEnumerable<Pedido>> PedidosEntregadosEnEnero()
        {
            var EntregadosEnEnero = await _context.Pedidos
                .Where(p => p.FechaEntrega.HasValue && p.FechaEntrega.Value.Month == 1)
                .ToListAsync();

            return EntregadosEnEnero;
        }

        public async Task<IEnumerable<object>> ProductosSinPedidos()
        {
            var query = from producto in _context.Productos
                        join detallePedido in _context.DetallePedidos on producto.CodigoProducto equals detallePedido.CodigoProducto into detallesPedidos
                        from detallePedido in detallesPedidos.DefaultIfEmpty()
                        where detallePedido == null
                        select new
                        {
                            producto.CodigoProducto,
                            producto.Nombre,
                            producto.Gama,
                            producto.Dimensiones,
                            producto.Proveedor,
                            producto.DescripcionText,
                            producto.CantidadEnStock,
                            producto.PrecioVenta,
                            producto.PrecioProveedor
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<object>> PedidosPorEstado()
        {
            var pedidosPorEstado = await _context.Pedidos
                .GroupBy(p => p.Estado)
                .Select(g => new { Estado = g.Key, CantidadPedidos = g.Count() })
                .OrderByDescending(result => result.CantidadPedidos)
                .ToListAsync();

            return pedidosPorEstado;
        }

        public async Task<IEnumerable<object>> ProductosEnPedidos()
        {
            var resultados = await _context.Pedidos
                .Select(p => new
                {
                    CodigoPedido = p.CodigoPedido,
                    NumeroProductos = p.DetallePedidos.Select(dp => dp.CodigoProducto).Distinct().Count()
                })
                .ToListAsync();

            return resultados;
        }

        public async Task<IEnumerable<object>> SumaCantidadTotalEnPedidos()
        {
            var resultados = await _context.Pedidos
                .Select(p => new
                {
                    CodigoPedido = p.CodigoPedido,
                    SumaCantidadTotal = p.DetallePedidos.Sum(dp => dp.Cantidad)
                })
                .ToListAsync();

            return resultados;
        }

        public async Task<IEnumerable<object>> ProductosMasVendidos()
        {
            var productosVendidos = await _context.DetallePedidos
                .GroupBy(dp => dp.CodigoProducto)
                .Select(group => new
                {
                    CodigoProducto = group.Key,
                    TotalUnidadesVendidas = group.Sum(dp => dp.Cantidad)
                })
                .OrderByDescending(result => result.TotalUnidadesVendidas)
                .Take(20)
                .ToListAsync();

            return productosVendidos;
        }

        public async Task<IEnumerable<object>> ProductosMasVendidosPorCodigo()
        {
            var productosVendidos = await _context.DetallePedidos
                .GroupBy(dp => dp.CodigoProducto)
                .Select(group => new
                {
                    CodigoProducto = group.Key,
                    TotalUnidadesVendidas = group.Sum(dp => dp.Cantidad)
                })
                .OrderByDescending(result => result.TotalUnidadesVendidas)
                .Take(20)
                .ToListAsync();

            return productosVendidos;
        }

        public async Task<IEnumerable<object>> ProductosMasVendidosPorOR()
        {
            var productosVendidos = await _context.DetallePedidos
                .Where(dp => dp.CodigoProducto.StartsWith("OR"))
                .GroupBy(dp => dp.CodigoProducto)
                .Select(group => new
                {
                    CodigoProducto = group.Key,
                    TotalUnidadesVendidas = group.Sum(dp => dp.Cantidad)
                })
                .OrderByDescending(result => result.TotalUnidadesVendidas)
                .Take(20)
                .ToListAsync();

            return productosVendidos;
        }

        public async Task<IEnumerable<object>> VentasMayoresA3000Iva()
        {
            var ventas = await _context.DetallePedidos
                .Join(
                    _context.Productos,
                    detalle => detalle.CodigoProducto,
                    producto => producto.CodigoProducto,
                    (detalle, producto) => new { detalle, producto }
                )
                .Where(joinResult => (joinResult.detalle.Cantidad * joinResult.detalle.PrecioUnidad) > 3000)
                .Select(result => new
                {
                    NombreProducto = result.producto.Nombre,
                    UnidadesVendidas = result.detalle.Cantidad,
                    TotalFacturado = result.detalle.Cantidad * result.detalle.PrecioUnidad,
                    TotalFacturadoConIVA = (decimal)(result.detalle.Cantidad) *(decimal)(result.detalle.PrecioUnidad )*(decimal)( 1.21) 
                })
                .ToListAsync();

            return ventas;
        }










    }
}