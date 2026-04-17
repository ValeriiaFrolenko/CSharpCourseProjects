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

        public async Task AddSessionAsync(Guid hallId, SessionInputDTO input)
        {
            await ValidateSessionOverlapAsync(hallId, input.StartTime, input.DurationInMinutes);

            var session = new SessionDBModel(hallId, input.MovieName, input.FilmGenre,
                input.YearOfRelease, input.StartTime, input.DurationInMinutes);

            await _sessionRepository.AddSessionAsync(session);
        }

        public async Task UpdateSessionAsync(Guid id, SessionInputDTO input)
        {
            var session = await _sessionRepository.GetSessionByIdAsync(id);
            if (session is null)
                throw new KeyNotFoundException("Session not found.");

            await ValidateSessionOverlapAsync(session.CinemaHallId, input.StartTime, input.DurationInMinutes, id);

            session.MovieName = input.MovieName;
            session.FilmGenre = input.FilmGenre;
            session.YearOfRelease = input.YearOfRelease;
            session.StartTime = input.StartTime;
            session.DurationInMinutes = input.DurationInMinutes;

            await _sessionRepository.UpdateSessionAsync(session);
        }

        public Task DeleteSessionAsync(Guid id) =>
            _sessionRepository.DeleteSessionAsync(id);

        private async Task ValidateSessionOverlapAsync(Guid hallId, DateTime newStartTime, int duration, Guid? currentSessionId = null)
        {
            var existingSessions = await _sessionRepository.GetSessionsByHallIdAsync(hallId);
            var newEndTime = newStartTime.AddMinutes(duration);

            bool hasOverlap = existingSessions.Any(s =>
                s.Id != currentSessionId &&
                newStartTime < s.StartTime.AddMinutes(s.DurationInMinutes) &&
                newEndTime > s.StartTime
            );

            if (hasOverlap)
            {
                throw new InvalidOperationException("The selected time overlaps with another session in this hall.");
            }
        }
    }
}