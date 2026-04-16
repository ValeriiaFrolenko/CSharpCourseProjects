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

        public Task<SessionDBModel?> GetSessionByIdAsync(Guid id) =>
            _storage.GetSessionByIdAsync(id);

        public Task<IEnumerable<SessionDBModel>> GetSessionsByHallIdAsync(Guid hallId) =>
            _storage.GetSessionsByHallIdAsync(hallId);

        public Task<int> GetSessionsCountByHallIdAsync(Guid hallId) =>
            _storage.GetSessionsCountByHallIdAsync(hallId);

        public Task<int> GetTotalDurationByHallIdAsync(Guid hallId) =>
            _storage.GetTotalDurationByHallIdAsync(hallId);

        public Task AddSessionAsync(SessionDBModel session) =>
            _storage.AddSessionAsync(session);

        public Task UpdateSessionAsync(SessionDBModel session) =>
            _storage.UpdateSessionAsync(session);

        public Task DeleteSessionAsync(Guid id) =>
            _storage.DeleteSessionAsync(id);

        public Task DeleteSessionsByHallIdAsync(Guid hallId) =>
            _storage.DeleteSessionsByHallIdAsync(hallId);
    }
}