using AgenceImmo.Business.Interfaces;
using AgenceImmo.Business.Services;
using AgenceImmo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BaseMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string connectionString = "Server=.;Database=AgenceImmobiliere;Trusted_Connection=True;TrustServerCertificate=True;";

            builder.Services.AddDbContext<AgenceImmobiliereContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure()));

            builder.Services.AddScoped<IBienService, BienService>();
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<ListeBiensPage>();
            builder.Services.AddTransient<DetailsBienPage>();
            builder.Services.AddTransient<NouveauBienPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
