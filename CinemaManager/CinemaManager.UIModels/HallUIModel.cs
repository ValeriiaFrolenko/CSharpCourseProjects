using CinemaManager.Common.Enums;
using CinemaManager.DBModels;

namespace CinemaManager.UIModels
{
    public class HallUIModel
    {
        private Guid _id;
        private string _name;
        private int _numberOfSeats;
        private CinemaHallType _cinemaHallType;
        private List<SessionUIModel> _sessions;

        public HallUIModel(string name, int numberOfSeats, CinemaHallType cinemaHallType, List<SessionUIModel> sessionUIModels)
        {
            _id = Guid.NewGuid();
            _name = name;
            _numberOfSeats = numberOfSeats;
            _cinemaHallType = cinemaHallType;
            _sessions = sessionUIModels;
        }
    
        public HallUIModel(HallDBModel hallDBModel, List<SessionUIModel> sessionUIModels)
        {
            _id = hallDBModel.Id;
            _name = hallDBModel.Name;
            _numberOfSeats = hallDBModel.NumberOfSeats;
            _cinemaHallType = hallDBModel.CinemaHallType;
            _sessions = sessionUIModels;
        }

        public Guid Id
        {
            get => _id;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int NumberOfSeats
        {
            get => _numberOfSeats;
            set => _numberOfSeats = value;
        }

        public CinemaHallType CinemaHallType
        {
            get => _cinemaHallType;
            set => _cinemaHallType = value;
        }

        public IReadOnlyList<SessionUIModel> Sessions => _sessions.AsReadOnly();

        public void AddSession(SessionUIModel session)
        {
            _sessions.Add(session);
        }

        public void AddSessions(IEnumerable<SessionUIModel> sessions)
        {
            _sessions.AddRange(sessions);
        }

        public int TotalDurationOfSessions => _sessions.Sum(s => s.DurationInMinutes);

        public override string ToString()
        {
            return $"Hall: {Name}, Type: {CinemaHallType}, Seats: {NumberOfSeats}, Sessions: {Sessions.Count}, Total Duration: {TotalDurationOfSessions} min";
        }
    }
}
