using CinemaManager.DTOs.Halls;
using CinemaManager.DTOs.Sessions;
using CinemaManager.Storage;

namespace CinemaManager.Pages;

[QueryProperty(nameof(HallId), "id")]
public partial class HallDetailsPage : ContentPage
{
    private readonly IHallStorageService _hallStorageService;
    private readonly ISessionStorageService _sessionStorageService;
    private Guid _hallId;

    public HallDetailsDTO? Hall { get; set; }
    public IEnumerable<SessionListDTO> Sessions { get; set; } = Enumerable.Empty<SessionListDTO>();

    public string HallId
    {
        set
        {
            if (Guid.TryParse(value, out _hallId))
                LoadHallDetails();
        }
    }

    public HallDetailsPage(IHallStorageService hallStorageService, ISessionStorageService sessionStorageService)
    {
        InitializeComponent();
        _hallStorageService = hallStorageService;
        _sessionStorageService = sessionStorageService;
        BindingContext = this;
    }

    private void LoadHallDetails()
    {
        Hall = _hallStorageService.GetHallById(_hallId);
        Sessions = _sessionStorageService.GetSessionsByHallId(_hallId);
        OnPropertyChanged(nameof(Hall));
        OnPropertyChanged(nameof(Sessions));
    }

    private async void OnSessionTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is not Guid sessionId)
            return;
        await Shell.Current.GoToAsync($"{nameof(SessionDetailsPage)}?id={sessionId}");
    }
}