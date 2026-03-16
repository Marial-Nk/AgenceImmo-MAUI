namespace BaseMauiApp.ViewModels;

public class ListeBiensViewModel
{
    public int TotalBiens { get; set; }
    public List<BienCardViewModel> Biens { get; set; } = new();
}
