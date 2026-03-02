using CinemaManager.Storage;
using CinemaManager.UIModels;

namespace CinemaManager.Pages;

[QueryProperty(nameof(SessionId), "id")]
public partial class SessionDetailsPage : ContentPage
{
    private readonly ISessionStorageService _sessionStorageService;
    private Guid _sessionId;

    public SessionUIModel Session { get; set; }

    public string SessionId
    {
        set
        {
            if (Guid.TryParse(value, out _sessionId))
            {
                LoadSessionDetails();
            }
        }
    }

    public SessionDetailsPage(ISessionStorageService sessionStorageService)
    {
        InitializeComponent();
        _sessionStorageService = sessionStorageService;
        BindingContext = this;
    }

    private void LoadSessionDetails()
    {
        Session = _sessionStorageService.GetSessionById(_sessionId);
        OnPropertyChanged(nameof(Session));
    }
}