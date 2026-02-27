using CinemaManager.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaManager.Composition
{
    public static class CompositionRoot
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<IStorageContext, InMemoryStorageContext>();
            services.AddSingleton<IHallStorageService, HallStorageService>();
            services.AddSingleton<ISessionStorageService, SessionStorageService>();
        }
    }
}