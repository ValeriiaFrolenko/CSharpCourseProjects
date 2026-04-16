using CinemaManager.DBModels;
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

        public async Task<SessionDetailsDTO?> GetSessionByIdAsync(Guid id)
        {
            var session = await _sessionRepository.GetSessionByIdAsync(id);
            if (session is null)
                return null;

            return new SessionDetailsDTO(session.Id, session.MovieName, session.FilmGenre,
                session.YearOfRelease, session.StartTime, session.DurationInMinutes);
        }

        public async Task<IEnumerable<SessionListDTO>> GetSessionsByHallIdAsync(Guid hallId)
        {
            var sessions = await _sessionRepository.GetSessionsByHallIdAsync(hallId);
            return sessions.Select(s => new SessionListDTO(s.Id, s.MovieName, s.StartTime));
        }

        public Task AddSessionAsync(Guid hallId, SessionInputDTO input)
        {
            var session = new SessionDBModel(hallId, input.MovieName, input.FilmGenre,
                input.YearOfRelease, input.StartTime, input.DurationInMinutes);
            return _sessionRepository.AddSessionAsync(session);
        }

        public async Task UpdateSessionAsync(Guid id, SessionInputDTO input)
        {
            var session = await _sessionRepository.GetSessionByIdAsync(id);
            if (session is null)
                return;

            session.MovieName = input.MovieName;
            session.FilmGenre = input.FilmGenre;
            session.YearOfRelease = input.YearOfRelease;
            session.StartTime = input.StartTime;
            session.DurationInMinutes = input.DurationInMinutes;

            await _sessionRepository.UpdateSessionAsync(session);
        }

        public Task DeleteSessionAsync(Guid id) =>
            _sessionRepository.DeleteSessionAsync(id);
    }
}