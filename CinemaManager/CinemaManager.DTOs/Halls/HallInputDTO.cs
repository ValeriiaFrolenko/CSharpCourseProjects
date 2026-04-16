using CinemaManager.Common.Enums;

namespace CinemaManager.DTOs.Halls
{
    public class HallInputDTO
    {
        public HallInputDTO(string name, int numberOfSeats, CinemaHallType cinemaHallType)
        {
            Name = name;
            NumberOfSeats = numberOfSeats;
            CinemaHallType = cinemaHallType;
        }

        public string Name { get; }
        public int NumberOfSeats { get; }
        public CinemaHallType CinemaHallType { get; }
    }
}