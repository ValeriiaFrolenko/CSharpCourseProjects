using CinemaManager.DTOs;
using CinemaManager.Storage;
using System.Collections.ObjectModel;

namespace CinemaManager.Pages;

public partial class HallsPage : ContentPage
{
    public ObservableCollection<HallListItemDTO> Halls { get; set; }

    public HallsPage(IHallStorageService hallStorageService)
    {
        InitializeComponent();

        var summary = hallStorageService.GetHallsSummary();
        Halls = new ObservableCollection<HallListItemDTO>(summary);

        HallsCollectionView.ItemsSource = Halls;
    }

    private async void OnHallButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Guid hallId)
        {
            await Shell.Current.GoToAsync($"{nameof(HallDetailsPage)}?id={hallId}");
        }
    }
}