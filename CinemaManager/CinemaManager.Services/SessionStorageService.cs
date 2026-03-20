using CinemaManager.DTOs.Sessions;
using CinemaManager.Repositories;

namespace CinemaManager.Services
{
    public class SessionStorageService : ISessionStorageService
    {
        private readonly ISessionRepository _sessionRepository;
        public SessionStorageService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public int GetSessionsCountByHallId(Guid hallId)
        {
            return _sessionRepository.GetSessionsCountByHallId(hallId);
        }

        public SessionDetailsDTO? GetSessionById(Guid id)
        {
            var sessionDB = _sessionRepository.GetSessionById(id);
            if (sessionDB == null)
                return null;
            return new SessionDetailsDTO(
                sessionDB.Id,
                sessionDB.MovieName,
                sessionDB.FilmGenre,
                sessionDB.YearOfRelease,
                sessionDB.StartTime,
                sessionDB.DurationInMinutes
                );
        }

        public IEnumerable<SessionListDTO> GetSessionsByHallId(Guid hallId)
        {
            return _sessionRepository.GetSessionsByHallId(hallId)
                .Select(s => new SessionListDTO(s.Id, s.MovieName, s.StartTime));
        }

    }
}