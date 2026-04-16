using CinemaManager.DTOs.Sessions;

namespace CinemaManager.Services
{
    public interface ISessionStorageService
    {
        Task<SessionDetailsDTO?> GetSessionByIdAsync(Guid id);
        Task<IEnumerable<SessionListDTO>> GetSessionsByHallIdAsync(Guid hallId);
        Task AddSessionAsync(Guid hallId, SessionInputDTO input);
        Task UpdateSessionAsync(Guid id, SessionInputDTO input);
        Task DeleteSessionAsync(Guid id);
    }
}