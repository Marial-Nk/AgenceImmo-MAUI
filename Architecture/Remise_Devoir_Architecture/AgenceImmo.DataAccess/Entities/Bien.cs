using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgenceImmo.DataAccess.Enums;

namespace AgenceImmo.DataAccess.Entities;

[Table("Bien")]
public class Bien
{
    [Key]
    public int IdBien { get; set; }

    [Required]
    public TypeBien TypeBien { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Prix { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Surface { get; set; }

    [MaxLength(1)]
    public string? PEB { get; set; }

    [Required]
    public int NbreChambre { get; set; }

    [Required]
    public int NbreFacade { get; set; }

    public string? Description { get; set; }

    [Required]
    public StatutBien StatutActuel { get; set; }

    public DateOnly? DateDisponible { get; set; }

    [Required]
    public DateOnly DateCreation { get; set; }

    public int? IdAdresse { get; set; }

    public TypeContrat? TypeContrat { get; set; }

    // Navigation
    [ForeignKey(nameof(IdAdresse))]
    public Adresse? Adresse { get; set; }

    public ICollection<Appartenance> Appartenances { get; set; } = new List<Appartenance>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<Evenement> Evenements { get; set; } = new List<Evenement>();
    public ICollection<HistoriqueStatut> HistoriqueStatuts { get; set; } = new List<HistoriqueStatut>();
}
