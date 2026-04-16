namespace CinemaManager.DTOs.Halls
{
    public class HallListDTO
    {
        public HallListDTO(Guid id, string name, int numberOfSessions, int totalDurationInMinutes)
        {
            Id = id;
            Name = name;
            NumberOfSessions = numberOfSessions;
            TotalDurationInMinutes = totalDurationInMinutes;
        }

        public Guid Id { get; }
        public string Name { get; }
        public int NumberOfSessions { get; }
        public int TotalDurationInMinutes { get; }

        /// <summary>Computed display string, e.g. "26:18".</summary>
        public string TotalDurationDisplay =>
            $"{TotalDurationInMinutes / 60}h {TotalDurationInMinutes % 60}m";
    }
}