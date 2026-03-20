using CinemaManager.ViewModels;

namespace CinemaManager.Pages;

public partial class SessionDetailsPage : ContentPage
{
    public SessionDetailsPage(SessionDetailsViewModel sessionDetailsViewModel)
    {
        InitializeComponent();
        BindingContext = sessionDetailsViewModel;
    }
}