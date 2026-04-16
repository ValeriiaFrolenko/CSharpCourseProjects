using CinemaManager.Common.Enums;

namespace CinemaManager.DTOs.Halls
{
    public class HallDetailsDTO
    {
        public HallDetailsDTO(Guid id, string name, int numberOfSeats,
            CinemaHallType cinemaHallType, int totalDurationInMinutes)
        {
            Id = id;
            Name = name;
            NumberOfSeats = numberOfSeats;
            CinemaHallType = cinemaHallType;
            TotalDurationInMinutes = totalDurationInMinutes;
        }

        public Guid Id { get; }
        public string Name { get; }
        public int NumberOfSeats { get; }
        public CinemaHallType CinemaHallType { get; }
        public int TotalDurationInMinutes { get; }

        /// <summary>Computed display string, e.g. "26h 18m".</summary>
        public string TotalDurationDisplay =>
            $"{TotalDurationInMinutes / 60}h {TotalDurationInMinutes % 60}m";
    }
}