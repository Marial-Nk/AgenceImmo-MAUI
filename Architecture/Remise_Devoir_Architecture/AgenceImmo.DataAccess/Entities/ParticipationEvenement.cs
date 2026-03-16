using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgenceImmo.DataAccess.Enums;

namespace AgenceImmo.DataAccess.Entities;

[Table("ParticipationEvenement")]
public class ParticipationEvenement
{
    [Key]
    public int IdParticipation { get; set; }

    [Required]
    public int IdPersonne { get; set; }

    [Required]
    public int IdEvenement { get; set; }

    [Required]
    public RolePersonne RolePersonne { get; set; }

    // Navigation
    [ForeignKey(nameof(IdPersonne))]
    public Personne Personne { get; set; } = null!;

    [ForeignKey(nameof(IdEvenement))]
    public Evenement Evenement { get; set; } = null!;
}
