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
    public class PagoRepository : GenericRepository<Pago>, IPago
    {
        protected readonly JardineriaContext _context;

        public PagoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Pago>> GetAllAsync()
        {
            return await _context.Pagos
                .ToListAsync();
        }

        public override async Task<Pago> GetByIdAsync(string id)
        {
            return await _context.Pagos
            .FirstOrDefaultAsync(p => p.IdTransaccion == id);
        }

        public async Task<IEnumerable<Pago>> PagosEn2008ConPaypal()
        {
            var pagosEn2008ConPaypal = await _context.Pagos
                .Where(p => p.FechaPago.Year == 2008 && p.FormaPago == "Paypal")
                .OrderByDescending(p => p.Total)
                .ToListAsync();

            return pagosEn2008ConPaypal;
        }

        public async Task<IEnumerable<object>> MetodosDePago()
        {
            var metodos = await _context.Pagos
                .Select(p => p.FormaPago)
                .Distinct()
                .ToListAsync();

            return metodos;
        }

        public async Task<decimal?> PagoMedioEn2009()
        {
            return await _context.Pagos
                .Where(p => p.FechaPago.Year == 2009)
                .AverageAsync(p => (decimal?)p.Total);
        }

        public async Task<IEnumerable<object>> SumaTotalPagosPorAño()
        {
            var pagosPorAño = await _context.Pagos
                .GroupBy(p => p.FechaPago.Year)
                .Select(group => new
                {
                    Año = group.Key,
                    SumaTotalPagos = group.Sum(p => p.Total)
                })
                .ToListAsync();

            return pagosPorAño;
        }



    }
}