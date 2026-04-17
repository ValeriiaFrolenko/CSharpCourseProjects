using CinemaManager.ViewModels;

namespace CinemaManager.Pages;

public partial class SessionDetailsPage : ContentPage
{
    private readonly SessionDetailsViewModel _viewModel;

    public SessionDetailsPage(SessionDetailsViewModel sessionDetailsViewModel)
    {
        InitializeComponent();
        _viewModel = sessionDetailsViewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}