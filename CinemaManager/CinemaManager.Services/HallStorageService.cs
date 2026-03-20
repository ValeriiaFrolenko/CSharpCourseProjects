using CinemaManager.DTOs.Halls;
using CinemaManager.Repositories;

namespace CinemaManager.Storage
{
    public class HallStorageService : IHallStorageService
    {
        private readonly IHallRepository _hallRepository;
        private readonly ISessionRepository _sessionRepository;

        public HallStorageService(IHallRepository hallRepository, ISessionRepository sessionRepository)
        {
            _hallRepository = hallRepository;
            _sessionRepository = sessionRepository;
        }

        public int GetHallsCount()
        {
            return _hallRepository.GetHallsCount();
        }

        public HallDetailsDTO? GetHallById(Guid id)
        {
            var hallDB = _hallRepository.GetHallById(id);
            if (hallDB == null)
                return null;
            return new HallDetailsDTO(
                hallDB.Id, 
                hallDB.Name, 
                hallDB.NumberOfSeats, 
                hallDB.CinemaHallType);
        }

        public IEnumerable<HallListDTO> GetAllHalls()
        {
            return _hallRepository.GetAllHalls()
                .Select(h => new HallListDTO(
                    h.Id, 
                    h.Name, 
                    _sessionRepository.GetSessionsCountByHallId(h.Id)
                    ));
        }
    }
}