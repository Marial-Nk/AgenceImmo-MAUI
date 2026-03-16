using AgenceImmo.Business.Interfaces;
using AgenceImmo.DataAccess.Enums;
using BaseMauiApp.ViewModels;

namespace BaseMauiApp;

public partial class DashboardPage : ContentPage
{
    private readonly IBienService _bienService;

    public DashboardPage(IBienService bienService)
    {
        InitializeComponent();
        _bienService = bienService;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        BindingContext = await InitDashboardViewModel();
    }

    private async Task<DashboardViewModel> InitDashboardViewModel()
    {
        var biens = await _bienService.GetAllBiensAsync();
        var evenements = await _bienService.GetEvenementsRecentsAsync(8);

        return new DashboardViewModel
        {
            TotalBiens = biens.Count,
            BiensDisponibles = biens.Count(b => b.StatutActuel == StatutBien.Disponible),
            BiensEnNegociation = biens.Count(b => b.StatutActuel == StatutBien.EnNegociation),
            BiensVendus = biens.Count(b => b.StatutActuel == StatutBien.Vendu || b.StatutActuel == StatutBien.Loue),

            EvenementsRecents = evenements
                .Select(e => new EvenementDashboardViewModel
                {
                    Date = e.DateEvenement?.ToString("dd/MM/yyyy") ?? "—",
                    Type = e.TypeEvenement.ToString(),
                    Description = e.Description ?? string.Empty,
                    Bien = $"Bien #{e.IdBien} — {e.Bien?.TypeBien}",
                    TypeColor = e.TypeEvenement switch
                    {
                        TypeEvenement.Visite            => Color.FromArgb("#644CFF"),
                        TypeEvenement.Offre             => Color.FromArgb("#FFA726"),
                        TypeEvenement.SignatureCompromis => Color.FromArgb("#4AD395"),
                        TypeEvenement.SignatureActe      => Color.FromArgb("#42A5F5"),
                        TypeEvenement.EtatDesLieux       => Color.FromArgb("#AB47BC"),
                        _                                => Color.FromArgb("#888888")
                    }
                })
                .ToList()
        };
    }

    private async void OnNouveauBienTapped(object sender, TappedEventArgs e)
        => await Shell.Current.GoToAsync("nouveaubien");

    private async void OnListeBiensTapped(object sender, TappedEventArgs e)
        => await Shell.Current.GoToAsync("listebiens");
}
