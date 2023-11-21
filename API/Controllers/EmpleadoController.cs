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

public class EmpleadoController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public EmpleadoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
    {
        var empleado = await unitofwork.Empleados.GetAllAsync();
        return mapper.Map<List<EmpleadoDto>>(empleado);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EmpleadoDto>> Get(int id)
    {
        var empleado = await unitofwork.Empleados.GetByIdAsync(id);
        if (empleado == null)
        {
            return NotFound();
        }
        return this.mapper.Map<EmpleadoDto>(empleado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Empleado>> Post(EmpleadoDto empleadoDto)
    {
        var empleado = this.mapper.Map<Empleado>(empleadoDto);
        this.unitofwork.Empleados.Add(empleado);
        await unitofwork.SaveAsync();
        if (empleado == null)
        {
            return BadRequest();
        }
        empleadoDto.CodigoEmpleado = empleado.CodigoEmpleado;
        return CreatedAtAction(nameof(Post), new { id = empleadoDto.CodigoEmpleado }, empleadoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody] EmpleadoDto empleadoDto)
    {
        if (empleadoDto == null)
        {
            return NotFound();
        }
        var empleado = this.mapper.Map<Empleado>(empleadoDto);
        unitofwork.Empleados.Update(empleado);
        await unitofwork.SaveAsync();
        return empleadoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var empleado = await unitofwork.Empleados.GetByIdAsync(id);
        if (empleado == null)
        {
            return NotFound();
        }
        unitofwork.Empleados.Remove(empleado);
        await unitofwork.SaveAsync();
        return NoContent();
    }


    [HttpGet("CuantosEmpleados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<object>> CuantosEmpleados()
    {
        var empleado = await unitofwork.Empleados.CuantosEmpleados();
        return Ok(empleado);
    }

    [HttpGet("EmpleadosSinVentas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> EmpleadosSinVentas()
    {
        var empleados = await unitofwork.Empleados.EmpleadosSinVentas();
        return mapper.Map<List<object>>(empleados);
    }

    
    [HttpGet("EmpleadosSinVentas4")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> EmpleadosSinVentas4()
    {
        var empleados = await unitofwork.Empleados.EmpleadosSinVentas4();
        return mapper.Map<List<object>>(empleados);
    }



}
