using CinemaManager.DBModels;

namespace CinemaManager.Repositories
{
    public interface ISessionRepository
    {
        Task<SessionDBModel?> GetSessionByIdAsync(Guid id);
        Task<IEnumerable<SessionDBModel>> GetSessionsByHallIdAsync(Guid hallId);
        Task<int> GetSessionsCountByHallIdAsync(Guid hallId);
        Task<int> GetTotalDurationByHallIdAsync(Guid hallId);
        Task AddSessionAsync(SessionDBModel session);
        Task UpdateSessionAsync(SessionDBModel session);
        Task DeleteSessionAsync(Guid id);
        Task DeleteSessionsByHallIdAsync(Guid hallId);
    }
}