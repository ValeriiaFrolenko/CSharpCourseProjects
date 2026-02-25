using CinemaManager.DBModels;
using CinemaManager.UIModels;

namespace CinemaManager.Storage
{
    public class SessionStorageService
    {
        public int GetSessionsCount()
        {
            return Storage.GetSessions().Count;
        }

        public SessionUIModel? GetSessionById(Guid id)
        {
            if (!Storage.TryGetSession(id, out var sessionDB))
                return null;
            return new SessionUIModel(sessionDB!);
        }

        public List<SessionUIModel> GetSessions()
        {
            return Storage.GetSessions().Values
                .Select(sessionDB => new SessionUIModel(sessionDB))
                .ToList();
        }

        public List<SessionUIModel> GetSessionsByHallId(Guid hallId)
        {
            return Storage.GetSessions().Values
                .Where(s => s.CinemaHallId == hallId)
                .Select(sessionDB => new SessionUIModel(sessionDB))
                .ToList();
        }

        public List<SessionUIModel> GetSessionsByMovieName(string movieName)
        {
            return Storage.GetSessions().Values
                .Where(s => s.MovieName.Equals(movieName, StringComparison.OrdinalIgnoreCase))
                .Select(sessionDB => new SessionUIModel(sessionDB))
                .ToList();
        }

        public List<SessionUIModel> GetSessionsByHallName(string hallName)
        {
            var hallDB = Storage.GetHalls().Values.FirstOrDefault(h => h.Name.Equals(hallName, StringComparison.OrdinalIgnoreCase));
            if (hallDB == null)
                return new List<SessionUIModel>();
            return GetSessionsByHallId(hallDB.Id);
        }
    }
}
