using CinemaManager.Common.Enums;
using CinemaManager.DTOs.Halls;
using CinemaManager.Pages;
using CinemaManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CinemaManager.ViewModels
{
    public partial class HallsViewModel : ObservableObject
    {
        private readonly IHallStorageService _hallStorageService;
        private List<HallListDTO> _allHalls = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        [ObservableProperty]
        private ObservableCollection<HallListDTO> _halls = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private bool _isAddingHall;

        [ObservableProperty]
        private string _newHallName = string.Empty;

        [ObservableProperty]
        private string _newHallSeatsText = string.Empty;

        [ObservableProperty]
        private CinemaHallType _selectedNewHallType = CinemaHallType.Standard2D;

        public bool IsNotBusy => !IsBusy;

        public IReadOnlyList<CinemaHallType> HallTypes { get; } =
            Enum.GetValues<CinemaHallType>().ToList();

        public HallsViewModel(IHallStorageService hallStorageService)
        {
            _hallStorageService = hallStorageService;
        }

        partial void OnSearchTextChanged(string value) => ApplyFilter();

        public async Task LoadAsync()
        {
            IsBusy = true;
            try
            {
                _allHalls = (await _hallStorageService.GetAllHallsAsync()).ToList();
                ApplyFilter();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ApplyFilter()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allHalls
                : _allHalls.Where(h =>
                    h.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            Halls = new ObservableCollection<HallListDTO>(filtered);
        }

        [RelayCommand]
        private async Task LoadHallDetailsAsync(Guid hallId)
        {
            await Shell.Current.GoToAsync($"{nameof(HallDetailsPage)}?id={hallId}");
        }


        [RelayCommand]
        private void ShowAddHallForm()
        {
            NewHallName = string.Empty;
            NewHallSeatsText = string.Empty;
            SelectedNewHallType = CinemaHallType.Standard2D;
            IsAddingHall = true;
        }

        [RelayCommand]
        private void CancelAddHall()
        {
            IsAddingHall = false;
        }

        [RelayCommand]
        private async Task SaveNewHallAsync()
        {
            if (string.IsNullOrWhiteSpace(NewHallName))
            {
                await Shell.Current.DisplayAlert("Validation", "Hall name is required.", "OK");
                return;
            }

            if (!int.TryParse(NewHallSeatsText, out int seats) || seats <= 0)
            {
                await Shell.Current.DisplayAlert("Validation",
                    "Number of seats must be a positive integer.", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                var input = new HallInputDTO(NewHallName.Trim(), seats, SelectedNewHallType);
                await _hallStorageService.AddHallAsync(input);
                IsAddingHall = false;
                await LoadAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteHallAsync(Guid hallId)
        {
            bool confirmed = await Shell.Current.DisplayAlert(
                "Delete Hall",
                "This will also delete all sessions in this hall. Continue?",
                "Delete", "Cancel");

            if (!confirmed)
                return;

            IsBusy = true;
            try
            {
                await _hallStorageService.DeleteHallAsync(hallId);
                await LoadAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}