using CinemaManager.UIModels;

namespace CinemaManager.Storage
{
    public class SessionStorageService
    {
        public int GetSessionsCount()
        {
            return Storage.sessions.Count;
        }
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

        public List<SessionUIModel> GetSessionsByMovieName(string movieName)
        {
            return Storage.sessions.Values
                .Where(s => s.MovieName.Equals(movieName, StringComparison.OrdinalIgnoreCase))
                .Select(sessionDB => new SessionUIModel(sessionDB))
                .ToList();
        }

        public List<SessionUIModel> GetSessionsByHallName(string hallName)
        {
            var hallDB = Storage.halls.Values.FirstOrDefault(h => h.Name.Equals(hallName, StringComparison.OrdinalIgnoreCase));
            if (hallDB == null)
                return new List<SessionUIModel>();
            return GetSessionsByHallId(hallDB.Id);
        }
    }
}
