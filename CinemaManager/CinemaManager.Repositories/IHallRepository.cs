using CinemaManager.DBModels;

namespace CinemaManager.Repositories
{
    public interface IHallRepository
    {
        int GetHallsCount();
        HallDBModel? GetHallById(Guid id);
        IEnumerable<HallDBModel> GetAllHalls();
    }
}
