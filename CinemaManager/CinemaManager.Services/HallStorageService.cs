using CinemaManager.DBModels;
using CinemaManager.DTOs;
using CinemaManager.UIModels;

namespace CinemaManager.Storage
{
    public class HallStorageService : IHallStorageService
    {
        private readonly IStorageContext _storage;

        public HallStorageService(IStorageContext storage)
        {
            _storage = storage;
        }

        public int GetHallsCount()
        {
            return _storage.GetHalls().Count;
        }

        public HallUIModel? GetHallById(Guid id)
        {
            if (!_storage.TryGetHall(id, out var hallDB))
                return null;
            var sessionsByHall = GroupSessionsByHall();
            return CreateHallUIModel(hallDB!, sessionsByHall);
        }

        public List<HallUIModel> GetAllHalls()
        {
            var sessionsByHall = GroupSessionsByHall();
            return _storage.GetHalls().Values
                .Select(hallDB => CreateHallUIModel(hallDB, sessionsByHall))
                .ToList();
        }

        public List<HallListItemDTO> GetHallsSummary()
        {
            return _storage.GetHalls().Values
                .Select(h => new HallListItemDTO(h.Id, h.Name))
                .ToList();
        }

        private Dictionary<Guid, List<SessionUIModel>> GroupSessionsByHall()
        {
            return _storage.GetSessions().Values
                .GroupBy(s => s.CinemaHallId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(s => new SessionUIModel(s)).ToList()
                );
        }

        private HallUIModel CreateHallUIModel(HallDBModel hallDB, Dictionary<Guid, List<SessionUIModel>> sessionsByHall)
        {
            var sessions = sessionsByHall.GetValueOrDefault(hallDB.Id, new List<SessionUIModel>());
            return new HallUIModel(hallDB, sessions);
        }
    }
}