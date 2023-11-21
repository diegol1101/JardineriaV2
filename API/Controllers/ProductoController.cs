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

public class ProductoController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public ProductoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    {
        var producto = await unitofwork.Productos.GetAllAsync();
        return mapper.Map<List<ProductoDto>>(producto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProductoDto>> Get(int id)
    {
        var producto = await unitofwork.Productos.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        return this.mapper.Map<ProductoDto>(producto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Producto>> Post(ProductoDto productoDto)
    {
        var producto = this.mapper.Map<Producto>(productoDto);
        this.unitofwork.Productos.Add(producto);
        await unitofwork.SaveAsync();
        if (producto == null)
        {
            return BadRequest();
        }
        productoDto.CodigoProducto = producto.CodigoProducto;
        return CreatedAtAction(nameof(Post), new { id = productoDto.CodigoProducto }, productoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto productoDto)
    {
        if (productoDto == null)
        {
            return NotFound();
        }
        var producto = this.mapper.Map<Producto>(productoDto);
        unitofwork.Productos.Update(producto);
        await unitofwork.SaveAsync();
        return productoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var producto = await unitofwork.Productos.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        unitofwork.Productos.Remove(producto);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("GamaOrnamentales")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> GamaOrnamentales()
    {
        var producto = await unitofwork.Productos.GamaOrnamentales();
        return mapper.Map<List<object>>(producto);
    }

    [HttpGet("ProductoConPrecioMasAlto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<object>> ProductoConPrecioMasAlto()
    {
        var producto = await unitofwork.Productos.ProductoConPrecioMasAlto();
        return Ok(producto);
    }

    [HttpGet("ProductoMasVendido")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> ProductoMasVendido()
    {
        var producto = await unitofwork.Productos.ProductoMasVendido();
        return Ok(producto);
    }

    [HttpGet("ProductoPrecioMasCaro")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> ProductoPrecioMasCaro()
    {
        var producto = await unitofwork.Productos.ProductoPrecioMasCaro();
        return Ok(producto);
    }

    [HttpGet("ProductosSinPedidos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ProductosSinPedidos()
    {
        var producto = await unitofwork.Productos.ProductosSinPedidos();
        return mapper.Map<List<object>>(producto);
    }
}
