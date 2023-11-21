using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces;

    public interface IUnitOfWork
    {   
        ICliente Clientes {get;}
        IDetallePedido DetallePedidos {get;}
        IEmpleado Empleados {get;}
        IGamaProducto GamaProductos {get;}
        IOficina Oficinas {get;}
        IPago Pagos {get;}
        IPedido Pedidos {get;}
        IProducto Productos {get;}
        Task<int> SaveAsync();
    }
