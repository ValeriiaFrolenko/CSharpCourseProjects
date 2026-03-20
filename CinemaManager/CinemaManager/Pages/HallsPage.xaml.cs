using CinemaManager.DTOs.Halls;
using CinemaManager.Storage;
using System.Collections.ObjectModel;

namespace CinemaManager.Pages;

public partial class HallsPage : ContentPage
{
    public ObservableCollection<HallListDTO> Halls { get; set; }

    public HallsPage(IHallStorageService hallStorageService)
    {
        InitializeComponent();
        var halls = hallStorageService.GetAllHalls();
        Halls = new ObservableCollection<HallListDTO>(halls);
        BindingContext = this;
    }
    private async void OnHallTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Guid hallId)
        {
            await Shell.Current.GoToAsync($"{nameof(HallDetailsPage)}?id={hallId}");
        }
    }
}