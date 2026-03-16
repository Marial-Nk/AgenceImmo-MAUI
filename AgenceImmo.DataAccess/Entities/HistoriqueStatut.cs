using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgenceImmo.DataAccess.Enums;

namespace AgenceImmo.DataAccess.Entities;

[Table("HistoriqueStatut")]
public class HistoriqueStatut
{
    [Key]
    public int IdHistorique { get; set; }

    [Required]
    public StatutBien Statut { get; set; }

    [Required]
    public DateOnly DateDebut { get; set; }

    public DateOnly? DateFin { get; set; }

    [Required]
    public int IdBien { get; set; }

    // Navigation
    [ForeignKey(nameof(IdBien))]
    public Bien Bien { get; set; } = null!;
}
