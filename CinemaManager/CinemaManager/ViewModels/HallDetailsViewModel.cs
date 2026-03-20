using CinemaManager.DTOs.Halls;
using CinemaManager.DTOs.Sessions;
using CinemaManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CinemaManager.ViewModels
{
    public partial class HallDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IHallStorageService _hallStorageService;
        private readonly ISessionStorageService _sessionStorageService;

        [ObservableProperty]
        private HallDetailsDTO? _hall;

        [ObservableProperty]
        private ObservableCollection<SessionListDTO>? _sessions;

        public HallDetailsViewModel(IHallStorageService hallStorageService, ISessionStorageService sessionStorageService)
        {
            _hallStorageService = hallStorageService;
            _sessionStorageService = sessionStorageService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var idString = (string)query["id"];
            var hallId = Guid.Parse(idString!);

            Hall = _hallStorageService.GetHallById(hallId);
            Sessions = new ObservableCollection<SessionListDTO>(_sessionStorageService.GetSessionsByHallId(hallId));
        }

        [RelayCommand]
        private async Task LoadSessionDetails(Guid sessionId)
        {
            await Shell.Current.GoToAsync($"SessionDetailsPage?id={sessionId}");
        }
    }
}