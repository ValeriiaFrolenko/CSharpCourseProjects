using CinemaManager.DBModels;
using CinemaManager.DTOs.Halls;
using CinemaManager.Repositories;

namespace CinemaManager.Services
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

        public async Task<HallDetailsDTO?> GetHallByIdAsync(Guid id)
        {
            var hall = await _hallRepository.GetHallByIdAsync(id);
            if (hall is null)
                return null;

            int totalDuration = await _sessionRepository.GetTotalDurationByHallIdAsync(id);

            return new HallDetailsDTO(hall.Id, hall.Name, hall.NumberOfSeats,
                hall.CinemaHallType, totalDuration);
        }

        public async Task<IEnumerable<HallListDTO>> GetAllHallsAsync()
        {
            var halls = await _hallRepository.GetAllHallsAsync();
            var result = new List<HallListDTO>();

            foreach (var hall in halls)
            {
                int sessionCount = await _sessionRepository.GetSessionsCountByHallIdAsync(hall.Id);
                int totalDuration = await _sessionRepository.GetTotalDurationByHallIdAsync(hall.Id);
                result.Add(new HallListDTO(hall.Id, hall.Name, sessionCount, totalDuration));
            }

            return result;
        }

        public Task AddHallAsync(HallInputDTO input)
        {
            var hall = new HallDBModel(input.Name, input.NumberOfSeats, input.CinemaHallType);
            return _hallRepository.AddHallAsync(hall);
        }

        public async Task UpdateHallAsync(Guid id, HallInputDTO input)
        {
            var hall = await _hallRepository.GetHallByIdAsync(id);
            if (hall is null)
                throw new KeyNotFoundException("Hall not found.");

            hall.Name = input.Name;
            hall.NumberOfSeats = input.NumberOfSeats;
            hall.CinemaHallType = input.CinemaHallType;

            await _hallRepository.UpdateHallAsync(hall);
        }

        public async Task DeleteHallAsync(Guid id)
        {
            var hall = await _hallRepository.GetHallByIdAsync(id);
            if (hall is null)
                throw new KeyNotFoundException("Hall not found.");

            await _sessionRepository.DeleteSessionsByHallIdAsync(id);
            await _hallRepository.DeleteHallAsync(id);
        }
    }
}