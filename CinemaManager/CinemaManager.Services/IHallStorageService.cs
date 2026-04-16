using CinemaManager.DTOs.Halls;

namespace CinemaManager.Services
{
    public interface IHallStorageService
    {
        Task<HallDetailsDTO?> GetHallByIdAsync(Guid id);
        Task<IEnumerable<HallListDTO>> GetAllHallsAsync();
        Task AddHallAsync(HallInputDTO input);
        Task UpdateHallAsync(Guid id, HallInputDTO input);
        Task DeleteHallAsync(Guid id);
    }
}