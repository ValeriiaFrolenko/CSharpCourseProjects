using CinemaManager.Common.Enums;

namespace CinemaManager.DTOs.Halls
{
    public class HallDetailsDTO
    {
        public HallDetailsDTO(Guid id, string name, int numberOfSeats, CinemaHallType cinemaHallType)
        {
            Id = id;
            Name = name;
            NumberOfSeats = numberOfSeats;
            CinemaHallType = cinemaHallType;
        }
        public Guid Id { get; }
        public string Name { get; }
        public int NumberOfSeats { get; }
        public CinemaHallType CinemaHallType { get; }
    }
}
