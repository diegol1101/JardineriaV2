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

public class PagoController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public PagoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PagoDto>>> Get()
    {
        var pago = await unitofwork.Pagos.GetAllAsync();
        return mapper.Map<List<PagoDto>>(pago);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PagoDto>> Get(int id)
    {
        var pago = await unitofwork.Pagos.GetByIdAsync(id);
        if (pago == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PagoDto>(pago);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pago>> Post(PagoDto pagoDto)
    {
        var pago = this.mapper.Map<Pago>(pagoDto);
        this.unitofwork.Pagos.Add(pago);
        await unitofwork.SaveAsync();
        if (pago == null)
        {
            return BadRequest();
        }
        pagoDto.IdTransaccion = pago.IdTransaccion;
        return CreatedAtAction(nameof(Post), new { id = pagoDto.IdTransaccion }, pagoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PagoDto>> Put(int id, [FromBody] PagoDto pagoDto)
    {
        if (pagoDto == null)
        {
            return NotFound();
        }
        var pago = this.mapper.Map<Pago>(pagoDto);
        unitofwork.Pagos.Update(pago);
        await unitofwork.SaveAsync();
        return pagoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var pago = await unitofwork.Pagos.GetByIdAsync(id);
        if (pago == null)
        {
            return NotFound();
        }
        unitofwork.Pagos.Remove(pago);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("PagosEn2008ConPaypal")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PagoDto>>> PagosEn2008ConPaypal()
    {
        var pago = await unitofwork.Pagos.PagosEn2008ConPaypal();
        return mapper.Map<List<PagoDto>>(pago);
    }

    [HttpGet("MetodosDePago")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> MetodosDePago()
    {
        var pago = await unitofwork.Pagos.MetodosDePago();
        return mapper.Map<List<object>>(pago);
    }

    [HttpGet("PagoMedioEn2009")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<decimal?>> PagoMedioEn2009()
    {
        var pago = await unitofwork.Pagos.PagoMedioEn2009();
        return Ok(pago);
    }

    [HttpGet("SumaTotalPagosPorAño")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> SumaTotalPagosPorAño()
    {
        var pago = await unitofwork.Pagos.SumaTotalPagosPorAño();
        return mapper.Map<List<object>>(pago);
    }



}
