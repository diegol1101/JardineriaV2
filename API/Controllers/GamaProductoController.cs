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
public class GamaProductoController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public GamaProductoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<GamaProductoDto>>> Get()
    {
        var gamaProducto = await unitofwork.GamaProductos.GetAllAsync();
        return mapper.Map<List<GamaProductoDto>>(gamaProducto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<GamaProductoDto>> Get(int id)
    {
        var gamaProducto = await unitofwork.GamaProductos.GetByIdAsync(id);
        if (gamaProducto == null)
        {
            return NotFound();
        }
        return this.mapper.Map<GamaProductoDto>(gamaProducto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<GamaProducto>> Post(GamaProductoDto gamaProductoDto)
    {
        var gamaProducto = this.mapper.Map<GamaProducto>(gamaProductoDto);
        this.unitofwork.GamaProductos.Add(gamaProducto);
        await unitofwork.SaveAsync();
        if (gamaProducto == null)
        {
            return BadRequest();
        }
        gamaProductoDto.Gama = gamaProducto.Gama;
        return CreatedAtAction(nameof(Post), new { id = gamaProductoDto.Gama }, gamaProductoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<GamaProductoDto>> Put(int id, [FromBody] GamaProductoDto gamaProductoDto)
    {
        if (gamaProductoDto == null)
        {
            return NotFound();
        }
        var gamaProducto = this.mapper.Map<GamaProducto>(gamaProductoDto);
        unitofwork.GamaProductos.Update(gamaProducto);
        await unitofwork.SaveAsync();
        return gamaProductoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var gamaProducto = await unitofwork.GamaProductos.GetByIdAsync(id);
        if (gamaProducto == null)
        {
            return NotFound();
        }
        unitofwork.GamaProductos.Remove(gamaProducto);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
