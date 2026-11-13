using Lab_Semana12.Data;
using Lab_Semana12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_Semana12.Controllers;

public class EstudiantesController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        var estudiantes = await _context.Estudiantes
            .OrderBy(estudiante => estudiante.Apellidos)
            .ThenBy(estudiante => estudiante.Nombres)
            .ToListAsync();

        return View(estudiantes);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes
            .FirstOrDefaultAsync(item => item.IdEstudiante == id);

        return estudiante is null ? NotFound() : View(estudiante);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombres,Apellidos,Email,Telefono,FechaRegistro,Activo")] Estudiante estudiante)
    {
        if (!ModelState.IsValid)
        {
            return View(estudiante);
        }

        _context.Add(estudiante);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes.FindAsync(id);
        return estudiante is null ? NotFound() : View(estudiante);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("IdEstudiante,Nombres,Apellidos,Email,Telefono,FechaRegistro,Activo")] Estudiante estudiante)
    {
        if (id != estudiante.IdEstudiante)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(estudiante);
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
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes
            .FirstOrDefaultAsync(item => item.IdEstudiante == id);

        return estudiante is null ? NotFound() : View(estudiante);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante is not null)
        {
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
