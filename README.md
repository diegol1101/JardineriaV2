# JardineriaV2

# Esta es la base de datos utilizada:

![image](https://github.com/diegol1101/JardineriaV2/assets/116105368/63535547-d051-4cc2-89f8-3cf39d4b628a)

# Consultas API RESTful

Este repositorio contiene una API RESTful que ofrece diversas consultas a una base de datos. A continuación, se presentan algunas de las consultas disponibles y ejemplos de cómo usarlas.

## Consultas Requeridas

### 1. Clientes Españoles
- Devuelve un listado con el nombre de todos los clientes españoles.
- [Consulta](http://localhost:5117/api/Cliente/clientesespaña)

### 2. Estados de Pedidos
- Devuelve un listado con los distintos estados por los que puede pasar un pedido.
- [Consulta](http://localhost:5117/api/Pedido/estadospedidos)

### 3. Clientes con Pagos en 2008
- Devuelve un listado con el código de cliente de aquellos clientes que realizaron algún pago en 2008. Se resuelve la consulta de tres maneras:
  - Utilizando la función YEAR de MySQL.
  - Utilizando la función DATE_FORMAT de MySQL.
  - Sin utilizar ninguna de las funciones anteriores.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConPagosEn2008)

### 9. Pedidos No Entregados a Tiempo
- Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos que no han sido entregados a tiempo.
- [Consulta](http://localhost:5117/api/Pedido/PedidosNoEntregadosATiempo)

### 10. Pedidos Dos Días Antes
- Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al menos dos días antes de la fecha esperada.
  - Utilizando la función ADDDATE de MySQL.
  - Utilizando la función DATEDIFF de MySQL.
  - ¿Sería posible resolver esta consulta utilizando el operador de suma + o resta -?
- [Consulta](http://localhost:5117/api/Pedido/PedidosDosDiasAntes)

### 11. Pedidos Rechazados en 2009
- Devuelve un listado de todos los pedidos que fueron rechazados en 2009.
- [Consulta](http://localhost:5117/api/Pedido/PedidosRechazadosEn2009)

### 12. Pedidos Entregados en Enero
- Devuelve un listado de todos los pedidos que han sido entregados en el mes de enero de cualquier año.
- [Consulta](http://localhost:5117/api/Pedido/PedidosEntregadosEnEnero)

### 13. Pagos en 2008 con Paypal
- Devuelve un listado con todos los pagos que se realizaron en el año 2008 mediante Paypal. El resultado está ordenado de mayor a menor.
- [Consulta](http://localhost:5117/api/Pago/PagosEn2008ConPaypal)

### 14. Métodos de Pago
- Devuelve un listado con todas las formas de pago que aparecen en la tabla de pagos. No deben aparecer formas de pago repetidas.
- [Consulta](http://localhost:5117/api/Pago/MetodosDePago)

### 15. Productos en Gama Ornamentales
- Devuelve un listado con todos los productos que pertenecen a la gama Ornamentales y tienen más de 100 unidades en stock. El listado está ordenado por su precio de venta, mostrando primero los de mayor precio.
- [Consulta](http://localhost:5117/api/Producto/GamaOrnamentales)

### 16. Clientes en Madrid
- Devuelve un listado con todos los clientes que son de la ciudad de Madrid y cuyo representante de ventas tiene el código de empleado 11 o 30.
- [Consulta](http://localhost:5117/api/Cliente/ClientesEnMadrid)
  

# Consultas Multi Tabla (Composición Interna)

En esta sección, se presentan consultas que involucran múltiples tablas y se resuelven utilizando la sintaxis de SQL1 y SQL2. Las consultas con sintaxis de SQL2 se abordan con INNER JOIN y NATURAL JOIN.

## 1. Clientes con Representantes de Ventas

- Obtén un listado con el nombre de cada cliente y el nombre y apellido de su representante de ventas.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConRepresentantesVentas)

## 2. Clientes con Pagos y Representantes de Ventas

- Muestra el nombre de los clientes que hayan realizado pagos junto con el nombre de sus representantes de ventas.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConPagosYRepresentantesVentas)

## 3. Clientes sin Pagos y con Representantes de Ventas

- Muestra el nombre de los clientes que no hayan realizado pagos junto con el nombre de sus representantes de ventas.
- [Consulta](http://localhost:5117/api/Cliente/ClientesNoPagosConRepresentantesVentas)

## 4. Clientes con Pagos, Representantes y Ciudad de Oficina

- Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConPagosYRepresentantesConCiudadOficina)

## 5. Clientes sin Pagos, Representantes y Ciudad de Oficina

- Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.
- [Consulta](http://localhost:5117/api/Cliente/ObtenerClientesNoPagosYRepresentantesConCiudadOficina)

## 6. Empleados, Jefes y Jefes de Jefes

- Devuelve un listado que muestre el nombre de cada empleado, el nombre de su jefe y el nombre del jefe de sus jefes.
- [Consulta](http://localhost:5117/api/Cliente/SoyElJefeDeJefes)

## 7. Clientes con Pedidos No Entregados a Tiempo

- Devuelve el nombre de los clientes a los que no se les ha entregado a tiempo un pedido.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConPedidosNoEntregadosATiempo)

## 8. Gamas de Producto Compradas por Cliente

- Devuelve un listado de las diferentes gamas de producto que ha comprado cada cliente.
- [Consulta](http://localhost:5117/api/Cliente/GamasProductosCompradasPorCliente)


# Consultas Multi Tabla (Composición Externa)

En esta sección, se presentan consultas que involucran múltiples tablas y se resuelven utilizando las cláusulas LEFT JOIN, RIGHT JOIN, NATURAL LEFT JOIN y NATURAL RIGHT JOIN.

## 1. Clientes sin Pagos

- Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago.
- [Consulta](http://localhost:5117/api/Cliente/ObtenerClientesSinPagos)

## 2. Clientes sin Pago y sin Pedido

- Devuelve un listado que muestre los clientes que no han realizado ningún pago y los que no han realizado ningún pedido.
- [Consulta](http://localhost:5117/api/Cliente/ClientesNoPagoNoPedido)

## 3. Empleados sin Cliente

- Devuelve un listado que muestre solamente los empleados que no tienen un cliente asociado junto con los datos de la oficina donde trabajan.
- [Consulta](http://localhost:5117/api/Cliente/EmpleadoNoCliente)

## 4. Empleados sin Oficina y sin Cliente

- Devuelve un listado que muestre los empleados que no tienen una oficina asociada y los que no tienen un cliente asociado.
- [Consulta](http://localhost:5117/api/Cliente/EmpleadoNoOficinaNoCliente)

## 5. Productos sin Pedidos

- Devuelve un listado de los productos que nunca han aparecido en un pedido.
- [Consulta](http://localhost:5117/api/Pedido/ProductosSinPedidos)

## 6. Productos sin Pedidos Detallados

- Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado muestra el nombre, la descripción y la imagen del producto.
- [Consulta](http://localhost:5117/api/Pedido/ProductosSinPedidos)

## 7. Oficinas sin Empleados de Representantes de Ventas de Frutales

- Devuelve las oficinas donde no trabajan ninguno de los empleados que han sido representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.
- [Consulta](http://localhost:5117/api/Oficina/OficinasNoEmFrutales)

## 8. Clientes con Pedidos sin Pagos

- Devuelve un listado con los clientes que han realizado algún pedido, pero no han realizado ningún pago.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConPedidosSinPagos)

## 9. Empleados sin Clientes con Jefe Asociado

- Devuelve un listado con los datos de los empleados que no tienen clientes asociados y el nombre de su jefe asociado.
- [Consulta](http://localhost:5117/api/Cliente/EmpleadosSinClientesConJefe)


# Consultas Resumen

En esta sección se presentan consultas que proporcionan información resumida sobre la base de datos.

## 1. Número de Empleados en la Compañía

- ¿Cuántos empleados hay en la compañía?
- [Consulta](http://localhost:5117/api/Empleado/CuantosEmpleados)

## 2. Número de Clientes por País

- ¿Cuántos clientes tiene cada país?
- [Consulta](http://localhost:5117/api/Cliente/ClientesPorPais)

## 3. Pago Promedio en 2009

- ¿Cuál fue el pago medio en 2009?
- [Consulta](http://localhost:5117/api/Pago/PagoMedioEn2009)

## 4. Número de Pedidos por Estado

- ¿Cuántos pedidos hay en cada estado? Ordena el resultado de forma descendente por el número de pedidos.
- [Consulta](http://localhost:5117/api/Pedido/PedidosPorEstado)

## 5. Número de Clientes en Madrid

- ¿Cuántos clientes existen con domicilio en la ciudad de Madrid?
- [Consulta](http://localhost:5117/api/Cliente/ContarClientesEnMadrid)

## 6. Número de Clientes en Ciudades que Empiezan con M

- ¿Calcula cuántos clientes tiene cada una de las ciudades que empiezan por M?
- [Consulta](http://localhost:5117/api/Cliente/ClientesEnCiudadesQueEmpiezanConM)

## 7. Representantes de Ventas con Clientes Atendidos

- Devuelve el nombre de los representantes de ventas y el número de clientes al que atiende cada uno.
- [Consulta](http://localhost:5117/api/Cliente/RepresentantesVentasConClientes)

## 8. Número de Clientes sin Representante de Ventas

- Calcula el número de clientes que no tiene asignado representante de ventas.
- [Consulta](http://localhost:5117/api/Cliente/ClientesSinRepresentanteVentas)

## 9. Fechas del Primer y Último Pago por Cliente

- Calcula la fecha del primer y último pago realizado por cada uno de los clientes. El listado deberá mostrar el nombre y los apellidos de cada cliente.
- [Consulta](http://localhost:5117/api/Cliente/PrimerUltimoPagoClientes)

## 10. Número de Productos Diferentes en cada Pedido

- Calcula el número de productos diferentes que hay en cada uno de los pedidos.
- [Consulta](http://localhost:5117/api/Pedido/ProductosEnPedidos)

## 11. Suma de la Cantidad Total de Productos en Pedidos

- Calcula la suma de la cantidad total de todos los productos que aparecen en cada uno de los pedidos.
- [Consulta](http://localhost:5117/api/Pedido/SumaCantidadTotalEnPedidos)

## 12. Top 20 de Productos Más Vendidos

- Devuelve un listado de los 20 productos más vendidos y el número total de unidades que se han vendido de cada uno. El listado deberá estar ordenado por el número total de unidades vendidas.
- [Consulta](http://localhost:5117/api/Pedido/ProductosMasVendidos)

## 13. Productos Más Vendidos Agrupados por Código

- La misma información que en la pregunta anterior, pero agrupada por código de producto.
- [Consulta](http://localhost:5117/api/Pedido/ProductosMasVendidosPorCodigo)

## 14. Productos Más Vendidos Filtrados por Código que Empieza con "OR"

- La misma información que en la pregunta anterior, pero agrupada por código de producto filtrada por los códigos que empiecen por "OR".
- [Consulta](http://localhost:5117/api/Pedido/ProductosMasVendidosPorOR)

## 15. Ventas Totales de Productos con Facturación Mayor a 3000 Euros

- Lista las ventas totales de los productos que hayan facturado más de 3000 euros. Se mostrará el nombre, unidades vendidas, total facturado y total facturado con impuestos (21% IVA).
- [Consulta](http://localhost:5117/api/Pedido/VentasMayoresA3000Iva)

## 16. Suma Total de Pagos por Año

- Muestra la suma total de todos los pagos que se realizaron para cada uno de los años que aparecen en la tabla pagos.
- [Consulta](http://localhost:5117/api/Pago/SumaTotalPagosPorAño)


# Subconsultas con Operadores Básicos de Comparación

En esta sección se presentan subconsultas que utilizan operadores básicos de comparación para obtener información específica de la base de datos.

## 1. Cliente con Mayor Límite de Crédito

- Devuelve el nombre del cliente con el mayor límite de crédito.
- [Consulta](http://localhost:5117/api/Cliente/ClienteConMayorLimiteCredito)

## 2. Producto con Precio de Venta Más Alto

- Devuelve el nombre del producto que tiene el precio de venta más alto.
- [Consulta](http://localhost:5117/api/Producto/ProductoConPrecioMasAlto)

## 3. Producto Más Vendido

- Devuelve el nombre del producto del que se han vendido más unidades. (Tenga en cuenta que se calculará el número total de unidades vendidas de cada producto a partir de los datos de la tabla detalle_pedido)
- [Consulta](http://localhost:5117/api/Producto/ProductoMasVendido)

## 4. Clientes con Límite de Crédito Mayor que Pagos Realizados

- Devuelve los clientes cuyo límite de crédito es mayor que los pagos que han realizado. (Sin utilizar INNER JOIN).
- [Consulta](http://localhost:5117/api/Cliente/ClientesConLimiteMayorQuePagos)


# Subconsultas con ALL y ANY

En esta sección, se presentan subconsultas que utilizan las cláusulas ALL y ANY para realizar comparaciones complejas en la base de datos.

## 8. Cliente con Límite de Crédito Más Alto

- Devuelve el nombre del cliente con el límite de crédito más alto.
- [Consulta](http://localhost:5117/api/Cliente/ClienteConLimiteMasAlto)

## 9. Producto con Precio de Venta Más Caro

- Devuelve el nombre del producto que tiene el precio de venta más caro.
- [Consulta](http://localhost:5117/api/Producto/ProductoPrecioMasCaro)

# Subconsultas con IN y NOT IN

En esta sección se presentan subconsultas que utilizan las cláusulas IN y NOT IN para filtrar resultados en la base de datos.

## 11. Clientes sin Pagos

- Devuelve un listado que muestra solamente los clientes que no han realizado ningún pago.
- [Consulta](http://localhost:5117/api/Cliente/ClientesSinPagos)

## 12. Clientes con Pagos Realizados

- Devuelve un listado que muestra solamente los clientes que han realizado algún pago.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConPagosRealizados)

## 13. Productos sin Pedidos

- Devuelve un listado de los productos que nunca han aparecido en un pedido.
- [Consulta](http://localhost:5117/api/Producto/ProductosSinPedidos)

## 14. Empleados sin Ventas

- Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos empleados que no son representantes de ventas de ningún cliente.
- [Consulta](http://localhost:5117/api/Empleado/EmpleadosSinVentas)

# Subconsultas con EXISTS y NOT EXISTS

En esta sección se presentan subconsultas que utilizan las cláusulas EXISTS y NOT EXISTS para verificar la existencia de registros en la base de datos.

## 18. Clientes sin Pagos 

- Devuelve un listado que muestra solamente los clientes que no han realizado ningún pago.
- [Consulta](http://localhost:5117/api/Cliente/ClientesSinPagos2)

## 19. Clientes con Pagos Realizados 

- Devuelve un listado que muestra solamente los clientes que han realizado algún pago.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConPagos2)


# Consultas Variadas

En esta sección se presentan consultas variadas que abordan diferentes aspectos de la base de datos.

## 1. Clientes con Pedidos

- Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Ten en cuenta que pueden existir clientes que no han realizado ningún pedido.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConPedidos2)

## 2. Clientes con Pedidos en 2008

- Devuelve el nombre de los clientes que hayan hecho pedidos en 2008, ordenados alfabéticamente de menor a mayor.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConPedidosEn2008v2)

## 3. Clientes sin Pagos (Versión 3)

- Devuelve el nombre del cliente, el nombre y primer apellido de su representante de ventas, y el número de teléfono de la oficina del representante de ventas, de aquellos clientes que no han realizado ningún pago.
- [Consulta](http://localhost:5117/api/Cliente/ClientesSinPagos3)

## 4. Clientes con Representante y Oficina

- Devuelve el listado de clientes donde aparece el nombre del cliente, el nombre y primer apellido de su representante de ventas, y la ciudad donde está su oficina.
- [Consulta](http://localhost:5117/api/Cliente/ClientesConRepresentanteYOficina)

## 5. Empleados sin Ventas (Versión 2)

- Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos empleados que no son representantes de ventas de ningún cliente.
- [Consulta](http://localhost:5117/api/Empleado/EmpleadosSinVentas4)

*Cabe resaltar que todas las entidades cuentan con sus respectivos métodos de: Editar, Agregar, Eliminar, Buscar, Buscar por Id.*








  

  
