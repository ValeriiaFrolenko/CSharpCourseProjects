using CinemaManager.ViewModels;

namespace CinemaManager.Pages;

public partial class HallsPage : ContentPage
{

    public HallsPage(HallsViewModel hallsViewModel)
    {
        InitializeComponent();
        BindingContext = hallsViewModel;
    }
}