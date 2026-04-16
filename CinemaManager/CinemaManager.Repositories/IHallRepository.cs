using CinemaManager.DBModels;

namespace CinemaManager.Repositories
{
    public interface IHallRepository
    {
        Task<HallDBModel?> GetHallByIdAsync(Guid id);
        Task<IEnumerable<HallDBModel>> GetAllHallsAsync();
        Task AddHallAsync(HallDBModel hall);
        Task UpdateHallAsync(HallDBModel hall);
        Task DeleteHallAsync(Guid id);
    }
}