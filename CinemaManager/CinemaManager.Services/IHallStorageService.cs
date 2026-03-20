using CinemaManager.DTOs.Halls;

namespace CinemaManager.Services
{
    public interface IHallStorageService
    {
        int GetHallsCount();
        HallDetailsDTO? GetHallById(Guid id);
        IEnumerable<HallListDTO> GetAllHalls();
    }
}