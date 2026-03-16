using AgenceImmo.DataAccess.Enums;

namespace AgenceImmo.Business.Models;

public class FiltreRechercheBien
{
    public TypeBien? TypeBien { get; set; }
    public TypeContrat? TypeContrat { get; set; }
    public StatutBien? Statut { get; set; }
    public decimal? PrixMin { get; set; }
    public decimal? PrixMax { get; set; }
    public decimal? SurfaceMin { get; set; }
    public string? Ville { get; set; }
    public int? NbChambresMin { get; set; }
}
