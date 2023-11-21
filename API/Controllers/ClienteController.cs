using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ClienteController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public ClienteController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        var cliente = await unitofwork.Clientes.GetAllAsync();
        return mapper.Map<List<ClienteDto>>(cliente);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ClienteDto>> Get(int id)
    {
        var cliente = await unitofwork.Clientes.GetByIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return this.mapper.Map<ClienteDto>(cliente);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Cliente>> Post(ClienteDto clienteDto)
    {
        var cliente = this.mapper.Map<Cliente>(clienteDto);
        this.unitofwork.Clientes.Add(cliente);
        await unitofwork.SaveAsync();
        if (cliente == null)
        {
            return BadRequest();
        }
        clienteDto.CodigoCliente = cliente.CodigoCliente;
        return CreatedAtAction(nameof(Post), new { id = clienteDto.CodigoCliente }, clienteDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto clienteDto)
    {
        if (clienteDto == null)
        {
            return NotFound();
        }
        var cliente = this.mapper.Map<Cliente>(clienteDto);
        unitofwork.Clientes.Update(cliente);
        await unitofwork.SaveAsync();
        return clienteDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await unitofwork.Clientes.GetByIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        unitofwork.Clientes.Remove(cliente);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("clientesespaña")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ClienteDto>>> ClientesEspanol()
    {
        var cliente = await unitofwork.Clientes.ClientesEspanol();
        return mapper.Map<List<ClienteDto>>(cliente);
    }

    [HttpGet("ClientesConPagosEn2008")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesConPagosEn2008()
    {
        var cliente = await unitofwork.Clientes.ClientesConPagosEn2008();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesEnMadrid")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ClienteDto>>> ClientesEnMadrid()
    {
        var cliente = await unitofwork.Clientes.ClientesEnMadrid();
        return mapper.Map<List<ClienteDto>>(cliente);
    }

    [HttpGet("ClientesConRepresentantesVentas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesConRepresentantesVentas()
    {
        var cliente = await unitofwork.Clientes.ClientesConRepresentantesVentas();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesConPagosYRepresentantesVentas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesConPagosYRepresentantesVentas()
    {
        var cliente = await unitofwork.Clientes.ClientesConPagosYRepresentantesVentas();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesNoPagosConRepresentantesVentas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesNoPagosConRepresentantesVentas()
    {
        var cliente = await unitofwork.Clientes.ClientesNoPagosConRepresentantesVentas();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesConPagosYRepresentantesConCiudadOficina")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesConPagosYRepresentantesConCiudadOficina()
    {
        var cliente = await unitofwork.Clientes.ClientesConPagosYRepresentantesConCiudadOficina();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ObtenerClientesNoPagosYRepresentantesConCiudadOficina")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ObtenerClientesNoPagosYRepresentantesConCiudadOficina()
    {
        var cliente = await unitofwork.Clientes.ObtenerClientesNoPagosYRepresentantesConCiudadOficina();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("SoyElJefeDeJefes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> SoyElJefeDeJefes()
    {
        var cliente = await unitofwork.Clientes.SoyElJefeDeJefes();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesConPedidosNoEntregadosATiempo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesConPedidosNoEntregadosATiempo()
    {
        var cliente = await unitofwork.Clientes.ClientesConPedidosNoEntregadosATiempo();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("GamasProductosCompradasPorCliente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> GamasProductosCompradasPorCliente()
    {
        var cliente = await unitofwork.Clientes.GamasProductosCompradasPorCliente();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ObtenerClientesSinPagos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ObtenerClientesSinPagos()
    {
        var cliente = await unitofwork.Clientes.ObtenerClientesSinPagos();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesNoPagoNoPedido")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Cliente>>> ClientesNoPagoNoPedido()
    {
        var cliente = await unitofwork.Clientes.ClientesNoPagoNoPedido();
        return mapper.Map<List<Cliente>>(cliente);
    }

    [HttpGet("EmpleadoNoCliente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> EmpleadoNoCliente()
    {
        var cliente = await unitofwork.Clientes.EmpleadoNoCliente();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("EmpleadoNoOficinaNoCliente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> EmpleadoNoOficinaNoCliente()
    {
        var cliente = await unitofwork.Clientes.EmpleadoNoOficinaNoCliente();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesConPedidosSinPagos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesConPedidosSinPagos()
    {
        var cliente = await unitofwork.Clientes.ClientesConPedidosSinPagos();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("EmpleadosSinClientesConJefe")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> EmpleadosSinClientesConJefe()
    {
        var cliente = await unitofwork.Clientes.EmpleadosSinClientesConJefe();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesPorPais")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesPorPais()
    {
        var cliente = await unitofwork.Clientes.ClientesPorPais();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ContarClientesEnMadrid")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> ContarClientesEnMadrid()
    {
        var cliente = await unitofwork.Clientes.ContarClientesEnMadrid();
        return Ok(cliente);
    }

    [HttpGet("ClientesEnCiudadesQueEmpiezanConM")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesEnCiudadesQueEmpiezanConM()
    {
        var cliente = await unitofwork.Clientes.ClientesEnCiudadesQueEmpiezanConM();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("RepresentantesVentasConClientes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> RepresentantesVentasConClientes()
    {
        var cliente = await unitofwork.Clientes.RepresentantesVentasConClientes();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesSinRepresentanteVentas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> ClientesSinRepresentanteVentas()
    {
        var cliente = await unitofwork.Clientes.ClientesSinRepresentanteVentas();
        return Ok(cliente);
    }

    [HttpGet("PrimerUltimoPagoClientes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> PrimerUltimoPagoClientes()
    {
        var cliente = await unitofwork.Clientes.PrimerUltimoPagoClientes();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClienteConMayorLimiteCredito")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<string>> ClienteConMayorLimiteCredito()
    {
        var cliente = await unitofwork.Clientes.ClienteConMayorLimiteCredito();
        return Ok(cliente);
    }

    [HttpGet("ClientesConLimiteMayorQuePagos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesConLimiteMayorQuePagos()
    {
        var cliente = await unitofwork.Clientes.ClientesConLimiteMayorQuePagos();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClienteConLimiteMasAlto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<object>> ClienteConLimiteMasAlto()
    {
        var cliente = await unitofwork.Clientes.ClienteConLimiteMasAlto();
        return Ok(cliente);
    }

    [HttpGet("ClientesSinPagos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesSinPagos()
    {
        var cliente = await unitofwork.Clientes.ClientesSinPagos();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesConPagosRealizados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ClientesConPagosRealizados()
    {
        var cliente = await unitofwork.Clientes.ClientesConPagosRealizados();
        return mapper.Map<List<object>>(cliente);
    }

    [HttpGet("ClientesSinPagos2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> ClientesSinPagos2()
    {
        var clientes = await unitofwork.Clientes.ClientesSinPagos2();
        return mapper.Map<List<object>>(clientes);
    }

    [HttpGet("ClientesConPagos2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> ClientesConPagos2()
    {
        var clientes = await unitofwork.Clientes.ClientesConPagos2();
        return mapper.Map<List<object>>(clientes);
    }

    [HttpGet("ClientesConPedidos2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> ClientesConPedidos2()
    {
        var clientes = await unitofwork.Clientes.ClientesConPedidos2();
        return mapper.Map<List<object>>(clientes);
    }

    [HttpGet("ClientesConPedidosEn2008v2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<string>>> ClientesConPedidosEn2008v2()
    {
        var clientes = await unitofwork.Clientes.ClientesConPedidosEn2008v2();
        return Ok(clientes);
    }

    [HttpGet("ClientesSinPagos3")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> ClientesSinPagos3()
    {
        var clientes = await unitofwork.Clientes.ClientesSinPagos3();
        return mapper.Map<List<object>>(clientes);
    }


    [HttpGet("ClientesConRepresentanteYOficina")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> ClientesConRepresentanteYOficina()
    {
        var clientes = await unitofwork.Clientes.ObtenerClientesConRepresentanteYOficina();
        return mapper.Map<List<object>>(clientes);
    }








}
