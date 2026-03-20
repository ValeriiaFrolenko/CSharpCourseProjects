using CinemaManager.DTOs;
using CinemaManager.DTOs.Halls;

namespace CinemaManager.Storage
{
    public interface IHallStorageService
    {
        int GetHallsCount();
        HallDetailsDTO? GetHallById(Guid id);
        IEnumerable<HallListDTO> GetAllHalls();
    }
}