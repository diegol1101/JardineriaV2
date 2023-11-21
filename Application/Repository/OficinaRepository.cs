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
    public class OficinaRepository : GenericRepository<Oficina>, IOficina
    {
        protected readonly JardineriaContext _context;

        public OficinaRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Oficina>> GetAllAsync()
        {
            return await _context.Oficinas
                .ToListAsync();
        }

        public override async Task<Oficina> GetByIdAsync(string id)
        {
            return await _context.Oficinas

            .FirstOrDefaultAsync(p => p.CodigoOficina == id);
        }

        public async Task<IEnumerable<Oficina>> OficinasNoEmFrutales()
        {
            var entities = await _context
            .Oficinas
            .Where(o => !_context.Empleados.Any(e => e.CodigoOficina == o.CodigoOficina &&
                _context.Clientes.Any(c => c.CodigoEmpleadoRepVentas == e.CodigoEmpleado &&
                    _context.Pedidos.Any(p => p.CodigoCliente == c.CodigoCliente &&
                        _context.DetallePedidos.Any(dp => dp.CodigoPedido == p.CodigoPedido &&
                            _context.Productos.Any(pr =>
                                pr.CodigoProducto == dp.CodigoProducto &&
                                pr.Gama == "Frutales"
                            )
                        )
                    )
                )
            ))
            .ToListAsync();

            return entities;
        }
    }
}