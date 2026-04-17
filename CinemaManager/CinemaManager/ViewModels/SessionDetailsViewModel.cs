using CinemaManager.DTOs.Sessions;
using CinemaManager.Services;
using CinemaManager.ViewModels.Forms;
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

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsDisplayMode))]
        private bool _isEditMode;

        public bool IsNotBusy => !IsBusy;
        public bool IsDisplayMode => !IsEditMode;

        public SessionFormViewModel EditForm { get; }

        public SessionDetailsViewModel(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;

            EditForm = new SessionFormViewModel();
            EditForm.SubmitHandler = SaveEditAsync;
            EditForm.CancelHandler = () => IsEditMode = false;
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
            EditForm.Populate(Session.MovieName, Session.FilmGenre,
                Session.YearOfRelease, Session.StartTime, Session.DurationInMinutes);
            IsEditMode = true;
        }

        private async Task SaveEditAsync()
        {
            IsBusy = true;
            try
            {
                var input = new SessionInputDTO(
                    EditForm.MovieName.Trim(),
                    EditForm.Genre,
                    EditForm.ParsedYear,
                    EditForm.ParsedStartTime,
                    EditForm.ParsedDuration);
                await _sessionStorageService.UpdateSessionAsync(_sessionId, input);
                IsEditMode = false;
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