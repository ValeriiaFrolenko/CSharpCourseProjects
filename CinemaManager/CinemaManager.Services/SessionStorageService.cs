using CinemaManager.DTOs;
using CinemaManager.UIModels;

namespace CinemaManager.Storage
{
    public class SessionStorageService : ISessionStorageService
    {
        private readonly IStorageContext _storage;

        public SessionStorageService(IStorageContext storage)
        {
            _storage = storage;
        }

        public int GetSessionsCount()
        {
            return _storage.GetSessions().Count;
        }

        public SessionUIModel? GetSessionById(Guid id)
        {
            if (!_storage.TryGetSession(id, out var sessionDB))
                return null;
            return new SessionUIModel(sessionDB!);
        }

        public List<SessionUIModel> GetSessions()
        {
            return _storage.GetSessions().Values
                .Select(sessionDB => new SessionUIModel(sessionDB))
                .ToList();
        }

        public List<SessionUIModel> GetSessionsByHallId(Guid hallId)
        {
            return _storage.GetSessions().Values
                .Where(s => s.CinemaHallId == hallId)
                .Select(sessionDB => new SessionUIModel(sessionDB))
                .ToList();
        }

        public List<SessionListItemDTO> GetSessionsSummary()
        {
            return _storage.GetSessions().Values
                .Select(s => new SessionListItemDTO(s.Id, s.MovieName, s.StartTime))
                .ToList();
        }
    }
}