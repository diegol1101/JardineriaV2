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

public class PedidoController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public PedidoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
    {
        var pedido = await unitofwork.Pedidos.GetAllAsync();
        return mapper.Map<List<PedidoDto>>(pedido);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PedidoDto>> Get(int id)
    {
        var pedido = await unitofwork.Pedidos.GetByIdAsync(id);
        if (pedido == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PedidoDto>(pedido);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pedido>> Post(PedidoDto pedidoDto)
    {
        var pedido = this.mapper.Map<Pedido>(pedidoDto);
        this.unitofwork.Pedidos.Add(pedido);
        await unitofwork.SaveAsync();
        if (pedido == null)
        {
            return BadRequest();
        }
        pedidoDto.CodigoPedido = pedido.CodigoPedido;
        return CreatedAtAction(nameof(Post), new { id = pedidoDto.CodigoPedido }, pedidoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PedidoDto>> Put(int id, [FromBody] PedidoDto pedidoDto)
    {
        if (pedidoDto == null)
        {
            return NotFound();
        }
        var pedido = this.mapper.Map<Pedido>(pedidoDto);
        unitofwork.Pedidos.Update(pedido);
        await unitofwork.SaveAsync();
        return pedidoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var pedido = await unitofwork.Pedidos.GetByIdAsync(id);
        if (pedido == null)
        {
            return NotFound();
        }
        unitofwork.Pedidos.Remove(pedido);
        await unitofwork.SaveAsync();
        return NoContent();
    }


    [HttpGet("estadospedidos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> EstadosDePedidos()
    {
        var pedido = await unitofwork.Pedidos.EstadosDePedidos();
        return mapper.Map<List<object>>(pedido);
    }

    [HttpGet("PedidosNoEntregadosATiempo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Pedido>>> PedidosNoEntregadosATiempo()
    {
        var pedido = await unitofwork.Pedidos.PedidosNoEntregadosATiempo();
        return mapper.Map<List<Pedido>>(pedido);
    }

    [HttpGet("PedidosDosDiasAntes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> PedidosDosDiasAntes()
    {
        var pedido = await unitofwork.Pedidos.PedidosDosDiasAntes();
        return mapper.Map<List<object>>(pedido);
    }

    [HttpGet("PedidosRechazadosEn2009")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Pedido>>> PedidosRechazadosEn2009()
    {
        var pedido = await unitofwork.Pedidos.PedidosRechazadosEn2009();
        return mapper.Map<List<Pedido>>(pedido);
    }

    [HttpGet("PedidosEntregadosEnEnero")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Pedido>>> PedidosEntregadosEnEnero()
    {
        var pedido = await unitofwork.Pedidos.PedidosEntregadosEnEnero();
        return mapper.Map<List<Pedido>>(pedido);
    }

    [HttpGet("ProductosSinPedidos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ProductosSinPedidos()
    {
        var pedido = await unitofwork.Pedidos.ProductosSinPedidos();
        return mapper.Map<List<object>>(pedido);
    }

    [HttpGet("PedidosPorEstado")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> PedidosPorEstado()
    {
        var pedido = await unitofwork.Pedidos.PedidosPorEstado();
        return mapper.Map<List<object>>(pedido);
    }

    [HttpGet("ProductosEnPedidos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ProductosEnPedidos()
    {
        var pedido = await unitofwork.Pedidos.ProductosEnPedidos();
        return mapper.Map<List<object>>(pedido);
    }

    [HttpGet("SumaCantidadTotalEnPedidos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> SumaCantidadTotalEnPedidos()
    {
        var pedido = await unitofwork.Pedidos.SumaCantidadTotalEnPedidos();
        return mapper.Map<List<object>>(pedido);
    }

    [HttpGet("ProductosMasVendidos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ProductosMasVendidos()
    {
        var pedido = await unitofwork.Pedidos.ProductosMasVendidos();
        return mapper.Map<List<object>>(pedido);
    }

    [HttpGet("ProductosMasVendidosPorCodigo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ProductosMasVendidosPorCodigo()
    {
        var pedido = await unitofwork.Pedidos.ProductosMasVendidosPorCodigo();
        return mapper.Map<List<object>>(pedido);
    }

    [HttpGet("ProductosMasVendidosPorOR")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ProductosMasVendidosPorOR()
    {
        var pedido = await unitofwork.Pedidos.ProductosMasVendidosPorOR();
        return mapper.Map<List<object>>(pedido);
    }

    [HttpGet("VentasMayoresA3000Iva")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> VentasMayoresA3000Iva()
    {
        var pedido = await unitofwork.Pedidos.VentasMayoresA3000Iva();
        return mapper.Map<List<object>>(pedido);
    }
}
