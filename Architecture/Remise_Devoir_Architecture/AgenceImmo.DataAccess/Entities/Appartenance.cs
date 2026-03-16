using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenceImmo.DataAccess.Entities;

[Table("Appartenance")]
public class Appartenance
{
    [Key]
    public int IdAppartenance { get; set; }

    [Required]
    public int IdPersonne { get; set; }

    [Required]
    public int IdBien { get; set; }

    // Navigation
    [ForeignKey(nameof(IdPersonne))]
    public Personne Personne { get; set; } = null!;

    [ForeignKey(nameof(IdBien))]
    public Bien Bien { get; set; } = null!;
}
