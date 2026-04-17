using CinemaManager.ViewModels;

namespace CinemaManager.Pages;

public partial class HallDetailsPage : ContentPage
{
    private readonly HallDetailsViewModel _viewModel;

    public HallDetailsPage(HallDetailsViewModel hallDetailsViewModel)
    {
        InitializeComponent();
        _viewModel = hallDetailsViewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}