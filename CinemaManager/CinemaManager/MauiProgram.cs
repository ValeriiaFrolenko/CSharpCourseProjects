using Microsoft.Extensions.Logging;
using CinemaManager.Composition;
using CinemaManager.Pages;

namespace CinemaManager
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

            CompositionRoot.Register(builder.Services);

            builder.Services.AddTransient<HallsPage>();
            builder.Services.AddTransient<HallDetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}