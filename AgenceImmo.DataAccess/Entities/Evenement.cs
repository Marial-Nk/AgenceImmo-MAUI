using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgenceImmo.DataAccess.Enums;

namespace AgenceImmo.DataAccess.Entities;

[Table("Evenement")]
public class Evenement
{
    [Key]
    public int IdEvenement { get; set; }

    [Required]
    public TypeEvenement TypeEvenement { get; set; }

    public DateOnly? DateEvenement { get; set; }

    [MaxLength(50)]
    public string? Description { get; set; }

    [Required]
    public int IdUtilisateur { get; set; }

    [Required]
    public int IdBien { get; set; }

    // Navigation
    [ForeignKey(nameof(IdUtilisateur))]
    public Utilisateur Utilisateur { get; set; } = null!;

    [ForeignKey(nameof(IdBien))]
    public Bien Bien { get; set; } = null!;

    public ICollection<ParticipationEvenement> Participations { get; set; } = new List<ParticipationEvenement>();
}
