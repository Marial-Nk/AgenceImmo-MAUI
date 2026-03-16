using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgenceImmo.DataAccess.Enums;

namespace AgenceImmo.DataAccess.Entities;

[Table("Document")]
public class Document
{
    [Key]
    public int IdDocument { get; set; }

    [Required]
    [MaxLength(50)]
    public string NomDocument { get; set; } = string.Empty;

    [Required]
    public TypeDocument TypeDocument { get; set; }

    [Required]
    public DateOnly DateAjout { get; set; }

    [MaxLength(50)]
    public string? Url { get; set; }

    [Required]
    public int IdBien { get; set; }

    // Navigation
    [ForeignKey(nameof(IdBien))]
    public Bien Bien { get; set; } = null!;
}
