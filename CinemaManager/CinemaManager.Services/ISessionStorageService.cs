using CinemaManager.DTOs.Sessions;

namespace CinemaManager.Services
{
    public interface ISessionStorageService
    {
        int GetSessionsCountByHallId(Guid hallId);
        SessionDetailsDTO? GetSessionById(Guid id);
        IEnumerable<SessionListDTO> GetSessionsByHallId(Guid hallId);
    }
}