using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPago:IGenericRepository<Pago>
    {
        Task<IEnumerable<Pago>> PagosEn2008ConPaypal();
        Task<IEnumerable<object>> MetodosDePago();
        Task<decimal?> PagoMedioEn2009();
        Task<IEnumerable<object>> SumaTotalPagosPorAño();
    }
}