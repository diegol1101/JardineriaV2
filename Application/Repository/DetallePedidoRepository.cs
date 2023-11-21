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
    public class DetallePedidoRepository : GenericRepository<DetallePedido>, IDetallePedido
    {
        protected readonly JardineriaContext _context;

        public DetallePedidoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<DetallePedido>> GetAllAsync()
        {
            return await _context.DetallePedidos
                .ToListAsync();
        }

        public override async Task<DetallePedido> GetByIdAsync(int id)
        {
            return await _context.DetallePedidos
            .FirstOrDefaultAsync(p => p.CodigoPedido == id);
        }



    }
}