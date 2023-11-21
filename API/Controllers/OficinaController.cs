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

public class OficinaController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public OficinaController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<OficinaDto>>> Get()
    {
        var oficina = await unitofwork.Oficinas.GetAllAsync();
        return mapper.Map<List<OficinaDto>>(oficina);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<OficinaDto>> Get(int id)
    {
        var oficina = await unitofwork.Oficinas.GetByIdAsync(id);
        if (oficina == null)
        {
            return NotFound();
        }
        return this.mapper.Map<OficinaDto>(oficina);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Oficina>> Post(OficinaDto oficinaDto)
    {
        var oficina = this.mapper.Map<Oficina>(oficinaDto);
        this.unitofwork.Oficinas.Add(oficina);
        await unitofwork.SaveAsync();
        if (oficina == null)
        {
            return BadRequest();
        }
        oficinaDto.CodigoOficina = oficina.CodigoOficina;
        return CreatedAtAction(nameof(Post), new { id = oficinaDto.CodigoOficina }, oficinaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<OficinaDto>> Put(int id, [FromBody] OficinaDto oficinaDto)
    {
        if (oficinaDto == null)
        {
            return NotFound();
        }
        var oficina = this.mapper.Map<Oficina>(oficinaDto);
        unitofwork.Oficinas.Update(oficina);
        await unitofwork.SaveAsync();
        return oficinaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var oficina = await unitofwork.Oficinas.GetByIdAsync(id);
        if (oficina == null)
        {
            return NotFound();
        }
        unitofwork.Oficinas.Remove(oficina);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("OficinasNoEmFrutales")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<OficinaDto>>> OficinasNoEmFrutales()
    {
        var oficina = await unitofwork.Oficinas.OficinasNoEmFrutales();
        return mapper.Map<List<OficinaDto>>(oficina);
    }
}
