using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgenceImmo.DataAccess.Enums;

namespace AgenceImmo.DataAccess.Entities;

[Table("Utilisateur")]
public class Utilisateur
{
    [Key]
    public int IdUtilisateur { get; set; }

    [Required]
    [MaxLength(50)]
    public string NomUtilisateur { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Prenom { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Identifiant { get; set; }

    [MaxLength(50)]
    public string? HashPassword { get; set; }

    public RoleUtilisateur? Role { get; set; }

    public int? IdAdresse { get; set; }

    // Navigation
    [ForeignKey(nameof(IdAdresse))]
    public Adresse? Adresse { get; set; }

    public ICollection<Evenement> Evenements { get; set; } = new List<Evenement>();
}
