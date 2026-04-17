using CinemaManager.Common.Enums;
using CinemaManager.DTOs.Halls;
using CinemaManager.Pages;
using CinemaManager.Services;
using CinemaManager.ViewModels.Forms;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CinemaManager.ViewModels
{
    public partial class HallsViewModel : ObservableObject
    {
        private readonly IHallStorageService _hallStorageService;
        private List<HallListDTO> _allHalls = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        [ObservableProperty]
        private ObservableCollection<HallListDTO> _halls = [];

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private bool _isAddingHall;

        [ObservableProperty]
        private HallSortOption _selectedSortOption = HallSortOption.NameAscending;

        public bool IsNotBusy => !IsBusy;

        public HallFormViewModel AddForm { get; }

        public IReadOnlyList<HallSortOption> SortOptions { get; } = Enum.GetValues<HallSortOption>().ToList();

        public HallsViewModel(IHallStorageService hallStorageService)
        {
            _hallStorageService = hallStorageService;

            AddForm = new HallFormViewModel();
            AddForm.SubmitHandler = SaveNewHallAsync;
            AddForm.CancelHandler = () =>
            {
                IsAddingHall = false;
                AddForm.Reset();
            };
        }

        partial void OnSearchTextChanged(string value) => ApplyFilter();

        partial void OnSelectedSortOptionChanged(HallSortOption value) => ApplyFilter();

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

            filtered = SelectedSortOption switch
            {
                HallSortOption.NameDescending => filtered.OrderByDescending(h => h.Name),
                HallSortOption.SessionsCountAscending => filtered.OrderBy(h => h.NumberOfSessions),
                HallSortOption.SessionsCountDescending => filtered.OrderByDescending(h => h.NumberOfSessions),
                _ => filtered.OrderBy(h => h.Name)
            };

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
            AddForm.Reset();
            IsAddingHall = true;
        }

        [RelayCommand]
        private async Task DeleteHallAsync(Guid hallId)
        {
            bool confirmed = await Shell.Current.DisplayAlert(
                "Delete Hall",
                "This will also delete all sessions in this hall. Continue?",
                "Delete", "Cancel");

            if (!confirmed) return;

            IsBusy = true;
            try
            {
                await _hallStorageService.DeleteHallAsync(hallId);
                await LoadAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                await LoadAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SaveNewHallAsync()
        {
            IsBusy = true;
            try
            {
                var input = new HallInputDTO(AddForm.Name.Trim(), AddForm.ParsedSeats, AddForm.HallType);
                await _hallStorageService.AddHallAsync(input);
                IsAddingHall = false;
                AddForm.Reset();
                await LoadAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}