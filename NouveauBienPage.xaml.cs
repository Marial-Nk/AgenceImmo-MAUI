namespace BaseMauiApp;

public partial class NouveauBienPage : ContentPage
{
    public NouveauBienPage()
    {
        InitializeComponent();
    }

    private async void OnRetourDashboardTapped(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//dashboard");

    private async void OnListeBiensTapped(object sender, TappedEventArgs e)
        => await Shell.Current.GoToAsync("listebiens");
}
