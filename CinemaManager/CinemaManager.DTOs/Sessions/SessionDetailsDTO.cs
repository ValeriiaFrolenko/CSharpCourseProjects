using CinemaManager.Common.Enums;

namespace CinemaManager.DTOs.Sessions
{
    public class SessionDetailsDTO
    {
        public SessionDetailsDTO(Guid id, string movieName, FilmGenre filmGenre, int yearOfRelease, DateTime startTime, int durationInMinutes)
        {
            Id = id;
            MovieName = movieName;
            FilmGenre = filmGenre;
            YearOfRelease = yearOfRelease;
            StartTime = startTime;
            DurationInMinutes = durationInMinutes;
        }

        public Guid Id { get; }
        public string MovieName { get; }
        public FilmGenre FilmGenre { get; }
        public int YearOfRelease { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime => StartTime.AddMinutes(DurationInMinutes);
        public int DurationInMinutes { get; }

    }
}
