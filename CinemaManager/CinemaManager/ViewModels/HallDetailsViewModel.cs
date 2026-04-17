using CinemaManager.DTOs.Halls;
using CinemaManager.DTOs.Sessions;
using CinemaManager.Pages;
using CinemaManager.Services;
using CinemaManager.ViewModels.Forms;
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
        private List<SessionListDTO> _allSessions = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        [ObservableProperty]
        private HallDetailsDTO? _hall;

        [ObservableProperty]
        private ObservableCollection<SessionListDTO> _sessions = [];

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsDisplayMode))]
        private bool _isEditMode;

        [ObservableProperty]
        private bool _isAddingSession;

        public bool IsNotBusy => !IsBusy;
        public bool IsDisplayMode => !IsEditMode;

        public HallFormViewModel EditForm { get; }
        public SessionFormViewModel AddSessionForm { get; }

        public HallDetailsViewModel(
            IHallStorageService hallStorageService,
            ISessionStorageService sessionStorageService)
        {
            _hallStorageService = hallStorageService;
            _sessionStorageService = sessionStorageService;

            EditForm = new HallFormViewModel();
            EditForm.SubmitHandler = SaveHallEditAsync;
            EditForm.CancelHandler = () => IsEditMode = false;

            AddSessionForm = new SessionFormViewModel();
            AddSessionForm.SubmitHandler = SaveNewSessionAsync;
            AddSessionForm.CancelHandler = () =>
            {
                IsAddingSession = false;
                AddSessionForm.Reset();
            };
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

        [RelayCommand]
        private void StartEdit()
        {
            if (Hall is null) return;
            EditForm.Populate(Hall.Name, Hall.NumberOfSeats, Hall.CinemaHallType);
            IsEditMode = true;
        }

        [RelayCommand]
        private async Task LoadSessionDetailsAsync(Guid sessionId)
        {
            await Shell.Current.GoToAsync($"{nameof(SessionDetailsPage)}?id={sessionId}");
        }

        [RelayCommand]
        private void ShowAddSessionForm()
        {
            AddSessionForm.Reset();
            IsAddingSession = true;
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

        private async Task SaveHallEditAsync()
        {
            IsBusy = true;
            try
            {
                var input = new HallInputDTO(EditForm.Name.Trim(), EditForm.ParsedSeats, EditForm.HallType);
                await _hallStorageService.UpdateHallAsync(_hallId, input);
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

        private async Task SaveNewSessionAsync()
        {
            IsBusy = true;
            try
            {
                var input = new SessionInputDTO(
                    AddSessionForm.MovieName.Trim(),
                    AddSessionForm.Genre,
                    AddSessionForm.ParsedYear,
                    AddSessionForm.ParsedStartTime,
                    AddSessionForm.ParsedDuration);
                await _sessionStorageService.AddSessionAsync(_hallId, input);
                IsAddingSession = false;
                AddSessionForm.Reset();
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