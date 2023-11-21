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

public class DetallePedidoController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public DetallePedidoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DetallePedidoDto>>> Get()
    {
        var detallePedido = await unitofwork.DetallePedidos.GetAllAsync();
        return mapper.Map<List<DetallePedidoDto>>(detallePedido);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DetallePedidoDto>> Get(int id)
    {
        var detallePedido = await unitofwork.DetallePedidos.GetByIdAsync(id);
        if (detallePedido == null)
        {
            return NotFound();
        }
        return this.mapper.Map<DetallePedidoDto>(detallePedido);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<DetallePedido>> Post(DetallePedidoDto detallePedidoDto)
    {
        var detallePedido = this.mapper.Map<DetallePedido>(detallePedidoDto);
        this.unitofwork.DetallePedidos.Add(detallePedido);
        await unitofwork.SaveAsync();
        if (detallePedido == null)
        {
            return BadRequest();
        }
        detallePedidoDto.CodigoPedido = detallePedido.CodigoPedido;
        return CreatedAtAction(nameof(Post), new { id = detallePedidoDto.CodigoPedido }, detallePedidoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DetallePedidoDto>> Put(int id, [FromBody] DetallePedidoDto detallePedidoDto)
    {
        if (detallePedidoDto == null)
        {
            return NotFound();
        }
        var detallePedido = this.mapper.Map<DetallePedido>(detallePedidoDto);
        unitofwork.DetallePedidos.Update(detallePedido);
        await unitofwork.SaveAsync();
        return detallePedidoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var detallePedido = await unitofwork.DetallePedidos.GetByIdAsync(id);
        if (detallePedido == null)
        {
            return NotFound();
        }
        unitofwork.DetallePedidos.Remove(detallePedido);
        await unitofwork.SaveAsync();
        return NoContent();
    }




}
