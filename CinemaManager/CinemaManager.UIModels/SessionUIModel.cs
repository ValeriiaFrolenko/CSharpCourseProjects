using CinemaManager.Common.Enums;
using CinemaManager.DBModels;

namespace CinemaManager.UIModels
{
    public class SessionUIModel
    {
        private Guid _id;
        private Guid _cinemaHallId;
        private string _movieName;
        private FilmGenre _filmGenre;
        private int _yearOfRelease;
        private DateTime _startTime;
        private int _durationInMinutes;

        public SessionUIModel(Guid cinemaHallId, string movieName, FilmGenre filmGenre, int yearOfRelease, DateTime startTime, int durationInMinutes)
        {
            _id = Guid.NewGuid();
            _cinemaHallId = cinemaHallId;
            _movieName = movieName;
            _filmGenre = filmGenre;
            _yearOfRelease = yearOfRelease;
            _startTime = startTime;
            DurationInMinutes = durationInMinutes;
        }

        public SessionUIModel(SessionDBModel sessionDBModel)
        {
            _id = sessionDBModel.Id;
            _cinemaHallId = sessionDBModel.CinemaHallId;
            _movieName = sessionDBModel.MovieName;
            _filmGenre = sessionDBModel.FilmGenre;
            _yearOfRelease = sessionDBModel.YearOfRelease;
            _startTime = sessionDBModel.StartTime;
            DurationInMinutes = sessionDBModel.DurationInMinutes;
        }

        public Guid Id
        {
            get => _id;
        }

        public Guid CinemaHallId
        {
            get => _cinemaHallId;
            set => _cinemaHallId = value;
        }

        public string MovieName
        {
            get => _movieName;
            set => _movieName = value;
        }

        public FilmGenre FilmGenre
        {
            get => _filmGenre;
            set => _filmGenre = value;
        }

        public int YearOfRelease
        {
            get => _yearOfRelease;
            set => _yearOfRelease = value;
        }

        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        public int DurationInMinutes
        {
            get => _durationInMinutes;
            set => _durationInMinutes = value;
        }

        public DateTime EndTime => _startTime.AddMinutes(_durationInMinutes);  
    }
}
