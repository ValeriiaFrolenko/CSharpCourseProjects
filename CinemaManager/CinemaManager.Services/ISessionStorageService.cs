using CinemaManager.DTOs;
using CinemaManager.UIModels;

namespace CinemaManager.Storage
{
    public interface ISessionStorageService
    {
        int GetSessionsCount();
        SessionUIModel? GetSessionById(Guid id);
        List<SessionUIModel> GetSessions();
        List<SessionUIModel> GetSessionsByHallId(Guid hallId);
        List<SessionListItemDTO> GetSessionsSummary();
    }
}