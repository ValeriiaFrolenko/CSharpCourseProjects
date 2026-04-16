using CinemaManager.DBModels;
using CinemaManager.Storage;

namespace CinemaManager.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly IStorageContext _storage;

        public HallRepository(IStorageContext storage)
        {
            _storage = storage;
        }

        public Task<HallDBModel?> GetHallByIdAsync(Guid id) =>
            _storage.GetHallByIdAsync(id);

        public Task<IEnumerable<HallDBModel>> GetAllHallsAsync() =>
            _storage.GetAllHallsAsync();

        public Task AddHallAsync(HallDBModel hall) =>
            _storage.AddHallAsync(hall);

        public Task UpdateHallAsync(HallDBModel hall) =>
            _storage.UpdateHallAsync(hall);

        public Task DeleteHallAsync(Guid id) =>
            _storage.DeleteHallAsync(id);
    }
}