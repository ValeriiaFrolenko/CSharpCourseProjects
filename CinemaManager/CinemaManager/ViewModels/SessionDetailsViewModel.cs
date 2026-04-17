using CinemaManager.Common.Enums;
using CinemaManager.DTOs.Sessions;
using CinemaManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CinemaManager.ViewModels
{
    public partial class SessionDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ISessionStorageService _sessionStorageService;
        private Guid _sessionId;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        [ObservableProperty]
        private SessionDetailsDTO? _session;

        // Edit mode

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsDisplayMode))]
        private bool _isEditMode;

        [ObservableProperty]
        private string _editMovieName = string.Empty;

        [ObservableProperty]
        private FilmGenre _editGenre;

        [ObservableProperty]
        private string _editYearText = string.Empty;

        [ObservableProperty]
        private DateTime _editDate = DateTime.Today;

        [ObservableProperty]
        private TimeSpan _editTime = TimeSpan.Zero;

        [ObservableProperty]
        private string _editDurationText = string.Empty;

        public bool IsNotBusy => !IsBusy;
        public bool IsDisplayMode => !IsEditMode;

        public IReadOnlyList<FilmGenre> FilmGenres { get; } =
            Enum.GetValues<FilmGenre>().ToList();

        public SessionDetailsViewModel(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _sessionId = Guid.Parse((string)query["id"]);
        }

        public async Task LoadAsync()
        {
            IsBusy = true;
            try
            {
                Session = await _sessionStorageService.GetSessionByIdAsync(_sessionId);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void StartEdit()
        {
            if (Session is null) return;
            EditMovieName = Session.MovieName;
            EditGenre = Session.FilmGenre;
            EditYearText = Session.YearOfRelease.ToString();
            EditDate = Session.StartTime.Date;
            EditTime = Session.StartTime.TimeOfDay;
            EditDurationText = Session.DurationInMinutes.ToString();
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
            if (Session is null) return;

            if (string.IsNullOrWhiteSpace(EditMovieName))
            {
                await Shell.Current.DisplayAlert("Validation", "Movie name is required.", "OK");
                return;
            }

            if (!int.TryParse(EditYearText, out int year) || year < 1900 || year > 2100)
            {
                await Shell.Current.DisplayAlert("Validation",
                    "Year of release must be between 1900 and 2100.", "OK");
                return;
            }

            if (!int.TryParse(EditDurationText, out int duration) || duration <= 0)
            {
                await Shell.Current.DisplayAlert("Validation",
                    "Duration must be a positive number of minutes.", "OK");
                return;
            }

            DateTime startTime = EditDate.Date + EditTime;

            IsBusy = true;
            try
            {
                var input = new SessionInputDTO(EditMovieName.Trim(), EditGenre,
                    year, startTime, duration);
                await _sessionStorageService.UpdateSessionAsync(_sessionId, input);
                IsEditMode = false;
                await LoadAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}