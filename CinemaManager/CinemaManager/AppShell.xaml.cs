using CinemaManager.Pages;

namespace CinemaManager;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute($"{nameof(HallsPage)}/{nameof(HallDetailsPage)}", typeof(HallDetailsPage));
        Routing.RegisterRoute($"{nameof(HallsPage)}/{nameof(HallDetailsPage)}/{nameof(SessionDetailsPage)}", typeof(SessionDetailsPage));
    }
}