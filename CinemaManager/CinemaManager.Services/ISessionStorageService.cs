using CinemaManager.DTOs;
using CinemaManager.DTOs.Sessions;
using CinemaManager.UIModels;

namespace CinemaManager.Storage
{
    public interface ISessionStorageService
    {
        int GetSessionsCountByHallId(Guid hallId);
        SessionDetailsDTO? GetSessionById(Guid id);
        IEnumerable<SessionListDTO> GetSessionsByHallId(Guid hallId);
    }
}