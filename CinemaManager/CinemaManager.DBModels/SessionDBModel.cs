using CinemaManager.Common.Enums;

namespace CinemaManager.DBModels
{
    public class SessionDBModel
    {
        public SessionDBModel(Guid cinemaHallId, string movieName, FilmGenre filmGenre, int yearOfRelease, DateTime startTime, int durationInMinutes)
        {
            Id = Guid.NewGuid();
            CinemaHallId = cinemaHallId;
            MovieName = movieName;
            FilmGenre = filmGenre;
            YearOfRelease = yearOfRelease;
            StartTime = startTime;
            DurationInMinutes = durationInMinutes;
        }

        public Guid Id { get; }
        public Guid CinemaHallId { get; }
        public string MovieName { get; set; }
        public FilmGenre FilmGenre { get; set; }
        public int YearOfRelease { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
