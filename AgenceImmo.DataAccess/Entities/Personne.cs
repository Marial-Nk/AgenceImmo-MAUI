using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenceImmo.DataAccess.Entities;

[Table("Personne")]
public class Personne
{
    [Key]
    public int IdPersonne { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nom { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Prenom { get; set; }

    [MaxLength(50)]
    public string? Email { get; set; }

    public int? IdAdresse { get; set; }

    // Navigation
    [ForeignKey(nameof(IdAdresse))]
    public Adresse? Adresse { get; set; }

    public ICollection<Appartenance> Appartenances { get; set; } = new List<Appartenance>();
    public ICollection<ParticipationEvenement> Participations { get; set; } = new List<ParticipationEvenement>();
}
