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
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
    {
        protected readonly JardineriaContext _context;

        public EmpleadoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Empleado>> GetAllAsync()
        {
            return await _context.Empleados
                .ToListAsync();
        }

        public override async Task<Empleado> GetByIdAsync(int id)
        {
            return await _context.Empleados

            .FirstOrDefaultAsync(p => p.CodigoEmpleado == id);
        }

        public async Task<object> CuantosEmpleados()
        {
            return new { EmployeesQuantity = await _context.Empleados.CountAsync() };
        }

        public async Task<IEnumerable<object>> EmpleadosSinVentas()
        {
            var empleadosSinVentas = await _context.Empleados
                .Where(e => !e.Clientes.Any(c => c.Pagos.Any()))
                .Select(e => new
                {
                    Nombre = e.Nombre,
                    Apellidos = $"{e.Apellido1} {e.Apellido2}",
                    Puesto = e.Puesto,
                    Telefono = e.Extension
                })
                .ToListAsync();

            return empleadosSinVentas;
        }

        public async Task<IEnumerable<object>> EmpleadosSinVentas4()
        {
            var query = from empleado in _context.Empleados
                        join oficina in _context.Oficinas on empleado.CodigoOficina equals oficina.CodigoOficina
                        where empleado.Puesto != "Representante de Ventas" &&
                              !_context.Clientes.Any(cliente => cliente.CodigoEmpleadoRepVentas == empleado.CodigoEmpleado)
                        select new
                        {
                            empleado.Nombre,
                            empleado.Apellido1,
                            empleado.Apellido2,
                            empleado.Puesto,
                            oficina.Telefono
                        };

            return await query.ToListAsync();
        }

    }
}