namespace BaseMauiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("detailsbien", typeof(DetailsBienPage));
            Routing.RegisterRoute("nouveaubien", typeof(NouveauBienPage));
            Routing.RegisterRoute("listebiens", typeof(ListeBiensPage));
        }
    }
}
