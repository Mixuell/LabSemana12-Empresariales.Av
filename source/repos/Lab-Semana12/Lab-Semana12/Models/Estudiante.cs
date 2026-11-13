using System.ComponentModel.DataAnnotations;

namespace Lab_Semana12.Models;

public class Estudiante
{
    [Key]
    public int IdEstudiante { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    [StringLength(254)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Telefono { get; set; } = string.Empty;

    [Required]
    public DateTime FechaRegistro { get; set; }

    public bool Activo { get; set; }
}
