using CinemaManager.Common.Enums;
using CinemaManager.DTOs.Halls;
using CinemaManager.DTOs.Sessions;
using CinemaManager.Pages;
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
        private Guid _hallId;
        private List<SessionListDTO> _allSessions = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        [ObservableProperty]
        private HallDetailsDTO? _hall;

        [ObservableProperty]
        private ObservableCollection<SessionListDTO> _sessions = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        // Edit mode

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsDisplayMode))]
        private bool _isEditMode;

        [ObservableProperty]
        private string _editName = string.Empty;

        [ObservableProperty]
        private string _editSeatsText = string.Empty;

        [ObservableProperty]
        private CinemaHallType _editHallType;

        // Add session form 

        [ObservableProperty]
        private bool _isAddingSession;

        [ObservableProperty]
        private string _newSessionMovieName = string.Empty;

        [ObservableProperty]
        private FilmGenre _selectedNewSessionGenre = FilmGenre.Action;

        [ObservableProperty]
        private string _newSessionYearText = string.Empty;

        [ObservableProperty]
        private DateTime _newSessionDate = DateTime.Today;

        [ObservableProperty]
        private TimeSpan _newSessionTime = TimeSpan.Zero;

        [ObservableProperty]
        private string _newSessionDurationText = string.Empty;


        public bool IsNotBusy => !IsBusy;
        public bool IsDisplayMode => !IsEditMode;

        public IReadOnlyList<CinemaHallType> HallTypes { get; } =
            Enum.GetValues<CinemaHallType>().ToList();

        public IReadOnlyList<FilmGenre> FilmGenres { get; } =
            Enum.GetValues<FilmGenre>().ToList();

        public HallDetailsViewModel(IHallStorageService hallStorageService,
            ISessionStorageService sessionStorageService)
        {
            _hallStorageService = hallStorageService;
            _sessionStorageService = sessionStorageService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _hallId = Guid.Parse((string)query["id"]);
        }

        partial void OnSearchTextChanged(string value) => ApplyFilter();

        public async Task LoadAsync()
        {
            IsBusy = true;
            try
            {
                Hall = await _hallStorageService.GetHallByIdAsync(_hallId);
                _allSessions = (await _sessionStorageService.GetSessionsByHallIdAsync(_hallId)).ToList();
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
                ? _allSessions
                : _allSessions.Where(s =>
                    s.MovieName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            Sessions = new ObservableCollection<SessionListDTO>(
                filtered.OrderBy(s => s.StartTime));
        }

        // Hall edit commands

        [RelayCommand]
        private void StartEdit()
        {
            if (Hall is null) return;
            EditName = Hall.Name;
            EditSeatsText = Hall.NumberOfSeats.ToString();
            EditHallType = Hall.CinemaHallType;
            IsEditMode = true;
        }

        [RelayCommand]
        private void CancelEdit()
        {
            IsEditMode = false;
        }

        [RelayCommand]
        private async Task SaveEditAsync()
        {
            if (Hall is null) return;

            if (string.IsNullOrWhiteSpace(EditName))
            {
                await Shell.Current.DisplayAlert("Validation", "Hall name is required.", "OK");
                return;
            }

            if (!int.TryParse(EditSeatsText, out int seats) || seats <= 0)
            {
                await Shell.Current.DisplayAlert("Validation",
                    "Number of seats must be a positive integer.", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                var input = new HallInputDTO(EditName.Trim(), seats, EditHallType);
                await _hallStorageService.UpdateHallAsync(Hall.Id, input);
                IsEditMode = false;
                await LoadAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Session commands

        [RelayCommand]
        private async Task LoadSessionDetailsAsync(Guid sessionId)
        {
            await Shell.Current.GoToAsync($"{nameof(SessionDetailsPage)}?id={sessionId}");
        }


        [RelayCommand]
        private void ShowAddSessionForm()
        {
            NewSessionMovieName = string.Empty;
            SelectedNewSessionGenre = FilmGenre.Action;
            NewSessionYearText = DateTime.Today.Year.ToString();
            NewSessionDate = DateTime.Today;
            NewSessionTime = TimeSpan.FromHours(12);
            NewSessionDurationText = string.Empty;
            IsAddingSession = true;
        }

        [RelayCommand]
        private void CancelAddSession()
        {
            IsAddingSession = false;
        }

        [RelayCommand]
        private async Task SaveNewSessionAsync()
        {
            if (string.IsNullOrWhiteSpace(NewSessionMovieName))
            {
                await Shell.Current.DisplayAlert("Validation", "Movie name is required.", "OK");
                return;
            }

            if (!int.TryParse(NewSessionYearText, out int year) || year < 1900 || year > 2100)
            {
                await Shell.Current.DisplayAlert("Validation",
                    "Year of release must be between 1900 and 2100.", "OK");
                return;
            }

            if (!int.TryParse(NewSessionDurationText, out int duration) || duration <= 0)
            {
                await Shell.Current.DisplayAlert("Validation",
                    "Duration must be a positive number of minutes.", "OK");
                return;
            }

            DateTime startTime = NewSessionDate.Date + NewSessionTime;

            IsBusy = true;
            try
            {
                var input = new SessionInputDTO(NewSessionMovieName.Trim(),
                    SelectedNewSessionGenre, year, startTime, duration);
                await _sessionStorageService.AddSessionAsync(_hallId, input);
                IsAddingSession = false;
                await LoadAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteSessionAsync(Guid sessionId)
        {
            bool confirmed = await Shell.Current.DisplayAlert(
                "Delete Session", "Delete this session?", "Delete", "Cancel");

            if (!confirmed) return;

            IsBusy = true;
            try
            {
                await _sessionStorageService.DeleteSessionAsync(sessionId);
                await LoadAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}