using CinemaManager.ViewModels;

namespace CinemaManager.Pages;

public partial class HallsPage : ContentPage
{
    private readonly HallsViewModel _viewModel;

    public HallsPage(HallsViewModel hallsViewModel)
    {
        InitializeComponent();
        _viewModel = hallsViewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}