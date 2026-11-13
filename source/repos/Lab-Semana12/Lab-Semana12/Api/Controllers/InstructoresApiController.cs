using Lab_Semana12.Data;
using Lab_Semana12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_Semana12.Api.Controllers;

[ApiController]
[Route("api/instructores")]
public class InstructoresApiController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Instructor>>> GetAll()
    {
        var instructores = await _context.Instructores
            .OrderBy(instructor => instructor.Apellidos)
            .ThenBy(instructor => instructor.Nombres)
            .ToListAsync();

        return Ok(instructores);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Instructor>> GetById(int id)
    {
        var instructor = await _context.Instructores.FindAsync(id);
        return instructor is null ? NotFound() : Ok(instructor);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Instructor), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Instructor>> Create(Instructor instructor)
    {
        _context.Instructores.Add(instructor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = instructor.IdInstructor }, instructor);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, Instructor instructor)
    {
        if (id != instructor.IdInstructor)
        {
            return BadRequest();
        }

        var instructorActual = await _context.Instructores.FindAsync(id);
        if (instructorActual is null)
        {
            return NotFound();
        }

        instructorActual.Nombres = instructor.Nombres;
        instructorActual.Apellidos = instructor.Apellidos;
        instructorActual.Especialidad = instructor.Especialidad;
        instructorActual.Email = instructor.Email;
        instructorActual.Activo = instructor.Activo;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var instructor = await _context.Instructores.FindAsync(id);
        if (instructor is null)
        {
            return NotFound();
        }

        _context.Instructores.Remove(instructor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
