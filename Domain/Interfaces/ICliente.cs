using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICliente :IGenericRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> ClientesEspanol();
        Task<IEnumerable<object>> ClientesConPagosEn2008();
        Task<IEnumerable<Cliente>> ClientesEnMadrid();
        Task<IEnumerable<object>> ClientesConRepresentantesVentas();
        Task<IEnumerable<object>> ClientesConPagosYRepresentantesVentas();
        Task<IEnumerable<object>> ClientesNoPagosConRepresentantesVentas();
        Task<IEnumerable<object>> ClientesConPagosYRepresentantesConCiudadOficina();
        Task<IEnumerable<object>> ObtenerClientesNoPagosYRepresentantesConCiudadOficina();
        Task<IEnumerable<object>> SoyElJefeDeJefes();
        Task<IEnumerable<object>> ClientesConPedidosNoEntregadosATiempo();
        Task<IEnumerable<object>> GamasProductosCompradasPorCliente();
        Task<IEnumerable<object>> ObtenerClientesSinPagos();
        Task<IEnumerable<Cliente>> ClientesNoPagoNoPedido();
        Task<IEnumerable<object>> EmpleadoNoCliente();
        Task<IEnumerable<object>> EmpleadoNoOficinaNoCliente();
        Task<IEnumerable<object>> ClientesConPedidosSinPagos();
        Task<IEnumerable<object>> EmpleadosSinClientesConJefe();
        Task<IEnumerable<object>> ClientesPorPais();
        Task<int> ContarClientesEnMadrid();
        Task<IEnumerable<object>> ClientesEnCiudadesQueEmpiezanConM();
        Task<IEnumerable<object>> RepresentantesVentasConClientes();
        Task<int> ClientesSinRepresentanteVentas();
        Task<IEnumerable<object>> PrimerUltimoPagoClientes();
        Task<string> ClienteConMayorLimiteCredito();
        Task<IEnumerable<object>> ClientesConLimiteMayorQuePagos();
        Task<object> ClienteConLimiteMasAlto();
        Task<IEnumerable<object>> ClientesSinPagos();
        Task<IEnumerable<object>> ClientesConPagosRealizados();
        Task<IEnumerable<object>> ClientesSinPagos2();
        Task<IEnumerable<object>> ClientesConPagos2();
        Task<IEnumerable<object>> ClientesConPedidos2();
        Task<IEnumerable<string>> ClientesConPedidosEn2008v2();
        Task<IEnumerable<object>> ClientesSinPagos3();
        Task<IEnumerable<object>> ObtenerClientesConRepresentanteYOficina();

    }
}