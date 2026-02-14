using CinemaManager.Common.Enums;

namespace CinemaManager.DBModels
{
    public class CinemaHallDBModel
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }
        public CinemaHallType CinemaHallType { get; set; }

        public CinemaHallDBModel(string name, int numberOfSeats, CinemaHallType cinemaHallType)
        {
            Id = Guid.NewGuid();
            Name = name;
            NumberOfSeats = numberOfSeats;
            CinemaHallType = cinemaHallType;
        }
    }
}
