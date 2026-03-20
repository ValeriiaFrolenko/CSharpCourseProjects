using CinemaManager.DTOs.Halls;
using CinemaManager.Pages;
using CinemaManager.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CinemaManager.ViewModels
{
    public partial class HallsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<HallListDTO> _halls;
        public HallsViewModel(IHallStorageService hallStorageService)
        {
            var halls = hallStorageService.GetAllHalls();
            Halls = new ObservableCollection<HallListDTO>(halls);
        }

        [RelayCommand]
        private async Task LoadHallDetails(Guid hallId)
        {
            await Shell.Current.GoToAsync($"{nameof(HallDetailsPage)}?id={hallId}");
        }
    }
}