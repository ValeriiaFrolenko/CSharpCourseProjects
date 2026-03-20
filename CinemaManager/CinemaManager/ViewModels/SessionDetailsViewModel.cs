using CinemaManager.DTOs.Sessions;
using CinemaManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CinemaManager.ViewModels
{
    public partial class SessionDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ISessionStorageService _sessionStorageService;

        [ObservableProperty]
        private SessionDetailsDTO? _session;

        public SessionDetailsViewModel(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var idString = (string)query["id"];
            var sessionId = Guid.Parse(idString!);
            Session = _sessionStorageService.GetSessionById(sessionId);
        }
    }
}
