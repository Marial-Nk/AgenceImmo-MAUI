using Microsoft.Maui.Graphics;

namespace BaseMauiApp.ViewModels;

public class BienCardViewModel
{
    public int IdBien { get; set; }
    public string TypeBien { get; set; } = string.Empty;
    public string PrixFormate { get; set; } = string.Empty;
    public string Statut { get; set; } = string.Empty;
    public string Ville { get; set; } = string.Empty;
    public Color StatutColor { get; set; } = Colors.Gray;
}

public class EvenementDashboardViewModel
{
    public string Date { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Bien { get; set; } = string.Empty;
    public Color TypeColor { get; set; } = Colors.Gray;
}

public class DashboardViewModel
{
    public int TotalBiens { get; set; }
    public int BiensDisponibles { get; set; }
    public int BiensEnNegociation { get; set; }
    public int BiensVendus { get; set; }
    public List<EvenementDashboardViewModel> EvenementsRecents { get; set; } = new();
}
