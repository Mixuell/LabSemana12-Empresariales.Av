using Lab_Semana12.Data;
using Lab_Semana12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_Semana12.Controllers;

public class InstructoresController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        var instructores = await _context.Instructores
            .OrderBy(instructor => instructor.Apellidos)
            .ThenBy(instructor => instructor.Nombres)
            .ToListAsync();

        return View(instructores);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructores
            .FirstOrDefaultAsync(item => item.IdInstructor == id);

        return instructor is null ? NotFound() : View(instructor);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombres,Apellidos,Especialidad,Email,Activo")] Instructor instructor)
    {
        if (!ModelState.IsValid)
        {
            return View(instructor);
        }

        _context.Add(instructor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructores.FindAsync(id);
        return instructor is null ? NotFound() : View(instructor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("IdInstructor,Nombres,Apellidos,Especialidad,Email,Activo")] Instructor instructor)
    {
        if (id != instructor.IdInstructor)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(instructor);
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
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructores
            .FirstOrDefaultAsync(item => item.IdInstructor == id);

        return instructor is null ? NotFound() : View(instructor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var instructor = await _context.Instructores.FindAsync(id);
        if (instructor is not null)
        {
            _context.Instructores.Remove(instructor);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
