using Microsoft.EntityFrameworkCore;
using Lab_Semana12.Models;

namespace Lab_Semana12.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Instructor> Instructores => Set<Instructor>();

    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
}
