using CinemaManager.Pages;

namespace CinemaManager;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(HallDetailsPage), typeof(HallDetailsPage));
        Routing.RegisterRoute(nameof(SessionDetailsPage), typeof(SessionDetailsPage));
    }
}