using CinemaManager.DTOs;
using CinemaManager.UIModels;

namespace CinemaManager.Storage
{
    public interface IHallStorageService
    {
        int GetHallsCount();
        HallUIModel? GetHallById(Guid id);
        List<HallUIModel> GetAllHalls();
        List<HallListItemDTO> GetHallsSummary();
    }
}