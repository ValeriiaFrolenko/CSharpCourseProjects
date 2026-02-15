using CinemaManager.UIModels;

namespace CinemaManager.Storage
{
    public class SessionStorageService
    {

        public List<SessionUIModel> GetSessionsByHallId(Guid hallId)
        {
            return Storage.sessions.Values
                .Where(s => s.CinemaHallId == hallId)
                .Select(sessionDB => new SessionUIModel(sessionDB))
                .ToList();
        }

        public List<SessionUIModel> GetSessions()
        {
            return Storage.sessions.Values
                .Select(sessionDB => new SessionUIModel(sessionDB))
                .ToList();
        }

        public SessionUIModel? GetSessionById(Guid id)
        {
            if (!Storage.sessions.TryGetValue(id, out var sessionDB))
                return null;
            return new SessionUIModel(sessionDB);
        }
    }
}
