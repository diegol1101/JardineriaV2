using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmpleado :IGenericRepository<Empleado>
    {
        Task<object> CuantosEmpleados ();
        Task<IEnumerable<object>> EmpleadosSinVentas();
        Task<IEnumerable<object>> EmpleadosSinVentas4();
    }
}