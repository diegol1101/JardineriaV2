using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.UnitOfWork;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly JardineriaContext _context;

        private ClienteRepository _clientes;
        private DetallePedidoRepository _detallePedidos;
        private EmpleadoRepository _empleados;
        private GamaProductoRepository _gamaProductos;
        private OficinaRepository _oficinas;
        private PagoRepository _pagos;
        private PedidoRepository _pedidos;
        private ProductoRepository _productos;
        
        public UnitOfWork( JardineriaContext context)
        {
            _context = context;
        }
        
        
        public ICliente Clientes
            {
                get
                {
                    if (_clientes == null)
                    {
                        _clientes = new ClienteRepository(_context);
                    }
                    return _clientes;
                }
            }
        
        
        public IDetallePedido DetallePedidos
            {
                get
                {
                    if (_detallePedidos == null)
                    {
                        _detallePedidos = new DetallePedidoRepository(_context);
                    }
                    return _detallePedidos;
                }
            }
        
        
        public IEmpleado Empleados
            {
                get
                {
                    if (_empleados == null)
                    {
                        _empleados = new EmpleadoRepository(_context);
                    }
                    return _empleados;
                }
            }
        
        
        public IGamaProducto GamaProductos
            {
                get
                {
                    if (_gamaProductos == null)
                    {
                        _gamaProductos = new GamaProductoRepository(_context);
                    }
                    return _gamaProductos;
                }
            }
        
        
        public IOficina Oficinas
            {
                get
                {
                    if (_oficinas == null)
                    {
                        _oficinas = new OficinaRepository(_context);
                    }
                    return _oficinas;
                }
            }
            
        
        public IPago Pagos
            {
                get
                {
                    if (_pagos == null)
                    {
                        _pagos = new PagoRepository(_context);
                    }
                    return _pagos;
                }
            }
        
        
        public IPedido Pedidos
            {
                get
                {
                    if (_pedidos == null)
                    {
                        _pedidos = new PedidoRepository(_context);
                    }
                    return _pedidos;
                }
            }
        
        
        public IProducto Productos
            {
                get
                {
                    if (_productos == null)
                    {
                        _productos = new ProductoRepository(_context);
                    }
                    return _productos;
                }
            }
            
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
    
    
