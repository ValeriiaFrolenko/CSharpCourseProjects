using CinemaManager.Common.Enums;

namespace CinemaManager.DTOs.Sessions
{
    public class SessionInputDTO
    {
        public SessionInputDTO(string movieName, FilmGenre filmGenre, int yearOfRelease,
            DateTime startTime, int durationInMinutes)
        {
            MovieName = movieName;
            FilmGenre = filmGenre;
            YearOfRelease = yearOfRelease;
            StartTime = startTime;
            DurationInMinutes = durationInMinutes;
        }

        public string MovieName { get; }
        public FilmGenre FilmGenre { get; }
        public int YearOfRelease { get; }
        public DateTime StartTime { get; }
        public int DurationInMinutes { get; }
    }
}