using CinemaManager.Repositories;
using CinemaManager.Storage;
using CinemaManager.Pages;
using CinemaManager.ViewModels;
using CinemaManager.Services;

namespace CinemaManager.Composition 
{
    public static class CompositionRoot
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<IStorageContext, InMemoryStorageContext>();
            services.AddSingleton<IHallRepository, HallRepository>();
            services.AddSingleton<ISessionRepository, SessionRepository>();
            services.AddSingleton<IHallStorageService, HallStorageService>();
            services.AddSingleton<ISessionStorageService, SessionStorageService>();

            services.AddTransient<HallsPage>();
            services.AddTransient<HallDetailsPage>();
            services.AddTransient<SessionDetailsPage>();

            services.AddTransient<HallsViewModel>();
            services.AddTransient<HallDetailsViewModel>();
            services.AddTransient<SessionDetailsViewModel>();
        }
    }
}