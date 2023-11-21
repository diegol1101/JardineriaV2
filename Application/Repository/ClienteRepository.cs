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
    public class ClienteRepository : GenericRepository<Cliente>, ICliente
    {
        protected readonly JardineriaContext _context;

        public ClienteRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes
                .ToListAsync();
        }

        public override async Task<Cliente> GetByIdAsync(int id)
        {
            return await _context.Clientes
            .FirstOrDefaultAsync(p => p.CodigoCliente == id);
        }

        public async Task<IEnumerable<Cliente>> ClientesEspanol()
        {
            var cli = await _context.Clientes
                    .Where(a => a.Pais.ToUpper() == "spain")
                    .ToListAsync();
            return cli;
        }

        public async Task<IEnumerable<object>> ClientesConPagosEn2008()
        {
            var clientes = await _context.Pagos
                            .Where(p => p.FechaPago.Year == 2008)
                            .Select(p => p.CodigoCliente.ToString())
                            .Distinct()
                            .ToListAsync();
            return clientes;
        }

        public async Task<IEnumerable<Cliente>> ClientesEnMadrid()
        {
            var clientesEnMadrid = await _context.Clientes
                .Where(c => c.Ciudad == "Madrid"
                && (c.CodigoEmpleadoRepVentas == 11 || c.CodigoEmpleadoRepVentas == 30))
                .ToListAsync();

            return clientesEnMadrid;
        }

        public async Task<IEnumerable<object>> ClientesConRepresentantesVentas()
        {
            var clientesConRepresentantes = await _context.Clientes
                .Include(c => c.CodigoEmpleadoRepVentasNavigation)
                .Select(c => new
                {
                    NombreCliente = c.NombreCliente,
                    NombreRepresentante = c.CodigoEmpleadoRepVentasNavigation.Nombre,
                    ApellidoRepresentante = c.CodigoEmpleadoRepVentasNavigation.Apellido1,
                    Apellido2Representante = c.CodigoEmpleadoRepVentasNavigation.Apellido2
                })
                .ToListAsync();

            return clientesConRepresentantes;
        }

        public async Task<IEnumerable<object>> ClientesConPagosYRepresentantesVentas()
        {
            var clientesConPagosYRepresentantes = await _context.Pagos
                .Include(p => p.CodigoClienteNavigation)
                .ThenInclude(c => c.CodigoEmpleadoRepVentasNavigation)
                .Where(p => p.CodigoClienteNavigation != null)
                .Select(p => new
                {
                    NombreCliente = p.CodigoClienteNavigation.NombreCliente,
                    NombreRepresentante = p.CodigoClienteNavigation.CodigoEmpleadoRepVentasNavigation.Nombre,
                    ApellidoRepresentante = p.CodigoClienteNavigation.CodigoEmpleadoRepVentasNavigation.Apellido1
                })
                .Distinct()
                .ToListAsync();

            return clientesConPagosYRepresentantes;
        }

        public async Task<IEnumerable<object>> ClientesNoPagosConRepresentantesVentas()
        {
            var clientesSinPagosYRepresentantes = await _context.Clientes
                .Include(c => c.CodigoEmpleadoRepVentasNavigation)
                .Where(c => c.CodigoEmpleadoRepVentasNavigation != null && !c.Pagos.Any())
                .Select(c => new
                {
                    NombreCliente = c.NombreCliente,
                    NombreRepresentante = c.CodigoEmpleadoRepVentasNavigation.Nombre,
                    ApellidoRepresentante = c.CodigoEmpleadoRepVentasNavigation.Apellido1
                })
                .ToListAsync();

            return clientesSinPagosYRepresentantes;
        }

        public async Task<IEnumerable<object>> ClientesConPagosYRepresentantesConCiudadOficina()
        {
            var clientesConPagosYRepresentantes = await _context.Pagos
                .Include(p => p.CodigoClienteNavigation)
                .ThenInclude(c => c.CodigoEmpleadoRepVentasNavigation)
                .ThenInclude(e => e.CodigoOficinaNavigation)
                .Where(p => p.CodigoClienteNavigation != null)
                .Select(p => new
                {
                    NombreCliente = p.CodigoClienteNavigation.NombreCliente,
                    NombreRepresentante = p.CodigoClienteNavigation.CodigoEmpleadoRepVentasNavigation.Nombre,
                    ApellidoRepresentante = p.CodigoClienteNavigation.CodigoEmpleadoRepVentasNavigation.Apellido1,
                    CiudadOficinaRepresentante = p.CodigoClienteNavigation.CodigoEmpleadoRepVentasNavigation.CodigoOficinaNavigation.Ciudad
                })
                .Distinct()
                .ToListAsync();

            return clientesConPagosYRepresentantes;
        }

        public async Task<IEnumerable<object>> ObtenerClientesNoPagosYRepresentantesConCiudadOficina()
        {
            var clientesSinPagosYRepresentantes = await _context.Clientes
                .Include(c => c.CodigoEmpleadoRepVentasNavigation)
                .ThenInclude(e => e.CodigoOficinaNavigation)
                .Where(c => c.CodigoEmpleadoRepVentasNavigation != null && !c.Pagos.Any())
                .Select(c => new
                {
                    NombreCliente = c.NombreCliente,
                    NombreRepresentante = c.CodigoEmpleadoRepVentasNavigation.Nombre,
                    ApellidoRepresentante = c.CodigoEmpleadoRepVentasNavigation.Apellido1,
                    CiudadOficinaRepresentante = c.CodigoEmpleadoRepVentasNavigation.CodigoOficinaNavigation.Ciudad
                })
                .ToListAsync();

            return clientesSinPagosYRepresentantes;
        }

        public async Task<IEnumerable<object>> SoyElJefeDeJefes()
        {
            var listadoEmpleadosConJefes = await _context.Empleados
                .Include(e => e.CodigoJefeNavigation)
                .ThenInclude(jefe => jefe.CodigoJefeNavigation) // Jefe del Jefe
                .Select(e => new
                {
                    NombreEmpleado = e.Nombre,
                    NombreJefe = e.CodigoJefeNavigation != null ? e.CodigoJefeNavigation.Nombre : null,
                    NombreJefeDelJefe = e.CodigoJefeNavigation != null && e.CodigoJefeNavigation.CodigoJefeNavigation != null
                        ? e.CodigoJefeNavigation.CodigoJefeNavigation.Nombre
                        : null
                })
                .ToListAsync();

            return listadoEmpleadosConJefes;
        }

        public async Task<IEnumerable<object>> ClientesConPedidosNoEntregadosATiempo()
        {
            var clientesConPedidosNoEntregados = await _context.Clientes
                .Include(c => c.Pedidos)
                .Where(c => c.Pedidos.Any(p => p.FechaEntrega == null || p.FechaEntrega > p.FechaEsperada))
                .Select(c => new
                {
                    NombreCliente = c.NombreCliente
                })
                .ToListAsync();

            return clientesConPedidosNoEntregados;
        }

        public async Task<IEnumerable<object>> GamasProductosCompradasPorCliente()
        {
            var gamasProductosPorCliente = await _context.Clientes
                .Include(c => c.Pedidos)
                .ThenInclude(p => p.DetallePedidos)
                .ThenInclude(dp => dp.CodigoProductoNavigation)
                .Where(c => c.Pedidos.Any())
                .Select(c => new
                {
                    NombreCliente = c.NombreCliente,
                    GamasProductosCompradas = c.Pedidos
                        .SelectMany(p => p.DetallePedidos)
                        .Select(dp => dp.CodigoProductoNavigation.Gama)
                        .Distinct()
                })
                .ToListAsync();

            return gamasProductosPorCliente;
        }

        public async Task<IEnumerable<object>> ObtenerClientesSinPagos()
        {
            var clientesSinPagos = await _context.Clientes
                .Include(c => c.Pagos)
                .Where(c => c.Pagos.All(p => p == null))
                .Select(c => new
                {
                    NombreCliente = c.NombreCliente
                })
                .ToListAsync();

            return clientesSinPagos;
        }

        public async Task<IEnumerable<Cliente>> ClientesNoPagoNoPedido()
        {
            return await _context.Clientes
                                .Where(c => !c.Pedidos.Any() &&
                                !c.Pagos.Any())
                                .ToListAsync();
        }

        public async Task<IEnumerable<object>> EmpleadoNoCliente()
        {
            var query = from empleado in _context.Empleados
                        join cliente in _context.Clientes on empleado.CodigoEmpleado
                        equals cliente.CodigoEmpleadoRepVentas into clientes
                        from cliente in clientes.DefaultIfEmpty()
                        where cliente == null
                        select new
                        {
                            empleado.CodigoEmpleado,
                            empleado.Nombre,
                            empleado.Apellido1,
                            empleado.Apellido2,
                            empleado.CodigoOficina,
                            empleado.Puesto,
                            Oficina = new
                            {
                                empleado.CodigoOficinaNavigation.CodigoOficina,
                                empleado.CodigoOficinaNavigation.Ciudad,
                                empleado.CodigoOficinaNavigation.Pais,
                                empleado.CodigoOficinaNavigation.Region,
                                empleado.CodigoOficinaNavigation.CodigoPostal,
                                empleado.CodigoOficinaNavigation.Telefono,
                                empleado.CodigoOficinaNavigation.LineaDireccion1,
                                empleado.CodigoOficinaNavigation.LineaDireccion2
                            }
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<object>> EmpleadoNoOficinaNoCliente()
        {
            var query = from empleado in _context.Empleados
                        join oficina in _context.Oficinas on empleado.CodigoOficina equals oficina.CodigoOficina into oficinas
                        from oficina in oficinas.DefaultIfEmpty()
                        join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into clientes
                        from cliente in clientes.DefaultIfEmpty()
                        where oficina == null && cliente == null
                        select new
                        {
                            empleado.CodigoEmpleado,
                            empleado.Nombre,
                            empleado.Apellido1,
                            empleado.Apellido2,
                            empleado.CodigoOficina,
                            empleado.Puesto,
                            TieneOficina = oficina != null,
                            TieneCliente = cliente != null
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<object>> ClientesConPedidosSinPagos()
        {
            var query = from cliente in _context.Clientes
                        join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente
                        join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into pagos
                        where !pagos.Any()
                        select new
                        {
                            cliente.CodigoCliente,
                            cliente.NombreCliente,
                            cliente.NombreContacto,
                            cliente.ApellidoContacto,
                            cliente.Telefono,
                        };

            return await query.Distinct().ToListAsync();
        }

        public async Task<IEnumerable<object>> EmpleadosSinClientesConJefe()
        {
            var query = from empleado in _context.Empleados
                        where empleado.Clientes.Count == 0
                        select new
                        {
                            empleado.CodigoEmpleado,
                            empleado.Nombre,
                            empleado.Apellido1,
                            empleado.Apellido2,
                            empleado.Extension,
                            empleado.Email,
                            JefeNombre = empleado.CodigoJefeNavigation != null
                                ? $"{empleado.CodigoJefeNavigation.Nombre} {empleado.CodigoJefeNavigation.Apellido1}"
                                : null,
                            JefeEmail = empleado.CodigoJefeNavigation != null ? empleado.CodigoJefeNavigation.Email : null
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<object>> ClientesPorPais()
        {
            var query = from cliente in _context.Clientes
                        group cliente by cliente.Pais into grupo
                        select new
                        {
                            Pais = grupo.Key,
                            CantidadClientes = grupo.Count()
                        };

            return await query.ToListAsync();
        }

        public async Task<int> ContarClientesEnMadrid()
        {
            return await _context.Clientes
                .Where(c => c.Ciudad == "Madrid")
                .CountAsync();
        }

        public async Task<IEnumerable<object>> ClientesEnCiudadesQueEmpiezanConM()
        {
            return await _context.Clientes
                .Where(c => c.Ciudad.StartsWith("M"))
                .GroupBy(c => c.Ciudad)
                .Select(group => new
                {
                    Ciudad = group.Key,
                    CantidadClientes = group.Count()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> RepresentantesVentasConClientes()
        {
            var representantes = await _context.Empleados
                                    .Where(e => e.Puesto == "Representante Ventas")
                                    .Select(r => new
                                    {
                                        NombreRepresentante = $"{r.Nombre} {r.Apellido1} {r.Apellido2}",
                                        NumeroClientes = _context.Clientes.Count(c => c.CodigoEmpleadoRepVentas == r.CodigoEmpleado)
                                    })
                                    .ToListAsync();

            return representantes;
        }

        public async Task<int> ClientesSinRepresentanteVentas()
        {
            var clientesSinRepresentante = await _context.Clientes
                .CountAsync(c => c.CodigoEmpleadoRepVentasNavigation == null);

            return clientesSinRepresentante;
        }

        public async Task<IEnumerable<object>> PrimerUltimoPagoClientes()
        {
            var result = await _context.Clientes
                .Select(c => new
                {
                    Nombre = c.NombreCliente,
                    Apellidos = c.ApellidoContacto,
                    PrimerPago = c.Pagos.OrderBy(p => p.FechaPago).Select(p => p.FechaPago).FirstOrDefault(),
                    UltimoPago = c.Pagos.OrderByDescending(p => p.FechaPago).Select(p => p.FechaPago).FirstOrDefault()
                })
                .ToListAsync();

            return result;
        }

        public async Task<string> ClienteConMayorLimiteCredito()
        {
            var cliente = await _context.Clientes
                .OrderByDescending(c => c.LimiteCredito)
                .Select(c => c.NombreCliente)
                .FirstOrDefaultAsync();

            return cliente;
        }

        public async Task<IEnumerable<object>> ClientesConLimiteMayorQuePagos()
        {
            var clientes = await _context.Clientes
                .Where(cliente => cliente.LimiteCredito > cliente.Pagos.Sum(pago => pago.Total))
                .Select(cliente => new
                {
                    CodigoCliente = cliente.CodigoCliente,
                    NombreCliente = cliente.NombreCliente,
                    LimiteCredito = cliente.LimiteCredito,
                    TotalPagosRealizados = cliente.Pagos.Sum(pago => pago.Total)
                })
                .ToListAsync();

            return clientes;
        }

        public async Task<object> ClienteConLimiteMasAlto()
        {
            var cliente = await _context.Clientes
                .Where(c => _context.Clientes.All(other => c.LimiteCredito >= other.LimiteCredito))
                .FirstOrDefaultAsync();

            if (cliente != null)
            {
                return new
                {
                    NombreCliente = cliente.NombreCliente,
                    LimiteCredito = cliente.LimiteCredito
                };
            }

            return null;
        }


        public async Task<IEnumerable<object>> ClientesSinPagos()
        {
            var clientesSinPagos = await _context.Clientes
                .Where(cliente => !_context.Pagos.Any(pago => pago.CodigoCliente == cliente.CodigoCliente))
                .Select(cliente => new
                {
                    NombreCliente = cliente.NombreCliente,
                })
                .ToListAsync();

            return clientesSinPagos;
        }


        public async Task<IEnumerable<object>> ClientesConPagosRealizados()
        {
            var clientesConPagos = await _context.Clientes
                .Where(cliente => _context.Pagos
                    .Where(pago => pago.CodigoCliente == cliente.CodigoCliente)
                    .Any())
                .Select(cliente => new
                {
                    CodigoCliente = cliente.CodigoCliente,
                    NombreCliente = cliente.NombreCliente,
                    // Otros campos que desees incluir
                })
                .ToListAsync();

            return clientesConPagos;
        }

        public async Task<IEnumerable<object>> ClientesSinPagos2()
        {
            var clientesSinPagos = await _context.Clientes
                .Where(c => !c.Pagos.Any())
                .Select(c => new
                {
                    CodigoCliente = c.CodigoCliente,
                    NombreCliente = c.NombreCliente,
                    NombreContacto = c.NombreContacto,
                    ApellidoContacto = c.ApellidoContacto,
                    Telefono = c.Telefono,
                    Fax = c.Fax,
                    LineaDireccion1 = c.LineaDireccion1,
                    LineaDireccion2 = c.LineaDireccion2,
                    Ciudad = c.Ciudad,
                    Region = c.Region,
                    Pais = c.Pais,
                    CodigoPostal = c.CodigoPostal,
                    CodigoEmpleadoRepVentas = c.CodigoEmpleadoRepVentas,
                    LimiteCredito = c.LimiteCredito
                })
                .ToListAsync();

            return clientesSinPagos;
        }


        public async Task<IEnumerable<object>> ClientesConPagos2()
        {
            var clientesConPagos = await _context.Clientes
                .Where(c => c.Pagos.Any())
                .Select(c => new
                {
                    CodigoCliente = c.CodigoCliente,
                    NombreCliente = c.NombreCliente,
                    NombreContacto = c.NombreContacto,
                    ApellidoContacto = c.ApellidoContacto,
                    Telefono = c.Telefono,
                    Fax = c.Fax,
                    LineaDireccion1 = c.LineaDireccion1,
                    LineaDireccion2 = c.LineaDireccion2,
                    Ciudad = c.Ciudad,
                    Region = c.Region,
                    Pais = c.Pais,
                    CodigoPostal = c.CodigoPostal,
                    CodigoEmpleadoRepVentas = c.CodigoEmpleadoRepVentas,
                    LimiteCredito = c.LimiteCredito
                })
                .ToListAsync();

            return clientesConPagos;
        }

        public async Task<IEnumerable<object>> ClientesConPedidos2()
        {
            var clientesConPedidos = await _context.Clientes
                .GroupJoin(
                    _context.Pedidos,
                    cliente => cliente.CodigoCliente,
                    pedido => pedido.CodigoCliente,
                    (cliente, pedidos) => new
                    {
                        NombreCliente = cliente.NombreCliente,
                        PedidosRealizados = pedidos.Count()
                    }
                )
                .ToListAsync();

            return clientesConPedidos;
        }

        public async Task<IEnumerable<string>> ClientesConPedidosEn2008v2()
        {
            var clientes = await _context.Clientes
                .Join(
                    _context.Pedidos,
                    cliente => cliente.CodigoCliente,
                    pedido => pedido.CodigoCliente,
                    (cliente, pedido) => new { cliente.NombreCliente, pedido.FechaPedido.Year }
                )
                .Where(joinResult => joinResult.Year == 2008)
                .Select(result => result.NombreCliente)
                .OrderBy(nombre => nombre)
                .Distinct()
                .ToListAsync();

            return clientes;
        }

        public async Task<IEnumerable<object>> ClientesSinPagos3()
        {
            var clientesSinPagos = await _context.Clientes
                .Where(cliente => !_context.Pagos.Any(pago => pago.CodigoCliente == cliente.CodigoCliente))
                .Select(cliente => new
                {
                    NombreCliente = cliente.NombreCliente,
                    NombreRepresentante = cliente.CodigoEmpleadoRepVentasNavigation.Nombre,
                    ApellidoRepresentante = cliente.CodigoEmpleadoRepVentasNavigation.Apellido1,
                    TelefonoOficinaRepresentante = cliente.CodigoEmpleadoRepVentasNavigation.CodigoOficinaNavigation.Telefono
                })
                .ToListAsync();

            return clientesSinPagos;
        }

        public async Task<IEnumerable<object>> ObtenerClientesConRepresentanteYOficina()
        {
            var clientes = await _context.Clientes
                .Where(cliente => cliente.CodigoEmpleadoRepVentasNavigation != null)
                .Select(cliente => new
                {
                    ClienteNombre = cliente.NombreCliente,
                    RepresentanteNombre = $"{cliente.CodigoEmpleadoRepVentasNavigation.Nombre} {cliente.CodigoEmpleadoRepVentasNavigation.Apellido1}",
                    OficinaCiudad = cliente.CodigoEmpleadoRepVentasNavigation.CodigoOficinaNavigation.Ciudad
                })
                .ToListAsync();

            return clientes;
        }

        

















    }
}