using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenceImmo.DataAccess.Entities;

[Table("Adresse")]
public class Adresse
{
    [Key]
    public int IdAdresse { get; set; }

    [Required]
    [MaxLength(50)]
    public string Rue { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? CodePostal { get; set; }

    [MaxLength(50)]
    public string? Ville { get; set; }

    [MaxLength(50)]
    public string? Pays { get; set; }

    // Navigation
    public ICollection<Bien> Biens { get; set; } = new List<Bien>();
    public ICollection<Personne> Personnes { get; set; } = new List<Personne>();
    public ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
