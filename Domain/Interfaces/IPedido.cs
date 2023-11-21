using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPedido :IGenericRepository<Pedido>
    {
        Task<IEnumerable<object>> EstadosDePedidos();
        Task<IEnumerable<Pedido>> PedidosNoEntregadosATiempo();
        Task<IEnumerable<object>> PedidosDosDiasAntes();
        Task<IEnumerable<Pedido>> PedidosRechazadosEn2009();
        Task<IEnumerable<Pedido>> PedidosEntregadosEnEnero();
        Task<IEnumerable<object>> ProductosSinPedidos();
        Task<IEnumerable<object>> PedidosPorEstado();
        Task<IEnumerable<object>> ProductosEnPedidos();
        Task<IEnumerable<object>> SumaCantidadTotalEnPedidos();
        Task<IEnumerable<object>> ProductosMasVendidos();
        Task<IEnumerable<object>> ProductosMasVendidosPorCodigo();
        Task<IEnumerable<object>> ProductosMasVendidosPorOR();
        Task<IEnumerable<object>> VentasMayoresA3000Iva();
    }
}