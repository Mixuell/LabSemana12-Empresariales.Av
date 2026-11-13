using System.ComponentModel.DataAnnotations;

namespace Lab_Semana12.Models;

public class Instructor
{
    [Key]
    public int IdInstructor { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Especialidad { get; set; } = string.Empty;

    [Required]
    [StringLength(254)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public bool Activo { get; set; }
}
