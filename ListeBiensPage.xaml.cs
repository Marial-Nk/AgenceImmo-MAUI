using AgenceImmo.Business.Interfaces;
using AgenceImmo.DataAccess.Enums;
using BaseMauiApp.ViewModels;

namespace BaseMauiApp;

public partial class ListeBiensPage : ContentPage
{
    private readonly IBienService _bienService;

    public ListeBiensPage(IBienService bienService)
    {
        InitializeComponent();
        _bienService = bienService;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        BindingContext = await InitListeBiensViewModel();
        AjusterColonnes(Width);
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        AjusterColonnes(width);
    }

    private void AjusterColonnes(double largeur)
    {
        if (BiensGridLayout == null) return;
        BiensGridLayout.Span = largeur switch
        {
            < 600  => 1,
            < 1000 => 2,
            _      => 3
        };
    }

    private async Task<ListeBiensViewModel> InitListeBiensViewModel()
    {
        var biens = await _bienService.GetAllBiensAsync();

        return new ListeBiensViewModel
        {
            TotalBiens = biens.Count,
            Biens = biens
                .OrderByDescending(b => b.DateCreation)
                .Select(b => new BienCardViewModel
                {
                    IdBien = b.IdBien,
                    TypeBien = b.TypeBien.ToString(),
                    PrixFormate = b.TypeContrat == TypeContrat.Location
                        ? $"{b.Prix:N0} €/mois"
                        : $"{b.Prix:N0} €",
                    Statut = b.StatutActuel.ToString(),
                    Ville = b.Adresse?.Ville ?? "—",
                    StatutColor = b.StatutActuel switch
                    {
                        StatutBien.Disponible    => Color.FromArgb("#4AD395"),
                        StatutBien.EnNegociation => Color.FromArgb("#FFA726"),
                        StatutBien.Vendu         => Color.FromArgb("#42A5F5"),
                        StatutBien.Loue          => Color.FromArgb("#AB47BC"),
                        StatutBien.Retire        => Color.FromArgb("#EF5350"),
                        _                        => Colors.Gray
                    }
                })
                .ToList()
        };
    }

    private async void OnRetourDashboardTapped(object sender, TappedEventArgs e)
        => await Shell.Current.GoToAsync("//dashboard");

    private async void OnNouveauBienTapped(object sender, TappedEventArgs e)
        => await Shell.Current.GoToAsync("nouveaubien");

    private async void OnBienSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is BienCardViewModel bien)
        {
            ((CollectionView)sender).SelectedItem = null;
            await Shell.Current.GoToAsync($"detailsbien?propertyId={bien.IdBien}");
        }
    }
}
