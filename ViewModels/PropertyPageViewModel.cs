namespace BaseMauiApp.ViewModels;

public class EvenementViewModel
{
    public string Date { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class PropertyPageViewModel
{
    public int IdBien { get; set; }
    public string TypeBien { get; set; } = string.Empty;
    public string Statut { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<EvenementViewModel> Evenements { get; set; } = new();
}
