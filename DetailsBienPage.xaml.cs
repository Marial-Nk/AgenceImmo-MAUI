using AgenceImmo.Business.Interfaces;
using BaseMauiApp.ViewModels;

namespace BaseMauiApp;

[QueryProperty(nameof(PropertyId), "propertyId")]
public partial class DetailsBienPage : ContentPage
{
    private readonly IBienService _bienService;

    public int PropertyId { get; set; }

    public DetailsBienPage(IBienService bienService)
    {
        InitializeComponent();
        _bienService = bienService;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        BindingContext = await InitPropertyPageViewModel(PropertyId);
    }

    private async void OnRetourDashboardTapped(object sender, TappedEventArgs e)
        => await Shell.Current.GoToAsync("//dashboard");

    private async void OnListeBiensTapped(object sender, TappedEventArgs e)
        => await Shell.Current.GoToAsync("listebiens");

    async Task<PropertyPageViewModel> InitPropertyPageViewModel(int propertyID)
    {
        var bien = await _bienService.GetBienAvecEvenementsAsync(propertyID);

        if (bien == null)
            return new PropertyPageViewModel { Description = "Bien introuvable." };

        return new PropertyPageViewModel
        {
            IdBien = bien.IdBien,
            TypeBien = bien.TypeBien.ToString(),
            Statut = bien.StatutActuel.ToString(),
            Description = bien.Description ?? string.Empty,
            Evenements = bien.Evenements
                .OrderByDescending(e => e.DateEvenement)
                .Select(e => new EvenementViewModel
                {
                    Date = e.DateEvenement?.ToString("dd/MM/yyyy") ?? "Date inconnue",
                    Type = e.TypeEvenement.ToString(),
                    Description = e.Description ?? string.Empty
                })
                .ToList()
        };
    }
}
