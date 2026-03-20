using CinemaManager.DBModels;
using CinemaManager.Storage;

namespace CinemaManager.Repositories
{
    public class HallRepository: IHallRepository
    {
        private readonly IStorageContext _storage;

        public HallRepository(IStorageContext storage)
        {
            _storage = storage;
        }

        public int GetHallsCount()
        {
            return _storage.GetHalls().Count;
        }

        public HallDBModel? GetHallById(Guid id)
        {
            if (!_storage.TryGetHall(id, out var hallDB))
                return null;
            return hallDB;
        }

        public IEnumerable<HallDBModel> GetAllHalls()
        {
            return _storage.GetHalls().Values;
        }
    }
}
