using CinemaManager.DBModels;
using CinemaManager.Storage;

namespace CinemaManager.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IStorageContext _storage;

        public SessionRepository(IStorageContext storage)
        {
            _storage = storage;
        }

        public int GetSessionsCountByHallId(Guid hallId)
        {
            return _storage.GetSessions().Values.Count(s => s.CinemaHallId == hallId);
        }

        public SessionDBModel? GetSessionById(Guid id)
        {
            if (!_storage.TryGetSession(id, out var sessionDB))
                return null;
            return sessionDB;
        }

        public IEnumerable<SessionDBModel> GetSessionsByHallId(Guid hallId)
        {
            return _storage.GetSessions().Values.Where(s => s.CinemaHallId == hallId);
        }
    }
    }
