using CinemaManager.ViewModels;

namespace CinemaManager.Pages;

public partial class HallDetailsPage : ContentPage
{
    public HallDetailsPage(HallDetailsViewModel hallDetailsViewModel)
    {
        InitializeComponent();
        BindingContext = hallDetailsViewModel;
    }
}