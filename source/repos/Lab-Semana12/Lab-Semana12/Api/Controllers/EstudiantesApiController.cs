using Lab_Semana12.Data;
using Lab_Semana12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_Semana12.Api.Controllers;

[ApiController]
[Route("api/estudiantes")]
public class EstudiantesApiController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Estudiante>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Estudiante>>> GetAll()
    {
        var estudiantes = await _context.Estudiantes
            .OrderBy(estudiante => estudiante.Apellidos)
            .ThenBy(estudiante => estudiante.Nombres)
            .ToListAsync();

        return Ok(estudiantes);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Estudiante), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Estudiante>> GetById(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        return estudiante is null ? NotFound() : Ok(estudiante);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Estudiante), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Estudiante>> Create(Estudiante estudiante)
    {
        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = estudiante.IdEstudiante }, estudiante);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, Estudiante estudiante)
    {
        if (id != estudiante.IdEstudiante)
        {
            return BadRequest();
        }

        var estudianteActual = await _context.Estudiantes.FindAsync(id);
        if (estudianteActual is null)
        {
            return NotFound();
        }

        estudianteActual.Nombres = estudiante.Nombres;
        estudianteActual.Apellidos = estudiante.Apellidos;
        estudianteActual.Email = estudiante.Email;
        estudianteActual.Telefono = estudiante.Telefono;
        estudianteActual.FechaRegistro = estudiante.FechaRegistro;
        estudianteActual.Activo = estudiante.Activo;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante is null)
        {
            return NotFound();
        }

        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
